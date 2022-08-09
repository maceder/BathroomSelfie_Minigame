using DG.Tweening;
using UnityEngine;


/// <summary>
/// RawBackgroundAnimationController arrowBox'dan yakaladýðým directionu tuttuðum yer
/// </summary>
public class RawBackgroundManager : MonoBehaviour
{
    [HideInInspector]
    public EnumSwipeDirection currentEnumDirection = EnumSwipeDirection.None;
    public RawBackgroundAnimationController rawBackgroundAnimationController;


    private int i = 0;

    private void OnEnable()
    {
        Message.AddListener<EnumSwipeDirection>(EventName.RawBacgroundArrowTouch, ArrowTouch);
    }

    private void OnDisable()
    {
        Message.RemoveListener<EnumSwipeDirection>(EventName.RawBacgroundArrowTouch, ArrowTouch);
    }


    private void ArrowTouch(EnumSwipeDirection enumSwipeDirection)
    {
        currentEnumDirection = enumSwipeDirection;
        if (i < 3)
        {
            rawBackgroundAnimationController.rawBackgroundKeepGoing = false;
            Vector3 pos = rawBackgroundAnimationController.rectTransform.position - new Vector3(130, 0, 0);
            rawBackgroundAnimationController.rectTransform.DOMove(pos, 1f);
        }
        i++;

        if (currentEnumDirection == EnumSwipeDirection.Finish)
        {
            rawBackgroundAnimationController.rawBackgroundKeepGoing = false;
            Message.Send(EventName.SetFinishAnimation);
        }
    }

    public void RawBackgroundIsPlay(bool swipeIsCorrectArrow)
    {
        if (swipeIsCorrectArrow)
            rawBackgroundAnimationController.rawBackgroundKeepGoing = true;

    }
}
