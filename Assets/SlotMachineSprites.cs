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

    private AudioSource audioSource;
    private bool isSpinning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Spin()
    {
        if (isSpinning) return;

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
        }
        else if (slot1 == slot2 || slot2 == slot3 || slot1 == slot3)
        {
            resultText.text = "You got a small win!";
            PlaySound(winSound);
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
        // Reset result text
        resultText.text = "Spin to win!";
        resultText.transform.rotation = Quaternion.identity;

        // Ensure the spin button is enabled
        spinButton.interactable = true;

        // Stop any ongoing audio
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Reset spinning state
        isSpinning = false;
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
