using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Card Image Parameters")]
    private Canvas canvas;
    private Image imageComponent;
    [SerializeField] private bool instantiateVisual = true;
    [SerializeField] private GameObject cardImagePrefab;
    private CardImage cardImage;

    private Vector3 offset;

    [Header("Movement")]
    [SerializeField] private float moveSpeedLimit = 10000;

    [Header("Selection")]
    [HideInInspector] private bool selected;
    private float selectionOffset = 50;
    private float pointerDownTime;
    private float pointerUpTime;

    [Header("States")]
    public bool isHovering;
    public bool isDragging;
    [HideInInspector] public bool wasDragged;

    private IEffect cardEffect;

    [Header("Events")]
    [HideInInspector] public UnityEvent<Card> PointerEnterEvent;
    [HideInInspector] public UnityEvent<Card> PointerExitEvent;
    [HideInInspector] public UnityEvent<Card, bool> PointerUpEvent;
    [HideInInspector] public UnityEvent<Card> PointerDownEvent;
    [HideInInspector] public UnityEvent<Card> BeginDragEvent;
    [HideInInspector] public UnityEvent<Card> EndDragEvent;
    [HideInInspector] public UnityEvent<Card, bool> SelectEvent;
    [HideInInspector] public UnityEvent<Card> ClickEvent;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        imageComponent = GetComponent<Image>();

        if (!instantiateVisual)
            return;

        GameObject cardImageObject = Instantiate(cardImagePrefab, GameObject.FindGameObjectWithTag("CardVisuals").transform);
        cardImage = cardImageObject.GetComponent<CardImage>();
        string cardType = GenerateCard();
        SetEffect(cardType);
        cardImage.Init(this, cardType);
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            Vector2 velocity = direction * Mathf.Min(moveSpeedLimit, Vector2.Distance(transform.position, targetPosition) / Time.deltaTime);
            transform.Translate(velocity * Time.deltaTime);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CheckCardGroup() == "CardSlots")
            return;

        BeginDragEvent.Invoke(this);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = mousePosition - (Vector2)transform.position;
        isDragging = true;
        wasDragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CheckCardGroup() == "CardSlots")
            return;

        EndDragEvent.Invoke(this);
        isDragging = false;

        StartCoroutine(FrameWait());
        ResetPosition();

        IEnumerator FrameWait()
        {
            yield return new WaitForEndOfFrame();
            wasDragged = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        PointerDownEvent.Invoke(this);
        pointerDownTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerUpTime = Time.time;
        PointerUpEvent.Invoke(this, pointerUpTime - pointerDownTime > .2f);

        if (pointerUpTime - pointerDownTime > .2f || wasDragged)
            return;

        if (CheckCardGroup() == "PlayerHand")
        {
            selected = !selected;
            ResetPosition();
        }
        else if (CheckCardGroup() == "CardSlots")
        {
            ClickEvent.Invoke(this);
        }
        else
        {
            Debug.LogError("Card is not in a parent group.");
        }
        
    }

    public bool IsSelected() {  return selected; }

    public void Deselect() { selected = false; }

    private void OnDestroy()
    {
    }

    public void ResetPosition()
    {
        if (selected)
        {
            transform.localPosition = Vector2.zero + new Vector2(0, selectionOffset);
        }
        else
        {
            transform.localPosition = Vector2.zero;
        }
        
    }

    private string CheckCardGroup()
    {
        if (GetComponentInParent<PlayerHand>() != null)
        {
            return "PlayerHand";
        }
        else if (GetComponentInParent<CardSlots>() != null)
        {
            return "CardSlots";
        }

        return "bruh";
    }

    private void SetEffect(string cardType)
    {
        if (cardType == "EasterBunny")
        {
            cardEffect = new EasterBunnyEffect();
        }
        else if (cardType == "Boogeyman")
        {
            cardEffect = new BoogeymanEffect();
        }
        else if (cardType == "Rudolph")
        {
            cardEffect = new RudolphEffect();
        }
        else if (cardType == "Turkey")
        {
            cardEffect = new TurkeyEffect();
        }
        else if (cardType == "Pumpkin")
        {
            cardEffect = new PumpkinEffect();
        }
        else if (cardType == "Snowflake")
        {
            cardEffect = new SnowflakeEffect();
        }
        else if (cardType == "Sled")
        {
            cardEffect = new BobsledEffect();
        }
        else if (cardType == "Zombie")
        {
            cardEffect = new ZombieEffect();
        }
        else if (cardType == "BirdsStuffing")
        {
            cardEffect = new StuffingEffect();
        }
        else if (cardType == "Pilgrimage")
        {
            cardEffect = new PilgrimageEffect();
        }
        else if (cardType == "Reindeer")
        {
            cardEffect = new ReindeerEffect();
        }
        else if (cardType == "Cornucopia")
        {
            cardEffect = new CornucopiaEffect();
        }
        else if (cardType == "HarvestMoon")
        {
            cardEffect = new HarvestMoonEffect();
        }
        else if (cardType == "JackFrost")
        {
            cardEffect = new JackFrostEffect();
        }
        else if (cardType == "NewYear")
        {
            cardEffect = new NewYearEffect();
        }
        else if (cardType == "Santa")
        {
            cardEffect = new SantaEffect();
        }
        else if (cardType == "Groundhog")
        {
            cardEffect = new GroundhogEffect();
        }
        else if (cardType == "Witch")
        {
            cardEffect = new WitchEffect();
        }
    }

    private string GenerateCard()
    {
        int rngValue = Random.Range(1, 159);

        if (rngValue >= 1 && rngValue <= 8)
        {
            return "EasterBunny";
        }
        else if (rngValue >= 9 && rngValue <= 25)
        {
            return "Boogeyman";
        }
        else if (rngValue >= 26 && rngValue <= 34)
        {
            return "Rudolph";
        }
        else if (rngValue >= 35 && rngValue <= 44)
        {
            return "Turkey";
        }
        else if (rngValue >= 45 && rngValue <= 49)
        {
            return "Pumpkin";
        }
        else if (rngValue >= 50 && rngValue <= 59)
        {
            return "Snowflake";
        }
        else if (rngValue >= 60 && rngValue <= 63)
        {
            return "Sled";
        }
        else if (rngValue >= 64 && rngValue <= 70)
        {
            return "Zombie";
        }
        else if (rngValue >= 71 && rngValue <= 85)
        {
            return "BirdsStuffing";
        }
        else if (rngValue >= 86 && rngValue <= 93)
        {
            return "Pilgrimage";
        }
        else if (rngValue >= 94 && rngValue <= 103)
        {
            return "Reindeer";
        }
        else if (rngValue >= 104 && rngValue <= 109)
        {
            return "Zombie";
        }
        else if (rngValue >= 110 && rngValue <= 118)
        {
            return "Cornucopia";
        }
        else if (rngValue >= 119 && rngValue <= 124)
        {
            return "HarvestMoon";
        }
        else if (rngValue >= 125 && rngValue <= 134)
        {
            return "JackFrost";
        }
        else if (rngValue >= 135 && rngValue <= 137)
        {
            return "NewYear";
        }
        else if (rngValue >= 138 && rngValue <= 145)
        {
            return "Santa";
        }
        else if (rngValue >= 145 && rngValue <= 148)
        {
            return "Groundhog";
        }
        else if (rngValue >= 148 && rngValue <= 158)
        {
            return "Witch";
        }

        return "";
    }
}
