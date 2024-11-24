using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotMachineSprites : MonoBehaviour
{
    public Image slot1Image;
    public Image slot2Image;
    public Image slot3Image;
    public TextMeshProUGUI resultText;
    public Button spinButton;

    public Sprite[] slotSymbols;
    public float shuffleSpeed = 0.1f;
    public float shuffleDuration = 2f;
    public float slotDelay = 0.5f;

    public AudioClip buttonPressSound;
    public AudioClip spinningSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    public int spinCost = 10; // Points deducted for each spin
    public int smallWinReward = 20; // Points rewarded for small win
    public int jackpotReward = 100; // Points rewarded for jackpot

    private AudioSource audioSource;
    private bool isSpinning = false;

    private void Start()
    {
        // Ensure there's an AudioSource attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on SlotMachineSprites object. Adding one automatically.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }


    public void Spin()
    {
        if (isSpinning) return;

        // Deduct score for spinning
        if (!ScoreManager.instance.DeductScore(spinCost))
        {
            resultText.text = "Not enough points!";
            return;
        }

        isSpinning = true;

        PlaySound(buttonPressSound);

        spinButton.interactable = false;

        StartCoroutine(ShuffleSlot(slot1Image, slotDelay * 0));
        StartCoroutine(ShuffleSlot(slot2Image, slotDelay * 1));
        StartCoroutine(ShuffleSlot(slot3Image, slotDelay * 2, true));
    }

    private System.Collections.IEnumerator ShuffleSlot(Image slotImage, float delay, bool isLast = false)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;

        if (!audioSource.isPlaying)
        {
            PlaySound(spinningSound, true);
        }

        while (elapsedTime < shuffleDuration)
        {
            slotImage.sprite = slotSymbols[Random.Range(0, slotSymbols.Length)];
            yield return new WaitForSeconds(shuffleSpeed);
            elapsedTime += shuffleSpeed;
        }

        Sprite finalSymbol = slotSymbols[Random.Range(0, slotSymbols.Length)];
        slotImage.sprite = finalSymbol;

        if (isLast)
        {
            audioSource.Stop();
            CheckResult();
        }
    }

    private void CheckResult()
    {
        Sprite slot1 = slot1Image.sprite;
        Sprite slot2 = slot2Image.sprite;
        Sprite slot3 = slot3Image.sprite;

        if (slot1 == slot2 && slot2 == slot3)
        {
            resultText.text = "Jackpot! You win!";
            PlaySound(winSound);
            ScoreManager.instance.AddScore(jackpotReward);
        }
        else if (slot1 == slot2 || slot2 == slot3 || slot1 == slot3)
        {
            resultText.text = "You got a small win!";
            PlaySound(winSound);
            ScoreManager.instance.AddScore(smallWinReward);
        }
        else
        {
            resultText.text = "Try again!";
            PlaySound(loseSound);
        }

        spinButton.interactable = true;
        isSpinning = false;
    }

    public void ResetSlotMachine()
    {
        Debug.Log("ResetSlotMachine method called."); // Debug log for tracking

        // Reset result text
        if (resultText != null)
        {
            resultText.text = "Spin to win!";
            resultText.transform.rotation = Quaternion.identity;
        }
        else
        {
            Debug.LogError("ResultText reference is missing!");
        }

        // Enable spin button
        if (spinButton != null)
        {
            spinButton.interactable = true;
        }
        else
        {
            Debug.LogError("SpinButton reference is missing!");
        }

        // Stop any ongoing audio
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Audio stopped.");
        }
        else if (audioSource == null)
        {
            Debug.LogError("AudioSource reference is missing!");
        }

        // Reset spinning state
        isSpinning = false;
        Debug.Log("Slot machine reset complete.");
    }


    private void PlaySound(AudioClip clip, bool loop = false)
    {
        if (clip != null)
        {
            audioSource.loop = loop;
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
