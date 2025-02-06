using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardGroup : MonoBehaviour
{
    [Header("Cards to Spawn")]
    [SerializeField] private GameObject cardSlotPrefab;

    private RectTransform rectTransform;
    private List<GameObject> cards;
    private Card draggedCard;
    private Card hoveredCard;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        cards = new List<GameObject>();
        cards.Add(Instantiate(cardSlotPrefab, transform));
        cards.Add(Instantiate(cardSlotPrefab, transform));
        cards.Add(Instantiate(cardSlotPrefab, transform));
        cards.Add(Instantiate(cardSlotPrefab, transform));

        foreach (GameObject card in cards)
        {
            card.GetComponent<Card>().PointerEnterEvent.AddListener(HoverEnter);
            card.GetComponent<Card>().PointerExitEvent.AddListener(HoverExit);
            card.GetComponent<Card>().BeginDragEvent.AddListener(BeginDrag);
            card.GetComponent<Card>().EndDragEvent.AddListener(EndDrag);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PositionCards();
    }

    private void PositionCards()
    {
        float partition = rectTransform.rect.width * (1.0f / (float)(cards.Count + 1));
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.localPosition = new Vector2(
                (rectTransform.rect.width * -0.5f) + (partition * (i + 1)),
                0
            );
        }
    }

    private void BeginDrag(Card card)
    {
        draggedCard = card;
    }

    void EndDrag(Card card)
    {
        draggedCard = null;
    }

    void HoverEnter(Card card)
    {
        hoveredCard = card;
    }

    void HoverExit(Card card)
    {
        hoveredCard = null;
    }
}
