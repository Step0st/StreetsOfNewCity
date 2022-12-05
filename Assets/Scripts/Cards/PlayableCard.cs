using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CardType cardType;
    public int effectValue;
    public int temporaryEffectValue;
    public int duration;
    private Animation _animation;

    private void OnValidate()
    {
        if (effectValue <= 0) effectValue = 1;
        if (temporaryEffectValue < 0) temporaryEffectValue = 0;
        if (duration < 0) duration = 0;
    }

    private void Start()
    {
        _animation = GetComponentInChildren<Animation>();
    }
    
    public void DestroyCard()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyWithAnimation()
    {
        if (gameObject != null)
        {
            _animation.Play();
            Destroy(gameObject, 0.5f);
        }
    }

    public void Deactivate()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CardEvents.OnDragStart(this.gameObject);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        CardEvents.OnDrag(this.gameObject);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        CardEvents.OnDragEnd(this.gameObject);
    }
}
