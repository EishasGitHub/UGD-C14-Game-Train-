using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float verticalInput;
    private float horizontalInput;
    public float rotationSpeed;

    public bool isGameStarted = false;

    private Vector3 offset = new Vector3(1, 1, 1);
    public Vector3 objectSize = new Vector3(1,1,1);
    private Vector3 minSize = new Vector3(1,1,1);
    private Vector3 maxSize = new Vector3(3,3,3);
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (isGameStarted) 
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            //rotate the player with up/down keys
            transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed * verticalInput);
            transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed * horizontalInput);

            if (Input.GetKeyDown(KeyCode.T))
            {
                EndGame();
            }

            ChangeObjectSize();
        }

        else
        {
            //check if Enter Key is pressed
            if (Input.GetKeyDown(KeyCode.Return)) 
            { 
                StartGame();
            }
        }
    }

    void ChangeObjectSize()
    {
        //continuous change in object size
        if (Input.GetKey(KeyCode.Space))
        {
            objectSize = transform.localScale + offset * Time.deltaTime;

            if ((objectSize.x <= maxSize.x) && (objectSize.y <= maxSize.y) && (objectSize.z <= maxSize.z))
            {
                transform.localScale = objectSize;
            }
        }

        else
        {
            objectSize = transform.localScale - offset * Time.deltaTime;

            if ((objectSize.x >= minSize.x) && (objectSize.y >= minSize.y) && (objectSize.z >= minSize.z))
            {
                transform.localScale = objectSize;
            }
        }
    }

    void StartGame ()
    {
        {
            isGameStarted = true;
            transform.localScale = objectSize;
        }
    }

    void EndGame ()
    {
        isGameStarted = false;
    }

}
