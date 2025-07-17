using UnityEngine;

public class BattleScript : MonoBehaviour
{
    public int battleID;
    public int turnCount;
    public int turnPhase; //0 = player phase, 1 = ally phase, 2 = enemy phase
    public int action; //1 = attack, 2 = block, 3 = run
    [SerializeField] CharacterStatus Player;
    [SerializeField] CharacterStatus Enemy;

    [ContextMenu("Attack")]
    void Attack()
    {
        action = 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnCount = 0;
        turnPhase = 0;
        action = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (turnPhase)
        {
            case 0:
                actionManager(Player,Enemy);
                break;
            case 1:
                turnAdvance();
                break;
            case 2:
                actionManager(Enemy, Player);
                break;
        }
    }
    private void turnAdvance()
    {
        action = 0;
        if (turnPhase < 2)
        {
            turnPhase += 1;
        }
        else
        {
            turnPhase = 0;
        }
    }
    
    private void actionManager(CharacterStatus A, CharacterStatus B)
    {
        float defModifier;
        float atkModifier;
        switch (action)
        {
            case 1: //attack
                float dmgDealt = 0;
                switch (B.buffStatus)
                {
                    case 0:
                        defModifier = 0;
                        atkModifier = 0.5F;
                        break;
                    default:
                        defModifier = 1;
                        atkModifier = 1;
                        break;
                    case 2:
                        defModifier = 2;
                        atkModifier = 1.5F;
                        break;
                }
                if (B.defending == true)
                {
                    defModifier = 2;
                }
                if (A.attack * atkModifier - B.defense * defModifier > 1)
                {
                    dmgDealt = A.attack * atkModifier - B.defense * defModifier;
                }
                else
                {
                    dmgDealt = 1;
                }
                A.defending = false;
                B.healthCurrent -= dmgDealt;
                Debug.Log(A.name + " attacks " + B.name + "!");
                turnAdvance();
                break;
            case 2:
                A.defending = true;
                turnAdvance();
                break;
            case 3:
                battleEnd();
                break;
            default:
                break;
        }
    }

    private void battleEnd()
    {
        Debug.Log("Player ran from the enemy");
    }
}
