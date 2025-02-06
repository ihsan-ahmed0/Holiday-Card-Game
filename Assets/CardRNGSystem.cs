using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRNGSystem : MonoBehaviour
{
   public float rngValue;
   public string cardName="";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      rngValue=  Random.Range(1,159);

      if( rngValue>1 &&rngValue<8){
        cardName="Easter Egg";
      }
       else if( rngValue>9 &&rngValue<25){
        cardName="Boogey Man";
      }
       else if( rngValue>26 &&rngValue<34){
        cardName="Rudolf";
      }
       else if( rngValue>35 &&rngValue<44){
        cardName="Turkey";
      }
       else if( rngValue>45 &&rngValue<49){
        cardName="Pumpkin";
      }
       else if( rngValue>50 &&rngValue<59){
        cardName="Snowflake";
      }
       else if( rngValue>60 &&rngValue<63){
        cardName="Bobsled";
      }
       else if( rngValue>64 &&rngValue<70){
        cardName="Bunny Hop";
      }
       else if( rngValue>71 &&rngValue<85){
        cardName="Birds Stuffing";
      }
       else if( rngValue>86 &&rngValue<93){
        cardName="Bunny Hop";
      }
       else if( rngValue>94 &&rngValue<103){
        cardName="Reindeer";
      }
       else if( rngValue>104 &&rngValue<109){
        cardName="Zombie";
      }
       else if( rngValue>110 &&rngValue<118){
        cardName="Cornocopia";
      }
       else if( rngValue>119 &&rngValue<124){
        cardName="Harevest Moon";
      }
       else if( rngValue>125 &&rngValue<134){
        cardName="Jack Frost";
      }
       else if( rngValue>135 &&rngValue<137){
        cardName="New Year";
      }
       else if( rngValue>138 &&rngValue<145){
        cardName="Krampus/Santa";
      }
       else if( rngValue>145 &&rngValue<148){
        cardName="Groundhog";
      }
       else if( rngValue>148 &&rngValue<158){
        cardName="Witch";
      }
    }
}
