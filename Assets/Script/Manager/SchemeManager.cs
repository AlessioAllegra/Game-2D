using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SchemeManager : MonoBehaviour
{

    [SerializeField] string curretScene;
    [SerializeField] string nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.layer == 8 && PlayerPrefs.GetFloat("PLAYERHEALTH") > 0)
        {
            Debug.Log("Change Scheme");
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (SceneManager.GetActiveScene().name == curretScene)
        {            
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }

}
