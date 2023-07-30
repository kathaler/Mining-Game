using UnityEngine;

public class TorchFlicker : MonoBehaviour
{
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.0f;
    public float flickerSpeed = 1.0f; // Slower flickerSpeed for a more gradual effect
    public float flickerAmount = 0.2f;
    public float flickerIntensityVariation = 0.3f; // Introduce variation in intensity changes

    private UnityEngine.Rendering.Universal.Light2D torchLight;
    private float randomOffset;
    private float initialIntensity;
    private float flickerTimer = 0f;

    void Start()
    {
        torchLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        randomOffset = Random.Range(0f, 100f);
        initialIntensity = torchLight.intensity;
    }

    void Update()
    {
        flickerTimer += Time.deltaTime * flickerSpeed;
        float flicker = Mathf.PerlinNoise(flickerTimer + randomOffset, 0f);
        float targetIntensity = Mathf.Lerp(minIntensity, maxIntensity, flicker);
        float intensityVariation = Random.Range(-flickerAmount * targetIntensity, flickerAmount * targetIntensity);
        float targetWithVariation = Mathf.Clamp(targetIntensity + intensityVariation, minIntensity, maxIntensity);
        float finalIntensity = Mathf.Lerp(torchLight.intensity, targetWithVariation, Time.deltaTime * 10f * flickerIntensityVariation);
        torchLight.intensity = finalIntensity;
    }

    public void ResetFlicker()
    {
        flickerTimer = 0f;
        torchLight.intensity = initialIntensity;
    }
}
