using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    Camera mainCamera;
    Ray ray;
    RaycastHit hitInfo;

    void Start()
    {
        mainCamera = Camera.main;
    }
    // T‰ss‰ p‰ivitys t‰h‰n luokkaan. Oli lis‰tty youtube videon kommenteissa.
    // P‰ivitys est‰‰ sen ett‰ pelaaja ei ammu sivuille tai taakse p‰in
    void Update()
    {
        ray.origin = mainCamera.transform.position;
        ray.direction = mainCamera.transform.forward;
        if (Physics.Raycast(ray, out hitInfo))
        {

            transform.position = hitInfo.point;

        }
        else
        {

            transform.position = ray.origin + ray.direction * 1000.0f;

        }
    }
    // Update is called once per frame
    //void Update()
    //{
    //    ray.origin = mainCamera.transform.position;
    //    ray.direction = mainCamera.transform.forward;
    //    Physics.Raycast(ray, out hitInfo);
    //    transform.position = hitInfo.point;
    //}


}
