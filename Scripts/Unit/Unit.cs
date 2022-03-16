using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    public GameObject GameObjectUnit;

    private Vector3 _finelPoint;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
    }

    private void Update()
    {
        if(transform.position.x == _finelPoint.x && transform.position.z ==  _finelPoint.z)
            _animator.SetFloat("Speed", 0);                
    }

    public void MoveUnit(Vector3 targetPoint)
    {
        _finelPoint = targetPoint;
        _agent.SetDestination(targetPoint);
        _animator.SetFloat("Speed", _speed);
    }
}
