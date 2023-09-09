using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform shootPoint;

    private void Awake()
    {
        if(TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += OnStartMoving;
            moveAction.OnStopMoving += OnStopMoving;    
        }
        if(TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShooting += OnShooting;
        }
    }

    private void OnShooting(object sender, ShootAction.OnShootingEventArgs e)
    {
        animator.SetTrigger("Shoot");
        Transform bulletProjectileTransform =Instantiate(pfBulletProjectile,shootPoint.position,Quaternion.identity);
        BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();
        Vector3 targetPosition = e.targetUnit.GetWorldPosition();
        targetPosition.y = shootPoint.position.y;
        bulletProjectile.Setup(targetPosition);
    }

    private void OnStopMoving(object sender, EventArgs e)
    {
       animator.SetBool("IsWalking", false);
    }

    private void OnStartMoving(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", true);
    }
}
