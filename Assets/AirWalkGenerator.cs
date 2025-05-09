// // // // using UnityEngine;
// // // // using UnityEngine.XR;
// // // // using UnityEngine.XR.Interaction.Toolkit;
// // // // using System.Collections.Generic;

// // // // public class AirWalkGenerator : MonoBehaviour
// // // // {
// // // //     /* ──────────── Prefab & Anchors ──────────── */
// // // //     [Header("Prefabs & Anchors")]
// // // //     [SerializeField] private GameObject platformPrefab;
// // // //     [SerializeField] private Transform  rightHandTransform;   // XR controller transform

// // // //     /* ──────────── Step Settings ──────────── */
// // // //     [Header("Step Settings")]
// // // //     [SerializeField] private float horizontalStep = 0.5f;     // front distance
// // // //     [SerializeField] private float verticalStep   = 0.6f;     // up / down offset
// // // //     [SerializeField] private float gridSize      = 0.1f;      // snap size (10 cm)

// // // //     /* ──────────── Collision / Pool ──────────── */
// // // //     [Header("Collision / Pooling")]
// // // //     [SerializeField] private LayerMask platformLayer;
// // // //     [SerializeField] private int       maxPlatforms = 40;

// // // //     /* ──────────── Feedback ──────────── */
// // // //     [Header("Feedback")]
// // // //     [SerializeField] private XRBaseController rightXRController; // drag‑in XR Controller
// // // //     [SerializeField] private float   hapticAmplitude = 0.3f;
// // // //     [SerializeField] private float   hapticDuration  = 0.08f;
// // // //     [SerializeField] private AudioSource audioSource;            // optional
// // // //     [SerializeField] private AudioClip  errorClip;
// // // //     [SerializeField] private AudioClip  placeClip;

// // // //     /* ──────────── Private state ──────────── */
// // // //     private Vector3 prefabHalfExtents;
// // // //     private readonly HashSet<Vector3Int> occupiedCells = new();
// // // //     private readonly Queue<GameObject>   spawned       = new();
// // // //     private float pressCooldown = 0.15f;
// // // //     private float lastPressTime = 0f;

// // // //     /* ──────────── Unity lifecycle ──────────── */
// // // //     private void Awake()
// // // //     {
// // // //         var col = platformPrefab.GetComponent<Collider>();
// // // //         prefabHalfExtents = col ? col.bounds.extents : Vector3.one * 0.25f;
// // // //     }

// // // //     private void Update()
// // // //     {
// // // //         if (Time.time - lastPressTime < pressCooldown) return;

// // // //         InputDevice right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
// // // //         if (!right.isValid) return;

// // // //         bool primary, secondary, grip;
// // // //         right.TryGetFeatureValue(CommonUsages.primaryButton,    out primary);
// // // //         right.TryGetFeatureValue(CommonUsages.secondaryButton,  out secondary);
// // // //         right.TryGetFeatureValue(CommonUsages.gripButton,       out grip);

// // // //         if (primary)   { TryPlace(Level.Neutral); lastPressTime = Time.time; }
// // // //         else if (secondary) { TryPlace(Level.Up);   lastPressTime = Time.time; }
// // // //         else if (grip)      { TryPlace(Level.Down); lastPressTime = Time.time; }
// // // //     }

// // // //     /* ──────────── Core placement logic ──────────── */
// // // //     private enum Level { Neutral, Up, Down }

// // // //     private void TryPlace(Level lvl)
// // // //     {
// // // //         Vector3 pos = rightHandTransform.position + rightHandTransform.forward.normalized * horizontalStep;
// // // //         if (lvl == Level.Up)   pos += Vector3.up   * verticalStep;
// // // //         if (lvl == Level.Down) pos += Vector3.down * verticalStep;

// // // //         Vector3Int cell = new Vector3Int(
// // // //             Mathf.RoundToInt(pos.x / gridSize),
// // // //             Mathf.RoundToInt(pos.y / gridSize),
// // // //             Mathf.RoundToInt(pos.z / gridSize));

// // // //         if (occupiedCells.Contains(cell))
// // // //         {
// // // //             FeedbackError();
// // // //             return;
// // // //         }

// // // //         Vector3 snappedPos = new Vector3(
// // // //             cell.x * gridSize,
// // // //             cell.y * gridSize,
// // // //             cell.z * gridSize);

