using UnityEngine;
using UnityGameFramework.Runtime;

public class MyConfigComponent : GameFrameworkComponent
{
    private readonly ConfigData.Runtime.Manager manager = new ConfigData.Runtime.Manager();
    public ConfigData.Runtime.Manager GetManager()
    {
        return manager;
    }
}
