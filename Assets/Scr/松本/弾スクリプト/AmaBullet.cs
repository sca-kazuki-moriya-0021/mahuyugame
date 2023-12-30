using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmaBullet : MonoBehaviour
{
    [SerializeField, Header("�e�̑��x")] private float speed;
    [SerializeField, Header("�����̏��")] private float maxHeight;
    [SerializeField, Header("�����̉���")] private float minHeight;
    [SerializeField, Header("��ʉ����̏��")] private float maxWidth;
    [SerializeField, Header("��ʉ����̉���")] private float minWidth;
    [SerializeField, Header("���]�񐔂̏��")] private int maxFlips = 3;

    private Vector3 direction;
    private Player player;
    private int flipCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 1f), 0).normalized;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float randomSpeed = Random.Range(0.8f, 1.2f) * speed;

        transform.Translate(direction * randomSpeed * Time.deltaTime);
        //�����̏���ɓ��B�����甽�]
        if ((direction.y > 0 && transform.position.y >= maxHeight) || (direction.y < 0 && transform.position.y <= minHeight))
        {
            direction = new Vector3(direction.x, -direction.y, 0);
            flipCount++;
        }

        if ((direction.x > 0 && transform.position.x >= maxWidth) || (direction.x < 0 && transform.position.x <= minWidth))
        {
            direction = new Vector3(-direction.x, direction.y, 0);
            flipCount++;
        }

        if (flipCount >= maxFlips || player.BulletSeverFlag == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
