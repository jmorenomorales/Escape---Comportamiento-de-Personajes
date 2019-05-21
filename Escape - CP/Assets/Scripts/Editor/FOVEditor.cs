using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (GuardMovement))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        GuardMovement gm = (GuardMovement)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(gm.transform.position, Vector3.up, Vector3.forward, 360, gm.viewRadius);
        Vector3 viewAngleA = gm.DirFromAngle(-gm.viewAngle / 2, false);
        Vector3 viewAngleB = gm.DirFromAngle(gm.viewAngle / 2, false);

        Handles.DrawLine(gm.transform.position, gm.transform.position + viewAngleA * gm.viewRadius);
        Handles.DrawLine(gm.transform.position, gm.transform.position + viewAngleB * gm.viewRadius);
    }
}
