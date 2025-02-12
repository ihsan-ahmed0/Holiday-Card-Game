using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [Header("Cards to Spawn")]
    [SerializeField] private GameObject cardSlotPrefab;

    private RectTransform rectTransform;
    private List<GameObject> cardSlots;

    [Header("Dragged Card")]
    [SerializeField] private Card draggedCard;
    [SerializeField] private int draggedCardIndex = -1;
    [SerializeField] private Card hoveredCard;

    private bool swappingCards = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        cardSlots = new List<GameObject>();
        cardSlots.Add(Instantiate(cardSlotPrefab, transform));
        cardSlots.Add(Instantiate(cardSlotPrefab, transform));
        cardSlots.Add(Instantiate(cardSlotPrefab, transform));
        cardSlots.Add(Instantiate(cardSlotPrefab, transform));

        foreach (GameObject cardSlot in cardSlots)
        {
            cardSlot.GetComponentInChildren<Card>().PointerEnterEvent.AddListener(HoverEnter);
            cardSlot.GetComponentInChildren<Card>().PointerExitEvent.AddListener(HoverExit);
            cardSlot.GetComponentInChildren<Card>().BeginDragEvent.AddListener(BeginDrag);
            cardSlot.GetComponentInChildren<Card>().EndDragEvent.AddListener(EndDrag);
        }

        PositionSlots();
    }

    // Update is called once per frame
    void Update()
    {
        PositionSlots();
        CheckDraggedCard();
    }

    private void PositionSlots()
    {
        if (swappingCards)
            return;

        float partition = rectTransform.rect.width * (1.0f / (float)(cardSlots.Count + 1));
        for (int i = 0; i < cardSlots.Count; i++)
        {
            cardSlots[i].transform.localPosition = new Vector2(
                (rectTransform.rect.width * -0.5f) + (partition * (i + 1)),
                0
            );
        }
    }

    private void CheckDraggedCard()
    {
        if (draggedCard == null || swappingCards)
            return;

        for (int i = 0; i < cardSlots.Count; i++)
        {
            Card currentCard = cardSlots[i].GetComponentInChildren<Card>();
            if (draggedCard.transform.position.x > currentCard.transform.position.x
                && draggedCardIndex < i)
            {
                Swap(i);
                break;
            }

            if (draggedCard.transform.position.x < currentCard.transform.position.x
                && draggedCardIndex > i)
            {
                Swap(i);
                break;
            }
        }
    }

    private void Swap(int index)
    {
        swappingCards = true;
        Card draggedCard = cardSlots[draggedCardIndex].GetComponentInChildren<Card>();

        if (draggedCardIndex < index)
        {
            for (int i = draggedCardIndex; i < index; i++)
            {
                Card currentCard = cardSlots[i + 1].GetComponentInChildren<Card>();
                currentCard.transform.SetParent(cardSlots[i].transform);
                currentCard.ResetPosition();
            }
        }
        else if (draggedCardIndex > index)
        {
            for (int i = draggedCardIndex; i > index; i--)
            {
                Card currentCard = cardSlots[i - 1].GetComponentInChildren<Card>();
                currentCard.transform.SetParent(cardSlots[i].transform);
                currentCard.ResetPosition();
            }
        }

        draggedCard.transform.SetParent(cardSlots[index].transform);
        draggedCardIndex = index;

        swappingCards = false;

        //PositionSlots();
    }

    private void BeginDrag(Card card)
    {
        draggedCard = card;
        draggedCardIndex = CardIndex(card);
    }

    void EndDrag(Card card)
    {
        draggedCard = null;
        draggedCardIndex = -1;
    }

    void HoverEnter(Card card)
    {
        hoveredCard = card;
    }

    void HoverExit(Card card)
    {
        hoveredCard = null;
    }

    int CardIndex(Card card)
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
