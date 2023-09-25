using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlimeStorageElement : MonoBehaviour
{

    private TMP_Text _text;
    private Image _image;

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
    }
}
