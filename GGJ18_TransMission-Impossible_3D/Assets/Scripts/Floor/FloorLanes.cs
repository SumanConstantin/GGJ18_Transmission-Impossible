using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLanes : MonoBehaviour {

    private Vector3 initPos;
    private const uint repositionAfterAmountOfFrames = 90;
    private uint repositionAfterAmountOfFramesCount = 0;
    private Vector3 moveVector = new Vector3(0,0,-.05f);

    public static bool allowReposition = true;  // Set to false when the level is won

    // Use this for initialization
    void Start () {
        initPos = transform.position;
        repositionAfterAmountOfFramesCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
    }

    private void Move()
    {
        // Move by moveVector
        transform.position += moveVector;

        repositionAfterAmountOfFramesCount++;

        // Check to reset the position
        if (repositionAfterAmountOfFramesCount > repositionAfterAmountOfFrames)
        {
            repositionAfterAmountOfFramesCount = 0;

            if(allowReposition)
                Reposition();
        }
    }

    private void Reposition()
    {
        // Get remainder of posZ 
        float posZremainder = transform.position.z - Mathf.FloorToInt(transform.position.z);

        // Set initial position
        transform.position = initPos;

        // Add the remainder or posZ
        transform.position += new Vector3(0, 0, posZremainder);
    }
}
