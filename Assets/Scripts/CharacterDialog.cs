using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterDialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI balloonText;
    [SerializeField] private GameObject commandPrompt;
    public List<requirementData> dialogList;  // reusing the requirementData since it's similar data (a list of strings)
    public List<string> currentdialogList;
    public int currentDialog;
    public int currentDialogListIndex;
    public bool isDialogComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        currentDialogListIndex = 0;
        currentdialogList = dialogList[currentDialogListIndex].npcCharacters;

        currentDialog = 0;
        if (currentdialogList.Count > 0)
            balloonText.text = currentdialogList[currentDialog];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void advanceDialog()
    {
        // do not advance dialog if it is the last item on the list
        if (currentDialog < currentdialogList.Count - 1)
        {
            balloonText.text = currentdialogList[++currentDialog];
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
        currentDialog = 0;
        if (currentDialogListIndex < dialogList.Count - 1)
        {
            currentdialogList = dialogList[++currentDialogListIndex].npcCharacters;
            balloonText.text = currentdialogList[currentDialog];
        }

        advanceDialog();
    }
}
