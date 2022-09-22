using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T instance;

    public static T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<T>();
                if (instance == null) {
                    GameObject tempObject = new GameObject();
                    tempObject.name = "Instance of " + nameof(T);
                    instance = tempObject.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    public virtual void awake() {
        if(instance == null) {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
