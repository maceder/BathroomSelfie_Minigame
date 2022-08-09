using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akan þeriti kontrol ettiðim yer
/// </summary>
public class RawBackgroundAnimationController : MonoBehaviour
{
    [SerializeField]
    private float rawBackgroundMoveSpeed;

    [HideInInspector]
    public bool rawBackgroundKeepGoing = true;
    [HideInInspector]
    public RectTransform rectTransform;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    private void Update()
    {
        if (rawBackgroundKeepGoing)
            rectTransform.position -= new Vector3(1, 0, 0) * Time.deltaTime * rawBackgroundMoveSpeed;
    }
}
