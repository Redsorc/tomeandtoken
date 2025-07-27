using UnityEngine;
using TMPro;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem Instance;
    public GameObject tooltipObject;
    public TMP_Text tooltipText;

    void Awake()
    {
        Instance = this;
        if (tooltipObject != null)
            tooltipObject.SetActive(false);
    }

    // Static wrapper to show tooltip
    public static void Show(string text)
    {
        if (Instance == null || Instance.tooltipObject == null) return;

        Instance.tooltipText.text = text;
        Instance.tooltipObject.SetActive(true);
    }

    // Static wrapper to hide tooltip
    public static void Hide()
    {
        if (Instance == null || Instance.tooltipObject == null) return;

        Instance.tooltipObject.SetActive(false);
    }
}
