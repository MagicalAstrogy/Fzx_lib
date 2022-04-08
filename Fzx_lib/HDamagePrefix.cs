using System.Linq;
using HarmonyLib;
using ZGames.AutoEtd.GameSystem;
using ZGames.AutoEtd.GameSystem.Battle.ActiveEffects;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;

namespace Fzx_lib
{
    
    [HarmonyPatch(typeof(HAttack))]
    [HarmonyPatch("Execute")]
    public class HDamagePrefix
    {
        static bool Prefix( BattleActionContext context)
        {
            if (context.Target == context.Actor)
                return true;
            var target = context.Target;
            DamageContext damageContext = new DamageContext(context, target);
            foreach (SeeCut seeCut in target.PassiveEffects.OfType<SeeCut>())
            {
                damageContext.ShowMessage(seeCut.GuardMessage);
                seeCut.ParentBuff.Level--;
                BattleActionContext exec = new BattleActionContext(context.BattleContext, target);
                if (context.JudgeRandom(70))
                    context.Actor.States.Add(exec, "麻痹", 1);
                if (seeCut.Level == 0)
                {
                    seeCut.ParentBuff.EndEffect(context);
                }
                return false;
            }

            return true;
        }
    }
}