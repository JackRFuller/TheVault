using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ForcefieldSwitchHandler))]
public class ForcefieldSwitchHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ForcefieldSwitchHandler targetScript = (ForcefieldSwitchHandler)target;

        if(GUILayout.Button("Generate Wires"))
        {
            targetScript.GenerateWire();
        }
    }
}
