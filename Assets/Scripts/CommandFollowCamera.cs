using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandFollowCamera : MonoBehaviour
{
    public Transform cameraTransform; // 摄像机的Transform
    public float distanceFromCamera = 100.0f; // UI与摄像机的距离
    public Vector3 offset = new Vector3(0, 0, 0); // 位置偏移
    public float positionLerpSpeed = 20.0f; // 平滑移动速度
    public float rotationLerpSpeed = 20.0f; // 平滑旋转速度

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // 计算目标位置
        Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera + offset;

        // 使用插值平滑移动到目标位置
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * positionLerpSpeed);

        // 计算目标旋转，使UI始终面向摄像机
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - cameraTransform.position);

        // 使用插值平滑旋转到目标旋转
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationLerpSpeed);
    }
}
