
using System.Linq;
using ZGames.AutoEtd.GameSystem;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;
using ZGames.AutoEtd.GameSystem.Battle.Battlers;
using ZGames.AutoEtd.GameSystem.Battle.PassiveEffects;
using ZGames.AutoEtd.GameSystem.Battle.States;

namespace Fzx_lib
{
    public class 见切 : BuffState
    {
        public 见切()
        {
            base.MaxLevel = 999;
            base.Turn = 9999;
            base.Level = 10;
            base.LevelDownByTurn = false;
            base.Effects.Add(new SeeCut
            {
                GuardMessage = "但是(target)由于见切的效果，防止了攻击，并反击了敌人！",
                TrapGuard = "但是(target)由于见切的效果，阻止了陷阱的运作！",
                ParentBuff = this
            });
        }
        
        public override string DisplayName
        {
            get
            {
                return string.Format("{0}({1}层)", base.Name, base.Level);
            }
        }
        
        public override bool CanAdd(Battler target)
        {
            return !target.PassiveEffects.OfType<SeeCut>().Any();
        }

        public void EndEffect(BattleActionContext context)
        {
            if (!string.IsNullOrEmpty(this.RemoveByTurnText))
            {
                context.ShowMessage(this.RemoveByTurnText);
            }
            this.Owner.States.Remove(context, this);
        }

        public override bool OnRemoveState(GameContext context)
        {
            context.ShowMessage(string.Format("解除的瞬间，积累的劳累向{0}袭来！", base.Owner));
            BattleActionContext ctx = context as BattleActionContext;
            base.Owner.States.Add(ctx, "精神涣散", 1);
            bool flag = base.OnRemoveState(context);
            return flag;
        }
        
        public override string DescriptionHead => "可以抵挡任何攻击的状态。失去后获得“精神涣散”状态 2 回合。";

        // Token: 0x1700032F RID: 815
        // (get) Token: 0x060009D1 RID: 2513 RVA: 0x000064C2 File Offset: 0x000046C2
        public override string DescriptionTail => "在即将受到一次任何类型伤害时发动：对造成伤害的敌人造成 麻痹，免除此次伤害，然后 该状态 等级 -1。";
    }
}