using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BattleScript : MonoBehaviour
{
    public int battleID;
    public int turnCount;
    public int suspicion;
    public int randRoll;
    public string activeChar;
    public float refHealth;
    public int turnPhase; //0 = player phase, 1 = ally phase, 2 = enemy phase
    public int action; //1 = attack, 2 = block, 3 = run
    public bool animComplete;
    public int allyTurnCount;
    public CharacterStatus Player;
    public CharacterStatus Enemy;
    public CharacterStatus Healer;
    public CharacterStatus Paladin;
    public CharacterStatus Monk;
    public CharacterStatus Target;

    public Animator transition;
    public float transitionTime = 1;
    //public int sceneNumber;

    public List<string> systemMessages = new List<string>();

    [ContextMenu("Attack")]
    void Attack()
    {
        Target = Enemy;
        action = 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeChar = "player";
        turnCount = 0;
        turnPhase = 0;
        action = 0;
        suspicion = 0;
        systemMessages.Add("The Monster Emerges");
    }

    // Update is called once per frame
    void Update()
    {
        switch (turnPhase)
        {
            case 0:
                if (animComplete == true)
                {
                    activeChar = "player";
                    actionManager(Player);
                }
                break;
            case 1:
                if (animComplete == true)
                {
                    switch (allyTurnCount)
                    {
                        case 0:
                            activeChar = "healer";
                            action = 3;
                            actionManager(Healer);
                            break;
                        case 1:
                            activeChar = "paladin";
                            action = Random.Range(1, 3);
                            if (action == 3) { action = 4; }
                            actionManager(Paladin);
                            break;
                        case 2:
                            activeChar = "healer";
                            action = 3;
                            actionManager(Healer);
                            break;
                        case 3:
                            activeChar = "monk";
                            action = Random.Range(1, 4);
                            actionManager(Monk);
                            break;

                        default:
                            activeChar = "null";
                            turnPhase = 2;
                            break;
                    }
                }
                break;
            case 2:
                if (animComplete == true)
                {
                    randRoll = Random.Range(0, 3);
                    switch (randRoll)
                    {
                        case 0:
                            Target = Player;
                            break;
                        case 1:
                            Target = Healer;
                            break;
                        case 2:
                            Target = Paladin;
                            break;
                        case 3:
                            Target = Monk;
                            break;
                    }
                    action = Random.Range(1, 2);
                    if (action == 2) { action = 5; }
                    actionManager(Enemy);
                    turnPhase = 0;
                }
                break;
        }
        if (Player.alive == false)
        {
            Lose();
        }
        if (Enemy.alive == false)
        {
            if (Monk.alive == true || Paladin.alive == true || Healer.alive == true)
            {
                Lose();
            }
            else
            {
                Success();
            }
        }

    }
    private void turnAdvance()
    {
        action = 0;
        if (turnPhase == 0)
        {
            allyTurnCount = 0;
            turnPhase = 1;
        }
    }

    private void actionManager(CharacterStatus A)
    {
        float defModifier;
        float atkModifier;
        switch (action)
        {
            case 1: //attack
                float dmgDealt = 0;
                if (turnPhase == 1 && suspicion > 50)
                {
                    randRoll = Random.Range(0, 100);
                    if (randRoll > suspicion) { Target = Player; }
                    else { Target = Enemy; }
                }
                else if (turnPhase == 1) { Target = Enemy; }
                if (A.nameTag == "Hero" && Target.nameTag == "Healer")
                {
                    suspicion += 25;
                }
                if (A.nameTag == "Hero" && Target.nameTag == "Paladin")
                {
                    suspicion += 15;
                }
                if (A.nameTag == "Hero" && Target.nameTag == "Monk")
                {
                    suspicion += 20;
                }
                switch (Target.buffStatus)
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
                if (Target.defending == true)
                {
                    defModifier = 2;
                }
                if (A.attack * atkModifier - Target.defense * defModifier > 1)
                {
                    dmgDealt = A.attack * atkModifier - Target.defense * defModifier;
                }
                else
                {
                    dmgDealt = 1;
                }
                A.defending = false;
                Target.healthCurrent -= dmgDealt;
                systemMessages.Add(A.nameTag + " attacks " + Target.nameTag + "!");
                Debug.Log(A.nameTag + " attacks " + Target.nameTag + "!");
                if (Target.healthCurrent <= 0)
                {
                    Target.alive = false;
                    systemMessages.Add(Target.nameTag + " has perished!");
                    Debug.Log(Target.nameTag + " has perished!");
                }
                turnAdvance();
                break;
            case 2: //defend
                A.defending = true;
                systemMessages.Add(A.nameTag + " defended themself!");
                Debug.Log(A.nameTag + " defended themself!");
                turnAdvance();
                break;
            case 3: //heal
                if (turnPhase == 1)
                {
                    Target = Player;
                    refHealth = Player.healthCurrent;
                    if (Healer.healthCurrent < refHealth)
                    {
                        Target = Healer;
                        refHealth = Healer.healthCurrent;
                    }
                    if (Paladin.healthCurrent < refHealth)
                    {
                        Target = Paladin;
                        refHealth = Paladin.healthCurrent;
                    }
                    if (Monk.healthCurrent < refHealth)
                    {
                        Target = Monk;
                        refHealth = Monk.healthCurrent;
                    }
                }
                Target.healthCurrent += A.attack;
                if (Target.healthCurrent > Target.healthMax)
                {
                    Target.healthCurrent = Target.healthMax;
                }
                if (turnPhase == 0 && Target.nameTag == "Monster")
                {
                    suspicion += 5;
                }
                A.defending = false;
                systemMessages.Add(A.nameTag + " healed " + Target.nameTag + "!");
                Debug.Log(A.nameTag + " healed " + Target.nameTag + "!");
                turnAdvance();
                break;
            case 4: //buff
                if (turnPhase == 1)
                {
                    if (Monk.buffStatus != 1)
                    {
                        Target = Monk;
                    }
                    if (Healer.buffStatus != 1)
                    {
                        Target = Healer;
                    }
                    if (Paladin.buffStatus != 1)
                    {
                        Target = Paladin;
                    }
                    if (Player.buffStatus != 1 && suspicion < 40)
                    {
                        Target = Healer;
                    }
                }
                if (A.nameTag == "Hero" && Target.nameTag == "Monster")
                {
                    suspicion += 40;
                }
                Target.buffStatus = 1;
                A.defending = false;
                systemMessages.Add(A.nameTag + " strengthened " + Target.nameTag + "!");
                Debug.Log(A.nameTag + " strengthened " + Target.nameTag + "!");
                turnAdvance();
                break;
            case 5: //debuff
                if (turnPhase == 1 && suspicion > 65)
                {
                    randRoll = Random.Range(0, 100);
                    if (randRoll > suspicion) { Target = Player; }
                    else { Target = Enemy; }
                }
                if (Target.buffStatus > 0)
                {
                    Target.buffStatus -= 1;
                }
                else if (turnPhase == 1) { Target = Enemy; }
                if (A.nameTag == "Hero" && Target.nameTag == "Healer")
                {
                    suspicion += 25;
                }
                if (A.nameTag == "Hero" && Target.nameTag == "Paladin")
                {
                    suspicion += 30;
                }
                if (A.nameTag == "Hero" && Target.nameTag == "Monk")
                {
                    suspicion += 20;
                }
                A.defending = false;
                systemMessages.Add(A.nameTag + " weakened " + Target.nameTag + "!");
                Debug.Log(A.nameTag + " weakened " + Target.nameTag + "!");
                turnAdvance();
                break;
            default:
                break;
        }
        if (turnPhase == 1)
        {
            allyTurnCount++;
        }
        animComplete = false;
    }

    private void Lose()
    {
        Debug.Log("The battle has been lost");
        //MoveToScene(4);
        StartCoroutine(LoadLevel(4));

    }

    private void Success()
    {
        Debug.Log("The battle has been won! Congratulations");
        //MoveToScene(3);
        StartCoroutine(LoadLevel(3));

    }


    IEnumerator LoadLevel(int sceneNumber)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneNumber);

    }


    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

}
