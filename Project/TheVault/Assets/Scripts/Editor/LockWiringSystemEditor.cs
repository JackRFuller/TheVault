using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LockWiringSystem))]
public class LockWiringSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LockWiringSystem script = (LockWiringSystem)target;

        if(GUILayout.Button("Generate Wires"))
        {
            script.GenerateWires();
        }
    }
}
