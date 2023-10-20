using System;
using System.Runtime;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(FOV))]

public class FOVEditor : Editor
{
    void onSceneGUI()
    {
        FOV fov = (FOV)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);
    }
  
}
