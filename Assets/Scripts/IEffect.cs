using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public enum Holiday
{
    Christmas,
    Halloween,
    Thanksgiving,
    Easter,
    Special
}

public interface IEffect
{
    // Base points of card.
    int BasePoints { get; }

    // Holiday type of card.
    Holiday Holiday { get; }

    // effect only triggers when card is Played
    public void PlayedEffect(GameManager gameManager);
    // effect is only triggered when card is Held in hand
    public void HeldEffect(GameManager gameManager);
}

public class EasterBunnyEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        // Easter egg multiplies total score by x1.5
        // retrieve totalScore from whatever handles points
        // int score = Mathf.RoundToInt(totalScore * 1.5f);
    }

    public void PlayedEffect(GameManager gameManager)
    {
        // Does nothing when played
        return;
    }

    public EasterBunnyEffect()
    {
        BasePoints = 0;
        Holiday = Holiday.Easter;
    }
}

public class RudolphEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        return;
    }

    public void PlayedEffect(GameManager gameManager)
    {
        // This will be an array check that gives 200 points for every card >=1 index behind 
        // behind Rudolf. If Rudolf is index 0 then it will score no points

        // HolidayCard[] cards = HandManager.Instance.GetPlayedCards();
        // Debug.Log(cards);
        // int count = 0;
        // int rudolfPos = 0;
        // foreach (var card in cards)
        // {
        //     // finds which index hold Rudolf.
        //     rudolfPos = (card.cardName == CardName.Rudolf) ? count : rudolfPos;
        //     count++;
        // }

        // If Rudolf is index 4 that means 4 cards (0-3) were behind him
        // so 800 points would be scored
        int pointsScored = 0;//(rudolfPos == 0) ? 0 : rudolfPos * 200;
        // send the points to whatever handles points
        gameManager.addPoints(pointsScored + 100);
    }

    public RudolphEffect()
    {
        BasePoints = 0;
        Holiday = Holiday.Easter;
    }
}

public class BoogeymanEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        // add 100 to score for each played Halloween type
        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            if (card.GetCardType() == CardType.Halloween)
            {
                // add 100 points to played score
            }
            else
            {
                // subtract 100 points from score
            }
        }
    }

    public void PlayedEffect(GameManager gameManager)
    {
        int numHalloween = 0;
        int numNonHalloween = 0;
        // Shuffle all non-matching types
        // shuffling should be handled by HandManager internally

        gameManager.addPoints(numHalloween * 100 + numNonHalloween * (-100) + 150);
    }

    public BoogeymanEffect()
    {
        BasePoints = 0;
        Holiday = Holiday.Easter;
    }
}

public class PilgrimageEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        return;
    }

    // When played it will give all cards in hand a +100 their base score
    // gives Turkey a + 300 to base score
    public void PlayedEffect(GameManager gameManager)
    {
        foreach (var card in HandManager.Instance.heldCards)
        {
            int points = (card.cardName == CardName.Turkey) ? 300 : 100;
            card.basePts += points;
            // none of this is added to round score. It just buffs held cards.
        }

        gameManager.addPoints(50);
    }

    public PilgrimageEffect()
    {
        BasePoints = 0;
        Holiday = Holiday.Easter;
    }
}

public class TurkeyEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        // Gains +50 for every turn not played when in hand
        // effect is stackable with multiple turkey cards
        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            if (card.cardName == CardName.Turkey)
            {
                card.basePts += 50;
            }
        }
    }

    public void PlayedEffect(GameManager gameManager)
    {
        gameManager.addPoints(50);
        return;
    }

    public TurkeyEffect()
    {
        BasePoints = 0;
        Holiday = Holiday.Easter;
    }
}

public class PumpkinEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
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

    public void PlayedEffect(GameManager gameManager)
    {
        //scores whatever points are left on the card
        return;
    }

    public PumpkinEffect()
    {
        BasePoints = 0;
        Holiday = Holiday.Easter;
    }
}

public class SnowflakeEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        return;
    }

    public void PlayedEffect(GameManager gameManager)
    {
        int numChristmas = 0;
        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            if (card.GetCardType() == CardType.Christmas)
            {
                numChristmas += 1;
            }
        }

        gameManager.addPoints(numChristmas * 100);
        return;
    }

    public SnowflakeEffect()
    {
        BasePoints = 0;
        Holiday = Holiday.Easter;
    }
}

public class StuffingEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        return;
    }

    public void PlayedEffect(GameManager gameManager)
    {
        //add 50 points to total
        int isThanksgiving = 0;

        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            if (card.cardName == CardName.Turkey)
            {
                isThanksgiving = 1;
            }
        }

        gameManager.addPoints(isThanksgiving * 100 + 50);
    }

    public StuffingEffect()
    {
        BasePoints = 0;
        Holiday = Holiday.Easter;
    }
}

public class BobsledEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        return;
    }

    public void PlayedEffect(GameManager gameManager)
    {
        //add 50 pts to total
        //and reduce the point total requirement of this turn by 500
        gameManager.addPoints(50);
        gameManager.addGoalPoints(-500);
    }
}

public class BunnyHopEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        return;
    }

    public void PlayedEffect(GameManager gameManager)
    {
        //all played easter cards will re-trigger their played effect!
        int prevPoints = 0;


        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            //prevents infinte loop of effect re-triggers
            if (card.GetCardType() == CardType.Easter && card.cardName != CardName.BunnyHop)
            {
                // card.GetEffect().PlayedEffect(gameManager);
            }
        }

        gameManager.addPoints(prevPoints);
    }
}

