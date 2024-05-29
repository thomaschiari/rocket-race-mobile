using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject scrollDownArrow;
    public GameObject scrollUpArrow;
    public GameObject scrollDownText;
    public GameObject scrollUpText;

    void Update()
    {
        if (scrollRect.verticalNormalizedPosition <= 0.05)  // Adjust based on testing
        {
            scrollDownArrow.SetActive(false);
            scrollDownText.SetActive(false);
        }
        else
        {
            scrollDownArrow.SetActive(true);
            scrollDownText.SetActive(true);
        }

        if (scrollRect.verticalNormalizedPosition >= 0.95)  // Adjust based on testing
        {
            scrollUpArrow.SetActive(false);
            scrollUpText.SetActive(false);
        }
        else
        {
            scrollUpArrow.SetActive(true);
            scrollUpText.SetActive(true);
        }
    }
}
