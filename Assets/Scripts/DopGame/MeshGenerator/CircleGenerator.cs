using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    private Mesh _mainMesh;
    [SerializeField] private Material meshMaterial;

    private List<Vector3> polygonPoints = new List<Vector3>();

    private List<Vector3> _mainPolygonPoints = new List<Vector3>();
    private List<int> _mainPolygonTriangles = new List<int>();
    private int _circleCount = 0;
    private List<Vector3> _lastCircleCenterPoints = new List<Vector3>();

    void Start()
    {
        _mainMesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = _mainMesh;
        GetComponent<MeshRenderer>().material = meshMaterial;

        EraserGameManager.instance.onDemonstratePhysics += OnDemonstratePhysics;
    }

    private void OnDestroy()
    {
        EraserGameManager.instance.onDemonstratePhysics -= OnDemonstratePhysics;
    }

    public void DrawCircle(int sides, float radius, Vector3 position)
    {
        if (_lastCircleCenterPoints.Count == 2) _lastCircleCenterPoints.RemoveAt(0);
        else position.z = 0.1f;
        _lastCircleCenterPoints.Add(position);

        var listToRestorePoint = new List<Vector3>();
        
        if (_lastCircleCenterPoints.Count == 2)
        {
            listToRestorePoint.AddRange(FindConnectionPoints(polygonPoints, _lastCircleCenterPoints[1]));
        }
        
        polygonPoints = GetCircumferencePoints(sides, radius, position);
        _mainPolygonPoints.AddRange(polygonPoints);
        _mainPolygonTriangles.AddRange(DrawFilledTriangles(polygonPoints.ToArray(), _circleCount, sides).ToList());
        _circleCount++;
        
        listToRestorePoint.AddRange(FindConnectionPoints(polygonPoints, _lastCircleCenterPoints[0]));

        if (_lastCircleCenterPoints.Count == 2)
        {
            var restoredPoints = RestoreRectanglePoints(listToRestorePoint);
            _mainPolygonTriangles.AddRange(DrawRectangle(restoredPoints));
        }

        _mainMesh.Clear();
        _mainMesh.vertices = _mainPolygonPoints.ToArray();
        _mainMesh.triangles = _mainPolygonTriangles.ToArray();
    }

    private int[] DrawRectangle(List<Vector3> points)
    {
        var triangleArray = new int[6];

        triangleArray[0] = GetVertexIndex(points[2]);
        triangleArray[1] = GetVertexIndex(points[1]);
        triangleArray[2] = GetVertexIndex(points[0]);
        triangleArray[3] = GetVertexIndex(points[3]);
        triangleArray[4] = GetVertexIndex(points[2]);
        triangleArray[5] = GetVertexIndex(points[0]);
        
        return triangleArray;
    }

    private int GetVertexIndex(Vector3 point)
    {
        return _mainPolygonPoints.IndexOf(point);
    }

    private List<Vector3> RestoreRectanglePoints(List<Vector3> points)
    {
        var result = points;
        
        var vectorBetweenCircles = _lastCircleCenterPoints[1] - _lastCircleCenterPoints[0];

        var vectorBetweenCenterAndFirstPoint = points[0] - _lastCircleCenterPoints[0];
        
        var vectorBetweenCenterAndThirdPoint = points[3] - _lastCircleCenterPoints[0];

        if (Vector3.Cross( vectorBetweenCenterAndFirstPoint,vectorBetweenCircles).z > 0)
        {
            (result[0], result[1]) = (result[1], result[0]);
        }
        
        if (Vector3.Cross( vectorBetweenCenterAndThirdPoint,vectorBetweenCircles).z > 0)
        {
            (result[2], result[3]) = (result[3], result[2]);
        }

        return result;
    }

    public void Clear()
    {
        _circleCount = 0;
        _mainMesh.Clear();
        _mainPolygonPoints.Clear();
        _mainPolygonTriangles.Clear();
        _lastCircleCenterPoints.Clear();
        polygonPoints.Clear();
    }

    private List<Vector3> FindConnectionPoints(List<Vector3> firstCirclePoints, Vector3 circleCenterPoint)
    {
        var circlePointsArray = firstCirclePoints.ToArray();
        
        for (int i = 0; i < circlePointsArray.Length; i++)
        for (int j = 0; j < circlePointsArray.Length - i - 1; j++)
        {
            var distanceBetweenCenterAndPointJ = Vector3.Distance(circlePointsArray[j], circleCenterPoint);
            
            var distanceBetweenCenterAndPointJOne = Vector3.Distance(circlePointsArray[j + 1], circleCenterPoint);
            
            if (distanceBetweenCenterAndPointJ > distanceBetweenCenterAndPointJOne)
            {
                (circlePointsArray[j], circlePointsArray[j + 1]) = (circlePointsArray[j + 1], circlePointsArray[j]);
            }
        }

        var nearestPoints = circlePointsArray.ToList().GetRange(0, firstCirclePoints.Count/2);

        var rectanglePoints = new List<Vector3>();
        rectanglePoints.Add(nearestPoints[^1]);
        rectanglePoints.Add(nearestPoints[^2]);
        
        return rectanglePoints;
    }

    
    private List<Vector3> GetCircumferencePoints(int sides, float radius, Vector3 position)   
    {
        List<Vector3> points = new List<Vector3>();
        float circumferenceProgressPerStep = (float)1/sides;
        float TAU = 2*Mathf.PI;
        float radianProgressPerStep = circumferenceProgressPerStep*TAU;
        
        for(int i = 0; i<sides; i++)
        {
            float currentRadian = radianProgressPerStep*i;
            points.Add(new Vector3(Mathf.Cos(currentRadian)*radius + position.x, Mathf.Sin(currentRadian)*radius + position.y,position.z));
        }
        return points;
    }
    
    private int[] DrawFilledTriangles([CanBeNull] Vector3[] points, int circleCount, int vertexCount)
    {   
        int triangleAmount = points.Length - 2;
        List<int> newTriangles = new List<int>();
        for(int i = 0; i<triangleAmount; i++)
        {
            newTriangles.Add(0 + circleCount * vertexCount);
            newTriangles.Add(i+2 + circleCount * vertexCount);
            newTriangles.Add(i+1 + circleCount * vertexCount);
        }
        return newTriangles.ToArray();
    }

    private void OnDemonstratePhysics()
    {
        var meshCollider = GetComponent<MeshCollider>();
        if (GetComponent<MeshCollider>() == null) meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;
        meshCollider.sharedMesh = _mainMesh;
    }
}
