using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationHandler : MonoBehaviour {

    private int playerCount;
    private int numberOfRows;

    private float rowOffset = -0.625f;
    private float xOffset= 1.25f;
    private float yOffset = -1.25f;

    private void Update() {
        playerCount = CrowdManager.Instance.players.Count;
    }

    public void Formation() {
        RowCalculator();
        int initialCount = CrowdManager.Instance.players.Count;
        StartCoroutine(DoFormation());
        CrowdManager.Instance.RemoveExcessedPlayer(numberOfRows, initialCount);

    }

    private void RowCalculator() {
        numberOfRows = 0;
        while (true) {
            int temp = (numberOfRows * numberOfRows) + numberOfRows;
            temp -= playerCount * 2;

            if (temp >= 0) {
                break;
            }

            numberOfRows++;
        }
        numberOfRows--;
    }

    private IEnumerator DoFormation() {
        Transform targetPosition = transform.Find("Indicator") ;
        targetPosition.position += (numberOfRows / 1.5f) * Vector3.left ;
        //Vector3 targetPosition = transform.Find("Indicator").position + (numberOfRows / 2) * Vector3.left;
        //Vector3 targetPosition = transform.position + (numberOfRows / 2) * Vector3.left;

        for (int i = numberOfRows - 1; i >= 0; i--) {

            for (int j = i; j >= 0; j--) {
                Player tempPlayer = CrowdManager.Instance.RemoveLastPlayer();
                targetPosition.position = new Vector3(targetPosition.position.x + xOffset, targetPosition.position.y, targetPosition.position.z);
                tempPlayer.transform.position = targetPosition.position;
            }
            yield return new WaitForSeconds(0.3f);
            targetPosition.position = new Vector3((rowOffset * i) + 1.0f, targetPosition.position.y - yOffset, targetPosition.position.z);
        }


    }

}
