using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class MatcapGenerator : MonoBehaviour
{
    public int Size = 512;

    public string folderPath = "Assets/MatcapTextures/";

    public string FileName = "Default";

    public Material TargetMaterial;

    [HideInInspector]
    public string TextureProperty;

    [HideInInspector]
    public int CurrentTexturePropertiesIdx;

    [HideInInspector]
    public Texture2D GeneratedTexture;

    private int texturePropertyID;

    private Camera cam;

    void OnEnable()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (TextureProperty != string.Empty)
        {
            GeneratedTexture = Capture();
            ApplyToMaterial(GeneratedTexture);
        }
    }

    public Texture2D Capture()
    {
        RenderTexture rt = new RenderTexture(Size, Size, 24);
        cam.targetTexture = rt;

        cam.Render();
        RenderTexture.active = rt;

        Texture2D texture = new Texture2D(Size, Size, TextureFormat.RGB24, false);
        Rect rect = new Rect(0, 0, Size, Size);
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        RenderTexture.active = null;

        cam.targetTexture = null;
        rt.Release();
        rt = null;

        return texture;
    }

    public void ApplyToMaterial(Texture2D texture)
    {
        if (TargetMaterial == null) return;
        texturePropertyID = Shader.PropertyToID(TextureProperty);
        TargetMaterial.SetTexture(texturePropertyID, texture);
    }

    public string SaveTexture(Texture2D texture)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        byte[] bytes = texture.EncodeToPNG();

        var filePath = folderPath + FileName + TextureProperty + "_" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        File.WriteAllBytes(filePath + ".png", bytes);

        return filePath + ".png";
    }
}
