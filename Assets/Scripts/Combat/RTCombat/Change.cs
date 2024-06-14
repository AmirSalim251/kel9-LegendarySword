using UnityEngine;

public class Change : MonoBehaviour
{
    void Start()
    {
        CharacterJoint[] characterJoints = GetComponentsInChildren<CharacterJoint>();
        
        foreach (CharacterJoint cj in characterJoints)
        {
            Transform parent = cj.transform.parent;
            
            // Create a new ConfigurableJoint
            ConfigurableJoint newJoint = cj.gameObject.AddComponent<ConfigurableJoint>();

            // Copy relevant properties from CharacterJoint to ConfigurableJoint
            newJoint.connectedBody = cj.connectedBody;
            newJoint.anchor = cj.anchor;
            newJoint.axis = cj.axis;
            newJoint.secondaryAxis = cj.swingAxis;

            // Setup ConfigurableJoint properties to mimic CharacterJoint behavior
            newJoint.xMotion = ConfigurableJointMotion.Locked;
            newJoint.yMotion = ConfigurableJointMotion.Locked;
            newJoint.zMotion = ConfigurableJointMotion.Locked;
            newJoint.angularXMotion = ConfigurableJointMotion.Limited;
            newJoint.angularYMotion = ConfigurableJointMotion.Limited;
            newJoint.angularZMotion = ConfigurableJointMotion.Limited;

            // Copy limits
            SoftJointLimitSpring twistLimitSpring = newJoint.angularXLimitSpring;
            twistLimitSpring.spring = cj.twistLimitSpring.spring;
            twistLimitSpring.damper = cj.twistLimitSpring.damper;
            newJoint.angularXLimitSpring = twistLimitSpring;

            SoftJointLimit lowAngularXLimit = newJoint.lowAngularXLimit;
            lowAngularXLimit.limit = cj.lowTwistLimit.limit;
            newJoint.lowAngularXLimit = lowAngularXLimit;

            SoftJointLimit highAngularXLimit = newJoint.highAngularXLimit;
            highAngularXLimit.limit = cj.highTwistLimit.limit;
            newJoint.highAngularXLimit = highAngularXLimit;

            SoftJointLimitSpring swingLimitSpring = newJoint.angularYZLimitSpring;
            swingLimitSpring.spring = cj.swingLimitSpring.spring;
            swingLimitSpring.damper = cj.swingLimitSpring.damper;
            newJoint.angularYZLimitSpring = swingLimitSpring;

            SoftJointLimit angularYLimit = newJoint.angularYLimit;
            angularYLimit.limit = cj.swing1Limit.limit;
            newJoint.angularYLimit = angularYLimit;

            SoftJointLimit angularZLimit = newJoint.angularZLimit;
            angularZLimit.limit = cj.swing2Limit.limit;
            newJoint.angularZLimit = angularZLimit;

            // Optionally, copy other properties like drive settings, if needed

            // Remove the old CharacterJoint
            Destroy(cj);
        }
    }
}
