using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    [SerializeField][Range(0, 2)] private float followSpeed = 1;
    [SerializeField] private Transform MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 BGPos = new Vector3(CameraP1.position.x + CameraP2.position.x, CameraP1.position.y + CameraP2.position.y, 0f);
        Vector2.MoveTowards(transform.position, MainCamera.transform.position, followSpeed * Time.deltaTime);
    }
}
