namespace CombatFramework
{
    /// <summary>
    /// 近战攻击能力
    /// </summary>
    public class MeleeAttack : Ability
    {
        /// <summary>
        /// 基础伤害
        /// </summary>
        private float _baseDamage;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">能力名称</param>
        /// <param name="cooldown">冷却时间</param>
        /// <param name="baseDamage">基础伤害</param>
        public MeleeAttack(string name, float cooldown, float baseDamage) 
            : base(name, cooldown)
        {
            _baseDamage = baseDamage;
        }
        
        /// <summary>
        /// 执行近战攻击
        /// </summary>
        public override bool Execute(ICombatant user, ICombatant target)
        {
            if (!IsReady || !target.IsAlive)
            {
                if (!IsReady)
                    Console.WriteLine($"[{user.Name}] 的 {Name} 还在冷却中，剩余 {CurrentCooldown:F1} 秒");
                else
                    Console.WriteLine($"[{user.Name}] 尝试攻击已死亡的目标 {target.Name}，攻击失败");
                return false;
            }
            
            // 计算伤害（这里简化处理，实际游戏中可能有复杂的伤害计算公式）
            float damage = _baseDamage;
            
            // 应用伤害
            target.TakeDamage(damage, user);
            Console.WriteLine($"[{user.Name}] 使用 {Name} 攻击了 {target.Name}，造成 {damage:F1} 点伤害");
            
            // 触发冷却
            CurrentCooldown = Cooldown;
            return true;
        }
    }
}
    