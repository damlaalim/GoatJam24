using System;
using _GoatJam24.Scripts.Movement;
using _GoatJam24.Scripts.MyExtensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _GoatJam24.Scripts.Player
{
    public class PlayerWorldController : MonoBehaviour
    {
        public bool canTeleport;
        [SerializeField] private Transform initTransform;
        [SerializeField] private float _teleportMouseTime;

        [Inject] private PlanetMovement _planet;
        
        private Camera _mainCam;
        private float _mouseTime;

        private void Start()
        {
            _mainCam = Camera.main;
        }

        public void StartGame()
        {
            Teleport(initTransform.position);    
        }
        
        public void Teleport(Vector3 target)
        {
            transform.DOMove(Vector3.down, .3f).OnComplete(() =>
            {
                transform.DOMove(target, .3f);
            });
            
            transform.up = -target;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _mouseTime = Time.time;
            
            if (canTeleport && Physics.Raycast(_mainCam.ScreenPointToRay(Input.mousePosition), out var hit, 100) 
                            && hit.transform.CompareTag("Planet") && Input.GetMouseButtonUp(0))
            {
                var heldTime = Time.time - _mouseTime;
                if (heldTime > _teleportMouseTime)
                    return;
                
                Teleport(hit.point);
            }
        }
    }
}