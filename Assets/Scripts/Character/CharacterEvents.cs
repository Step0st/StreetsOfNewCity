using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class CharacterEvents
{
    public static void OnEnterCharacter(GameObject obj)
    {
        MouseData.charMouseIsOver = obj.GetComponent<CharacterManager>();
    }

    public static void OnExitCharacter(GameObject obj)
    {
        MouseData.charMouseIsOver = null;
    }
}