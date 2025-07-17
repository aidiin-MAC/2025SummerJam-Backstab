using UnityEngine;

public class TextScroll : MonoBehaviour
{
    public float introTextSpeed = 10;


   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, 1) * Time.deltaTime * introTextSpeed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            introTextSpeed += 150;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            introTextSpeed -= 150;
            print("Key Released");
        }
    }
}
