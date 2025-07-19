using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndOfIntroText : MonoBehaviour
{

    public int sceneNumber;

    public Animator transition;
    public float transitionTime = 1;


    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EndPoint")
        {

            Debug.Log("Touched end point");
            MoveToScene(sceneNumber);

        }
    }



    IEnumerator LoadLevel(int sceneNumber)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneNumber);

    }




    private void OnTriggerEnter(Collider collision)
    {
        if (gameObject.CompareTag("EndPoint"))
        {
            Debug.Log("Touched end point");
            //MoveToScene(sceneNumber);
            StartCoroutine(LoadLevel(sceneNumber));

        }
    }

}
