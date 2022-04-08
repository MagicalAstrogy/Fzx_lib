using AutoEtd;
using HarmonyLib;
using ZGames.AutoEtd.GameSystem.Battle.States;
using AutoEtd.ViewModels;
using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;
using ZGames.AutoEtd.GameSystem.Battle;

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
        }
    }
}