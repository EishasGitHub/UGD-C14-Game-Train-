using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI countText;
    public RectTransform canvasTransform;
    public RawImage starPrefab;
    private int offset = 2;
    private bool firstStar = false;
    private bool secondStar = false;
    private bool thirdStar = false;

    void Start()
    {
        UpdateCountUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCountUI();
    }

    void UpdateCountUI()
    {
        countText.text = "Pizza Eaten: " + DestroyCollectible.counter;

        if (DestroyCollectible.counter == 3 && !firstStar)
        {
            //print("one star added!");
            starInstantiate();
            firstStar = true;
        }

        else if (DestroyCollectible.counter == 6 && !secondStar)
        {
            //print("two stars added!");
            starInstantiate();
            secondStar = true;
        }

        else if (DestroyCollectible.counter == 9 && !thirdStar)
        {
            //print("three stars added!");
            starInstantiate();
            thirdStar = true;
        }
    }

    void starInstantiate()
    {
        RawImage starInstance = Instantiate(starPrefab, canvasTransform);
        starInstance.rectTransform.anchoredPosition = new Vector2(10 + (offset*50), 100);
        offset++;
    }
}
