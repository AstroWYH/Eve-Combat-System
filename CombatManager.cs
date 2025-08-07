using System;
using System.Collections.Generic;
using System.Linq;

namespace CombatFramework
{
    /// <summary>
    /// 战斗管理器，负责协调战斗流程
    /// </summary>
    public class CombatManager
    {
        private readonly List<ICombatant> _combatants = new List<ICombatant>();
        public bool IsInCombat { get; private set; }
        private float _combatTime;
        
        /// <summary>
        /// 开始战斗
        /// </summary>
        /// <param name="combatants">参与战斗的所有实体</param>
        public void StartCombat(IEnumerable<ICombatant> combatants)
        {
            if (IsInCombat)
            {
                Console.WriteLine("[战斗管理] 已经在战斗中，无法再次开始战斗");
                return;
            }
            
            _combatants.Clear();
            _combatants.AddRange(combatants);
            
            // 注册死亡事件
            foreach (var combatant in _combatants)
            {
                combatant.OnDeath += OnCombatantDeath;
            }
            
            IsInCombat = true;
            _combatTime = 0;
            
            Console.WriteLine($"[战斗管理] 战斗开始，参与战斗的实体数量: {_combatants.Count}");
            OnCombatStarted?.Invoke();
        }
        
        /// <summary>
        /// 结束战斗
        /// </summary>
        public void EndCombat()
        {
            if (!IsInCombat) return;
            
            // 取消注册事件
            foreach (var combatant in _combatants)
            {
                combatant.OnDeath -= OnCombatantDeath;
            }
            
            IsInCombat = false;
            Console.WriteLine($"[战斗管理] 战斗结束，持续时间: {_combatTime:F1}秒");
            OnCombatEnded?.Invoke();
        }
        
        /// <summary>
        /// 更新战斗状态
        /// </summary>
        /// <param name="deltaTime">帧间隔时间</param>
        public void Update(float deltaTime)
        {
            if (!IsInCombat)
                return;
                
            _combatTime += deltaTime;
            
            // 更新所有能力冷却
            foreach (var combatant in _combatants)
            {
                foreach (var ability in combatant.Abilities)
                {
                    ability.UpdateCooldown(deltaTime);
                }
            }
            
            // 检查战斗是否结束
            CheckCombatEndConditions();
        }
        
        /// <summary>
        /// 检查战斗结束条件
        /// </summary>
        private void CheckCombatEndConditions()
        {
            // 示例：一方全部死亡则战斗结束
            var teams = _combatants.OfType<ITeamMember>().GroupBy(c => c.TeamId).ToList();
            
            if (teams.Count <= 1)
                return;
                
            // 检查是否有队伍全灭
            foreach (var team in teams)
            {
                if (team.All(member => !member.IsAlive))
                {
                    Console.WriteLine($"[战斗管理] 队伍 {team.Key} 已全灭，战斗结束");
                    EndCombat();
                    return;
                }
            }
        }
        
        /// <summary>
        /// 战斗者死亡时的处理
        /// </summary>
        private void OnCombatantDeath(ICombatant combatant)
        {
            Console.WriteLine($"[战斗管理] {combatant.Name} 已倒下");
            OnCombatantKilled?.Invoke(combatant);
        }
        
        /// <summary>
        /// 战斗开始事件
        /// </summary>
        public event Action OnCombatStarted;
        
        /// <summary>
        /// 战斗结束事件
        /// </summary>
        public event Action OnCombatEnded;
        
        /// <summary>
        /// 战斗者被杀死事件
        /// </summary>
        public event Action<ICombatant> OnCombatantKilled;
    }
}
    