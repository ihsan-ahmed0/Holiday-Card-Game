using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [Header("Game Data")]
    [SerializeField] private int currentPoints = 0;
    [SerializeField] private int goalPoints = 100;
    [SerializeField] private float goalPointIncrements = 1.25f;
    [SerializeField] private int turn = 0;

    [Header("UI Elements")]
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private TMP_Text currentPointsText;
    [SerializeField] private TMP_Text goalPointsText;
    [SerializeField] private Button addCardButton;
    [SerializeField] private Button playCardsButton;
    [SerializeField] private Button endTurnButton;

    [Header("Card Groups")]
    [SerializeField] private GameObject playerHandObject;
    [SerializeField] private GameObject cardSlotsObject;
    private List<GameObject> cardsInPlay = new List<GameObject>();


    private PlayerHand playerHand;
    private CardSlots cardSlots;


    // Start is called before the first frame update
    void Start()
    {
        Reset();
        updatePoints();

        addCardButton.onClick.AddListener(OnAddButtonClick);
        playCardsButton.onClick.AddListener(OnPlayButtonClick);
        endTurnButton.onClick.AddListener(OnEndTurnClick);

        playerHand = playerHandObject.GetComponent<PlayerHand>();
        cardSlots = cardSlotsObject.GetComponent<CardSlots>();
        cardSlots.ReturnToHandEvent.AddListener(ReturnCardToHand);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addPoints(int x)
    {
        currentPoints += x;
        Debug.Log(currentPoints);
    }

    public void addGoalPoints(int x)
    {
        goalPoints += x;
    }

    public void multiplyPoints(float x)
    {
        currentPoints = Mathf.CeilToInt(currentPoints * x);
    }

    public void Reset()
    {
        currentPoints = 0;
        goalPoints = 100;
        goalPointIncrements = 1.25f;
        turn = 0;
        loseCanvas.SetActive(false);
    }

    public void endTurn()
    {
        if (currentPoints >= goalPoints)
        {
            currentPoints = 0;
            goalPoints = Mathf.CeilToInt(goalPoints * goalPointIncrements);
            Debug.Log("You have enough points to move on! New point goal: " + goalPoints);
            updatePoints();
            turn += 1;
        }
        else
        {
            // Implement game over
            loseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    public void updatePoints()
    {
        currentPointsText.text = "Points: " + currentPoints;
        goalPointsText.text = "Goal: " + goalPoints;
    }

    private void DrawCard()
    {
        playerHand.AddCard();
    }

    private void PlayCardsFromHand()
    {
        List<GameObject> playedCards = playerHand.PlayCards();

        foreach (GameObject card in playedCards)
        {
            cardsInPlay.Add(card);
        }

        cardSlots.AddCards(playedCards); 

        playerHand.PositionSlots();


        // print played cards
        Debug.Log("Cards in play:");
        foreach (GameObject cardSlot in cardsInPlay)
        {
            Card card = cardSlot.GetComponentInChildren<Card>();
            if (card != null)
            {
                Debug.Log(card.cardEffect);
            }
        }
    }

    private void ReturnCardToHand(GameObject cardSlot)
    {
        playerHand.AddExistingCard(cardSlot);
    }

    private void OnPlayButtonClick()
    {
        PlayCardsFromHand();
    }

    private void OnAddButtonClick()
    {
        DrawCard();
    }

    private void OnEndTurnClick()
    {
        // Prints held cards
        Debug.Log("Player's Hand:");
        foreach (GameObject cardSlot in playerHand.GetCardSlots())
        {
            Card card = cardSlot.GetComponentInChildren<Card>();
            if (card != null)
            {
                Debug.Log("Card in Hand: " + card.cardEffect);
            }
        }

        // Print cards in play
        Debug.Log("Cards in Play:");
        foreach (GameObject cardSlot in cardsInPlay)
        {
            Card card = cardSlot.GetComponentInChildren<Card>();
            if (card != null)
            {
                Debug.Log("Card in Play: " + card.cardEffect);
            }
        }
        // endTurn();
    }
}
