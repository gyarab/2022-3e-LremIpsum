using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{

    public PostProcessVolume volume;
    public float Bloom;
    public float Vignette;
    public float ColorGrading;
    //public GameObject mùjObjekt;
    private Bloom _Bloom;
    private Vignette _Vignette;
    private ColorGrading _ColorGrading;
    public static bool fadeOut = false;
    private static bool isFadingOut = false;
    public static bool willBeFadingIn;
    private static bool isFadingIn = false;

    private float speedOfFadingIn = 15;
    private float speedOfFadingOut = 20;
    void Start()
    {
        volume.profile.TryGetSettings(out _Bloom);
        _Bloom.intensity.value = Bloom;
        volume.profile.TryGetSettings(out _Vignette);
        _Vignette.intensity.value = Vignette;
        volume.profile.TryGetSettings(out _ColorGrading);
        _ColorGrading.postExposure.overrideState = true;
        _ColorGrading.postExposure.value = ColorGrading;

        if (willBeFadingIn)
        {
            _ColorGrading.postExposure.value = -10;
            isFadingIn = true;
            willBeFadingIn = false;
        }
    }
    void Update()
    {

        //mùjObjekt.transform.position = new Vector3((mùjObjekt.transform.position.x + 2)*Time.deltaTime, 0, 0);
        /*if (_Vignette.intensity.value < 0.45f)
        {
            _Vignette.intensity.value += 0.2f * Time.deltaTime;
        }*/
        if (fadeOut)
        {
            isFadingOut = true;
            fadeOut = false;
            willBeFadingIn = true;
        }
        if (isFadingOut)
        {
            if (_ColorGrading.postExposure.value > -10)
            {
                _ColorGrading.postExposure.value -= speedOfFadingOut * Time.deltaTime;
            }
            else
            {
                isFadingOut = false;
            }
        }

        if (isFadingIn)
        {
            if (_ColorGrading.postExposure.value < 0)
            {
                _ColorGrading.postExposure.value += speedOfFadingIn * Time.deltaTime;
            }
            else
            {
                _ColorGrading.postExposure.value = 0;
                isFadingIn = false;
            }
        }
    }
        
}
