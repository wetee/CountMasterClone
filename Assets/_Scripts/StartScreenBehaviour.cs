using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartScreenBehaviour : MonoBehaviour{
    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            GameManager.Instance.currentGameState = GameState.PlayScreen;
        }
    }

}
