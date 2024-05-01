using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class LCD : MonoBehaviour
{
    public Shader shader;
    private Material _material;

    [Range(0, 1)] public float redCoef = 0.6f;
    [Range(0, 1)] public float greenCoef = 0.8f;
    [Range(0, 1)] public float blueCoef = 0.2f;

    [Range(-3, 20)] public float contrast = -2.0f;
    [Range(-200, 200)] public float brightness = 0.0f;


    protected Material material
    {
        get
        {
            if (_material == null)
            {
                _material = new Material(shader);
                _material.hideFlags = HideFlags.HideAndDontSave;
            }
            return _material;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (shader == null) return;
        Material mat = material;

        mat.SetFloat("_RedCoef", redCoef);
        mat.SetFloat("_GreenCoef", greenCoef);
        mat.SetFloat("_BlueCoef", blueCoef);

        mat.SetFloat("_Contrast", contrast);
        mat.SetFloat("_Brightness", brightness);

        mat.SetInt("_ScaleFactor", GetScaleFactor());

        Graphics.Blit(source, destination, mat);
    }

    void OnDisable()
    {
        if (_material)
        {
            DestroyImmediate(_material);
        }
    }

    private int GetScaleFactor()
    {
        int ret;
        if (((decimal)Screen.height / (decimal)Screen.width) > ((decimal)180 / (decimal)320))
        {
            ret = Screen.width / 320;
        }
        else
        {
            ret = Screen.height / 180;
        }
        return ret;
    }
}