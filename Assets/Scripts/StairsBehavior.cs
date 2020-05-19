using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsBehavior : MonoBehaviour
{
    public Transform upStairs;
    public Transform downStairs;
    public GameObject hull;
    public StairsBehavior upperFloor;
    public StairsBehavior lowerFloor;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public void UseStairs(bool goingDown)
    {
        if (!player.GetComponent<PlayerController>().canMove)
            return;

        if(goingDown) {
            lowerFloor.hull.SetActive(false);
            StartCoroutine(Teleport(lowerFloor.upStairs));
        }
        else {
            upperFloor.hull.SetActive(false);
            StartCoroutine(Teleport(upperFloor.downStairs));
        }
    }

    IEnumerator Teleport(Transform to) {
        float a = 1;
        SpriteRenderer sprite = player.GetComponent<SpriteRenderer>();
        player.GetComponent<PlayerController>().canMove = false;

        while (a > 0) {
            a -= Time.deltaTime;
            player.localScale = new Vector3(a, a, a);
            sprite.color = new Color(1, 1, 1, a);
            yield return null;
        }

        a = 0;
        sprite.color = Color.clear;
        player.transform.position = to.position;

        while (a < 1)
        {
            a += Time.deltaTime;
            player.localScale = new Vector3(a, a, a);
            sprite.color = new Color(1, 1, 1, a);
            yield return null;
        }

        player.localScale = Vector3.one;
        sprite.color = Color.white;
        player.GetComponent<PlayerController>().canMove = true;
        hull.SetActive(true);
    }
}
