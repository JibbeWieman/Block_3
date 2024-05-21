using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

public class TakeDamage : MonoBehaviour
{
    public GameObject LostLife;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Landlord"))
        {
            gotHurt();

            Debug.Log("Collision detected.");
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            gotHurt();
        }
    }

    void gotHurt()
    {
        Debug.Log("Player hit.");
        
        var color = LostLife.GetComponent<Image>().color;
        color.a = 0.8f;
        color = color;

        Debug.Log("LostLife image alpha set to " + color.a);

        StartCoroutine(FadeOutAlpha());
    }

    IEnumerator FadeOutAlpha()
    {
        var imageComponent = LostLife.GetComponent<Image>();

        var color = imageComponent.color;
        color.a = 0.8f;
        imageComponent.color = color;

        while (color.a > 0)
        {
            color.a -= 0.05f;
            imageComponent.color = color;
            yield return new WaitForSeconds(0.1f); // Adjust the time interval as needed
        }

        Debug.Log("Alpha has reached 0.");
    }

}





    //[Header("References")]
    //[SerializeField] private VolumeProfile volumeProfile;
    //private static TakeDamage Instance;
    //private Volume volume;

    //[Header("Vignette Settings")]
    //[SerializeField] private float vignetteBlackIntensity = 0.2f;
    //[SerializeField] private float vignetteRedIntensity;
    //[SerializeField] private float redVignetteShowTime;
    //[SerializeField] private bool vignetteOnLowHealth = false;
    //private Vignette vignette;
    //private Color myRed;
    //private Color black = Color.black;

    //private void Awake()
    //{
    //    // Singleton pattern
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Destroy(this);
    //    }
    //}

    //private void Start()
    //{
    //    // Find references make sure to only have 1 volume in the scene
    //    volume = FindObjectOfType<Volume>();
    //    volume.profile = volumeProfile;

    //    volumeProfile.TryGet(out vignette);

    //    // Set the hexadecimal color to the desired color
    //    if (ColorUtility.TryParseHtmlString("#E51E25", out myRed)) // Red
    //        vignette.color.value = myRed;
    //}

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Landlord"))
    //    {
    //        ShowVignetteOnHit();
    //    }
    //}

    //public static void ShowVignetteOnHit()
    //{
    //    Instance.StopAllCoroutines();
    //    Instance.StartCoroutine(Instance.ShowVignetteOnHitCoroutine());
    //}

    ////public static void ShowVignetteOnLowHealth() => Instance.TurnOnVignetteOnLowHealth();
    ////public static void HideVignetteOnHighHealth() => Instance.TurnOffVignetteOnHighHealth();

    //private IEnumerator ShowVignetteOnHitCoroutine()
    //{
    //    // If the vignette is already on, don't show it again
    //    if (vignetteOnLowHealth) yield break;

    //    // Reset Intensity and set up hit vignette
    //    float intensity = vignetteRedIntensity;
    //    vignette.color.value = myRed;

    //    // Show hit Vignette for a short time
    //    yield return new WaitForSeconds(redVignetteShowTime);

    //    // Start decreasing intensity (i have a normal vignette with intesity 0.25f, so i decrease to that value)
    //    while (intensity > 0.25f)
    //    {
    //        if (vignetteOnLowHealth) yield break;

    //        intensity -= 0.01f;
    //        vignette.intensity.value = intensity;
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //    // Reset intensity to the normal intensity of the Black Vignette
    //    vignette.intensity.value = vignetteBlackIntensity;
    //    vignette.color.value = black;
    //}

    //private void TurnOnVignetteOnLowHealth()
    //{
    //    vignetteOnLowHealth = true;
    //    vignette.intensity.value = vignetteRedIntensity;
    //    vignette.color.value = myRed;
    //}

    //private void TurnOffVignetteOnHighHealth()
    //{
    //    // Reset to normal Vignette
    //    vignetteOnLowHealth = false;
    //    vignette.intensity.value = vignetteBlackIntensity;
    //    vignette.color.value = black;
    //}