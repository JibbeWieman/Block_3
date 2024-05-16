using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakingPictures : MonoBehaviour
{

    // The dimensions and position of the area you want to capture
    /*public Rect captureRect = new Rect(0f, 0f, 365f, 648f);

    public RawImage screenshotRI;
    public RenderTexture render;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
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
            Graphics.CopyTexture(croppedTexture, render);
            screenshotRI.texture = render;

            // Save the cropped screenshot
            //[] bytes = croppedTexture.EncodeToPNG();
            //System.IO.File.WriteAllBytes("screenshot.png", bytes);
        }
    }*/

    public Rect captureRect;

    public RawImage screenshotRI;
    public RenderTexture render;

    void Start()
    {
        // Adjusted captureRect based on canvas scaler reference resolution
        captureRect = new Rect(40f * (1920f / Screen.width), 0f, 500f * (1920f / Screen.width), 450f * (1080f / Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(CaptureScreenshot());
        }
    }

    IEnumerator CaptureScreenshot()
    {
        // Wait until the end of the frame
        yield return new WaitForEndOfFrame();

        // Capture the entire screen
        Texture2D screenTexture = ScreenCapture.CaptureScreenshotAsTexture();

        // Crop the screenshot to the desired area
        Texture2D croppedTexture = new Texture2D((int)captureRect.width, (int)captureRect.height, TextureFormat.RGB24, false);
        Color[] pixels = screenTexture.GetPixels((int)captureRect.x, (int)captureRect.y, (int)captureRect.width, (int)captureRect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        // Create a RenderTexture with the same dimensions
        render = new RenderTexture((int)captureRect.width, (int)captureRect.height, 0);
        render.enableRandomWrite = true;
        render.Create();

        // Copy the data from the Texture2D to the RenderTexture
        Graphics.Blit(croppedTexture, render);

        // Assign the RenderTexture to the RawImage
        screenshotRI.texture = render;

        // Save the cropped screenshot
        // byte[] bytes = croppedTexture.EncodeToPNG();
        // System.IO.File.WriteAllBytes("screenshot.png", bytes);
    }
}