using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    
    private Vector3 pos1 = new Vector3(5.31f, 6.04f, -4f);
    private Vector3 pos1Rot = new Vector3(34.16f, -35.3f, 0f);
    private Vector3 pos2 = new Vector3(-5.31f, 6.04f, -4f);
    private Vector3 pos2Rot = new Vector3(34.16f, 35.3f, 0f);

    private Vector3[] positions = new Vector3[] { new Vector3(5.31f, 6.04f, -4f), new Vector3(-5.31f, 6.04f, -4f) };
    private Vector3[] rotations = new Vector3[] { new Vector3(34.16f, -35.3f, 0f), new Vector3(34.16f, 35.3f, 0f) };

    private int currentPosIndex = 0;
    private Vector3 targetPos;
    private Vector3 targetRot;

    private const float delayBetweenPositionSwitch = 300;
    private float delayCounter = 0;

    // Use this for initialization
    void Start () {
        delayCounter = 0;
        currentPosIndex = 0;

        targetPos = positions[0];
        targetRot = rotations[0];
    }
	
	// Update is called once per frame
	void Update () {
        return;
        CheckToChangePosition();
        LerpChangeLane();
    }

    private void CheckToChangePosition()
    {
        delayCounter++;

        if (currentPosIndex == 0)
        {
            if (delayCounter >= delayBetweenPositionSwitch)
            {
                delayCounter = 0;

                // Cycle through positions
                if (currentPosIndex < positions.Length - 1)
                    currentPosIndex++;
                else
                    currentPosIndex = 0;

                targetPos = positions[currentPosIndex];
                targetRot = rotations[currentPosIndex];
            }
        }
    }

    private void LerpChangeLane()
    {
        Quaternion targetRotX = Quaternion.AngleAxis(targetRot.x, Vector3.right);
        Quaternion targetRotY= Quaternion.AngleAxis(targetRot.y, Vector3.up);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotX, Time.deltaTime);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotY, Time.deltaTime);

        float angleX = Mathf.LerpAngle(transform.rotation.x, targetRot.x, Time.time);
        float angleY = Mathf.LerpAngle(transform.rotation.y, targetRot.y, Time.time);
        transform.eulerAngles = new Vector3(angleX, angleY, 0);
    }
}
