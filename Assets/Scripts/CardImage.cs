using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardImage : MonoBehaviour
{
    private Card parent;
    private Transform parentTransform;
    private bool initialized = false;

    [Header("Movement")]
    [SerializeField] private float speed = 30;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized)
            return;

        FollowCard();
        Rotate();
    }

    public void Init(Card card)
    {
        if (initialized) return;

        parent = card;
        parentTransform = card.transform;
        initialized = true;
    }

    private void FollowCard()
    {
        transform.position = Vector3.Lerp(transform.position, parentTransform.position, speed * Time.deltaTime);
    }

    private void Rotate()
    {
        float distance = Vector2.Distance(parentTransform.position, transform.position) * 0.3f;

        if (distance > 60)
            distance = 60;

        if (parentTransform.position.x > transform.position.x)
            distance *= -1;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, distance);
    }
}
