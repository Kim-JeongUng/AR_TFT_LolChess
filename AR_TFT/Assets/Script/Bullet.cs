using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform Target;
    public Rigidbody bulletrigid;
    public float turn = 10f;
    public float bulletVelocity  = 10f;

    public void FixedUpdate() // 유도탄
    {
        bulletrigid.velocity = transform.forward * bulletVelocity;
        var bulletTargetRotation = Quaternion.LookRotation(Target.position + new Vector3(0, 100f) - transform.position);
        bulletrigid.MoveRotation(Quaternion.RotateTowards(transform.rotation, bulletTargetRotation,turn));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