// // // //         if (Physics.OverlapBox(snappedPos, prefabHalfExtents, Quaternion.identity, platformLayer).Length > 0)
// // // //         {
// // // //             FeedbackError();
// // // //             return;
// // // //         }

// // // //         GameObject plat = Instantiate(platformPrefab, snappedPos, Quaternion.identity);
// // // //         plat.layer = Mathf.RoundToInt(Mathf.Log(platformLayer.value, 2));

// // // //         occupiedCells.Add(cell);
// // // //         spawned.Enqueue(plat);
// // // //         FeedbackSuccess();

// // // //         if (spawned.Count > maxPlatforms)
// // // //         {
// // // //             GameObject oldest = spawned.Dequeue();
// // // //             Vector3Int oldCell = new Vector3Int(
// // // //                 Mathf.RoundToInt(oldest.transform.position.x / gridSize),
// // // //                 Mathf.RoundToInt(oldest.transform.position.y / gridSize),
// // // //                 Mathf.RoundToInt(oldest.transform.position.z / gridSize));
// // // //             occupiedCells.Remove(oldCell);
// // // //             Destroy(oldest);
// // // //         }
// // // //     }

// // // //     /* ──────────── Feedback helpers ──────────── */
// // // //     private void FeedbackSuccess()
// // // //     {
// // // //         SendHaptic(0.2f, 0.05f);
// // // //         PlayClip(placeClip);
// // // //     }

// // // //     private void FeedbackError()
// // // //     {
// // // //         SendHaptic(hapticAmplitude, hapticDuration);
// // // //         PlayClip(errorClip);
// // // //     }

// // // //     private void SendHaptic(float amp, float dur)
// // // //     {
// // // //         if (rightXRController != null)
// // // //             rightXRController.SendHapticImpulse(amp, dur);
// // // //     }

// // // //     private void PlayClip(AudioClip clip)
// // // //     {
// // // //         if (audioSource != null && clip != null)
// // // //             audioSource.PlayOneShot(clip);
// // // //     }
// // // // }



// // // /*
// // //  * AirWalkGenerator  —  v2.2  (dual‑controller, fixed‑platform edition)
// // //  *
// // //  *  Left‑hand X   →  platform BELOW feet
// // //  *  Right‑hand A  →  platform ABOVE feet
// // //  *  Right‑hand B  →  platform AT foot level
// // //  *
// // //  *  • Platforms are snapped to a 10 cm grid so they never overlap.
// // //  *  • A rigidbody (if present on the prefab) is frozen & set kinematic so the
// // //  *    platform stays perfectly still after it spawns.
// // //  */

// // // using UnityEngine;
// // // using UnityEngine.XR;
// // // using UnityEngine.XR.Interaction.Toolkit;
// // // using System.Collections.Generic;

// // // public class AirWalkGenerator : MonoBehaviour
// // // {
// // //     /* ────── Prefab & Anchors ────── */
// // //     [Header("Prefabs & Anchors")]
// // //     [SerializeField] private GameObject platformPrefab;
// // //     [SerializeField] private Transform  rightHandTransform;  // assign XR right‑hand
// // //     [SerializeField] private Transform  leftHandTransform;   // assign XR left‑hand

// // //     /* ────── Step Settings ────── */
// // //     [Header("Step Settings")]
// // //     [SerializeField] private float horizontalStep = 0.5f;   // forward distance
// // //     [SerializeField] private float verticalStep   = 0.6f;   // up / down offset
// // //     [SerializeField] private float gridSize      = 0.1f;    // 10 cm snap

// // //     /* ────── Collision / Pool ────── */
// // //     [Header("Collision / Pooling")]
// // //     [SerializeField] private LayerMask platformLayer;
// // //     [SerializeField] private int       maxPlatforms = 60;

// // //     /* ────── Feedback ────── */
// // //     [Header("Feedback")]
// // //     [SerializeField] private XRBaseController rightXRController;
// // //     [SerializeField] private XRBaseController leftXRController;
// // //     [SerializeField] private float   hapticAmplitude = 0.35f;
// // //     [SerializeField] private float   hapticDuration  = 0.08f;
// // //     [SerializeField] private AudioSource audioSource;
// // //     [SerializeField] private AudioClip  errorClip;
// // //     [SerializeField] private AudioClip  placeClip;

