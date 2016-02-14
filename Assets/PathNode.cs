using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {

    public GameObject nextTarget = null;
    public float radius = 25;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1);
        Gizmos.DrawWireSphere(transform.position, radius);
        if (nextTarget != null)
        {
            Vector3 offset = nextTarget.transform.position - transform.position;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + offset / 2);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + offset / 2, nextTarget.transform.position);
        }
	}
}
