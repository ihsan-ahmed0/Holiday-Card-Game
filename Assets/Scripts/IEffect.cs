using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;

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
            else
            {
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
        foreach (var card in HandManager.Instance.heldCards)
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
            if (card.cardName == CardName.Turkey)
            {
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

public struct SnowflakeEffect : IEffect
{
    public void HeldEffect()
    {
        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            if (card.GetCardType() == CardType.Christmas)
            {
                // add 100 points to score total for each christmas card
            }
        }
    }

    public void PlayedEffect()
    {
        return;
    }
}

public struct StuffingEffect : IEffect
{
    public void HeldEffect()
    {
        return;
    }

    public void PlayedEffect()
    {
        //add 50 points to total

        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            if (card.cardName == CardName.Turkey)
            {
                //add 100 additional points to total
            }
        }
    }
}

public struct BobsledEffect : IEffect
{
    public void HeldEffect()
    {
        return;
    }

    public void PlayedEffect()
    {
        //add 50 pts to total
        //and reduce the point total requirement of this turn by 500
    }
}

public struct BunnyHopEffect : IEffect
{
    public void HeldEffect()
    {
        return;
    }

    public void PlayedEffect()
    {
        //all played easter cards will re-trigger their played effect!
        foreach (var card in HandManager.Instance.GetPlayedCards())
        {
            //prevents infinte loop of effect re-triggers
            if (card.GetCardType() == CardType.Easter && card.cardName != CardName.BunnyHop)
            {
                card.GetEffect().PlayedEffect();
            }
        }
    }
}

public struct ReindeerEffect : IEffect
{
    public void HeldEffect()
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

    public void PlayedEffect()
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
    }
}

    public struct ZombieEffect : IEffect
    {
        public void HeldEffect() {
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
        public void PlayedEffect() { 
            //revives halloween types with half their usual score
            //if there is a card graveyard that can be used
            //if not then random halloween cards will be revived
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

    public struct CornucopiaEffect : IEffect
    {
        public void HeldEffect()
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
        public void PlayedEffect()
        {
            foreach (var card in HandManager.Instance.GetPlayedCards())
            {
                if(card.GetCardType() == CardType.Thanksgiving)
                {
                    card.basePts += 50;
                }
            }

            if (BonusCheck()){
                //add a bonus 500 to round score if bonus applies
            }
        }
    }

