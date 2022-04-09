using System.Linq;
using ZGames.AutoEtd.GameSystem;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;
using ZGames.AutoEtd.GameSystem.Battle.Battlers;
using ZGames.AutoEtd.GameSystem.Battle.PassiveEffects;
using ZGames.AutoEtd.GameSystem.Battle.States;

namespace Fzx_lib
{
    public class 多层护甲 : BuffState
    {
        
        public 多层护甲()
        {
            base.MaxLevel = 999;
            base.Turn = 9999;
            base.LevelDownByTurn = false;
            
            base.Effects.Add(new SensitivityPercentage
            {
                AllPart = true,
                LevelDependency = false,
                Value = -10
            });
            base.Effects.Add(new HCriticalPercentage
            {
                AllPart = true,
                LevelDependency = false,
                Value = -5
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
            return !target.States.OfType<多层护甲>().Any();
        }
        
        public override bool OnRemoveState(GameContext context)
        {
            context.ShowMessage(string.Format("因为经历了各种各样的陷阱，{0}的衣物已经破破烂烂了！", base.Owner));
            BattleActionContext ctx = context as BattleActionContext;
            base.Owner.States.Add(ctx, "衣不遮体", 1);
            bool flag = base.OnRemoveState(context);
            return flag;
        }

        public string GuardText => "由于衣物的遮挡，奇怪的东西不会被装到身上。";
        
        public override string DescriptionHead => "当受到 束缚、道具付与 系 debuff 的场合发动：\n不再获得对应的 debuff；然后 多层护甲 状态 等级 - 1。\n当失去 多层护甲 状态 的场合，获得 “衣不遮体” 状态。";

        // Token: 0x1700032F RID: 815
        // (get) Token: 0x060009D1 RID: 2513 RVA: 0x000064C2 File Offset: 0x000046C2
        public override string DescriptionTail => "";
    }
}