namespace CombatFramework
{
    /// <summary>
    /// 团队战斗者，属于某个队伍的战斗者
    /// </summary>
    public class TeamCombatant : Combatant, ITeamMember
    {
        public int TeamId { get; private set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public TeamCombatant(string name, float maxHealth, int teamId) 
            : base(name, maxHealth)
        {
            TeamId = teamId;
        }
    }
}
    