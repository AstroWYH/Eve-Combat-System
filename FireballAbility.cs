namespace CombatFramework
{
    /// <summary>
    /// 火球术能力（示例远程/魔法攻击）
    /// </summary>
    public class FireballAbility : Ability
    {
        /// <summary>
        /// 基础伤害
        /// </summary>
        private float _baseDamage;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public FireballAbility() 
            : base("火球术", 8.0f)
        {
            _baseDamage = 15.0f;
        }
        
        /// <summary>
        /// 执行火球术
        /// </summary>
        public override bool Execute(ICombatant user, ICombatant target)
        {
            if (!IsReady || !target.IsAlive)
            {
                if (!IsReady)
                    Console.WriteLine($"[{user.Name}] 的 {Name} 还在冷却中，剩余 {CurrentCooldown:F1} 秒");
                else
                    Console.WriteLine($"[{user.Name}] 尝试对已死亡的目标 {target.Name} 使用 {Name}，失败");
                return false;
            }
            
            // 计算伤害
            float damage = _baseDamage;
            
            // 应用伤害
            target.TakeDamage(damage, user);
            Console.WriteLine($"[{user.Name}] 对 {target.Name} 使用了 {Name}，造成 {damage:F1} 点火焰伤害！");
            
            // 触发冷却
            CurrentCooldown = Cooldown;
            return true;
        }
    }
}
    