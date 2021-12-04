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
        var bulletTargetRotation = Quaternion.LookRotation(Target.position + new Vector3(0, 60f) - transform.position);
        bulletrigid.MoveRotation(Quaternion.RotateTowards(transform.rotation, bulletTargetRotation,turn));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Champion")  // Champion => 상대 적 챔피언이면
        {
            Debug.Log("Bullet Trigger Champion");
            Destroy(gameObject);  // 총알 삭제
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
