using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum FollowerState
{
    Idle,
    Follow,
    WalkToUnit,
    AttackEnemy,
    AttackBoss
}

public class Follower : MonoBehaviour
{
    public FollowerState currentFollowerState;
    [SerializeField] private bool isFollowed = false;
    
    [Header("Player's scripts")]
    [SerializeField] private Player _player;
    [SerializeField] private SwerveMovement _swerveMovement;

    [Header("Follower")]
    [SerializeField] private Renderer _followerRenderer;
    [SerializeField] private Material _followerMaterial;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _punchSound;
    [SerializeField] private ParticleSystem dieParticle;
    
    private Enemy _currentEnemy;

    [Header("Gizmos")]
    [SerializeField] private float _distanceToFollow;
    [SerializeField] private float _distanceToWalk;
    [SerializeField] private float _distanceToAttack;
    

    private void Update()
    {
        switch (currentFollowerState)
        {
            case FollowerState.Idle:
                FindPlayer();
                break;
            case FollowerState.Follow:
                if (_swerveMovement.isMouseDown)
                {
                    _animator.SetBool("isRunning",true);
                }
                else
                {
                    _animator.SetBool("isRunning",false);
                }
                FindClosestEnemy();
                break;
            case FollowerState.WalkToUnit:
                if (_currentEnemy)
                {
                    _navMeshAgent.SetDestination(_currentEnemy.transform.position);
                    var distance = Vector3.Distance(transform.position, _currentEnemy.transform.position);
                    
                    if (distance < _distanceToAttack)
                    {
                        _punchSound.Play();
                        SetState(FollowerState.AttackEnemy);
                    }
                }
                break;
            case FollowerState.AttackEnemy:
                var particle = Instantiate(dieParticle , transform.position, transform.rotation);
                Destroy(particle.gameObject,1f);
                Destroy(gameObject,1f);
                break;
            case FollowerState.AttackBoss:
                break;
        }
    }
    
    private void SetState(FollowerState followerState)
    {
        currentFollowerState = followerState;
        
        switch (currentFollowerState)
        {
            case FollowerState.Idle:
                break;
            case FollowerState.WalkToUnit:
                break;
            case FollowerState.AttackEnemy:
                break;
            case FollowerState.AttackBoss:
                break;
        }
    }

    private void FindClosestEnemy()
    {
        var allEnemies = FindObjectsOfType<Enemy>();
        var minDistance = Mathf.Infinity;
        
        Enemy closestEnemy = null;

        foreach (var t in allEnemies)
        {
            var distance = Vector3.Distance(transform.position, t.transform.position);

            if (distance < minDistance)
            {
                if (t.isAttacked == false)
                {
                    minDistance = distance;
                    closestEnemy = t;
                }
            }
        }

        if (minDistance < _distanceToWalk)
        {
            _currentEnemy = closestEnemy;
            _currentEnemy.isAttacked = true;
            
            UnParent(_player.transform);
            SetState(FollowerState.WalkToUnit);
        }
    }

    private void FindPlayer()
    {
        var distance = Vector3.Distance(transform.position, _player.transform.position);
        
        if (distance < _distanceToFollow)
        {
            if (!isFollowed)
            {
                isFollowed = true;
                SetParent(_player.transform);
                _followerRenderer.material = _followerMaterial;
                
                SetState(FollowerState.Follow);
            }
        }
    }

    private void SetParent(Transform newParent)
    {
        this.transform.SetParent(newParent);
    }
    
    private void UnParent(Transform newParent)
    {
        this.transform.SetParent(null);
    }
    
    #region Gizmos

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToFollow, 1f);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up,_distanceToWalk, 1f);
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToAttack, 1f);
    }
#endif

    #endregion
}
