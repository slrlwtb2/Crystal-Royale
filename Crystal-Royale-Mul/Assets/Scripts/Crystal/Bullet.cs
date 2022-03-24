using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks,IPunObservable
{
	public GameObject tar;
	private Transform target;

	public float speed = 70f;


	//public void Seek(Transform _target)
	//{
	//	target = _target;
	//}
	public void Seek(Transform _target)
	{
		if (photonView.IsMine)
		{
			//tar = new GameObject();
			//      tar.transform.SetPositionAndRotation(_target, rot);
			//target = tar.transform;
			target = _target;
			if (target.gameObject.tag == "Player")
			{
            //if (target.gameObject.GetComponent<PlayerInfo>() != null)
            //{
            if (target.gameObject.GetComponent<PlayerInfo>().getkill)
            {
                PhotonNetwork.Destroy(this.gameObject);
				//Destroy(this.gameObject);
            }
            //}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (photonView.IsMine)
		{
			if (target == null)
			{
				PhotonNetwork.Destroy(gameObject);
				//Destroy(gameObject);
				return;
			}

			Vector3 dir = target.position - transform.position;
			float distanceThisFrame = speed * Time.deltaTime;

			if (dir.magnitude <= distanceThisFrame)
			{
				return;
			}

			transform.Translate(dir.normalized * distanceThisFrame, Space.World);
			transform.LookAt(target);
		}
	}

    private void OnTriggerEnter(Collider other)
    {
		if (photonView.IsMine)
		{
			if (other.gameObject.tag == "Player")
			{
				other.gameObject.GetComponent<PlayerController3>().TakeDamage(20);
				PhotonNetwork.Destroy(this.gameObject);
			}
			if (other.gameObject.tag == "Monster")
			{
				other.gameObject.GetComponent<Monster>().TakeDamage(30);
				PhotonNetwork.Destroy(this.gameObject);
			}
		}
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //stream.SendNext(target.transform.position);
            //stream.SendNext(target.transform.rotation);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        if (stream.IsReading)
        {
            //target.transform.position = (Vector3)stream.ReceiveNext();
            //target.transform.rotation = (Quaternion)stream.ReceiveNext();
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}


