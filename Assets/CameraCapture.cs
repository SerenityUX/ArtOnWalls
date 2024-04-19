using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class CameraCapture : MonoBehaviour
{
    // You can remove these if they are not needed externally
    public Camera cameraToCapture;
    public RenderTexture renderTexture;

    public void TakePhoto(Action<string> onPhotoTaken)
    {
        CaptureView(onPhotoTaken);
    }

    void CaptureView(Action<string> onPhotoTaken)
    {
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = null;

        byte[] imageBytes = texture.EncodeToPNG();
        string base64Image = Convert.ToBase64String(imageBytes);
        string dataUrl = "data:image/png;base64," + base64Image;

        StartCoroutine(UploadImageToFreeImageHost(dataUrl, onPhotoTaken));

        Destroy(texture); // Clean up the texture to prevent memory leaks
    }

    IEnumerator UploadImageToFreeImageHost(string imageUrl, Action<string> onPhotoTaken)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("key", "6d207e02198a847aa98d0a2a901485a5");
        formData.AddField("action", "upload");
        formData.AddBinaryData("source", Convert.FromBase64String(imageUrl.Split(',')[1]), "image.png", "image/png");

        using (UnityWebRequest www = UnityWebRequest.Post("https://freeimage.host/api/1/upload", formData))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Image uploaded successfully.");
                string responseText = www.downloadHandler.text;
                FreeImageHostResponse response = JsonUtility.FromJson<FreeImageHostResponse>(responseText);
                if (response.status_code == 200 && response.image != null)
                {
                    onPhotoTaken?.Invoke(response.image.url); // Notify caller with the image URL
                }
                else
                {
                    Debug.LogError("Invalid response from FreeImageHost.");
                }
            }
            else
            {
                Debug.LogError("Image upload failed: " + www.error);
            }
        }
    }

    [Serializable]
    public class FreeImageHostResponse
    {
        public int status_code;
        public ImageData image;
    }

    [Serializable]
    public class ImageData
    {
        public string url;
    }
}
