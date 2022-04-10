using AutoEtd;
using HarmonyLib;
using ZGames.AutoEtd.GameSystem.Battle.States;
using AutoEtd.ViewModels;
using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;
using System.Xml.Serialization;
using ZGames.AutoEtd.GameSystem.Battle;
using ZGames.AutoEtd.GameSystem.Battle.Conditions;

namespace Fzx_lib
{
    [HarmonyPatch(typeof(App))]
    [HarmonyPatch("OnStartup")]
    public class FactoryPostfix
    {
        static void Postfix(/*...*/)
        {
            Type factoryType = typeof(StateFactory);
            var addFactoryInfo = factoryType.GetRuntimeMethods().First(method => method.Name == "AddFactory");
            var addFactory = (Action< Func<State> >)((Func<State> stat) =>
            {
                addFactoryInfo.Invoke(null, new object[] { stat });
            });
            addFactory(() => new 见切());
            addFactory(() => new 精神涣散());
            addFactory(() => new 我的领域());
            addFactory(() => new 世界());
            addFactory(() => new 衣不遮体());
            addFactory(() => new 多层护甲());
            addFactory(() => new 缝补());
            addFactory(() => new 灵击());
            addFactory(() => new 魔力耗尽());
            
        }
    }
}