using UnityEngine;
using System;
using System.Collections;
using Zenject;

namespace _GoatJam24.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public Vector3 movement;
        [SerializeField] private float _speed;

        [Inject] private PlayerController _playerController;
        
        private Coroutine _movementRoutine;
        private Rigidbody2D _rb;

        public void StartGame()
        {
            _rb = GetComponent<Rigidbody2D>();
            _movementRoutine = StartCoroutine(MiniGameMovement_Routine());
        }

        public void OverGame()
        {
            if (_movementRoutine is not null)
                StopCoroutine(_movementRoutine);
        }

        private IEnumerator MiniGameMovement_Routine()
        {
            while (true)
            {
                if (!_playerController.canTakeDamage)
                {
                    yield return 0;
                    continue;
                }
                var moveHz = Input.GetAxis("Horizontal");
                var moveVt = Input.GetAxis("Vertical");

                _rb.velocity = new Vector2(moveHz * _speed, moveVt * _speed);

                // movement = new Vector3(moveHz, moveVt, 0) * _speed * Time.deltaTime;
                // var newPos = transform.position + movement;
                // transform.position = newPos;
                
                yield return 0;
            }
        }
    }
}