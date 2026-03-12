using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyState _currentState;
    public EnemyState CurrentState { get => _currentState; set => _currentState = value; }

    private EnemyController controller;

    private void Awake()
    {
        controller = GetComponent<EnemyController>();
    }

    private void Start()
    {
        ChangeState(EnemyState.Idle);
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case EnemyState.Idle:
                controller.HandleIdle();
                break;

            case EnemyState.Patrol:
                controller.HandlePatrol();
                break;

            case EnemyState.Chase:
                controller.HandleChase();
                break;

            case EnemyState.Attack:
                controller.HandleAttack();
                break;

            case EnemyState.Return:
                controller.HandleReturn();
                break;
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if(GameManager.Instance.DebugMode) Debug.Log($"Enemy {gameObject} state are changed to {newState}");
        CurrentState = newState;
    }
}