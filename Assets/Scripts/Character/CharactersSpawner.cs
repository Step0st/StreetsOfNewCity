using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    public List<Transform> allyPositions;
    public List<Transform> enemyPositions;
    
    public List<CharacterManager> SpawnAlliesFromList(List<CharacterManager> chars, int charsToSpawn)
    {
        return MakeListOfCharacters(chars, charsToSpawn, allyPositions);
    }   
    
    public List<CharacterManager> SpawnEnemiesFromList(List<CharacterManager> chars, int charsToSpawn)
    {
        return MakeListOfCharacters(chars, charsToSpawn, enemyPositions);
    }

    public List<CharacterManager> MakeListOfCharacters(List<CharacterManager> chars, int charsToSpawn, List<Transform> positions)
    {
        List<CharacterManager> tempList = new List<CharacterManager>(); 
        for (int i = 0; i < charsToSpawn; i++)
        {
            var clone = Instantiate(chars[i], positions[i].position, Quaternion.identity);
            tempList.Add(clone);
        }
        return tempList;
    }
}