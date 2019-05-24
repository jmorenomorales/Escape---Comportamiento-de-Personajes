using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RunnerMovement))]
public class FOVEditorRunner : Editor
{
    private void OnSceneGUI()
    {
        RunnerMovement rm = (RunnerMovement)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(rm.transform.position, Vector3.up, Vector3.forward, 360, rm.viewRadius);
        Vector3 viewAngleA = rm.DirFromAngle(-rm.viewAngle / 2, false);
        Vector3 viewAngleB = rm.DirFromAngle(rm.viewAngle / 2, false);

        Handles.DrawLine(rm.transform.position, rm.transform.position + viewAngleA * rm.viewRadius);
        Handles.DrawLine(rm.transform.position, rm.transform.position + viewAngleB * rm.viewRadius);


        Handles.color = Color.red;
        Handles.DrawWireArc(rm.transform.position, Vector3.up, Vector3.forward, 360, rm.listeningRadius);
    }
}
