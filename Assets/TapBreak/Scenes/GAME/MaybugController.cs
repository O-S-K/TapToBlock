using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class MaybugController : MonoBehaviour
{
    public static MaybugController instance;

    [SpineAnimation]
    public string m_Stand;
    [SpineAnimation]
    public string m_Happy;
    [SpineAnimation]
    public string m_Sad;
    public bool isEvent = false;

    SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState animationState;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        animationState = skeletonAnimation.AnimationState;
    }

    public void SetAnimationMaybug()
    {
        if (GameManager.instance.isWin)
        {
            animationState.SetAnimation(0, m_Happy, true);
        }
        else if (GameManager.instance.isLose)
        {
            animationState.SetAnimation(0, m_Sad, true);
        }
        else
        {
            animationState.SetAnimation(0, m_Stand, true);
        }
    }
}
