using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlots : MonoBehaviour
{
    private RectTransform rectTransform;
    private List<GameObject> cardSlots;

    [Header("Card Interactions")]
    [SerializeField] private Card draggedCard;
    [SerializeField] private int draggedCardIndex = -1;
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
            cardSlot.GetComponentInChildren<Card>().BeginDragEvent.AddListener(BeginDrag);
            cardSlot.GetComponentInChildren<Card>().EndDragEvent.AddListener(EndDrag);
            cardSlots.Add(cardSlot);
        }
        PositionSlots();
    }

    private void BeginDrag(Card card)
    {
        draggedCard = card;
        draggedCardIndex = CardIndex(card);
    }

    private void EndDrag(Card card)
    {
        draggedCard = null;
        draggedCardIndex = -1;
    }

    private void HoverEnter(Card card)
    {
        hoveredCard = card;
    }

    private void HoverExit(Card card)
    {
        hoveredCard = null;
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
