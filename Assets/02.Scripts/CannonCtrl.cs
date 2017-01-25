using UnityEngine;
using System.Collections;

public class CannonCtrl : MonoBehaviour {

    private Transform tr;

    public float rotSpeed = 100.0f;

    private PhotonView pv = null;

    private Quaternion currRot = Quaternion.identity;



	// Use this for initialization
	void Awake () {

        tr = GetComponent<Transform>();

        pv = GetComponent<PhotonView>();

        pv.synchronization = ViewSynchronization.UnreliableOnChange;

        currRot = tr.localRotation;

	}
	
	// Update is called once per frame
	void Update () {

        if (pv.isMine)
        {
            float angle = -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * rotSpeed;

            tr.Rotate(angle, 0, 0); //在yoz平面内围绕锚点旋转            
        }
        else
        {
            tr.localRotation = Quaternion.Slerp(tr.localRotation,currRot,Time.deltaTime*3.0f);
        }

	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
            stream.SendNext(tr.localRotation);
        }
        else
        {
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }


}
