using System.Linq;
using AutoEtd;
using HarmonyLib;
using ZGames.AutoEtd.GameSystem;
using ZGames.AutoEtd.GameSystem.Battle.ActiveEffects;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;
using ZGames.AutoEtd.GameSystem.Battle.Battlers;
using ZGames.AutoEtd.GameSystem.Battle.PassiveEffects;

namespace Fzx_lib
{
    
    [HarmonyPatch(typeof(Damage))]
    [HarmonyPatch("ExecuteSingle")]
    public class DamagePrefix
    {
        static bool Prefix( BattleActionContext battleActionContext, Battler target)
        {
            DamageContext damageContext = new DamageContext(battleActionContext, target);
            foreach (SeeCut seeCut in target.PassiveEffects.OfType<SeeCut>())
            {
                damageContext.ShowMessage(seeCut.GuardMessage);
                seeCut.ParentBuff.Level--;
                BattleActionContext exec = new BattleActionContext(battleActionContext.BattleContext, target);
                if (battleActionContext.JudgeRandom(70))
                    battleActionContext.Actor.States.Add(exec, "麻痹", 1);
                if (seeCut.Level == 0)
                {
                    seeCut.ParentBuff.EndEffect(battleActionContext);
                }
                return false;
            }

            return true;
        }
    }
}