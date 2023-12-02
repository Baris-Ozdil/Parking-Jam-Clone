using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{

    public float rotateAngel = 20;
    public float rotateTime = 0.15f;
    public float speed = 3f;
    public float upLimit = 10;
    public float DownLimit = -10;
    public float LeftLimit = -10;
    public float RightLimit = 10;
    public float turnTime = 0.3f;

    public bool isMoveFront = true;
    public bool isMove = false;
    public bool isTrack = false;

    public int road = 0;

    private bool canMove = true;
    private bool isWait = false;

    // Start is called before the first frame update
    void Start()
    {
        Datas.Cars.Add(this.gameObject);
    }


    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        if (!isTrack)
        {
            if (isMove)
            {
                if (!canMove)
                {
                    isMove = false;
                    if(!isWait)
                        StartCoroutine(wait());
                    return;
                }
                if (isMoveFront)
                {
                    transform.position += transform.forward * speed * Time.deltaTime;
                    //if(transform.position.z < DownLimit || transform.position.z > upLimit || transform.position.x < LeftLimit || transform.position.x > RightLimit)
                    //{
                    //    isMove = false;
                    //    isTrack = true;
                    //    transform.DORotate(new Vector3 (transform.eulerAngles.x , transform.eulerAngles.y + 90 , transform.eulerAngles.z) , turnTime);
                    //}

                    if (transform.position.x > RightLimit)
                    {
                        isMove = false;
                        isTrack = true;
                        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z), turnTime);
                        road = 1;
                        
                    }
                    else if (transform.position.x < LeftLimit)
                    {
                        isMove = false;
                        isTrack = true;
                        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z), turnTime);
                        road = 3;
                    }
                    else if (transform.position.z < DownLimit)
                    {
                        isMove = false;
                        isTrack = true;
                        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z), turnTime);
                        road = 2;
                    }
                    else if (transform.position.z > upLimit)
                    {
                        isMove = false;
                        isTrack = true;
                        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z), turnTime);
                        road = 0;
                    }
                }
                else
                {
                    transform.position -= transform.forward * speed * Time.deltaTime;
                    if (transform.position.x > RightLimit)
                    {
                        isMove = false;
                        isTrack = true;
                        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z), turnTime);
                        road = 1;

                    }
                    else if ( transform.position.x < LeftLimit)
                    {
                        isMove = false;
                        isTrack = true;
                        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z), turnTime);
                        road = 3;
                    }
                    else if (transform.position.z < DownLimit)
                    {
                        isMove = false;
                        isTrack = true;
                        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z), turnTime);
                        road = 2;
                    }
                    else if (transform.position.z > upLimit)
                    {
                        isMove = false;
                        isTrack = true;
                        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z), turnTime);
                        road = 0;
                    }
                }
                    
            }
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;

            if(road == 0 && transform.position.x > RightLimit)
            {
                transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z), turnTime);
                road ++;
            }
            else if( road == 2 && transform.position.x < LeftLimit)
            {
                transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z), turnTime);
                road ++;
            }
            else if (road == 1 && transform.position.z < DownLimit)
            {
                transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z), turnTime);
                road ++;
            }
        }
    }

    IEnumerator wait()
    {
        isWait = true;
        yield return new WaitForSeconds(rotateTime);
        canMove = true;
        isWait = false;
    }
    private void MyCrashEffect()
    {
        if (isMoveFront)
        {
            transform.DOLocalRotate(new Vector3(transform.eulerAngles.x - rotateAngel, transform.eulerAngles.y, transform.eulerAngles.z), rotateTime, RotateMode.Fast).SetLoops(2, LoopType.Yoyo);
            transform.DOMove(transform.position - (transform.forward / 4), rotateTime);
        }
        else
        {
            transform.DOLocalRotate(new Vector3(transform.eulerAngles.x + rotateAngel, transform.eulerAngles.y, transform.eulerAngles.z), rotateTime, RotateMode.Fast).SetLoops(2, LoopType.Yoyo);
            transform.DOMove(transform.position + (transform.forward / 4), rotateTime);
        }
    }
    
    public void HitMe(Transform otherCar ,bool otherMoveFoward)
    {
        if (otherCar.transform.forward == transform.forward)
        {
            if (otherMoveFoward)
            {
                transform.DOLocalRotate(new Vector3(transform.eulerAngles.x - rotateAngel, transform.eulerAngles.y, transform.eulerAngles.z), rotateTime, RotateMode.Fast).SetLoops(2, LoopType.Yoyo);
            }
            else
            {
                transform.DOLocalRotate(new Vector3(transform.eulerAngles.x + rotateAngel, transform.eulerAngles.y, transform.eulerAngles.z), rotateTime, RotateMode.Fast).SetLoops(2, LoopType.Yoyo);
            }
        }
        else if (otherCar.transform.forward == -transform.forward)
        {
            if (!otherMoveFoward)
            {
                transform.DOLocalRotate(new Vector3(transform.eulerAngles.x - rotateAngel, transform.eulerAngles.y, transform.eulerAngles.z), rotateTime, RotateMode.Fast).SetLoops(2, LoopType.Yoyo);
            }
            else
            {
                transform.DOLocalRotate(new Vector3(transform.eulerAngles.x + rotateAngel, transform.eulerAngles.y, transform.eulerAngles.z), rotateTime, RotateMode.Fast).SetLoops(2, LoopType.Yoyo);
            }
        }
        else if(transform.rotation.y == 0|| transform.rotation.y == 180)
        {
            if(transform.position.x - otherCar.position.x > 0)
            {
                //left
                transform.DORotate(new Vector3(transform.eulerAngles.x , transform.eulerAngles.y, transform.eulerAngles.z - rotateAngel), rotateTime, RotateMode.Fast).SetLoops(2, LoopType.Yoyo);
            }
            else
            {
                //right
                transform.DORotate(new Vector3(transform.eulerAngles.x , transform.eulerAngles.y, transform.eulerAngles.z + rotateAngel), rotateTime, RotateMode.Fast).SetLoops(2, LoopType.Yoyo);
            }
        }
        else
        {
            if (transform.position.z - otherCar.position.z > 0)
            {
                //left
                transform.DORotate(new Vector3(transform.eulerAngles.x , transform.eulerAngles.y, transform.eulerAngles.z + rotateAngel), rotateTime, RotateMode.Fast).SetLoops(2, LoopType.Yoyo);
            }
            else
            {
                //right
                transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - rotateAngel), rotateTime, RotateMode.Fast).SetLoops(2, LoopType.Yoyo);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMove)
        {
            if (other.gameObject.tag == "Car")
            {
                if (!other.GetComponent<Car>().isTrack)
                {
                    other.gameObject.GetComponent<Car>().HitMe(transform , isMoveFront);
                    isMove = false;
                    MyCrashEffect();
                }
            }
            else if(other.gameObject.tag == "Obstacle")
            {
                isMove = false;
                MyCrashEffect();
            }
                
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (isMove)
        {
            
            if (other.gameObject.tag == "Car")
            {
                if (!other.GetComponent<Car>().isTrack)
                {
                    other.gameObject.GetComponent<Car>().HitMe(transform, isMoveFront);
                    isMove = false;
                    MyCrashEffect();
                }
            }
            else if (other.gameObject.tag == "Obstacle")
            {
                isMove = false;
                MyCrashEffect();
            }
        }
    }
}
