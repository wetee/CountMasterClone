using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CrowdActions : MonoBehaviour {

    private CrowdController crowdController;
    private FormationHandler formationHandler;
    [SerializeField] private CinemachineVirtualCamera virtualCam;

    private void Awake() {
        crowdController = GetComponent<CrowdController>();
        formationHandler = GetComponent<FormationHandler>();
        CinemachineFramingTransposer transposer = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        transposer.m_CameraDistance = 21.5f;

    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Obstacle")) {
            StartCoroutine(CrowdManager.Instance.DelayedFormat());
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
            //GameManager.Instance.currentGameState = GameState.Victory;
            formationHandler.Formation();
            CinemachineFramingTransposer transposer = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            transposer.m_CameraDistance += 10f;
        }
        else if (other.gameObject.CompareTag("ZombieHorde")) {
            StartCoroutine(SpeedHandler());
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

    private IEnumerator SpeedHandler() {
        crowdController.forwardSpeed = crowdController.baseForwardSpeed / 2f;
        yield return new WaitForSeconds(1.5f);
        crowdController.forwardSpeed = crowdController.baseForwardSpeed;

    }

}
