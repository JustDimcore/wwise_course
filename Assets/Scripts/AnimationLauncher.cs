﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationLauncher : ClickableObject
{
    public enum PlayingMode
    {
        SingleTime,
        PerClick,
        Switch,
    }

    [SerializeField] private Animation _animator;
    [SerializeField] private List<AkEvent> _soundEvents = new List<AkEvent>();
    [SerializeField] private PlayingMode _mode = PlayingMode.PerClick;
    [SerializeField] private UnityEvent _onAnimationFinished;

    private bool _played;
    private int _index;
    private bool _started;

    private void Reset()
    {
        if (_soundEvents.Count == 0)
            _soundEvents.AddRange(GetComponents<AkEvent>());
        if (_animator == null)
            _animator = GetComponent<Animation>();
    }

    public override void OnClick()
    {
        if (_animator.isPlaying)
            return;

        if (_mode == PlayingMode.SingleTime)
        {
            if (_played)
                return;

            _played = true;
        }

        if (_mode == PlayingMode.Switch)
        {
            int counter = 0;
            foreach (AnimationState state in _animator)
            {
                if (counter == _index)
                {
                    _animator.clip = state.clip;
                    break;
                }

                counter++;
            }

            _index++;
            if (_index >= _animator.GetClipCount())
                _index = 0;
        }

        _animator.Play();
        if (_soundEvents.Count > 0)
            _soundEvents[_index % _soundEvents.Count].HandleEvent(null);
    }
    
    private void Update()
    {
        if (_animator.isPlaying)
        {
            _started = true;
            return;
        }

        if (_started)
            _onAnimationFinished?.Invoke();
    }
}