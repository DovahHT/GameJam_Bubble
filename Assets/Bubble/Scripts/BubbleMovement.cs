using UnityEngine;
using EventDomain.Core;

public class BubbleMovement : MonoBehaviour
{
    public float floatStrength = 1.0f; // Force to make the bubble float up
    public float weight = 1.0f; // Weight to make the bubble drop down
    public float maxSpeed = 5.0f; // Maximum speed of the bubble

    public int Index = 0;

    private Rigidbody2D rb;

    private void Update()
    {
        if (transform.position.y > 8)
        {
            DestroyBubble();
            EventSystem.Instance.BroadcastEvent<int>(EEventType.OnBubblePass, Index);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Apply upward force (floating)
        Vector2 floatForce = Vector2.up * floatStrength;
        rb.AddForce(floatForce);

        // Apply downward force (weight)
        Vector2 weightForce = Vector2.down * weight;
        rb.AddForce(weightForce);

        // Limit the bubble's speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    // Public method to change the bubble's weight
    public void SetWeight(float newWeight)
    {
        weight = newWeight;
    }

    // Public method to change the bubble's float strength
    public void SetFloatStrength(float newFloatStrength)
    {
        floatStrength = newFloatStrength;
    }

    private void OnMouseUpAsButton()
    {
        DestroyBubble();
        EventSystem.Instance.BroadcastEvent<int>(EEventType.OnBubbleDestroy, Index);
    }

    private void DestroyBubble()
    {
        Destroy(gameObject);
    }
}