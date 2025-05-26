using Unity.Mathematics;
using UnityEngine;

public class Stick : MonoBehaviour
{
    Transform testObject;

    void Start()
    {
        
    }

    void Update()
    {
        //for the controller
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        input *= math.rcp(math.length(input));
        testObject.position = input;
        Debug.Log(input + " mag: " + input.magnitude);
    }
}
