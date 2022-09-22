using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour {
    private float speed = 3f;
    private State currenState;

    public Vector3 LeftCorner;
    public Vector3 RightCorner;

    private void Start() {
        currenState = State.OnRight;
    }
    private void Update() {
        switch (currenState) {
            case State.OnLeft:
                GoRight();
                break;
            case State.OnRight:
                GoLeft();
                break;
        }
    }

    private void GoRight() {
        //transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        transform.position = Vector3.MoveTowards(transform.position, RightCorner, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, RightCorner) < .3f) {
            currenState = State.OnRight;
        }
    }
    private void GoLeft() {
        //transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
        transform.position = Vector3.MoveTowards(transform.position, LeftCorner, -speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, LeftCorner) < .3f) {
            currenState = State.OnLeft;
        }
    }

    private enum State {
        OnLeft,
        OnRight
    }

}
