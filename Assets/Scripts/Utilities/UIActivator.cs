using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivator : MonoBehaviour
{
    public GameObject[] toBeActivated;

    private void Awake()
    {
        foreach (GameObject go in toBeActivated)
            go.SetActive(true);
    }
}
