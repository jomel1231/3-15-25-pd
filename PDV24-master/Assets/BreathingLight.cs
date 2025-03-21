using UnityEngine;

public class BreathingLight : MonoBehaviour
{
    public float pulseSpeed = 2f;
    public float minEmission = 0.2f;
    public float maxEmission = 1f;

    private Material mat;
    private Color baseEmissionColor;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        mat = rend.material;

        // Enable emission on material
        mat.EnableKeyword("_EMISSION");
        baseEmissionColor = mat.GetColor("_EmissionColor");
    }

    void Update()
    {
        float emissionIntensity = Mathf.Lerp(minEmission, maxEmission, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
        mat.SetColor("_EmissionColor", baseEmissionColor * emissionIntensity);
    }
}
