using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]

public class GraphicsProjController : MonoBehaviour
{
    [SerializeField] private GameObject[] obj;
    [SerializeField] private Material[] LUTMats;
    [SerializeField] private Text texText;
    [SerializeField] private Text LutText;
    private bool texture = true;
    private int colourCorrection = 0;
    [SerializeField] private Renderer rend;
    private Material m_renderMaterial;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) texture = !texture;
        if (Input.GetKeyDown(KeyCode.Alpha2)) colourCorrection++;
        if (colourCorrection > 2) colourCorrection = 0;
        m_renderMaterial = LUTMats[colourCorrection];

        if (LutText != null)
        {
            if (m_renderMaterial != null) LutText.text = "Current LUT: " + m_renderMaterial.name;
            else LutText.text = "Current LUT: NeutralLUT";
        }
        if (texText != null) texText.text = "Textures enabled = " + texture;
    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (m_renderMaterial != null) Graphics.Blit(source, destination, m_renderMaterial);
    }
}
