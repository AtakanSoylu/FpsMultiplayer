using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiFps.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Vector3 _velecity;
        [SerializeField] private bool _isGrounded;

        [SerializeField] private Transform _ground;
        [SerializeField] private float _distance = 0.3f;

        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _gravity;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private LayerMask _layerMask;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _rigidbody = GetComponent<Rigidbody>();
            _characterController = GetComponent<CharacterController>();
        }
        private void Update()
        {
            #region Movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            _characterController.Move(move * _speed * Time.deltaTime);
            #endregion

            #region Jump

            if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _velecity.y += Mathf.Sqrt(_jumpHeight * -3f * _gravity);
            }
            #endregion

            #region Gravity
            _isGrounded = Physics.CheckSphere(_ground.position, _distance, _layerMask);

            if(_isGrounded && _velecity.y < 0)
            {
                _velecity.y = 0f;
            }

            _velecity.y += _gravity * Time.deltaTime;
            _characterController.Move(_velecity * Time.deltaTime);
            #endregion
        }
    }
}