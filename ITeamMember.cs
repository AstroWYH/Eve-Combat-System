namespace CombatFramework
{
    /// <summary>
    /// 团队成员接口，表示属于某个队伍的战斗者
    /// </summary>
    public interface ITeamMember : ICombatant
    {
        /// <summary>
        /// 队伍ID
        /// </summary>
        int TeamId { get; }
    }
}
    