// // //     /* ────── Private state ────── */
// // //     private Vector3 prefabHalfExtents;
// // //     private readonly HashSet<Vector3Int> occupiedCells = new();
// // //     private readonly Queue<GameObject>   spawned       = new();
// // //     private float pressCooldown = 0.15f;
// // //     private float lastPressTime = 0f;

// // //     /* ────── Init ────── */
// // //     private void Awake()
// // //     {
// // //         var col = platformPrefab.GetComponent<Collider>();
// // //         prefabHalfExtents = col ? col.bounds.extents : Vector3.one * 0.25f;
// // //     }

// // //     /* ────── Per‑frame input polling (raw XR API) ────── */
// // //     private void Update()
// // //     {
// // //         if (Time.time - lastPressTime < pressCooldown) return;

// // //         InputDevice right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
// // //         InputDevice left  = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
// // //         if (!right.isValid && !left.isValid) return;

// // //         bool rPrimary, rSecondary, lPrimary;
// // //         right.TryGetFeatureValue(CommonUsages.primaryButton,   out rPrimary);     // A
// // //         right.TryGetFeatureValue(CommonUsages.secondaryButton, out rSecondary);   // B
// // //         left .TryGetFeatureValue(CommonUsages.primaryButton,   out lPrimary);     // X

// // //         if (rPrimary)      { TryPlace(Level.Up);      lastPressTime = Time.time; }
// // //         else if (rSecondary){ TryPlace(Level.Neutral); lastPressTime = Time.time; }
// // //         else if (lPrimary) { TryPlace(Level.Down);    lastPressTime = Time.time; }
// // //     }

// // //     /* ────── Placement logic ────── */
// // //     private enum Level { Neutral, Up, Down }

// // //     private void TryPlace(Level lvl)
// // //     {
// // //         Vector3 pos = rightHandTransform.position + rightHandTransform.forward.normalized * horizontalStep;
// // //         if (lvl == Level.Up)   pos += Vector3.up   * verticalStep;
// // //         if (lvl == Level.Down) pos += Vector3.down * verticalStep;

// // //         Vector3Int cell = new Vector3Int(
// // //             Mathf.RoundToInt(pos.x / gridSize),
// // //             Mathf.RoundToInt(pos.y / gridSize),
// // //             Mathf.RoundToInt(pos.z / gridSize));

// // //         if (occupiedCells.Contains(cell))
// // //         {
// // //             FeedbackError();
// // //             return;
// // //         }

// // //         Vector3 snappedPos = new Vector3(
// // //             cell.x * gridSize,
// // //             cell.y * gridSize,
// // //             cell.z * gridSize);

// // //         if (Physics.OverlapBox(snappedPos, prefabHalfExtents, Quaternion.identity, platformLayer).Length > 0)
// // //         {
// // //             FeedbackError();
// // //             return;
// // //         }

// // //         GameObject plat = Instantiate(platformPrefab, snappedPos, Quaternion.identity);
// // //         plat.layer = Mathf.RoundToInt(Mathf.Log(platformLayer.value, 2));

// // //         // Make sure the platform stays perfectly still
// // //         Rigidbody rb = plat.GetComponent<Rigidbody>();
// // //         if (rb != null)
// // //         {
// // //             rb.isKinematic = true;
// // //             rb.useGravity  = false;
// // //             rb.constraints = RigidbodyConstraints.FreezeAll;
// // //         }

// // //         occupiedCells.Add(cell);
// // //         spawned.Enqueue(plat);
// // //         FeedbackSuccess();

// // //         if (spawned.Count > maxPlatforms)
// // //         {
// // //             GameObject oldest = spawned.Dequeue();
// // //             Vector3Int oldCell = new Vector3Int(
// // //                 Mathf.RoundToInt(oldest.transform.position.x / gridSize),
// // //                 Mathf.RoundToInt(oldest.transform.position.y / gridSize),
// // //                 Mathf.RoundToInt(oldest.transform.position.z / gridSize));
// // //             occupiedCells.Remove(oldCell);
// // //             Destroy(oldest);
// // //         }
// // //     }

// // //     /* ────── Feedback helpers ────── */
// // //     private void FeedbackSuccess()
// // //     {
// // //         SendHaptic(0.2f, 0.05f, rightXRController);
// // //         SendHaptic(0.2f, 0.05f, leftXRController);
// // //         PlayClip(placeClip);
// // //     }

