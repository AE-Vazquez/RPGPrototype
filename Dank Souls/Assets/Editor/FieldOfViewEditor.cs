using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{

    override public void OnInspectorGUI()
    {
        serializedObject.Update();

        serializedObject.FindProperty("viewRadius").floatValue= EditorGUILayout.FloatField("View Radius", serializedObject.FindProperty("viewRadius").floatValue);
        serializedObject.FindProperty("viewAngle").floatValue = EditorGUILayout.Slider("View Angle", serializedObject.FindProperty("viewAngle").floatValue,0f, 360f);

        serializedObject.FindProperty("targetMask").intValue = EditorGUILayout.LayerField("Target Mask", serializedObject.FindProperty("targetMask").intValue);
        serializedObject.FindProperty("obstacleMask").intValue = EditorGUILayout.LayerField("Obstacle Mask", serializedObject.FindProperty("obstacleMask").intValue);


        serializedObject.FindProperty("drawFOV").boolValue = EditorGUILayout.Toggle("Draw FOV", serializedObject.FindProperty("drawFOV").boolValue);

        bool showDrawParameters = serializedObject.FindProperty("drawFOV").boolValue;

        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(showDrawParameters)))
        {
            if (group.visible == true)
            {
                EditorGUI.indentLevel++;
                serializedObject.FindProperty("meshResolution").floatValue = Mathf.Max(0f, EditorGUILayout.FloatField("Mesh Resolution",serializedObject.FindProperty("meshResolution").floatValue));
                serializedObject.FindProperty("edgeResolveIterations").intValue = Mathf.Max(0, EditorGUILayout.IntField("Edge Iterations", serializedObject.FindProperty("edgeResolveIterations").intValue));
                serializedObject.FindProperty("edgeDistThreshold").floatValue = Mathf.Max(0f, EditorGUILayout.FloatField("Edge Dist Threshold", serializedObject.FindProperty("edgeDistThreshold").floatValue));
                EditorGUI.indentLevel--;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;

        float viewRadius = serializedObject.FindProperty("viewRadius").floatValue;
        float viewAngle = serializedObject.FindProperty("viewAngle").floatValue;

        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * viewRadius);
        
    }

}