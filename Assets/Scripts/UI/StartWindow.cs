using System;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow: MonoBehaviour
{
    public Action<int, int> StartGameEvent;
    public Action QuitEvent;
    public Slider allySlider;
    public Slider enemySlider;
    

    public void OnStartGame()
    {
        int alliesToSpawn = (int)allySlider.value;
        int enemiesToSpawn = (int)enemySlider.value;
        StartGameEvent?.Invoke(alliesToSpawn, enemiesToSpawn);
    }
    
    public void OnGameQuit()
    {
        QuitEvent?.Invoke();
    }
    
}