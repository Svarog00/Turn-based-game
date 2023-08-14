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

    private List<Vector3> _pathVectorList;

    private Rigidbody2D _rigidbody2;

    private Vector2 _direction;
    private Vector3 _targetPosition;

    private Pathfinding _pathfinding;
    private int _currentPathListIndex;

    private void Start()
    {
        _rigidbody2 = GetComponent<Rigidbody2D>();
        _pathfinding = Pathfinding.Instance;
    }

    private void FixedUpdate()
    {
        if(!CanMove)
        {
            return;
        }

        Move();
    }

    public void MoveTo(Vector2 position)
    {
        SetMovementTarget(position);
        CanMove = true;
    }

    public void StopMove()
    {
        CanMove = false;
    }

    private void SetMovementTarget(Vector2 position)
    {
        _currentPathListIndex = 0;
        _pathVectorList = _pathfinding.FindPath(transform.position, position);
        _targetPosition = _pathVectorList[_currentPathListIndex];
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

        if (Vector3.Distance(transform.position, _targetPosition) <= 0.1f)
        {
            _currentPathListIndex++;
            if (_currentPathListIndex >= _pathVectorList.Count)
            {
                CanMove = false;
                //Return to idle animation state
                return;
            }
            _targetPosition = _pathVectorList[_currentPathListIndex];
        }
        else
        {
            _direction = (_targetPosition - transform.position).normalized;
            _rigidbody2.MovePosition(_rigidbody2.position + _direction * _speed * Time.deltaTime);
        }
    }
}
