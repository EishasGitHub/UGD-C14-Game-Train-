using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float speed = 10.0f;
    private float horizontalInput;
    private float verticalInput;
    private float rotationSpeed = 100.0f;
    private int range = 14;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime);
        transform.Rotate(Vector3.up * rotationSpeed  * horizontalInput * Time.deltaTime);

        if (transform.position.x < -range)
        {
            transform.position = new Vector3(-range, transform.position.y, transform.position.z);
        }
        if (transform.position.x > range)
        {
            transform.position = new Vector3(range, transform.position.y, transform.position.z);
        }
        if (transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        }
        if (transform.position.z > range)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, range);
        }
    }
}
