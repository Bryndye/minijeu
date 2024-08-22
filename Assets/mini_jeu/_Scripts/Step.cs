using System;
using UnityEngine;
using UnityEngine.UI;

public class Step : MonoBehaviour
{
    private Image _circle;
    private Image _border;

    private void Awake()
    {
        _circle = GetComponentInChildren<Image>();
        _border = GetComponent<Image>();
    }

    public void SetColor(Color color)
    {
        _circle.color = color;
    }
    
    public void SetBorder(Color color)
    {
        _border.color = color;
    }
    
    public void SetActiveBorder(bool state)
    {
        _border.enabled = state;
    }
}
