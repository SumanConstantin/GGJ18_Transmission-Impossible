using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChar : MonoBehaviour {
    
    private const float delayBetweenLaneSwitch = 12;
    private float delayBetweenLaneSwitchCount = delayBetweenLaneSwitch;
    private bool counterMayReset = true;    // Used to reset delayBetweenLaneSwitchCount if a lane switch has been executed
    
    private int currentLaneId = 3;
    private int targetLaneId = 3;
    private bool useLerpLaneSwitch = true;
    private bool doLerp = false;
    private Vector3 targetPos;

    private bool isControlsEnabled = true;
    public bool canReceiveDamage = true;
    
    private float flyAwayAcceleration = 0.001f;
    private float flyAwaySpeed = 0;
    private float flyAwayDistanceMax = 140;

    // Use this for initialization
    void Start() {
        Init();
    }

    private void Init()
    {
        InitEventListeners();
        isControlsEnabled = true;
        canReceiveDamage = true;
        flyAwaySpeed = 0;
    }

    // Update is called once per frame
    void Update() {
        CheckToSwitchLane();
        CheckToFlyAway();

        LerpChangeLane();
    }

    private void LerpChangeLane()
    {
        if (GameState.GetCurrentState().Equals(GameState.PLAYING_BOSS_DEFEATED))
            return;

        transform.position = Vector3.Lerp(transform.position, targetPos, 999999999);
    }

    protected void InitEventListeners()
    {
        GameEvent.onGameStateChanged += OnGameStateChanged;
    }

    protected void RemoveEventListeners()
    {
        GameEvent.onGameStateChanged -= OnGameStateChanged;
    }

    virtual protected void OnGameStateChanged(string gameState, bool isWin = false)
    {
        if(gameState.Equals(GameState.PLAYING_BOSS_DEFEATED))
        {
            isControlsEnabled = false;
            canReceiveDamage = false;
            // TODO: activate shield
        }
    }

    private void CheckToFlyAway()
    {
        if (GameState.GetCurrentState().Equals(GameState.PLAYING_BOSS_DEFEATED))
        {
            flyAwayAcceleration *= 1.05f;
            flyAwaySpeed += flyAwayAcceleration;
            transform.position += new Vector3(0, 0, flyAwaySpeed);

            if(transform.position.z > flyAwayDistanceMax)
            {
                GameState.SetCurrentState(GameState.WIN, true);
            }
        }
    }

    private void CheckToSwitchLane()
    {
        if (!isControlsEnabled) return;

        float inputHorizontal = Input.GetAxis("Horizontal");

        targetLaneId = currentLaneId + (int)Mathf.Sign(inputHorizontal) * 1;

        if (!MaySwitchLaneByDelay() || inputHorizontal == 0 || !MaySwitchLaneByPositionLimit(inputHorizontal))
            return;

        float currentPosX = (targetLaneId - 3) * 1.5f;
        targetPos = new Vector3(currentPosX, transform.position.y, transform.position.z);

        if (useLerpLaneSwitch)
        {
            doLerp = true;
        }
        else
        {
            transform.position = targetPos;
        }

        currentLaneId = targetLaneId;

        counterMayReset = true;
    }

    // Limit the movement by side borders
    private bool MaySwitchLaneByPositionLimit(float inputHorizontal)
    {
        
        if (targetLaneId > 0 && targetLaneId <= Main.nrOfLanes)
            return true;

        return false;
    }

    // Sets a time delay between moves
    private bool MaySwitchLaneByDelay()
    {
        if (counterMayReset)
        {
            delayBetweenLaneSwitchCount = 0;
            doLerp = false;
            counterMayReset = false;
        }

        if (delayBetweenLaneSwitchCount >= delayBetweenLaneSwitch)
        {
            return true;
        }

        ++delayBetweenLaneSwitchCount;
        return false;
    }

    virtual public void Destroy()
    {
        RemoveEventListeners();
    }

    void OnDestroy()
    {
        Destroy();
    }
}
