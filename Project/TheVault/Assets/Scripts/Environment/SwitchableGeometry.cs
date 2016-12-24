using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchableGeometry : MonoBehaviour
{
    [SerializeField]
    private GeometryState.State geometryState;

    //Components
    private Collider geoCollider;
    private MeshRenderer geoMesh;    
    private int scanIndex = 0;

    //Materials
    private Material enabledMaterial;
    private Material disabledMaterial;

    private void Start()
    {
        Init();
        SetGeometryStartingState();
    }

    void Init()
    {
        geoCollider = this.GetComponent<Collider>();
        geoMesh = this.GetComponent<MeshRenderer>();

        enabledMaterial = LevelManager.Instance.CurrentLevel.EnabledMaterial;
        disabledMaterial = LevelManager.Instance.CurrentLevel.DisabledMaterial;
    }

    void SetGeometryStartingState()
    {
        switch (geometryState)
        {
            case GeometryState.State.Enabled:
                TurnGeomtryOn();
                break;
            case GeometryState.State.Disabled:
                TurnGeometryOff();
                break;
        }
    }

    public void ToggleGeometry(int index)
    {
        if(scanIndex < index)
        {
            switch (geometryState)
            {
                case GeometryState.State.Enabled:
                    TurnGeometryOff();
                    break;
                case GeometryState.State.Disabled:
                    TurnGeomtryOn();
                    break;
            }

            scanIndex = index;
        }
    }

    void TurnGeomtryOn()
    {
        geoCollider.enabled = true;
        geoMesh.material = enabledMaterial;

        geometryState = GeometryState.State.Enabled;
    }

    void TurnGeometryOff()
    {
        geoCollider.enabled = false;
        geoMesh.material = disabledMaterial;

        geometryState = GeometryState.State.Disabled;
    }
}
