// // // // // using UnityEngine;
// // // // // using UnityEngine.XR.Hands.Samples.GestureSample;  // Important for StaticHandGesture reference

// // // // // public class ShakaSpawner : MonoBehaviour
// // // // // {
// // // // //     [Tooltip("Assign the StaticHandGesture component for the LEFT Shaka gesture.")]
// // // // //     public StaticHandGesture leftShakaGesture;

// // // // //     [Tooltip("Prefab to spawn when the Left Shaka gesture is detected.")]
// // // // //     public GameObject objectPrefab;

// // // // //     [Tooltip("Optional: If assigned, the spawned object will appear at this transform's position. " +
// // // // //              "If left empty, it will spawn at the LeftShakaGesture's transform position.")]
// // // // //     public Transform spawnPoint;

// // // // //     [Tooltip("Cooldown in seconds between spawns (to avoid spamming).")]
// // // // //     public float spawnCooldown = 2f;

// // // // //     private float _lastSpawnTime = -100f;

// // // // //     private void OnEnable()
// // // // //     {
// // // // //         if (leftShakaGesture != null)
// // // // //         {
// // // // //             // Subscribe to the gesturePerformed event
// // // // //             leftShakaGesture.gesturePerformed.AddListener(OnLeftShakaPerformed);
// // // // //             Debug.Log("ShakaSpawner: Subscribed to Left Shaka gesturePerformed event.");
// // // // //         }
// // // // //         else
// // // // //         {
// // // // //             Debug.LogError("ShakaSpawner: 'leftShakaGesture' is not assigned in the Inspector!");
// // // // //         }
// // // // //     }

// // // // //     private void OnDisable()
// // // // //     {
// // // // //         if (leftShakaGesture != null)
// // // // //         {
// // // // //             // Unsubscribe when disabled to avoid errors
// // // // //             leftShakaGesture.gesturePerformed.RemoveListener(OnLeftShakaPerformed);
// // // // //         }
// // // // //     }

// // // // //     private void OnLeftShakaPerformed()
// // // // //     {
// // // // //         // Check cooldown to prevent spamming multiple spawns
// // // // //         if (Time.time - _lastSpawnTime < spawnCooldown)
// // // // //         {
// // // // //             Debug.Log("ShakaSpawner: Spawn on cooldown. Try again later.");
// // // // //             return;
// // // // //         }
// // // // //         _lastSpawnTime = Time.time;

// // // // //         // Determine the spawn position
// // // // //         Vector3 spawnPosition = (spawnPoint != null)
// // // // //             ? spawnPoint.position
// // // // //             : leftShakaGesture.transform.position; // If no spawnPoint is set, use the gesture's position

// // // // //         // Instantiate the prefab
// // // // //         if (objectPrefab != null)
// // // // //         {
// // // // //             GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
// // // // //             Debug.Log($"ShakaSpawner: Spawned '{spawnedObject.name}' at position {spawnPosition}");
// // // // //         }
// // // // //         else
// // // // //         {
// // // // //             Debug.LogError("ShakaSpawner: No objectPrefab assigned to spawn!");
// // // // //         }
// // // // //     }
// // // // // }





// // // // // default distance generation

// // // // using UnityEngine;
// // // // using UnityEngine.XR.Hands.Samples.GestureSample;  // For StaticHandGesture

// // // // public class ShakaSpawner : MonoBehaviour
// // // // {
// // // //     [Tooltip("Assign the StaticHandGesture component for the Left Shaka gesture.")]
// // // //     public StaticHandGesture leftShakaGesture;

// // // //     [Tooltip("Platform prefab to spawn when the Shaka gesture is performed.")]
// // // //     public GameObject platformPrefab;

// // // //     [Tooltip("Maximum distance for the raycast to detect a surface.")]
// // // //     public float maxSpawnDistance = 10f;

// // // //     [Tooltip("Default distance in front of the camera to spawn the platform if no surface is hit.")]
// // // //     public float defaultSpawnDistance = 5f;

// // // //     [Tooltip("Cooldown time (in seconds) between spawns.")]
// // // //     public float spawnCooldown = 2f;

// // // //     private float _lastSpawnTime = -100f;
// // // //     private Camera mainCamera;

// // // //     void Awake()
// // // //     {
// // // //         // Use the main camera as the source for our gaze ray
// // // //         mainCamera = Camera.main;
// // // //         if (mainCamera == null)
// // // //         {
// // // //             Debug.LogError("GazePlatformSpawner: Main Camera not found!");
// // // //         }
// // // //     }

// // // //     void OnEnable()
// // // //     {
// // // //         if (leftShakaGesture != null)
// // // //         {
// // // //             leftShakaGesture.gesturePerformed.AddListener(OnShakaPoseDetected);
// // // //             Debug.Log("GazePlatformSpawner: Subscribed to Left Shaka gesture event.");
// // // //         }
// // // //         else
// // // //         {
// // // //             Debug.LogError("GazePlatformSpawner: Left Shaka gesture is not assigned!");
// // // //         }
// // // //     }

// // // //     void OnDisable()
// // // //     {
// // // //         if (leftShakaGesture != null)
// // // //         {
// // // //             leftShakaGesture.gesturePerformed.RemoveListener(OnShakaPoseDetected);
// // // //         }
// // // //     }

// // // //     void OnShakaPoseDetected()
// // // //     {
// // // //         // Check cooldown
// // // //         if (Time.time - _lastSpawnTime < spawnCooldown)
// // // //         {
// // // //             Debug.Log("GazePlatformSpawner: Spawn on cooldown. Try again later.");
// // // //             return;
// // // //         }
// // // //         _lastSpawnTime = Time.time;

// // // //         // Cast a ray from the main camera forward to simulate gaze interaction.
// // // //         Vector3 spawnPosition;
// // // //         Ray gazeRay = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
// // // //         RaycastHit hit;
// // // //         if (Physics.Raycast(gazeRay, out hit, maxSpawnDistance))
// // // //         {
// // // //             spawnPosition = hit.point;
// // // //             Debug.Log("GazePlatformSpawner: Raycast hit at " + spawnPosition);
// // // //         }
// // // //         else
// // // //         {
// // // //             // If nothing is hit, spawn at a default distance in front of the camera.
// // // //             spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * defaultSpawnDistance;
// // // //             Debug.Log("GazePlatformSpawner: No hit detected. Using default spawn position at " + spawnPosition);
// // // //         }

// // // //         // Instantiate the platform prefab at the determined position.
// // // //         if (platformPrefab != null)
// // // //         {
// // // //             GameObject spawnedPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
// // // //             Debug.Log("GazePlatformSpawner: Spawned platform '" + spawnedPlatform.name + "' at position " + spawnPosition);
// // // //         }
// // // //         else
// // // //         {
// // // //             Debug.LogError("GazePlatformSpawner: Platform prefab not assigned!");
// // // //         }
// // // //     }
// // // // }




// // // // Working with angles
// // // // using System.Collections.Generic;
// // // // using UnityEngine;
// // // // using UnityEngine.XR.Hands.Samples.GestureSample; // For StaticHandGesture

// // // // public class ShakaSpawner : MonoBehaviour
// // // // {
// // // //     [Header("Gesture Components")]
// // // //     [Tooltip("StaticHandGesture component for the Shaka gesture (to spawn preview platforms).")]
// // // //     public StaticHandGesture shakaGesture;
    
// // // //     // Removed point gesture; selection is now automatic based on HMD angle.
// // // //     [Tooltip("StaticHandGesture component for the FistBump gesture (to confirm and spawn the final platform).")]
// // // //     public StaticHandGesture fistBumpGesture;

// // // //     [Header("Prefabs")]
// // // //     [Tooltip("Prefab for the final platform that will be spawned.")]
// // // //     public GameObject finalPlatformPrefab;
    
