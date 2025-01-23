using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDScreen : MonoBehaviour
{
    private static HUDScreen instance;
    public static HUDScreen Instance => instance;

    public TextMeshProUGUI waveText;

    private void Awake()
    {
        instance = this;
        waveText.gameObject.SetActive(false);
    }

    public void ShowStartWaveText(int waveIndex)
    {
        waveText.text = $"Wave {waveIndex}";
        waveText.gameObject.SetActive(true);
        StartCoroutine(HideText());
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(2);
        waveText.gameObject.SetActive(false);
    }
}
