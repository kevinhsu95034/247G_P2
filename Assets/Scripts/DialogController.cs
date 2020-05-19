using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogController : MonoBehaviour
{
    [Header("Parameters")]
    public Vector3 offset;

    [Header("Game Reference")]
    public RectTransform bubble;
    public TextMeshProUGUI dialogText;
    public Transform optionRoot;

    [Header("Prefabs")]
    public GameObject optionPrefab;

    private Transform player, talkingTo;
    private AudioSource source;

    private DialogSO currentDialog, nextDialog;
    private int currentLine;


    private float playSpeed;
    private bool isPlaying;
    private bool isTalking;

    private void Start()
    {
        bubble.gameObject.SetActive(false);
        player = GameObject.FindWithTag("Player").transform;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isTalking && Input.anyKeyDown)
        {
            if (isPlaying) { playSpeed *= 10; }
            else
            {
                if (currentLine < currentDialog.lines.Length)
                {
                    StartCoroutine(PlayNextText());
                }
                else if (currentLine == currentDialog.lines.Length)
                {
                    ShowOptions();
                    currentLine++;
                }
            }
        }
    }

    public void SetTalkingTarget(Transform other) {
        this.talkingTo = other;
    }

    public void ResetTalkingTarget()
    {
        this.talkingTo = null;
    }

    public void StartDialog(DialogSO dialog) {
        PlayerController.instance.canMove = false;
        isTalking = true;
        currentDialog = dialog;
        currentLine = 0;

        StartCoroutine(PlayNextText());
    }

    public void EndDialog()
    {
        bubble.gameObject.SetActive(false);
        optionRoot.gameObject.SetActive(false);
        foreach (Transform child in optionRoot)
            Destroy(child.gameObject);
        dialogText.text = "";


        if (nextDialog != null) {
            currentLine = 0;
            StartDialog(nextDialog);
            nextDialog = null;
        }
        else {
            currentLine = 0;
            currentDialog = null;
            talkingTo = null;
            PlayerController.instance.canMove = true;
            isTalking = false;
        }
    }

    IEnumerator PlayNextText()
    {
        switch (currentDialog.lines[currentLine].talking) {
            case DialogSO.Line.Who.Player:
                bubble.position = Camera.main.WorldToScreenPoint(player.position + offset);
                bubble.localScale = new Vector3(1, 1, 1);
                dialogText.transform.localScale = new Vector3(1, 1, 1);
                break;
            case DialogSO.Line.Who.Other:
                bubble.position = Camera.main.WorldToScreenPoint(talkingTo.position + offset);
                bubble.localScale = new Vector3(1, 1, 1);
                dialogText.transform.localScale = new Vector3(1, 1, 1);
                break;
            default:
                bubble.anchoredPosition = new Vector2(-400, -200);
                bubble.localScale = new Vector3(1, -1, 1);
                dialogText.transform.localScale = new Vector3(1, -1, 1);
                break;
        }

        if (currentDialog.lines[currentLine].audio != null) {
            source.PlayOneShot(currentDialog.lines[currentLine].audio);
        }

        while (source.isPlaying) {
            yield return null;
        }

        dialogText.text = "";
        playSpeed = 1;
        isPlaying = true;
        bubble.gameObject.SetActive(true);

        string stringToPlay = currentDialog.lines[currentLine].content;

        for (int i = 0; i < stringToPlay.Length; i++)
        {
            dialogText.text += stringToPlay[i];
            yield return new WaitForSeconds(0.05f / playSpeed);
        }

        isPlaying = false;
        currentLine++;
    }

    void ShowOptions()
    {
        if (currentDialog.options.Length < 1) {
            EndDialog();
            return;
        }

        optionRoot.gameObject.SetActive(true);
        foreach (DialogSO.Option option in currentDialog.options) {
            GameObject go = Instantiate(optionPrefab, optionRoot);

            go.GetComponentInChildren<TextMeshProUGUI>().text = option.content;
            go.GetComponent<Button>().onClick.RemoveAllListeners();
            if (option.nextDialog != null) go.GetComponent<Button>().onClick.AddListener(() => SetNextDialog(option.nextDialog));
            go.GetComponent<Button>().onClick.AddListener(option.doThis.Invoke);
            go.GetComponent<Button>().onClick.AddListener(EndDialog);
        }
    }

    void SetNextDialog(DialogSO dialogSO) {
        nextDialog = dialogSO;
    }
}
