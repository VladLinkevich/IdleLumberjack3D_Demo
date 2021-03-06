﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float lengthOfHit = 1.5f;

    private CharacterController _charController;
    private Animator _animator;

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Messenger<Vector3>.AddListener(GameEvent.MOVE, Move);
    }

    private void Start()
    {
        CameraManager.instance.Target = transform;

        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 velocity = _charController.velocity;
        _animator.SetFloat("Speed", Mathf.Abs(velocity.z + velocity.x));
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            Collider[] trees = Physics.OverlapSphere(transform.position, lengthOfHit, 1 << LayerMask.NameToLayer("Tree"));
            if (trees.Length > 0) { 
                _animator.SetTrigger("Melee");

                Vector3 distance = new Vector3(trees[0].transform.position.x - transform.position.x,
                    0,
                    trees[0].transform.position.z - transform.position.z);
                distance.Normalize();

                transform.rotation = Quaternion.Euler(0,
                    Mathf.Atan2(distance.x, distance.z) * Mathf.Rad2Deg, 0);
            }
        } else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            _animator.ResetTrigger("Melee");
        }
        
        _charController.Move(new Vector3(0, -0.1f ,0));
    }

    #endregion

    private void Move(Vector3 mov)
    {
        _charController.Move(mov * moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(mov.x, mov.z) * Mathf.Rad2Deg, 0);
    }

    public void Hit()
    {
        Collider[] trees = Physics.OverlapSphere(transform.position, lengthOfHit, 1 << LayerMask.NameToLayer("Tree"));
        foreach (Collider tree in trees)
        {
            tree.GetComponent<TreeController>()?.Hit(AxeManager.instance.Damage);
        }
    }

}
