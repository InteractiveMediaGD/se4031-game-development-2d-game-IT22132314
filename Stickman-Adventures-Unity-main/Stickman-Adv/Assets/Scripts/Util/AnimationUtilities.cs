using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationUtilities
{

    public static float GetAnimationLength(Animator animator)
    {
        AnimatorClipInfo[] animationInfo = animator.GetCurrentAnimatorClipInfo(0);
        return animationInfo[0].clip.length;
    }
}
