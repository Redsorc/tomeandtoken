using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DraftUIController : MonoBehaviour
{
    public Transform packContainer;
    public GameObject cardSlotPrefab;
    public Text progressText;
    void OnEnable()
    {
        DraftManager.Instance.OnPackReady += ShowPack;
        DraftManager.Instance.OnDraftComplete += _ => UIManager.Instance.ShowScreen(ScreenID.Bracket);
    }
    void OnDisable()
    {
        DraftManager.Instance.OnPackReady -= ShowPack;
    }
    public void BeginDraft() => DraftManager.Instance.StartDraft();
    void ShowPack(List<Card> pack)
    {
        foreach (Transform t in packContainer) Destroy(t.gameObject);
        for (int i = 0; i < pack.Count; i++)
        {
            var go = Instantiate(cardSlotPrefab, packContainer);
            var ui = go.GetComponent<CardSlotUI>(); ui.Init(pack[i], i);
            go.GetComponent<Button>().onClick.AddListener(ui.OnClick);
        }
        progressText.text = $"Pick {DraftManager.Instance.Deck.Count + 1} of 30";
    }
}