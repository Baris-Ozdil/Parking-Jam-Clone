using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject SceneLoader;
    public GameObject canvas;
    //Destroys cars and loads the next scene if there are no cars left in the scene
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Car")
        {
            Datas.Cars.Remove(other.gameObject);
            Destroy(other.gameObject);
            if(Datas.Cars.Count == 0)
            {
                canvas.SetActive(true);
                StartCoroutine(changeScene());
            }
        }
        
    }


    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(3f);
        SceneLoader.GetComponent<SceneLoader>().loadScene();
    }
}
