using System;
using System.Linq;
using ZGames.AutoEtd.GameSystem;
using ZGames.AutoEtd.GameSystem.Battle;
using ZGames.AutoEtd.GameSystem.Battle.ActiveEffects;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;
using ZGames.AutoEtd.GameSystem.Battle.Battlers;
using ZGames.AutoEtd.GameSystem.Battle.PassiveEffects;
using ZGames.AutoEtd.GameSystem.Battle.States;
using ZGames.AutoEtd.GameSystem.Exploration.Explorers;

namespace Fzx_lib
{
    public class 灵击 : BuffState,  IOnCalcOrgasmBehavior
    {
        
        public 灵击()
        {
            base.MaxLevel = 999;
            base.Turn = 9999;
            base.Level = 10;
            base.LevelDownByTurn = false;
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
            return target.States.OfType<灵击>().Any();
        }

        public override bool OnRemoveState(GameContext context)
        {
            context.ShowMessage(string.Format("灵击后的瞬间，积累的劳累向{0}袭来！", base.Owner));
            BattleActionContext ctx = context as BattleActionContext;
            base.Owner.States.Add(ctx, "魔力耗尽", 1);
            bool flag = base.OnRemoveState(context);
            return flag;
        }
        
        public override string DescriptionHead => "当进行绝顶判定的场合发动：\n不绝顶，立刻消费 50% 当前魔法值，对所有敌人造成 [ Buff层数 * 快感 / 200 / 5 * 消费魔法值 ] 的伤害。\n然后快感减半。";

        // Token: 0x1700032F RID: 815
        // (get) Token: 0x060009D1 RID: 2513 RVA: 0x000064C2 File Offset: 0x000046C2
        public override string DescriptionTail => "";
        public void OnCalcOrgasm(HDamageContext context)
        {
            if (context.OrgasmCount <= 0) return;
            
            GuardOrgasm guardOrgasm = context.Target.PassiveEffects.OfType<GuardOrgasm>().FirstOrDefault<GuardOrgasm>();
            if (guardOrgasm != null) return;
            if (context.Target.States.OfType<绝顶禁止>().Any()) return;
            context.OrgasmCount = -1000;
            context.MilkCount = -1000;
            context.EjaculationCount = -1000;
            context.ShowMessage(string.Format("灵击发动，对敌人们造成了伤害！", base.Owner));
            this.Level--;
            if (this.Level == 0)
            {
                this.RestTurn = 0;
            }

            if (this.Level < 0) return; 
            
            var bCtx = context.BattleActionContext?.BattleContext;
            if (bCtx == null) return;
            
            State state = StateFactory.Create("灵击领域", 1);
            base.Owner.States.Add(bCtx, state);
            var player = (bCtx.PartyA.FirstOrDefault()) as Explorer;
            if (player == null) return;
            var multiplier = (float)player.Sensibility / player.OrgasmSensibility;
            if (multiplier > 16f) multiplier = 16f;
            var levelMultiplier = (float)this.Level / 10;
            //player.Sensibility /= 2;
            var beforeMp = player.Mp;
            player.Mp = Math.Max(1, player.Mp / 2);
            var cost = beforeMp - player.Mp + 1;
            var dmg = new Damage(){BasePoint = (int)(cost * multiplier * levelMultiplier), 
                MagicalPercentage = 50, PhysicalPercentage = 50};
            var ctx = new BattleActionContext(bCtx, player);
            ctx.Targets = bCtx.PartyB.Lives.ToList();
            dmg.Execute(ctx);
            
        }
    }
}