using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    private CrowdManager crowdManager;
    private Animator animator;
    private void Awake() {
        crowdManager = transform.parent.GetComponent<CrowdManager>();
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    private void Update() {
        if(GameManager.Instance.currentGameState == GameState.PlayScreen) {
            animator.SetBool("IsRunning", true);
        }
        else if (GameManager.Instance.currentGameState == GameState.Victory) {
            animator.SetBool("IsRunning", false);

        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            crowdManager.RemovePlayer(GetComponent<Player>());
        }
        else if (collision.gameObject.CompareTag("Zombie")) {
            crowdManager.RemovePlayer(GetComponent<Player>());
            Destroy(collision.gameObject);
        }
    }
}
