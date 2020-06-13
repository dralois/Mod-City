using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BasicRobot))]
[CanEditMultipleObjects]
public class BasicRobotEditor : Editor
{
    BasicRobot robot;

    void OnEnable()
    {
        robot = (BasicRobot) target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        Handles.color = Color.green;
        robot.pos1 = Handles.FreeMoveHandle(robot.pos1, Quaternion.identity, 0.1F, Vector3.one, Handles.CircleHandleCap);
        robot.pos2 = Handles.FreeMoveHandle(robot.pos2, Quaternion.identity, 0.1F, Vector3.one, Handles.CircleHandleCap);
        Handles.DrawDottedLine(robot.pos1,robot.pos2, 2F);
    }
}