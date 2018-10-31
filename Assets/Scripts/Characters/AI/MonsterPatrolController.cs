using UnityEngine;
using UnityEngine.AI;

public class MonsterPatrolController : MonoBehaviour
{
    public GameObject[] PatrolPoints;

    private int _currentPoint;
    private NavMeshAgent _nma;

    // Use this for initialization
    void Start()
    {
        _nma = GetComponent<NavMeshAgent>();
    }

    public void Partrol()
    {
        var point = PatrolPoints[_currentPoint].transform.position;
        point.y = transform.position.y;
        _nma.SetDestination(point);
        _nma.isStopped = false;

        if (_nma.remainingDistance <= 2.5f && !_nma.pathPending)
        {
            _currentPoint = (_currentPoint + 1) % PatrolPoints.Length;
        }
    }
}
