using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuibi : MonoBehaviour
{
    [SerializeField] float limit; // �ő�����x
    Vector3 velocity; // ���x
    Vector3 position; // �ʒu
    Transform target; // �^�[�Q�b�g
    [SerializeField] float maxSpeed; // �ő呬�x
    [SerializeField] float slowingDistance; // ���B�������n�߂鋗��
    [SerializeField] float timeToTarget; // �ڕW�ɓ��B���鎞��

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        // �~�T�C���̏����ʒu��ݒ�
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 desiredVelocity = (target.position - position);
        float distance = desiredVelocity.magnitude;

        if (distance < slowingDistance)
        {
            // ���B�������J�n
            float rampedSpeed = maxSpeed * (distance / slowingDistance);
            float clippedSpeed = Mathf.Min(rampedSpeed, maxSpeed);
            desiredVelocity = desiredVelocity.normalized * clippedSpeed;
        }
        else
        {
            desiredVelocity = desiredVelocity.normalized * maxSpeed;
        }

        Vector3 acceleration = (desiredVelocity - velocity) / timeToTarget;

        // ���x����
        if (velocity.magnitude > limit)
        {
            velocity = velocity.normalized * limit;
        }

        // ���x�ƈʒu�̍X�V
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        transform.position = position;
    }
}
