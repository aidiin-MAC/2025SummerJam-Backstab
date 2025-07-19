using UnityEngine;
using UnityEngine.UI;

public class SuspicionAnimLink : MonoBehaviour
{
    public Image meterFill;
    public BattleScript battleData;
    public float suspicion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        suspicion = battleData.suspicion;
        meterFill.fillAmount = suspicion / 100;
    }
}
