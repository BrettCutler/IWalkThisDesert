using System;
using UnityEngine;

namespace Unity
{
    public class CameraRefocus
    {
        public Camera Camera;
        public Vector3 Lookatpoint;
        public Transform Parent;

        private Vector3 m_OrigCameraPos;
        private Quaternion m_OrigCameraRot;
        private bool m_Refocus;


        public CameraRefocus(Camera camera, Transform parent, Vector3 origCameraPos, Quaternion origCameraRot)
        {
            m_OrigCameraPos = origCameraPos;
            m_OrigCameraRot = origCameraRot;
            Camera = camera;
            Parent = parent;
        }


        public void ChangeCamera(Camera camera)
        {
            Camera = camera;
        }


        public void ChangeParent(Transform parent)
        {
            Parent = parent;
        }


        public void GetFocusPoint()
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Parent.transform.position + m_OrigCameraPos, Parent.transform.forward + m_OrigCameraRot.eulerAngles, out hitInfo,
                                100f))
            {
                Lookatpoint = hitInfo.point;
                m_Refocus = true;
                return;
            }
            m_Refocus = false;
        }


        public void SetFocusPoint()
        {
            if (m_Refocus)
            {
                Camera.transform.LookAt(Lookatpoint);
            }
        }
    }
}
