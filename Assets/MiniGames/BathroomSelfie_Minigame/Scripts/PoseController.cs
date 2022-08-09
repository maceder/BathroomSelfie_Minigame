using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// EnumSwipeDirection aldýðým directe göre pose ve photo
/// </summary>

public class PoseController : MonoBehaviour
{
    public List<GameObject> posePhoto;
    public Transform createAreaPosition;
    [HideInInspector]
    public List<GameObject> createdPhotos;

    private Animator womanAnimator;
    private GameObject currentPhoto;

    private void Start()
    {
        womanAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Message.AddListener(EventName.SetFinishAnimation, PhotoFinishAnimation);
    }

    private void OnDisable()
    {
        Message.RemoveListener(EventName.SetFinishAnimation, PhotoFinishAnimation);
    }

    public void ChangePose(EnumSwipeDirection enumSwipeDirection)
    {
        int i = Random.Range(-15, 30);
        switch (enumSwipeDirection)
        {
            case EnumSwipeDirection.Up:
                currentPhoto = Instantiate(posePhoto[0], createAreaPosition.transform.position, Quaternion.Euler(0, 0, i), createAreaPosition);
                createdPhotos.Add(currentPhoto);
                womanAnimator.SetTrigger("Pose_0");
                break;
            case EnumSwipeDirection.Down:
                currentPhoto = Instantiate(posePhoto[1], createAreaPosition.transform.position, Quaternion.Euler(0, 0, i), createAreaPosition);
                createdPhotos.Add(currentPhoto);
                womanAnimator.SetTrigger("Pose_1");
                break;
            case EnumSwipeDirection.Left:
                currentPhoto = Instantiate(posePhoto[2], createAreaPosition.transform.position, Quaternion.Euler(0, 0, i), createAreaPosition);
                createdPhotos.Add(currentPhoto);
                womanAnimator.SetTrigger("Pose_2");
                break;
            case EnumSwipeDirection.Right:
                currentPhoto = Instantiate(posePhoto[3], createAreaPosition.transform.position, Quaternion.Euler(0, 0, i), createAreaPosition);
                createdPhotos.Add(currentPhoto);
                womanAnimator.SetTrigger("Pose_3");
                break;
        }
    }

    private void PhotoFinishAnimation()
    {
        createAreaPosition.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-300, 0), 2f).OnComplete(() =>
        {
            foreach (var item in createdPhotos)
            {
                int posX = Random.Range(-120, 300);
                int posY = Random.Range(400, -200);
                item.GetComponent<RectTransform>().anchoredPosition = new Vector3(posX, posY);
            }
        });
        
    }
}
