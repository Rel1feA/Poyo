using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicPred : MonoBehaviour
{
    public float objGravityScale;//需要预测的物体的重力
    public int trajectoryPoints;//轨迹线点数
    public float timeStep;//时间步长
    public LineRenderer lineRenderer;

    public Vector2 CalculatePosition(float t,Vector2 startVel)
    {
        return new Vector2(startVel.x * t, startVel.y*t + 0.5f * Physics2D.gravity.y * objGravityScale * t * t);
    }

    public List<Vector2> CalculateTrajectory(Vector2 startPos, Vector2 startVel)
    {
        List<Vector2> points= new List<Vector2>();
        for(int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * timeStep;
            Vector2 point=CalculatePosition(t, startVel);
            points.Add(startPos + point);
        }
        return points;
    }

    public void DrawTrajectory(List<Vector2> points)
    {
        lineRenderer.positionCount=points.Count;
        for(int i = 0;i < points.Count;i++)
        {
            lineRenderer.SetPosition(i,points[i]);
        }
    }
}
