using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;

    private Transform _transform;

    void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = new Vector3(playerTransform.position.x, _transform.position.y, _transform.position.z);

    }
    void FixedUpdate()
    {
        

    }
    public void Shake(int level)
    {
        StartCoroutine("ShakeWait", level);
    }

    IEnumerator ShakeWait(int level)
    {
        _transform.position = new Vector3(_transform.position.x, _transform.position.y + (level * 1f), _transform.position.z);
        for (int i = 1; i <= level; i++)
        {
            yield return new WaitForSeconds(0.05f);
            _transform.position = new Vector3(_transform.position.x, _transform.position.y - 1f, _transform.position.z);
        }
    }

    
}
