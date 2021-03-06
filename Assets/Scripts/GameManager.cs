using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject slideshow;
    public GameObject playerCharacter;

    private GameObject[] npcs;
    public List<requirementData> phaseEndRequirements;
    private int currentPhase;
    private requirementData currentRequirements;
    public bool isGameOver = false;
    public bool isSlideshowOpen;

    public GameObject confetti;

    // Start is called before the first frame update
    void Start()
    {
        npcs = GameObject.FindGameObjectsWithTag("Character");
        currentPhase = 0;
        currentRequirements = phaseEndRequirements[currentPhase];

        slideshow.GetComponent<Slideshow>().turnOn(true);
        playerCharacter.GetComponent<Playable>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        isSlideshowOpen = slideshow.GetComponent<Canvas>().enabled;
        if (pollForNextPhase() && !isGameOver)
        {
            setNextPhase();
        }
        else if (isGameOver)
        {
            confetti.SetActive(true);
        }
    }

    public void endSlideshow()
    {
        slideshow.GetComponent<Slideshow>().turnOn(false);
        playerCharacter.GetComponent<Playable>().enabled = true;
    }

    private bool pollForNextPhase()
    {
        bool isComplete = true;
        foreach (GameObject go in npcs)
        {
            if (isNPCRequired(go))
                if (go.GetComponent<CharacterDialog>().isDialogComplete == false)
                    isComplete = false;
        }

        return isComplete;
    }

    private bool isNPCRequired(GameObject go)
    {
        foreach (string check in currentRequirements.npcCharacters)
        {
            if (go.name == check)
                return true;
        }
        return false;
    }

    private void setNextPhase()
    {
        currentPhase++;
        foreach (GameObject go in npcs)
        {
            go.GetComponent<CharacterDialog>().setNewDialog(currentPhase);
        }
        slideshow.GetComponent<Slideshow>().setNewShow(currentPhase);
        if (currentPhase < phaseEndRequirements.Count)
            currentRequirements = phaseEndRequirements[currentPhase];
        else
            isGameOver = true;
    }
}
