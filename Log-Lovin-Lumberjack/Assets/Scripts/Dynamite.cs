using UnityEngine;

public class Dynamite : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Fuse");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().DecreaseHeart(3);
            FindObjectOfType<AudioManager>().PlaySFX("Explode");
            FindObjectOfType<GameManager>().Explode();
        }
    }
}
