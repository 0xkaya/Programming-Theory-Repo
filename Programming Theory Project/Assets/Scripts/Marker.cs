using UnityEngine;

public class Marker : MonoBehaviour
{
    public float verticalAmplitude = 0.5f; // Vertical movement range
    public float verticalSpeed = 2f; // Vertical movement speed
    public float rotationSpeed = 50f; // Rotation speed

    [SerializeField]
    private float baseHeight =1.5f;

    private void Update()
    {
        // Apply up and down movement
        float offsetY = Mathf.Sin(Time.time * verticalSpeed) * verticalAmplitude+baseHeight;
        transform.localPosition =new Vector3(0, offsetY, 0);

        // Apply rotation
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
