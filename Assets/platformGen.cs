
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.XR; // Ensure this is included for XR Input

public class platformGen : MonoBehaviour
{
    public GameObject platformPrefab; // Assign in Inspector
    public Transform rightHandTransform; // Assign Right Controller Transform
    private List<GameObject> previewPlatforms = new List<GameObject>(); // Store preview objects
    private bool previewMode = false; // Toggle preview mode
    private GameObject selectedPreview; // The selected preview platform

    private bool buttonPressed = false; // Prevents rapid multiple presses
    private float pressCooldown = 0.2f; // Cooldown duration
    private float lastPressTime = 0f; // Stores last press time

    // **NEW SETTINGS**
    private float horizontalSpacing = 1.0f; // Distance in front of the user
    private float verticalSpacing = 0.75f;  // Distance between height levels

    void Update()
    {
        // Check if Input System is working
        if (Mouse.current == null && !UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.RightHand).isValid)
        {
            Debug.LogError("❌ Input System not detected! Check XR Settings.");
            return;
        }

        // ✅ Show Preview Platforms when A Button is Pressed
        if (IsAButtonPressed() && !previewMode && Time.time - lastPressTime > pressCooldown)
        {
            Debug.Log("✅ A Button Pressed - Showing Preview Platforms");
            ShowPreviewPlatforms();
            lastPressTime = Time.time; // Update last press time
        }

        // ✅ Continuously Update Selection based on Controller Direction
        if (previewMode)
        {
            UpdateSelectedPreview();
        }

        // ✅ Confirm Selection and Spawn Platform when Trigger is Pressed
        if (IsTriggerPressed() && previewMode && Time.time - lastPressTime > pressCooldown)
        {
            Debug.Log("✅ Trigger Pressed - Selecting Platform Position");
            ConfirmPlatformPlacement();
            lastPressTime = Time.time; // Update last press time
        }
    }

    // 🔹 Check if A Button is Pressed
    bool IsAButtonPressed()
    {
        UnityEngine.XR.InputDevice rightController = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        bool aButtonPressed;
        rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out aButtonPressed);

        return aButtonPressed;
    }

    // 🔹 Check if Trigger Button is Pressed
    bool IsTriggerPressed()
    {
        UnityEngine.XR.InputDevice rightController = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        bool triggerPressed;
        rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerPressed);

        return triggerPressed;
    }

    // 🔹 Show 3 Preview Platforms (Same Level, Higher, Lower)
    void ShowPreviewPlatforms()
    {
        ClearPreviewPlatforms(); // Remove previous previews

        Vector3 basePosition = rightHandTransform.position + (rightHandTransform.forward * horizontalSpacing);

        // **UPDATED: Reduced Vertical Distance**
        previewPlatforms.Add(CreatePreviewPlatform(basePosition)); // Same Level
        previewPlatforms.Add(CreatePreviewPlatform(basePosition + Vector3.up * verticalSpacing)); // Higher Level
        previewPlatforms.Add(CreatePreviewPlatform(basePosition - Vector3.up * verticalSpacing)); // Lower Level

        previewMode = true; // Enter preview mode
        selectedPreview = null; // Reset selection to allow pointing at a new platform
    }

    // 🔹 Continuously update which preview platform is being pointed at
    void UpdateSelectedPreview()
    {
        GameObject newSelection = GetPointedPreview(); // Get the preview that the controller is pointing at

        // If selection changes, update colors and store it as selected
        if (newSelection != selectedPreview)
        {
            selectedPreview = newSelection;
            foreach (GameObject preview in previewPlatforms)
            {
                if (preview == selectedPreview)
                {
                    preview.GetComponent<Renderer>().material.color = Color.green; // Highlight selected platform
                }
                else
                {
                    preview.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f); // Transparent
                }
            }
        }
    }

    // 🔹 Confirm Selection and Place Platform
    void ConfirmPlatformPlacement()
    {
        if (selectedPreview != null)
        {
            Vector3 spawnPosition = selectedPreview.transform.position;
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("✅ Platform Spawned at: " + spawnPosition);
        }
        else
        {
            Debug.Log("❌ No platform selected! Make sure you are pointing at a platform.");
        }

        ClearPreviewPlatforms(); // Remove previews after selection
        previewMode = false; // Exit preview mode
    }

    // 🔹 Create Transparent Preview Platforms
    GameObject CreatePreviewPlatform(Vector3 position)
    {
        GameObject preview = Instantiate(platformPrefab, position, Quaternion.identity);
        preview.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f); // Make it transparent
        preview.GetComponent<Collider>().enabled = true; // Enable colliders for raycasting
        return preview;
    }

    // 🔹 Get the Preview Platform that the Controller is Pointing At
    GameObject GetPointedPreview()
    {
        Ray ray = new Ray(rightHandTransform.position, rightHandTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            foreach (GameObject preview in previewPlatforms)
            {
                if (hit.collider.gameObject == preview)
                {
                    return preview; // Return the preview platform that the controller is pointing at
                }
            }
        }

        return null; // If nothing is pointed at, return null
    }

    // 🔹 Remove All Preview Platforms
    void ClearPreviewPlatforms()
    {
        foreach (var preview in previewPlatforms)
        {
            Destroy(preview);
        }
        previewPlatforms.Clear();
    }
}