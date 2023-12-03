using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public GameObject whole;
    public GameObject split;

    private Rigidbody logRigidbody;
    private Collider logCollider;
    private ParticleSystem particleEffect;

    private void Awake()
    {
        logRigidbody = GetComponent<Rigidbody>();
        logCollider = GetComponent<Collider>();
        particleEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (gameObject.transform.position.y <= -20 && gameObject.CompareTag("Logs"))
        {
            FindObjectOfType<GameManager>().DecreaseHeart(1);
            Destroy(gameObject);
        }
    }

    private void Chop(Vector3 direction, Vector3 position, float force)
    {
        whole.SetActive(false);
        split.SetActive(true);

        logCollider.enabled = false;
        particleEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        split.transform.rotation = Quaternion.Euler(0f, 0f, angle * -0.5f);

        Rigidbody[] pieces = split.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody piece in pieces)
        {
            piece.velocity = logRigidbody.velocity;
            piece.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Axe axe = other.GetComponent<Axe>();
            Chop(axe.direction, axe.transform.position, axe.chopForce);
            FindObjectOfType<AudioManager>().PlaySFX("Chop");
        }
    }
}
