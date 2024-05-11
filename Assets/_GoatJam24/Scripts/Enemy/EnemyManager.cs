using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Zenject;
using _GoatJam24.Scripts.Player;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _GoatJam24.Scripts.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public List<EnemyController> createdEnemyList;

        [SerializeField] private List<EnemyController> _enemyPrefabList;
        [SerializeField] private Vector2 xBorder, yBorder;
        [SerializeField] private float zAxis;
        [SerializeField] private int maxEnemyCount;
        [SerializeField] private Transform enemyParent;
        
        [Inject] private PlayerMovement _playerMovement;

        private Coroutine _enemyControlRoutine;

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
            _enemyControlRoutine = StartCoroutine(EnemyCreateControl_Routine());
        }

        private IEnumerator EnemyCreateControl_Routine()
        {
            while (true)
            {
                if (createdEnemyList.Count < maxEnemyCount)
                    SpawnNewEnemy();
             
                yield return new WaitForSeconds(3);
            }
        }

        public void SpawnNewEnemy()
        {
            var enemy = _enemyPrefabList[Random.Range(0, _enemyPrefabList.Count)];
            var randomPos = new Vector3(Random.Range(xBorder.x, xBorder.y), Random.Range(yBorder.x, yBorder.y), zAxis);

            var enemyController = Instantiate(enemy, enemyParent).GetComponent<EnemyController>();
            enemyController.transform.localPosition = randomPos;
            enemyController.Instantiate(this);
            createdEnemyList.Add(enemyController);
            enemyController.FollowToPlayer(_playerMovement.transform);
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

            createdEnemyList.Clear();
        }
    }
}