using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    // effect only triggers when card is Played
    public void PlayedEffect();
    // effect is only triggered when card is Held in hand
    public void HeldEffect();
} 

public struct EasterEggEffect : IEffect
{
    public void HeldEffect()
    {
        // Easter egg multiplies total score by x1.5
       // retrieve totalScore from whatever handles points
       // int score = Mathf.RoundToInt(totalScore * 1.5f);
    }

    public void PlayedEffect()
    {
        return;
    }
}

public struct RudolfEffect : IEffect
{
    public void HeldEffect()
    {
        return;
    }

    public void PlayedEffect()
    {
        // This will be an array check that gives 200 points for every card >=1 index behind 
        // behind Rudolf. If Rudolf is index 0 then it will score no points
        HolidayCard[] cards = HandManager.Instance.GetPlayedCards();
        int count = 0;
        int rudolfPos = 0;
        foreach (var card in cards)
        {
            // finds which index hold Rudolf.
            rudolfPos = (card.cardName == CardName.Rudolf) ? count : rudolfPos;
            count++;
        }

        // If Rudolf is index 4 that means 4 cards (0-3) were behind him
        // so 800 points would be scored
        int pointsScored = (rudolfPos == 0) ? 0 : rudolfPos * 200;
        // send the points to whatever handles points
    }
}

public struct BoogeymanEffect : IEffect
{
    public void HeldEffect()
    {
        // add 100 to score for each played Halloween type
        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            if (card.GetCardType() == CardType.Halloween)
            {
                // add 100 points to played score
            }
            else {
                // subtract 100 points from score
            }
        }
    }

    public void PlayedEffect()
    {
        // Shuffle all non-matching types
        // shuffling should be handled by HandManager internally
    }
}

public struct PilgrimageEffect : IEffect
{
    public void HeldEffect()
    {
        return;
    }

    // When played it will give all cards in hand a +100 their base score
    // gives Turkey a + 300 to base score
    public void PlayedEffect()
    {
        foreach ( var card in HandManager.Instance.heldCards)
        {
            int points = (card.cardName == CardName.Turkey) ? 300 : 100;
            card.basePts += points;
            // none of this is added to round score. It just buffs held cards.
        }
    }
}

public struct TurkeyEffect : IEffect
{
    public void HeldEffect()
    {
        // Gains +50 for every turn not played when in hand
        // effect is stackable with multiple turkey cards
        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            if(card.cardName == CardName.Turkey) {
                card.basePts += 50;
            }
        }
    }

    public void PlayedEffect()
    {
        return;
    }
}

public struct PumpkinEffect : IEffect
{
    public void HeldEffect()
    {
        //Holds a base value of -300 to be a hindrance
        //decrease effect is stackable with other pumpkin cards
        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            if (card.cardName == CardName.Pumpkin)
            {
                card.basePts -= 100;
            }
        }
    }

    public void PlayedEffect()
    {
        //scores whatever points are left on the card
    }
}
