using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    // Serialized Fields: Variables that are visible in the inspector. Values can be changed without editing any code.
    [SerializeField] public float mouseSensitivity = 2f;
    [SerializeField] private float speed = .5f;             // How strong of a force to apply for general movement
    [SerializeField] private float jumpHeight = 2f;         // How strong of a force to apply for jumping
    [SerializeField] private float groundSlamSpeed = 3;     // How strong of a force to apply for ground pound 
    [SerializeField] private float jumpTorque = 10f;        // How much to rotate the player upon a jump
    [SerializeField] private bool creative = false;         // Set to true if the player should have creative flight, like Minecraft.
    [SerializeField] private Camera cam;
    [SerializeField] private float maxVelocity;
    // Private fields: Used by this script frequently to justify global scope
    private Rigidbody rb;                                   // Reference to the player's rigidbody, which is responsible for physics calculations
    
    // Current input state
    private Vector3 moveInput;                                // Contains the player current movement input: (x, y)
    private Vector2 lookInput;
    private float jumpPressed = 0;                            // 1 if the player is currently holding jump
    private bool jumpRequested = false;                       // True if jump was pressed this frame, false otherwise.
    private float crouchPressed = 0;                          // 1 if the player is currently holding crouch
    private bool creativeToggleRequested = false;             // True if the creative toggle button was pressed this frame
    float verticalLookRotation = 0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Find the rigidbody attached to this gameObject
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {   
        doMouseLook();
        // Gamemode switching
        if (creativeToggleRequested)
        {
            creative = !creative;
        }
        creativeToggleRequested = false;
    }

    private void FixedUpdate() {
        // WASD Movement (or controller!)
        doPlayerMovement();

        // Gravity and Jumping
        if(creative)
        {
            doPlayerFlight();
        } else
        {   
            doPlayerJumping();
        }

        // Reset "Pressed this frame" flags to false.
        jumpRequested = false;
        Vector2 horizontalVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z);
        float velocityRatio = horizontalVelocity.magnitude/maxVelocity;
        if(velocityRatio > 1.0)
        {
            horizontalVelocity = horizontalVelocity / velocityRatio;
            rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.y);
        }
    }

    // Utility Functions:
    // ==============================================================================================================================

    void doMouseLook()
    {
        float mouseX = lookInput.x * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * 100f * Time.deltaTime;

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }


    // Handle basic player movement (WASD or left-stick movement)
    private void doPlayerMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.z;
        rb.AddForce(move.normalized * speed, ForceMode.VelocityChange);
    }

    // Handle creative mode flight (if enabled)
    private void doPlayerFlight()
    {    
        float flightDir = jumpPressed - crouchPressed; // 1 if trying to fly, -1 if trying to descend, 0 if neither (or both)
        
        Vector3 movementDir = new Vector3(moveInput.normalized.x, flightDir, moveInput.normalized.z);
        rb.AddForce(movementDir * speed, ForceMode.Force);
    }

    // Handle player jumping logic (if not in creative mode)
    private void doPlayerJumping()
    {
        if(jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            rb.AddTorque(rb.linearVelocity * jumpTorque, ForceMode.Impulse);   // Rotate the player based on current movement direction
        }

        // Fall faster if player is holding crouch
        rb.AddForce(Vector3.down * crouchPressed * groundSlamSpeed, ForceMode.Force);
    }

    


    // Event handlers: Will be automatically called when some external event takes place, like a player input
    //                  Ideally should only be responsible for *reading* input, not for performing actions.
    // =======================================================================================================================

    // Called when the player presses or releases any movement input (WASD)
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput.x = context.ReadValue<Vector2>().x;
        moveInput.z = context.ReadValue<Vector2>().y;
    }

    // Called when the player presses or releases any movement input (WASD)
    public void onLook(InputAction.CallbackContext context)
    {
        lookInput.x = context.ReadValue<Vector2>().x;
        lookInput.y = context.ReadValue<Vector2>().y;
    }

    // Called when the player presses or releases the jump button
    public void onJump(InputAction.CallbackContext context)
    {   
        jumpPressed = context.ReadValue<float>();
        if (context.performed)
        {
            jumpRequested = true;
        }
    }

    // Called when the player presses or releases the creative flight toggle button (C)
    public void onCreativeToggle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            creativeToggleRequested = true;
            // Realistically we could just switch modes here. But we may want to disable that functionality in
            // certain contexts, or during animations, or whatever. Keeping the action external to this function
            // will make those types of changes easier.
        }
    }

    // Called when the player presses or releases the crouch button
    public void onCrouch(InputAction.CallbackContext context)
    {
        crouchPressed = context.ReadValue<float>();
    }
}
