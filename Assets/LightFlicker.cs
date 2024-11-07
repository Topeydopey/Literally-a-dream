using UnityEngine;


public class LightFlicker : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light2D;
    public float minIntensity = 0.5f;  // Minimum intensity for flicker
    public float maxIntensity = 1.5f;  // Maximum intensity for flicker
    public float flickerSpeed = 0.1f;  // Speed of flicker change

    private float targetIntensity;     // Intensity to reach
    private float flickerTimer;        // Timer to control flicker intervals

    void Start()
    {
        if (light2D == null)
        {
            light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        }
        SetRandomTargetIntensity();
    }

    void Update()
    {
        // Lerp the intensity towards the target intensity
        light2D.intensity = Mathf.Lerp(light2D.intensity, targetIntensity, flickerSpeed * Time.deltaTime);

        // Countdown timer for when to change the target intensity
        flickerTimer -= Time.deltaTime;
        if (flickerTimer <= 0)
        {
            SetRandomTargetIntensity();
        }
    }

    void SetRandomTargetIntensity()
    {
        // Set a new random intensity within the defined range
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        // Reset the timer with a random flicker interval
        flickerTimer = Random.Range(0.05f, 0.3f);  // Adjust timing for desired flicker speed
    }
}
