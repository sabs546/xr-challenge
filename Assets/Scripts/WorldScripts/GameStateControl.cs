using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateControl : MonoBehaviour
{
    public enum GameState { Menu, Cutscene, Playing, LevelComplete, LevelFailed };
    public static GameState gameState { get; private set; }
    private int currentLevel;

    [SerializeField]
    private GameObject cam;
    private Vector3 menuCamPos;
    private Quaternion menuCamRot;

    public Transform cutsceneCam;

    [Header("State Change Objects")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject[] levelDescription;
    [SerializeField]
    private GameObject inGameUI;
    [SerializeField]
    private GameObject levelComplete;
    [SerializeField]
    private GameObject levelFailed;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject[] spawner;
    [SerializeField]
    private GameObject[] finishZone;

    private void Awake()
    {
        menuCamPos = cam.transform.position;
        menuCamRot = cam.transform.rotation;
        SetGameState(GameState.Menu);
        currentLevel = 0;
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
        switch (state) // I know this is a little excessive, but I won't be taking any chances
        {
            case GameState.Menu:
                gameState = GameState.Menu;
                mainMenu.SetActive(true);
                levelDescription[currentLevel].SetActive(false);
                inGameUI.SetActive(false);
                levelComplete.SetActive(false);
                levelFailed.SetActive(false);
                player.GetComponent<Controller>().enabled = false;
                spawner[currentLevel].SetActive(false);
                finishZone[currentLevel].SetActive(false);

                // Bring the camera back to the menu position
                cam.transform.position = menuCamPos;
                cam.transform.rotation = menuCamRot;
                cam.GetComponent<CameraControl>().enabled = false;

                // Cursor settings
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                player.GetComponent<ScoreManager>().ResetScore();
                break;

            case GameState.Cutscene:
                gameState = GameState.Cutscene;
                mainMenu.SetActive(false);
                levelDescription[currentLevel].SetActive(true);
                inGameUI.SetActive(false);
                levelComplete.SetActive(false);
                levelFailed.SetActive(false);
                player.GetComponent<Controller>().enabled = false;
                spawner[currentLevel].SetActive(true);
                finishZone[currentLevel].SetActive(true);

                // Bring the camera back to the preview position
                cam.transform.position = cutsceneCam.position;
                cam.transform.rotation = cutsceneCam.rotation;
                cam.GetComponent<CameraControl>().enabled = false;

                // Cursor settings
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;

            case GameState.Playing:
                gameState = GameState.Playing;
                mainMenu.SetActive(false);
                levelDescription[currentLevel].SetActive(false);
                inGameUI.SetActive(true);
                levelComplete.SetActive(false);
                levelFailed.SetActive(false);
                player.GetComponent<Controller>().enabled = true;
                spawner[currentLevel].SetActive(false);
                finishZone[currentLevel].SetActive(true);

                // Hook the camera to the player
                cam.transform.position = player.transform.position;
                cam.transform.rotation = player.transform.rotation;
                cam.GetComponent<CameraControl>().enabled = true;

                // Cursor settings
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;

            case GameState.LevelComplete:
                gameState = GameState.LevelComplete;
                mainMenu.SetActive(false);
                levelDescription[currentLevel].SetActive(false);
                inGameUI.SetActive(false);
                levelComplete.SetActive(true);
                levelFailed.SetActive(false);
                player.GetComponent<Controller>().enabled = false;
                player.GetComponent<Controller>().Halt();
                spawner[currentLevel].SetActive(false);
                finishZone[currentLevel].SetActive(false);
                currentLevel++;

                // Bring the camera back to the menu position
                cam.transform.position = menuCamPos;
                cam.transform.rotation = menuCamRot;
                cam.GetComponent<CameraControl>().enabled = false;

                // Cursor settings
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;

            case GameState.LevelFailed:
                gameState = GameState.LevelFailed;
                mainMenu.SetActive(false);
                levelDescription[currentLevel].SetActive(false);
                inGameUI.SetActive(false);
                levelComplete.SetActive(false);
                levelFailed.SetActive(true);
                player.GetComponent<Controller>().enabled = false;
                spawner[currentLevel].SetActive(false);
                finishZone[currentLevel].SetActive(false);

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
        SetGameState(GameState.Cutscene);
    }

    public void RestartGame()
    {
        SetGameState(GameState.Menu);
    }
}
