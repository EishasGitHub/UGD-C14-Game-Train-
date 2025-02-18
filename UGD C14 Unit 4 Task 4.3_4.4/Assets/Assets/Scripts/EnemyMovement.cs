using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float speed = 2.0f;
    private GameObject player;
    public Animator animator;
    private bool isGoingBack = false;
    private float goBackDistance = 2.0f;  
    private float goBackDuration = 1.5f;  
    private float followDuration = 3.0f;
    private float timer = 0f;

    void Start()
    {
        player = GameObject.Find("Player");
        timer = followDuration;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        if (isGoingBack)
        {
            // Move backward (opposite of player direction)
            transform.Translate(-directionToPlayer * speed * Time.deltaTime, Space.World);
            transform.forward = -directionToPlayer;
        }
        else
        {
            // Move towards the player
            transform.Translate(directionToPlayer * speed * Time.deltaTime, Space.World);
            transform.forward = directionToPlayer;
        }


        timer -= Time.deltaTime;

        if (isGoingBack && timer <= 0.0f)
        {
            timer = followDuration;
            isGoingBack = false;
        }
        else if (!isGoingBack && timer <= 0.0f)
        {
            timer = goBackDuration;
            isGoingBack = true;
        }

        OutOfBounds();
    }

    void OutOfBounds()
    {
        if (transform.position.y < -3)
        {
            Destroy(gameObject);
        }
    }
}
