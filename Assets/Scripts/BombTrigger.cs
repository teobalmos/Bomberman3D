using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    public BombBehaviour bombbehaviour;
    
    private List<Collider> ignoredColliders = new List<Collider>();
    private void Start()
    {
        ignoredColliders = bombbehaviour.GetColliders();
    }

    private void collisionIgnore()
    {
        Debug.Log("Trigger exit");
        foreach (var collider in ignoredColliders)
        {
            Physics.IgnoreCollision(collider, gameObject.transform.parent.gameObject.GetComponent<Collider>(), false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Invoke("collisionIgnore", 0f);
    }
}
