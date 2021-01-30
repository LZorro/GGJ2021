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
        if (currentDialog < dialogList.Count-1)
        {
            balloonText.text = dialogList[++currentDialog];
        }
    }

    public void enablePrompt(bool isOn)
    {
        if (commandPrompt != null)
            commandPrompt.SetActive(isOn);
    }
}
