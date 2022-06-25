using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Enable;
    [SerializeField]
    private GameObject[] Disable;
    [SerializeField]
    private GameStateControl gameStateControl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSwap()
    {
        foreach (GameObject obj in Enable)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in Disable)
        {
            obj.SetActive(false);
        }

        gameStateControl.SetGameState(GameStateControl.GameState.Cutscene);
    }
}
