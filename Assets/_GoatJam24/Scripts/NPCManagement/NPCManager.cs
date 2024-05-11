using System.Collections.Generic;
using UnityEngine;

namespace _GoatJam24.Scripts.NPCManagement
{
    public class NPCManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> _npcList;
        [SerializeField] private List<Transform> _transformList;

        private List<Transform> _createdTransformList = new();
        
        public void StartGame()
        {
            _createdTransformList = _transformList;
            
            foreach (var npc in _npcList)
            {
                var selectedTransform = _createdTransformList[Random.Range(0, _createdTransformList.Count)];
                
                var newNpc = Instantiate(npc, selectedTransform);
                newNpc.localPosition = Vector3.zero;
                newNpc.up = -selectedTransform.position;
                
                _createdTransformList.Remove(selectedTransform);
            }
        }
    }
}