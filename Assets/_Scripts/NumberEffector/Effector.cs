using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Effector : MonoBehaviour {

    private TextMeshPro effectInfoText;

    public OperatorType operatorType;
    public int operatorFactor;

    private void Awake() {
        effectInfoText = transform.Find("text").GetComponent<TextMeshPro>();

        switch (operatorType) {
            case OperatorType.add:
                effectInfoText.text = "+" + operatorFactor;
                break;
            case OperatorType.subtract:
                effectInfoText.text = "-" + operatorFactor;
                break;
            case OperatorType.multiply:
                effectInfoText.text = "x" + operatorFactor;
                break;
            case OperatorType.divide:
                effectInfoText.text = "/" + operatorFactor;
                break;
        }
    }


    public enum OperatorType {
        add,
        subtract,
        multiply,
        divide
    }

}
