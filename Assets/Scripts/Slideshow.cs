using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slideshow : MonoBehaviour
{

    public List<Sprite> openingSlideshow;
    public List<Sprite> midgameSlideshow;
    public List<Sprite> endingSlideshow;

    private List<Sprite> currentSlideshow;
    private int currentSlideIndex;
    [SerializeField] private Image currentImage;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();

        currentSlideIndex = 0;
        currentSlideshow = openingSlideshow;
        currentImage.sprite = currentSlideshow[currentSlideIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentSlideIndex < currentSlideshow.Count - 1)
                advanceSlide();
            else
            {
                gm.endSlideshow();
            }
        }
    }

    public void turnOn(bool isOn)
    {
        this.gameObject.GetComponent<Canvas>().enabled = isOn;
    }

    private void advanceSlide()
    {
        currentImage.sprite = currentSlideshow[++currentSlideIndex];
    }

    public void setNewShow(int phaseNum)
    {
        currentSlideIndex = -1; // number resets to 0 in advanceSlide()
        if (phaseNum == 1)
            currentSlideshow = midgameSlideshow;
        else if (phaseNum == 2)
            currentSlideshow = endingSlideshow;

        turnOn(true);
        advanceSlide();
    }
}
