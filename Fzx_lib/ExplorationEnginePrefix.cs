using System.Linq;
using AutoEtd;
using HarmonyLib;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;
using ZGames.AutoEtd.GameSystem.Exploration;

namespace Fzx_lib
{
    
    [HarmonyPatch(typeof(ExplorationEngine))]
    [HarmonyPatch("Begin")]
    public class ExplorationEnginePrefix
    {
        public static void Postfix(ExplorationEngine __instance)
        {
            var player = __instance.Context.Explorer;
            if (player == null) return;
            BattleActionContext ctx = new BattleActionContext(__instance.Context.BattleContext, player);
            var armorSkill = player.SkillInfo.Skills.FirstOrDefault(sk => sk.Name == "多层护甲");
            if (armorSkill != null && armorSkill.Level > 0)
            {
                player.States.Add(ctx, "多层护甲", armorSkill.Level * player.Parameter.Defence / 40);
            }

            {
                var spiritualSkill = player.SkillInfo.Skills.FirstOrDefault(sk => sk.Name == "灵击");
                if (spiritualSkill != null && spiritualSkill.Level > 0)
                {
                    player.States.Add(ctx, "灵击", spiritualSkill.Level * 2);
                }
            }
        }
    }
}