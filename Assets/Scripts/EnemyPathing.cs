using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig WaveConfig;
    List<Transform> waypoints;
    [SerializeField] float enemyMoveSpeed = 3f;
    int waypintIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        waypoints = WaveConfig.GetWaypoints();
        transform.position = waypoints[waypintIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (waypintIndex <= waypoints.Count - 1)
        {
            var nextPosition = waypoints[waypintIndex].transform.position;
            var moveSpeedFrame = enemyMoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(
                transform.position,
                nextPosition,
                moveSpeedFrame);
            if (transform.position == nextPosition) waypintIndex++;
        }
        else Destroy(gameObject);
    }
}