// // // //     [Tooltip("Prefab for the preview platform (should be semi-transparent and have a Collider).")]
// // // //     public GameObject previewPlatformPrefab;

// // // //     [Header("Spawn Settings")]
// // // //     [Tooltip("Vertical offset between preview platforms.")]
// // // //     public float verticalOffset = 1.0f;
    
// // // //     [Tooltip("Cooldown in seconds between Shaka gesture spawns.")]
// // // //     public float spawnCooldown = 2f;

// // // //     [Header("Raycast Settings")]
// // // //     [Tooltip("Maximum distance for raycasting to determine the spawn point.")]
// // // //     public float maxSpawnDistance = 10f;
    
// // // //     [Tooltip("Default distance from the camera if the raycast does not hit any collider.")]
// // // //     public float defaultDistance = 5f;

// // // //     private float lastSpawnTime = -100f;
// // // //     private List<GameObject> previewPlatforms = new List<GameObject>();
// // // //     private bool previewMode = false;
// // // //     private GameObject selectedPreview = null;
// // // //     private Camera mainCamera;

// // // //     // Colors for preview: default (semi-transparent white) and selected (light green tint)
// // // //     private Color defaultColor = new Color(1f, 1f, 1f, 0.5f);
// // // //     private Color selectedColor = new Color(0.7f, 1f, 0.7f, 0.5f);

// // // //     void Awake()
// // // //     {
// // // //         mainCamera = Camera.main;
// // // //         if (mainCamera == null)
// // // //             Debug.LogError("GestureBasedPlatformSpawner: Main Camera not found!");
// // // //     }

// // // //     void OnEnable()
// // // //     {
// // // //         if (shakaGesture != null)
// // // //         {
// // // //             shakaGesture.gesturePerformed.AddListener(OnShakaGesture);
// // // //             Debug.Log("GestureBasedPlatformSpawner: Subscribed to Shaka gesture.");
// // // //         }
// // // //         else
// // // //             Debug.LogError("GestureBasedPlatformSpawner: Shaka gesture not assigned!");

// // // //         if (fistBumpGesture != null)
// // // //         {
// // // //             fistBumpGesture.gesturePerformed.AddListener(OnFistBumpGesture);
// // // //             Debug.Log("GestureBasedPlatformSpawner: Subscribed to FistBump gesture.");
// // // //         }
// // // //         else
// // // //             Debug.LogError("GestureBasedPlatformSpawner: FistBump gesture not assigned!");
// // // //     }

// // // //     void OnDisable()
// // // //     {
// // // //         if (shakaGesture != null)
// // // //             shakaGesture.gesturePerformed.RemoveListener(OnShakaGesture);
// // // //         if (fistBumpGesture != null)
// // // //             fistBumpGesture.gesturePerformed.RemoveListener(OnFistBumpGesture);
// // // //     }

// // // //     void Update()
// // // //     {
// // // //         // While in preview mode, automatically update the selected preview based on the HMD's pitch.
// // // //         if (previewMode && previewPlatforms.Count == 3)
// // // //         {
// // // //             UpdateAutoSelection();
// // // //         }
// // // //     }

// // // //     // Called when the Shaka gesture is performed.
// // // //     void OnShakaGesture()
// // // //     {
// // // //         if (Time.time - lastSpawnTime < spawnCooldown)
// // // //         {
// // // //             Debug.Log("GestureBasedPlatformSpawner: Spawn cooldown active.");
// // // //             return;
// // // //         }
// // // //         lastSpawnTime = Time.time;

// // // //         if (previewMode)
// // // //         {
// // // //             Debug.Log("GestureBasedPlatformSpawner: Preview mode already active.");
// // // //             return;
// // // //         }

// // // //         Vector3 basePosition = GetSpawnPosition();
// // // //         ClearPreviews();

// // // //         // Spawn three preview platforms in a vertical column.
// // // //         GameObject preview1 = Instantiate(previewPlatformPrefab, basePosition, Quaternion.identity);
// // // //         GameObject preview2 = Instantiate(previewPlatformPrefab, basePosition + Vector3.up * verticalOffset, Quaternion.identity);
// // // //         GameObject preview3 = Instantiate(previewPlatformPrefab, basePosition + Vector3.up * verticalOffset * 2f, Quaternion.identity);
        
// // // //         previewPlatforms.Add(preview1);
// // // //         previewPlatforms.Add(preview2);
// // // //         previewPlatforms.Add(preview3);

// // // //         // Set all preview platforms to default color.
// // // //         foreach (var preview in previewPlatforms)
// // // //         {
// // // //             SetPreviewColor(preview, defaultColor);
// // // //         }

// // // //         previewMode = true;
// // // //         selectedPreview = null;
// // // //         Debug.Log("GestureBasedPlatformSpawner: Preview platforms spawned at " + basePosition);
// // // //     }

// // // //     // Determines the spawn position using a raycast from the main camera.
// // // //     Vector3 GetSpawnPosition()
// // // //     {
// // // //         Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
// // // //         RaycastHit hit;
// // // //         if (Physics.Raycast(ray, out hit, maxSpawnDistance))
// // // //         {
// // // //             Debug.Log("GestureBasedPlatformSpawner: Raycast hit at " + hit.point);
// // // //             return hit.point;
// // // //         }
// // // //         else
// // // //         {
// // // //             Vector3 defaultPos = mainCamera.transform.position + mainCamera.transform.forward * defaultDistance;
// // // //             Debug.Log("GestureBasedPlatformSpawner: No hit detected, using default position " + defaultPos);
// // // //             return defaultPos;
// // // //         }
// // // //     }

// // // //     // Automatically selects one preview based on the HMD's (camera's) pitch.
// // // //     void UpdateAutoSelection()
// // // //     {
// // // //         // Get the y component of the camera's forward vector.
// // // //         float forwardY = mainCamera.transform.forward.y;
// // // //         GameObject autoSelected = null;

// // // //         // Decide selection based on thresholds:
// // // //         // If looking more downward (y < -0.33): select lowest preview.
// // // //         // If looking more upward (y > 0.33): select highest preview.
// // // //         // Otherwise, select the middle preview.
// // // //         if (forwardY < -0.33f)
// // // //         {
// // // //             autoSelected = previewPlatforms[0];
// // // //         }
// // // //         else if (forwardY > 0.33f)
// // // //         {
// // // //             autoSelected = previewPlatforms[2];
// // // //         }
// // // //         else
// // // //         {
// // // //             autoSelected = previewPlatforms[1];
// // // //         }

// // // //         // If selection changed, update the colors.
// // // //         if (selectedPreview != autoSelected)
// // // //         {
// // // //             selectedPreview = autoSelected;
// // // //             foreach (var preview in previewPlatforms)
// // // //             {
// // // //                 SetPreviewColor(preview, (preview == selectedPreview) ? selectedColor : defaultColor);
// // // //             }
// // // //             Debug.Log("AutoSelection updated: Selected preview " + selectedPreview.name);
// // // //         }
// // // //     }

// // // //     // Called when the FistBump gesture is performed.
// // // //     void OnFistBumpGesture()
// // // //     {
// // // //         if (!previewMode)
// // // //         {
// // // //             Debug.Log("GestureBasedPlatformSpawner: Not in preview mode; fist bump gesture ignored.");
// // // //             return;
// // // //         }

// // // //         if (selectedPreview != null)
// // // //         {
// // // //             Instantiate(finalPlatformPrefab, selectedPreview.transform.position, Quaternion.identity);
// // // //             Debug.Log("GestureBasedPlatformSpawner: Final platform spawned at " + selectedPreview.transform.position);
// // // //         }
// // // //         else
// // // //         {
// // // //             Debug.Log("GestureBasedPlatformSpawner: No preview selected, cannot spawn final platform.");
// // // //         }

// // // //         ClearPreviews();
// // // //         previewMode = false;
// // // //     }

