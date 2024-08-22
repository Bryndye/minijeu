using UnityEngine;

[CreateAssetMenu]
public class TextBulle : ScriptableObject
{
    //String to store the text of the bubble
    [TextArea(3, 20)]
    public string text;
    
    //Type of the text bulle
    public CardModel.CardType type;
}