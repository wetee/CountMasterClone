using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour {

    private CrowdManager crowdManager;
    private Animator animator;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;


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
        else if (collision.gameObject.CompareTag("StoneBlock")) {
             //something can be implement
        }
        else if(collision.gameObject.CompareTag("Victory")) {
            GameManager.Instance.currentGameState = GameState.Victory;
        }
    }
}
