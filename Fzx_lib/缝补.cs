using System.Linq;
using ZGames.AutoEtd.GameSystem;
using ZGames.AutoEtd.GameSystem.Battle;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;
using ZGames.AutoEtd.GameSystem.Battle.Battlers;
using ZGames.AutoEtd.GameSystem.Battle.States;
using ZGames.AutoEtd.GameSystem.Exploration.Explorers;

namespace Fzx_lib
{
    public class 缝补 : BuffState, IPlanActionOverride
    {
        
        public 缝补()
        {
            base.Turn = 5;
            base.MaxLevel = 1;
            base.LevelDownByTurn = false;
        }
        
        public override bool CanAdd(Battler target)
        {
            return target.States.OfType<衣不遮体>().Any();
        }
        
        public override string DisplayName
        {
            get
            {
                return string.Format("{0}({1}回合)", base.Name, base.RestTurn);
            }
        }
        
        
        public Command PlanAction(BattleActionContext context)
        {
            context.ShowMessage(string.Format("{0}正在缝补衣服……", base.Owner));
            return new Command()
            {
                Actor = context.Actor,
                Target = context.Actor,
                Action = (BattleAction) new WaitAction()
            };
        }


        public override bool OnRemoveState(GameContext context)
        {
            var player = (Explorer)base.Owner;
            if (player == null) return base.OnRemoveState(context);
            context.ShowMessage(string.Format("{0}缝补好了衣服。", base.Owner));
            var armorSkill = player.SkillInfo.Skills.FirstOrDefault(sk => sk.Name == "多层护甲");
            if (armorSkill != null && armorSkill.Level > 0)
            {
                player.States.Add(context, "多层护甲", armorSkill.Level * player.Parameter.Defence / 40);
            }
            bool flag = base.OnRemoveState(context);
            return flag;
        }
        
        public override string DescriptionHead => "正在缝补自己的衣服，无法进行其他行动。";

        // Token: 0x1700032F RID: 815
        // (get) Token: 0x060009D1 RID: 2513 RVA: 0x000064C2 File Offset: 0x000046C2
        public override string DescriptionTail => "";
    }
}