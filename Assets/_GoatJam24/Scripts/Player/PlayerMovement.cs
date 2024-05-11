using UnityEngine;
using System;
using System.Collections;

namespace _GoatJam24.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Start()
        {
            StartCoroutine(MiniGameMovement_Routine());
        }

        private IEnumerator MiniGameMovement_Routine()
        {
            while (true)
            {
                var moveHz = Input.GetAxis("Horizontal");
                var moveVt = Input.GetAxis("Vertical");

                var movement = new Vector3(moveHz, moveVt, 0) * _speed * Time.deltaTime;

                transform.position += movement;
                
                yield return 0;
            }
        }
    }
}