using System.Collections.Generic;
using UnityEngine;

public static class BotTargeting
{
    public static CharacterManager ChooseTarget(PlayableCard card, CharacterManager cardPlayingCharacter, 
        List<CharacterManager> allies, List<CharacterManager> enemies)
    {
        CharacterManager targetChar;
        
        if (card.cardType == CardType.AttackCard || card.cardType == CardType.PoisonCard)
        {
            if (cardPlayingCharacter.characterTeam == CharacterTeam.OpponentTeam)
            {
                targetChar = ChooseRandomTarget(allies);
            }
            else
            {
                targetChar = ChooseRandomTarget(enemies);
            }
            
            return targetChar;
        }

        if (card.cardType == CardType.HealCard || card.cardType == CardType.DefenceCard)
        {
            targetChar = ChooseMostDamagedAlly(enemies);
            return targetChar;
        }
        return null;
    }
    
    private static CharacterManager ChooseRandomTarget(List<CharacterManager> characterList)
    {
        int randomTarget = Random.Range(0, characterList.Count);
        return characterList[randomTarget];
    }

    private static CharacterManager ChooseMostDamagedAlly(List<CharacterManager> characterList)
    {
        int lowestHealth = characterList[0].health;
        int index = 0;
        for (int i = 0; i < characterList.Count; i++)
        {
            if(characterList[i].health < lowestHealth)
            {
                lowestHealth = characterList[i].health;
                index = i;
            }
        }
        return characterList[index];
    } 
}
