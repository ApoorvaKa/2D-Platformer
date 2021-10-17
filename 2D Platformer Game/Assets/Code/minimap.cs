using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public float offset;
    private void LateUpdate() {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        newPosition.z = transform.position.z;
        newPosition.x=newPosition.x+offset;
        transform.position=newPosition;
    }
}
