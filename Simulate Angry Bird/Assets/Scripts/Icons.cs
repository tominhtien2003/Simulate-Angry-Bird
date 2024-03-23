using UnityEngine;
using UnityEngine.UI;

public class Icons : MonoBehaviour
{
    [SerializeField] private Image[] iconArrs;
    [SerializeField] private Color useColor;

    public void UseShot(int shootNumber)
    {
        for (int i = 0; i < iconArrs.Length; i++)
        {
            if (i+1 == shootNumber)
            {
                iconArrs[i].color = useColor;
                return;
            }
        }
    }
}
