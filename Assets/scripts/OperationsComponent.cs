using UnityEngine;
using System.Collections;

public class OperationsComponent : MonoBehaviour {
    
    public void DeleteThis() {
		Destroy(gameObject);
	}

    private void OnBecameInvisible() {
        transform.position = new Vector3(0, 0, transform.position.z);
    }

}
