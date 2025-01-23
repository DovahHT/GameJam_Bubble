using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDScreen : BaseScreenGeneric<HUDScreen>
{
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI healthText;

    public void ShowNexttWaveAnnouncerText()
    {
        waveText.text = $"Next Wave is Comming Soon";
        waveText.gameObject.SetActive(true);
        StartCoroutine(HideText());
    }
    public void ShowStartWaveText(int waveIndex)
    {
        waveText.text = $"Wave {waveIndex}";
        waveText.gameObject.SetActive(true);
        StartCoroutine(HideText());
    }

    public void ShowGameOver()
    {
        waveText.text = $"Game Over";
        waveText.gameObject.SetActive(true);
    }

    public void SetHealth(int hp)
    {
        hp = Mathf.Clamp(hp, 0, int.MaxValue);
        healthText.text = hp.ToString();
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(2);
        waveText.gameObject.SetActive(false);
    }
}