// // // //     // Sets the preview's material color.
// // // //     void SetPreviewColor(GameObject preview, Color color)
// // // //     {
// // // //         Renderer rend = preview.GetComponent<Renderer>();
// // // //         if (rend != null)
// // // //         {
// // // //             rend.material.color = color;
// // // //         }
// // // //     }

// // // //     // Clears all preview platforms.
// // // //     void ClearPreviews()
// // // //     {
// // // //         foreach (var preview in previewPlatforms)
// // // //         {
// // // //             Destroy(preview);
// // // //         }
// // // //         previewPlatforms.Clear();
// // // //     }
// // // // }





// // // using System.Collections.Generic;
// // // using UnityEngine;
// // // using UnityEngine.XR.Hands.Samples.GestureSample; // For StaticHandGesture

// // // public class ShakaSpawner : MonoBehaviour
// // // {
// // //     [Header("Gesture Components")]
// // //     [Tooltip("StaticHandGesture component for the Shaka gesture (to spawn preview platforms).")]
// // //     public StaticHandGesture shakaGesture;
    
// // //     // Point gesture removed: selection is automatic based on HMD angle.
// // //     [Tooltip("StaticHandGesture component for the FistBump gesture (to confirm and spawn the final platform).")]
// // //     public StaticHandGesture fistBumpGesture;

// // //     [Header("Prefabs")]
// // //     [Tooltip("Prefab for the final platform that will be spawned.")]
// // //     public GameObject finalPlatformPrefab;
    
// // //     [Tooltip("Prefab for the preview platform (should be semi-transparent and have a Collider).")]
// // //     public GameObject previewPlatformPrefab;

// // //     [Header("Spawn Settings")]
// // //     [Tooltip("Vertical offset between preview platforms (reduce for less gap).")]
// // //     public float verticalOffset = 0.5f;
    
// // //     [Tooltip("Cooldown in seconds between Shaka gesture spawns.")]
// // //     public float spawnCooldown = 2f;

// // //     [Header("Raycast Settings")]
// // //     [Tooltip("Maximum distance for raycasting to determine the spawn point.")]
// // //     public float maxSpawnDistance = 10f;
    
// // //     [Tooltip("Default distance from the camera if the raycast does not hit any collider.")]
// // //     public float defaultDistance = 5f;

// // //     private float lastSpawnTime = -100f;
// // //     private List<GameObject> previewPlatforms = new List<GameObject>();
// // //     private bool previewMode = false;
// // //     private GameObject selectedPreview = null;
// // //     private Camera mainCamera;

// // //     // Colors for preview: default (semi-transparent white) and selected (light green tint)
// // //     private Color defaultColor = new Color(1f, 1f, 1f, 0.5f);
// // //     private Color selectedColor = new Color(0.7f, 1f, 0.7f, 0.5f);

// // //     void Awake()
// // //     {
// // //         mainCamera = Camera.main;
// // //         if (mainCamera == null)
// // //             Debug.LogError("GestureBasedPlatformSpawner: Main Camera not found!");
// // //     }

// // //     void OnEnable()
// // //     {
// // //         if (shakaGesture != null)
// // //         {
// // //             shakaGesture.gesturePerformed.AddListener(OnShakaGesture);
// // //             Debug.Log("GestureBasedPlatformSpawner: Subscribed to Shaka gesture.");
// // //         }
// // //         else
// // //             Debug.LogError("GestureBasedPlatformSpawner: Shaka gesture not assigned!");

// // //         if (fistBumpGesture != null)
// // //         {
// // //             fistBumpGesture.gesturePerformed.AddListener(OnFistBumpGesture);
// // //             Debug.Log("GestureBasedPlatformSpawner: Subscribed to FistBump gesture.");
// // //         }
// // //         else
// // //             Debug.LogError("GestureBasedPlatformSpawner: FistBump gesture not assigned!");
// // //     }

// // //     void OnDisable()
// // //     {
// // //         if (shakaGesture != null)
// // //             shakaGesture.gesturePerformed.RemoveListener(OnShakaGesture);
// // //         if (fistBumpGesture != null)
// // //             fistBumpGesture.gesturePerformed.RemoveListener(OnFistBumpGesture);
// // //     }

// // //     void Update()
// // //     {
// // //         // While in preview mode, automatically update the selected preview based on the HMD's pitch.
// // //         if (previewMode && previewPlatforms.Count == 3)
// // //         {
// // //             UpdateAutoSelection();
// // //         }
// // //     }

// // //     // Called when the Shaka gesture is performed.
// // //     void OnShakaGesture()
// // //     {
// // //         if (Time.time - lastSpawnTime < spawnCooldown)
// // //         {
// // //             Debug.Log("GestureBasedPlatformSpawner: Spawn cooldown active.");
// // //             return;
// // //         }
// // //         lastSpawnTime = Time.time;

// // //         if (previewMode)
// // //         {
// // //             Debug.Log("GestureBasedPlatformSpawner: Preview mode already active.");
// // //             return;
// // //         }

// // //         Vector3 basePosition = GetSpawnPosition();
// // //         ClearPreviews();

// // //         // Spawn three preview platforms in a vertical column with reduced gap.
// // //         GameObject preview1 = Instantiate(previewPlatformPrefab, basePosition, Quaternion.identity);
// // //         GameObject preview2 = Instantiate(previewPlatformPrefab, basePosition + Vector3.up * verticalOffset, Quaternion.identity);
// // //         GameObject preview3 = Instantiate(previewPlatformPrefab, basePosition + Vector3.up * verticalOffset * 2f, Quaternion.identity);
        
// // //         previewPlatforms.Add(preview1);
// // //         previewPlatforms.Add(preview2);
// // //         previewPlatforms.Add(preview3);

// // //         // Set all preview platforms to the default color.
// // //         foreach (var preview in previewPlatforms)
// // //         {
// // //             SetPreviewColor(preview, defaultColor);
// // //         }

// // //         previewMode = true;
// // //         selectedPreview = null;
// // //         Debug.Log("GestureBasedPlatformSpawner: Preview platforms spawned at " + basePosition);
// // //     }

// // //     // Determines the spawn position using a raycast from the main camera.
// // //     Vector3 GetSpawnPosition()
// // //     {
// // //         Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
// // //         RaycastHit hit;
// // //         if (Physics.Raycast(ray, out hit, maxSpawnDistance))
// // //         {
// // //             Debug.Log("GestureBasedPlatformSpawner: Raycast hit at " + hit.point);
// // //             return hit.point;
// // //         }
// // //         else
// // //         {
// // //             Vector3 defaultPos = mainCamera.transform.position + mainCamera.transform.forward * defaultDistance;
// // //             Debug.Log("GestureBasedPlatformSpawner: No hit detected, using default position " + defaultPos);
// // //             return defaultPos;
// // //         }
// // //     }

// // //     // Automatically selects one preview based on the HMD's (camera's) pitch.
// // //     void UpdateAutoSelection()
// // //     {
// // //         // Get the y component of the camera's forward vector.
// // //         float forwardY = mainCamera.transform.forward.y;
// // //         GameObject autoSelected = null;

// // //         // Adjusted thresholds:
// // //         // If looking downward (y < -0.2): select bottom preview.
// // //         // If looking upward (y > 0.2): select top preview.
// // //         // Otherwise: select middle preview.
// // //         if (forwardY < -0.2f)
// // //         {
// // //             autoSelected = previewPlatforms[0];
// // //         }
// // //         else if (forwardY > 0.2f)
// // //         {
// // //             autoSelected = previewPlatforms[2];
// // //         }
// // //         else
// // //         {
// // //             autoSelected = previewPlatforms[1];
// // //         }

