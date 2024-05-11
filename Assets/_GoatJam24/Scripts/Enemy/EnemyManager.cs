using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Zenject;
using _GoatJam24.Scripts.Player;
using _GoatJam24.Scripts.Game;
using Random = UnityEngine.Random;

namespace _GoatJam24.Scripts.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public List<EnemyController> createdEnemyList;
        public List<EnemyController> _orderEnemyList;

        [SerializeField] private List<EnemyController> _enemyPrefabList;
        [SerializeField] private Vector2 xBorder, yBorder;
        [SerializeField] private float zAxis;
        [SerializeField] private int maxEnemyCount;
        [SerializeField] private Transform enemyParent;
        [SerializeField] private int levelEnemyCount;
        
        [Inject] private PlayerMovement _playerMovement;
        [Inject] private GameManager _gameManager;

        private Coroutine _enemyControlRoutine;
        private int _createdEnemyCount;

        public Transform GetNearestEnemy(Transform player)
        {
            if (createdEnemyList.Count <= 0)
                return null;
            
            var nearestEnemy = createdEnemyList[0];
            foreach (var enemy in createdEnemyList)
            {
                var currentEnemyDis = Vector3.Distance(enemy.transform.position, player.position); 
                var nearestEnemyDis = Vector3.Distance(nearestEnemy.transform.position, player.position);

                if (currentEnemyDis < nearestEnemyDis)
                    nearestEnemy = enemy;
            }

            return nearestEnemy.transform;
        }

        public void StartEnemyCreate()
        {
            _createdEnemyCount = 0;
            _enemyControlRoutine = StartCoroutine(EnemyCreateControl_Routine());
        }

        private IEnumerator EnemyCreateControl_Routine()
        {
            while (_createdEnemyCount < levelEnemyCount)
            {
                if (createdEnemyList.Count < maxEnemyCount)
                    SpawnNewEnemy();
             
                yield return new WaitForSeconds(3);
            }
            
            _gameManager.MiniGameOver(true);
        }

        public void SpawnNewEnemy()
        {
            var enemy = _enemyPrefabList[Random.Range(0, _enemyPrefabList.Count)];
            var randomPos = new Vector3(Random.Range(xBorder.x, xBorder.y), Random.Range(yBorder.x, yBorder.y), zAxis);

            var enemyController = Instantiate(enemy, enemyParent).GetComponent<EnemyController>();
            enemyController.transform.localPosition = randomPos;
            enemyController.Instantiate(this, _playerMovement.transform);
            _orderEnemyList.Add(enemyController);
            _createdEnemyCount++;
        }

        public void Reset()
        {
            if (_enemyControlRoutine is not null)
                StopCoroutine(_enemyControlRoutine);

            foreach (var enemy in createdEnemyList)
            {
                enemy.Death();
                Destroy(enemy.gameObject);
            }

            foreach (var enemy in _orderEnemyList)
            {
                Destroy(enemy.gameObject);
            }

            createdEnemyList.Clear();
            _orderEnemyList.Clear();
            _createdEnemyCount = 0;
        }
    }
}