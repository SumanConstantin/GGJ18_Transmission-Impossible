using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorObject : MonoBehaviour
{
    [Tooltip("Explosion particle emitter")]
    public Transform explodeAnimPrefab;

    [Tooltip("Higher is faster")]
    public float moveSpeed = 0.07f;    // Higher is faster

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        Move();
	}

    // Move this object on the floor on the Z axis by moveSpeed
    protected void Move()
    {
        transform.position += new Vector3(0, 0, -moveSpeed);
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if(     collider.gameObject.tag == "PlayerChar"
            &&  collider.gameObject.GetComponent<PlayerChar>().canReceiveDamage
            )
            GameEvent.PlayerCharHit(collider.gameObject);

        Explode();
    }

    virtual public void Explode()
    {
        // TODO: add explosion effect
        Destroy(gameObject);
    }

    virtual protected void PlayExplodeAnimation()
    {
        if(explodeAnimPrefab)
        {
            Instantiate(explodeAnimPrefab, transform.position, Quaternion.identity);
        }
    }
}
