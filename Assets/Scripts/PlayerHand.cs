using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHand : MonoBehaviour
{
    [Header("Cards to Spawn")]
    [SerializeField] private GameObject cardSlotPrefab;

    private RectTransform rectTransform;
    private List<GameObject> cardSlots;

    [Header("Card Interactions")]
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

    public void AddCard()
    {
        GameObject newCardSlot = Instantiate(cardSlotPrefab, transform);
        newCardSlot.GetComponentInChildren<Card>().PointerEnterEvent.AddListener(HoverEnter);
        newCardSlot.GetComponentInChildren<Card>().PointerExitEvent.AddListener(HoverExit);
        newCardSlot.GetComponentInChildren<Card>().BeginDragEvent.AddListener(BeginDrag);
        newCardSlot.GetComponentInChildren<Card>().EndDragEvent.AddListener(EndDrag);
        cardSlots.Add(newCardSlot);
        PositionSlots();
    }

    public void AddExistingCard(GameObject cardSlot)
    {
        cardSlot.transform.SetParent(transform);
        cardSlot.GetComponentInChildren<Card>().PointerEnterEvent.AddListener(HoverEnter);
        cardSlot.GetComponentInChildren<Card>().PointerExitEvent.AddListener(HoverExit);
        cardSlot.GetComponentInChildren<Card>().BeginDragEvent.AddListener(BeginDrag);
        cardSlot.GetComponentInChildren<Card>().EndDragEvent.AddListener(EndDrag);
        cardSlots.Add(cardSlot);
        PositionSlots();
    }

    public GameObject RemoveCard()
    {
        return null;
    }

    public List<GameObject> PlayCards()
    {
        List<GameObject> selectedCards = new List<GameObject>();
        int numCards = cardSlots.Count;

        for (int i = 0; i < numCards;)
        {
            Card currentCard = cardSlots[i].GetComponentInChildren<Card>();
            if (currentCard.IsSelected())
            {
                currentCard.Deselect();
                currentCard.ResetPosition();
                currentCard.PointerEnterEvent.RemoveListener(HoverEnter);
                currentCard.PointerExitEvent.RemoveListener(HoverExit);
                currentCard.BeginDragEvent.RemoveListener(BeginDrag);
                currentCard.EndDragEvent.RemoveListener(EndDrag);

                selectedCards.Add(cardSlots[i]);
                cardSlots.Remove(cardSlots[i]);
                numCards = cardSlots.Count;
            }
            else
            {
                i++;
            }
        }

        return selectedCards;
    }

    public void PositionSlots()
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
