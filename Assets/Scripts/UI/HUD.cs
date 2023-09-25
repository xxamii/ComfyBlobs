using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI _text;

    private Eater _player;


    protected void Awake()
    {
        _player = FindObjectOfType<Eater>();
    }

    protected void Start()
    {
        _player.OnSplitWeightChange += DisplayCurrentSplitWeight;
    }

    public void DisplayCurrentSplitWeight()
    {
        _text.text = _player.GetCurrentWeight().ToString();
    }

    protected void OnDestroy()
    {
        _player.OnSplitWeightChange -= DisplayCurrentSplitWeight;
    }
}
