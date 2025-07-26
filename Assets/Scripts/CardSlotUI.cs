using UnityEngine;
using UnityEngine.UI;

public class CardSlotUI : MonoBehaviour
{
    public Image artImage;
    public Text nameText;
    int index;

    public void Init(Card card, int slotIndex)
    {
        index = slotIndex;
        artImage.sprite = card.Art;
        nameText.text = card.Name;
    }

    public void OnClick()
    {
        DraftManager.Instance.Select(index);
    }
}