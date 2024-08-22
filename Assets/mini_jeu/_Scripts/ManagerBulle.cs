using TMPro;
using UnityEngine;

public class ManagerBulle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private Manager manager;
    [SerializeField] private GameObject _bubble;
    
    //Method to update the text of the bubble
    public void UpdateText(CardModel cardModel)
    {
        _bubble.SetActive(false);
        _bubble.SetActive(true);
        _textMeshPro.text = cardModel.textBulle.text;
    }
    
    public void DisplayRetry()
    {
        _bubble.SetActive(false);
        _bubble.SetActive(true);
        _textMeshPro.text = "Les cartes ne correspondent pas, veuillez réessayer";
    }

    public void Start()
    {
        _bubble.SetActive(false);
        manager = Manager.Instance;
        manager.OnSameCards += UpdateText;
        manager.OnDifferentCards += DisplayRetry;
    }
}
