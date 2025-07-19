using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuChange : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1;
    public int sceneNumber;

    /*
        public void MoveToScene(int sceneID)
        {

            SceneManager.LoadScene(sceneID);
        }
    */


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(sceneNumber));
    }

    IEnumerator LoadLevel(int sceneNumber)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneNumber);

    }
    
}
