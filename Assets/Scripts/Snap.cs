using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{

    private GameObject obje;
    private Vector2 starttouchPos;
    private Vector2 endtouchPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            starttouchPos = Input.GetTouch(0).position;
            var ray = Camera.main.ScreenPointToRay(starttouchPos);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                if(hit.transform.gameObject.tag == "Car")
                {
                    obje = hit.transform.gameObject;
                    Debug.Log("obje:"  + obje.name);
                }
                    
            }
        }
        else if(Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endtouchPos = Input.GetTouch(0).position;
            if (obje == null)
                return;
            Car car = obje.GetComponent<Car>();

            //To avoid moving cars on the road
            if (car.isTrack)
                return;

            var objeForward = obje.transform.forward;
            
            if(objeForward.x > 0)
            {
                if(starttouchPos.x < endtouchPos.x)
                {
                    car.isMoveFront = true;
                    car.isMove = true;
                }
                else
                {
                    car.isMoveFront = false;
                    car.isMove = true;
                }
            }
            else if (objeForward.x < 0)
            {
                if (starttouchPos.x > endtouchPos.x)
                {
                    car.isMoveFront = true;
                    car.isMove = true;
                }
                else
                {
                    car.isMoveFront = false;
                    car.isMove = true;
                }
            }
            else if(objeForward.z > 0)
            {
                if (starttouchPos.y < endtouchPos.y)
                {
                    car.isMoveFront = true;
                    car.isMove = true;
                }
                else
                {
                    car.isMoveFront = false;
                    car.isMove = true;
                }
            }
            else if(objeForward.z < 0)
            {
                if (starttouchPos.y > endtouchPos.y)
                {
                    car.isMoveFront = true;
                    car.isMove = true;
                }
                else
                {
                    car.isMoveFront = false;
                    car.isMove = true;
                }
            }

            //if(obje.transform.forward.x > 0 || obje.transform.forward.z > 0)
            //{
            //    car.isMoveFront = true;
            //    car.isMove = true;
            //}
            //else
            //{
            //    car.isMoveFront = false;
            //    car.isMove = true;
            //}
            obje = null;
        }
    }
}
