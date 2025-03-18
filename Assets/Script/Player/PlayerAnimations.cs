using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Player3rdPersonMovement animationData;
    [SerializeField]private Animator animator;

    void Start()
    {
        if(animationData!=null) animationData.speedChanges+=SpeedParameterChages;
        PlayerInput.Instance.OnCrouch+=CrouchAnimation;
        PlayerInput.Instance.OnAttack+=AttackAnimation;
    }

    void OnDisable()
    {
        if (animationData != null) animationData.speedChanges-=SpeedParameterChages;
        PlayerInput.Instance.OnAttack-=AttackAnimation;
        PlayerInput.Instance.OnCrouch-=CrouchAnimation;       
    }
    void SpeedParameterChages(float speed)
    {
        animator.SetFloat("speed",speed);
    }
    void CrouchAnimation (bool isCrouching)
    {
        animator.SetBool("isCrouching",isCrouching);
    }
    void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
}
