using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightLightning : MonoBehaviour
{
    public Light2D globalLight;
    public AnimationCurve intensityCurve;
    public Gradient colorGradient;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (globalLight == null || TimeManager.Instance == null)
            return;

        float t = TimeManager.Instance.GetTimePercent();
        globalLight.intensity = intensityCurve.Evaluate(t);
        globalLight.color = colorGradient.Evaluate(t);
    }
}
