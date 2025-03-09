using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Scroll : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float floatSpeed = 0.001f; 

    private bool movingUp = true;
    private Vector3 currentpos;
    private PlayerMovement pm;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            pm = playerObj.GetComponent<PlayerMovement>();
        }

        else
        {
            Debug.LogError("Player GameObject not found!");
        }
        currentpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.GameStarted)
        {
            if (movingUp)
            {
                transform.Translate(Vector3.right * floatSpeed);
                if (transform.localPosition.y >= currentpos.y + 0.2f)
                    movingUp = false;
            }
            else
            {
                transform.Translate(Vector3.left * floatSpeed);
                if (transform.localPosition.y < currentpos.y)
                    movingUp = true;
            }
        }
        
    }
}
