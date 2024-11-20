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

    [Header("Hat References")]
    public Transform hat; // Reference to the hat object
    public Transform catHead; // Reference to the cat's head
    public Transform hatOffScreenLocation; // Reference to the off-screen location for the hat
    public float hatDropSpeed = 5f; // Speed of the hat dropping/rising

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

        // Hat rising transition
        StartCoroutine(MoveHat(catHead, hatOffScreenLocation)); // Use the off-screen location

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

        // Hat dropping transition
        StartCoroutine(MoveHat(catHead, hatOffScreenLocation)); // Use the off-screen location

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

    private IEnumerator MoveHat(Transform catHeadTransform, Transform offScreenTransform)
    {
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
