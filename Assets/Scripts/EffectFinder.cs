using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EffectFinder 
{
    public static IEffect Find(CardName name) => name switch
    {
        CardName.Rudolf => new RudolfEffect(),
        CardName.EasterEgg => new EasterEggEffect(),
        CardName.Boogeyman => new BoogeymanEffect(),
        CardName.Pilgrimage => new PilgrimageEffect(),
        CardName.Turkey => new TurkeyEffect(),
        CardName.Pumpkin => new PumpkinEffect(),
        _ => throw new ArgumentException("Invalid card name", nameof(name))
    };
}
