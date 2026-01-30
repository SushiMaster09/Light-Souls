using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform;

    [Header("Flip Stats")]
    [SerializeField] private float flipRotationTime = 0.5f;

    private Coroutine turnCoroutine;

    private PlayerController player;

    private bool isFacingRight;

    private void Awake()
    {
        player = playerTransform.gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        transform.position = playerTransform.position;
        isFacingRight = player.isFacingRight;
    }

    public void CallTurn()
    {
        turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.eulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < flipRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / flipRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        if (isFacingRight)
        {
            return 180f;
        }
        else
        {
            return 0f;
        }
    }
}
