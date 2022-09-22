using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    private Transform startScreenTransform;

    private void Awake() {
        startScreenTransform = transform.Find("StartScreen");    
    }

    private void Update() {
        switch (GameManager.Instance.currentGameState) {
            case GameState.StartScreen:
                startScreenTransform.gameObject.SetActive(true);
                break;
            case GameState.PlayScreen:
                startScreenTransform.gameObject.SetActive(false);
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
