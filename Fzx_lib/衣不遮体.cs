using ZGames.AutoEtd.GameSystem.Battle;
using ZGames.AutoEtd.GameSystem.Battle.PassiveEffects;

namespace Fzx_lib
{
    public class 衣不遮体 : State
    {
        // Token: 0x060007B5 RID: 1973 RVA: 0x0001DCB0 File Offset: 0x0001BEB0
        public 衣不遮体()
        {
            base.MaxLevel = 1;
            base.Turn = 9999;
            base.Effects.Add(new ParameterPercentage
            {
                MagicDefence = -25,
                Defence = -25,
                Agility = -10,
                Resistance = -10
            });
            base.Effects.Add(new SensitivityPercentage
            {
                AllPart = true,
                LevelDependency = false,
                Value = 10
            });
            base.Effects.Add(new HCriticalPercentage
            {
                AllPart = true,
                LevelDependency = false,
                Value = 10
            });
        }

        // Token: 0x170001EA RID: 490
        // (get) Token: 0x060007B6 RID: 1974 RVA: 0x000055BD File Offset: 0x000037BD
        public override StateKind Kind => StateKind.修正;

        // Token: 0x170001EB RID: 491
        // (get) Token: 0x060007B7 RID: 1975 RVA: 0x000055CF File Offset: 0x000037CF
        public override string DescriptionHead => "由于衣物破损，导致防御降低的状态。";

        // Token: 0x170001EC RID: 492
        // (get) Token: 0x060007B8 RID: 1976 RVA: 0x000055D6 File Offset: 0x000037D6
        public override string DescriptionTail => "";

        // Token: 0x170001ED RID: 493
        // (get) Token: 0x060007B9 RID: 1977 RVA: 0x00004CBC File Offset: 0x00002EBC
        public override bool CanAvoid => false;
    }
}