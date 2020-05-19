using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnnouncement : MonoBehaviour
{
    public DialogSO startAnnouncement;
    public DialogController dialogController;

    void Start()
    {
        Invoke("Announce", 1);
    }

    void Announce() {
        dialogController.StartDialog(startAnnouncement);
    }
}
