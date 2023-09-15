using UnityEngine;
using Cinemachine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] readonly bool freezeXZAxis = true;

    private Transform _cameraTransform;

    private void Awake() {
        _cameraTransform = Camera.main.transform;
    }

    private void OnEnable() {
        CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
    }

    private void OnDisable() {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(OnCameraUpdated);
    }

    void OnCameraUpdated(CinemachineBrain brain) {
        transform.forward = _cameraTransform.forward;
    }

    // Update is called once per frame
    // void LateUpdate()
    // {
    //     if (freezeXZAxis) {
    //         transform.rotation = Quaternion.Euler(0f,Camera.main.transform.rotation.eulerAngles.y,0f);
    //     }
    //     else {
    //         transform.rotation = Camera.main.transform.rotation;
    //     }
    // }
}
