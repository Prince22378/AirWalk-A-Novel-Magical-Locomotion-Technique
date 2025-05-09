// using UnityEngine;
// using UnityEngine.XR.Hands.Samples.GestureSample;

// /// <summary>
// /// Moves the CharacterController forward continuously while the left-hand "Thumbs Up" gesture is active.
// /// Attach this script to your XR Origin (which has a CharacterController).
// /// </summary>
// [RequireComponent(typeof(CharacterController))]
// public class GestureCharacterMover : MonoBehaviour
// {
//     [Header("Gesture Component")]
//     [Tooltip("StaticHandGesture for the left-hand Thumbs Up pose.")]
//     public StaticHandGesture thumbsUpGesture;

//     [Header("Movement Settings")]
//     [Tooltip("Movement speed in meters per second.")]
//     public float moveSpeed = 2.0f;

//     private CharacterController characterController;
//     private bool isMoving = false;

//     void Awake()
//     {
//         characterController = GetComponent<CharacterController>();
//         if (thumbsUpGesture == null)
//             Debug.LogWarning("GestureCharacterMover: thumbsUpGesture not assigned.");
//     }

//     void OnEnable()
//     {
//         // Subscribe to start/stop events
//         thumbsUpGesture.gesturePerformed.AddListener(OnThumbsUpStart);
//         thumbsUpGesture.gestureEnded.AddListener(OnThumbsUpEnd);
//     }

//     void OnDisable()
//     {
//         thumbsUpGesture.gesturePerformed.RemoveListener(OnThumbsUpStart);
//         thumbsUpGesture.gestureEnded.RemoveListener(OnThumbsUpEnd);
//     }

//     void Update()
//     {
//         if (isMoving)
//         {
//             // Move in the camera's forward direction, ignoring vertical
//             Vector3 forward = Camera.main.transform.forward;
//             forward.y = 0;
//             forward.Normalize();
//             characterController.Move(forward * moveSpeed * Time.deltaTime);
//         }
//     }

//     private void OnThumbsUpStart()
//     {
//         isMoving = true;
//     }

//     private void OnThumbsUpEnd()
//     {
//         isMoving = false;
//     }
// }




using UnityEngine;
using UnityEngine.XR.Hands.Samples.GestureSample;

/// <summary>
/// Moves the CharacterController forward while the thumbs-up gesture is active and applies gravity.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class GestureCharacterMover : MonoBehaviour
{
    [Header("Gesture Component")]
    [Tooltip("StaticHandGesture for the left-hand Thumbs Up pose.")]
    public StaticHandGesture thumbsUpGesture;

    [Header("Movement Settings")]
    [Tooltip("Movement speed in meters per second.")]
    public float moveSpeed = 2.0f;

    [Header("Gravity Settings")]
    [Tooltip("Gravity force applied in meters per second squared.")]
    public float gravity = -9.81f;
    [Tooltip("Small negative value to keep grounded.")]
    public float groundedYOffset = -0.5f;

    private CharacterController characterController;
    private bool isMoving = false;
    private float verticalVelocity = 0.0f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (thumbsUpGesture == null)
            Debug.LogWarning("GestureCharacterMover: thumbsUpGesture not assigned.");
    }

    void OnEnable()
    {
        thumbsUpGesture.gesturePerformed.AddListener(OnThumbsUpStart);
        thumbsUpGesture.gestureEnded.AddListener(OnThumbsUpEnd);
    }

    void OnDisable()
    {
        thumbsUpGesture.gesturePerformed.RemoveListener(OnThumbsUpStart);
        thumbsUpGesture.gestureEnded.RemoveListener(OnThumbsUpEnd);
    }

    void Update()
    {
        Vector3 move = Vector3.zero;

        // Movement when gesture is active
        if (isMoving)
        {
            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0;
            forward.Normalize();
            move += forward * moveSpeed;
        }

        // Apply gravity
        if (characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = groundedYOffset; // Slight downward force to stay grounded
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        characterController.Move(move * Time.deltaTime);
    }

    private void OnThumbsUpStart()
    {
        isMoving = true;
    }

    private void OnThumbsUpEnd()
    {
        isMoving = false;
    }
}
