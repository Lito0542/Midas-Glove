using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandler : MonoBehaviour {

    public Transform PlayerTarget;
    public float tilt = 10;

    [Header("[Basic Setting]")]
    public Transform LeftWrist;
    public Transform RightWrist;

    Animator anim;

    public float ikWeight = 1;

    public Transform leftFootIKTarget;
    public Transform rightFootIKTarget;

    public Transform hintLeftFoot;
    public Transform hintRightFoot;

    [Space]
    [Header("[Hand]")]
    public Transform leftHandIKTarget;
    public Transform rightHandIKTarget;

    public Transform hintLeftHand;
    public Transform hintRightHand;

    public LayerMask GroundLayer;

    public float height = 1;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        transform.position = PlayerTarget.position + Vector3.down * 0.55f - PlayerTarget.forward * 0.15f;
        transform.eulerAngles = new Vector3(0, PlayerTarget.eulerAngles.y + tilt, 0);

        RaycastHit hit;
        if (Physics.Raycast(PlayerTarget.position + PlayerTarget.right * 0.05f - transform.forward * 0.2f + Vector3.down * 0.5f
            , Vector3.down, out hit, 5f, GroundLayer))
        {
            rightFootIKTarget.position = hit.point + Vector3.up * 0.1f;
            hintRightFoot.position = PlayerTarget.position - Vector3.up * 0.7f + PlayerTarget.right * 0.125f + PlayerTarget.forward * 0.5f;
        }
        if (Physics.Raycast(PlayerTarget.position - PlayerTarget.right * 0.05f - transform.forward * 0.2f + Vector3.down * 0.5f
            , Vector3.down, out hit, 5f, GroundLayer))
        {
            leftFootIKTarget.position = hit.point + Vector3.up * 0.1f;
            hintLeftFoot.position = PlayerTarget.position - Vector3.up * 0.7f - PlayerTarget.right * 0.125f + PlayerTarget.forward * 0.5f;
        }
        hintLeftHand.position = 
            PlayerTarget.position - PlayerTarget.forward * 0.3f - PlayerTarget.right * 0.45f;
        hintRightHand.position = 
            PlayerTarget.position - PlayerTarget.forward * 0.3f + PlayerTarget.right * 0.45f;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ikWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, ikWeight);

        anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootIKTarget.position);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootIKTarget.position);

        anim.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, ikWeight);
        anim.SetIKHintPositionWeight(AvatarIKHint.RightKnee, ikWeight);

        anim.SetIKHintPosition(AvatarIKHint.LeftKnee, hintLeftFoot.position);
        anim.SetIKHintPosition(AvatarIKHint.RightKnee, hintRightFoot.position);


        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, ikWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, ikWeight);

        anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootIKTarget.rotation);
        anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootIKTarget.rotation);


        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);

        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKTarget.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKTarget.position);

        anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, ikWeight);
        anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, ikWeight);

        anim.SetIKHintPosition(AvatarIKHint.LeftElbow, hintLeftHand.position);
        anim.SetIKHintPosition(AvatarIKHint.RightElbow, hintRightHand.position);

        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);

        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandIKTarget.rotation);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandIKTarget.rotation);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);

        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKTarget.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKTarget.position);
    }
}
