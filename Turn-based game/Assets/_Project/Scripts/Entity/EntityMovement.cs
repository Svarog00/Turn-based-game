using Assets._Project.Scripts.Entity.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityMovement : MonoBehaviour, IMovement
{
    public bool CanMove;

    [SerializeField] private float _speed;
    [SerializeField] private List<Vector3> _pathVectorList;

    private Rigidbody2D _rigidbody2;
    private Vector2 _direction;

    private Pathfinding _pathfinding;
    private int _currentPathListIndex;

    private float _desiredRange;
    private Vector2 _targetPosition;

    private void Start()
    {
        _rigidbody2 = GetComponent<Rigidbody2D>();
        _pathfinding = Pathfinding.Instance;

        _desiredRange = -1f;
    }

    private void FixedUpdate()
    {
        Move();
        if(_desiredRange > -1f)
        {
            CheckRange();
        }
    }

    private void CheckRange()
    {
        if(Vector2.Distance(transform.position, _targetPosition) <= _desiredRange)
        {
            CanMove = false;
            _desiredRange = -1f;
        }
    }

    public void MoveToInRange(Vector2 position, float range)
    {
        _desiredRange = range;
        _targetPosition = position;
        MoveTo(position);
    }

    public void MoveTo(Vector2 position)
    {
        SetMovementTarget(position);
        CanMove = true;
    }

    private void SetMovementTarget(Vector2 position)
    {
        _currentPathListIndex = 0;
        _pathVectorList = _pathfinding.FindPath(transform.position, position);
    }

    private void Move()
    {
        if(!CanMove)
        {
            return;
        }

        if (_pathVectorList == null)
        {
            return;
        }

        Vector3 targetPosition = _pathVectorList[_currentPathListIndex];
        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        {
            _currentPathListIndex++;
            if (_currentPathListIndex >= _pathVectorList.Count)
            {
                CanMove = false;
                //Return to idle animation state
            }
        }
        else
        {
            _direction = (targetPosition - transform.position).normalized;
            _rigidbody2.MovePosition(_rigidbody2.position + _direction * _speed * Time.deltaTime);
            //Animate movement
        }
    }
}
