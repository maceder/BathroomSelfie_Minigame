using System;
using UnityEngine;



/// <summary>
/// Ekrandaki kaydýrma haraketinin yönünü belirliyor
/// </summary>
public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 10f;

    public static event Action<SwipeData> OnSwipe = delegate { };


   

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if(!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }

            if(touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    
    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? EnumSwipeDirection.Up : EnumSwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? EnumSwipeDirection.Right : EnumSwipeDirection.Left;
                SendSwipe(direction);
            }
        }
        fingerUpPosition = fingerDownPosition;
    }

    //Vertical ve horizontalýn kýyaslandýðý ve minDistanceForSwip'la kýyaslandýðý
    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    //Parmaðý dokunduðum ile kaldýrdýðým yer arasýndaki uzaklýðýn hesaplandýðý yer
    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    

    private void SendSwipe(EnumSwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
        };
        OnSwipe(swipeData);
    }
}

public struct SwipeData
{
    public EnumSwipeDirection Direction;
}


