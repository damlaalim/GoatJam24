using System;
using _GoatJam24.Scripts.Enemy;
using UnityEngine;
using Zenject;

namespace _GoatJam24.Scripts.Player
{
    public class PlayerCollider : MonoBehaviour
    {
        [Inject] private PlayerController _playerController;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<EnemyController>(out var _enemy) && _playerController.canTakeDamage)
                _playerController.TakeDamage(_enemy.damage, _enemy.transform.position);
        }
    }
}