using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private StartWindow _startWindow;
    [SerializeField] private GameWindow _gameWindow;
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private GameObject _winWindow;
    private GameManager _gameManager;
    [HideInInspector] public Button endTurnButton;

    private void Start()
    {
        Time.timeScale = 0;
        _gameManager = GetComponent<GameManager>();
        _startWindow.gameObject.SetActive(true);
        _gameWindow.gameObject.SetActive(false);
        _loseWindow.gameObject.SetActive(false);
        _winWindow.gameObject.SetActive(false);
        endTurnButton = _gameWindow.endTurnButton;

        _startWindow.StartGameEvent += (alliesToSpawn, enemiesToSpawn) =>
        {
            Time.timeScale = 1;
            _startWindow.gameObject.SetActive(false);
            _gameWindow.gameObject.SetActive(true);
            _gameManager.SpawnCharacters(alliesToSpawn, enemiesToSpawn);
            _gameManager.StartGame();
        };

        _startWindow.QuitEvent += () => ExitHelper.Exit();

        _gameWindow.EndTurnEvent += () =>
        {
            _gameManager.ChangeTurn();
        };
    }
    public void WinGame()
    {
        Time.timeScale = 0;
        _gameWindow.gameObject.SetActive(false);
        _winWindow.gameObject.SetActive(true);
    }
    
    public void LoseGame()
    {
        Time.timeScale = 0;
        _gameWindow.gameObject.SetActive(false);
        _loseWindow.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void NewGameButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
}