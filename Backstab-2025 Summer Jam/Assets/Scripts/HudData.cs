using UnityEngine;
using TMPro;

public class HudData : MonoBehaviour
{
    [SerializeField] string dataType;
    public CharacterStatus referenceData;
    public TMP_Text textEntry;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (dataType)
        {
            case "Name":
                textEntry.text = referenceData.name;
                break;
            case "Health":
                textEntry.text = string.Format("{0:N2}", referenceData.healthCurrent);
                break;
            case "Sprite":
                break;
        }
    }
}