// // //         // If selection changed, update the colors.
// // //         if (selectedPreview != autoSelected)
// // //         {
// // //             selectedPreview = autoSelected;
// // //             foreach (var preview in previewPlatforms)
// // //             {
// // //                 SetPreviewColor(preview, (preview == selectedPreview) ? selectedColor : defaultColor);
// // //             }
// // //             Debug.Log("AutoSelection updated: Selected preview " + selectedPreview.name);
// // //         }
// // //     }

// // //     // Called when the FistBump gesture is performed.
// // //     void OnFistBumpGesture()
// // //     {
// // //         if (!previewMode)
// // //         {
// // //             Debug.Log("GestureBasedPlatformSpawner: Not in preview mode; fist bump gesture ignored.");
// // //             return;
// // //         }

// // //         if (selectedPreview != null)
// // //         {
// // //             Instantiate(finalPlatformPrefab, selectedPreview.transform.position, Quaternion.identity);
// // //             Debug.Log("GestureBasedPlatformSpawner: Final platform spawned at " + selectedPreview.transform.position);
// // //         }
// // //         else
// // //         {
// // //             Debug.Log("GestureBasedPlatformSpawner: No preview selected, cannot spawn final platform.");
// // //         }

// // //         ClearPreviews();
// // //         previewMode = false;
// // //     }

// // //     // Sets the preview's material color.
// // //     void SetPreviewColor(GameObject preview, Color color)
// // //     {
// // //         Renderer rend = preview.GetComponent<Renderer>();
// // //         if (rend != null)
// // //         {
// // //             rend.material.color = color;
// // //         }
// // //     }

// // //     // Clears all preview platforms.
// // //     void ClearPreviews()
// // //     {
// // //         foreach (var preview in previewPlatforms)
// // //         {
// // //             Destroy(preview);
// // //         }
// // //         previewPlatforms.Clear();
// // //     }
// // // }



// // using System.Collections.Generic;
// // using UnityEngine;
// // using UnityEngine.XR;
// // using UnityEngine.XR.Interaction.Toolkit;
// // using UnityEngine.XR.Hands.Samples.GestureSample;

// // /// <summary>
// // /// Single-gesture platform spawner: 
// // /// - On Shaka gesture start: spawn 3 preview platforms.
// // /// - While holding Shaka: tilt HMD to select one (green).
// // /// - On Shaka gesture end: spawn final platform at selected preview.
// // /// </summary>
// // public class ShakaSpawner : MonoBehaviour
// // {
// //     [Header("Gesture Components")]
// //     [Tooltip("StaticHandGesture component for the Shaka gesture.")]
// //     public StaticHandGesture shakaGesture;

// //     [Header("Prefabs")]
// //     public GameObject finalPlatformPrefab;
// //     public GameObject previewPlatformPrefab;

// //     [Header("Spawn Settings")]
// //     [Tooltip("Vertical gap between preview platforms.")]
// //     public float previewGap = 0.3f;
// //     [Tooltip("Cooldown in seconds between Shaka gesture spawns.")]
// //     public float spawnCooldown = 2f;

// //     [Header("Raycast Settings")]
// //     [Tooltip("Max distance for raycast to place previews.")]
// //     public float maxSpawnDistance = 10f;
// //     [Tooltip("Default distance if no raycast hit.")]
// //     public float defaultDistance = 5f;

// //     [Header("Placement Settings")]
// //     [Tooltip("Horizontal offset forward from camera.")]
// //     public float horizontalOffset = 0.5f;
// //     public CharacterController characterController;
// //     [Tooltip("Vertical offset from foot-plane if needed.")]
// //     public float verticalOffset = 0.3f;
// //     [Tooltip("Grid snap on X & Z.")]
// //     public float gridXZ = 0.1f;
// //     [Tooltip("Grid snap on Y.")]
// //     public float gridY = 0.1f;
// //     public LayerMask platformLayer;
// //     public int maxPlatforms = 60;

// //     [Header("Angle Thresholds (camera pitch)")]
// //     [Tooltip("If camera pitch > this: top preview.")]
// //     public float upAngleThreshold = 0.2f;
// //     [Tooltip("If camera pitch < this: bottom preview.")]
// //     public float downAngleThreshold = -0.2f;

// //     [Header("Feedback")]
// //     public XRBaseController rightXRController;
// //     public XRBaseController leftXRController;
// //     public float hapticAmplitude = 0.35f;
// //     public float hapticDuration = 0.08f;
// //     public AudioSource audioSource;
// //     public AudioClip placeClip;
// //     public AudioClip errorClip;

// //     // Internal state
// //     private float lastSpawnTime = -999f;
// //     private List<GameObject> previewPlatforms = new List<GameObject>();
// //     private bool previewMode = false;
// //     private GameObject selectedPreview = null;
// //     private Camera mainCamera;

// //     // Pooling
// //     private Vector3 prefabHalfExtents;
// //     private HashSet<Vector3Int> occupiedCells = new HashSet<Vector3Int>();
// //     private Queue<GameObject> spawned = new Queue<GameObject>();

// //     void Awake()
// //     {
// //         mainCamera = Camera.main;
// //         if (characterController == null)
// //             characterController = FindObjectOfType<CharacterController>();

// //         // compute half-extents from final prefab
// //         var col = finalPlatformPrefab.GetComponent<Collider>();
// //         prefabHalfExtents = col ? col.bounds.extents : Vector3.one * 0.25f;
// //     }

// //     void OnEnable()
// //     {
// //         shakaGesture.gesturePerformed.AddListener(OnShakaStart);
// //         shakaGesture.gestureEnded.AddListener(OnShakaEnd);
// //     }

// //     void OnDisable()
// //     {
// //         shakaGesture.gesturePerformed.RemoveListener(OnShakaStart);
// //         shakaGesture.gestureEnded.RemoveListener(OnShakaEnd);
// //     }

// //     void Update()
// //     {
// //         if (previewMode)
// //             UpdateAutoSelection();
// //     }

// //     /// <summary>
// //     /// Called once when Shaka pose is recognized: spawn previews.
// //     /// </summary>
// //     void OnShakaStart()
// //     {
// //         if (Time.time - lastSpawnTime < spawnCooldown) return;
// //         lastSpawnTime = Time.time;
// //         if (previewMode) return;

// //         previewMode = true;
// //         ClearPreviews();

// //         Vector3 basePos = GetSpawnPosition();
// //         // spawn 3 stacked previews
// //         for (int i = 0; i < 3; i++)
// //         {
// //             Vector3 pos = basePos + Vector3.up * previewGap * i;
// //             var p = Instantiate(previewPlatformPrefab, pos, Quaternion.identity);
// //             SetPreviewColor(p, new Color(1f, 1f, 1f, 0.5f));
// //             previewPlatforms.Add(p);
// //         }
// //         selectedPreview = null;
// //     }

// //     /// <summary>
// //     /// Called once when Shaka pose ends: place final platform.
// //     /// </summary>
// //     void OnShakaEnd()
// //     {
// //         if (!previewMode) return;

// //         // choose the selected preview or default middle
// //         var target = selectedPreview ?? previewPlatforms[1];
// //         PlacePlatformAt(target.transform.position);

// //         ClearPreviews();
// //         previewMode = false;
// //     }

// //     Vector3 GetSpawnPosition()
// //     {
// //         Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
// //         if (Physics.Raycast(ray, out RaycastHit hit, maxSpawnDistance))
// //             return hit.point;
// //         return mainCamera.transform.position + mainCamera.transform.forward * defaultDistance;
// //     }

// //     void UpdateAutoSelection()
// //     {
// //         float pitchY = mainCamera.transform.forward.y;
// //         GameObject newSel = null;
// //         if (pitchY < downAngleThreshold)
// //             newSel = previewPlatforms[0];
// //         else if (pitchY > upAngleThreshold)
// //             newSel = previewPlatforms[2];
// //         else
// //             newSel = previewPlatforms[1];

