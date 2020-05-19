using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialog")]
public class DialogSO : ScriptableObject
{
    //public enum Expression { Neutral, Joy, Surprised, Sad, Angry };

    public Line[] lines;
    public Option[] options;

    [System.Serializable]
    public struct Line
    {
        [TextArea]
        public string content;
        public enum Who { Other, Player, Announcement };
        public Who talking;
        public AudioClip audio;
        //public Expression expression;
    }

    [System.Serializable]
    public struct Option
    {
        [TextArea]
        public string content;
        public DialogSO nextDialog;
        public UnityEvent doThis;
    }
}
