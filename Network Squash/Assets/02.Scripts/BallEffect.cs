using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffect : MonoBehaviour
{
    public GameObject effect;
    private GameObject InstantiateObj;
    private AudioSource sound;


    private void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision coll)
    {
        if (this.gameObject == coll.collider.CompareTag("BALL"))
        {
            ContactPoint contacts = coll.contacts[0];  //콜라이더의 충돌부위의 포인트의 첫번째 포인트를 충돌지점으로 판단함수
            Vector3 _normal = contacts.point;
            sound.Play();

            InstantiateObj = Instantiate(effect, coll.transform.position, Quaternion.FromToRotation(Vector3.up, contacts.normal));
            
            Destroy(InstantiateObj, 1f);
        }
    }
}
