using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for Light2D
using UnityEngine.SceneManagement; // Required for scene-loaded event
using System.Collections;

public class DayNightCycleManager : MonoBehaviour
{
    [Header("Light References")]
    private Light2D sunLight; // Reference to the Light2D acting as the sun
    private Light2D globalLight; // Reference to the global light (ambient light)

    [Header("Cycle Settings")]
    public int cycleThreshold = 50; // Points required for a day-night cycle
    public float transitionDuration = 2f; // Duration of the transition

    [Header("Day Settings")]
    public Color daySunColor = Color.white; // Sunlight color for day
    public float daySunIntensity = 1f; // Sunlight intensity for day
    public float dayGlobalIntensity = 1f; // Global light intensity for day

    [Header("Night Settings")]
    public Color nightSunColor = Color.blue; // Sunlight color for night
    public float nightSunIntensity = 0.5f; // Sunlight intensity for night
    public float nightGlobalIntensity = 0.3f; // Global light intensity for night

    [Header("Hat References")]
    private Transform hat; // Reference to the hat object
    private Transform catHead; // Reference to the cat's head
    private Transform hatOffScreenLocation; // Reference to the off-screen location for the hat
    public float hatDropSpeed = 5f; // Speed of the hat dropping/rising

    private bool isNight = false; // Tracks whether it is currently night

    private void OnEnable()
    {
        // Dynamically refresh references whenever the object is enabled
        RefreshReferences();

        // Listen for scene loaded events to refresh references
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Stop listening for scene loaded events
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void RefreshReferences()
    {
        // Find lights dynamically by tag
        sunLight = GameObject.FindWithTag("SunLight")?.GetComponent<Light2D>();
        globalLight = GameObject.FindWithTag("GlobalLight")?.GetComponent<Light2D>();

        if (sunLight == null)
        {
            Debug.LogError("SunLight with the tag 'SunLight' not found!");
        }

        if (globalLight == null)
        {
            Debug.LogError("GlobalLight with the tag 'GlobalLight' not found!");
        }

        // Find hat-related objects dynamically by tag
        hat = GameObject.FindWithTag("Hat")?.transform;
        catHead = GameObject.FindWithTag("CatHead")?.transform;
        hatOffScreenLocation = GameObject.FindWithTag("HatOffScreenLocation")?.transform;

        if (hat == null)
        {
            Debug.LogError("Hat with the tag 'Hat' not found!");
        }

        if (catHead == null)
        {
            Debug.LogError("CatHead with the tag 'CatHead' not found!");
        }

        if (hatOffScreenLocation == null)
        {
            Debug.LogError("HatOffScreenLocation with the tag 'HatOffScreenLocation' not found!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Refresh references after a scene is loaded
        RefreshReferences();
    }

    public void CheckForDayNightCycle(int score)
    {
        // Trigger day-night cycle if the score crosses a multiple of the cycle threshold
        if (score % cycleThreshold == 0)
        {
            if (isNight)
            {
                StartCoroutine(TransitionToDay());
            }
            else
            {
                StartCoroutine(TransitionToNight());
            }
        }
    }

    private IEnumerator TransitionToDay()
    {
        isNight = false;

        float timer = 0f;
        Color initialSunColor = sunLight.color;
        float initialSunIntensity = sunLight.intensity;
        float initialGlobalIntensity = globalLight.intensity;

        // Hat rising transition
        StartCoroutine(MoveHat(catHead, hatOffScreenLocation)); // Use the off-screen location

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float t = timer / transitionDuration;

            // Lerp sunlight color and intensity
            if (sunLight != null)
            {
                sunLight.color = Color.Lerp(initialSunColor, daySunColor, t);
                sunLight.intensity = Mathf.Lerp(initialSunIntensity, daySunIntensity, t);
            }

            // Lerp global light intensity
            if (globalLight != null)
            {
                globalLight.intensity = Mathf.Lerp(initialGlobalIntensity, dayGlobalIntensity, t);
            }

            yield return null;
        }

        SetDayMode();
    }

    private IEnumerator TransitionToNight()
    {
        isNight = true;

        float timer = 0f;
        Color initialSunColor = sunLight.color;
        float initialSunIntensity = sunLight.intensity;
        float initialGlobalIntensity = globalLight.intensity;

        // Hat dropping transition
        StartCoroutine(MoveHat(catHead, hatOffScreenLocation)); // Use the off-screen location

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float t = timer / transitionDuration;

            // Lerp sunlight color and intensity
            if (sunLight != null)
            {
                sunLight.color = Color.Lerp(initialSunColor, nightSunColor, t);
                sunLight.intensity = Mathf.Lerp(initialSunIntensity, nightSunIntensity, t);
            }

            // Lerp global light intensity
            if (globalLight != null)
            {
                globalLight.intensity = Mathf.Lerp(initialGlobalIntensity, nightGlobalIntensity, t);
            }

            yield return null;
        }

        SetNightMode();
    }

    private IEnumerator MoveHat(Transform catHeadTransform, Transform offScreenTransform)
    {
        if (hat == null || catHeadTransform == null || offScreenTransform == null)
        {
            Debug.LogError("MoveHat: One or more required references are missing! Hat, CatHead, or HatOffScreenLocation.");
            yield break;
        }

        Vector3 offScreenPosition = offScreenTransform.position; // Off-screen location
        float timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime * hatDropSpeed;

            // Dynamically determine the target position
            Vector3 currentTarget = isNight ? catHeadTransform.position : offScreenPosition;

            // Smoothly move the hat
            hat.position = Vector3.Lerp(hat.position, currentTarget, timer);

            yield return null;
        }

        // Snap the hat to the final position
        hat.position = isNight ? catHeadTransform.position : offScreenPosition;
    }

    private void SetDayMode()
    {
        if (sunLight != null)
        {
            sunLight.color = daySunColor;
            sunLight.intensity = daySunIntensity;
        }

        if (globalLight != null)
        {
            globalLight.intensity = dayGlobalIntensity;
        }
    }

    private void SetNightMode()
    {
        if (sunLight != null)
        {
            sunLight.color = nightSunColor;
            sunLight.intensity = nightSunIntensity;
        }

        if (globalLight != null)
        {
            globalLight.intensity = nightGlobalIntensity;
        }
    }
}
