using ZGames.AutoEtd.GameSystem.Battle.PassiveEffects;
using ZGames.AutoEtd.GameSystem.Battle.States;

namespace Fzx_lib
{
    
    public class 灵击领域 : BuffState
    {
        public 灵击领域()
        {
            this.Turn = 1;
            this.Effects.Add((PassiveEffect) new GuardOrgasm()
            {
                GuardMessage = "但是(target)出于灵击期间，不会绝顶。"
            });
        }
    }
}