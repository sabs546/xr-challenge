using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeedLimit; // Walking maximum
    public float sprintSpeedLimit; // Sprinting maximum
    public float chargedSpeedLimit; // Charged maximum
    public float airSpeedLimit; // Air maneuverability
    public float currentSpeedLimit { get; private set; } // Speed being applied
    public float jumpPower;
    private Vector3 moveDirection;

    // Inputs
    private Vector2 directionalInput;
    private float jumpInput;
    private float sprintInput;

    [Header("Charge Bar")]
    [SerializeField]
    private RectTransform chargeBar;
    private float chargeTimer;
    public int charged { get; private set; } // Charged is a multiplier of 2 when active
    [SerializeField]
    [Tooltip("How fast do you need to move to start building meter")]
    private float chargingThreshold;
    [SerializeField]
    [Range(0, 10)]
    [Tooltip("How low does the meter need to go to remove charge")]
    private float removalThreshold;

    [Header("Physics")]
    [SerializeField]
    private float groundDrag;
    public LayerMask Ground;
    public LayerMask Air;
    public LayerMask RockyGround;
    public LayerMask CleanGround;
    public float gravity;
    private Vector3 currentVelocity; // Current speed of the rigid body
    float oldFallSpeed;


    [Header("Walk Bobbing")]
    [SerializeField]
    private Transform orientation;
    private Rigidbody rb;
    private Controls controls;

    public float bouncePower { get; private set; }
    public float fallSpeed { get; private set; }
    [SerializeField]
    private float walkBouncePower;
    [SerializeField]
    private float sprintBouncePower;
    [SerializeField]
    private float walkFallSpeed;
    [SerializeField]
    private float sprintFallSpeed;

    [Header("World Limits")]
    public float xLimit; // X Border limits for the map
    public float zLimit; // Z Border limits for the map

    [Header("Audio")]
    [SerializeField]
    private AudioClip[] skids;
    [SerializeField]
    private AudioClip[] landing;
    [SerializeField]
    private AudioClip[] wind;
    [HideInInspector]
    public AudioSource[] audioSource; // Source 0 for steps, 1 for other stuff
    [HideInInspector]
    public bool moving;

    // Start is called before the first frame update
    void Awake()
    {
        controls = GetComponent<Controls>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentSpeedLimit = 0.0f;
        charged = 1;

        audioSource = GetComponents<AudioSource>();
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, transform.lossyScale.y * 0.5f + 0.3f, RockyGround))
            Ground = RockyGround;
        else if (Physics.Raycast(transform.position, Vector3.down, transform.lossyScale.y * 0.5f + 0.3f, CleanGround))
            Ground = CleanGround;
        else Ground = Air;

        PlayerInput();
        SpeedLimit();

        if (Ground != Air) rb.drag = groundDrag;
        else rb.drag = 0.0f;

        if (Input.GetKeyDown(controls.Exit))
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        Movement();

        // Charge timer code
        if (rb.velocity.magnitude >= chargingThreshold || (directionalInput == Vector2.zero && sprintInput != 0.0f))
        {
            chargeTimer += Time.deltaTime;
        }
        else
        {
            chargeTimer -= Time.deltaTime;
            if (chargeTimer <= removalThreshold)
            {
                charged = 1;
                if (Camera.main.fieldOfView > 60.0f)
                {
                    Camera.main.fieldOfView -= 10.0f * Time.deltaTime;
                }
            }
        }

        if (chargeTimer < 0.0f)
        {
            chargeTimer = 0.0f;
        }
        else if (chargeTimer >= 5.0f) // 5.0 is just for the full bar length
        {
            chargeTimer = 5.0f;
            charged = 2;
            currentSpeedLimit = chargedSpeedLimit;
            if (Camera.main.fieldOfView < 70.0f)
            {
                Camera.main.fieldOfView += 10.0f * Time.deltaTime;
            }
        }
        Mathf.Clamp(Camera.main.fieldOfView, 60.0f, 70.0f);

        chargeBar.localScale = new Vector3(1.0f, chargeTimer * 0.2f, 1.0f);
        chargeBar.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, chargeTimer * 0.2f);
    }

    private void PlayerInput()
    {
        directionalInput.x = Input.GetAxisRaw("Vertical");
        directionalInput.y = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetAxisRaw("Jump");
        sprintInput = Input.GetAxisRaw("Sprint");

        if (directionalInput.x == 0.0f && directionalInput.y == 0.0f && Ground != Air)
        {
            if (currentSpeedLimit > walkSpeedLimit && moving)
            {
                audioSource[1].clip = skids[Random.Range(0, skids.Length)];
                audioSource[1].Play();
            }
            moving = false;
            if (currentSpeedLimit > 0.0f)
            {
                currentSpeedLimit -= groundDrag * Time.deltaTime;
                if (currentSpeedLimit < 0.0f)
                {
                    currentSpeedLimit = 0.0f;
                }
            }
        }
        else
        {
            moving = true;
            if (sprintInput != 0)
            {
                currentSpeedLimit += sprintSpeedLimit * Time.deltaTime;
                if (currentSpeedLimit > sprintSpeedLimit)
                {
                    currentSpeedLimit = sprintSpeedLimit;
                }
                bouncePower = sprintBouncePower;
                fallSpeed = sprintFallSpeed;
            }
            else
            {
                if (currentSpeedLimit > walkSpeedLimit)
                {
                    currentSpeedLimit -= walkSpeedLimit * Time.deltaTime;
                }
                else
                {
                    currentSpeedLimit = walkSpeedLimit;
                }
                bouncePower = walkBouncePower;
                fallSpeed = walkFallSpeed;
                currentSpeedLimit = walkSpeedLimit;
            }
        }
    }

    private void Movement()
    {
        moveDirection = orientation.forward * directionalInput.x + orientation.right * directionalInput.y;
        rb.AddForce(moveDirection.normalized * (Ground != Air ? currentSpeedLimit : airSpeedLimit), ForceMode.Force);
        rb.AddForce(0.0f, -gravity * Time.deltaTime, 0.0f, ForceMode.Force);

        if (jumpInput != 0 && Ground != Air)
        {
            rb.AddExplosionForce(jumpPower, new Vector3(transform.position.x, (transform.position.y - 0.6f) * charged, transform.position.z), 1.0f);
        }

        oldFallSpeed = currentVelocity.y;
        currentVelocity = rb.GetPointVelocity(transform.position);
        if (currentVelocity.y == 0.0f)
        {
            // Big thud or small thud?
            if (oldFallSpeed <= -8.0f)
            {
                audioSource[1].clip = landing[1];
                audioSource[1].Play();
            }
            else if (oldFallSpeed <= -5.0f)
            {
                audioSource[1].clip = landing[0];
                audioSource[1].Play();
            }
        }
    }

    private void SpeedLimit()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        if (flatVelocity.magnitude > currentSpeedLimit)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * currentSpeedLimit;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
}
