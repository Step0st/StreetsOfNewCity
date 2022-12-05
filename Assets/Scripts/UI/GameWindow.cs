using System;
using UnityEngine;
using UnityEngine.UI;

public class GameWindow: MonoBehaviour
{
    public Action EndTurnEvent;
    public Button endTurnButton;
    
     public void OnTurnEnd()
     {
         EndTurnEvent?.Invoke();
     }
    
}