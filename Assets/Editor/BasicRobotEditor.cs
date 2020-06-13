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
        if (GUILayout.Button("Reset"))
        {
            robot.pos1 = robot.transform.position + Vector3.left * 5;
            robot.pos2 = robot.transform.position + Vector3.right * 5;
        }
    }

    private void OnSceneGUI()
    {
        Handles.color = Color.green;
        robot.pos1 = Handles.FreeMoveHandle(robot.pos1, Quaternion.identity, 0.1F, Vector3.one, Handles.CircleHandleCap);
        robot.pos2 = Handles.FreeMoveHandle(robot.pos2, Quaternion.identity, 0.1F, Vector3.one, Handles.CircleHandleCap);
        Handles.DrawDottedLine(robot.pos1,robot.pos2, 2F);

        if (GUI.changed)
            EditorUtility.SetDirty(robot);
    }
}