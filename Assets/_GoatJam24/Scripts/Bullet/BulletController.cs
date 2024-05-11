using UnityEngine;
using System.Collections;
using DG.Tweening;
using _GoatJam24.Scripts.MyExtensions;

namespace _GoatJam24.Scripts.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed, targetDis;
        public void Destroy()
        {
            gameObject.SetActive(false);
        }
        
        public void Move(Transform target)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position);
            StartCoroutine(Move_Routine(target));
        }

        private IEnumerator Move_Routine(Transform target)
        {
            var heading = target.position - transform.position;
            var dis = heading.magnitude;
            var dir = heading / dis;
            var initPos = transform.position;

            while (gameObject.activeSelf)
            {
                var currentPos = initPos + dir * targetDis;
                transform.position = Vector3.MoveTowards(transform.position, currentPos, speed * Time.deltaTime).With(z: transform.position.z);
                yield return 0;
            }
            
        } 
    }
}