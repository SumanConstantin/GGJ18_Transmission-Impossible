using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorObstacle : FloorObject
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // TODO: implement this in an interface
    protected override void OnTriggerEnter(Collider collider)
    {
        PlayExplodeAnimation();
        base.OnTriggerEnter(collider);
    }

}
