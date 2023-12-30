using HarmonyLib;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;

namespace Fzx_lib
{
    [HarmonyPatch(typeof(BattleAction))]
    public class BattleActionPrefix
    {
        
        [HarmonyPatch("JudgeHit")]
        [HarmonyPrefix]
        static bool Prefix2(ref bool __result, int basePercentage)
        {
            if (basePercentage == 0)//在概率为 0 的场合，不会再命中。
            {
                __result = false;
                return false;
            }

            return true;
        }
    }
}