using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed; // Walking maximum
    public float sprintSpeed; // Sprinting maximum
    public float airSpeed; // Air maneuverability
    public float currentSpeed { get; private set; } // Speed being applied
    public float jumpPower;
    private Vector2 directionalInput;
    private float jumpInput;
    private Vector3 moveDirection;

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
    public AudioSource audioSource;
    public bool moving;

    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<Controls>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentSpeed = 0.0f;

        audioSource = GetComponent<AudioSource>();
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
    }

    private void PlayerInput()
    {
        directionalInput.x = Input.GetAxisRaw("Vertical");
        directionalInput.y = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetAxisRaw("Jump");

        if (directionalInput.x == 0.0f && directionalInput.y == 0.0f && Ground != Air)
        {
            if (currentSpeed > walkSpeed && moving)
            {
                audioSource.clip = skids[Random.Range(0, skids.Length)];
                audioSource.Play();
            }
            moving = false;
            if (currentSpeed > 0.0f)
            {
                currentSpeed -= groundDrag * Time.deltaTime;
                if (currentSpeed < 0.0f)
                {
                    currentSpeed = 0.0f;
                }
            }
        }
        else
        {
            moving = true;
            if (Input.GetKey(controls.Sprint))
            {
                currentSpeed += sprintSpeed * Time.deltaTime;
                if (currentSpeed > sprintSpeed)
                {
                    currentSpeed = sprintSpeed;
                }
                bouncePower = sprintBouncePower;
                fallSpeed = sprintFallSpeed;
            }
            else
            {
                if (currentSpeed > walkSpeed)
                {
                    currentSpeed -= walkSpeed * Time.deltaTime;
                }
                else
                {
                    currentSpeed = walkSpeed;
                }
                bouncePower = walkBouncePower;
                fallSpeed = walkFallSpeed;
            }
        }
    }

    private void Movement()
    {
        moveDirection = orientation.forward * directionalInput.x + orientation.right * directionalInput.y;
        rb.AddForce(moveDirection.normalized * (Ground != Air ? currentSpeed : airSpeed), ForceMode.Force);
        rb.AddForce(0.0f, -gravity * Time.deltaTime, 0.0f, ForceMode.Force);

        if (jumpInput != 0 && Ground != Air)
        {
            rb.AddExplosionForce(jumpPower, new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z), 1.0f);
        }

        oldFallSpeed = currentVelocity.y;
        currentVelocity = rb.GetPointVelocity(transform.position);
        if (currentVelocity.y == 0.0f)
        {
            // Big thud or small thud?
            if (oldFallSpeed <= -8.0f)
            {
                audioSource.clip = landing[1];
                audioSource.Play();
            }
            else if (oldFallSpeed <= -5.0f)
            {
                audioSource.clip = landing[0];
                audioSource.Play();
            }
        }
    }

    private void SpeedLimit()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        if (flatVelocity.magnitude > currentSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * currentSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
}
