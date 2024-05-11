using System;
using System.Collections;
using _GoatJam24.Scripts.Game;
using _GoatJam24.Scripts.Player;
using UnityEngine;
using Zenject;

namespace _GoatJam24.Scripts.Movement
{
    public class PlanetMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        [Inject] private GameManager _gameManager;
        [Inject] private PlayerWorldController _playerWorldController;
        
        private Camera _cam;
        
        private void Start()
        {
            _cam = Camera.main;
        }

        private void OnMouseDrag()
        {
            if (!_gameManager.PlanetCanMovement)
                return;
            _playerWorldController.canTeleport = false;

            var rotX = Input.GetAxis("Mouse X") * _speed;
            var rotY = Input.GetAxis("Mouse Y") * _speed;

            var right = Vector3.Cross(_cam.transform.up, transform.position - _cam.transform.position);
            var up = Vector3.Cross(transform.position - _cam.transform.position, right);
            transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
            transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;
        }

        private void OnMouseUp()
        {
            if (!_gameManager.PlanetCanMovement)
                return;
            
            _playerWorldController.canTeleport = true;
        }
    }
}
