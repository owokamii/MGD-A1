using UnityEngine;

public class Axe : MonoBehaviour
{
    private Camera mainCamera;
    private Collider axeCollider;
    private TrailRenderer axeTrail;
    private bool chopping;

    public Vector3 direction { get; private set; }
    public float chopForce = 5f;
    public float minChopVelocity = 0.01f;

    public int points = 1;

    private void Awake()
    {
        mainCamera = Camera.main;
        axeCollider = GetComponent<Collider>();
        axeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopChopping();
    }

    private void OnDisable()
    {
        StopChopping();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartChopping();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            StopChopping();
        }
        else if(chopping)
        {
            ContinueChopping();
        }
    }

    private void StartChopping()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        transform.position = newPosition;

        chopping = true;
        axeCollider.enabled = true;
        axeTrail.enabled = true;
        axeTrail.Clear();
    }

    private void StopChopping()
    {
        chopping = false;
        axeCollider.enabled = false;
        axeTrail.enabled = false;
    }

    private void ContinueChopping()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        axeCollider.enabled = velocity > minChopVelocity;

        transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Logs"))
        {
            other.gameObject.tag = "Split";
            FindObjectOfType<GameManager>().IncreaseScore(points);
        }
    }
}