// // //     private void FeedbackError()
// // //     {
// // //         SendHaptic(hapticAmplitude, hapticDuration, rightXRController);
// // //         SendHaptic(hapticAmplitude, hapticDuration, leftXRController);
// // //         PlayClip(errorClip);
// // //     }

// // //     private void SendHaptic(float amp, float dur, XRBaseController ctrl)
// // //     {
// // //         if (ctrl != null) ctrl.SendHapticImpulse(amp, dur);
// // //     }

// // //     private void PlayClip(AudioClip clip)
// // //     {
// // //         if (audioSource != null && clip != null)
// // //             audioSource.PlayOneShot(clip);
// // //     }
// // // }





// // /*
// //  * AirWalkGenerator  —  v2.3  (fixed‑height offsets, dual‑controller)
// //  *
// //  *  Left‑hand X  →  0.30 m BELOW player feet
// //  *  Right‑hand A →  0.30 m ABOVE player feet
// //  *  Right‑hand B →  exactly AT player feet
// //  *
// //  *  “Player feet” are taken from the CharacterController bottom, so the
// //  *  platforms no longer follow the ray height; they stay locked to the
// //  *  avatar’s foot plane ±0.30 m.
// //  */

// // using UnityEngine;
// // using UnityEngine.XR;
// // using UnityEngine.XR.Interaction.Toolkit;
// // using System.Collections.Generic;

// // public class AirWalkGenerator : MonoBehaviour
// // {
// //     /* ────── References ────── */
// //     [Header("References")]
// //     [SerializeField] private GameObject       platformPrefab;
// //     [SerializeField] private Transform        rightHandTransform;
// //     [SerializeField] private Transform        leftHandTransform;
// //     [SerializeField] private CharacterController characterController; // ← drag your CC here

// //     /* ────── Placement settings ────── */
// //     [Header("Placement")]
// //     [SerializeField] private float horizontalStep = 0.5f; // forward distance
// //     [SerializeField] private float verticalOffset = 0.30f; // ±0.30 m
// //     [SerializeField] private float gridSize      = 0.1f;  // 10 cm snap

// //     /* ────── Collision / Pool ────── */
// //     [Header("Collision / Pooling")]
// //     [SerializeField] private LayerMask platformLayer;
// //     [SerializeField] private int       maxPlatforms = 11;

// //     /* ────── Feedback ────── */
// //     [Header("Feedback")]
// //     [SerializeField] private XRBaseController rightXRController;
// //     [SerializeField] private XRBaseController leftXRController;
// //     [SerializeField] private float   hapticAmplitude = 0.35f;
// //     [SerializeField] private float   hapticDuration  = 0.08f;
// //     [SerializeField] private AudioSource audioSource;
// //     [SerializeField] private AudioClip  errorClip;
// //     [SerializeField] private AudioClip  placeClip;

// //     /* ────── Runtime state ────── */
// //     private Vector3 prefabHalfExtents;
// //     private readonly HashSet<Vector3Int> occupiedCells = new();
// //     private readonly Queue<GameObject>   spawned       = new();
// //     private float pressCooldown = 0.15f;
// //     private float lastPressTime = 0f;

// //     /* ────── Init ────── */
// //     private void Awake()
// //     {
// //         var col = platformPrefab.GetComponent<Collider>();
// //         prefabHalfExtents = col ? col.bounds.extents : Vector3.one * 0.25f;
// //     }

// //     /* ────── Per‑frame polling ────── */
// //     private void Update()
// //     {
// //         if (Time.time - lastPressTime < pressCooldown) return;

// //         InputDevice right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
// //         InputDevice left  = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
// //         if (!right.isValid && !left.isValid) return;

// //         bool rA, rB, lX;
// //         right.TryGetFeatureValue(CommonUsages.primaryButton,   out rA); // A
// //         right.TryGetFeatureValue(CommonUsages.secondaryButton, out rB); // B
// //         left .TryGetFeatureValue(CommonUsages.primaryButton,   out lX); // X

// //         if (rA)      { TryPlace(Level.Up);      lastPressTime = Time.time; }
// //         else if (rB) { TryPlace(Level.Neutral); lastPressTime = Time.time; }
// //         else if (lX) { TryPlace(Level.Down);    lastPressTime = Time.time; }
// //     }

// //     /* ────── Placement logic ────── */
// //     private enum Level { Neutral, Up, Down }

// //     private void TryPlace(Level lvl)
// //     {
// //         /* 1. Base horizontal position (still in front of hand) */
// //         Vector3 basePos = rightHandTransform.position +
// //                           rightHandTransform.forward.normalized * horizontalStep;

