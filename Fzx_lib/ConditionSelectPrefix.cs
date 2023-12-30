using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HarmonyLib;
using ZGames.AutoEtd.GameSystem.Battle.ActiveEffects;
using ZGames.AutoEtd.GameSystem.Battle.Conditions;
using AutoEtd.ViewModels.Explorers;
using ZGames.AutoEtd.GameSystem;

namespace Fzx_lib
{
    public class ConditionSelectPrefix
    {
        //[HarmonyPatch(typeof(ConditionSelectViewModel))]
        //[HarmonyPatch("ExecuteSingle")]

        public static void Initialize(Harmony harmony)
        {
            var modelType = AccessTools.TypeByName("ConditionSelectViewModel");
            var modelCtor = AccessTools.Constructor(modelType, new []{typeof(AutoEtdContext)});
            var postfix = typeof(ConditionSelectPrefix).GetMethod("Postfix");
            harmony.Patch(modelCtor, null, new HarmonyMethod(postfix));
        }

        // From: https://stackoverflow.com/questions/40035777/c-sharp-get-delegate-from-methodinfo
        public static Delegate CreateDelegate(object instance, MethodInfo method)
        {
            var parameters = method.GetParameters()
                .Select(p => Expression.Parameter(p.ParameterType, p.Name))
                .ToArray();

            var call = Expression.Call(Expression.Constant(instance), method, parameters);
            return Expression.Lambda(call, parameters).Compile();
        }

        private static ConditionGroupItemViewModel CreatePercentageItem(string name, params Func<int, TargetingCondition>[] factories)
        {
            List<TargetingCondition> list = new List<TargetingCondition>();
            for (int j = 0; j < factories.Length; j++)
            {
                Func<int, TargetingCondition> factry = factories[j];
                list.AddRange(from i in Enumerable.Range(1, 10)
                    select factry(i * 10));
            }
            return new ConditionGroupItemViewModel(name, list);
        }

        // Token: 0x06000181 RID: 385 RVA: 0x00006BF8 File Offset: 0x00004DF8
        // 因为不太方便反射提取（类型不一致），所以直接复制了一份实现
        private static ConditionGroupItemViewModel CreateSteteItem(string name, params string[] states)
        {
            List<TargetingCondition> list = new List<TargetingCondition>();
            foreach (string state in states)
            {
                list.Add(new SelfStateCondition
                {
                    State = state,
                    Level = 1
                });
                list.Add(new SelfStateCondition
                {
                    State = state,
                    Level = 2
                });
                list.Add(new SelfStateCondition
                {
                    State = state,
                    Level = 4
                });
                list.Add(new SelfStateCondition
                {
                    State = state,
                    Level = 6
                });
                list.Add(new SelfStateCondition
                {
                    State = state,
                    Level = 8
                });
            }
            return new ConditionGroupItemViewModel(name, list);
        }

        public static void Postfix(object __instance)
        {
            var modelType = AccessTools.TypeByName("ConditionSelectViewModel");
            var groupsProp = AccessTools.Property(modelType, "Groups");


            var dstValue = new List<ConditionGroupItemViewModel>()
            {
                new ConditionGroupItemViewModel()
                {
                    Name = "敌人",
                    Conditions = (IReadOnlyList<TargetingCondition>)new TargetingCondition[11]
                    {
                        (TargetingCondition)new EnemyCondition(EnemyConditionType.Any),
                        (TargetingCondition)new EnemyCondition(EnemyConditionType.HighestHp),
                        (TargetingCondition)new EnemyCondition(EnemyConditionType.LowestHp),
                        (TargetingCondition)new EnemyCondition(EnemyConditionType.HighestMp),
                        (TargetingCondition)new EnemyCondition(EnemyConditionType.LowestMp),
                        (TargetingCondition)new EnemyCondition(EnemyConditionType.HighestMaxHp),
                        (TargetingCondition)new EnemyCondition(EnemyConditionType.LowestMaxHp),
                        (TargetingCondition)new EnemyCountCondition(2),
                        (TargetingCondition)new EnemyCountCondition(3),
                        (TargetingCondition)new EnemyCountCondition(4),
                        (TargetingCondition)new EnemyCountCondition(5)
                    }
                },
                new ConditionGroupItemViewModel()
                {
                    Name = "敌人的状态",
                    Conditions = (IReadOnlyList<TargetingCondition>)new TargetingCondition[10]
                    {
                        (TargetingCondition)new EnemyCanActionCondition(),
                        (TargetingCondition)new EnemyCanActionCountCondition(2),
                        (TargetingCondition)new EnemyCanActionCountCondition(3),
                        (TargetingCondition)new EnemyCanActionCountCondition(4),
                        (TargetingCondition)new EnemyCanActionCountCondition(5),
                        (TargetingCondition)new EnemyCanNotActionCondition(),
                        (TargetingCondition)new EnemyCanNotActionCountCondition(2),
                        (TargetingCondition)new EnemyCanNotActionCountCondition(3),
                        (TargetingCondition)new EnemyCanNotActionCountCondition(4),
                        (TargetingCondition)new EnemyCanNotActionCountCondition(5)
                    }
                },
                CreatePercentageItem("自己的HP", (Func<int, TargetingCondition>)(per =>
                    (TargetingCondition)new SelfHpCondition()
                    {
                        Percentage = per,
                        IsOver = true
                    }), (Func<int, TargetingCondition>)(per => (TargetingCondition)new SelfHpCondition()
                {
                    Percentage = per,
                    IsOver = false
                })),
                CreatePercentageItem("自己的MP", (Func<int, TargetingCondition>)(per =>
                    (TargetingCondition)new SelfMpCondition()
                    {
                        Percentage = per,
                        IsOver = true
                    }), (Func<int, TargetingCondition>)(per => (TargetingCondition)new SelfMpCondition()
                {
                    Percentage = per,
                    IsOver = false
                })),
                CreatePercentageItem("自己的快感", (Func<int, TargetingCondition>)(per =>
                    (TargetingCondition)new SelfSensibilityCondition()
                    {
                        Sensibility = per,
                        IsOver = true
                    }), (Func<int, TargetingCondition>)(per => (TargetingCondition)new SelfSensibilityCondition()
                {
                    Sensibility = per,
                    IsOver = false
                })),
                CreateSteteItem("自己的状态", "媚药", "粘液", "润滑液", "发情", "催淫", "催眠", "魅惑", "恐惧", "混乱","衣不遮体", "我的领域"),
                new ConditionGroupItemViewModel()
                {
                    Name = "其他",
                    Conditions = (IReadOnlyList<TargetingCondition>)new TargetingCondition[]
                    {
                        (TargetingCondition)new NoCondition(),
                        (TargetingCondition)new OutOfBattleCondition()
                    }
                }
            };
            
            var objVal = (List<ConditionGroupItemViewModel>)groupsProp.GetValue(__instance);
            objVal.Clear();
            objVal.AddRange(dstValue);
        }
    }
}