using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractOnTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent onKeyPress;

    private bool isActive;

    private void Start()
    {
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isActive = true;
        onTriggerEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false;
        onTriggerExit.Invoke();
    }

    private void OnTriggerEnter(Collider collision)
    {
        isActive = true;
        onTriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider collision)
    {
        isActive = false;
        onTriggerExit.Invoke();
    }

    private void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Space))
        {
            isActive = false;
            onKeyPress.Invoke();
        }
    }
}
