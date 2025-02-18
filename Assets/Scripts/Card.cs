using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    private Canvas canvas;
    private Image imageComponent;
    [SerializeField] private bool instantiateVisual = true;
    private Vector3 offset;

    [Header("Movement")]
    [SerializeField] private float moveSpeedLimit = 5000;

    [Header("Selection")]
    [HideInInspector] private bool selected;
    private float selectionOffset = 50;
    private float pointerDownTime;
    private float pointerUpTime;

    [Header("States")]
    public bool isHovering;
    public bool isDragging;
    [HideInInspector] public bool wasDragged;

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
}
