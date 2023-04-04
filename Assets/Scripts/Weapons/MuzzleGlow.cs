using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleGlow : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private MeshRenderer muzzle;
    private Color baseColor;
    [SerializeField] private Color glowColor;
    private Color currentColor;
    private float currIntensity = 0f;
    private float goalIntensity = 0f;
    private readonly float rate = 4f;
    private int numshots = 0;
    private void Awake()
    {
        baseColor = mat.GetColor("_Color");
        mat.SetColor("_BloomColor", baseColor * 0f);
    }
    void Update()
    {
        if (numshots > 0)
        {
            // sets the material of the muzzle object to the bloom material
            goalIntensity = numshots;
            currIntensity = Mathf.Lerp(currIntensity, goalIntensity, rate * Time.deltaTime);
            currentColor = Color.Lerp(currentColor, glowColor, rate * Time.deltaTime);
        }
        else
        {
            currIntensity = Mathf.Lerp(currIntensity, 0f, 1f * Time.deltaTime);
            currentColor = Color.Lerp(glowColor, baseColor, 2f * Time.deltaTime);
        }
        // updates the color and intensity of the bloom material accordingly
        mat.SetColor("_BloomColor", currentColor);
        mat.SetFloat("_Intensity", currIntensity);
    }
    public void SetNumShots(int s)
    {
        numshots = s;
    }
}
