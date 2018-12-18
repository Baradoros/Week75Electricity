using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script causes it's gameobject to snap to the grid on instantiate
/// </summary>
public class T : MonoBehaviour {
    
    private void Start() {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
    }
}
