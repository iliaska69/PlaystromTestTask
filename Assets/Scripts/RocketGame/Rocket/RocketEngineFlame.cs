using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngineFlame : MonoBehaviour
{

    [SerializeField] private ParticleSystem rocketFlameParticleSystem;

    private bool _activeFlameStatus;
    
    void Update()
    {
        HandleScreenTap();
    }

    private void HandleScreenTap()
    {
        if (InputManager.instance.GetScreenTapStatus() && !_activeFlameStatus)
        {
            rocketFlameParticleSystem.Play();
            _activeFlameStatus = true;
        }
        else if (!InputManager.instance.GetScreenTapStatus() && _activeFlameStatus)
        {
            rocketFlameParticleSystem.Stop();
            _activeFlameStatus = false;
        }
    }
}
