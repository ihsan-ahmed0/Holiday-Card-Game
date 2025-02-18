using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardSlots : MonoBehaviour
{
    public UnityEvent<GameObject> ReturnToHandEvent;

    private RectTransform rectTransform;
    private List<GameObject> cardSlots;

    [Header("Card Interactions")]
    [SerializeField] private Card hoveredCard;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        cardSlots = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCards(List<GameObject> newCardSlots)
    {
        foreach (GameObject cardSlot in newCardSlots)
        {
            cardSlot.transform.SetParent(transform);
            cardSlot.GetComponentInChildren<Card>().PointerEnterEvent.AddListener(HoverEnter);
            cardSlot.GetComponentInChildren<Card>().PointerExitEvent.AddListener(HoverExit);
            cardSlot.GetComponentInChildren<Card>().ClickEvent.AddListener(CardClick);
            cardSlots.Add(cardSlot);
        }
        PositionSlots();
    }

    private void HoverEnter(Card card)
    {
        hoveredCard = card;
    }

    private void HoverExit(Card card)
    {
        hoveredCard = null;
    }

    private void CardClick(Card card)
    {
        card.GetComponentInChildren<Card>().PointerEnterEvent.RemoveListener(HoverEnter);
        card.GetComponentInChildren<Card>().PointerExitEvent.RemoveListener(HoverExit);
        card.GetComponentInChildren<Card>().ClickEvent.RemoveListener(CardClick);
        ReturnToHandEvent.Invoke(cardSlots[CardIndex(card)]);
        cardSlots.RemoveAt(CardIndex(card));
        PositionSlots();
    }

    private void PositionSlots()
    {
        /*if (swappingCards)
            return;*/

        float partition = rectTransform.rect.width * (1.0f / (float)(cardSlots.Count + 1));
        for (int i = 0; i < cardSlots.Count; i++)
        {
            cardSlots[i].transform.localPosition = new Vector2(
                (rectTransform.rect.width * -0.5f) + (partition * (i + 1)),
                0
            );
        }
    }

    private int CardIndex(Card card)
    {
        for (int i = 0; i < cardSlots.Count; i++)
        {
            if (cardSlots[i].GetComponentInChildren<Card>() == card)
            {
                return i;
            }
        }
        return -1;
    }
}
