using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class CardEvents
{
    public static float cardLift;
    private static Vector3 startPosition;
    public static Action<CharacterManager> cardPlayed;
    
    public static void OnDragStart(GameObject obj)
    {
        startPosition = obj.transform.position;
        MoveObjToMousePos(obj, cardLift);
        MouseData.DraggedCard = obj.GetComponent<PlayableCard>();
    }
    
    public static void OnDrag(GameObject obj)
    {
        MoveObjToMousePos(obj, 0);
        obj.GetComponent<BoxCollider>().enabled = false;
    }
    
    public static void MoveObjToMousePos(GameObject obj, float lift)
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            Camera.main.WorldToScreenPoint(obj.transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        obj.transform.position = new Vector3(worldPosition.x, obj.transform.position.y + lift, worldPosition.z);
    }
    
    public static void OnDragEnd(GameObject obj)
    {
        if (MouseData.charMouseIsOver != null)
        {
            var targetCharacter = PlayerTargeting.ChooseTarget(MouseData.DraggedCard);
            if (targetCharacter != null)
            {
                cardPlayed?.Invoke(targetCharacter);
            }
            else
            {
                ReturnObjAtStartPos(obj);
            }
        }
        else
        {
            ReturnObjAtStartPos(obj);
        }
    }

    public static void ReturnObjAtStartPos(GameObject obj)
    {
        obj.transform.position = startPosition;
        obj.GetComponent<BoxCollider>().enabled = true; 
    }
}