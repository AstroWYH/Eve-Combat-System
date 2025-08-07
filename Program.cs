using System;
using System.Collections.Generic;
using System.Threading;

namespace CombatFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 战斗框架演示 ===");
            Console.WriteLine("===================");
            
            // 创建战斗管理器
            var combatManager = new CombatManager();
            
            // 注册战斗事件
            combatManager.OnCombatStarted += () => Console.WriteLine("[系统] 战斗已经开始！");
            combatManager.OnCombatEnded += () => Console.WriteLine("[系统] 战斗已经结束！");
            combatManager.OnCombatantKilled += (combatant) => Console.WriteLine($"[系统] {combatant.Name} 被击败了！");
            
            // 创建战斗者 - 队伍1
            var player = new TeamCombatant("玩家", 100, 1);
            player.AddAbility(new MeleeAttack("普通攻击", 2.0f, 10.0f));
            player.AddAbility(new FireballAbility());
            
            // 创建战斗者 - 队伍2
            var monster1 = new TeamCombatant("骷髅兵", 50, 2);
            monster1.AddAbility(new MeleeAttack("骨棒打击", 2.5f, 8.0f));
            
            var monster2 = new TeamCombatant("巫师", 40, 2);
            monster2.AddAbility(new FireballAbility());
            
            // 开始战斗
            combatManager.StartCombat(new List<ICombatant> { player, monster1, monster2 });
            
            // 模拟战斗过程
            SimulateCombat(combatManager, player, monster1, monster2);
            
            Console.WriteLine("===================");
            Console.WriteLine("演示结束，按任意键退出...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// 模拟战斗过程
        /// </summary>
        static void SimulateCombat(CombatManager manager, params ICombatant[] combatants)
        {
            // 模拟游戏帧更新（每300ms一帧）
            int frameCount = 0;
            while (manager.IsInCombat && frameCount < 100) // 最多100帧，防止无限循环
            {
                frameCount++;
                float deltaTime = 0.3f; // 每帧约0.3秒
                manager.Update(deltaTime);
                
                // 每几帧让战斗者使用一次能力
                if (frameCount % 3 == 0)
                {
                    SimulateCombatantActions(combatants);
                }
                
                // 等待一小段时间，让玩家能看清输出
                Thread.Sleep(300);
            }
        }
        
        /// <summary>
        /// 模拟战斗者行动
        /// </summary>
        static void SimulateCombatantActions(IEnumerable<ICombatant> combatants)
        {
            var combatantList = combatants.ToList();
            
            foreach (var attacker in combatantList)
            {
                if (!attacker.IsAlive)
                    continue;
                    
                // 找到一个敌方目标
                var target = combatantList.FirstOrDefault(c => 
                    c.IsAlive && 
                    !(attacker is ITeamMember a && c is ITeamMember t && a.TeamId == t.TeamId));
                    
                if (target == null)
                    continue;
                    
                // 使用第一个可用的能力
                var abilityToUse = attacker.Abilities.FirstOrDefault(a => a.IsReady);
                if (abilityToUse != null)
                {
                    abilityToUse.Execute(attacker, target);
                }
            }
        }
    }
}
    