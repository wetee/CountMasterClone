using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>{

    public GameState currentGameState;


    private void Start() {
        currentGameState = GameState.StartScreen;
    }

    private void Update() {

        switch (currentGameState) {
            case GameState.StartScreen:
                break;
            case GameState.PlayScreen:
                break;
            case GameState.PauseScreen:
                break;
            case GameState.Fighting:
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            case GameState.EndScreen:
                break;
        }

    }

}

public enum GameState {
    StartScreen,
    PlayScreen,
    PauseScreen,
    Fighting,
    Victory,
    Lose,
    EndScreen
}
