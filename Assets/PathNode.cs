using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {

    public GameObject nextTarget = null;
    public float radius = 80;
    public float goodEnoughRadius = 160;
    public float goodEnoughTime = 2;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.Lerp(Color.green, Color.black, 0.5f);
        Gizmos.DrawWireSphere(transform.position, goodEnoughRadius);
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
