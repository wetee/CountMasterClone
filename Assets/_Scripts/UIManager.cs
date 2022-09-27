using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    private Transform startScreenTransform;
    private Transform victoryScreenTransform;

    private void Awake() {
        startScreenTransform = transform.Find("StartScreen");
        victoryScreenTransform = transform.Find("VictoryScreen");
        victoryScreenTransform.transform.Find("nextLevelButton").GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    private void Update() {
        switch (GameManager.Instance.currentGameState) {
            case GameState.StartScreen:
                startScreenTransform.gameObject.SetActive(true);
                victoryScreenTransform.gameObject.SetActive(false);
                break;
            case GameState.PlayScreen:
                startScreenTransform.gameObject.SetActive(false);
                break;
            case GameState.PauseScreen:
                break;
            case GameState.Fighting:
                break;
            case GameState.Victory:
                victoryScreenTransform.gameObject.SetActive(true);
                break;
            case GameState.Lose:
                break;
            case GameState.EndScreen:
                break;
        }
    }

}
