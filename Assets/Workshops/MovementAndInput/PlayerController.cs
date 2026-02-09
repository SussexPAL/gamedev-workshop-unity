using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Serialized Fields: Variables that are visible in the inspector. Values can be changed without editing any code.
    [SerializeField] private float speed = .5f;             // How many units per second the player should move from WASD
    [SerializeField] private float jumpHeight = 2f;         // How many units high the player should jump
    [SerializeField] private float gravity = 9.8f;          // How many units per second the player's velocity.y will decrease.
    [SerializeField] private float groundSlamSpeed = 3;     // What to multiply gravity by when the player is holding crouch
    [SerializeField] private bool creative = false;         // Set to true if the player should have creative flight, like Minecraft.

    // Private fields: Used by this script frequently to justify global scope
    private CharacterController controller;                 // Reference to the CharacterController component, a Unity helper class for handling player movement.
    private Vector3 moveInput;                              // Contains the player current movement input: (x, y)
    private Vector3 velocity;                               // Stores the velocity used to calculate movement this frame
    private float jumpPressed = 0;                            // 1 if the player is currently holding jump
    private bool jumpJustPressed = false;                   // True if jump was pressed this frame, false otherwise.
    private float crouchPressed = 0;                          // 1 if the player is currently holding crouch
    private bool crouchJustPressed = false;                 // True if the player just hit crouch this frame
    private bool creativeToggleJustPressed = false;         // True if the creative toggle button was pressed this frame

    // Called once at GameObject initialization (right before the first update() call)
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame (variable)
    private void Update()
    {   
        // Gamemode switching
        if (creativeToggleJustPressed)
        {
            creative = !creative;
        }

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

        // Move the player using velocity.
        Debug.Log($"Grounded: {controller.isGrounded}");
        Debug.Log($"Velocity: {velocity}");
        controller.Move(velocity * Time.deltaTime);         // Time.deltaTime ensures consistent behaviour independent of framerate.
    }

    // Called after *every* gameObject has completed its update(). 
    private void LateUpdate()
    {
        jumpJustPressed = false;
        creativeToggleJustPressed = false;
        crouchJustPressed = false;
    }


    // Utility Functions: Code blocks called by start()/update() that was extracted into their own functions for organization's sake.
    // ==============================================================================================================================

    // Handle basic player movement (WASD or left-stick movement)
    private void doPlayerMovement()
    {
        velocity.x = moveInput.x * speed;
        velocity.z = moveInput.z * speed;
    }

    // Handle player jumping logic (and gravity, as flight is disabled)
    private void doPlayerJumping()
    {
        velocity.y -= gravity * (groundSlamSpeed*crouchPressed+1f) * Time.deltaTime;         // Apply gravity if not grounded. Also perform ground slam (increase gravity) if crouch is held.
        if(controller.isGrounded)
        {   
            velocity.y = -3f;
            if(jumpJustPressed)
            {
                velocity.y = (float) Math.Sqrt(jumpHeight * 2f * gravity);
            }
        }
    }

    private void doPlayerFlight()
    {    
        float flightDir = jumpPressed - crouchPressed; // 1 if trying to fly, -1 if trying to descend, 0 if neither (or both)
        velocity.y = flightDir * speed;
    }


    // Event handlers: Will be automatically called when some external event takes place, like a player input
    // =======================================================================================================================

    // Only updates the moveInput vector rather than moving the player directly.
    // This is because onMove is only called when the input is pressed/released, while we want movement to occur the entire time the input is held.
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput.x = context.ReadValue<Vector2>().x;
        moveInput.z = context.ReadValue<Vector2>().y;
        Debug.Log($"Move Input: {moveInput}");
    }

    // Called when the player presses or releases a jump input (like space, or A)
    public void onJump(InputAction.CallbackContext context)
    {   
        jumpPressed = context.ReadValue<float>();
        jumpJustPressed = context.performed;
    }

    // Called when the player presses or releases the creative flight toggle button (C)
    public void onCreativeToggle(InputAction.CallbackContext context)
    {
        creativeToggleJustPressed = context.performed;
    }

    // Called when the player presses or releases the crouch button
    public void onCrouch(InputAction.CallbackContext context)
    {
        crouchPressed = context.ReadValue<float>();
        crouchJustPressed = context.performed;
    }
}
