using UnityEngine;

public class SlingshotArea : MonoBehaviour
{
    [SerializeField] private LayerMask _slingshotAreaMask;
    public bool isWithinSlingshotArea()
    {
        Vector3 wordPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Physics2D.OverlapPoint(wordPoint,_slingshotAreaMask))
        {
            return true;
        }
        return false;
    }
}
