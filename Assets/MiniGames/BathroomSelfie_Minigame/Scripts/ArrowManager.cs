using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ArrowManager : MonoBehaviour
{
    private void OnEnable()
    {
        Message.AddListener<GameObject>(EventName.CorrectArrow, SwipeArrow);
    }

    private void OnDisable()
    {
        Message.RemoveListener<GameObject>(EventName.CorrectArrow, SwipeArrow);
    }

    private void SwipeArrow(GameObject currentArrow)
    {
        Destroy(currentArrow.GetComponent<BoxCollider2D>());
        currentArrow.transform.SetParent(transform.parent);
        currentArrow.GetComponent<Image>().DOFade(0, 1f);
        currentArrow.transform.DOMoveY(30, 1f).OnComplete(() =>
        {
            Destroy(currentArrow);
        });
    }
}
