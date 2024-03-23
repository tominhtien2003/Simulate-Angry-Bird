using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    [SerializeField] private int maxNumberOfShoots = 3;
    private int useNumberOfShoots;
    private Icons icons;

    private void Awake()
    {
        instance = this;
        icons = GameObject.FindObjectOfType<Icons>();
    }
    public void UseShoot()
    {
        useNumberOfShoots++;
        icons.UseShot(useNumberOfShoots);
    }
    public bool HasEnoughtShoot()
    {
        return useNumberOfShoots < maxNumberOfShoots;
    }
}
