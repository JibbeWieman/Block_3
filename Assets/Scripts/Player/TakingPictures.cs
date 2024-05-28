using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakingPictures : MonoBehaviour
{
    public Rect captureRect = new Rect(0f, 0f, 365f, 648f);
    public RawImage screenshotRI;
    public List<RawImage> inventoryImages;  // List of RawImage components for the inventory
    public RenderTexture render;

    [SerializeField] Camera playerCam;
    private CheckVisible checkVisible;
    private GameObject[] objectsToCheck;

    private bool isAlreadyTakingPicture;
    private List<Texture2D> capturedPictures = new List<Texture2D>();  // List to store captured pictures

    private void Start()
    {
        checkVisible = playerCam.GetComponent<CheckVisible>();
        objectsToCheck = checkVisible.GetObjectsToCheck();

        // Ensure all inventory images are initially empty
        foreach (RawImage image in inventoryImages)
        {
            image.texture = null;
        }
    }

    void Update()
    {
        isAlreadyTakingPicture = false;

        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (var checkedObject in objectsToCheck)
            {
                if (checkVisible.ObjectInCameraFrustrum(checkedObject))
                {
                    if (!checkVisible.ObjectBlocked(checkedObject))
                    {
                        if (!isAlreadyTakingPicture)
                        {
                            TakePicture();
                            checkedObject.GetComponent<Collider>().enabled = false; // Disable collider to prevent multiple pictures
                        }
                    }
                }
            }
        }
    }
    private void TakePicture()
    {
        isAlreadyTakingPicture = true;

        // Capture the entire screen
        Texture2D screenTexture = ScreenCapture.CaptureScreenshotAsTexture();

        // Crop the screenshot to the desired area
        Texture2D croppedTexture = new Texture2D((int)captureRect.width, (int)captureRect.height, TextureFormat.ARGB32, false);
        Color[] pixels = screenTexture.GetPixels((int)captureRect.x, (int)captureRect.y, (int)captureRect.width, (int)captureRect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        render = new RenderTexture((int)captureRect.width, (int)captureRect.height, 0);
        render.enableRandomWrite = true;
        render.Create();

        // Copy the data from the Texture2D to the RenderTexture
        Graphics.Blit(croppedTexture, render);
        screenshotRI.texture = render;

        // Add the captured texture to the list of captured pictures
        capturedPictures.Add(croppedTexture);

        // Update the inventory display
        UpdateInventoryDisplay();
    }

    private void UpdateInventoryDisplay()
    {
        for (int i = 0; i < inventoryImages.Count; i++)
        {
            if (i < capturedPictures.Count)
            {
                inventoryImages[i].texture = capturedPictures[i];
            }
            else
            {
                inventoryImages[i].texture = null;
            }
        }
    }
}
