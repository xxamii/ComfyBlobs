using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDoor : MonoBehaviour
{
    public AudioClip _openSound;
    public List<Plate> _platesList;

    protected Animator _animator;
    protected AudioPlayer _audioPlayer;

    protected bool _isOpened;

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _audioPlayer = AudioPlayer.Instance;

        foreach (Plate plate in _platesList)
        {
            if (plate != null)
                plate.OnToggle += CheckPlatesStatus;
        }
    }

    public void PlayOpenSound()
    {
        _audioPlayer.PlayEffect(_openSound);
    }

    private void CheckPlatesStatus()
    {
        foreach (Plate plate in _platesList)
        {
            if (!plate.IsPressed)
            {
                if (_isOpened)
                {
                    Close();
                }

                return;
            }
        }

        if (!_isOpened)
        {
            Open();
        }
    }

    private void Close()
    {
        _isOpened = false;
        _animator.SetTrigger("close");
    }

    private void Open()
    {
        _isOpened = true;
        _animator.SetTrigger("open");
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Plate plate in _platesList)
        {
            if (plate != null)
                Gizmos.DrawLine(transform.position, plate.transform.position);
        }
    }

    private void OnDestroy()
    {
        foreach (Plate plate in _platesList)
        {
            if (plate != null)
                plate.OnToggle -= CheckPlatesStatus;
        }
    }
}
