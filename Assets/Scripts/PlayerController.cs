using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;

    private CharacterController _charController;
    private Animator _animator;

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Messenger<Vector3>.AddListener(GameEvent.MOVE, Move);
    }

    private void Start()
    {
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_charController.velocity.z + _charController.velocity.x));
        _charController.Move(Vector3.zero);
    }

    #endregion
   
    private void Move(Vector3 mov)
    {
        _charController.Move(mov * moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(mov.x, mov.z) * Mathf.Rad2Deg, 0);
    }

}
