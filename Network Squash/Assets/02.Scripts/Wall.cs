using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private GameObject reflect;
    private GameObject _reflect;


    void Start()
    {
        reflect = Resources.Load<GameObject>("Reflect");

    }


    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("BALL"))
        {
            //충돌한 지점의 배열
            ContactPoint[] contacts = coll.contacts;
            //첫 번째 충돌 지점의 법선 벡터 산출 (- 방향)
            Vector3 _normal = -contacts[0].normal;

            _reflect = Instantiate(reflect, coll.transform.position, Quaternion.LookRotation(_normal));

            Destroy(_reflect, 1f);

        }
    }



}
