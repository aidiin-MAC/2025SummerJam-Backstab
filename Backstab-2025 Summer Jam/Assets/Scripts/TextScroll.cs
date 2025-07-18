using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TextScroll : MonoBehaviour
{
    public float introTextSpeed = 10;

    public int sceneNumber;

    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, 1) * Time.deltaTime * introTextSpeed);
        if (Input.GetKeyDown("space"))
        {
            introTextSpeed += 350;
        }
        else if (Input.GetKeyUp("space"))
        {
            introTextSpeed -= 350;
           // print("Key Released");
        }
        if (Input.GetKeyDown("escape"))
        {
            MoveToScene(sceneNumber);
        }
    }


    


}