// //         /* 2. Calculate foot‑plane Y from CharacterController */
// //         float footY = characterController.transform.position.y
// //                     + characterController.center.y
// //                     - characterController.height * 0.5f;

// //         /* 3. Apply vertical offset */
// //         float targetY = footY;
// //         if (lvl == Level.Up)   targetY += verticalOffset;
// //         if (lvl == Level.Down) targetY -= verticalOffset;

// //         Vector3 pos = new Vector3(basePos.x, targetY, basePos.z);

// //         /* 4. Grid snap */
// //         Vector3Int cell = new Vector3Int(
// //             Mathf.RoundToInt(pos.x / gridSize),
// //             Mathf.RoundToInt(pos.y / gridSize),
// //             Mathf.RoundToInt(pos.z / gridSize));

// //         if (occupiedCells.Contains(cell)) { FeedbackError(); return; }

// //         Vector3 snappedPos = new Vector3(
// //             cell.x * gridSize,
// //             cell.y * gridSize,
// //             cell.z * gridSize);

// //         /* 5. Overlap test */
// //         if (Physics.OverlapBox(snappedPos, prefabHalfExtents,
// //                                Quaternion.identity, platformLayer).Length > 0)
// //         { FeedbackError(); return; }

// //         /* 6. Spawn & freeze */
// //         GameObject plat = Instantiate(platformPrefab, snappedPos, Quaternion.identity);
// //         plat.layer = Mathf.RoundToInt(Mathf.Log(platformLayer.value, 2));

// //         Rigidbody rb = plat.GetComponent<Rigidbody>();
// //         if (rb != null)
// //         {
// //             rb.isKinematic = true;
// //             rb.useGravity  = false;
// //             rb.constraints = RigidbodyConstraints.FreezeAll;
// //         }

// //         occupiedCells.Add(cell);
// //         spawned.Enqueue(plat);
// //         FeedbackSuccess();

// //         /* 7. Pool */
// //         if (spawned.Count > maxPlatforms)
// //         {
// //             GameObject oldest = spawned.Dequeue();
// //             Vector3Int oldCell = new Vector3Int(
// //                 Mathf.RoundToInt(oldest.transform.position.x / gridSize),
// //                 Mathf.RoundToInt(oldest.transform.position.y / gridSize),
// //                 Mathf.RoundToInt(oldest.transform.position.z / gridSize));
// //             occupiedCells.Remove(oldCell);
// //             Destroy(oldest);
// //         }
// //     }

// //     /* ────── Feedback helpers ────── */
// //     private void FeedbackSuccess()
// //     {
// //         SendHaptic(0.2f, 0.05f, rightXRController);
// //         SendHaptic(0.2f, 0.05f, leftXRController);
// //         PlayClip(placeClip);
// //     }
// //     private void FeedbackError()
// //     {
// //         SendHaptic(hapticAmplitude, hapticDuration, rightXRController);
// //         SendHaptic(hapticAmplitude, hapticDuration, leftXRController);
// //         PlayClip(errorClip);
// //     }
// //     private void SendHaptic(float amp, float dur, XRBaseController ctrl)
// //     {
// //         if (ctrl != null) ctrl.SendHapticImpulse(amp, dur);
// //     }
// //     private void PlayClip(AudioClip clip)
// //     {
// //         if (audioSource != null && clip != null) audioSource.PlayOneShot(clip);
// //     }
// // }





// /*
//  * AirWalkGenerator  —  v2.4  (single‑CharacterController safe)
//  *
//  *  Left‑hand  X  →  0.30 m BELOW feet   (footY - verticalOffset)
//  *  Right‑hand A  →  0.30 m ABOVE feet   (footY + verticalOffset)
//  *  Right‑hand B  →  exactly AT feet     (footY)
//  */

// using UnityEngine;
// using UnityEngine.XR;
// using UnityEngine.XR.Interaction.Toolkit;
// using System.Collections.Generic;

// public class AirWalkGenerator : MonoBehaviour
// {
//     /* ────── References ────── */
//     [Header("References")]
//     [SerializeField] private GameObject  platformPrefab;
//     [SerializeField] private Transform   rightHandTransform;
//     [SerializeField] private Transform   leftHandTransform;
//     [Tooltip("Drag your ONE CharacterController here. If left empty, the script will auto‑find the first active CC in the scene.")]
//     [SerializeField] private CharacterController characterController;

