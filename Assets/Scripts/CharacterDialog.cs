using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterDialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI balloonText;
    [SerializeField] private GameObject commandPrompt;
    public List<string> dialogList;
    private int currentDialog;
    public bool isDialogComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        currentDialog = 0;
        if (dialogList.Count > 0)
            balloonText.text = dialogList[currentDialog];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void advanceDialog()
    {
        // do not advance dialog if it is the last item on the list
        if (currentDialog < dialogList.Count - 1)
        {
            balloonText.text = dialogList[++currentDialog];
        }
        else
            isDialogComplete = true;
    }

    public void enablePrompt(bool isOn)
    {
        if (commandPrompt != null)
            commandPrompt.SetActive(isOn);
    }

    public void setNewDialog(int phaseNum)
    {
        isDialogComplete = false;
        currentDialog = -1;
        advanceDialog();
    }
}
