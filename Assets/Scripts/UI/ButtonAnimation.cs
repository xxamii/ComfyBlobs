using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    public GameObject text;
    public float shift = 0.3f;

    private AudioPlayer ap;

    // Start is called before the first frame update
    void Start()
    {
        ap = AudioPlayer.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.transform.localPosition = new Vector3(0, shift, 0);
        if (ap.buttonHoverClip != null)
            ap.PlayEffect(ap.buttonHoverClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.transform.localPosition = new Vector3(0, 0, 0);        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        text.transform.localPosition = new Vector3(0, -shift / 3, 0);
        if (ap.buttonClickClip != null)
            ap.PlayEffect(ap.buttonClickClip);
    }
}
