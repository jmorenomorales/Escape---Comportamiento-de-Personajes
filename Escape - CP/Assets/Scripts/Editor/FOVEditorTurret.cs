using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TurretController))]
public class FOVEditorTurret : Editor
{
    private void OnSceneGUI()
    {
        TurretController tc = (TurretController)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(tc.transform.position, Vector3.up, Vector3.forward, 360, tc.viewRadius);
        Vector3 viewAngleA = tc.DirFromAngle(-tc.viewAngle / 2, false);
        Vector3 viewAngleB = tc.DirFromAngle(tc.viewAngle / 2, false);

        Handles.DrawLine(tc.transform.position, tc.transform.position + viewAngleA * tc.viewRadius);
        Handles.DrawLine(tc.transform.position, tc.transform.position + viewAngleB * tc.viewRadius);
    }
}