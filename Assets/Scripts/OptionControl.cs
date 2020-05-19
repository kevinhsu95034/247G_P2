using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionControl : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Start()
    {
        LeanTween.scale(text.gameObject, Vector3.zero, 0);
    }

    public void ShowOptions(string message) {
        LeanTween.cancelAll();
        text.text = message;
        LeanTween.scale(text.gameObject, Vector3.one, 0);
    }

    public void Hide()
    {
        LeanTween.scale(text.gameObject, Vector3.zero, 0.7f);
    }
}
