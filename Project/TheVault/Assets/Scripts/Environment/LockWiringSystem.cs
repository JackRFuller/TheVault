using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockWiringSystem : BaseMonoBehaviour
{
    [SerializeField]
    private Transform wires;
    [SerializeField]
    private GameObject wirePrefab;
    [SerializeField]
    private Transform wireWaypoint;

    private List<Transform> wireWaypoints = new List<Transform>();

    private List<MeshFilter> generatedMeshes = new List<MeshFilter>();

    public void GenerateWires()
    {
        //Remove Existing Wires
        if (wires.childCount > 0)
            RemoveExistingWires();

        if (generatedMeshes.Count > 0)
            generatedMeshes.Clear();

        //Get All Wire Waypoints
        GetWireWaypoints();

        //Cycle through all waypoints
        for (int i = 0; i < wireWaypoints.Count - 1; i++)
        {
            GameObject newWire = (GameObject)Instantiate(wirePrefab);

            //Is Waypoint above or below?
            if (IsWayPointOnDifferentYValue(wireWaypoints[i],wireWaypoints[i+1]))
            {
                //Position Between Points
                float newY = wireWaypoints[i].position.y + wireWaypoints[i+1].position.y;
                newY *= 0.5f;

                Vector3 newPosition = new Vector3(wireWaypoints[i].position.x,
                                                  newY,
                                                  wireWaypoints[i].position.z);

                newWire.transform.position = newPosition;

                //Adjust Scale
                float newYScale = wireWaypoints[i+1].position.y - wireWaypoints[i].position.y;
                newYScale = Mathf.Abs(newYScale);
                newYScale += 0.1f;

                Vector3 newScale = new Vector3(0.1f, newYScale, 0.1f);

                newWire.transform.localScale = newScale;
            }
            else if (IsWayPointOnDifferentXValue(wireWaypoints[i], wireWaypoints[i + 1]))
            {
                //Position Between Points
                float newX = wireWaypoints[i].position.x + wireWaypoints[i + 1].position.x;
                newX *= 0.5f;

                Vector3 newPosition = new Vector3(newX,
                                                  wireWaypoints[i].position.y,
                                                  wireWaypoints[i].position.z);

                newWire.transform.position = newPosition;

                //Adjust Scale
                float newXScale = wireWaypoints[i + 1].position.x - wireWaypoints[i].position.x;
                newXScale = Mathf.Abs(newXScale);
                newXScale += 0.1f;

                Vector3 newScale = new Vector3(newXScale, 0.1f, 0.1f);

                newWire.transform.localScale = newScale;
            }
            else if (IsWayPointOnDifferentZValue(wireWaypoints[i], wireWaypoints[i + 1]))
            {
                //Position Between Points
                float newZ = wireWaypoints[i].position.z + wireWaypoints[i + 1].position.z;
                newZ *= 0.5f;

                Vector3 newPosition = new Vector3(wireWaypoints[i].position.x,
                                                  wireWaypoints[i].position.y,
                                                  newZ);

                newWire.transform.position = newPosition;

                //Adjust Scale
                float newZScale = wireWaypoints[i + 1].position.z - wireWaypoints[i].position.z;
                newZScale = Mathf.Abs(newZScale);
                newZScale += 0.1f;

                Vector3 newScale = new Vector3(0.1f, 0.1f, newZScale);

                newWire.transform.localScale = newScale;
            }

            MeshFilter mesh = newWire.GetComponent<MeshFilter>();
            generatedMeshes.Add(mesh);

            newWire.transform.parent = wires;

        }

        GenerateMeshes();
    }

    void GenerateMeshes()
    {
        CombineInstance[] combine = new CombineInstance[generatedMeshes.Count];

        int i = 0;
        while(i < generatedMeshes.Count)
        {
            combine[i].mesh = generatedMeshes[i].sharedMesh;
            combine[i].transform = generatedMeshes[i].transform.localToWorldMatrix;
            i++;
        }

        GameObject newWire = new GameObject();

        newWire.AddComponent<MeshFilter>();
        newWire.AddComponent<MeshRenderer>();

        newWire.GetComponent<MeshFilter>().sharedMesh = new Mesh();
        newWire.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
        newWire.SetActive(true);       

    }

    void RemoveExistingWires()
    {
        foreach(Transform wire in wires)
        {
            DestroyImmediate(wire.gameObject);
        }
    }

    void GetWireWaypoints()
    {
        wireWaypoints.Clear();

        foreach(Transform waypoint in wireWaypoint)
        {
            wireWaypoints.Add(waypoint);            
        }
    }

    bool IsWayPointOnDifferentYValue(Transform waypoint1, Transform waypoint2)
    {
        if (waypoint1.position.y != waypoint2.position.y)
            return true;
        else
            return false;
    }

    bool IsWayPointOnDifferentXValue(Transform waypoint1, Transform waypoint2)
    {
        if (waypoint1.position.x != waypoint2.position.x)
            return true;
        else
            return false;
    }

    bool IsWayPointOnDifferentZValue(Transform waypoint1, Transform waypoint2)
    {
        if (waypoint1.position.z != waypoint2.position.z)
            return true;
        else
            return false;
    }
}

