using System.Collections;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Line Renderers")]
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;

    [Header("Transforms")]
    [SerializeField] private Transform _leftStart;
    [SerializeField] private Transform _rightStart;
    [SerializeField] private Transform _center;
    [SerializeField] private Transform _idle;

    [Header("Slingshot Max Distance")]
    [SerializeField] private float maxDis;
    [SerializeField] private float shotForce = 5f;
    [SerializeField] private float timeBettwenBirdSpawn = 2f;

    [Header("Scripts")]
    [SerializeField] private SlingshotArea slingshotArea;

    [Header("Slingshot Area")]
    private Vector3 _slingshotLinePosition;
    private bool isWithinArea;

    [Header("Bird")]
    [SerializeField] private Transform birdPrefab;
    [SerializeField] private float birdPositionOffset;
    private Transform spawnBird;
    private Vector2 direc;
    private Vector2 direcNormalized;
    private bool birdOnSlingshot;

    private void Awake()
    {
        _leftLineRenderer.enabled = false;
        _rightLineRenderer.enabled = false;
        SpawnBird();
    }
    private void Update()
    {
        /*Debug.Log(Input.mousePosition);
        Debug.Log(Mouse.current.position.ReadValue());*/
        
        if (Input.GetMouseButton(0) && slingshotArea.isWithinSlingshotArea())
        {
            isWithinArea = true;
        }
        if (Input.GetMouseButton(0) && isWithinArea && birdOnSlingshot)
        {
            DrawSlingshot();
            UpdatePositionBirdFollowSlingshot();
        }
        if (Input.GetMouseButtonUp(0) && birdOnSlingshot)
        {
            if (GameManager.instance.HasEnoughtShoot())
            {
                GameManager.instance.UseShoot();
                isWithinArea = false;
                spawnBird.GetComponent<AngryBird>().LaunchBird(direc, shotForce);
                birdOnSlingshot = false;
                SetLine(_center.position);
                if (GameManager.instance.HasEnoughtShoot())
                {
                    StartCoroutine(SpawnBirdAfterTime());
                }
            }
        }
        //Debug.Log(isWithinArea);
        
    }
    private void DrawSlingshot()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _slingshotLinePosition = _center.position + Vector3.ClampMagnitude(touchPosition -_center.position, maxDis);
        SetLine(_slingshotLinePosition);

        direc = _center.position - _slingshotLinePosition;
        direcNormalized = direc.normalized;
    }
    private void SetLine(Vector3 touchPosition)
    {
        if (!_leftLineRenderer.enabled && !_rightLineRenderer.enabled)
        {
            _leftLineRenderer.enabled = true;
            _rightLineRenderer.enabled = true;
        }

        _leftLineRenderer.SetPosition(0, touchPosition);
        _leftLineRenderer.SetPosition(1, _leftStart.position);
        _rightLineRenderer.SetPosition(0, touchPosition);
        _rightLineRenderer.SetPosition(1, _rightStart.position);
    }

    private void SpawnBird()
    {
        SetLine(_idle.position);

        Vector2 dir = (_center.position - _idle.position).normalized;
        Vector2 spawnPosition = (Vector2)_idle.position + dir * birdPositionOffset;

        spawnBird = Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
        spawnBird.right = dir;
        birdOnSlingshot = true;
    }
    private void UpdatePositionBirdFollowSlingshot()
    {
        spawnBird.position = (Vector2)_slingshotLinePosition + direcNormalized * birdPositionOffset;
        spawnBird.right = direcNormalized;
    }
    private IEnumerator SpawnBirdAfterTime()
    {
        yield return new WaitForSeconds(timeBettwenBirdSpawn);
        SpawnBird();
    }
}
