using Assets._Project.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowWeapon : MonoBehaviour, IWeapon
{
    public int Damage => _damage;

    public float AttackRange => _range;

    [SerializeField] private float _range;
    [SerializeField] private int _damage;

    public void Attack(IHealth target)
    {
        target.Hurt(_damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
