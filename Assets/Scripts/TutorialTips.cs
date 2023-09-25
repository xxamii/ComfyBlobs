using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTips : MonoBehaviour
{

    public GameObject tip;
    public TutorialTips nextTip;
    public bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered)
        {
            PlayerInput input = other.GetComponent<PlayerInput>();
            CharacterMovement movement = other.GetComponent<CharacterMovement>();

            if (input && movement)
            {
                isTriggered = true;
                // show tooptip
                tip.SetActive(true);
                // activate next trigger
                if (nextTip != null)
                {
                    nextTip.gameObject.SetActive(true);
                }
            }
        }
    }

}
