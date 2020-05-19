using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    private RectTransform rect;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rect = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        rect.position = Camera.main.WorldToScreenPoint(player.position);
    }
}
