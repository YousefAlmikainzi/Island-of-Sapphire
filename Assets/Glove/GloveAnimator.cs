using UnityEngine;
using NaughtyAttributes;
using NUnit.Framework;
public class GloveAnimator : MonoBehaviour
{
    public GameObject rightGlove;
    public GameObject leftGlove;

    public float transitionAnimTime;
    public float idleAnimTime;
    public float idleStartY;
    public float idleEndY;

    public float walkAnimTime;
    public float pushAnimTime;
    public float grabAnimTime;
    public float throwAnimTime;

    public Vector3 walkStartPosR;
    public Vector3 walkEndPosR;
    public Vector3 walkStartPosL;
    public Vector3 walkEndPosL;
    public Quaternion walkStartRotR;
    public Quaternion walkEndRotR;
    public Quaternion walkStartRotL;
    public Quaternion walkEndRotL;

    public Vector3 pushStartPosR;
    public Vector3 pushEndPosR;
    public Vector3 pushStartPosL;
    public Vector3 pushEndPosL;
    public Quaternion pushStartRotR;
    public Quaternion pushEndRotR;
    public Quaternion pushStartRotL;
    public Quaternion pushEndRotL;

    public Vector3 grabStartPosR;
    public Vector3 grabEndPosR;
    public Vector3 grabStartPosL;
    public Vector3 grabEndPosL;
    public Quaternion grabStartRotR;
    public Quaternion grabEndRotR;
    public Quaternion grabStartRotL;
    public Quaternion grabEndRotL;

    public Vector3 throwStartPosR;
    public Vector3 throwEndPosR;
    public Vector3 throwStartPosL;
    public Vector3 throwEndPosL;
    public Quaternion throwStartRotR;
    public Quaternion throwEndRotR;
    public Quaternion throwStartRotL;
    public Quaternion throwEndRotL;

    public Vector3 originPosR;
    public Quaternion originRotR;
    public Vector3 originPosL;
    public Quaternion originRotL;

    public Quaternion rightPalmUpRotation;

    PrimeTween.Tween currentPosTweenR;
    PrimeTween.Tween currentRotTweenR;
    PrimeTween.Tween currentPosTweenL;
    PrimeTween.Tween currentRotTweenL;

    public bool is_grabbing = false;

    enum GloveAnimLoopType { NONE, ONE_TIME_ANIM, IDLE, WALK, GRAB_IDLE, GRAB_WALK };
    GloveAnimLoopType currentLoop = GloveAnimLoopType.NONE;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //originPos = leftGlove.transform.localPosition;
        //originRot = leftGlove.transform.localRotation;

