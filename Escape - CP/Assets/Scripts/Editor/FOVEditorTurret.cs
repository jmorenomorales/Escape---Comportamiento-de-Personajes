using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TurretMovement))]
public class FOVEditorTurret : Editor
{
    private void OnSceneGUI()
    {
        TurretMovement tm = (TurretMovement)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(tm.transform.position, Vector3.up, Vector3.forward, 360, tm.viewRadius);
        Vector3 viewAngleA = tm.DirFromAngle(-tm.viewAngle / 2, false);
        Vector3 viewAngleB = tm.DirFromAngle(tm.viewAngle / 2, false);

        Handles.DrawLine(tm.transform.position, tm.transform.position + viewAngleA * tm.viewRadius);
        Handles.DrawLine(tm.transform.position, tm.transform.position + viewAngleB * tm.viewRadius);
    }
}