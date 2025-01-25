using UnityEngine;

public class DestroyCollectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    public static int counter = 0;

    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        counter++;

        //print(counter);
    }
}
