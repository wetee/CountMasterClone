using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrowdManager : Singleton<CrowdManager> {

    public List<Player> players = new List<Player>();
    public SphereCollider _collider;

    public Transform playerTemplate;
    private CrowdController crowdController;

    [Range(0f, 1f)][SerializeField] private float distanceFactor;
    [Range(0f, 1f)][SerializeField] private float radius;

    private void Awake() {
        InitializeList();
        playerTemplate = Resources.Load<Transform>("Luffy");
        _collider = GetComponent<SphereCollider>();
        crowdController = GetComponent<CrowdController>();
    }

    //setup the initial list
    private void InitializeList() {
        //for (int i = 0; i < transform.childCount; i++) players.Add(transform.GetChild(i).GetComponent<Player>());

        players.Add(transform.Find("Luffy").GetComponent<Player>());
    }

    public void SpawnPlayer(int count) {
        for (int i = 0; i < count; i++) {
            Transform tempPlayer = Instantiate(playerTemplate, GetPositon(players.Count + 1), Quaternion.identity, transform);
            players.Add(tempPlayer.GetComponent<Player>());
            StartCoroutine(TurnOffCollider(tempPlayer.GetComponent<CapsuleCollider>()));
            tempPlayer.transform.DOLocalMove(GetPositon(players.Count), 0.05f);
        }
        StartCoroutine(DelayedRadiusCalc());

    }

    public void FormatPositions() {
        for (int i = 0; i < players.Count; i++) {
            float tempX = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            float tempZ = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            Vector3 tempPos = new(tempX, 0f, tempZ);

            players[i].transform.DOLocalMove(tempPos, 1f).SetEase(Ease.OutBack);
        }
    }

    // get the next position of the new player of crowd
    private Vector3 GetPositon(int i) {
        float tempX = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
        float tempZ = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

        return new(tempX, 0f, tempZ);
    }

    // handle the radius of collider by num of players
    public void ColliderRadiusHandler() {
        float tempRange = Vector3.Distance(players[0].transform.position, players[players.Count - 1].transform.position);
        _collider.radius = tempRange;

        crowdController.minBound = crowdController.minBoundStander + tempRange;
        crowdController.maxBound = crowdController.maxBoundStander - tempRange;

    }

    public void RemovePlayer(Player playerArg) {
        if (playerArg == null) return;

        players.Remove(playerArg);
        Destroy(playerArg.gameObject);

    }

    public Player RemoveLastPlayer() {
        Player playerArg = players[^1];
        players.RemoveAt(players.Count - 1);

        return playerArg;
    }

    public void RemoveExcessedPlayer(int rowCountArg, int initialCountArg) {
        int tempCount = rowCountArg * (rowCountArg + 1) / 2;
        tempCount = initialCountArg - tempCount;

        for (int i = 0; i < tempCount; i++) {
            Player player = RemoveLastPlayer();
            Destroy(player.gameObject);
        }

    }

    public void RemovePlayers(int count) {
        for(int i = count - 1; i > players.Count - count; i--) {
            Player tempPlayer = players[i];
            players.Remove(tempPlayer);
            Destroy(tempPlayer);
        }

    }

    public IEnumerator DelayedFormat() {
        yield return new WaitForSeconds(0.4f);
        FormatPositions();
        ColliderRadiusHandler();
    }

    public IEnumerator TurnOffCollider(Collider collider) {
        collider.enabled = false;
        yield return new WaitForSeconds(1.25f);
        collider.enabled = true;
    }

    public IEnumerator DelayedRadiusCalc() {
        yield return new WaitForSeconds(0.4f);
        ColliderRadiusHandler();
    }

}
