using UnityEngine;

[RequireComponent (typeof(Collider2D), typeof(CardModel))]
public class EventCard : MonoBehaviour
{
    //Enum to represent the state of the card (face up or face down)
    public enum CardState
    {
        FaceUp,
        FaceDown
    }
    
    //Enum to determine if card is discovered or not (pair is found)
    public enum CardDiscovered
    {
        Discovered,
        NotDiscovered
    }
    
    
    private Camera _cam;
    private Collider2D _cardCollider;
    private CardState _cardState;
    private CardDiscovered _cardDiscovered;
    private Animator _cardAnimator;
    private Manager _manager;
    private CardModel _cardModel;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _spriteCadreDiscovered;
    
    // Start is called before the first frame update
    void Awake()
    {
        _cam = Camera.main;   
        _cardCollider = GetComponent<Collider2D>();
        _cardAnimator = GetComponent<Animator>();
        _cardModel = GetComponent<CardModel>();
        
        ChangeColor();
        SetCardState(CardState.FaceDown);

        SetCardDiscovered(CardDiscovered.NotDiscovered);
    }

    private void Start()
    {
        _manager = Manager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the mouse is over the card
        CheckHover();
        
        //Check if the card is clicked
        CheckClick();
    }

    public void CheckClick()
    {
        if (_cardDiscovered == CardDiscovered.Discovered)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !_manager.IsUsingCards)
        {
            Vector3 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the z-coordinate is zero for 2D

            if (_cardCollider.OverlapPoint(mousePosition))
            {
                //Edit state of the card
                if (_cardState == CardState.FaceDown)
                {
                    SetCardState(CardState.FaceUp);
                }
                else
                {
                    SetCardState(CardState.FaceDown);
                    _manager?.ResetCard(_cardModel);
                }
            }
        }
    }

    public void CheckHover()
    {
        Vector3 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z-coordinate is zero for 2D

        if (_cardCollider.OverlapPoint(mousePosition))
        {
            //Debug.Log("Mouse is over the card");
        }
    }
    
    //Create setter for the card state
    public void SetCardState(CardState state)
    {
        _cardState = state;
        _handleState();
    }

    //Create setter for the card discovered
    public void SetCardDiscovered(CardDiscovered discovered)
    {
        _cardDiscovered = discovered;

        //Switch method to handle the state of the card
        switch (discovered)
        {
            case CardDiscovered.Discovered:
                //Debug.Log("Card is discovered");
                _spriteCadreDiscovered.enabled = true;
                break;
            case CardDiscovered.NotDiscovered:
                //Debug.Log("Card is not discovered");
                _spriteCadreDiscovered.enabled = false;
                break;
        }
    }
    
    private void _handleState()
    {
        //Switch method to handle the state of the card
        switch (_cardState)
        {
            case CardState.FaceUp:
                //Debug.Log("Card is face up");
                _cardAnimator?.SetTrigger("on_click_selected");
                _manager?.ReturnCard(_cardModel);
                break;
            case CardState.FaceDown:
                //Debug.Log("Card is face down");
                _cardAnimator?.SetTrigger("on_click_unselected");
                break;
        }
    }
    
    //Change color of the card
    public void ChangeColor()
    {
        //Depending on state of the card, change the color
        if (_cardState == CardState.FaceDown)
        {
            _spriteRenderer.sprite = _cardModel.backSprite;
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.sprite = _cardModel.frontSprite;
            _spriteRenderer.flipX = true;
        }
    }
}
