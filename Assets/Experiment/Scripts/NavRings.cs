using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class NavRings : MonoBehaviour {
    public int ringVertices = 120;
    public float ringWidth = 1f;
    public float markWidth = 2f;
    public float ringRadius = 10f;
    public float ringMarkCount = 36f;
    public float ringMarkLengthFactor = 0.1f;
    public Material lineMaterial;

    void Start() {
        setupRing("axisX", Vector3.right);
        setupRing("axisY", Vector3.up);
        setupRing("axisZ", Vector3.forward);

        setupMarks("axisMarkers");
    }

    private void setupRing(string id, Vector3 upVector) {
        var ring = new VectorLine(id, new List<Vector3>(ringVertices), ringWidth, LineType.Continuous, Joins.Weld);
        ring.material = lineMaterial;
        ring.MakeCircle(Vector3.zero, upVector, ringRadius);
        // ring.Draw3DAuto();
        
        GameObject container = new GameObject(id);
        container.transform.parent = transform;
        container.transform.localPosition = Vector3.zero;
        VectorManager.useDraw3D = true;
        VectorManager.ObjectSetup(container, ring, Visibility.Always, Brightness.None);
    }

    private void setupMarks(string id) {
        var stepAngle = 360f / ringMarkCount;
        var markLength = ringRadius * ringMarkLengthFactor;
        var innerRad = ringRadius - markLength;
        var points = new List<Vector3>();
        for (int i = 0; i < ringMarkCount; i++) {
            var angle = Mathf.Deg2Rad * stepAngle * i + (Mathf.PI / 2f);
            var xAngle = Mathf.Cos(angle);
            var yAngle = Mathf.Sin(angle);
            var p1 = new Vector3(ringRadius * xAngle, ringRadius * yAngle, 0);
            var p2 = new Vector3(innerRad * xAngle, innerRad * yAngle, 0);
            var q1 = new Vector3(ringRadius * xAngle, 0, ringRadius * yAngle);
            var q2 = new Vector3(innerRad * xAngle, 0, innerRad * yAngle);
            var r1 = new Vector3(0, ringRadius * xAngle, ringRadius * yAngle);
            var r2 = new Vector3(0, innerRad * xAngle, innerRad * yAngle);
            points.Add(p1);
            points.Add(p2);
            points.Add(q1);
            points.Add(q2);
            points.Add(r1);
            points.Add(r2);
        }

        var marks = new VectorLine(id, points, markWidth);
        GameObject container = new GameObject(id);
        container.transform.parent = transform;
        container.transform.localPosition = Vector3.zero;
        VectorManager.useDraw3D = true;
        VectorManager.ObjectSetup(container, marks, Visibility.Always, Brightness.None);
    }

    void Update() {

    }
}
