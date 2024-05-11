using System;
using System.Collections;
using UnityEngine;
using _GoatJam24.Scripts.Bullet;

namespace _GoatJam24.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent<BulletController>(out var bulletController))
                bulletController.Destroy();
        }

        public void FollowToPlayer(Transform player)
        {
            StartCoroutine(FollowToPlayer_Routine(player));
        }

        private IEnumerator FollowToPlayer_Routine(Transform player)
        {
            while (true)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * _speed);
                yield return 0;
            }
        }
    }
}