using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UnitySteer2D.Behaviors;
using System.Collections.Generic;
using UnitySteer2D;

public class MyTests {

	[Test]
    public void SteerForTether2D_MaximumDistanceTest()
    {
		GameObject gameObject = new GameObject();
        SteerForTether2D sft = gameObject.AddComponent<SteerForTether2D>();

        Assert.GreaterOrEqual(sft.MaximumDistance, 0);
	}

    [Test]
    public void DetectableObject2D_RadiusTest()
    {
        GameObject gameObject = new GameObject();
        DetectableObject2D obj = gameObject.AddComponent<DetectableObject2D>();

        Assert.GreaterOrEqual(obj.Radius, 1);
    }

    [Test]
    public void DetectableObject2D_CenterTest()
    {
        GameObject gameObject = new GameObject();
        DetectableObject2D obj = gameObject.AddComponent<DetectableObject2D>();
        Vector2 pos = (Vector2)obj.Transform.position + obj.Center;

        Assert.LessOrEqual((obj.Position - pos).magnitude, Mathf.Epsilon);
    }

    [Test]
    public void DetectableObject2D_CenterChangeTest([NUnit.Framework.Range(-5, 10, 3)] int x)
    {
        GameObject gameObject = new GameObject();
        DetectableObject2D obj = gameObject.AddComponent<DetectableObject2D>();
        obj.Center = Vector2.right * x;
        Vector2 pos = (Vector2)obj.Transform.position + Vector2.right * x;

        Assert.LessOrEqual((obj.Position - pos).magnitude, Mathf.Epsilon);
    }


    //Vehicle2D IsInNeighborhood Test -------------------------------------


    [Test]
    public void Vehicle2D_IsInNeighborhoodTest_True()
    {
        GameObject gameObject1 = new GameObject();
        GameObject gameObject2 = new GameObject();
        AutonomousVehicle2D obj1 = gameObject1.AddComponent<AutonomousVehicle2D>();
        AutonomousVehicle2D obj2 = gameObject2.AddComponent<AutonomousVehicle2D>();

        Vector2 pos = new Vector2();
        pos.Set(2, 2);
        obj1.transform.SetPositionAndRotation(pos, new Quaternion());

        Assert.True(obj1.IsInNeighborhood(obj2,3,5,1));
    }

    [Test]
    public void Vehicle2D_IsInNeighborhoodTest_Max_False()
    {
        GameObject gameObject1 = new GameObject();
        GameObject gameObject2 = new GameObject();
        AutonomousVehicle2D obj1 = gameObject1.AddComponent<AutonomousVehicle2D>();
        AutonomousVehicle2D obj2 = gameObject2.AddComponent<AutonomousVehicle2D>();

        Vector2 pos = new Vector2();
        pos.Set(10, 10);
        obj1.transform.SetPositionAndRotation(pos, new Quaternion());

        Assert.False(obj1.IsInNeighborhood(obj2, 0, 3, 1));
    }

    [Test]
    public void Vehicle2D_IsInNeighborhoodTest_Min_False()
    {
        GameObject gameObject1 = new GameObject();
        GameObject gameObject2 = new GameObject();
        AutonomousVehicle2D obj1 = gameObject1.AddComponent<AutonomousVehicle2D>();
        AutonomousVehicle2D obj2 = gameObject2.AddComponent<AutonomousVehicle2D>();

        Vector2 pos = new Vector2();
        pos.Set(2, 2);
        obj1.transform.SetPositionAndRotation(pos, new Quaternion());

        Assert.False(obj1.IsInNeighborhood(obj2, 10000, 50, 1));
    }

    [Test]
    public void Vehicle2D_IsInNeighborhoodTest_Rot_False()
    {
        GameObject gameObject1 = new GameObject();
        GameObject gameObject2 = new GameObject();
        AutonomousVehicle2D obj1 = gameObject1.AddComponent<AutonomousVehicle2D>();
        AutonomousVehicle2D obj2 = gameObject2.AddComponent<AutonomousVehicle2D>();

        Vector2 pos = new Vector2();
        pos.Set(2, 2);
        obj1.transform.SetPositionAndRotation(pos, new Quaternion());

        Assert.False(obj1.IsInNeighborhood(obj2, 0, 3, 0.01f));
    }


    //Vehicle2D DistanceFromPerimeter Test -------------------------------------
    
    [Test]
    public void Vehicle2D_DistanceFromPerimeterTest()
    {
        GameObject gameObject1 = new GameObject();
        GameObject gameObject2 = new GameObject();
        AutonomousVehicle2D obj1 = gameObject1.AddComponent<AutonomousVehicle2D>();
        AutonomousVehicle2D obj2 = gameObject2.AddComponent<AutonomousVehicle2D>();

        Vector2 pos = new Vector2();
        pos.Set(3, 3);
        obj1.transform.SetPositionAndRotation(pos, new Quaternion());

        Assert.LessOrEqual(obj1.DistanceFromPerimeter(obj2),3);
    }

