using UnityEngine;
using System.Collections;
using System.Linq;
using Player;

public class BikeController : MonoBehaviour
{
    #region Dependencies

    public GameObject ragdollPrefab;
    private GameObject rider;
    private Rigidbody2D body;
    private AudioSource audioSource;
    public AudioClip pump;
    public AudioClip jump;
    public AudioClip crash;
    BikeConfiguration Configuration { get; set; }

    #endregion

    #region StateVariables

    private bool started = false;
    private float currentMaxSpeed = 5;
    private bool grounded;
    private bool crashed = false;
    private bool rearWheelDown = true;
    private float jumpTorque;
    private float jumpStrength = 0;

    public bool IsGrounded => grounded;

    #endregion

    #region PhysicsParameters

    Vector2 BodyCenterOfMass => Configuration.bodyCenterOfMass;

    public float MaxSpeed
    {
        get => Configuration.maxSpeed;
        set => Configuration.maxSpeed = value;
    }

    float MaxSpeedLerp => Configuration.maxSpeedLerp;
    float Acceleration => Configuration.acceleration;
    float GroundedStaticTorque => Configuration.groundedStaticTorque;
    float PumpSpeedBoost => Configuration.pumpSpeedBoost;
    float JumpSpeedBoost => Configuration.jumpSpeedBoost;

    float GroundedPumpTorque => Configuration.groundedPumpTorque;

    float AerialPumpStrength => Configuration.aerialPumpStrength;

    float UngroundGraceTime => Configuration.ungroundGraceTime;

    float MaxJumpStrength => Configuration.maxJumpStrength;
    float GroundedJumpTorque => Configuration.groundedJumpTorque;
    float AerialJumpTorque => Configuration.aerialJumpTorque;
    float LandStrength => Configuration.landStrength;

    #endregion

    #region UnityCallbacks

    void Awake()
    {
        Configuration = GetComponentsInChildren<BikeConfiguration>().First(it => it.gameObject.activeSelf);
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        body.centerOfMass = BodyCenterOfMass;
        rider = transform.Find("Rider").gameObject;
    }

    void FixedUpdate()
    {
        if (started && grounded && rearWheelDown)
        {
            body.AddTorque(GroundedStaticTorque);
            if (body.velocity.magnitude < currentMaxSpeed)
            {
                body.AddForce(Vector2.right * Acceleration);
            }
        }
        else if (!started && body.velocity.magnitude > 0)
        {
            body.velocity = new Vector2(Mathf.Lerp(body.velocity.x, 0, 0.016f), body.velocity.y);
        }

        currentMaxSpeed = Mathf.Lerp(currentMaxSpeed, MaxSpeed, MaxSpeedLerp);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        Land();
    }

    void OnCollisionExit2D(Collision2D col)
    {
        Invoke("LeaveGround", UngroundGraceTime);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        rearWheelDown = true;
        Land();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        rearWheelDown = false;
        LeaveGround();
    }

    #endregion

    #region BasicStates

    void Go()
    {
        if (!crashed)
        {
            started = true;
            rider.SendMessage("Go", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            SendMessageUpwards("Restart", SendMessageOptions.DontRequireReceiver);
        }
    }

    void Stop()
    {
        if (!crashed)
        {
            started = false;
            rider.SendMessage("Stop", SendMessageOptions.DontRequireReceiver);
        }
    }

    void Crash()
    {
        if (!crashed)
        {
            started = false;
            crashed = true;
            CancelInvoke();
            SendMessageUpwards("PlayerCrashed", SendMessageOptions.DontRequireReceiver);
            audioSource.PlayOneShot(crash, 1.0f);
            rider.SetActive(false);
            Score.AddCrash();
            InstantiateRagdoll();
        }
    }

    void LeaveGround()
    {
        rider.SendMessage("LeaveGround", SendMessageOptions.DontRequireReceiver);
        grounded = false;
    }

    void Land()
    {
        rider.SendMessage("Land", SendMessageOptions.DontRequireReceiver);
        grounded = true;
    }

    #endregion

    private void InstantiateRagdoll()
    {
        GameObject downedRider = (GameObject)Instantiate(ragdollPrefab, transform.position, transform.rotation);
        downedRider.SendMessage("SetVelocity", body.velocity);
        downedRider.transform.parent = transform.parent;
    }

    void Pump()
    {
        if (!crashed && started)
        {
            if (grounded || rearWheelDown)
            {
                GroundPump();
            }

            if (!grounded)
            {
                AerialPump();
            }
        }
    }

    void GroundPump()
    {
        audioSource.PlayOneShot(pump, 0.9f);
        rider.SendMessage("Pump", SendMessageOptions.DontRequireReceiver);
        body.AddTorque(GroundedPumpTorque);
        currentMaxSpeed += PumpSpeedBoost;
        jumpStrength = MaxJumpStrength;
        jumpTorque = GroundedJumpTorque;
        CancelInvoke();
        InvokeRepeating("AdjustJumpStrength", 0.50f, 0.016f);
    }

    void AerialPump()
    {
        jumpStrength = 500;
        rider.SendMessage("Pump", SendMessageOptions.DontRequireReceiver);
        body.AddTorque(AerialPumpStrength * Mathf.Sign(body.angularVelocity));
    }

    void AdjustJumpStrength()
    {
        jumpStrength = Mathf.Clamp(jumpStrength - 50, 500, 2000);
        jumpTorque = 50;
    }

    void Jump()
    {
        if (!crashed && started)
        {
            CancelInvoke();
            if (rearWheelDown || grounded)
            {
                GroundJump();
            }

            if (!grounded)
            {
                AerialJump();
            }
        }
    }

    void GroundJump()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(jump, (jumpStrength - 500) / 2000);
        rider.SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
        body.AddForce(Vector2.up * jumpStrength);
        body.AddTorque(jumpTorque);
        grounded = false;
    }

    void AerialJump()
    {
        rider.SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
        currentMaxSpeed += JumpSpeedBoost;
        body.AddForce(Vector2.down * LandStrength);
        body.AddTorque(AerialJumpTorque);
    }

    public bool GetCrashed()
    {
        return crashed;
    }
}
