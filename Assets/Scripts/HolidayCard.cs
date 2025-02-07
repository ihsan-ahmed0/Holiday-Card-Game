using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Feel free to refactor this class
//it was created to avoid compile erros with the rest of the classes
public class HolidayCard : MonoBehaviour
{
    public CardName cardName;
    public int basePts; // points that card has inscribed to it
    private IEffect cardEffect;
    private CardType cardType; // any cardtype can be given to any selcted card (useful for type-changing effects)
   
    public void Init(CardName name, int pts, CardType type)
    {
        cardName = name;
        basePts = pts;
        cardEffect = EffectFinder.Find(cardName);
    }

    public CardType GetCardType() => cardType;
    public IEffect GetEffect() => cardEffect;
}

// string matching is case sensitive and annoying so I'm opting out for enum
public enum CardType
{
    Christmas,
    Halloween,
    Easter,
    Thanksgiving,
    Special

}

public enum CardName
{
    Rudolf,
    EasterEgg,
    Boogeyman,
    Pilgrimage,
    Turkey,
    Pumpkin
}