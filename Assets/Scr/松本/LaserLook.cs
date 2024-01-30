using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLook : MonoBehaviour
{
    [SerializeField]
    string playerTag = "Player";
    [SerializeField]
    float rotationInterval;

    private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag(playerTag);

        StartCoroutine(Laser());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Laser()
    {
        if(playerObject != null)
        {
            Transform playerTransform = playerObject.transform;

            Vector3 direction = playerTransform.position - transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f,0f,angle + 180);
        }

        yield return new WaitForSeconds(rotationInterval);
    }
}
