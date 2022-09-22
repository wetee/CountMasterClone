using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour
{
    
    // movement stuff
    [HideInInspector] public float swerveSpeed;
    [HideInInspector] public readonly float maxSwerveSpeed = 7f;
    [HideInInspector] public float forwardSpeed;
    [HideInInspector] public readonly float maxForwardSpeed = 4f;

    // input clamp stuff
    [HideInInspector] public float minBound;
    [HideInInspector] public float minBoundStander = -5f;
    [HideInInspector] public float maxBound;
    [HideInInspector] public float maxBoundStander = 5f;

    // input stuff
    private Touch currentTouch;
    private Vector3 targetPosition;



    private void Start() {
        swerveSpeed = maxSwerveSpeed;
        forwardSpeed = maxForwardSpeed;
        minBound = minBoundStander;
        maxBound = maxBoundStander;
    }

    
    private void LateUpdate() {
        switch (GameManager.Instance.currentGameState) {
            case GameState.PlayScreen:
                HandleMovement();
                HandleSwerve();
                break;
            default:
                break;
        }
        
    }

    private void HandleSwerve() {
        if (Input.touchCount <= 0) return;

        currentTouch = Input.GetTouch(0);

        if (currentTouch.phase != TouchPhase.Moved) return;

        float newX = currentTouch.deltaPosition.x * swerveSpeed * Time.deltaTime;

        targetPosition = transform.position;
        targetPosition.x += newX;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minBound, maxBound);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * swerveSpeed);
    }

    private void HandleMovement() {
        transform.position += new Vector3(0f, 0f, forwardSpeed * Time.deltaTime);
    }
}
