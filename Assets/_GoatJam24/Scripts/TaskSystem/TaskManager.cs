using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace _GoatJam24.Scripts.TaskSystem
{
    public class TaskManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> tasks;
        [SerializeField] private GameObject taskThick;

        private int _taskNumber;

        private void Start()
        {
            _taskNumber = 0;
            foreach (var task in tasks)
            {
                task.transform.localScale = Vector3.zero;
            }
        }

        public void StartGame()
        {
            tasks[_taskNumber].transform.DOScale(Vector3.one, .3f);
        }

        public void NextTask(int completedTask)
        {
            if (_taskNumber != completedTask - 1)
                return;
            
            StartCoroutine(CompleteRoutine());
            
            IEnumerator CompleteRoutine()
            {
                taskThick.transform.localScale = Vector3.zero;
                taskThick.SetActive(true);
                taskThick.transform.DOScale(Vector3.one, .3f).SetEase(Ease.OutBounce);

                yield return new WaitForSeconds(.5f);

                tasks[_taskNumber].transform.DOScale(Vector3.zero, .3f);
                taskThick.transform.DOScale(Vector3.zero, .3f);
                _taskNumber++;
                if (_taskNumber < 3)
                    tasks[_taskNumber].transform.DOScale(Vector3.one, .3f);
            }
        }
    }
}