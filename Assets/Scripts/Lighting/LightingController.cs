using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class LightingController : MonoBehaviour
{
    public Material dayMat;
    public Material nightMat;
    public Material sunRiseMat;
    public Material sunSetMat;
    public Cubemap test;
    

    [SerializeField]
    private Light directionalLight;

    public LightPreset preset;

    [SerializeField, Range(0,24)]
    private float TimeOfDay;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(preset==null)
            return;

        if(Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);
        }

        else {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        if(directionalLight != null)
        {
            directionalLight.color = preset.directionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(timePercent*360f - 90f, 170f, 0));
        }

        if(timePercent > 6.0f /24f && timePercent <= 16.0f / 24f)
        {
            RenderSettings.skybox = dayMat;
        }
        else if(timePercent <= 6.0f /24f && timePercent > 5.0f /24f)
        {
            RenderSettings.skybox = sunRiseMat;
        }
        else if(timePercent > 16.0f / 24f && timePercent <= 17.0f / 24f)
        {
            RenderSettings.skybox = sunSetMat;
        }
        else {
            RenderSettings.skybox = nightMat;
        }

        RenderSettings.skybox.SetFloat("_Rotation", timePercent * 360f);
    }

    private void Onvalidate()
    {
        if (directionalLight != null)
            return;

        if(RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else{
            Light[] lights = GameObject.FindObjectsOfType<Light>();

            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }

        }
    }
}
