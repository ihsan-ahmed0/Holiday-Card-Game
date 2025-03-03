using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }
    // played cards array will be cleared after cards are played
    private HolidayCard[] playedCards = new HolidayCard[0];
    public List<HolidayCard> heldCards;
    private GameManager gameManager;
    
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GameManager>();
            
    }

    public void TriggerHeldCards()
    {
        // loops naturally go from 0 to n index so cards will trigger left to right
        foreach (var card in heldCards) {
            card.GetEffect().HeldEffect(gameManager);
        }
    }

    public void TriggerPlayedCards()
    {
        foreach(var card in playedCards){
            card.GetEffect().PlayedEffect(gameManager);
        }
    }


    public HolidayCard[] GetPlayedCards() => playedCards;

    // Change to get unplayed cards
    public HolidayCard[] GetUnplayedCards() => playedCards;


    
}

