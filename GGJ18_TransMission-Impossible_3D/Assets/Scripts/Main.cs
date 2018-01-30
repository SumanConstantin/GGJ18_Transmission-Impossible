using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public const float distanceBetweenLanes = 1.5f;
    public const uint nrOfLanes = 5;

    [Tooltip("Player Character Prefab")]
    public Transform charPrefab;
    public GameObject heartBarGO;

    public Text distanceTextField;

    // TODO: feed this value from a level config file per each level
    private const float travelDistanceInit = 200f;
    private float travelDistanceLeft = travelDistanceInit;
    private float speedInLightYears = .1f;

    private const uint heartCountMax = 3;
    private static uint heartCount;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    virtual protected void Init()
    {
        Transform tr = Instantiate(charPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        InitEventListeners();
        heartCount = heartCountMax;
        travelDistanceLeft = travelDistanceInit;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistance();

    }

    private void UpdateDistance()
    {
        if (!GameState.GetCurrentState().Equals(GameState.PLAYING))
        {
            return;
        }

        travelDistanceLeft -= speedInLightYears;
        distanceTextField.text = "LightYears: " + (int)travelDistanceLeft;

        if (travelDistanceLeft < 1)
        {
            // Play without boss
            GameState.SetCurrentState(GameState.PLAYING_BOSS_DEFEATED);

            // Play with boss
            //GameState.SetCurrentState(GameState.PLAYING_BOSS);
        }
    }

    public static bool NoLivesLeft()
    {
        return heartCount <= 0;
    }

    protected void InitEventListeners()
    {
        GameEvent.onPlayerCharHit += OnPlayerCharHit;
    }

    protected void RemoveEventListeners()
    {
        GameEvent.onPlayerCharHit -= OnPlayerCharHit;
    }

    virtual protected void OnPlayerCharHit(GameObject go)
    {
        heartCount--;
        heartBarGO.GetComponent<HeartBar>().RemoveOneHeart();

        // TODO: Check if there's no more health -> Lose
        if(heartCount <= 0)
        {
            // Trigger the Lose State
            GameEvent.GameStateChanged(GameState.LOSE, false);
        }
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
