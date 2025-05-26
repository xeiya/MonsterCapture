using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    Rigidbody rb;
    Vector2 input;

    bool isGrounded = false;

    [SerializeField] private float jumpSpeed = 30;
    [SerializeField] private float moveSpeed = 50;

    private float maxSpeed = 100f;

    //When gameobject is enabled for the first time
    private void Start()
    {
        Debug.Log("Start");
    }

    //When gameobject is first enabled
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (cam == null) 
        { 
            cam = Camera.main;
        }

        if (cam == null) 
        { 
            cam = FindAnyObjectByType<Camera>();
        }
    }

    //When the script and gameobject is enableed
    private void OnEnable()
    {
        Debug.Log("On Enabled");
    }

    void OnJump() 
    {
        if (!isGrounded) return; 
        rb.AddForce(Vector3.up * jumpSpeed);
    }

    void OnMove(InputValue value) 
    {
        input = value.Get<Vector2>();
        if (input.magnitude > 1) 
        { 
            input.Normalize();
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement3D = new Vector3(input.x, 0f, input.y);
        movement3D = cam.transform.TransformDirection(movement3D);
        movement3D.y = 0f;
        movement3D = movement3D.normalized * movement3D.magnitude;

        if (!Physics.Raycast(transform.position, movement3D)) 
        {
            rb.AddForce(movement3D * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        
        if (isGrounded) 
        {
            Vector3 goalMovement = movement3D * moveSpeed;
            Vector3 newVelocity = Vector3.Lerp(rb.linearVelocity, goalMovement, Time.deltaTime * 5f);
            rb.linearVelocity = newVelocity;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 floorNormal = collision.contacts[0].normal.normalized;
        if (Vector3.Dot(floorNormal, Vector3.up) > 0.75f) 
        { 
            isGrounded = true;
        
        }

    }

    private void OnCollisionExit(Collision collision) 
    {
        isGrounded = false;
    }
}
