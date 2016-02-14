using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour {
    public float yCorrectionSpeed = 100;
    public float rotationSpeed = 1;
    public float bankFactor = 1;
    public float maxBank = 25;
    public float thrust = 100;

    public float drag = 1;
    public float angularDrag = 1;

    [HideInInspector]
    public Vector3 targetVelocity;
    

    void Update()
    {
        Rigidbody body = gameObject.GetComponent<Rigidbody>();
        
        Vector2 yawTransform = new Vector2(transform.forward.x, transform.forward.z);
        Vector2 yawTarget = new Vector2(targetVelocity.x, targetVelocity.z);

        float requiredYaw = Vector2.Angle(yawTransform, yawTarget) * Mathf.Sign(Vector3.Cross(yawTarget, yawTransform).z);

        body.AddForce(transform.forward * thrust);
        
        body.AddRelativeTorque(
            -(targetVelocity.normalized.y - transform.forward.y) * yCorrectionSpeed,
            0,
            Mathf.DeltaAngle(transform.rotation.eulerAngles.z, Mathf.Clamp(-requiredYaw * bankFactor, -maxBank, maxBank))
            );
        
        body.AddTorque(Vector3.up * requiredYaw * rotationSpeed);

        body.angularDrag = angularDrag;
        body.drag = drag;
    }
}
