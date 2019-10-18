using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSound : MonoBehaviour
{
    private GameObject goal_Par;
    private GameObject _goal_Par;

    public AudioClip goal;
    private AudioSource _audio;

    void Start()
    {
        goal_Par = Resources.Load<GameObject>("Goal");
        _audio = GetComponent<AudioSource>();
    }
      

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.CompareTag("BALL")) {
            ContactPoint[] contacts = coll.contacts;
            Vector3 _normal = -contacts[0].normal;

            _goal_Par = Instantiate(goal_Par, coll.transform.position, Quaternion.LookRotation(_normal));
            Destroy(_goal_Par, 1f);

            _audio.PlayOneShot(goal);
        }
        
    }
}
