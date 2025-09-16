using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;

public class HandGestureDistanceSwitche : MonoBehaviour
{
    [SerializeField] private XRHandSubsystem handSubsystem;

    void Update()
    {
        if (handSubsystem == null)
        {
            handSubsystem = XRGeneralSettings.Instance
                .Manager
                .activeLoader
                .GetLoadedSubsystem<XRHandSubsystem>();
            return;
        }

        XRHand leftHand = handSubsystem.leftHand;
        if (!leftHand.isTracked) return;

        if (IsPinching(leftHand))
        {
            Debug.Log("Pinch detected!");
        }
    }

    bool IsPinching(XRHand hand)
    {
        // ✅ Use GetJoint instead of TryGetJoint
        XRHandJoint indexTip = hand.GetJoint(XRHandJointID.IndexTip);
        XRHandJoint thumbTip = hand.GetJoint(XRHandJointID.ThumbTip);

        Pose indexPose, thumbPose;

        if (!indexTip.TryGetPose(out indexPose)) return false;
        if (!thumbTip.TryGetPose(out thumbPose)) return false;

        float distance = Vector3.Distance(indexPose.position, thumbPose.position);
        return distance < 0.02f; // pinch threshold
    }
}
