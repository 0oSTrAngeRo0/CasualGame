using System;
using Game;
using UnityEngine;

namespace Game
{
    public class GrabController : MonoController
    {
        [SerializeField] private Camera m_Cam;
        [SerializeField] private float m_RayRange;

        private void Start()
        {
            this.GetModel<IGrabModel>().CurrentInteractive.Register((old, current) =>
            {
                old?.OnUnselected();
                current?.OnSelected();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            Vector3 aim = m_Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            bool isHit = Physics.Raycast(aim, m_Cam.transform.forward, out RaycastHit hit, m_RayRange);
            IInteractive target = isHit ? hit.transform.gameObject.GetComponent<IInteractive>() : null;
            this.GetModel<IGrabModel>().CurrentInteractive.Value = target;
        }
    }
}