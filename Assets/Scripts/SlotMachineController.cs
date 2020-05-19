using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    public RectTransform[] rolls;
    public RectTransform lever;

    private int[] values;
    private bool[] isSpinning;
    private float[] speed;
    private int currentlyStopping;
    private bool fullStop;

    // Start is called before the first frame update
    void Start()
    {
        speed = new float[] { 1, 1, 1 };
        isSpinning = new bool[3];
        values = new int[3];
        fullStop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (StillSpinning())
            {
                isSpinning[currentlyStopping] = false;
                currentlyStopping++;
            }
            else if (fullStop)
                StartSpinning();
        }
    }

    bool StillSpinning() { 
        foreach(bool b in isSpinning) {
            if (b) return true;
        }
        return false;
    }

    void StartSpinning() {
        currentlyStopping = 0;
        fullStop = false;
        StartCoroutine(Spin(0));
        StartCoroutine(Spin(1));
        StartCoroutine(Spin(2));
    }

    public void CloseSlot()
    {
        gameObject.SetActive(false);
        for(int i = 0; i < 3; i++) { isSpinning[i] = false; }
        fullStop = true;
        speed = new float[] { 0.1f, 0.1f, 0.1f };
    }


    IEnumerator Spin(int i) {
        speed[i] = 1;
        isSpinning[i] = true;

        while (isSpinning[i])
        {
            rolls[i].anchoredPosition += Vector2.down * Time.deltaTime * 3000 * speed[i];
            if (rolls[i].anchoredPosition.y < -2100)
                rolls[i].anchoredPosition = Vector3.zero;
            yield return null;
        }

        while(speed[i] > 0.1f) {
            speed[i] -= Time.deltaTime * 0.3f;
            rolls[i].anchoredPosition += Vector2.down * Time.deltaTime * 3000 * speed[i];
            values[i] = (int)rolls[i].anchoredPosition.y / -300;
            if (rolls[i].anchoredPosition.y < -2100)
                rolls[i].anchoredPosition = Vector3.zero;
            yield return null;
        }

        float t = 0;
        Vector2 originalPosition = rolls[i].anchoredPosition;
        Vector2 targetPosition = Vector2.down * 300 * values[i];
        while (t < 1)
        {
            t += Time.deltaTime * (t * 5 + 1);
            rolls[i].anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, t);
            yield return null;
        }
        
        rolls[i].anchoredPosition = targetPosition;
        if (i == 2) fullStop = true;
    }
}
