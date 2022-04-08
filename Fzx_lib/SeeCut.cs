using ZGames.AutoEtd.GameSystem.Battle.PassiveEffects;
using ZGames.AutoEtd.GameSystem.Battle.States;

namespace Fzx_lib
{
    public class SeeCut : PassiveEffect
    {
        public override PassiveEffect Clone()
        {
            return new SeeCut
            {
                GuardMessage = this.GuardMessage
            };
        }
        

        public string GuardMessage { get; set; }
        public 见切 ParentBuff { get; set; } 
        public override string Text => "抵挡攻击";
    }
}