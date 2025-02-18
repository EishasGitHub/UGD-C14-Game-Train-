using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 cameraPos = (player.transform.position - transform.position).normalized;
        transform.Translate(cameraPos * speed * Time.deltaTime);
    }
}
