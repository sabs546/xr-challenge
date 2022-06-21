using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateControl : MonoBehaviour
{
    public enum GameState { Menu, Playing, GameOver };
    public GameState gameState { get; private set; }

    [SerializeField]
    private GameObject cam;
    private Vector3 menuCamPos;
    private Quaternion menuCamRot;

    [Header("State Change Objects")]
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

    private void Awake()
    {
        menuCamPos = cam.transform.position;
        menuCamRot = cam.transform.rotation;
        SetGameState(GameState.Menu);
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
                gameState = GameState.Menu;
                mainMenu.SetActive(true);
                inGameUI.SetActive(false);
                gameOver.SetActive(false);
                player.GetComponent<Controller>().enabled = false;
                spawner.SetActive(false);
                finishZone.SetActive(false);

                // Bring the camera back to the menu position
                cam.transform.position = menuCamPos;
                cam.transform.rotation = menuCamRot;
                cam.GetComponent<CameraControl>().enabled = false;

                // Cursor settings
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                player.GetComponent<ScoreManager>().ResetScore();
                break;

            case GameState.Playing:
                gameState = GameState.Playing;
                mainMenu.SetActive(false);
                inGameUI.SetActive(true);
                gameOver.SetActive(false);
                player.GetComponent<Controller>().enabled = true;
                spawner.SetActive(true);
                finishZone.SetActive(true);

                // Hook the camera to the player
                cam.transform.position = player.transform.position;
                cam.transform.rotation = player.transform.rotation;
                cam.GetComponent<CameraControl>().enabled = true;

                // Cursor settings
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;

            case GameState.GameOver:
                gameState = GameState.GameOver;
                mainMenu.SetActive(false);
                inGameUI.SetActive(false);
                gameOver.SetActive(true);
                player.GetComponent<Controller>().enabled = false;
                spawner.SetActive(false);
                finishZone.SetActive(false);

                // Bring the camera back to the menu position
                cam.transform.position = menuCamPos;
                cam.transform.rotation = menuCamRot;
                cam.GetComponent<CameraControl>().enabled = false;

                // Cursor settings
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
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
