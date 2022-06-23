using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBoxing : MonoBehaviour
{
    [SerializeField]
    private float boxYSize;
    [SerializeField]
    private RectTransform letterBoxTop;
    [SerializeField]
    private RectTransform letterBoxBottom;
    private Controller playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.charged == 2 && letterBoxTop.anchoredPosition.y > 650)
        {
            letterBoxTop.anchoredPosition = new Vector2(letterBoxTop.anchoredPosition.x, letterBoxTop.anchoredPosition.y - 100.0f * Time.deltaTime);
            letterBoxBottom.anchoredPosition = new Vector2(letterBoxBottom.anchoredPosition.x, letterBoxBottom.anchoredPosition.y + 100.0f * Time.deltaTime);
        }
        else if (playerController.charged == 1 && letterBoxTop.anchoredPosition.y < 750)
        {
            letterBoxTop.anchoredPosition = new Vector2(letterBoxTop.anchoredPosition.x, letterBoxTop.anchoredPosition.y + 100.0f * Time.deltaTime);
            letterBoxBottom.anchoredPosition = new Vector2(letterBoxBottom.anchoredPosition.x, letterBoxBottom.anchoredPosition.y - 100.0f * Time.deltaTime);
        }

        letterBoxTop.anchoredPosition = new Vector2(letterBoxTop.anchoredPosition.x, Mathf.Clamp(letterBoxTop.anchoredPosition.y, 650.0f, 750.0f));
        letterBoxBottom.anchoredPosition = new Vector2(letterBoxBottom.anchoredPosition.x, Mathf.Clamp(letterBoxBottom.anchoredPosition.y, -750.0f, -650.0f));
    }
}
