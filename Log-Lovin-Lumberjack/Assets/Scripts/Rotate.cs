using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateX;
    public float rotateY;
    public float rotateZ;

    void Update()
    {
        transform.Rotate(rotateX * Time.deltaTime, rotateY * Time.deltaTime, rotateZ * Time.deltaTime, Space.Self);
    }
}
