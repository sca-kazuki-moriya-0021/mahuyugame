using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmaBullet : MonoBehaviour
{
    [SerializeField, Header("弾の速度")] private float speed;
    [SerializeField, Header("高さの上限")] private float maxHeight;
    [SerializeField, Header("高さの下限")] private float minHeight;
    [SerializeField, Header("画面横幅の上限")] private float maxWidth;
    [SerializeField, Header("画面横幅の下限")] private float minWidth;
    [SerializeField, Header("反転回数の上限")] private int maxFlips = 3;

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
        //高さの上限に到達したら反転
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