        IdleLoop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Button]
    public void StopAllTweens()
    {
        currentPosTweenR.Stop();
        currentRotTweenR.Stop();
        currentPosTweenL.Stop();
        currentRotTweenL.Stop();
    }

    [Button]
    public void RecordWalkStart()
    {
        walkStartPosR = rightGlove.transform.localPosition;
        walkStartPosL = leftGlove.transform.localPosition;
        walkStartRotR = rightGlove.transform.localRotation;
        walkStartRotL = leftGlove.transform.localRotation;
    }

    [Button]
    public void RecordWalkEnd()
    {
        walkEndPosR = rightGlove.transform.localPosition;
        walkEndPosL = leftGlove.transform.localPosition;
        walkEndRotR = rightGlove.transform.localRotation;
        walkEndRotL = leftGlove.transform.localRotation;
    }

    [Button]
    public void RecordPushStart()
    {
        pushStartPosR = rightGlove.transform.localPosition;
        pushStartPosL = leftGlove.transform.localPosition;
        pushStartRotR = rightGlove.transform.localRotation;
        pushStartRotL = leftGlove.transform.localRotation;
    }

    [Button]
    public void RecordPushEnd()
    {
        pushEndPosR = rightGlove.transform.localPosition;
        pushEndPosL = leftGlove.transform.localPosition;
        pushEndRotR = rightGlove.transform.localRotation;
        pushEndRotL = leftGlove.transform.localRotation;
    }

    [Button]
    public void RecordGrabStart()
    {
        grabStartPosR = rightGlove.transform.localPosition;
        grabStartPosL = leftGlove.transform.localPosition;
        grabStartRotR = rightGlove.transform.localRotation;
        grabStartRotL = leftGlove.transform.localRotation;
    }

    [Button]
    public void RecordGrabEnd()
    {
        grabEndPosR = rightGlove.transform.localPosition;
        grabEndPosL = leftGlove.transform.localPosition;
        grabEndRotR = rightGlove.transform.localRotation;
        grabEndRotL = leftGlove.transform.localRotation;
    }

    [Button]
    public void RecordThrowStart()
    {
        throwStartPosR = rightGlove.transform.localPosition;
        throwStartPosL = leftGlove.transform.localPosition;
        throwStartRotR = rightGlove.transform.localRotation;
        throwStartRotL = leftGlove.transform.localRotation;
    }

    [Button]
    public void RecordThrowEnd()
    {
        throwEndPosR = rightGlove.transform.localPosition;
        throwEndPosL = leftGlove.transform.localPosition;
        throwEndRotR = rightGlove.transform.localRotation;
        throwEndRotL = leftGlove.transform.localRotation;
    }

    [Button]
    public void RecordOrigin()
    {
        originPosR = rightGlove.transform.localPosition;
        originPosL = leftGlove.transform.localPosition;
        originRotR = rightGlove.transform.localRotation;
        originRotL = leftGlove.transform.localRotation;
    }

    [Button]
    public void ResetToOrigin()
    {
        rightGlove.transform.localPosition = originPosR;
        leftGlove.transform.localPosition = originPosL;
        rightGlove.transform.localRotation = originRotR;
        leftGlove.transform.localRotation = originRotL;
    }

    [Button]
    public void ResetToGrabStart()
    {
        rightGlove.transform.localPosition = grabStartPosR;
        leftGlove.transform.localPosition = grabStartPosL;
        rightGlove.transform.localRotation = grabStartRotR;
        leftGlove.transform.localRotation = grabStartRotL;
    }

    public void AnimResetToOrigin()
    {
        currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, originPosR, transitionAnimTime, ease: PrimeTween.Ease.Linear);
        currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, originRotR, transitionAnimTime, ease: PrimeTween.Ease.Linear);

        currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, originPosL, transitionAnimTime, ease: PrimeTween.Ease.Linear);
        currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, originRotL, transitionAnimTime, ease: PrimeTween.Ease.Linear);
    }

    [Button]
    public void IdleLoop()
    {
        if (currentLoop == GloveAnimLoopType.IDLE || currentLoop == GloveAnimLoopType.ONE_TIME_ANIM) return;
        currentLoop = GloveAnimLoopType.IDLE;

        StopAllTweens();

        Vector3 targetR = originPosR;
        targetR.y = idleStartY;
        Vector3 targetL = originPosL;
        targetL.y = idleEndY;

        currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, originRotR, transitionAnimTime, ease: PrimeTween.Ease.Linear);

        currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, targetR, transitionAnimTime, ease: PrimeTween.Ease.Linear).OnComplete(() => {
            currentPosTweenR = PrimeTween.Tween.LocalPositionY(rightGlove.transform, idleEndY, idleAnimTime, cycles: -1, cycleMode: PrimeTween.CycleMode.Yoyo, ease: PrimeTween.Ease.InOutSine);
        }
        );

        currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, originRotL, transitionAnimTime, ease: PrimeTween.Ease.Linear);

        currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, targetL, transitionAnimTime, ease: PrimeTween.Ease.Linear).OnComplete(() => {
            currentPosTweenL = PrimeTween.Tween.LocalPositionY(leftGlove.transform, idleStartY, idleAnimTime, cycles: -1, cycleMode: PrimeTween.CycleMode.Yoyo, ease: PrimeTween.Ease.InOutSine);
        }
        );
    }

    [Button]
    public void WalkLoop()
    {
        if (currentLoop == GloveAnimLoopType.WALK || currentLoop == GloveAnimLoopType.ONE_TIME_ANIM) return;
        currentLoop = GloveAnimLoopType.WALK;

        StopAllTweens();

        currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, walkStartPosL, transitionAnimTime, ease: PrimeTween.Ease.Linear);
        currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, walkStartRotL, transitionAnimTime, ease: PrimeTween.Ease.Linear).OnComplete(
            () => {
                currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, walkEndPosL, walkAnimTime, ease: PrimeTween.Ease.InOutSine, cycles: -1, cycleMode: PrimeTween.CycleMode.Yoyo);
                currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, walkEndRotL, walkAnimTime, ease: PrimeTween.Ease.InOutSine, cycles: -1, cycleMode: PrimeTween.CycleMode.Yoyo);
            }
        );

        currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, walkEndPosR, transitionAnimTime, ease: PrimeTween.Ease.Linear);
        currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, walkEndRotR, transitionAnimTime, ease: PrimeTween.Ease.Linear).OnComplete(
            () => {
                currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, walkStartPosR, walkAnimTime, ease: PrimeTween.Ease.InOutSine, cycles: -1, cycleMode: PrimeTween.CycleMode.Yoyo);
                currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, walkStartRotR, walkAnimTime, ease: PrimeTween.Ease.InOutSine, cycles: -1, cycleMode: PrimeTween.CycleMode.Yoyo);
            }
        );
    }

    [Button]
    public void PlayPush()
    {
        currentLoop = GloveAnimLoopType.ONE_TIME_ANIM;

        StopAllTweens();

        currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, pushStartPosR, transitionAnimTime, ease: PrimeTween.Ease.InSine);
        currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, pushStartRotR, transitionAnimTime, ease: PrimeTween.Ease.InSine).OnComplete(
            () =>
            {
                currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, pushEndPosR, pushAnimTime, ease: PrimeTween.Ease.InSine);
                currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, pushEndRotR, pushAnimTime, ease: PrimeTween.Ease.InSine);
            }
        );

        currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, pushStartPosL, transitionAnimTime, ease: PrimeTween.Ease.InSine);
        currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, pushStartRotL, transitionAnimTime, ease: PrimeTween.Ease.InSine).OnComplete(
            () =>
            {
                currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, pushEndPosL, pushAnimTime, ease: PrimeTween.Ease.InSine);
                currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, pushEndRotL, pushAnimTime, ease: PrimeTween.Ease.InSine).OnComplete(() => currentLoop = GloveAnimLoopType.NONE);
            }
        );
    }

    [Button]
    public void PlayGrab()
    {
        currentLoop = GloveAnimLoopType.ONE_TIME_ANIM;

        StopAllTweens();

        currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, grabStartPosR, transitionAnimTime, ease: PrimeTween.Ease.InSine);
        currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, grabStartRotR, transitionAnimTime, ease: PrimeTween.Ease.InSine).OnComplete(
            () =>
            {
                currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, grabEndPosR, grabAnimTime, ease: PrimeTween.Ease.InSine);
                currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, grabEndRotR, grabAnimTime, ease: PrimeTween.Ease.InSine);
            }
        );

        currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, grabStartPosL, transitionAnimTime, ease: PrimeTween.Ease.InSine);
        currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, grabStartRotL, transitionAnimTime, ease: PrimeTween.Ease.InSine).OnComplete(
            () =>
            {
                currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, grabEndPosL, grabAnimTime, ease: PrimeTween.Ease.InSine);
                currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, grabEndRotL, grabAnimTime, ease: PrimeTween.Ease.InSine).OnComplete(() => currentLoop = GloveAnimLoopType.NONE);
            }
        );
    }

    [Button]
    public void GrabIdleLoop()
    {
        if (currentLoop == GloveAnimLoopType.GRAB_IDLE || currentLoop == GloveAnimLoopType.ONE_TIME_ANIM) return;
        currentLoop = GloveAnimLoopType.GRAB_IDLE;

        StopAllTweens();

        Vector3 targetR = originPosR;
        targetR.y = idleStartY;
        Vector3 targetL = originPosL;
        targetL.y = idleEndY;

        currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, rightPalmUpRotation, transitionAnimTime, ease: PrimeTween.Ease.Linear);

        currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, targetR, transitionAnimTime, ease: PrimeTween.Ease.Linear).OnComplete(() => {
            currentPosTweenR = PrimeTween.Tween.LocalPositionY(rightGlove.transform, idleEndY, idleAnimTime, cycles: -1, cycleMode: PrimeTween.CycleMode.Yoyo, ease: PrimeTween.Ease.InOutSine);
        }
        );

        currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, originRotL, transitionAnimTime, ease: PrimeTween.Ease.Linear);

        currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, targetL, transitionAnimTime, ease: PrimeTween.Ease.Linear).OnComplete(() => {
            currentPosTweenL = PrimeTween.Tween.LocalPositionY(leftGlove.transform, idleStartY, idleAnimTime, cycles: -1, cycleMode: PrimeTween.CycleMode.Yoyo, ease: PrimeTween.Ease.InOutSine);
        }
        );
    }

    [Button]
    public void GrabWalkLoop()
    {
        if (currentLoop == GloveAnimLoopType.GRAB_WALK || currentLoop == GloveAnimLoopType.ONE_TIME_ANIM) return;
        currentLoop = GloveAnimLoopType.GRAB_WALK;

        StopAllTweens();

        currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, walkStartPosL, transitionAnimTime, ease: PrimeTween.Ease.Linear);
        currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, walkStartRotL, transitionAnimTime, ease: PrimeTween.Ease.Linear).OnComplete(
            () => {
                currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, walkEndPosL, walkAnimTime, ease: PrimeTween.Ease.InOutSine, cycles: -1, cycleMode: PrimeTween.CycleMode.Yoyo);
                currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, walkEndRotL, walkAnimTime, ease: PrimeTween.Ease.InOutSine, cycles: -1, cycleMode: PrimeTween.CycleMode.Yoyo);
            }
        );

        currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, walkEndPosR, transitionAnimTime, ease: PrimeTween.Ease.Linear);
        currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, rightPalmUpRotation, transitionAnimTime, ease: PrimeTween.Ease.Linear).OnComplete(
            () => {
                currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, walkStartPosR, walkAnimTime, ease: PrimeTween.Ease.InOutSine, cycles: -1, cycleMode: PrimeTween.CycleMode.Yoyo);
                currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, rightPalmUpRotation, walkAnimTime, ease: PrimeTween.Ease.InOutSine, cycles: -1, cycleMode: PrimeTween.CycleMode.Yoyo);
            }
        );
    }

    [Button]
    public void PlayThrow()
    {
        currentLoop = GloveAnimLoopType.ONE_TIME_ANIM;

        StopAllTweens();

        currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, throwStartPosR, transitionAnimTime, ease: PrimeTween.Ease.Linear);
        currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, throwStartRotR, transitionAnimTime, ease: PrimeTween.Ease.Linear).OnComplete(
            () =>
            {
                currentPosTweenR = PrimeTween.Tween.LocalPosition(rightGlove.transform, throwEndPosR, throwAnimTime, ease: PrimeTween.Ease.InOutBack);
                currentRotTweenR = PrimeTween.Tween.LocalRotation(rightGlove.transform, throwEndRotR, throwAnimTime / 2, ease: PrimeTween.Ease.Linear);
            }
        );

        currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, throwStartPosL, transitionAnimTime, ease: PrimeTween.Ease.Linear);
        currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, throwStartRotL, transitionAnimTime, ease: PrimeTween.Ease.Linear).OnComplete(
            () =>
            {
                currentPosTweenL = PrimeTween.Tween.LocalPosition(leftGlove.transform, throwEndPosL, throwAnimTime, ease: PrimeTween.Ease.Linear);
                currentRotTweenL = PrimeTween.Tween.LocalRotation(leftGlove.transform, throwEndRotL, throwAnimTime, ease: PrimeTween.Ease.Linear).OnComplete(()=> currentLoop = GloveAnimLoopType.NONE);
            }
        );
    }

    public void gIdleLoop()
    {
        if (is_grabbing)
        {
            GrabIdleLoop();
        }
        else
        {
            IdleLoop();
        }
    }

    public void gWalkLoop()
    {
        if (is_grabbing)
        {
            GrabWalkLoop();
        }
        else
        {
            WalkLoop();
        }
    }
}