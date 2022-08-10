using UnityEngine;

/// <summary>
/// SwipeDirector den fýrlatýlan isteðe göre iþlemerin yapýldýðý manager class
/// </summary>
public class SwipeManager : MonoBehaviour
{
    public RawBackgroundManager rawBackgroundManager;
    public PoseController poseController;

    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }


    //SwipeDetektör'den gelen datanýn kýyaslanmasý
    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        if (rawBackgroundManager.CompareSwipeDirection(data))
        {
            rawBackgroundManager.RawBackgroundIsPlay(true);
            rawBackgroundManager.CorrectSwipeAnimation();
            poseController.ChangePose(data.Direction);
        }
        else
        {
            rawBackgroundManager.WrongSwipeAnimation();
        }
    }
}
