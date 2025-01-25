using UnityEngine;

public class CollectCollectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int totalCollectibles = 9;
    public GameObject collectiblePrefab;
    private float range = 14.0f;
    public Vector3 collectiblePosition;

    void Start()
    {
        for (int i = 0; i < totalCollectibles; i++)
        {
            collectiblePosition = new Vector3(Random.Range(-range, range), 0, Random.Range(range, 3));
            Instantiate(collectiblePrefab, collectiblePosition, collectiblePrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
