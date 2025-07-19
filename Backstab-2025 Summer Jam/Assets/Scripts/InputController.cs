using UnityEngine;

public class InputController : MonoBehaviour
{
    public BattleScript gameStatus;
    public int target;
    public int pendingAction;
    public int inputState;
    public bool animComplete;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = 0;
        inputState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        animComplete = gameStatus.animComplete;
        if (gameStatus.animComplete == false && gameStatus.turnPhase == 0)
        {
            switch (inputState)
            {
                case 0:
                    if (gameStatus.systemMessages.Count < 1)
                    {
                        gameStatus.systemMessages.Add("Use WASD to choose an action");
                    }
                    if (Input.GetKeyDown("w"))
                    {
                        pendingAction = 1;
                        inputState++;
                    }
                    if (Input.GetKeyDown("a"))
                    {
                        pendingAction = 3;
                        inputState++;
                    }
                    if (Input.GetKeyDown("s"))
                    {
                        pendingAction = 2;
                        inputState = 2;
                    }
                    if (Input.GetKeyDown("d"))
                    {
                        pendingAction = 5;
                        inputState++;
                    }
                    break;
                case 1:
                    if (gameStatus.systemMessages.Count < 1)
                    {
                        gameStatus.systemMessages.Add("Use A and D to select a target");
                    }
                    if (Input.GetKeyDown("a"))
                    {
                        if (target > 0)
                        {
                            target--;
                        }
                        else
                        {
                            target = 4;
                        }
                    }
                    if (Input.GetKeyDown("d"))
                    {
                        if (target < 4)
                        {
                            target++;
                        }
                        else
                        {
                            target = 0;
                        }
                    }
                    if (Input.GetKeyDown("x"))
                    {
                        inputState--;
                    }
                    if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
                    {
                        inputState++;
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
                    {
                        switch (target)
                        {
                            case 0:
                                gameStatus.Target = gameStatus.Enemy;
                                break;
                            case 1:
                                gameStatus.Target = gameStatus.Player;
                                break;
                            case 2:
                                gameStatus.Target = gameStatus.Paladin;
                                break;
                            case 3:
                                gameStatus.Target = gameStatus.Healer;
                                break;
                            case 4:
                                gameStatus.Target = gameStatus.Monk;
                                break;
                            default:
                                gameStatus.Target = gameStatus.Enemy;
                                break;
                        }
                        gameStatus.action = pendingAction;
                        gameStatus.animComplete = true;
                        inputState = 0;
                    }
                    else
                    {
                        if (gameStatus.systemMessages.Count < 1)
                        {
                            gameStatus.systemMessages.Add("Confirm?");
                        }
                    }
                    if (Input.GetKeyDown("x"))
                    {
                        inputState--;
                    }
                    break;
            }
        }
        else if (gameStatus.animComplete == false)
        {
            if (Input.anyKeyDown)
            {
                gameStatus.animComplete = true;
            }
        }
    }
}
