using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Follower _follower;
    private bool _isAttacked = false;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private Player _player;
    public bool isAttacked
    {
        get => _isAttacked;
        set => _isAttacked = value;
    }

    [SerializeField] private float _distanceToAttack = 1f;

    private void Update()
    {
        FindClosestUnit();

        //Attack Player
        var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        if (distanceToPlayer < _distanceToAttack)
        {
            GameManager.Instance.Lose();
        }
    }

    private void FindClosestUnit()
    {
        var allFollowers = FindObjectsOfType<Follower>();
        var distanceToAttack = Mathf.Infinity;

        Follower closestFollower = null;

        foreach (var t in allFollowers)
        {
            var distance = Vector3.Distance(transform.position, t.transform.position);

            if (distance < distanceToAttack)
            {
                distanceToAttack = distance;
                closestFollower = t;
            }
        }

        if (distanceToAttack < _distanceToAttack && closestFollower.currentFollowerState == FollowerState.WalkToUnit)
        {
            Destroy(gameObject, 0.5f);
        }
    }


    #region Gizmos

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position,Vector3.up, _distanceToAttack, 1f);
    }
#endif

    #endregion

}
