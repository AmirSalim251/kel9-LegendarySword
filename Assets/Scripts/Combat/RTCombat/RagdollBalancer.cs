using UnityEngine;

public class RagdollBalancer: MonoBehaviour
{
    public Transform upperBody; // The main part of the body to keep upright
    public float balanceForce = 500f;
    public float balanceDamping = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = upperBody.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        BalanceRagdoll();
    }

    private void BalanceRagdoll()
    {
        if (upperBody == null)
            return;

        // Calculate the target rotation to keep the upper body upright
        Quaternion targetRotation = Quaternion.Euler(0, upperBody.rotation.eulerAngles.y, 0);

        // Calculate the current rotation difference
        Quaternion currentRotation = upperBody.rotation;
        Quaternion deltaRotation = targetRotation * Quaternion.Inverse(currentRotation);

        // Calculate the torque required to achieve the target rotation
        Vector3 torque = new Vector3(deltaRotation.x, deltaRotation.y, deltaRotation.z) * balanceForce;

        // Apply damping to the torque to avoid oscillations
        torque -= rb.angularVelocity * balanceDamping;

        // Apply the calculated torque to the rigidbody
        rb.AddTorque(torque);
    }
}
