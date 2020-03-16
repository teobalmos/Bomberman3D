using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem particleSystemPrefab;

    [SerializeField] public float explosionRadius;

    private void Start(){
        // need to change the way it ignores collision with the player who placed the bomb
        var playerCollider = GameObject.Find("Player").GetComponent<Collider>();
        Physics.IgnoreCollision(playerCollider, GetComponent<Collider>(), true);
    }

    private void OnDestroy(){
        playParticles();

        destroyObjects();
    }

    void destroyObjects()
    {
        var directions = new Vector3[]{Vector3.back, Vector3.forward, Vector3.left, Vector3.right};

        foreach (var direction in directions)
        {
            RaycastHit hit;
            if (Physics.Raycast(GetComponent<Transform>().position, direction, out hit, explosionRadius))
            {
                if (hit.transform.tag == "Crate")
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    void playParticles()
    {
        ParticleSystem particleSystem = Instantiate(particleSystemPrefab) as ParticleSystem;

        particleSystem.transform.position = transform.position;
        particleSystem.Play();

        Destroy(particleSystem.gameObject, particleSystem.main.duration);
    }
    
}
