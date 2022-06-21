using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed;
    public float sprintSpeed;
    public float currentSpeed { get; private set; }
    private Vector2 directionalInput;
    private Vector3 moveDirection;
    [SerializeField]
    private float groundDrag;
    [SerializeField]
    private bool grounded;
    public LayerMask Ground;

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
        PlayerInput();
        SpeedLimit();

        if (grounded) rb.drag = groundDrag;
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

        if (directionalInput.x == 0.0f && directionalInput.y == 0.0f)
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
        rb.AddForce(moveDirection.normalized * currentSpeed * 10.0f, ForceMode.Force);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Ground) grounded = true;
    }
}
