using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    [Tooltip("Floor object prefabs")]
    public Transform[] floorObjectPrefabs;
    [Tooltip("Time-frames between floor object creation")]
    public const int floorObjectCreationDelay = 90;
    private int floorObjectCreationDelayCount = floorObjectCreationDelay;

    private List<GameObject> floorObjects;
    private Vector3 floorObjectInitialPosition = new Vector3(0f, 0f, 20f);

    private const float floorObjectDestroyPositionZLowerLimit = -15;

	// Use this for initialization
	void Start () {
        floorObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update () {
        CheckToCreateFloorObject();
        CheckToDeleteFloorObjects();
	}

    // Check if an object should be created
    protected void CheckToCreateFloorObject()
    {
        print("GameState.GetCurrentState():" + GameState.GetCurrentState());
        if (GameState.GetCurrentState().Equals(GameState.PLAYING))
        {
            // TODO: implement floorObject pool
            if (MayCreateObject())
            {
                Transform tr = Instantiate(floorObjectPrefabs[Random.Range(0, floorObjectPrefabs.Length)], floorObjectInitialPosition, Quaternion.identity);
                SetOnRandomLane(tr);
                floorObjects.Add(tr.gameObject);
            }
        }
    }

    // Check if objects should be deleted
    protected void CheckToDeleteFloorObjects()
    {
        // TODO: implement floorObject pool

        int len = floorObjects.Count;
        for (int i = len - 1; i > -1; i--)
        {
            if (floorObjects[i] == null || ObjectIsOutOfScreen(floorObjects[i]))
            {
                if (floorObjects[i] != null)
                {
                    Destroy(floorObjects[i]);
                }

                floorObjects.RemoveAt(i);
            }
        }
    }

    // Check if the floor object is out of screen
    protected bool ObjectIsOutOfScreen(GameObject go)
    {
        if (go.transform.position.z < floorObjectDestroyPositionZLowerLimit)
            return true;

        return false;
    }

    // Check if creation of a floor object is allowed
    protected bool MayCreateObject()
    {
        if (floorObjectCreationDelayCount >= floorObjectCreationDelay)
        {
            floorObjectCreationDelayCount = 0;
            return true;
        }

        ++floorObjectCreationDelayCount;

        return false;
    }

    // Set the object on a random lane
    protected void SetOnRandomLane(Transform tr)
    {
        float posX = (Random.Range((int)0, (int)Main.nrOfLanes) - 2) * Main.distanceBetweenLanes;
        tr.position += new Vector3(posX, 0, 0);
    }
}
