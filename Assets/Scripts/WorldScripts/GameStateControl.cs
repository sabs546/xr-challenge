using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateControl : MonoBehaviour
{
    public enum GameState { Menu, Playing, GameOver };
    public GameState gameState { get; private set; }

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject inGameUI;
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject spawner;
    [SerializeField]
    private GameObject finishZone;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Menu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                mainMenu.SetActive(true);
                inGameUI.SetActive(false);
                gameOver.SetActive(false);
                player.SetActive(false);
                spawner.SetActive(false);
                finishZone.SetActive(false);
                break;
            case GameState.Playing:
                gameState = GameState.Playing;
                mainMenu.SetActive(false);
                inGameUI.SetActive(true);
                gameOver.SetActive(false);
                player.SetActive(true);
                spawner.SetActive(true);
                finishZone.SetActive(true);
                break;
            case GameState.GameOver:
                mainMenu.SetActive(false);
                inGameUI.SetActive(false);
                gameOver.SetActive(true);
                player.SetActive(false);
                spawner.SetActive(false);
                finishZone.SetActive(false);
                break;
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.Playing);
    }

    public void RestartGame()
    {
        SetGameState(GameState.Menu);
    }
}
