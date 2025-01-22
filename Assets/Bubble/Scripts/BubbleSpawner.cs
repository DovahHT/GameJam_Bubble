using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public ClickDetector clickDetector;

    // Start is called before the first frame update
    void Start()
    {
        clickDetector.ClickPositionCallback = OnMouseClick;
    }

    private void OnMouseClick(Vector3 pos)
    {
        Instantiate(bubblePrefab, pos, Quaternion.identity);
    }
}
