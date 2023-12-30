using System;
using System.Xml.Serialization;
using HarmonyLib;
using ZGames.AutoEtd.GameSystem.Battle;
using ZGames.AutoEtd.GameSystem.Battle.Conditions;
using ZGames.AutoEtd.Helpers;

namespace Fzx_lib
{
    [HarmonyPatch(typeof(TargetingCondition))]
    public class SerializerPrefix
    {
        
        //Init Condition
        public static Type[] Subclasses = new Type[]
        {
            typeof (EnemyBodyPartCondition),
            typeof (EnemyCanActionCondition),
            typeof (EnemyCanActionCountCondition),
            typeof (EnemyCanAvoidCondition),
            typeof (EnemyCanNotActionCondition),
            typeof (EnemyCanNotActionCountCondition),
            typeof (EnemyCanNotAvoidCondition),
            typeof (EnemyCondition),
            typeof (EnemyCountCondition),
            typeof (EnemyHEquipmentCountCondition),
            typeof (EnemySanityCondition),
            typeof (EnemySensitivityCondition),
            typeof (EnemyStateCondition),
            typeof (EnemyStatePercentageCondition),
            typeof (EnemyVergeOnOrgasm),
            typeof (FriendHpCondition),
            typeof (FriendMpCondition),
            typeof (FriendSensibilityCondition),
            typeof (HoldOnEnemyCondition),
            typeof (NoCondition),
            typeof (PercentageCondition),
            typeof (SelfHpCondition),
            typeof (SelfMpCondition),
            typeof (SelfSensibilityCondition),
            typeof (SelfStateCondition),
            typeof (SelfStatePercentageCondition),
            typeof (TurnCondition),
            typeof (OutOfBattleCondition)
        };
        //Recreate serializer
        
        private static XmlSerializer Serializer { get; } = new XmlSerializer(typeof (TargetingCondition), extraTypes: Subclasses);
        
        //public static TargetingCondition FromXmlString(string xmlString)
        [HarmonyPatch("FromXmlString")]
        [HarmonyPrefix]
        public static bool FromPrefix(ref TargetingCondition __result, string xmlString)
        {
            __result = Serializer.DeserializeFromString<TargetingCondition>(xmlString);
            return false;
        }
        
        //string ToXmlString()
        [HarmonyPatch("ToXmlString")]
        [HarmonyPrefix]
        public static bool ToPrefix(ref string __result, TargetingCondition __instance)
        {
            __result = Serializer.SerializeToString<TargetingCondition>(__instance);
            return false;
        }
    }
}