    //Vehicle2D GetSeekVector Test -------------------------------------

    [Test]
    public void Vehicle2D_GetSeekVectorTest()
    {
        GameObject gameObject1 = new GameObject();
        AutonomousVehicle2D obj1 = gameObject1.AddComponent<AutonomousVehicle2D>();

        Vector2 pos = new Vector2();
        pos.Set(3, 3);

        Assert.LessOrEqual(obj1.GetSeekVector(pos, true).x, 3);
    }

    //Vehicle2D PredictNearestApproachTime Test -------------------------------------

    [Test]
    public void Vehicle2D_PredictNearestApproachTimeTest()
    {
        GameObject gameObject1 = new GameObject();
        GameObject gameObject2 = new GameObject();
        AutonomousVehicle2D obj1 = gameObject1.AddComponent<AutonomousVehicle2D>();
        AutonomousVehicle2D obj2 = gameObject2.AddComponent<AutonomousVehicle2D>();

        Vector2 pos = new Vector2();
        pos.Set(3, 3);
        obj1.transform.SetPositionAndRotation(pos, new Quaternion());

        Assert.LessOrEqual(obj1.PredictNearestApproachTime(obj2), 0);
    }

    //Vector2Pathway Creation Test -------------------------------------

    [Test]
    public void Vector2DPathWayTest_Creation()
    {
        List<Vector2> points =new List<Vector2>();
        points.Add(new Vector2(0, 0));
        points.Add(new Vector2(1, 0));
        points.Add(new Vector2(1, 1));

        Vector2Pathway path = new Vector2Pathway(points, 1);

        Assert.NotZero(path.SegmentCount);
    }

    //Vector2Pathway Curve Test -------------------------------------

    [Test]
    public void Vector2DPathWayTest_Curve()
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(new Vector2(0, 0));
        points.Add(new Vector2(1, 0));
        points.Add(new Vector2(1, 1));

        Vector2Pathway path = new Vector2Pathway(points, 1);
        Vector2 point = path.MapPathDistanceToPoint(1.3f);

        Assert.True(point.x==1 && point.y>0);
    }

    //Vector2Pathway MapPoint Test -------------------------------------

    [Test]
    public void Vector2DPathWayTest_MapPoint()
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(new Vector2(0, 0));
        points.Add(new Vector2(1, 0));
        points.Add(new Vector2(1, 1));

        Vector2Pathway path = new Vector2Pathway(points, 1);
        Vector2 point = new Vector2(1, 1.2f);
        PathRelativePosition pos = new PathRelativePosition();

        Assert.True(path.MapPointToPath(point,ref pos).x>0);
    }

    //Vector2Pathway Reference Test -------------------------------------

    [Test]
    public void Vector2DPathWayTest_Reference_Zero()
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(new Vector2(0, 0));
        points.Add(new Vector2(1, 0));
        points.Add(new Vector2(1, 1));

        Vector2Pathway path = new Vector2Pathway(points, 1);
        Vector2 point = path.MapPathDistanceToPoint(1);
        PathRelativePosition pos = new PathRelativePosition();

        Assert.True(path.MapPointToPath(point, ref pos).x>0);
    }

    //Vector2Pathway Inside Test ---------------------------------------

    [Test]
    public void Vector2DPathWayTest_IsInsidePath_True()
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(new Vector2(0, 0));
        points.Add(new Vector2(1, 0));
        points.Add(new Vector2(1, 1));

        Vector2Pathway path = new Vector2Pathway(points, 1);
        Vector2 point = path.MapPathDistanceToPoint(1);

        Assert.True(path.IsInsidePath(point));
    }

    [Test]
    public void Vector2DPathWayTest_IsInsidePath_False()
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(new Vector2(0, 0));
        points.Add(new Vector2(1, 0));
        points.Add(new Vector2(1, 1));

        Vector2Pathway path = new Vector2Pathway(points, 1);
        Vector2 point = new Vector2(-1, -1);

        Assert.False(path.IsInsidePath(point));
    }

    //Vector2Pathway Outside Test ---------------------------------------

    [Test]
    public void Vector2DPathWayTest_HowFarOutsidePath_NotZero()
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(new Vector2(0, 0));
        points.Add(new Vector2(1, 0));
        points.Add(new Vector2(1, 1));

        Vector2Pathway path = new Vector2Pathway(points, 1);
        Vector2 point = new Vector2(2, 2);

        Assert.NotZero(path.HowFarOutsidePath(point));
    }

    [Test]
    public void Vector2DPathWayTest_HowFarOutsidePath_Zero()
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(new Vector2(0, 0));
        points.Add(new Vector2(1, 0));
        points.Add(new Vector2(1, 1));

        Vector2Pathway path = new Vector2Pathway(points, 1);
        Vector2 point = path.MapPathDistanceToPoint(1);

        Assert.LessOrEqual(path.HowFarOutsidePath(point),0);
    }

}
