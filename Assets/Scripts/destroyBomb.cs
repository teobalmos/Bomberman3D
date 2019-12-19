using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyBomb : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem particleSystemPrefab;

    void OnDestroy(){

        ParticleSystem particleSystem = Instantiate(particleSystemPrefab) as ParticleSystem;

        particleSystem.transform.position = transform.position;
        particleSystem.Play();

        Destroy(particleSystem.gameObject, particleSystem.main.duration);

    }
}
