namespace CombatFramework
{
    /// <summary>
    /// 能力基类，表示战斗中的各种技能、攻击等
    /// </summary>
    public abstract class Ability
    {
        /// <summary>
        /// 能力名称
        /// </summary>
        public string Name { get; protected set; }
        
        /// <summary>
        /// 冷却时间（秒）
        /// </summary>
        public float Cooldown { get; protected set; }
        
        /// <summary>
        /// 当前剩余冷却时间
        /// </summary>
        public float CurrentCooldown { get; protected set; }
        
        /// <summary>
        /// 是否可以使用
        /// </summary>
        public bool IsReady => CurrentCooldown <= 0;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">能力名称</param>
        /// <param name="cooldown">冷却时间</param>
        public Ability(string name, float cooldown)
        {
            Name = name;
            Cooldown = cooldown;
            CurrentCooldown = 0;
        }
        
        /// <summary>
        /// 更新冷却时间
        /// </summary>
        /// <param name="deltaTime">帧间隔时间</param>
        public virtual void UpdateCooldown(float deltaTime)
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown -= deltaTime;
                if (CurrentCooldown < 0)
                    CurrentCooldown = 0;
            }
        }
        
        /// <summary>
        /// 使用能力
        /// </summary>
        /// <param name="user">使用者</param>
        /// <param name="target">目标</param>
        /// <returns>是否使用成功</returns>
        public abstract bool Execute(ICombatant user, ICombatant target);
    }
}
    