using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class CRT : MonoBehaviour
{
    public Shader shader;
    private Material _material;

    [Range(0, 1)] public float maxRedCoef = 1.0f;
    [Range(0, 1)] public float minRedCoef = 0.8f;
    [Range(0, 1)] public float maxGreenCoef = 1.0f;
    [Range(0, 1)] public float minGreenCoef = 0.8f;
    [Range(0, 1)] public float maxBlueCoef = 1.0f;
    [Range(0, 1)] public float minBlueCoef = 0.8f;

    [Range(0, 1)] public float scansCoef = 0.8f;
    [Range(0, 180)] public float scansCount = 180f;

    [Range(-3, 20)] public float contrast = 0.0f;
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

        mat.SetFloat("_MaxRedCoef", maxRedCoef);
        mat.SetFloat("_MinRedCoef", minRedCoef);
        mat.SetFloat("_MaxGreenCoef", maxGreenCoef);
        mat.SetFloat("_MinGreenCoef", minGreenCoef);
        mat.SetFloat("_MaxBlueCoef", maxBlueCoef);
        mat.SetFloat("_MinBlueCoef", minBlueCoef);

        mat.SetFloat("_ScansCoef", scansCoef);
        mat.SetFloat("_ScansCount", scansCount);

        mat.SetFloat("_Contrast", contrast);
        mat.SetFloat("_Brightness", brightness);

        Graphics.Blit(source, destination, mat);
    }

    void OnDisable()
    {
        if (_material)
        {
            DestroyImmediate(_material);
        }
    }
}