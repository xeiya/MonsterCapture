using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TrapGun : MonoBehaviour
{
    public float shootSpeed = 10f;
    public GameObject trapPrefab;
    public List<GameObject> traps;
    public Vector3 trapOffset;
    public Vector3 trapRotation;

    private Camera cam;

    private void Awake()
    {
        cam ??= Camera.main ?? GetComponent<Camera>() ?? FindFirstObjectByType<Camera>();
    }

    void OnAttack() 
    {
        Vector3 spawnPosition = transform.position + (cam.transform.forward * trapOffset.z);
        spawnPosition.y += trapOffset.y;
        spawnPosition += cam.transform.right * trapOffset.x;

        GameObject trap = Instantiate(trapPrefab, spawnPosition, Quaternion.LookRotation(cam.transform.forward, Vector3.up) * Quaternion.Euler(trapRotation));

        trap.GetComponent<Rigidbody>()?.AddForce(cam.transform.forward * shootSpeed);
        
        traps.Add(trap);
    }
}
