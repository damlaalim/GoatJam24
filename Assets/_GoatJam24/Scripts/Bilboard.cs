using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Ana kamerayı bul
        mainCamera = Camera.main;
    }

    void Update()
    {
        var target = transform.position + mainCamera.transform.rotation * Vector3.forward;
        target.z = 0;
        // Billboard'un daima kameraya doğru bakmasını sağla
        transform.eulerAngles = target;
        
    }
}