using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    //Create a card type and remove the card id
    //We create an enum with the 6 types of cards (fear, joy, sadness, anger, surprise, disgust)
    public enum CardType
    {
        Colere,
        Degout,
        Joie,
        Peur,
        Surprise,
        Tristesse
    }
    
    [SerializeField] private CardType _cardType;
    public CardType Type { get { return _cardType; } }

    private Event_card myEventCard;
    public Event_card MyEventCard { get { return myEventCard; } }

    public Sprite frontSprite;
    public Sprite backSprite;

    private void Awake()
    {
        myEventCard = GetComponent<Event_card>();
    }

}