//     /* ────── Placement settings ────── */
//     [Header("Placement")]
//     [SerializeField] private float horizontalStep  = 0.3f;   // forward distance
//     [SerializeField] private float verticalOffset  = 0.30f;  // ±0.30 m
//     [Tooltip("Fine‑tune if the platform spawns a few cm too high/low. Positive value pushes it up.")]
//     [SerializeField] private float fineTuneYOffset = -0.1f;   // tweak in cm if needed
//     // [SerializeField] private float gridSize        = 0.1f;   // 10 cm snap

//     [Header("Grid Size")]
//     [SerializeField] private float gridXZ = 0.10f;
//     [SerializeField] private float gridY = 0.10f; 

//     /* ────── Collision / Pool ────── */
//     [Header("Collision / Pooling")]
//     [SerializeField] private LayerMask platformLayer;
//     [SerializeField] private int       maxPlatforms = 11;

//     /* ────── Feedback ────── */
//     [Header("Feedback")]
//     [SerializeField] private XRBaseController rightXRController;
//     [SerializeField] private XRBaseController leftXRController;
//     [SerializeField] private float   hapticAmplitude = 0.35f;
//     [SerializeField] private float   hapticDuration  = 0.08f;
//     [SerializeField] private AudioSource audioSource;
//     [SerializeField] private AudioClip  errorClip;
//     [SerializeField] private AudioClip  placeClip;

//     /* ────── Runtime state ────── */
//     private Vector3 prefabHalfExtents;
//     private readonly HashSet<Vector3Int> occupiedCells = new();
//     private readonly Queue<GameObject>   spawned       = new();
//     private float pressCooldown = 0.15f;
//     private float lastPressTime = 0f;

//     /* ────── Init ────── */
//     private void Awake()
//     {
//         // Cache prefab extents
//         var col = platformPrefab.GetComponent<Collider>();
//         prefabHalfExtents = col ? col.bounds.extents : Vector3.one * 0.25f;

//         // Auto‑find the main CharacterController if none assigned
//         if (characterController == null)
//         {
//             characterController = FindObjectOfType<CharacterController>();
//             if (characterController != null)
//                 Debug.Log($"[AirWalk] Auto‑using CharacterController on <{characterController.name}>");
//             else
//                 Debug.LogError("[AirWalk] No CharacterController found! Platforms will default to y = 0.");
//         }
//     }

//     /* ────── Per‑frame polling ────── */
//     private void Update()
//     {
//         if (Time.time - lastPressTime < pressCooldown) return;

//         InputDevice right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
//         InputDevice left  = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
//         if (!right.isValid && !left.isValid) return;

//         bool rA, rB, lX;
//         right.TryGetFeatureValue(CommonUsages.primaryButton,   out rA); // A
//         right.TryGetFeatureValue(CommonUsages.secondaryButton, out rB); // B
//         left .TryGetFeatureValue(CommonUsages.primaryButton,   out lX); // X

//         if (rA)      { TryPlace(Level.Up);      lastPressTime = Time.time; }
//         else if (rB) { TryPlace(Level.Neutral); lastPressTime = Time.time; }
//         else if (lX) { TryPlace(Level.Down);    lastPressTime = Time.time; }
//     }

//     /* ────── Placement logic ────── */
//     private enum Level { Neutral, Up, Down }

//     private void TryPlace(Level lvl)
//     {
//         /* 1. Horizontal position: always in front of RIGHT hand */
//         Vector3 basePos = rightHandTransform.position +
//                           rightHandTransform.forward.normalized * horizontalStep;

//         /* 2. Foot plane calculation */
//         float footY;
//         if (characterController != null)
//         {
//             footY = characterController.transform.position.y
//                   + characterController.center.y
//                   - characterController.height * 0.5f;
//         }
//         else
//         {
//             footY = 0f; // fallback
//         }
//         footY += fineTuneYOffset; // manual nudge if needed

//         /* 3. Apply vertical offset */
//         float targetY = lvl switch
//         {
//             Level.Up      => footY + verticalOffset,
//             Level.Down    => footY - verticalOffset,
//             _             => footY
//         };

//         Vector3 pos = new Vector3(basePos.x, targetY, basePos.z);

