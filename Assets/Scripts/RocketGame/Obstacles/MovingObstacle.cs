using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private Transform obstacleTransform;

    [SerializeField] private bool isRandomEndPoint;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 endPoint;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxStartDelay;

    private Vector3 _targetPoint;
    private bool _isMoving;

    private void Start()
    {
        if(isRandomEndPoint) endPoint = GetRandomEndPoint(); 
        _targetPoint = endPoint;
        StartCoroutine(DelayCoroutine());
    }
    
    private IEnumerator DelayCoroutine()
    {
        _isMoving = false;
        var delay = GetRandomDelay();
        yield return new WaitForSeconds(delay);
        _isMoving = true;
    }   

    void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        if(!_isMoving) return;

        obstacleTransform.localPosition = Vector3.Lerp(obstacleTransform.localPosition, _targetPoint, moveSpeed * Time.deltaTime);
        
        if (Vector3.Distance(obstacleTransform.localPosition, _targetPoint) <= 0.1f) _targetPoint = GetNewTargetPoint();

    }

    private Vector3 GetNewTargetPoint()
    {
        if (_targetPoint == startPoint) return endPoint;
        return startPoint;
    }

    private Vector3 GetRandomEndPoint()
    {
        var vectorBetweenStartAndEnd = endPoint - startPoint;
        var magnitude = vectorBetweenStartAndEnd.magnitude;

        var randomMagnitude = Random.Range(0, magnitude);

        return vectorBetweenStartAndEnd.normalized * randomMagnitude;
    }

    private float GetRandomDelay()
    {
        return Random.Range(0, maxStartDelay);
    }
}
