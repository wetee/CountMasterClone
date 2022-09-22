using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsStabilizer : MonoBehaviour{

    private void Start() {
        Application.targetFrameRate = 60;
    }
}
