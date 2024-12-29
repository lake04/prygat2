using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Awake()
    {
      
    }
    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(player.position.x - 0.7f, player.position.y, this.transform.position.z);
        transform.position = targetPos;
    }
}
