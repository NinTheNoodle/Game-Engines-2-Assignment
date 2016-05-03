using UnityEngine;
using System.Collections;

public class BehaviourController : MonoBehaviour
{
    [HideInInspector]
    public GameObject target = null;

    private MonoBehaviour currentBehaviour = null;

    public T setBehaviour<T>() where T : MonoBehaviour
    {
        T component = gameObject.GetComponent<T>();

        if (component == null)
            component = gameObject.AddComponent<T>();
        
        if (component != currentBehaviour)
        {
            if (currentBehaviour != null)
                currentBehaviour.enabled = false;
            currentBehaviour = component;
            component.enabled = true;
        }

        return component;
    }

    void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, transform.position + (target.transform.position - transform.position) * 0.01f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + (target.transform.position - transform.position) * 0.01f, target.transform.position);
        }
    }
}