public class ReindeerEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        //strengthens card points of eqivalent cards
        //effect is stackable with other reindeer cards
        foreach (var card in HandManager.Instance.heldCards)
        {
            if (card.cardName == CardName.Reindeer)
            {
                card.basePts += 50;
            }
        }
    }

    public void PlayedEffect(GameManager gameManager)
    {
        int count = 0;
        int deerPos = 0;
        int rudolfPos = 0;
        HolidayCard rudolf;
        HolidayCard[] cards = HandManager.Instance.GetPlayedCards();

        //if all deers are behind rudolf like the myth then rudolf gets a x2 bonus to its base points
        //if not then no bonus is awarded
        foreach (var card in cards)
        {

            //verify card positions
            deerPos = (card.cardName == CardName.Reindeer) ? count : deerPos;
            rudolfPos = (card.cardName == CardName.Rudolf) ? count : rudolfPos;
            count++;

        }

        //the bonus is stackable if true among all reindeers
        if (rudolfPos > deerPos) {
            rudolf = cards[rudolfPos];
            rudolf.basePts += 200;
        }

        // Only adds base 25 pts for now
        gameManager.addPoints(25);
        gameManager.addPoints(25);
    }
}

public class ZombieEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager) {
        //reduces score of non-Halloween cards by 75 when held
        foreach (var card in HandManager.Instance.heldCards)
        {
            if(card.GetCardType() != CardType.Halloween){
                card.basePts -= 75;
            }
        }
        //triggers secondary held effect
        Eliminate();
    }
    public void PlayedEffect(GameManager gameManager) { 
        int revivedPoints = 0;

    //revives halloween types with half their usual score
    //if there is a card graveyard that can be used
    //if not then random halloween cards will be revived

        gameManager.addPoints(revivedPoints + 25);
    }

    //will disappear in hand if there are no other Halloween types
    private void Eliminate(){
        bool discardZombie = true;
        foreach (var card in HandManager.Instance.heldCards)
        {
            if (card.GetCardType() == CardType.Halloween){
                discardZombie = false;
                break;
            }
        }

        if (discardZombie)
        {
            //remove this card from deck
            //ideally should be a method in HandManager
        }
    }
}

public class CornucopiaEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        return;
    }

    //creates a bonus +500 if both Turkey and Pilgrimage are in hand
    private bool BonusCheck()
    {
        bool hasTurkey = false;
        bool hasPilgrim = false;
        foreach(var card in HandManager.Instance.heldCards)
        {
            //checks if cards are in hand, if not, use assigned value
            hasTurkey = (card.cardName == CardName.Turkey) ? true : hasTurkey;
            hasPilgrim = (card.cardName == CardName.Pilgrimage) ? true : hasPilgrim;
        }

        //only returns true when both are true
        return hasTurkey && hasPilgrim;
    }

    //boosts all Thanksgiving types by +50 points when played
    public void PlayedEffect(GameManager gameManager)
    {
        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            if(card.GetCardType() == CardType.Thanksgiving)
            {
                card.basePts += 50;
            }
        }

        if (BonusCheck()){
            gameManager.addPoints(525);
            //add a bonus 500 to round score if bonus applies
        }
    }
}

public class HarvestMoonEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        
    }

    public void PlayedEffect(GameManager gameManager)
    {
        int unplayedThanksgiving = 0;

        foreach (var card in HandManager.Instance.GetUnplayedCards())
        {
            if(card.GetCardType() == CardType.Thanksgiving)
            {
                unplayedThanksgiving += 1;
            }
        }

        gameManager.addPoints(unplayedThanksgiving * 100 + 75);
    }
}

public class JackFrostEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {

    }

    public void PlayedEffect(GameManager gameManager)
    {
        int numChristmas = 0;
        int christmasPoints = 0;
        
        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            if(card.GetCardType() != CardType.Christmas)
            {
                numChristmas += 1;
            } 
            else 
            {
                christmasPoints += card.basePts;
            }
        }

        gameManager.addPoints(numChristmas * (-100) + christmasPoints * 2 + 75);
    }
}

public class NewYearEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {

    }

    public void PlayedEffect(GameManager gameManager)
    {
        int numCardsPlayed = 0;
        
        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            numCardsPlayed += 1;
        }

        gameManager.addPoints((int) Math.Ceiling(numCardsPlayed * 0.5) + 90);
    }
}

public class SantaEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {
        // Eliminate penalties of all cards
    }

    public void PlayedEffect(GameManager gameManager)
    {
        int numHalloween = 0;

        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            if (card.GetCardType() == CardType.Halloween)
            {
                if (card.GetCardType() == CardType.Halloween)
                {
                    numHalloween += 1;
                }

            }

            gameManager.addPoints(numHalloween * (-150) + 150);
        }
    }
}

public class GroundhogEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {

    }

    public void PlayedEffect(GameManager gameManager)
    {
        int prevRoundScore = 0;
        int numTurnsNotPlayed = 0;


        gameManager.addPoints(prevRoundScore + numTurnsNotPlayed * 100 + 60);
    }
}

public class WitchEffect : IEffect
{
    public int BasePoints { get; }

    public Holiday Holiday { get; }

    public void HeldEffect(GameManager gameManager)
    {

    }

    public void PlayedEffect(GameManager gameManager)
    {
        // HolidayCard leftCard;
        // HolidayCard rightCard;

        // // finds nearby halloween cards
        
        // // implement some checking to prevent infinite loops if either card is a witch card
        // leftCard.PlayCard();
        // rightCard.PlayCard();

        // Only adds base 25 pts for now
        gameManager.addPoints(75);
    }
}