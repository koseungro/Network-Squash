using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSound : MonoBehaviour
{

    public AudioClip goal;
    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.CompareTag("BALL")) {
            _audio.PlayOneShot(goal);
        }
    }
}
