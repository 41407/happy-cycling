using UnityEngine;

public class FlipDetector : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    BikeController bikeController;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        bikeController = GetComponent<BikeController>();
    }

    void OnEnable()
    {
        previousRotation = rigidbody2D.rotation;
    }

    float absoluteRotation;
    float previousRotation;

    void FixedUpdate()
    {
        if (!bikeController.IsGrounded)
        {
            var rotation = rigidbody2D.rotation;
            absoluteRotation += previousRotation - rotation;
            previousRotation = rotation;
        }
        else
        {
            if (absoluteRotation is >= 180 or <= -180)
            {
                absoluteRotation -= Mathf.Sign(absoluteRotation) * Mathf.Abs(absoluteRotation % 360);
                Score.AddFlip();
            }
        }
    }
}
