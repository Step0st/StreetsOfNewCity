using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharactersSpawner))]
[RequireComponent(typeof(UIManager))]
public class GameManager : MonoBehaviour
{
    [Header("Game settings")] 
    public float cardLiftWhenDragged = 0.5f;
    public int timeForBotTurn = 5;
    [Header("All characters")]
    public List<CharacterManager> allies;
    public List<CharacterManager> enemies;
    public static int turn;
    public static Action newTurn;
    
    private Queue<CharacterManager> allCharactersQueue;
    private List<CharacterManager> alliesInGame;
    private List<CharacterManager> enemiesInGame;
    private CharactersSpawner _charactersSpawner;
    private UIManager uiManager;
    

    private void Start()
    {
        turn = 0;
        CardEvents.cardLift = cardLiftWhenDragged;
        _charactersSpawner = GetComponent<CharactersSpawner>();
        uiManager = GetComponent<UIManager>();
        CharacterManager.CharacterDead += CharacterDead;
    }
    
    public void StartGame()
    {
        allCharactersQueue = new Queue<CharacterManager>(alliesInGame.Concat(enemiesInGame).ToList());
        ChangeTurn();
    }

    public void SpawnCharacters(int alliesToSpawn, int enemiesToSpawn)
    {
        alliesInGame = _charactersSpawner.SpawnAlliesFromList(allies, alliesToSpawn);
        enemiesInGame = _charactersSpawner.SpawnEnemiesFromList(enemies, enemiesToSpawn);
    }
    
    private void CharacterDead(CharacterManager obj)
    {
        alliesInGame.RemoveAll(x => x == obj);
        enemiesInGame.RemoveAll(x => x == obj);
        allCharactersQueue = new Queue<CharacterManager>(allCharactersQueue.Where(x => x != obj));
        Destroy(obj.gameObject);
        CheckForGameEnd();
    }
    
    public void CheckForGameEnd()
    {
        if (alliesInGame.Count == 0)
        {
            uiManager.LoseGame();
            return;
        }
        if (enemiesInGame.Count == 0)
        {
            uiManager.WinGame();
        }
    }

    private IEnumerator AiActionTimer(PlayableCard card, CharacterManager character)
    {
        int actionTime = timeForBotTurn;
        while (actionTime-- > 0)
        {
            yield return new WaitForSeconds(1f);
        }
        var targetCharacter = BotTargeting.ChooseTarget(card, character, alliesInGame, enemiesInGame);
        EffectsManager.TakeEffect(card, targetCharacter);
        card.DestroyWithAnimation();
        ChangeTurn();
    }
    
    public void ChangeTurn()
    {
        StopAllCoroutines();
        turn++;
        var charTurn = allCharactersQueue.Dequeue();
        allCharactersQueue.Enqueue(charTurn);
        newTurn?.Invoke();
        
        if (charTurn.isPlayableCharacter)
        {
            uiManager.endTurnButton.interactable = true;
            var card = charTurn.DrawCard();
            CardEvents.cardPlayed = null;
            CardEvents.cardPlayed += (targetCharacter) =>
            {
                EffectsManager.TakeEffect(card, targetCharacter);
                card.DestroyCard();
            };
            uiManager.endTurnButton.onClick.AddListener(card.DestroyCard);
        }
        else
        {
            uiManager.endTurnButton.interactable = false;
            var card = charTurn.DrawCard();
            card.Deactivate();
            StartCoroutine(AiActionTimer(card, charTurn));
        }
    }
    
    private void OnDestroy()
    {
        CharacterManager.CharacterDead -= CharacterDead;
    }
}
