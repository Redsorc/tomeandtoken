using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ArchetypesUIController : MonoBehaviour
{
    [System.Serializable]
    public class ArchetypeData
    {
        public string archetypeName;
        public string description;
        public Sprite background;
        public List<Card> sampleCards;
    }

    public List<ArchetypeData> archetypes;
    public Image backgroundImage;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public Transform cardPreviewContainer;
    public GameObject cardPreviewPrefab;

    private List<CardPreview> cardPreviews = new List<CardPreview>();

    void Start()
    {
        // Initialize card previews
        foreach (Transform child in cardPreviewContainer)
            Destroy(child.gameObject);

        for (int i = 0; i < 5; i++)
        {
            var cardGO = Instantiate(cardPreviewPrefab, cardPreviewContainer);
            var preview = cardGO.GetComponent<CardPreview>();
            cardPreviews.Add(preview);
        }

        ShowArchetype(0);
    }

    public void ShowArchetype(int index)
    {
        var data = archetypes[index];
        titleText.text = data.archetypeName;
        descriptionText.text = data.description;
        backgroundImage.sprite = data.background;

        for (int i = 0; i < cardPreviews.Count; i++)
        {
            if (i < data.sampleCards.Count)
                cardPreviews[i].SetCard(data.sampleCards[i]);
            else
                cardPreviews[i].Clear();
        }
    }
}