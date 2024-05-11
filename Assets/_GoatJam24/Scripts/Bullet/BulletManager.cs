using System.Collections.Generic;
using UnityEngine;

namespace _GoatJam24.Scripts.Bullet
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private int _bulletCount = 10;
        [SerializeField] private GameObject _bulletPrefab;
        
        private Stack<BulletController> _bulletStack = new();
        
        private void Start()
        {
            for (var i = 0; i < _bulletCount; i++)
            {
                var bullet = Instantiate(_bulletPrefab, transform).GetComponent<BulletController>();
                bullet.gameObject.SetActive(false);
                bullet.Initialize(this);
                _bulletStack.Push(bullet);
            }
        }

        public BulletController GetBullet()
        {
            if (_bulletStack.Count <= 0)
                return null;
            var bullet = _bulletStack.Pop();
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        public void SetBullet(BulletController bullet)
        {
            _bulletStack.Push(bullet);
        }
    }
}