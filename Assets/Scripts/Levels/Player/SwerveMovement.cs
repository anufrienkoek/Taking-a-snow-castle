using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    private SwerveInputSystem _swerveInputSystem;
    [SerializeField] private Animator _animator;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    [SerializeField] private float _playerSpeed;
    public bool isMouseDown;
    
    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.moveFactorX;
            var maximumSwerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
            _animator.SetBool("isRunning",true);
            isMouseDown = true;
            
            transform.Translate(maximumSwerveAmount,0f,_playerSpeed * Time.deltaTime);
        }
        else
        {
            isMouseDown = false;
            _animator.SetBool("isRunning",false);
        }
    }
}
