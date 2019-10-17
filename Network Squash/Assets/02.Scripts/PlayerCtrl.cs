using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance = null;

    private Transform tr;
    private GameObject face1;
    private GameObject face2;

    void Start()
    {
        instance = this;

        tr = GetComponent<Transform>();
        face1 = tr.GetChild(0).gameObject;
        face2 = tr.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FaceChange();

        }
    }
    
    public void FaceChange()
    {
        if (face1 != null)
        {
            face1.SetActive(false);
            face2.SetActive(true);
            StartCoroutine(FaceReturn());
        }
    }

    IEnumerator FaceReturn()
    {
        yield return new WaitForSeconds(1f);
        face2.SetActive(false);
        face1.SetActive(true);
    }
}
