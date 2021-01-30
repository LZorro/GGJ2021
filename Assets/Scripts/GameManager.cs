using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject slideshow;
    public GameObject playerCharacter;

    private GameObject[] npcs;
    private int currentPhase;

    // Start is called before the first frame update
    void Start()
    {
        npcs = GameObject.FindGameObjectsWithTag("Character");
        currentPhase = 0;

        slideshow.GetComponent<Slideshow>().turnOn(true);
        playerCharacter.GetComponent<Playable>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pollForNextPhase())
        {
            setNextPhase();
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
            if (go.GetComponent<CharacterDialog>().isDialogComplete == false)
                isComplete = false;
        }

        return isComplete;
    }

    private void setNextPhase()
    {
        currentPhase++;
        slideshow.GetComponent<Slideshow>().setNewShow(currentPhase);
        foreach (GameObject go in npcs)
        {
            go.GetComponent<CharacterDialog>().setNewDialog(currentPhase);
        }
    }
}
