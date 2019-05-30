using UnityGameFramework.Runtime;
using System.Collections;
using GameFramework.DataNode;

namespace Commando
{

    namespace Path
    {
        public class PathAnalysis
        {
            private static  DataNodeComponent dataNodeComponent;
            private static EntityComponent entityComponent;
            private static EventComponent eventComponent;
            private  PlayerLogic owner;
            private int no_have_path = 0;
            private ArrayList data = new ArrayList();
            public PathAnalysis(PlayerLogic _owner)
            {
                if(dataNodeComponent==null)dataNodeComponent = GameEntry.GetComponent<DataNodeComponent>();
                if(entityComponent==null)entityComponent = GameEntry.GetComponent<EntityComponent>();
                if(eventComponent==null)eventComponent = GameEntry.GetComponent<EventComponent>();
                owner = _owner;
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
                int owner_id = owner.cur_block_id;
                int width = Commando.Terrain.Config.Width;
                int owner_y = owner_id/width;
                int owner_x = owner_id%width;

                int _x = owner_x + x;
                int _y = owner_y + y;
                return !Terrain.BigMapInfo.BlockIsStackableForPath(_x,_y);
            }
            
            public void DrawCircle(int Weight)
            {
                if(Weight>owner.CanWalkSteps)
                {
                    return;
                }
                else
                {
                    int owner_id = owner.cur_block_id;
                    int width = Commando.Terrain.Config.Width;
                    int owner_y = owner_id/width;
                    int owner_x = owner_id%width;
                    for(int x = -Weight;x<=Weight;x++)
                    {
                        for(int y = -Weight;y<=Weight;y++)
                        {
                            IDataNode s = dataNodeComponent.GetNode("PathNode." + owner.EntityId + "." + x + "." + y);
                            if(owner_x+x>=0&& owner_x+x<Terrain.Config.Width && owner_y+y>=0&& owner_y+y<Terrain.Config.Height &&(s==null|| s.GetData<VarInt>().Value!= Weight))
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
                        data.Add(new Path.Struct{
                            Weight = weight,
                            Owner = owner,
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
