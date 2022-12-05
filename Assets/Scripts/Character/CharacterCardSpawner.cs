using UnityEngine;

public class CharacterCardSpawner : MonoBehaviour
{
    public PlayableCard SpawnCard(PlayableCard[] characterDeck)
    {
        int randomCard = Random.Range(0, characterDeck.Length);
        var card = Instantiate(characterDeck[randomCard], transform.position, Quaternion.identity, transform);
        return card;
    }
}