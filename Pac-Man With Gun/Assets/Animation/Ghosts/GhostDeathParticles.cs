using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDeathParticles : MonoBehaviour {
    [SerializeField] ParticleSystem particleSystem;

    public void PlayParticles () {
        particleSystem.Play ();
    }
}
