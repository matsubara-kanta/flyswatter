using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[System.Serializable]
public struct FlyParam
{
    public float startTime;
    public Vector2 startPos;
    public GameObject[] waypoints;
    public float[] speeds;
    public bool generated;
}

public class FlyGenerator : MonoBehaviour
{
    [SerializeField] GameObject FlyPrefab;
    [SerializeField] FlyParam[] FlyParams;
    [SerializeField] float flyScale;

    private float elapsedTime;
    private FlyBehaviour flyBehaviour;

    private int cnt;

    // Start is called before the first frame update
    void Start()
    {
        flyBehaviour = FlyPrefab.GetComponent<FlyBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        for(int i = 0; i < FlyParams.Length; i++)
        {
            if (FlyParams[i].generated) continue;
            if (elapsedTime < FlyParams[i].startTime) continue;

            flyBehaviour.SetWaypoints(FlyParams[i].waypoints);

            if (FlyParams[i].speeds.Length == 1)
            {
                flyBehaviour.SetSpeeds(FlyParams[i].speeds[0]);
            }
            else
            {
                flyBehaviour.SetSpeeds(FlyParams[i].speeds);
            }

            FlyPrefab.transform.localScale = new Vector3(flyScale, flyScale, flyScale);
            Instantiate(FlyPrefab, FlyParams[i].startPos, Quaternion.identity);
            FlyParams[i].generated = true;
        }

        cnt = 0;
        for (int i = 0; i < FlyParams.Length; i++)
        {
            if (FlyParams[i].generated) cnt++;
        }

        if(cnt == FlyParams.Length)
        {
            cnt = 0;
            elapsedTime = 0;
            for (int i = 0; i < FlyParams.Length; i++)
            {
                FlyParams[i].generated = false;
            }
        }
    }
}
