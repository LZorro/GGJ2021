using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Slideshow : MonoBehaviour
{

    public List<AnimationClip> openingSlideshow;
    public List<AnimationClip> midgameSlideshow;
    public List<AnimationClip> endingSlideshow;

    public List<AudioClip> openingAudio;
    public List<AudioClip> midgameAudio;
    public List<AudioClip> endingAudio;

    private List<AnimationClip> currentSlideshow;
    private List<AudioClip> currentAudio;
    private int currentSlideIndex;
    [SerializeField] private Image currentImage;
    private Animator currentAnimation;
    private AudioSource currentAudioSource;
    private AnimationClip clipToPlay;
    private GameManager gm;
    private PlayableGraph pg;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        currentAnimation = currentImage.gameObject.GetComponent<Animator>();
        currentAudioSource = currentImage.gameObject.GetComponent<AudioSource>();

        currentSlideIndex = -1;  // resets to 0 in advanceSlide()
        currentSlideshow = openingSlideshow;
        currentAudio = openingAudio;
        // clipToPlay = currentSlideshow[currentSlideIndex];
        // currentAnimation.GetComponent<Animation>().clip = currentSlideshow[currentSlideIndex];
        //currentAnimation.StartPlayback();

        // AnimationPlayableUtilities.PlayClip(currentAnimation, clipToPlay, out pg);
        advanceSlide();
        
        //currentImage.sprite = currentSlideshow[currentSlideIndex].clip.
    }

    private void OnDisable()
    {
        pg.Destroy();
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

        if (!isOn)
            currentAudioSource.Stop();
    }

    private void advanceSlide()
    {
        if (currentSlideIndex < currentSlideshow.Count)
        {
            currentSlideIndex++;
            if (currentSlideshow[currentSlideIndex] != null)
            {
                clipToPlay = currentSlideshow[currentSlideIndex];
                AnimationPlayableUtilities.PlayClip(currentAnimation, clipToPlay, out pg);
            }

            if (currentAudioSource.isPlaying)
                currentAudioSource.Stop();
            if (currentAudio[currentSlideIndex] != null)
            {
                currentAudioSource.clip = currentAudio[currentSlideIndex];
                currentAudioSource.Play();
            }
            
        }
    }

    public void setNewShow(int phaseNum)
    {
        currentSlideIndex = -1; // number resets to 0 in advanceSlide()
        if (phaseNum == 1)
        {
            currentSlideshow = midgameSlideshow;
            currentAudio = midgameAudio;
        }
        else if (phaseNum == 2)
        {
            currentSlideshow = endingSlideshow;
            currentAudio = endingAudio;
        }

        turnOn(true);
        advanceSlide();
    }
}
