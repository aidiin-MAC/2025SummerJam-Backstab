using UnityEngine;

public class IndicatorPlacement : MonoBehaviour
{
    public InputController Input;
    public RectTransform myTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTransform.position = myTransform.TransformVector(new Vector2(-532, 32));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.inputState == 1)
        {
            switch (Input.target)
            {
                case 0:
                    myTransform.position = myTransform.TransformVector(new Vector2(-176, 149));
                    break;
                case 1:
                    myTransform.position = myTransform.TransformVector(new Vector2(-317, 32));
                    break;
                case 2:
                    myTransform.position = myTransform.TransformVector(new Vector2(-94, 32));
                    break;
                case 3:
                    myTransform.position = myTransform.TransformVector(new Vector2(98, 32));
                    break;
                case 4:
                    myTransform.position = myTransform.TransformVector(new Vector2(278, 32));
                    break;
            }
        }
        else
        {
            myTransform.position = myTransform.TransformVector(new Vector2(-532, 32));
        }
    }
}
