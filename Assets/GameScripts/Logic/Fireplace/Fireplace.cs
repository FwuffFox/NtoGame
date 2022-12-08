using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts.Logic.Player;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private bool _particleIsPlayed;
    public FireplaceType Type;
    public Action OnFinalCampfireReached;


    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlayer(other)) return;
        if (Type == FireplaceType.Final) StartCoroutine(OnFinalReach());
        if (Type == FireplaceType.Checkpoint)
        {
            PlayerPrefs.SetFloat("CheckpointX", transform.position.x + 1);
            PlayerPrefs.SetFloat("CheckpointZ", transform.position.z + 1);
        }
        _particleIsPlayed = true;
        print("Play");
        _particle.Play();
    }

    private IEnumerator OnFinalReach()
    {
        yield return new WaitForSeconds(5);
        PlayerPrefs.SetFloat("CheckpointX", 0);
        PlayerPrefs.SetFloat("CheckpointZ", 0);
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

public enum FireplaceType
{
    Start,
    Checkpoint,
    Final
}
