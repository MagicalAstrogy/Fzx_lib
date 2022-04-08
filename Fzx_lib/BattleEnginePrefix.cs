using System;
using System.Linq;
using System.Reflection;
using AutoEtd;
using HarmonyLib;
using ZGames.AutoEtd.GameSystem.Battle;
using ZGames.AutoEtd.GameSystem.Battle.Battlers;

namespace Fzx_lib
{
    
    [HarmonyPatch(typeof(BattleEngine))]
    [HarmonyPatch("OnAction")]
    public class BattleEnginePrefix
    {
        private static MethodInfo processAction = null;
        private static MethodInfo judgeEnd = null;
        public static bool Prefix(BattleEngine __instance, ref BattleResult __result)
        {
            if (processAction == null)
            {
                processAction = AccessTools.Method(typeof(BattleEngine), "ProcessAction");
                judgeEnd = AccessTools.Method(typeof(BattleEngine), "JudgeEnd");
            }
            var explorer = __instance.Context.PartyA.Lives.FirstOrDefault();
            if (explorer == null)
                return true;
            if (explorer.States.OfType<我的领域>().Any())
            {
                __instance.Context.ShowMessage(String.Format("在{0}的领域中，一切都被静止。", explorer));
                processAction.Invoke(__instance, new object[]{explorer});
                BattleResult battleResult = (BattleResult)judgeEnd.Invoke(__instance, null);
                __result = battleResult;
                return false;
            }
            return true;
        }
    }
}