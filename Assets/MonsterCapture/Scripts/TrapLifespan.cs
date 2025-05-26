using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class TrapLifespan : MonoBehaviour
{
    MeshRenderer[] renderers;
    Light[] lights;

    void Start()
    {
        //GetComponent<MeshRenderer>();
        renderers = GetComponentsInChildren<MeshRenderer>();
        lights = GetComponentsInChildren<Light>();
        StartCoroutine(LifeSpan());

    }

    IEnumerator LifeSpan() 
    {
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        Color currentColor = renderers[0].material.color;
        yield return new WaitForSeconds(1f);

        //alph from 1 to 0
        float startTime = Time.time;
        float endTime = startTime + 3f;
        float lightStart = lights[0].intensity;
        while (Time.time < endTime) 
        {
            float t = 1 - Mathf.InverseLerp(startTime, endTime, Time.time);
            currentColor.a = t;
            propertyBlock.SetColor("_BaseColor", currentColor);

            foreach (var renderer in renderers)
            {
                renderer.SetPropertyBlock(propertyBlock);
            }

            foreach (Light light in lights)
            {
                light.intensity = lightStart * t;
            }

            yield return null;
        }
        Destroy(gameObject);
    }
}
