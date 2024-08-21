using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardId : MonoBehaviour
{
    [SerializeField] private int _cardId;
    public int Cardid { get { return _cardId; } }

    private Event_card myEventCard;
    public Event_card MyEventCard { get { return myEventCard; } }

    public Sprite frontSprite;
    public Sprite backSprite;

    private void Awake()
    {
        myEventCard = GetComponent<Event_card>();
    }

}