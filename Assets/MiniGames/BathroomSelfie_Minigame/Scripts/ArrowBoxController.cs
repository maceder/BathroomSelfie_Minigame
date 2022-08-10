using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Arrow'un directionunu aldýðým ve fýrlattýðým yer 
/// ArrowBox renk ve titreþimi
/// </summary>
public class ArrowBoxController : MonoBehaviour
{
   
    public EnumSwipeDirection inBoxArrow = EnumSwipeDirection.Empty;

    private Image boxImage;
    private RectTransform boxRectTransform;
    private GameObject currentTriggerArrow;
    private void Start()
    {
        boxImage = GetComponent<Image>();
        boxRectTransform = GetComponent<RectTransform>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ArrowType>() != null)
        {
            inBoxArrow = collision.GetComponent<ArrowType>().enumSwipeDirection;

            Message.Send<EnumSwipeDirection>(EventName.RawBacgroundArrowTouch, inBoxArrow);

            currentTriggerArrow = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ArrowType>() != null)
        {
            inBoxArrow = EnumSwipeDirection.None;
        }
    }



    public void WrongArrowAnimation()
    {
        if(inBoxArrow != EnumSwipeDirection.Empty)
        {
            boxImage.color = Color.red;
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOMove(new Vector3(boxRectTransform.position.x - 30, boxRectTransform.position.y, boxRectTransform.position.z), 0.3f)).
                       Append(transform.DOMove(new Vector3(boxRectTransform.position.x + 30, boxRectTransform.position.y, boxRectTransform.position.z), 0.3f)).
                       Append(transform.DOMove(boxRectTransform.position, 0.3f));
            boxImage.DOColor(Color.white, 1f);
        }
        
    }

    public void CorrectArrowAnimation()
    {
        boxImage.color = Color.green;
        boxImage.DOColor(Color.white, 1f);

        Message.Send<GameObject>(EventName.CorrectArrow, currentTriggerArrow.gameObject);
    }

}
