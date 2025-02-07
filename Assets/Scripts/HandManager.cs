using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }
    // played cards array will be cleared after cards are played
    private HolidayCard[] playedCards;
    public List<HolidayCard> heldCards;
    
    void Awake()
    {
        if (Instance == null) 
            Instance = this;
    }

    public void TriggerHeldCards()
    {
        // loops naturally go from 0 to n index so cards will trigger left to right
        foreach (var card in heldCards) {
            card.GetEffect().HeldEffect();
        }
    }

    public void TriggerPlayedCards()
    {
        foreach(var card in playedCards){
            card.GetEffect().PlayedEffect();
        }
    }

    public HolidayCard[] GetPlayedCards() => playedCards;


    
}

