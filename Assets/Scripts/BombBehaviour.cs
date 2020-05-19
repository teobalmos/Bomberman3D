using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem particleSystemPrefab;
    [SerializeField] public float explosionRadius;

    private List<Collider> playerColliders = new List<Collider>();

    public List<Collider> GetColliders()
    {
        return playerColliders;
    }

    private void Start()
    {
        var position = transform.position;
        var colliders = Physics.OverlapSphere(position, 0.2f);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                Debug.Log(collider);
                playerColliders.Add(collider);
                Physics.IgnoreCollision(collider, GetComponent<Collider>(), true);
            }
        }
        
        Debug.Log("Bomb placed");
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
        var particleSystem = Instantiate(particleSystemPrefab) as ParticleSystem;

        particleSystem.transform.position = transform.position;
        particleSystem.Play();

        Destroy(particleSystem.gameObject, particleSystem.main.duration);
    }
}
