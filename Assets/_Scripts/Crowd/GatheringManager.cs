using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringManager : MonoBehaviour {

    private List<Transform> diamonds = new List<Transform>();

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Diamond")) {
            diamonds.Add(other.transform);
            Destroy(other.gameObject);

        }
    }

}
