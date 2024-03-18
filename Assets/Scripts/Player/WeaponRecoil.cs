using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook playerCamera;
    public Cinemachine.CinemachineImpulseSource cameraShake;

    public Vector2[] recoilPattern;
    public float verticalRecoil;
    public float horizontalRecoil;
    public float duration;
    float time;
    int index;

    // Start is called before the first frame update
    void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            playerCamera.m_YAxis.Value -= ((verticalRecoil / 1000) * Time.deltaTime) / duration;
            playerCamera.m_XAxis.Value -= ((verticalRecoil / 10) * Time.deltaTime) / duration;
            time -= Time.deltaTime;
        }
    }

    public void Reset()
    {
        index = 0;
    }

    public void GenerateRecoil(string weaponName)
    {
        time = duration;
        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        horizontalRecoil = recoilPattern[index].x;
        verticalRecoil = recoilPattern[index].y;

        index = NextIndex(index);

        //rigController.Play("weapon_recoil");
    }

    int NextIndex(int index)
    {
        return (index + 1) % recoilPattern.Length;
    }

}
