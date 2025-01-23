using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    [ContextMenu("Push")]
    private void PushBubbles()
    {
        foreach (var item in WaveController.Instance.currentWaveBubbles)
        {
            item.SetReverseForce(5f);
        }

        StartCoroutine(ResetPush(1));
    }

    IEnumerator ResetPush(float duration)
    {
        yield return new WaitForSeconds(duration);

        foreach (var item in WaveController.Instance.currentWaveBubbles)
        {
            item.SetReverseForce(0);
        }
    }

    [ContextMenu("Pull")]
    private void PullBubbles()
    {
        foreach (var item in WaveController.Instance.currentWaveBubbles)
        {
            item.SetAdditionalForce(1.5f);
        }

        StartCoroutine(ResetPull(1));
    }

    IEnumerator ResetPull(float duration)
    {
        yield return new WaitForSeconds(duration);

        foreach (var item in WaveController.Instance.currentWaveBubbles)
        {
            item.SetAdditionalForce(1);
        }
    }
}
