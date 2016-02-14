using UnityEngine;
using System.Collections;

public class BehaviourController : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;

    public MonoBehaviour behaviour
    {
        get
        {
            return currentBehaviour;
        }
        set
        {
            if (currentBehaviour == value)
                return;

            if (currentBehaviour != null)
                currentBehaviour.enabled = false;
            value.enabled = true;
            currentBehaviour = value;
        }
    }

    private MonoBehaviour currentBehaviour = null;

    void Start()
    {
        gameObject.AddComponent<Seek>();
        gameObject.AddComponent<Flee>();
        gameObject.AddComponent<Path>();
    }

    void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
}
