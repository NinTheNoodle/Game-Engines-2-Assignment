using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour {
    public float yCorrectionSpeed = 100;
    public float rotationSpeed = 1;
    public float bankFactor = 1;
    public float maxBank = 25;
    public float thrust = 100;
    public float maxLift = 40;
    public float maxLiftSpeed = 20;
    public float stallingFactor = 2.2f;

    [Range(0, 1)]
    public float stallPoint = 0.1f;

    [Range(0, 1)]
    public float unstallPoint = 0.85f;

    [Range(0, 1)]
    public float stallAversion = 0.1f;

    [Range(0, 1)]
    public float minumumTurnSpeed = 0.5f;

    [Range(0, 1)]
    public float fullTurnSpeed = 0.7f;

    public float drag = 0.5f;
    public float angularDrag = 0.5f;

    [HideInInspector]
    public Vector3 targetVelocity;

    private bool stalled = false;
    

    void Update()
    {
        targetVelocity.Normalize();
        Rigidbody body = gameObject.GetComponent<Rigidbody>();

        float liftSpeed = Mathf.Min(1, Vector3.Project(body.velocity, transform.forward).magnitude / maxLiftSpeed);
        float liftPitch = Mathf.Pow(Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.x), stallingFactor);

        if (!stalled)
        {
            Vector2 yawTransform = new Vector2(transform.forward.x, transform.forward.z);
            Vector2 yawTarget = new Vector2(targetVelocity.x, targetVelocity.z);

            float requiredYaw = Vector2.Angle(yawTransform, yawTarget) * Mathf.Sign(Vector3.Cross(yawTarget, yawTransform).z);
            float lift = liftPitch * liftSpeed * maxLift;

            if (targetVelocity.y < 0)
                lift = 0;

            requiredYaw *= Mathf.Clamp((liftSpeed - minumumTurnSpeed) / (fullTurnSpeed - minumumTurnSpeed), 0, 1);

            body.AddForce(transform.forward * thrust * liftPitch + transform.up * lift);

            float y_offset = Mathf.Clamp(targetVelocity.y - transform.forward.y, stallAversion - stallPoint, stallPoint - stallAversion);

            body.AddRelativeTorque(
                -y_offset * yCorrectionSpeed * liftPitch,
                -Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z) * rotationSpeed * liftPitch,
                Mathf.DeltaAngle(transform.rotation.eulerAngles.z, Mathf.Clamp(-requiredYaw * bankFactor * liftPitch, -maxBank, maxBank))
                );

            body.angularDrag = angularDrag * liftPitch * liftSpeed;
            body.drag = drag * liftPitch;

            if (liftPitch < stallPoint)
                stalled = true;
        }
        else
        {
            transform.FindChild("Burning").gameObject.SetActive(true);
            body.angularDrag = 0.05f;
            body.drag = 0;

            if (liftSpeed > unstallPoint && liftPitch > stallPoint)
                stalled = false;
        }
    }
}
