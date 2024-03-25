using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimation : EventSystem
{
    [SerializeField] private Animator _animator;
    public override void StartEvenement()
    {
        _animator.enabled = true;
    }
}
