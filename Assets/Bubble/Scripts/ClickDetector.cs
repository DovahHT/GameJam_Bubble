using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    private Camera mainCamera;
    public SpriteRenderer sprite;
    public System.Action<Vector3> ClickPositionCallback;

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;


            Vector2 mousePosition1 = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            ClickPositionCallback?.Invoke(mousePosition1);
        }
    }
}