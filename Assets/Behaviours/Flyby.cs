using UnityEngine;
using System.Collections;

public class Flyby : MonoBehaviour {
    public float swerveFactor = 3;
    public float swerveOuterDistance = 40;
    public float swerveInnerDistance = 10;
    public float overCorrect = 1;

    private Vector3 swerveTarget = Random.insideUnitSphere.normalized;

    void Update()
    {
        Fly fly = gameObject.GetComponent<Fly>();
        BehaviourController controller = gameObject.GetComponent<BehaviourController>();

        Vector3 velocity = controller.target.transform.position - transform.position;

        float correctionFactor = Mathf.Max(0, 45 - Vector3.Angle(transform.forward, Vector3.SlerpUnclamped(transform.forward, velocity, overCorrect))) / 45;
        velocity = Vector3.SlerpUnclamped(transform.forward, velocity, 1 + overCorrect * (correctionFactor));

        float swerveAmount = 1 - Mathf.Clamp((velocity.magnitude - swerveInnerDistance) / (swerveOuterDistance - swerveInnerDistance), 0, 1);

        if (swerveAmount == 0)
        {
            swerveTarget = transform.forward;
            swerveTarget.y = Mathf.Abs(swerveTarget.y);
        }
        
        fly.targetVelocity = Vector3.RotateTowards(velocity, swerveTarget, -swerveAmount * swerveFactor, 0f);
    }
}
