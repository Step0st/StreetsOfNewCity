using System;
using UnityEngine;

public class TemporaryEffectsManager : MonoBehaviour
{
    private CharacterManager _characterManager;

    private bool isPoisoned = false;
    private int poisonEffectDamage;
    [SerializeField] private MeshRenderer _poisonEffectImage;
    private int turnToEndPoison;

    private bool isArmored = false;
    private int turnToEndArmor;
    private void Start()
    {
        _characterManager = GetComponent<CharacterManager>();
        GameManager.newTurn += CheckEffects;
    }

    public void PoisonEffectsStart(int temporaryDamage, int duration)
    {
        int effectTurn = GameManager.turn;
        turnToEndPoison = effectTurn + duration;
        poisonEffectDamage = temporaryDamage;
        _poisonEffectImage.enabled = true;
        isPoisoned = true;
    }

    public void RemovePoisoning()
    {
        isPoisoned = false;
        _poisonEffectImage.enabled = false;
    }

    private void DoTemporaryDamage(int damage)
    {
        _characterManager.TakeDamage(damage);
    }
    
    public void ArmoringStart(int duration)
    {
        int effectTurn = GameManager.turn;
        turnToEndArmor = effectTurn + duration;
        isArmored = true;
    }

    public void RemoveArmor()
    {
        isArmored = false;
        _characterManager.RemoveArmor();
    }

    private void CheckEffects()
    {
        if (isPoisoned)
        {
            DoTemporaryDamage(poisonEffectDamage);
            if (GameManager.turn == turnToEndPoison)
            {
                RemovePoisoning();
            }
        }
            
        if (GameManager.turn == turnToEndArmor && isArmored)
        {
            RemoveArmor();
        }
    }

    private void OnDestroy()
    {
        GameManager.newTurn -= CheckEffects;
    }
}