// //         if (newSel != selectedPreview)
// //         {
// //             selectedPreview = newSel;
// //             foreach (var p in previewPlatforms)
// //                 SetPreviewColor(p, p == selectedPreview ? new Color(0.7f, 1f, 0.7f, 0.5f) : new Color(1f, 1f, 1f, 0.5f));
// //         }
// //     }

// //     void SetPreviewColor(GameObject preview, Color color)
// //     {
// //         if (preview.TryGetComponent<Renderer>(out var rend))
// //             rend.material.color = color;
// //     }

// //     void ClearPreviews()
// //     {
// //         foreach (var p in previewPlatforms)
// //             Destroy(p);
// //         previewPlatforms.Clear();
// //     }

// //     void PlacePlatformAt(Vector3 pos)
// //     {
// //         // grid-snap
// //         var cell = new Vector3Int(
// //             Mathf.RoundToInt(pos.x / gridXZ),
// //             Mathf.RoundToInt(pos.y / gridY),
// //             Mathf.RoundToInt(pos.z / gridXZ)
// //         );
// //         if (occupiedCells.Contains(cell)) { FeedbackError(); return; }
// //         var snapped = new Vector3(cell.x * gridXZ, cell.y * gridY, cell.z * gridXZ);

// //         // overlap test
// //         if (Physics.OverlapBox(snapped, prefabHalfExtents, Quaternion.identity, platformLayer).Length > 0)
// //         { FeedbackError(); return; }

// //         // instantiate & freeze
// //         var plat = Instantiate(finalPlatformPrefab, snapped, Quaternion.identity);
// //         if (plat.TryGetComponent<Rigidbody>(out var rb))
// //         {
// //             rb.isKinematic = true;
// //             rb.useGravity = false;
// //             rb.constraints = RigidbodyConstraints.FreezeAll;
// //         }

// //         occupiedCells.Add(cell);
// //         spawned.Enqueue(plat);
// //         FeedbackSuccess();

// //         // pool oldest beyond max
// //         if (spawned.Count > maxPlatforms)
// //         {
// //             var old = spawned.Dequeue();
// //             var oldCell = new Vector3Int(
// //                 Mathf.RoundToInt(old.transform.position.x / gridXZ),
// //                 Mathf.RoundToInt(old.transform.position.y / gridY),
// //                 Mathf.RoundToInt(old.transform.position.z / gridXZ)
// //             );
// //             occupiedCells.Remove(oldCell);
// //             Destroy(old);
// //         }
// //     }

// //     void FeedbackSuccess()
// //     {
// //         rightXRController?.SendHapticImpulse(0.2f, 0.05f);
// //         leftXRController?.SendHapticImpulse(0.2f, 0.05f);
// //         if (audioSource != null && placeClip != null)
// //             audioSource.PlayOneShot(placeClip);
// //     }

// //     void FeedbackError()
// //     {
// //         rightXRController?.SendHapticImpulse(hapticAmplitude, hapticDuration);
// //         leftXRController?.SendHapticImpulse(hapticAmplitude, hapticDuration);
// //         if (audioSource != null && errorClip != null)
// //             audioSource.PlayOneShot(errorClip);
// //     }
// // }




// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR;
// using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.XR.Hands.Samples.GestureSample;

// /// <summary>
// /// Single-gesture platform spawner: 
// /// - On Shaka gesture start: spawn 3 preview platforms.
// /// - While holding Shaka and moving your hand up/down: select one (green).
// /// - On Shaka gesture end: spawn final platform at selected preview.
// /// </summary>
// public class ShakaSpawner : MonoBehaviour
// {
//     [Header("Gesture Components")]
//     [Tooltip("StaticHandGesture component for the Shaka gesture.")]
//     public StaticHandGesture shakaGesture;

//     [Tooltip("Transform of the hand (attached to your gesture-tracker object). Used to read vertical movement.")]
//     public Transform handTransform;

//     [Header("Prefabs")]
//     public GameObject finalPlatformPrefab;
//     public GameObject previewPlatformPrefab;

//     [Header("Spawn Settings")]
//     public float previewGap        = 0.3f;
//     public float spawnCooldown     = 2f;

//     [Header("Raycast Settings")]
//     public float maxSpawnDistance  = 10f;
//     public float defaultDistance   = 5f;

//     [Header("Placement Settings")]
//     public float horizontalOffset  = 0.5f;
//     public CharacterController characterController;
//     public float verticalOffset    = 0.3f;
//     public float gridXZ            = 0.1f;
//     public float gridY             = 0.1f;
//     public LayerMask platformLayer;
//     public int       maxPlatforms   = 60;

//     [Header("Feedback")]
//     public XRBaseController rightXRController;
//     public XRBaseController leftXRController;
//     public float hapticAmplitude   = 0.35f;
//     public float hapticDuration    = 0.08f;
//     public AudioSource audioSource;
//     public AudioClip   placeClip;
//     public AudioClip   errorClip;

//     // Internal state
//     private float lastSpawnTime = -999f;
//     private List<GameObject> previewPlatforms = new List<GameObject>();
//     private bool previewMode = false;
//     private GameObject selectedPreview = null;
//     private Camera mainCamera;
//     private Vector3 previewBasePos;       // Y-base for selection

//     // Pooling
//     private Vector3 prefabHalfExtents;
//     private HashSet<Vector3Int> occupiedCells = new HashSet<Vector3Int>();
//     private Queue<GameObject>   spawned       = new Queue<GameObject>();

//     void Awake()
//     {
//         mainCamera = Camera.main;
//         if (characterController == null)
//             characterController = FindObjectOfType<CharacterController>();

//         var col = finalPlatformPrefab.GetComponent<Collider>();
//         prefabHalfExtents = col ? col.bounds.extents : Vector3.one * 0.25f;
//     }

//     void OnEnable()
//     {
//         shakaGesture.gesturePerformed.AddListener(OnShakaStart);
//         shakaGesture.gestureEnded.AddListener(OnShakaEnd);
//     }

//     void OnDisable()
//     {
//         shakaGesture.gesturePerformed.RemoveListener(OnShakaStart);
//         shakaGesture.gestureEnded.RemoveListener(OnShakaEnd);
//     }

//     void Update()
//     {
//         if (previewMode)
//             UpdateAutoSelection();
//     }

//     /// <summary>
//     /// Called when Shaka gesture is recognized: spawn previews and record base position.
//     /// </summary>
//     void OnShakaStart()
//     {
//         if (Time.time - lastSpawnTime < spawnCooldown) return;
//         lastSpawnTime = Time.time;
//         if (previewMode) return;

//         Vector3 basePos = GetSpawnPosition();
//         previewBasePos = basePos;
//         previewMode = true;

//         // Clear old previews
//         foreach (var p in previewPlatforms) Destroy(p);
//         previewPlatforms.Clear();

//         // Spawn three preview platforms vertically
//         for (int i = 0; i < 3; i++)
//         {
//             Vector3 pos = basePos + Vector3.up * previewGap * i;
//             var p = Instantiate(previewPlatformPrefab, pos, Quaternion.identity);
//             SetPreviewColor(p, new Color(1f, 1f, 1f, 0.5f));
//             previewPlatforms.Add(p);
//         }
//         selectedPreview = null;
//     }

//     /// <summary>
//     /// Called when Shaka gesture ends: place final platform at selected preview.
//     /// </summary>
//     void OnShakaEnd()
//     {
//         if (!previewMode) return;
//         var target = selectedPreview ?? previewPlatforms[1];
//         PlacePlatformAt(target.transform.position);
//         ClearPreviews();
//         previewMode = false;
//     }

//     Vector3 GetSpawnPosition()
//     {
//         Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
//         if (Physics.Raycast(ray, out RaycastHit hit, maxSpawnDistance))
//             return hit.point;
//         return mainCamera.transform.position + mainCamera.transform.forward * defaultDistance;
//     }

