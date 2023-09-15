using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float minFov = 45f;
    float maxFov = 75f;
    float sensitivity = 30f;
    CinemachineVirtualCamera virtualCamera;


    public void Start() {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    public void Update() {
        float fov = virtualCamera.m_Lens.FieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        virtualCamera.m_Lens.FieldOfView = fov;
    }
}
