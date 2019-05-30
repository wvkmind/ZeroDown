using UnityGameFramework.Runtime;
using System.Collections;
using GameFramework.DataNode;

namespace Commando
{
    namespace Entity
    {
        namespace Logic
        {
            public class PathAnalysis
            {
                private static  DataNodeComponent dataNodeComponent;
                private static EntityComponent entityComponent;
                private static EventComponent eventComponent;
                private  Logic.Character.CharacterStruct owner;
                private  Logic.Character.CharacterStruct [] other;
                private int no_have_path = 0;
                private ArrayList data = new ArrayList();
                public PathAnalysis(Logic.Character.CharacterStruct _owner,Logic.Character.CharacterStruct [] _other)
                {
                    if(dataNodeComponent==null)dataNodeComponent = GameEntry.GetComponent<DataNodeComponent>();
                    if(entityComponent==null)entityComponent = GameEntry.GetComponent<EntityComponent>();
                    if(eventComponent==null)eventComponent = GameEntry.GetComponent<EventComponent>();
                    owner = _owner;
                    other = _other;
                }
                public int RelatedHas(int entity_id,int x,int y,int maxWeight)
                {
                    IDataNode u = dataNodeComponent.GetNode("PathNode." + entity_id + "." + x + "." + (y + 1));
                    IDataNode d = dataNodeComponent.GetNode("PathNode." + entity_id + "." + x + "." + (y - 1));
                    IDataNode l = dataNodeComponent.GetNode("PathNode." + entity_id + "." + (x+1) + "." + y);
                    IDataNode r = dataNodeComponent.GetNode("PathNode." + entity_id + "." + (x-1) + "." + y);
                    int s_Weight = int.MaxValue;
                    if (u!=null&&u.GetData<VarInt>().Value< maxWeight)
                    {
                        s_Weight = u.GetData<VarInt>().Value;
                    }
                    else if (d!=null && d.GetData<VarInt>().Value < maxWeight&&s_Weight>d.GetData<VarInt>().Value)
                    {
                        s_Weight = d.GetData<VarInt>().Value;
                    }
                    else if (l!=null && l.GetData<VarInt>().Value < maxWeight&&s_Weight>l.GetData<VarInt>().Value)
                    {
                        s_Weight = l.GetData<VarInt>().Value;
                    }
                    else if (r!=null && r.GetData<VarInt>().Value < maxWeight&&s_Weight>r.GetData<VarInt>().Value)
                    {
                        s_Weight = r.GetData<VarInt>().Value;
                    }
                    if(s_Weight==int.MaxValue)
                        return s_Weight;
                    else
                        return s_Weight+1;
                }
                public bool IsBlock(int x,int y)
                {
                    int _x = owner.x + x;
                    int _y = owner.y + y;
                    bool pre_flag = false;
                    IDataNode node = dataNodeComponent.GetNode("Ground." + _x + "." + _y);
                    if (node!=null)
                    {
                        Ground.HeightType _type = ((Ground.HeightType)(dataNodeComponent.GetData<VarObject>("Ground." + _x + "." + _y).Value));
                        pre_flag = _type == Ground.HeightType.Highland;
                    }
                    else
                    {
                        pre_flag = false; 
                    }
                    foreach (var o in other)
                    {
                        if(o.x == _x||o.y==_y)
                        {
                            pre_flag = true;
                            break;
                        }
                    }

                    return pre_flag;
                }
                
                public void DrawCircle(int Weight)
                {
                    if(Weight>owner.CanWalkSteps)
                    {
                        return;
                    }
                    else
                    {
                        for(int x = -Weight;x<=Weight;x++)
                        {
                            for(int y = -Weight;y<=Weight;y++)
                            {
                                IDataNode s = dataNodeComponent.GetNode("PathNode." + owner.EntityId + "." + x + "." + y);
                                if(owner.x+x>=-Ground.XBoundary&& owner.x+x<Ground.XBoundary && owner.y+y>=-Ground.YBoundary&& owner.y+y<Ground.YBoundary &&(s==null|| s.GetData<VarInt>().Value!= Weight))
                                {
                                    int d = System.Math.Abs(x) + System.Math.Abs(y);
                                    if (d <= Weight && d > Weight - 1)
                                    {
                                        if(!IsBlock( x, y))
                                        {
                                            int s_Weight = RelatedHas( owner.EntityId, x, y, owner.CanWalkSteps);
                                            if (s_Weight!=int.MaxValue||Weight == 1)
                                            {
                                                if(Weight == 1&&s_Weight==int.MaxValue)s_Weight = 1;
                                                dataNodeComponent.SetData("PathNode." + owner.EntityId + "." + x + "." + y, new VarInt(s_Weight));
                                            }
                                            else
                                            {
                                                no_have_path = no_have_path + 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        DrawCircle(Weight+1);
                    }
                }
                public ArrayList CreatePath()
                {
                    no_have_path = 0;
                    DrawCircle(1);
                    
                    int end = no_have_path;
                    for(int i=0;i< end;i++)
                        DrawCircle(1);
                    IDataNode main_path =  dataNodeComponent.GetNode("PathNode." + owner.EntityId);
                    foreach (var xchild in main_path.GetAllChild())
                    {
                        int x = int.Parse(xchild.Name);
                        foreach(var ychild in xchild.GetAllChild())
                        {
                            int y = int.Parse(ychild.Name);
                            int weight = ychild.GetData<VarInt>().Value;
                            data.Add(new Logic.PathBlock.PathBlockStruct{
                                Weight = weight,
                                RelatedX = x,
                                RelatedY = y
                            });
                        }
                    }
                    main_path.Clear();
                    return data;
                }

            }
        }
    }
}
