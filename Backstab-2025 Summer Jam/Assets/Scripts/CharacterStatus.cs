using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public string name;
    public float healthMax;
    public float healthCurrent;
    public int attack;
    public int defense;
    public bool alive;
    public bool defending;
    public int buffStatus;
    public int buffTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthCurrent = healthMax;
        alive = true;
        defending = false;
        buffStatus = 1;
        buffTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthMax <= 0)
        {
            alive = false;
        }
    }
}
