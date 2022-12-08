using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts.Logic.Player;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private bool _particleIsPlayed;
    public bool IsFinal;
    public Action OnFinalCampfireReached;
    

    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlayer(other)) return;
        if (IsFinal) StartCoroutine(OnFinalReach());
        _particleIsPlayed = true;
        print("Play");
        _particle.Play();
    }

    private IEnumerator OnFinalReach()
    {
        yield return new WaitForSeconds(5);
        OnFinalCampfireReached?.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!IsPlayer(other)) return;
        if (!_particleIsPlayed)
        {
            print("Play");
            _particleIsPlayed = true;
            _particle.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsPlayer(other)) return;
        _particleIsPlayed = false;
        print("Stop");
        _particle.Stop();
    }

    private bool IsPlayer(Collider collider)
    {
        return collider.TryGetComponent<PlayerMovement>(out _);
    }
}
