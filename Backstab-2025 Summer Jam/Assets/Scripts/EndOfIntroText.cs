using UnityEngine;
using UnityEngine.SceneManagement;


public class EndOfIntroText : MonoBehaviour
{

    public int sceneNumber;

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


   
    

    private void OnTriggerEnter(Collider collision)
    {
        if (gameObject.CompareTag("EndPoint"))
        {
            Debug.Log("Touched end point");
            MoveToScene(sceneNumber);
        }
    }

}
