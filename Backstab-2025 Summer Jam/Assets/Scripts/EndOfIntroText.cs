using UnityEngine;
using UnityEngine.SceneManagement;


public class EndOfIntroText : MonoBehaviour
{

    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EndPoint")
        {

            Debug.Log("Touched end point");
            MoveToScene(2);

        }
    }


   
    

    private void OnTriggerEnter(Collider collision)
    {
        if (gameObject.CompareTag("EndPoint"))
        {
            Debug.Log("Touched end point");
            MoveToScene(2);
        }
    }

}
