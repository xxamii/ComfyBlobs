using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimator : MonoBehaviour
{
    public float scaleTime = 1.0f;
    public float baseScale = 2.0f;
    public float scaleMultiplier = 1.0f;

    private Animator animator;
    public CharacterMovement _movement;
    public PlayerInput _input;

    public GameObject shadow;

    private Transform parentTransform;
    private Transform tr;

    private Vector3 startScale;    

    public void Awake()
    {
        tr = this.GetComponent<Transform>();
        parentTransform = tr.parent;
        animator = this.GetComponent<Animator>();
    }
    
    // Use to instantly set size
    public void SetScale(float size)
    {
        parentTransform.localScale = new Vector3(baseScale * scaleMultiplier, baseScale * scaleMultiplier, baseScale * scaleMultiplier);
    }

    // Use to set future size before the animation start
    public void PresetScale(float size)
    {
        _movement.CanMove = false;
        _input.CanInput = false;

        scaleMultiplier = size;
    }

    // Called from animation to start scale animation
    public void StartScaleAnimation()
    {        
        startScale = parentTransform.localScale;
        StartCoroutine(Scale(new Vector3(baseScale * scaleMultiplier, baseScale * scaleMultiplier, baseScale * scaleMultiplier)));
    }

    // Called from animation to control shadow
    public void ShowShadow()
    {
        shadow.SetActive(true);
    }

    // Called from animation to control shadow
    public void HideShadow()
    {
        shadow.SetActive(false);
    }

    public IEnumerator Scale(Vector3 endSize)
    {
        float t = 0.0f;
        while (t < scaleTime)
        {
            t += Time.deltaTime;
            parentTransform.localScale = Vector3.Lerp(startScale, endSize, Mathf.SmoothStep(0.0f, 1.0f, t / scaleTime));
            yield return null;
        }
        parentTransform.localScale = endSize;

        _movement.CanMove = true;
        _input.CanInput = true;
    }

    public void PlaySuckAnimation()
    {
        SetAnimationTrigger("suck");
    }

    public void PlaySplitAnimation()
    {
        SetAnimationTrigger("split");
    }

    public void PlaySplitedAnimation()
    {
        SetAnimationTrigger("splited");
    }

    public void PlaySuckedAnimation()
    {
        SetAnimationTrigger("sucked");
    }

    public void SetAnimationTrigger(string name)
    {
        animator.SetTrigger(name);
    }

    public void SetAnimationBool(string name, bool state)
    {
        animator.SetBool(name, state);
    }
}
