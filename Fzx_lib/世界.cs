using System.Linq;
using ZGames.AutoEtd.GameSystem;
using ZGames.AutoEtd.GameSystem.Battle;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;
using ZGames.AutoEtd.GameSystem.Battle.Battlers;
using ZGames.AutoEtd.GameSystem.Battle.States;
using ZGames.AutoEtd.GameSystem.Exploration.Explorers;

namespace Fzx_lib
{
    public class 世界 : BuffState, IPlanActionOverride
    {
        public 世界()
        {
            base.Turn = 5;
            base.MaxLevel = 1;
            base.LevelDownByTurn = false;
        }
        
        public override bool CanAdd(Battler target)
        {
            return !target.States.OfType<世界>().Any();
        }
        
        public override string DisplayName
        {
            get
            {
                return string.Format("{0}({1}回合)", base.Name, base.RestTurn);
            }
        }

        public override bool OnRemoveState(GameContext context)
        {
            context.ShowMessage(string.Format("=========The World!!!========！"));
            BattleActionContext ctx = context as BattleActionContext;
            if(!base.Owner.States.OfType<绝顶>().Any())
                base.Owner.States.Add(ctx, "我的领域", 1);
            bool flag = base.OnRemoveState(context);
            return flag;
        }
        
        public Command PlanAction(BattleActionContext context)
        {
            return new Command()
            {
                Actor = context.Actor,
                Target = context.Actor,
                Action = (BattleAction) new WaitAction()
            };
        }
        
        public override string DescriptionHead => "自己无法进行行动，但可以闪避。当剩余回合数 为 0，且非 绝顶 状态的场合，获得“我的领域”，持续 9 回合。在时间暂停中依然会减少。";

        // Token: 0x1700032F RID: 815
        // (get) Token: 0x060009D1 RID: 2513 RVA: 0x000064C2 File Offset: 0x000046C2
        public override string DescriptionTail => "";
    }
}