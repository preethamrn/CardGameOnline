using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {

    Vector3 realPosition;
    Quaternion realRotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!photonView.isMine) {
            //only update over network if card is not mine
            transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            // this is OUR card. Send data to stream
            //OPTIMIZATION?: only SendNext if something has changed.
            //if (realPosition != transform.position || realRotation != transform.rotation) {
            //    realPosition = transform.position;
            //    realRotation = transform.rotation;
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //}
            //TODO: first send an integer defining the nature of change (like changing position/rotation/scale vs sprite vs cards in deck)
        } else {
            // this is someone else's card. Receive position (from a few milliseconds ago) and update
            realPosition = (Vector3) stream.ReceiveNext();
            realRotation = (Quaternion) stream.ReceiveNext();
        }
    }
}
