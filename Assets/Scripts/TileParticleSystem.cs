using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileParticleSystem : MonoBehaviour
{
public ParticleSystem firstSystem;
public ParticleSystem secondSystem;

    void Start() {
        firstSystem.Play();
    }

    public void Burst() {
        secondSystem.Play();

        StartCoroutine(BurstRoutine());
    }

    public ParticleSystem GetMiningParticles() {
        return secondSystem;
    }

    public IEnumerator BurstRoutine() {
        yield return new WaitForSeconds(.2f);
        secondSystem.Stop();
    }

}
