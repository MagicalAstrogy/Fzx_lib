using System.Linq;
using HarmonyLib;
using ZGames.AutoEtd.GameSystem;
using ZGames.AutoEtd.GameSystem.Battle.ActiveEffects;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;
using ZGames.AutoEtd.GameSystem.Exploration.Traps;

namespace Fzx_lib
{
    
    [HarmonyPatch(typeof(HAttack))]
    public class HDamagePrefix
    {
        
        [HarmonyPatch("Execute")]
        [HarmonyPrefix]
        static bool Prefix(HAttack __instance, BattleActionContext context)
        {
            if (context.Target == context.Actor)
                return true;
            var target = context.Target;
            DamageContext damageContext = new DamageContext(context, target);
            foreach (SeeCut seeCut in target.PassiveEffects.OfType<SeeCut>())
            {
                context.ShowMessageIfNotNull(__instance.PreText);
                seeCut.ParentBuff.Level--;
                if (seeCut.Level == 0)
                {
                    seeCut.ParentBuff.EndEffect(context);
                }

                if (context.Actor is Trap)
                {
                    damageContext.ShowMessage(seeCut.TrapGuard);
                    return false;
                }
                damageContext.ShowMessage(seeCut.GuardMessage);

                BattleActionContext exec = new BattleActionContext(context.BattleContext, target);
                if (context.JudgeRandom(70))
                    context.Actor.States.Add(exec, "麻痹", 1);

                
                return false;
            }

            return true;
        }
    }
}