using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlots : MonoBehaviour
{
    private RectTransform rectTransform;
    private List<GameObject> cardSlots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PositionSlots()
    {
        /*if (swappingCards)
            return;*/

        float partition = rectTransform.rect.width * (1.0f / (float)(cardSlots.Count + 1));
        for (int i = 0; i < cardSlots.Count; i++)
        {
            cardSlots[i].transform.localPosition = new Vector2(
                (rectTransform.rect.width * -0.5f) + (partition * (i + 1)),
                0
            );
        }
    }
}
