using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float[] moveSpeed;
    // [SerializeField] private float[] waitOnWayPoint;
    [SerializeField] private float accuracy = 0.1f;

    private Vector3 goal;
    private Vector3 direction;
    private bool chooseNextPosition;
    private int currentIndex;

    [SerializeField] private float leaveTime = 3f;

    private float leaveTimer = 3f;

    private bool _leave = false;

    private Vector3 leaveGoal;

    // Start is called before the first frame update
    void Start()
    {
        goal = waypoints[0].transform.position;
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        chooseNextPosition = VisitGoal(goal);

        updateLeave();

        if (chooseNextPosition)
        {
            chooseNextPosition = false;
            // StartCoroutine(wait());
            
            if (currentIndex < waypoints.Length - 1)
            {
                currentIndex++;
            }
            

            goal = waypoints[currentIndex].transform.position;
        }

        direction = goal - transform.position;
        if (direction.magnitude < accuracy && currentIndex == waypoints.Length - 1)
        {
            Destroy(gameObject);
        }
    }

    private void startLeave()
    {
        var playerGameObject = GameObject.Find("Player");
        if (playerGameObject != null)
        {
            var vector = gameObject.transform.position - playerGameObject.transform.position;

            var leaveVector = vector.normalized;

            leaveGoal = gameObject.transform.position + (leaveVector * 3f);

            leaveGoal.z = gameObject.transform.position.z;

            goal = leaveGoal;

            leaveTimer = leaveTime;

            _leave = true;
        }
    }

    private void updateLeave()
    {
        if (TimerManager.Instance.Click)
        {
            startLeave();
        }

        if (_leave)
        {
            leaveTimer -= Time.deltaTime;

            if (leaveTimer <= 0f)
            {
                leaveTimer = 0f;
                _leave = false;
            }
        }
    }

    private bool VisitGoal(Vector3 goal)
    {
        direction = goal - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.zero, direction);

        if(transform.position.x >= goal.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (direction.magnitude > accuracy)
        {
            var rate = 1f;

            if (TimerManager.Instance.SkillMode)
            {
                rate = TimerManager.Instance.SkillSlowRate;
            }
            
            transform.Translate(direction.normalized * moveSpeed[currentIndex] * Time.deltaTime * rate, Space.World);
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetWaypoints(GameObject[] wp)
    {
        waypoints = wp;
    }

    public void SetSpeeds(float[] sp)
    {
        moveSpeed = sp;
    }

    public void SetSpeeds(float sp)
    {
        moveSpeed = new float[waypoints.Length];
        for(int i = 0; i < waypoints.Length; i++)
        {
            moveSpeed[i] = sp;
        }
    }
}
