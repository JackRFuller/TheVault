using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldSwitchHandler : BaseMonoBehaviour
{
    [Header("Interaction Objects")]
    [SerializeField]
    private Collider switchCollider;

    [Header("Mesh Attributes")]
    [SerializeField]
    private MeshRenderer switchMesh;
    [SerializeField]
    private Material activeSwitchMaterial;
    [SerializeField]
    private Material inactiveSwitchMaterial;

    [Header("Lights")]
    [SerializeField]
    private Light switchLight;

    [Header("Wire Generation")]
    [SerializeField]
    private MeshRenderer wireMesh;
    [SerializeField]
    private Transform wireWaypointHolder;
    [SerializeField]
    private GameObject wireMeshPrefab;
    [SerializeField]
    private Transform wireMeshesHolder;
    [SerializeField]
    private Material wireMeshMaterial;
    private List<Transform> wireWaypoints = new List<Transform>();
    private List<MeshFilter> wireMeshFilters = new List<MeshFilter>();


    private LockState lockState;
    private enum LockState
    {
        Unlocked,
        Locked,
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        wireMesh.material = wireMeshMaterial;
        lockState = LockState.Locked;
        switchMesh.material = activeSwitchMaterial;
        switchCollider.enabled = true;
        switchLight.enabled = true;
    }

    
    public void DeactivateSwitch()
    {
        if(lockState == LockState.Locked)
        {
            EventManager.TriggerEvent("Unlock");
            lockState = LockState.Unlocked;
            switchMesh.material = inactiveSwitchMaterial;
            switchCollider.enabled = false;
            wireMesh.material = inactiveSwitchMaterial;
            switchLight.enabled = false;
        }
    }



#region WireGeneration
    
    public void GenerateWire()
    {
        RemoveAnyExistingWires();
        GetWireWaypoints();        
        CreateWires();
        CombineMeshes();
    }

    private void RemoveAnyExistingWires()
    {
        foreach (Transform child in wireMeshesHolder)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    private void GetWireWaypoints()
    {
        if (wireWaypoints.Count > 0)
            wireWaypoints.Clear();

        foreach(Transform child in wireWaypointHolder)
        {
            wireWaypoints.Add(child);
        }
    }    

    private void CreateWires()
    {
        if(wireMeshFilters.Count > 0)
            wireMeshFilters.Clear();

        for (int i = 0; i < wireWaypoints.Count - 1; i++)
        {
            GameObject newWire = (GameObject)Instantiate(wireMeshPrefab);

            Vector3 wireWaypoint1WorldPos = wireWaypoints[i].position;
            Debug.Log(wireWaypoint1WorldPos);

            Vector3 wireWaypoint2WorldPos = wireWaypoints[i+1].position;
            Debug.Log(wireWaypoint2WorldPos);          
            
            if(CheckIfOnTheDifferentX(wireWaypoint1WorldPos,wireWaypoint2WorldPos))
            {
                float newX = wireWaypoint1WorldPos.x + wireWaypoint2WorldPos.x;
                newX *= 0.5f;

                Vector3 wirePos = new Vector3(newX,
                                              wireWaypoint1WorldPos.y,
                                              wireWaypoint1WorldPos.z);

                newWire.transform.position = wirePos;

                float newXSale = wireWaypoint2WorldPos.x - wireWaypoint1WorldPos.x;
                newXSale = Mathf.Abs(newXSale);

                Vector3 newScale = new Vector3(newXSale + 0.15f, 0.15f, 0.15f);
                newWire.transform.localScale = newScale;
            }
            else if(CheckIfOnTheDifferentZ(wireWaypoint1WorldPos, wireWaypoint2WorldPos))
            {
                float newZ = wireWaypoint1WorldPos.z + wireWaypoint2WorldPos.z;
                newZ *= 0.5f;

                Vector3 wirePos = new Vector3(wireWaypoint1WorldPos.x,
                                              wireWaypoint1WorldPos.y,
                                              newZ);

                newWire.transform.position = wirePos;

                float newZSale = wireWaypoint2WorldPos.z - wireWaypoint1WorldPos.z;
                newZSale = Mathf.Abs(newZSale);

                Vector3 newScale = new Vector3(0.15f,  0.15f, newZSale + 0.15f);
                newWire.transform.localScale = newScale;
            }
            else if(CheckIfOnTheDifferentY(wireWaypoint1WorldPos, wireWaypoint2WorldPos))
            {
                float newY = wireWaypoint1WorldPos.y + wireWaypoint2WorldPos.y;
                newY *= 0.5f;

                Vector3 wirePos = new Vector3(wireWaypoint1WorldPos.x,
                                              newY,
                                              wireWaypoint1WorldPos.z);

                newWire.transform.position = wirePos;

                float newYSale = wireWaypoint2WorldPos.y - wireWaypoint1WorldPos.y;
                newYSale = Mathf.Abs(newYSale);

                Vector3 newScale = new Vector3(0.15f, newYSale + 0.15f, 0.15f);
                newWire.transform.localScale = newScale;
            }

            newWire.transform.parent = wireMeshesHolder.transform;
            MeshFilter filter = newWire.GetComponent<MeshFilter>();

            wireMeshFilters.Add(filter);
        }
    }

    private void CombineMeshes()
    {
        CombineInstance[] combine = new CombineInstance[wireMeshFilters.Count];

        int i = 0;
        while (i < wireMeshFilters.Count)
        {
            combine[i].mesh = wireMeshFilters[i].sharedMesh;
            combine[i].transform = wireMeshFilters[i].transform.localToWorldMatrix;
            i++;
        }

        GameObject newWire = new GameObject();

        newWire.AddComponent<MeshFilter>();
        newWire.AddComponent<MeshRenderer>();

        newWire.GetComponent<MeshFilter>().sharedMesh = new Mesh();
        newWire.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
        newWire.SetActive(true);
        newWire.transform.parent = this.transform;
        newWire.name = "New Wire Mesh";

        newWire.GetComponent<MeshRenderer>().material = wireMeshMaterial;

        RemoveAnyExistingWires();
    }

    private bool CheckIfOnTheDifferentY(Vector3 wireWP1, Vector3 wireWP2)
    {
        float yOne = Mathf.Round(wireWP1.y);
        float yTwo = Mathf.Round(wireWP2.y);

        if (yOne != yTwo)
            return true;
        else
            return false;
    }

    private bool CheckIfOnTheDifferentX(Vector3 wireWP1, Vector3 wireWP2)
    {
        float xOne = Mathf.Round(wireWP1.x);
        float XTwo = Mathf.Round(wireWP2.x);

        if (xOne != XTwo)
            return true;
        else
            return false;
    }

    private bool CheckIfOnTheDifferentZ(Vector3 wireWP1, Vector3 wireWP2)
    {
        float zOne = Mathf.Round(wireWP1.z);
        float zTwo = Mathf.Round(wireWP2.z);

        if (zOne != zTwo)
            return true;
        else
            return false;
    }


    #endregion
}
