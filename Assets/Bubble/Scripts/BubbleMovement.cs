using UnityEngine;
using EventDomain.Core;

public class BubbleMovement : MonoBehaviour
{
    public float floatStrength = 1.0f; // Force to make the bubble float up
    public float maxSpeed = 5.0f; // Maximum speed of the bubble

    [HideInInspector]
    public int Index = 0;

    private Rigidbody2D rb;

    private float additionalForce = 1;
    private float reverseForce;

    private void Update()
    {
        if (transform.position.y > 8)
        {
            DestroyBubble();
            EventSystem.Instance.BroadcastEvent<BubbleMovement>(EEventType.OnBubblePass, this);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Apply downward force (weight)
        if (reverseForce > 0)
        {
            Vector2 weightForce = Vector2.down * reverseForce;
            rb.AddForce(weightForce);
        }
        else
        {
            // Apply upward force (floating)
            Vector2 floatForce = Vector2.up * floatStrength * additionalForce;
            rb.AddForce(floatForce);
        }

        float tempMaxSpeed = (additionalForce + reverseForce == 1) ? maxSpeed : int.MaxValue;

        // Limit the bubble's speed
        if (rb.velocity.magnitude > tempMaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnMouseUpAsButton()
    {
        DestroyBubble();
        EventSystem.Instance.BroadcastEvent<BubbleMovement>(EEventType.OnBubbleDestroy, this);
    }

    private void DestroyBubble()
    {
        additionalForce = 1;
        reverseForce = 0;
        Destroy(gameObject);
    }

    public void SetAdditionalForce(float multiplier)
    {
        additionalForce = multiplier;
    }

    public void SetReverseForce(float multiplier)
    {
        reverseForce = multiplier;
    }
}