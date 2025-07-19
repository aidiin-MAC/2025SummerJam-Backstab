using TMPro;
using UnityEngine;

public class HudData : MonoBehaviour
{
    [SerializeField] string dataType;
    public CharacterStatus referenceData;
    public BattleScript battleData;
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
            case "SystemMsg":
                if (battleData.animComplete == false)
                {
                    textEntry.text = battleData.systemMessages[0];
                    battleData.systemMessages.RemoveAt(0);
                }
                else if (battleData.systemMessages.Count > 0)
                {
                    battleData.animComplete = false;
                }
                break;
        }
    }
}
