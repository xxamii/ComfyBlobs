using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EjectSlot : MonoBehaviour
{
    private TMP_Text _text;
    private Image _image;
    private Eater _player;
    private CanvasGroup _canvasGroup;

    public string Text
    {
        get { return _text.text; }
        set { _text.text = value; }
    }

    public Sprite Sprite
    {
        get { return _image.sprite; }
        set { _image.sprite = value; }
    }


    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _image = GetComponentInChildren<Image>();
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    public void Start()
    {
        _player = FindObjectOfType<Eater>();
        _player.OnSplitWeightChange += UpdateView;

        UpdateView();
    }

    public void UpdateView()
    {
        
        if (_player.CurrentSplitWeight == 0)
            SetVisible(false);
        else
        {
            SetVisible(true);
            _text.text = _player.CurrentSplitWeight.ToString();
        }
    }

    private void SetVisible(bool state)
    {
        _canvasGroup.alpha = state ? 1 : 0;
    }

    public void OnDisable()
    {
        _player.OnSplitWeightChange -= UpdateView;
    }
}
