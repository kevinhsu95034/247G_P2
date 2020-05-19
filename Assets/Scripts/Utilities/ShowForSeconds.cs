using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowForSeconds : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Start()
    {
        LeanTween.scale(text.gameObject, Vector3.zero, 0);
    }

    public void Show(string message) {
        LeanTween.cancelAll();
        text.text = message;
        LeanTween.scale(text.gameObject, Vector3.one, 0);
        Invoke("Hide", 0.7f);
    }

    public void Hide() {
        LeanTween.scale(text.gameObject, Vector3.zero, 0.5f);
    }
}