//         /* 4. Grid snap */
//         Vector3Int cell = new Vector3Int(
//             Mathf.RoundToInt(pos.x / gridXZ),
//             Mathf.RoundToInt(pos.y / gridY),
//             Mathf.RoundToInt(pos.z / gridXZ));

//         if (occupiedCells.Contains(cell)) { FeedbackError(); return; }

//         Vector3 snappedPos = new Vector3(
//             cell.x * gridXZ,
//             cell.y * gridY,
//             cell.z * gridXZ);

//         /* 5. Overlap test */
//         if (Physics.OverlapBox(snappedPos, prefabHalfExtents,
//                                Quaternion.identity, platformLayer).Length > 0)
//         { FeedbackError(); return; }

//         /* 6. Spawn & freeze */
//         GameObject plat = Instantiate(platformPrefab, snappedPos, Quaternion.identity);
//         plat.layer = Mathf.RoundToInt(Mathf.Log(platformLayer.value, 2));

//         Rigidbody rb = plat.GetComponent<Rigidbody>();
//         if (rb != null)
//         {
//             rb.isKinematic = true;
//             rb.useGravity  = false;
//             rb.constraints = RigidbodyConstraints.FreezeAll;
//         }

//         occupiedCells.Add(cell);
//         spawned.Enqueue(plat);
//         FeedbackSuccess();

//         /* 7. Pool oldest */
//         if (spawned.Count > maxPlatforms)
//         {
//             GameObject oldest = spawned.Dequeue();
//             Vector3Int oldCell = new Vector3Int(
//                 Mathf.RoundToInt(oldest.transform.position.x / gridXZ),
//                 Mathf.RoundToInt(oldest.transform.position.y / gridY),
//                 Mathf.RoundToInt(oldest.transform.position.z / gridXZ));
//             occupiedCells.Remove(oldCell);
//             Destroy(oldest);
//         }
//     }

//     /* ────── Feedback helpers ────── */
//     private void FeedbackSuccess()
//     {
//         SendHaptic(0.2f, 0.05f, rightXRController);
//         SendHaptic(0.2f, 0.05f, leftXRController);
//         PlayClip(placeClip);
//     }
//     private void FeedbackError()
//     {
//         SendHaptic(hapticAmplitude, hapticDuration, rightXRController);
//         SendHaptic(hapticAmplitude, hapticDuration, leftXRController);
//         PlayClip(errorClip);
//     }
//     private void SendHaptic(float amp, float dur, XRBaseController ctrl)
//     {
//         if (ctrl != null) ctrl.SendHapticImpulse(amp, dur);
//     }
//     private void PlayClip(AudioClip clip)
//     {
//         if (audioSource != null && clip != null) audioSource.PlayOneShot(clip);
//     }
// }



/*
 * AirWalkGenerator  —  v2.6  (edge‑triggered, custom grid XZ vs Y)
 *
 *  Left‑hand  X  →  0.30 m BELOW feet  (one platform per press)
 *  Right‑hand A  →  0.30 m ABOVE feet
 *  Right‑hand B  →  at feet
 */

