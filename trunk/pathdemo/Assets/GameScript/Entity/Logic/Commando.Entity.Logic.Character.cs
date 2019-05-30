using UnityGameFramework.Runtime;
using GameFramework.Fsm;
using GameFramework.Event;
using System;

namespace Commando
{
    namespace Entity
    {
        namespace Logic
        {
            public class Character : EntityLogic
            {

                public struct CharacterStruct 
                {
                    public int EntityId;
                    public ulong GroundId;
                    public int CanWalkSteps;
                    public int x;
                    public int y;
                }

                public CharacterStruct Data;

                private IFsm<Character> mFsm;

                EntityComponent entityComponent;
                FsmComponent fsmComponent;
                DataNodeComponent dataNodeComponent;
                EventComponent eventComponent;
                protected Character()
                { 
                }

                protected override void OnShow (object userData) 
                {
                    base.OnShow (userData);


                }

                protected override void OnInit (object userData) 
                {
                    base.OnInit(userData);
                    fsmComponent = GameEntry.GetComponent<FsmComponent>();
                    entityComponent = GameEntry.GetComponent<EntityComponent>();
                    dataNodeComponent = GameEntry.GetComponent<DataNodeComponent>();
                    eventComponent = GameEntry.GetComponent<EventComponent>();
                    Data = (CharacterStruct)userData;
                    SetPos(Data.x,Data.y);
                    GetComponent<UnityEngine.SpriteRenderer>().sortingLayerName = "Characters";
                    FsmState<Character>[] _playerStates = {
                        new Fsm.PlayerStay(),
                        new Fsm.PlayerWalk()
                    };
                    mFsm = fsmComponent.CreateFsm(this, _playerStates);
                    mFsm.Start<Fsm.PlayerStay>();
                }
                public void SetPos(int x,int y)
                {
                    transform.localPosition = new UnityEngine.Vector3(x+0.5f, y+0.5f, 0);
                }
                public void DrawPath()
                {
                    
                    PathAnalysis analysis = new PathAnalysis(this.Data,Helper.Character.GetOtherData());
                    Logic.PathBlock.PathBlockStruct [] path_datas = (Logic.PathBlock.PathBlockStruct[]) analysis.CreatePath().ToArray(typeof(Logic.PathBlock.PathBlockStruct));
                    foreach (var child in entityComponent.GetEntityGroup("PathBlock").GetAllEntities())
                    {
                    child.OnHide(null);
                    }
                    foreach (var data in path_datas)
                    {
                        var entity_id = Helper.EntityCreate.GenerateId();
                        entityComponent.ShowEntity<PathBlock>(entity_id, "Assets/Prefab/Path.prefab", "PathBlock", 0, new Logic.PathBlock.PathBlockStruct
                        {
                            Owner = this,
                            EntityId = entity_id,
                            Weight = data.Weight,
                            RelatedX = data.RelatedX,
                            RelatedY = data.RelatedY
                        });
                    }
                }
            }
        }
    }
}