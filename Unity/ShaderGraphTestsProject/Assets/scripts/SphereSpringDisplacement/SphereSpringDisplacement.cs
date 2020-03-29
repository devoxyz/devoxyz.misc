using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpringDisplacement : MonoBehaviour
{

    public  SpringJoint     m_SpringJoint = null;
    private Material        m_SphereSprinMaterial = null;
    private Rigidbody       m_RigidBody = null;
    private LineRenderer    m_LineRenderer = null;
    
    void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null)
        {
            m_SphereSprinMaterial = mr.sharedMaterial;
            Transform parent = this.transform.parent;
            m_RigidBody = parent.GetComponent<Rigidbody>();
            if (m_SpringJoint != null && m_SphereSprinMaterial != null)
            {
                m_SphereSprinMaterial.SetVector("_AnchorPoint_Local", this.transform.InverseTransformPoint(parent.TransformPoint(m_SpringJoint.connectedAnchor)));

                m_LineRenderer = m_SpringJoint.GetComponent<LineRenderer>();
            }
        }
    }

    void Update()
    {
        SetSpringForce(this.transform.InverseTransformDirection(m_RigidBody.velocity));
        if (m_LineRenderer != null)
        {
            m_LineRenderer.SetPosition(0, m_SpringJoint.transform.TransformPoint(m_SpringJoint.anchor));
            m_LineRenderer.SetPosition(1, m_SpringJoint.connectedBody.transform.TransformPoint(m_SpringJoint.connectedAnchor));
        }
    }

    private void OnDisable()
    {
        SetSpringForce(Vector3.zero);
    }

    private void SetSpringForce(Vector3 iForce)
    {
        if (m_SphereSprinMaterial != null && m_RigidBody != null)
        {
            m_SphereSprinMaterial.SetVector("_SpringOffset_Local", iForce);
        }
    }
}
