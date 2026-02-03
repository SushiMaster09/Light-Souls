using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform;

    [Header("Flip Stats")]
    [SerializeField] private float flipScreenTime = 0.5f;

    private Coroutine cameraFaceDirection;

    private PlayerController player;

    private CinemachineFramingTransposer framingTransposer;

    private bool isFacingRight;

    private void Awake()
    {
        player = playerTransform.gameObject.GetComponent<PlayerController>();
        framingTransposer = CameraManager.instance.currentVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Update()
    {
        transform.position = playerTransform.position;
        isFacingRight = player.isFacingRight;
    }

    public void CallCameraFaceDirection()
    {
        cameraFaceDirection = StartCoroutine(FacingDirectionBias());
    }

    private IEnumerator FacingDirectionBias()
    {
        float startPoint = framingTransposer.m_ScreenX;
        float endPoint = DetermineEndPoint();

        float lerpedAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flipScreenTime)
        {
            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(startPoint, endPoint, (elapsedTime / flipScreenTime));
            framingTransposer.m_ScreenX = lerpedAmount;
            yield return null;
        }
    }

    private float DetermineEndPoint()
    {

        if (player.isFacingRight)
        {
            return 0.45f;
        }

        else
        {
            return 0.55f;
        }
    }
}
