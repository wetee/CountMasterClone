using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdActions : MonoBehaviour {

    private CrowdController crowdController;

    private void Awake() {
        crowdController = GetComponent<CrowdController>();
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Obstacle")) {
            StartCoroutine(CrowdManager.Instance.DelayedFormat());
        }
        else if (other.gameObject.CompareTag("ZombieHorde")) {
            StartCoroutine(CrowdManager.Instance.DelayedFormat());
            crowdController.forwardSpeed = crowdController.maxForwardSpeed;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("NumberEffector")) {
            Destroy(other.transform.parent.gameObject);
            StartCoroutine(CrowdManager.Instance.TurnOffCollider(CrowdManager.Instance._collider));
            Effector effector = other.GetComponent<Effector>();
            HandleOperator(effector);
            CrowdManager.Instance.FormatPositions();
        }
        else if (other.gameObject.CompareTag("Finish")) {
            GameManager.Instance.currentGameState = GameState.Victory;
        }
        else if (other.gameObject.CompareTag("ZombieHorde")) {
            crowdController.forwardSpeed = crowdController.maxForwardSpeed / 3.5f;
        }
    }
    private void HandleOperator(Effector effectorArg) {
        switch (effectorArg.operatorType) {
            case Effector.OperatorType.add:
                CrowdManager.Instance.SpawnPlayer(effectorArg.operatorFactor);
                break;
            case Effector.OperatorType.subtract:
                break;
            case Effector.OperatorType.multiply:
                int count = CrowdManager.Instance.players.Count * effectorArg.operatorFactor;
                CrowdManager.Instance.SpawnPlayer(count - CrowdManager.Instance.players.Count);
                break;
            case Effector.OperatorType.divide:
                break;
        }
    }

    
}
