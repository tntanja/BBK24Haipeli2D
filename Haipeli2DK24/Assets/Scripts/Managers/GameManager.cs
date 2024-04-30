using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public static GameManager Instance;
    public GameState currentState;
    public PlayerController getPlayer { get; set; }
    private Master controls;
   
   
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        controls = new Master();
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }

    private void Update() {
        PauseGame();
    }

    private void PauseGame() {
        if (controls.Game.Pause.triggered) {
            Debug.Log("pause nappula");
            if(IsGamePlay()) {
                ChangeGameState(GameState.Pause);
            } else {
                ChangeGameState(GameState.Gameplay);
            }
        }
    }

    public void ChangeGameState(GameState newState){
        currentState = newState;
    }

    public bool IsGamePlay() {
        return currentState == GameState.Gameplay;
    }
}
