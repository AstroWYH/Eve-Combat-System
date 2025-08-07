using System.Collections.Generic;

namespace CombatFramework
{
    /// <summary>
    /// 战斗参与者接口，所有能参与战斗的实体都要实现此接口
    /// </summary>
    public interface ICombatant
    {
        /// <summary>
        /// 战斗者名称
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// 当前生命值
        /// </summary>
        float CurrentHealth { get; set; }
        
        /// <summary>
        /// 最大生命值
        /// </summary>
        float MaxHealth { get; }
        
        /// <summary>
        /// 是否存活
        /// </summary>
        bool IsAlive { get; }
        
        /// <summary>
        /// 拥有的能力列表
        /// </summary>
        List<Ability> Abilities { get; }
        
        /// <summary>
        /// 受到伤害
        /// </summary>
        /// <param name="damage">伤害值</param>
        /// <param name="source">伤害来源</param>
        void TakeDamage(float damage, ICombatant source);
        
        /// <summary>
        /// 死亡事件
        /// </summary>
        event System.Action<ICombatant> OnDeath;
    }
}
    