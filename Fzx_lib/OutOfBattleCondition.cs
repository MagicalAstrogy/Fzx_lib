using System.Collections.Generic;
using System.Linq;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;
using ZGames.AutoEtd.GameSystem.Battle.Battlers;
using ZGames.AutoEtd.GameSystem.Battle.Conditions;

namespace Fzx_lib
{
    public class OutOfBattleCondition : TargetingCondition
    {
        // Token: 0x17000470 RID: 1136
        // (get) Token: 0x06000C8C RID: 3212 RVA: 0x00008420 File Offset: 0x00006620
        public override string Name
        {
            get
            {
                return "不在战斗中";
            }
        }

        // Token: 0x06000C8D RID: 3213 RVA: 0x00008427 File Offset: 0x00006627
        public override IEnumerable<Battler> GetTargets(BattleActionContext context)
        {
            if (!context.Enemies.Any(enemy => enemy.IsLive))
                return new[] { context.Friends.First() };
            return new Battler[] { };
        }

        // Token: 0x06000C8E RID: 3214 RVA: 0x0000843A File Offset: 0x0000663A
        public override TargetingCondition Clone()
        {
            return new OutOfBattleCondition();
        }
    }
}