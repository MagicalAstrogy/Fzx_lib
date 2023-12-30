using System;
using System.Linq;
using HarmonyLib;
using ZGames.AutoEtd.GameSystem;
using ZGames.AutoEtd.GameSystem.Battle;
using ZGames.AutoEtd.GameSystem.Battle.ActiveEffects;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;
using ZGames.AutoEtd.GameSystem.Battle.Battlers;
using ZGames.AutoEtd.GameSystem.Battle.States;

namespace Fzx_lib
{
    
    [HarmonyPatch(typeof(AddState))]
    [HarmonyPatch("Execute")]
    public class AddStatePrefix
    {
        public static bool Prefix(AddState __instance, BattleActionContext context)
        {
            foreach (Battler battler in context.Targets.Where(new Func<Battler, bool>(__instance.CanExecute)))
            {
                if (context.JudgeRandom(__instance.Percentage))
                {
                    var state = StateFactory.Create(__instance.State, __instance.Level);
                    if (state.Kind == StateKind.装备)
                    {
                        var armorBuff = battler.States.OfType<多层护甲>().FirstOrDefault();
                        if (armorBuff != null)
                        {
                            context.ShowMessage(armorBuff.GuardText);
                            armorBuff.Level--;
                            if (armorBuff.Level == 0)
                            {
                                battler.States.Remove(context, armorBuff);
                            }
                            continue;
                        }
                    }
                    battler.States.Add(context, state);
                }
            }

            return false;
        }
        
    }
}