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
    [SerializeField] private float goalPointIncrements = 1.5f;
    [SerializeField] private int turn = 0;

    [Header("UI Elements")]
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private TMP_Text currentPointsText;
    [SerializeField] private TMP_Text goalPointsText;
    [SerializeField] private Button addCardButton;
    [SerializeField] private Button playCardsButton;

    [Header("Card Groups")]
    [SerializeField] private GameObject playerHandObject;
    [SerializeField] private GameObject cardSlotsObject;

    private PlayerHand playerHand;
    private CardSlots cardSlots;


    // Start is called before the first frame update
    void Start()
    {
        Reset();
        updatePoints();

        addCardButton.onClick.AddListener(OnAddButtonClick);
        playCardsButton.onClick.AddListener(OnPlayButtonClick);

        playerHand = playerHandObject.GetComponent<PlayerHand>();
        cardSlots = cardSlotsObject.GetComponent<CardSlots>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            addPoints(10);
            updatePoints();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            multiplyPoints(1.5f);
            updatePoints();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            endTurn();
            updatePoints();
        }
    }

    public void addPoints(int x)
    {
        currentPoints += x;
    }

    public void multiplyPoints(float x)
    {
        currentPoints = Mathf.CeilToInt(currentPoints * x);
    }

    public void Reset()
    {
        currentPoints = 0;
        goalPoints = 100;
        goalPointIncrements = 1.5f;
        turn = 0;
        loseCanvas.SetActive(false);
    }

    void endTurn()
    {
        if (currentPoints >= goalPoints)
        {
            currentPoints = 0;
            goalPoints = Mathf.CeilToInt(goalPoints * goalPointIncrements);
            Debug.Log("You have enough points to move on! New point goal: " + goalPoints);
            turn += 1;
        }
        else
        {
            // Implement game over
            loseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    void updatePoints()
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

        playedCards.ForEach(cardSlot =>
        {
            Destroy(cardSlot);
        });

        playerHand.PositionSlots();
    }

    private void OnPlayButtonClick()
    {
        PlayCardsFromHand();
    }

    private void OnAddButtonClick()
    {
        DrawCard();
    }
}
