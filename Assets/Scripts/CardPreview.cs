using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image cardImage;
    private Card cardData;
    private Vector3 originalScale;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    // Assigns a card to this preview
    public void SetCard(Card card)
    {
        cardData = card;
        if (cardImage != null && card != null)
        {
            cardImage.sprite = card.Art;
            cardImage.enabled = true;
        }
        else if (cardImage != null)
        {
            cardImage.enabled = false;
        }
    }

    // Clears the preview (no card)
    public void Clear()
    {
        cardData = null;
        if (cardImage != null)
        {
            cardImage.sprite = null;
            cardImage.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cardData == null) return;

        transform.localScale = originalScale * 1.2f;
        TooltipSystem.Show(cardData.GetTooltipText());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        TooltipSystem.Hide();
    }
}
