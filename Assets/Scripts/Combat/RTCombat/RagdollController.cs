using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody[] ragdollRigidbodies;
    public Collider[] ragdollColliders;
    public float transitionDuration = 1.0f;

    private bool isRagdollActive = false;
    private Dictionary<Transform, Pose> ragdollPoses = new Dictionary<Transform, Pose>();

    private void Start()
    {
        // Disable ragdoll at start
        SetRagdollActive(false);
    }

    private void Update()
    {
        
    }

    public void RagdollOn()
    {
        StartCoroutine(SmoothTransitionToRagdoll());
    }

    public void RagdollOff()
    {
        StartCoroutine(SmoothTransitionToAnimation());
    }

    private void SetRagdollActive(bool active)
    {
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = !active;
            if (!active)
            {
                rb.velocity = Vector3.zero; // Reset velocity to prevent launch
                rb.angularVelocity = Vector3.zero; // Reset angular velocity to prevent spinning
            }
        }

        foreach (Collider col in ragdollColliders)
        {
            col.enabled = active;
        }

        animator.enabled = !active;
        isRagdollActive = active;
    }

    private IEnumerator SmoothTransitionToRagdoll()
    {
        float elapsedTime = 0.0f;

        // Ensure animator is not completely turned off immediately
        animator.enabled = true;

        while (elapsedTime < transitionDuration)
        {
            // Gradually blend animation influence to zero
            float blend = elapsedTime / transitionDuration;
            animator.SetFloat("RagdollBlend", 1 - blend);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Fully enable ragdoll mode
        SetRagdollActive(true);
    }

    private IEnumerator SmoothTransitionToAnimation()
    {
        // Capture ragdoll poses
        CaptureRagdollPose();

        // Set ragdoll to kinematic to avoid further physics influence
        SetRagdollActive(false);

        // Re-enable animator
        animator.enabled = true;

        float elapsedTime = 0.0f;

        while (elapsedTime < transitionDuration)
        {
            float blend = elapsedTime / transitionDuration;

            // Blend ragdoll pose back to animation
            foreach (var kvp in ragdollPoses)
            {
                Transform bone = kvp.Key;
                Pose pose = kvp.Value;

                bone.localPosition = Vector3.Lerp(pose.position, bone.localPosition, blend);
                bone.localRotation = Quaternion.Lerp(pose.rotation, bone.localRotation, blend);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void CaptureRagdollPose()
    {
        ragdollPoses.Clear();

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            Transform bone = rb.transform;
            ragdollPoses[bone] = new Pose(bone.localPosition, bone.localRotation);
        }
    }

    private struct Pose
    {
        public Vector3 position;
        public Quaternion rotation;

        public Pose(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }
}
