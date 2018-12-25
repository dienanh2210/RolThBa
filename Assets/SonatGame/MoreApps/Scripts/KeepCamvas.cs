using UnityEngine;
using System.Collections;

public class KeepCamvas : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public Canvas canvas;
}
