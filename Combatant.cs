using System;
using System.Collections.Generic;

namespace CombatFramework
{
    /// <summary>
    /// 战斗者基类，实现ICombatant接口
    /// </summary>
    public class Combatant : ICombatant
    {
        public string Name { get; protected set; }
        public float CurrentHealth { get; set; }
        public float MaxHealth { get; protected set; }
        public bool IsAlive => CurrentHealth > 0;
        public List<Ability> Abilities { get; protected set; }
        
        public event Action<ICombatant> OnDeath;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="maxHealth">最大生命值</param>
        public Combatant(string name, float maxHealth)
        {
            Name = name;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            Abilities = new List<Ability>();
        }
        
        /// <summary>
        /// 受到伤害
        /// </summary>
        public virtual void TakeDamage(float damage, ICombatant source)
        {
            if (!IsAlive)
                return;
                
            CurrentHealth -= damage;
            if (CurrentHealth < 0)
                CurrentHealth = 0;
                
            Console.WriteLine($"[{Name}] 受到 {damage:F1} 点伤害，剩余生命值: {CurrentHealth:F1}/{MaxHealth:F1}");
            
            // 检查是否死亡
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
        
        /// <summary>
        /// 死亡处理
        /// </summary>
        protected virtual void Die()
        {
            Console.WriteLine($"[{Name}] 已经倒下了！");
            OnDeath?.Invoke(this);
        }
        
        /// <summary>
        /// 添加能力
        /// </summary>
        public void AddAbility(Ability ability)
        {
            Abilities.Add(ability);
            Console.WriteLine($"[{Name}] 学会了能力: {ability.Name}");
        }
    }
}
    