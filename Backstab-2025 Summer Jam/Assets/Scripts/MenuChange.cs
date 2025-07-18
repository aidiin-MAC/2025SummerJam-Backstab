using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuChange : MonoBehaviour
{
   
        public void MoveToScene(int sceneID)
        {
            SceneManager.LoadScene(sceneID);
        }
    
}