//     /// <summary>
//     /// Updates which preview is selected by comparing the hand's Y to preview heights.
//     /// </summary>
//     void UpdateAutoSelection()
//     {
//         float handY = handTransform.position.y;
//         float lowerThresh = previewBasePos.y + previewGap * 0.5f;
//         float upperThresh = previewBasePos.y + previewGap * 1.5f;

//         GameObject newSel;
//         if (handY < lowerThresh)         newSel = previewPlatforms[0];
//         else if (handY > upperThresh)    newSel = previewPlatforms[2];
//         else                              newSel = previewPlatforms[1];

//         if (newSel != selectedPreview)
//         {
//             selectedPreview = newSel;
//             foreach (var p in previewPlatforms)
//                 SetPreviewColor(p, p == selectedPreview ? new Color(0.7f, 1f, 0.7f, 0.5f) : new Color(1f, 1f, 1f, 0.5f));
//         }
//     }

//     void SetPreviewColor(GameObject preview, Color color)
//     {
//         if (preview.TryGetComponent<Renderer>(out var rend))
//             rend.material.color = color;
//     }

//     void ClearPreviews()
//     {
//         foreach (var p in previewPlatforms) Destroy(p);
//         previewPlatforms.Clear();
//     }

//     void PlacePlatformAt(Vector3 pos)
//     {
//         var cell = new Vector3Int(
//             Mathf.RoundToInt(pos.x / gridXZ),
//             Mathf.RoundToInt(pos.y / gridY),
//             Mathf.RoundToInt(pos.z / gridXZ)
//         );
//         if (occupiedCells.Contains(cell)) { FeedbackError(); return; }

//         Vector3 snapped = new Vector3(cell.x * gridXZ, cell.y * gridY, cell.z * gridXZ);
//         if (Physics.OverlapBox(snapped, prefabHalfExtents, Quaternion.identity, platformLayer).Length > 0)
//         { FeedbackError(); return; }

//         var plat = Instantiate(finalPlatformPrefab, snapped, Quaternion.identity);
//         if (plat.TryGetComponent<Rigidbody>(out var rb))
//         {
//             rb.isKinematic = true;
//             rb.useGravity = false;
//             rb.constraints = RigidbodyConstraints.FreezeAll;
//         }

//         occupiedCells.Add(cell);
//         spawned.Enqueue(plat);
//         FeedbackSuccess();

//         if (spawned.Count > maxPlatforms)
//         {
//             var old = spawned.Dequeue();
//             var oldCell = new Vector3Int(
//                 Mathf.RoundToInt(old.transform.position.x / gridXZ),
//                 Mathf.RoundToInt(old.transform.position.y / gridY),
//                 Mathf.RoundToInt(old.transform.position.z / gridXZ)
//             );
//             occupiedCells.Remove(oldCell);
//             Destroy(old);
//         }
//     }

//     void FeedbackSuccess()
//     {
//         rightXRController?.SendHapticImpulse(0.2f, 0.05f);
//         leftXRController?.SendHapticImpulse(0.2f, 0.05f);
//         if (audioSource != null && placeClip != null)
//             audioSource.PlayOneShot(placeClip);
//     }

//     void FeedbackError()
//     {
//         rightXRController?.SendHapticImpulse(hapticAmplitude, hapticDuration);
//         leftXRController?.SendHapticImpulse(hapticAmplitude, hapticDuration);
//         if (audioSource != null && errorClip != null)
//             audioSource.PlayOneShot(errorClip);
//     }
// }

// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR;
// using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.XR.Hands.Samples.GestureSample;

// /// <summary>
// /// Three-gesture spawner:
// /// - On UpGesture (e.g. ThumbUp): spawn platform at Up level.
// /// - On NeutralGesture (e.g. Shaka): spawn at neutral level.
// /// - On DownGesture (e.g. FistBump): spawn at Down level.
// /// </summary>
// public class ShakaSpawner : MonoBehaviour
// {
//     [Header("Gesture Components")]
//     [Tooltip("Gesture to spawn Up-level platform.")]
//     public StaticHandGesture upGesture;

//     [Tooltip("Gesture to spawn Neutral-level platform.")]
//     public StaticHandGesture neutralGesture;

//     [Tooltip("Gesture to spawn Down-level platform.")]
//     public StaticHandGesture downGesture;

//     [Header("Platform Prefab")]
//     public GameObject finalPlatformPrefab;

//     [Header("Placement Settings")]
//     public float horizontalOffset = 0.5f;
//     public float verticalOffset   = 0.3f;
//     public float gridXZ           = 0.1f;
//     public float gridY            = 0.1f;
//     public LayerMask platformLayer;
//     public int       maxPlatforms   = 60;

//     [Header("Character Controller (foot-plane)")]
//     public CharacterController characterController;

//     [Header("Feedback & Haptics")]
//     public XRBaseController rightXRController;
//     public XRBaseController leftXRController;
//     public AudioSource audioSource;
//     public AudioClip   placeClip;
//     public AudioClip   errorClip;
//     public float hapticAmplitude = 0.35f;
//     public float hapticDuration  = 0.08f;

//     // Internal pooling and collision
//     private Vector3 prefabHalfExtents;
//     private HashSet<Vector3Int> occupiedCells = new HashSet<Vector3Int>();
//     private Queue<GameObject>   spawnedPlatforms = new Queue<GameObject>();

//     private void Awake()
//     {
//         if (finalPlatformPrefab.TryGetComponent<Collider>(out var col))
//             prefabHalfExtents = col.bounds.extents;
//         else
//             prefabHalfExtents = Vector3.one * 0.25f;

//         if (characterController == null)
//             characterController = FindObjectOfType<CharacterController>();
//     }

//     private void OnEnable()
//     {
//         upGesture.gesturePerformed.AddListener(() => SpawnAtLevel(Level.Up));
//         neutralGesture.gesturePerformed.AddListener(() => SpawnAtLevel(Level.Neutral));
//         downGesture.gesturePerformed.AddListener(() => SpawnAtLevel(Level.Down));
//     }

//     private void OnDisable()
//     {
//         upGesture.gesturePerformed.RemoveAllListeners();
//         neutralGesture.gesturePerformed.RemoveAllListeners();
//         downGesture.gesturePerformed.RemoveAllListeners();
//     }

//     private enum Level { Down, Neutral, Up }

//     private void SpawnAtLevel(Level level)
//     {
//         // Determine base XZ using camera forward
//         Camera cam = Camera.main;
//         Vector3 basePos = cam.transform.position + cam.transform.forward * horizontalOffset;

//         // Compute foot-plane Y
//         float footY = characterController != null
//             ? characterController.transform.position.y + characterController.center.y - characterController.height * 0.5f
//             : basePos.y;

//         // Select target Y
//         float targetY = level == Level.Up    ? footY + verticalOffset
//                        : level == Level.Down  ? footY - verticalOffset
//                                               : footY;
//         Vector3 rawPos = new Vector3(basePos.x, targetY, basePos.z);

//         // Place the platform
//         TryPlace(rawPos);
//     }

//     private void TryPlace(Vector3 rawPos)
//     {
//         Vector3Int cell = new Vector3Int(
//             Mathf.RoundToInt(rawPos.x / gridXZ),
//             Mathf.RoundToInt(rawPos.y / gridY),
//             Mathf.RoundToInt(rawPos.z / gridXZ)
//         );
//         if (occupiedCells.Contains(cell)) { PlayError(); return; }

//         Vector3 snapped = new Vector3(cell.x * gridXZ, cell.y * gridY, cell.z * gridXZ);
//         if (Physics.OverlapBox(snapped, prefabHalfExtents, Quaternion.identity, platformLayer).Length > 0)
//         { PlayError(); return; }

