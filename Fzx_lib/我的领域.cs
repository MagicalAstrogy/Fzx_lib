using System.Linq;
using ZGames.AutoEtd.GameSystem;
using ZGames.AutoEtd.GameSystem.Battle.BattleActions;
using ZGames.AutoEtd.GameSystem.Battle.Battlers;
using ZGames.AutoEtd.GameSystem.Battle.PassiveEffects;
using ZGames.AutoEtd.GameSystem.Battle.States;

namespace Fzx_lib
{
    public class 我的领域 : BuffState
    {
        public 我的领域()
        {
            base.Turn = 9;
            base.MaxLevel = 1;
            base.LevelDownByTurn = false;
            base.Effects.Add(new AvoidTrap());
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
            context.ShowMessage(string.Format("然后，时间开始运转！"));
            bool flag = base.OnRemoveState(context);
            return flag;
        }
        
        public override string DescriptionHead => "暂停时间的状态，所有敌人无法行动。";

        // Token: 0x1700032F RID: 815
        // (get) Token: 0x060009D1 RID: 2513 RVA: 0x000064C2 File Offset: 0x000046C2
        public override string DescriptionTail => "";
        
    }
}