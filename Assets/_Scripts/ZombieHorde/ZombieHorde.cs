using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ZombieHorde : MonoBehaviour{

    private Transform zombieTransform;
    [SerializeField] private int AmountOfZombies;
    private List<Transform> zombies = new List<Transform>();

    private float distanceFactor = 0.3f;
    private float radius = 1f;

    private SphereCollider sphereCollider;

    private void Awake() {
        zombieTransform = transform.Find("zombie");
        zombies.Add(zombieTransform);
        sphereCollider = GetComponent<SphereCollider>();

        for (int i = 0; i < AmountOfZombies - 1; i++) {
            Transform tempZombie = Instantiate(zombieTransform, GetPositon(i) + new Vector3(0f, 0.8f, 0f) + transform.position, Quaternion.Euler(-90, 0, 180), transform) ;
            zombies.Add(tempZombie);
        }

    }

    private Vector3 GetPositon(int i) {
        float tempX = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
        float tempZ = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

        return new(tempX, 0f, tempZ);
    }

}
