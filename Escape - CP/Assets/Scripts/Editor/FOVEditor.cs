using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (GuardController))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        GuardController gc = (GuardController)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(gc.transform.position, Vector3.up, Vector3.forward, 360, gc.viewRadius);
        Vector3 viewAngleA = gc.DirFromAngle(-gc.viewAngle/2, false);
        Vector3 viewAngleB = gc.DirFromAngle(gc.viewAngle / 2, false);

        Handles.DrawLine(gc.transform.position, gc.transform.position + viewAngleA * gc.viewRadius);
        Handles.DrawLine(gc.transform.position, gc.transform.position + viewAngleB * gc.viewRadius);
    }
}
