using UnityGameFramework.Runtime;
using GameFramework.Fsm;
using System.Collections;
using UnityEngine;

namespace  Commando
{
    public class PlayerLogic : EntityLogic
    {
        public struct InitData
        {
            public int id;
            public int CanWalkSteps;
            public int EntityId;
        }
        public int cur_block_id;
        public int CanWalkSteps;
        public int EntityId;
        private IFsm<PlayerLogic> mFsm;

        public ArrayList work_id_list = new ArrayList();
		public int end_id ;
		public int work_index = -1;

        public EntityComponent entityComponent;
        protected PlayerLogic()
        {
            entityComponent = GameEntry.GetComponent<EntityComponent>();
        }


        protected override void  OnInit (object userData) {
            base.OnShow (userData);

            InitData data = (InitData) userData;
            cur_block_id = data.id;
            CanWalkSteps = data.CanWalkSteps;
            EntityId = data.EntityId;
            FsmComponent fsmComponent = GameEntry.GetComponent<FsmComponent>();
            FsmState<PlayerLogic>[] _playerStates = new FsmState<PlayerLogic>[]{
                new Fsm.Idle(),
                new Fsm.Path(),
                new Fsm.Walk()
            };
            mFsm = fsmComponent.CreateFsm(this,_playerStates);

            int width = Commando.Terrain.Config.Width;
            int id = cur_block_id;
            int y = id/width;
            int x = id%width;
            float  X = 0.5f*x+0.5f+y*0.5f;
            float  Y = y * 0.25f - 0.38f - (y * 0.5f) + x * 0.25f;
            float  Z = 0.7f;
            SetInit(X,Y,Z);
        }

        protected override void  OnShow (object userData) {
            base.OnShow (userData);
            mFsm.Start<Fsm.Idle>();
        }

        public void DeletePath()
        {
            foreach (var child in entityComponent.GetEntityGroup("PathBlock").GetAllEntities())
            {
               entityComponent.HideEntity(child.Id);
            }
        }
        public void DrawPath()
        {
            Path.PathAnalysis analysis = new Path.PathAnalysis(this);
            Path.Struct [] path_datas = (Path.Struct[]) analysis.CreatePath().ToArray(typeof(Path.Struct));
            foreach (var data in path_datas)
            {
                var entity_id =EntityHelper.EntityCreate.GenerateId();
                entityComponent.ShowEntity<Path.Logic>(entity_id, "Assets/Prefab/Characters/Path.prefab", "PathBlock", 0, new Path.Logic.Struct
                {
                    Owner = this,
                    EntityId = entity_id,
                    Weight = data.Weight,
                    RelatedX = data.RelatedX,
                    RelatedY = data.RelatedY
                });
            }
        }
        public void SetInit(float x,float y,float z)
		{
			transform.localPosition = new UnityEngine.Vector3(x, y+z, y+100.0f);
		}
		public void SetPos(float x,float y,float z)
		{
			
			transform.localPosition = Vector3.MoveTowards(transform.localPosition,new UnityEngine.Vector3(x, y+z, y+100.0f),3*Time.deltaTime);
		}
        
        private bool Equals(float a,float b)
		{
			return System.Math.Abs(a - b) < 0.0005f;
		}
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds,realElapseSeconds);
			if(work_index!=-1)
			{
				int width = Commando.Terrain.Config.Width;

                int id = cur_block_id;
                int x = id%width;
                int y = id/width;
                id = (int)work_id_list[work_index];
                int nx = id%width;
                int ny = id/width;

                if(x==nx)
                {
                    if(y<ny)
                        GetComponentInChildren<FootSoldier>().Walk(true);
                    else
                        GetComponentInChildren<FootSoldier>().Walk(false);
                }
                else
                {
                    if(x<nx)
                        GetComponentInChildren<FootSoldier>().Walk(true);
                    else
                        GetComponentInChildren<FootSoldier>().Walk(false);
                }

                x = nx;
                y = ny;
				
				float X = 0.5f*x+0.5f+y*0.5f;
                float  Y = y * 0.25f - 0.38f - (y * 0.5f) + x * 0.25f;
                float  Z = 0.7f;

				if(!Equals(transform.localPosition.x,X)&&!Equals(transform.localPosition.y,Y+Z)&&!Equals(transform.localPosition.z,Y+100.0f))
				{
					SetPos(X,Y,Z);
				}
				else
				{
                    cur_block_id = (int)work_id_list[work_index];
					work_index++;
					if(work_index==work_id_list.Count)
					{
						work_index = -1;
						work_id_list.Clear();
						end_id = id;
					}
				}
			}
		}
    }
}
