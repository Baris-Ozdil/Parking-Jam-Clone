using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public GameObject arm;

    public List<GameObject> Cars = new List<GameObject>();

    private bool isStay = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Car")
        {

            Cars.Add(other.gameObject);
            if(!isStay)
            {
                arm.transform.DORotate(new Vector3(0 , 0, 90), 1, RotateMode.Fast);
                isStay = true;
            }   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Car")
        {
            Cars.Remove(other.gameObject);
            if(Cars.Count == 0)
            {
                isStay=false;
                arm.transform.DOLocalRotate(Vector3.zero, 1, RotateMode.Fast);
            }
        }
    }

}