//         var plat = Instantiate(finalPlatformPrefab, snapped, Quaternion.identity);
//         if (plat.TryGetComponent<Rigidbody>(out var rb))
//         {
//             rb.isKinematic = true;
//             rb.useGravity  = false;
//             rb.constraints = RigidbodyConstraints.FreezeAll;
//         }

//         occupiedCells.Add(cell);
//         spawnedPlatforms.Enqueue(plat);
//         PlaySuccess();

//         if (spawnedPlatforms.Count > maxPlatforms)
//         {
//             var old = spawnedPlatforms.Dequeue();
//             var oldCell = new Vector3Int(
//                 Mathf.RoundToInt(old.transform.position.x / gridXZ),
//                 Mathf.RoundToInt(old.transform.position.y / gridY),
//                 Mathf.RoundToInt(old.transform.position.z / gridXZ)
//             );
//             occupiedCells.Remove(oldCell);
//             Destroy(old);
//         }
//     }

//     private void PlaySuccess()
//     {
//         rightXRController?.SendHapticImpulse(0.2f, 0.05f);
//         leftXRController?.SendHapticImpulse(0.2f, 0.05f);
//         if (audioSource != null && placeClip != null) audioSource.PlayOneShot(placeClip);
//     }

//     private void PlayError()
//     {
//         rightXRController?.SendHapticImpulse(hapticAmplitude, hapticDuration);
//         leftXRController?.SendHapticImpulse(hapticAmplitude, hapticDuration);
//         if (audioSource != null && errorClip != null) audioSource.PlayOneShot(errorClip);
//     }
// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR.Hands.Samples.GestureSample;

// /// <summary>
// /// Gesture-based continuous spawner with grid snapping:
// /// - upGesture spawns continuously at Up level while held
// /// - neutralGesture at Middle
// /// - downGesture at Down
// /// Spawns platforms in front of the user’s gaze direction (camera forward) at heights matching the controller version.
// /// </summary>
// public class ShakaSpawner : MonoBehaviour
// {
//     [Header("Gesture Components")]
//     public StaticHandGesture upGesture;
//     public StaticHandGesture neutralGesture;
//     public StaticHandGesture downGesture;

//     [Header("References & Settings")]
//     [SerializeField] private GameObject platformPrefab;
//     [SerializeField] private CharacterController characterController;

//     [Header("Placement Settings")]
//     [Tooltip("Distance forward from the camera/gaze to spawn platforms.")]
//     [SerializeField] private float horizontalStep = 0.5f;
//     [Tooltip("Vertical step height above/below foot-plane.")]
//     [SerializeField] private float verticalOffset = 0.30f;
//     [Tooltip("Fine tune additional Y offset on foot-plane.")]
//     [SerializeField] private float fineTuneYOffset = 0f;

//     [Header("Grid Snap")]
//     [SerializeField] private float gridXZ = 0.10f;
//     [SerializeField] private float gridY  = 0.10f;

//     [Header("Collision / Pooling")]
//     [SerializeField] private LayerMask platformLayer;
//     [SerializeField] private int maxPlatforms = 60;

//     [Header("Continuous Spawn")]
//     [Tooltip("Delay between consecutive spawns while gesture is held.")]
//     [SerializeField] private float spawnInterval = 0.3f;

//     [Header("Feedback & Haptics")]
//     [SerializeField] private UnityEngine.XR.Interaction.Toolkit.XRBaseController rightXRController;
//     [SerializeField] private UnityEngine.XR.Interaction.Toolkit.XRBaseController leftXRController;
//     [SerializeField] private AudioSource audioSource;
//     [SerializeField] private AudioClip   placeClip;
//     [SerializeField] private AudioClip   errorClip;
//     [SerializeField] private float hapticAmplitude = 0.35f;
//     [SerializeField] private float hapticDuration  = 0.08f;

//     // Internal state
//     private Vector3 prefabHalfExtents;
//     private readonly HashSet<Vector3Int> occupiedCells = new();
//     private readonly Queue<GameObject> spawnedPlatforms = new();
//     private Coroutine spawnRoutine;
//     private Camera mainCamera;

//     private enum Level { Down, Neutral, Up }

//     private void Awake()
//     {
//         mainCamera = Camera.main;
//         if (mainCamera == null)
//             Debug.LogError("GestureBasedPlatformSpawner: No MainCamera found in scene.");

//         if (characterController == null)
//             characterController = FindObjectOfType<CharacterController>();

//         var col = platformPrefab.GetComponent<Collider>();
//         prefabHalfExtents = col ? col.bounds.extents : Vector3.one * 0.25f;
//     }

//     private void OnEnable()
//     {
//         upGesture.gesturePerformed.AddListener(() => StartSpawn(Level.Up));
//         upGesture.gestureEnded.AddListener(StopSpawn);
//         neutralGesture.gesturePerformed.AddListener(() => StartSpawn(Level.Neutral));
//         neutralGesture.gestureEnded.AddListener(StopSpawn);
//         downGesture.gesturePerformed.AddListener(() => StartSpawn(Level.Down));
//         downGesture.gestureEnded.AddListener(StopSpawn);
//     }

//     private void OnDisable()
//     {
//         StopSpawn();
//         upGesture.gesturePerformed.RemoveAllListeners();
//         upGesture.gestureEnded.RemoveAllListeners();
//         neutralGesture.gesturePerformed.RemoveAllListeners();
//         neutralGesture.gestureEnded.RemoveAllListeners();
//         downGesture.gesturePerformed.RemoveAllListeners();
//         downGesture.gestureEnded.RemoveAllListeners();
//     }

//     private void StartSpawn(Level lvl)
//     {
//         StopSpawn();
//         spawnRoutine = StartCoroutine(SpawnRoutine(lvl));
//     }

//     private void StopSpawn()
//     {
//         if (spawnRoutine != null)
//         {
//             StopCoroutine(spawnRoutine);
//             spawnRoutine = null;
//         }
//     }

//     private IEnumerator SpawnRoutine(Level lvl)
//     {
//         while (true)
//         {
//             SpawnAtLevel(lvl);
//             yield return new WaitForSeconds(spawnInterval);
//         }
//     }

//     private void SpawnAtLevel(Level lvl)
//     {
//         // 1. Horizontal anchor: in front of camera (gaze)
//         Vector3 basePos = mainCamera.transform.position + mainCamera.transform.forward.normalized * horizontalStep;

//         // 2. Compute foot-plane Y like controller code
//         float footY = characterController != null
//             ? characterController.transform.position.y + characterController.center.y - characterController.height * 0.5f
//             : basePos.y;
//         footY += fineTuneYOffset;

//         // 3. Vertical offset for Up/Down/Neutral
//         float targetY = lvl switch
//         {
//             Level.Up      => footY + verticalOffset,
//             Level.Down    => footY - verticalOffset,
//             _             => footY
//         };
//         Vector3 rawPos = new Vector3(basePos.x, targetY, basePos.z);

//         // 4. Grid snap XZ & Y
//         Vector3Int cell = new Vector3Int(
//             Mathf.RoundToInt(rawPos.x / gridXZ),
//             Mathf.RoundToInt(rawPos.y / gridY),
//             Mathf.RoundToInt(rawPos.z / gridXZ)
//         );
//         if (occupiedCells.Contains(cell)) { PlayError(); return; }
//         Vector3 snapped = new Vector3(cell.x * gridXZ, cell.y * gridY, cell.z * gridXZ);

//         // 5. Overlap test
//         if (Physics.OverlapBox(snapped, prefabHalfExtents, Quaternion.identity, platformLayer).Length > 0)
//         { PlayError(); return; }

//         // 6. Instantiate & freeze
//         var plat = Instantiate(platformPrefab, snapped, Quaternion.identity);
//         if (plat.TryGetComponent<Rigidbody>(out var rb))
//         {
//             rb.isKinematic = true;
//             rb.useGravity  = false;
//             rb.constraints = RigidbodyConstraints.FreezeAll;
//         }

