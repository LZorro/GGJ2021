using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slideshow : MonoBehaviour
{

    public List<AnimationClip> openingSlideshow;
    public List<AnimationClip> midgameSlideshow;
    public List<AnimationClip> endingSlideshow;

    private List<AnimationClip> currentSlideshow;
    private int currentSlideIndex;
    [SerializeField] private Image currentImage;
    private Animator currentAnimation;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        currentAnimation = currentImage.gameObject.GetComponent<Animator>();

        currentSlideIndex = 0;
        currentSlideshow = openingSlideshow;
        currentAnimation.GetComponent<Animation>().clip = currentSlideshow[currentSlideIndex];
        currentAnimation.StartPlayback();
        
        //currentImage.sprite = currentSlideshow[currentSlideIndex].clip.
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
        //currentImage.sprite = currentSlideshow[++currentSlideIndex];

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
