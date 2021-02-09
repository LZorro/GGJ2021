using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterDialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI balloonText;
    [SerializeField] private GameObject commandPrompt;
    private AudioSource dialogAudio;
    public List<requirementData> dialogList;  // reusing the requirementData since it's similar data (a list of strings)
    public List<string> currentdialogList;
    public List<AudioClip> currentdialogAudioList;
    public int currentDialog;
    public int currentDialogListIndex;
    public bool isDialogComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogAudio = this.gameObject.GetComponent<AudioSource>();

        currentDialogListIndex = 0;
        currentdialogList = dialogList[currentDialogListIndex].npcCharacters;
        currentdialogAudioList = dialogList[currentDialogListIndex].dialogVFX;

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

            // if there is an associated audio, stop what's playing and play that
            playCurrentAudio();
          
        }
        else
            isDialogComplete = true;
    }

    public void enablePrompt(bool isOn)
    {
        if (commandPrompt != null)
            commandPrompt.SetActive(isOn);

        if (isOn)
        {
            playCurrentAudio();
        }
    }

    public void setNewDialog(int phaseNum)
    {
        isDialogComplete = false;
        currentDialog = 0;
        if (currentDialogListIndex < dialogList.Count - 1)
        {
            currentDialogListIndex++;
            currentdialogList = dialogList[currentDialogListIndex].npcCharacters;
            currentdialogAudioList = dialogList[currentDialogListIndex].dialogVFX;
        }
        balloonText.text = currentdialogList[currentDialog];

        // advanceDialog();
    }

    private void playCurrentAudio()
    {
        if (currentdialogAudioList[currentDialog] != null)
        {
            if (dialogAudio.isPlaying)
                dialogAudio.Stop();
            dialogAudio.clip = currentdialogAudioList[currentDialog];
            dialogAudio.Play();
        }
    }
}