//         // 7. Record occupancy & feedback
//         occupiedCells.Add(cell);
//         spawnedPlatforms.Enqueue(plat);
//         PlaySuccess();

//         // 8. Pool oldest beyond maxPlatforms
//         if (spawnedPlatforms.Count > maxPlatforms)
//         {
//             var old = spawnedPlatforms.Dequeue();
//             var oldCell = new Vector3Int(
//                 Mathf.RoundToInt(old.transform.position.x / gridXZ),
//                 Mathf.RoundToInt(old.transform.position.y / gridY),
//                 Mathf.RoundToInt(old.transform.position.z / gridXZ)
//             );
//             occupiedCells.Remove(oldCell);
//             Destroy(old);
//         }
//     }

//     private void PlaySuccess()
//     {
//         rightXRController?.SendHapticImpulse(0.2f, 0.05f);
//         leftXRController?.SendHapticImpulse(0.2f, 0.05f);
//         if (audioSource != null && placeClip != null)
//             audioSource.PlayOneShot(placeClip);
//     }

//     private void PlayError()
//     {
//         rightXRController?.SendHapticImpulse(hapticAmplitude, hapticDuration);
//         leftXRController?.SendHapticImpulse(hapticAmplitude, hapticDuration);
//         if (audioSource != null && errorClip != null)
//             audioSource.PlayOneShot(errorClip);
//     }
// }




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.GestureSample;

/// <summary>
/// Gesture-based continuous spawner with grid snapping using gaze direction:
/// - upGesture spawns at a fixed height above the floor
/// - neutralGesture spawns at floor level
/// - downGesture spawns just below floor level
/// Platforms appear in front of where the user looks, with controller-like offsets.
/// </summary>
public class ShakaSpawner : MonoBehaviour
{
    [Header("Gesture Components")]
    public StaticHandGesture upGesture;
    public StaticHandGesture neutralGesture;
    public StaticHandGesture downGesture;

    [Header("Platform Prefab & Rig")]
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private CharacterController characterController;

    [Header("Placement Settings")]
    [Tooltip("Horizontal distance from camera to spawn.")]
    [SerializeField] private float horizontalStep = 0.5f;
    [Tooltip("Vertical offset above/below floor for up/down levels.")]
    [SerializeField] private float verticalOffset = 0.30f;
    [Tooltip("Additional fine-tune offset on floor Y.")]
    [SerializeField] private float fineTuneYOffset = 0f;

    [Header("Grid Snap Settings")]
    [SerializeField] private float gridXZ = 0.10f;
    [SerializeField] private float gridY  = 0.10f;

    [Header("Collision / Pool")]
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private int maxPlatforms = 60;

    [Header("Continuous Spawn")]
    [Tooltip("Delay between spawns while gesture held.")]
    [SerializeField] private float spawnInterval = 0.3f;

    [Header("Feedback & Haptics")]
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.XRBaseController rightXRController;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.XRBaseController leftXRController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip   placeClip;
    [SerializeField] private AudioClip   errorClip;
    [SerializeField] private float hapticAmplitude = 0.35f;
    [SerializeField] private float hapticDuration  = 0.08f;

    private enum Level { Down, Neutral, Up }

    // Internal pooling & state
    private Vector3 prefabHalfExtents;
    private readonly HashSet<Vector3Int> occupiedCells = new();
    private readonly Queue<GameObject> spawnedPlatforms = new();
    private Coroutine spawnRoutine;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
            Debug.LogError("GestureBasedPlatformSpawner: No MainCamera found.");

        if (characterController == null)
            characterController = FindObjectOfType<CharacterController>();

        var col = platformPrefab.GetComponent<Collider>();
        prefabHalfExtents = col ? col.bounds.extents : Vector3.one * 0.25f;
    }

    private void OnEnable()
    {
        upGesture.gesturePerformed.AddListener(() => StartSpawn(Level.Up));
        upGesture.gestureEnded.AddListener(StopSpawn);
        neutralGesture.gesturePerformed.AddListener(() => StartSpawn(Level.Neutral));
        neutralGesture.gestureEnded.AddListener(StopSpawn);
        downGesture.gesturePerformed.AddListener(() => StartSpawn(Level.Down));
        downGesture.gestureEnded.AddListener(StopSpawn);
    }

    private void OnDisable()
    {
        StopSpawn();
        upGesture.gesturePerformed.RemoveAllListeners();
        upGesture.gestureEnded.RemoveAllListeners();
        neutralGesture.gesturePerformed.RemoveAllListeners();
        neutralGesture.gestureEnded.RemoveAllListeners();
        downGesture.gesturePerformed.RemoveAllListeners();
        downGesture.gestureEnded.RemoveAllListeners();
    }

    private void StartSpawn(Level lvl)
    {
        StopSpawn();
        spawnRoutine = StartCoroutine(SpawnRoutine(lvl));
    }

    private void StopSpawn()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine(Level lvl)
    {
        while (true)
        {
            SpawnAtLevel(lvl);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnAtLevel(Level lvl)
    {
        // 1. Base anchor: in front of camera/gaze
        Vector3 basePos = mainCamera.transform.position + mainCamera.transform.forward.normalized * horizontalStep;

        // 2. Floor Y (foot-plane)
        float floorY = characterController != null
            ? characterController.transform.position.y + fineTuneYOffset
            : mainCamera.transform.position.y + fineTuneYOffset;

        // 3. Compute target Y by level
        float targetY = lvl switch
        {
            Level.Up      => floorY + verticalOffset,
            Level.Down    => floorY - verticalOffset,
            _             => floorY
        };
        Vector3 rawPos = new Vector3(basePos.x, targetY, basePos.z);

        // 4. Grid snap
        Vector3Int cell = new Vector3Int(
            Mathf.RoundToInt(rawPos.x / gridXZ),
            Mathf.RoundToInt(rawPos.y / gridY),
            Mathf.RoundToInt(rawPos.z / gridXZ)
        );
        if (occupiedCells.Contains(cell)) { PlayError(); return; }
        Vector3 snapped = new Vector3(cell.x * gridXZ, cell.y * gridY, cell.z * gridXZ);

        // 5. Overlap test
        if (Physics.OverlapBox(snapped, prefabHalfExtents, Quaternion.identity, platformLayer).Length > 0)
        { PlayError(); return; }

        // 6. Instantiate & freeze
        var plat = Instantiate(platformPrefab, snapped, Quaternion.identity);
        if (plat.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
            rb.useGravity  = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        // 7. Record & feedback
        occupiedCells.Add(cell);
        spawnedPlatforms.Enqueue(plat);
        PlaySuccess();

        // 8. Pool oldest
        if (spawnedPlatforms.Count > maxPlatforms)
        {
            var old = spawnedPlatforms.Dequeue();
            var oldCell = new Vector3Int(
                Mathf.RoundToInt(old.transform.position.x / gridXZ),
                Mathf.RoundToInt(old.transform.position.y / gridY),
                Mathf.RoundToInt(old.transform.position.z / gridXZ)
            );
            occupiedCells.Remove(oldCell);
            Destroy(old);
        }
    }

    private void PlaySuccess()
    {
        rightXRController?.SendHapticImpulse(0.2f, 0.05f);
        leftXRController?.SendHapticImpulse(0.2f, 0.05f);
        if (audioSource != null && placeClip != null)
            audioSource.PlayOneShot(placeClip);
    }

    private void PlayError()
    {
        rightXRController?.SendHapticImpulse(hapticAmplitude, hapticDuration);
        leftXRController?.SendHapticImpulse(hapticAmplitude, hapticDuration);
        if (audioSource != null && errorClip != null)
            audioSource.PlayOneShot(errorClip);
    }
}
