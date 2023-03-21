using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip levelTheme;
    [Header("Sounds when moving Moveable objects")]
    public bool useClickingSoundWhenMoveing = true;
    public AudioClip[] clickingMoveableObjects;
    public float distBtSounds = 0.4f;
    public float clickingVolume = 20;

    [Header("Sounds when clicked on path")]
    public AudioClip[] soundsOnClick;
    public float volumeOfClicks = 20;
    AudioSource audioS;
    AudioSource clickS;
    // Start is called before the first frame update
    void Start()
    {
        if(levelTheme != null){
            audioS = gameObject.AddComponent<AudioSource>() as AudioSource;
            audioS.spatialBlend = 0;
            audioS.loop = true;
            audioS.clip = levelTheme;
            audioS.Play();
        }
        clickS = gameObject.AddComponent<AudioSource>() as AudioSource;
    }
    public static SoundManager getManager(){
        SoundManager output;
        try{
            output = GameObject.Find("LevelController").GetComponent<SoundManager>();
        }catch{
            return null;
        }
        return output;
    }
    public AudioClip getRandomClickingSound(){
        return clickingMoveableObjects[(int)Random.Range((int)0,(int)clickingMoveableObjects.Length)];
    }
    public void playClick(){
        clickS.PlayOneShot(soundsOnClick[(int)Random.Range((int)0,(int)soundsOnClick.Length)], volumeOfClicks);
    }
    public static void playClickIfPossible(){
        SoundManager output;
        try{
            output = GameObject.Find("LevelController").GetComponent<SoundManager>();
        }catch{
            return;
        }
        output.clickS.PlayOneShot(output.soundsOnClick[(int)Random.Range((int)0,(int)output.soundsOnClick.Length)], output.volumeOfClicks);
    }
}
