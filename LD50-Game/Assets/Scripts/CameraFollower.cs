using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_zOffset;


    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = new Vector3(this.m_target.transform.position.x, this.transform.position.y, this.m_target.position.z + this.m_zOffset);
    }
}