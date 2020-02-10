using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void collisionIgnore()
    {
        Debug.Log("Trigger exit");
        var playerCollider = GameObject.Find("Player").GetComponent<Collider>();
        var colliders = playerCollider.gameObject.GetComponentsInChildren<Collider>();
        if (colliders.Length > 0)
        {
            Debug.Log("multiple colliders: " + colliders.Length);
        }
        //Debug.Log(other.gameObject.name);

        if (playerCollider.gameObject.tag == "Player")
        {            
            Physics.IgnoreCollision(playerCollider, gameObject.transform.parent.gameObject.GetComponent<Collider>(), false);
            Debug.Log("player collider found");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Invoke("collisionIgnore", 0f);
        
        //        Physics.IgnoreCollision(playerCollider, GetComponent<Collider>(), false);
    }
}
