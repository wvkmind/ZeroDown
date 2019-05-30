using GameFramework.Procedure;
using GameFramework.Event;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace Commando
{
    public class ProcedureBigMap : Commando.ProcedureBase
    {   
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameObject Boundary = GameObject.FindGameObjectWithTag("Boundary");
            Boundary.GetComponent<UnityEngine.Transform>().localScale = new Vector3(Terrain.Config.XBoundary,Terrain.Config.YBoundary/2,1.0f);
            Boundary.GetComponent<UnityEngine.Transform>().position = new Vector3(Terrain.Config.XBoundary,0,0.0f);
            GameEntry.GetComponent<EventComponent>().Subscribe(Terrain.CreateSuccessEventArgs.EventId,OnBigMapInit);
            new Terrain.Generator();
        }
        private void OnBigMapInit(object sender, GameEventArgs e)
        {
            Cinemachine.CinemachineVirtualCamera cv_camera = GameObject.FindGameObjectWithTag("CMvcam").GetComponent<Cinemachine.CinemachineVirtualCamera>();
            var pos = GameObject.FindGameObjectWithTag("Player").transform.position;
            cv_camera.m_Follow.position =  new Vector3(pos.x,pos.y,cv_camera.m_Follow.position.z);
            cv_camera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneHeight=0.1f;
            cv_camera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneWidth=0.1f;
            Loading.loadingUI.gameObject.SetActive(false);
        }
    }
}