using UnityEngine;
using System;

namespace _GoatJam24.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            var moveHz = Input.GetAxis("Horizontal");
            var moveVt = Input.GetAxis("Vertical");

            var movement = new Vector3(moveHz, moveVt, 0) * _speed ;

            transform.position += movement;
        }
    }
}