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
    public int health;

    public List<BubbleMovement> currentWaveBubbles = new List<BubbleMovement>();


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

        EventSystem.Instance.ListenToEvent<BubbleMovement>(EEventType.OnBubbleSpawn, OnBubbleSpawn);
        EventSystem.Instance.ListenToEvent<BubbleMovement>(EEventType.OnBubblePass, OnBubblePass);
        EventSystem.Instance.ListenToEvent<BubbleMovement>(EEventType.OnBubbleDestroy, OnBubbleDestroy);
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
            currentWaveBubbles.Clear();
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

    private void OnBubbleSpawn(BubbleMovement bubble)
    {
        currentWaveBubbles.Add(bubble);
    }

    private void OnBubblePass(BubbleMovement bubble)
    {
        if (RemoveBubble(bubble))
        {
            health -= bubble.Index + 1;
            HUDScreen.Instance.SetHealth(health);

            if (health <= 0)
            {
                gameOver = true;
            }
            else if (currentWaveBubbles.Count <= 0)
            {
                isWaveEnded = true;
            }
        }
    }

    private void OnBubbleDestroy(BubbleMovement bubble)
    {
        if (RemoveBubble(bubble))
        {
            if (currentWaveBubbles.Count <= 0)
            {
                isWaveEnded = true;
            }
        }
    }

    private bool RemoveBubble(BubbleMovement bubble)
    {
        if (currentWaveBubbles.Contains(bubble))
        {
            currentWaveBubbles.Remove(bubble);
            return true;
        }
        else
        {
            Debug.LogError("Should not happen!!");
            return false;
        }
    }
}
