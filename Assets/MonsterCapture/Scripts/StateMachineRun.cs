using System.Collections;
using UnityEngine;

public class StateMachineRun : MonoBehaviour, ITrappable
{
    public enum State
    {
        Patrol,
        Run,
        Flee
    }
    public State state;

    [SerializeField]private Material material;

    public GameObject player;
    private Rigidbody rb;
    private Vector3 originalScale;

    private float timer = 1;
    public bool isBeingCaptured { get; set; } = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        NextState();
    }

    void NextState()
    {
        switch (state)
        {
            case State.Patrol:
                StartCoroutine(PatrolState());
                material.color = Color.green;
                break;
            case State.Run:
                StartCoroutine(RunState());
                material.color = Color.yellow;
                break;
            case State.Flee:
                StartCoroutine(FleeState());
                material.color = Color.red;
                break;
            default:
                break;
        }
    }

    bool isFacingPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer.Normalize();

        float dotResult = Vector3.Dot(directionToPlayer, transform.forward);

        return dotResult >= 0.95f;
    }

    IEnumerator PatrolState() 
    {
        while (state == State.Patrol) 
        {
            transform.rotation = transform.rotation *= Quaternion.Euler(0f, 50f * Time.deltaTime, 0f);
            if (!isFacingPlayer())
                transform.rotation = transform.rotation *= Quaternion.Euler(0f, 50f * Time.deltaTime, 0f);

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= 10)
            { state = State.Run; }

            if (distanceToPlayer <= 5)
            {
                state = State.Flee;
            }

            yield return null;
        }
        NextState();
    }

    IEnumerator RunState() 
    {
        Debug.Log("Entering Run State");
        while (state == State.Run) 
        {
            float wave = Mathf.Sin(Time.time * 20f) * 0.1f + 1f;
            float wave2 = Mathf.Cos(Time.time * 20f) * 0.1f + 1f;
            transform.localScale = new Vector3(wave, wave2, wave);

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            Vector3 direction = player.transform.position - transform.position;
            rb.AddForce(direction.normalized * (-1000f * Time.deltaTime));

            if (distanceToPlayer >= 15)
            {
                state = State.Patrol;
            }

            if (distanceToPlayer <= 5)
            {
                state = State.Flee;
            }

            yield return null;
        }
        NextState();
    }
    IEnumerator FleeState()
    {
        transform.localScale = new Vector3(transform.localScale.x * 0.4f,
                                            transform.localScale.y * 0.4f,
                                            transform.localScale.z * 3);

        Vector3 direction = player.transform.position - transform.position;
        rb.AddForce(direction.normalized * -1000f);

        while (state == State.Flee)
        {
            yield return new WaitForSeconds(2f);
            state = State.Patrol;
        }

        transform.localScale = originalScale;
        Debug.Log("Exiting Attack State");
        NextState();
    }


    public int PointValue()
    {
        return 4;
    }

    public bool CaptureAnimation()
    {
        isBeingCaptured = true;
        timer -= Time.deltaTime * 1f;
        transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, timer);

        if (timer <= 0)
            return false;

        return true;
    }
}