using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class AirWalkGenerator : MonoBehaviour
{
    /* ────── References ────── */
    [Header("References")]
    [SerializeField] private GameObject  platformPrefab;
    [SerializeField] private Transform   rightHandTransform;
    [SerializeField] private Transform   leftHandTransform;
    [SerializeField] private CharacterController characterController;

    /* ────── Placement settings ────── */
    [Header("Placement")]
    [SerializeField] private float horizontalStep  = 0.5f;
    [SerializeField] private float verticalOffset  = 0.30f;
    [SerializeField] private float fineTuneYOffset = 0.0f;

    [Header("Grid Snap")]
    [SerializeField] private float gridXZ = 0.10f;   // X & Z
    [SerializeField] private float gridY  = 0.10f;   // Y

    /* ────── Collision / Pool ────── */
    [Header("Collision / Pooling")]
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private int       maxPlatforms = 60;

    /* ────── Feedback ────── */
    [Header("Feedback")]
    [SerializeField] private XRBaseController rightXRController;
    [SerializeField] private XRBaseController leftXRController;
    [SerializeField] private float   hapticAmplitude = 0.35f;
    [SerializeField] private float   hapticDuration  = 0.08f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip  errorClip;
    [SerializeField] private AudioClip  placeClip;

    /* ────── Runtime state ────── */
    private Vector3 prefabHalfExtents;
    private readonly HashSet<Vector3Int> occupiedCells = new();
    private readonly Queue<GameObject>   spawned       = new();

    // edge‑detection flags
    private bool prevRA, prevRB, prevLX;

    /* ────── Init ────── */
    private void Awake()
    {
        var col = platformPrefab.GetComponent<Collider>();
        prefabHalfExtents = col ? col.bounds.extents : Vector3.one * 0.25f;

        if (characterController == null)
            characterController = FindObjectOfType<CharacterController>();
    }

    /* ────── Input polling (edge‑trigger) ────── */
    private void Update()
    {
        InputDevice right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        InputDevice left  = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        if (!right.isValid && !left.isValid) return;

        bool rA, rB, lX;
        right.TryGetFeatureValue(CommonUsages.primaryButton,   out rA); // A
        right.TryGetFeatureValue(CommonUsages.secondaryButton, out rB); // B
        left .TryGetFeatureValue(CommonUsages.primaryButton,   out lX); // X

        // rising edges
        if ( rA && !prevRA) TryPlace(Level.Up);
        if ( rB && !prevRB) TryPlace(Level.Neutral);
        if ( lX && !prevLX) TryPlace(Level.Down);

        prevRA = rA;
        prevRB = rB;
        prevLX = lX;
    }

    /* ────── Placement ────── */
    private enum Level { Neutral, Up, Down }

    private void TryPlace(Level lvl)
    {
        /* 1. Horizontal anchor */
        Vector3 basePos = rightHandTransform.position +
                          rightHandTransform.forward.normalized * horizontalStep;

        /* 2. Foot plane */
        float footY = characterController != null
            ? characterController.transform.position.y
              + characterController.center.y
              - characterController.height * 0.5f
            : 0f;

        footY += fineTuneYOffset;

        /* 3. Vertical offset */
        float targetY = lvl switch
        {
            Level.Up   => footY + verticalOffset,
            Level.Down => footY - verticalOffset,
            _          => footY
        };

        Vector3 pos = new Vector3(basePos.x, targetY, basePos.z);

        /* 4. Grid snap (separate XZ vs Y) */
        Vector3Int cell = new Vector3Int(
            Mathf.RoundToInt(pos.x / gridXZ),
            Mathf.RoundToInt(pos.y / gridY),
            Mathf.RoundToInt(pos.z / gridXZ));

        if (occupiedCells.Contains(cell)) { FeedbackError(); return; }

        Vector3 snappedPos = new Vector3(
            cell.x * gridXZ,
            cell.y * gridY,
            cell.z * gridXZ);

        /* 5. Overlap test */
        if (Physics.OverlapBox(snappedPos, prefabHalfExtents, Quaternion.identity, platformLayer).Length > 0)
        { FeedbackError(); return; }

        /* 6. Spawn & freeze */
        GameObject plat = Instantiate(platformPrefab, snappedPos, Quaternion.identity);
        plat.layer = Mathf.RoundToInt(Mathf.Log(platformLayer.value, 2));

        if (plat.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
            rb.useGravity  = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        occupiedCells.Add(cell);
        spawned.Enqueue(plat);
        FeedbackSuccess();

        /* 7. Pool */
        if (spawned.Count > maxPlatforms)
        {
            GameObject oldest = spawned.Dequeue();
            Vector3Int oldCell = new Vector3Int(
                Mathf.RoundToInt(oldest.transform.position.x / gridXZ),
                Mathf.RoundToInt(oldest.transform.position.y / gridY),
                Mathf.RoundToInt(oldest.transform.position.z / gridXZ));
            occupiedCells.Remove(oldCell);
            Destroy(oldest);
        }
    }

    /* ────── Feedback ────── */
    private void FeedbackSuccess()
    {
        SendHaptic(0.2f, 0.05f, rightXRController);
        SendHaptic(0.2f, 0.05f, leftXRController);
        PlayClip(placeClip);
    }
    private void FeedbackError()
    {
        SendHaptic(hapticAmplitude, hapticDuration, rightXRController);
        SendHaptic(hapticAmplitude, hapticDuration, leftXRController);
        PlayClip(errorClip);
    }
    private void SendHaptic(float amp, float dur, XRBaseController ctrl)
    {
        if (ctrl != null) ctrl.SendHapticImpulse(amp, dur);
    }
    private void PlayClip(AudioClip clip)
    {
        if (audioSource != null && clip != null) audioSource.PlayOneShot(clip);
    }
}
