using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventDomain.Core;

public class WaveController : MonoBehaviour
{
    private static WaveController instance;
    public static WaveController Instance => instance;

    public RandomBubbleSpawner bubbleSpawner;

    public float endWaveDelay = 5f;
    private int currentWaveIndex;
    private bool gameOver;
    private bool isWaveEnded;
    private int totalBubbleCount;
    public int health;

    private Dictionary<int, int> currentWaveBubbleDict = new Dictionary<int, int>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        Initiate();
    }

    private void Initiate()
    {
        StartCoroutine(StartWaves());

        EventSystem.Instance.ListenToEvent<int>(EEventType.OnBubbleSpawn, OnBubbleSpawn);
        EventSystem.Instance.ListenToEvent<int>(EEventType.OnBubblePass, OnBubblePass);
        EventSystem.Instance.ListenToEvent<int>(EEventType.OnBubbleDestroy, OnBubbleDestroy);
    }

    IEnumerator StartWaves()
    {
        gameOver = false;
        currentWaveIndex = 1;
        health = 10;
        HUDScreen.Instance.Show();

        while (true)
        {
            HUDScreen.Instance.ShowNexttWaveAnnouncerText();

            yield return new WaitForSeconds(endWaveDelay);

            HUDScreen.Instance.ShowStartWaveText(currentWaveIndex);
            currentWaveBubbleDict.Clear();
            totalBubbleCount = 0;
            HUDScreen.Instance.SetHealth(health);
            bubbleSpawner.StartWave();
            isWaveEnded = false;


            while (!gameOver && !isWaveEnded)
            {
                yield return new WaitForSeconds(0.5f);
            }

            if (gameOver)
            {
                HUDScreen.Instance.ShowGameOver();
                break;
            }

            currentWaveIndex++;
        }
    }

    private void OnBubbleSpawn(int index)
    {
        if (currentWaveBubbleDict.ContainsKey(index))
        {
            currentWaveBubbleDict[index]++;
        }
        else
        {
            currentWaveBubbleDict.Add(index, 1);
        }
        totalBubbleCount++;
    }

    private void OnBubblePass(int index)
    {
        if (RemoveBubble(index))
        {
            health -= index + 1;
            HUDScreen.Instance.SetHealth(health);

            if (health <= 0)
            {
                gameOver = true;
            }
            else if (totalBubbleCount <= 0)
            {
                isWaveEnded = true;
            }
        }
    }

    private void OnBubbleDestroy(int index)
    {
        if (RemoveBubble(index))
        {
            if (totalBubbleCount <= 0)
            {
                isWaveEnded = true;
            }
        }
    }

    private bool RemoveBubble(int index)
    {
        if (currentWaveBubbleDict.ContainsKey(index) && currentWaveBubbleDict[index] > 0)
        {
            currentWaveBubbleDict[index]--;
            totalBubbleCount--;

            return true;
        }
        else
        {
            Debug.LogError("Should not happen!!");
            return false;
        }
    }
}
