using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for Light2D
using System.Collections;

public class DayNightCycleManager : MonoBehaviour
{
    [Header("Light References")]
    public Light2D sunLight; // Reference to the Light2D acting as the sun
    public Light2D globalLight; // Reference to the global light (ambient light)

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

    private bool isNight = false; // Tracks whether it is currently night

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

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float t = timer / transitionDuration;

            // Lerp sunlight color and intensity
            sunLight.color = Color.Lerp(initialSunColor, daySunColor, t);
            sunLight.intensity = Mathf.Lerp(initialSunIntensity, daySunIntensity, t);

            // Lerp global light intensity
            globalLight.intensity = Mathf.Lerp(initialGlobalIntensity, dayGlobalIntensity, t);

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

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float t = timer / transitionDuration;

            // Lerp sunlight color and intensity
            sunLight.color = Color.Lerp(initialSunColor, nightSunColor, t);
            sunLight.intensity = Mathf.Lerp(initialSunIntensity, nightSunIntensity, t);

            // Lerp global light intensity
            globalLight.intensity = Mathf.Lerp(initialGlobalIntensity, nightGlobalIntensity, t);

            yield return null;
        }

        SetNightMode();
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
