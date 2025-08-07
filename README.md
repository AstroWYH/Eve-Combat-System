# 战斗系统框架 (Combat System Framework)

一个基于C#的通用战斗系统框架，设计用于单机游戏开发，提供灵活可扩展的战斗机制、能力系统和团队对战支持。

## 框架特点

- **模块化设计**：各组件职责清晰，便于扩展和维护
- **面向接口编程**：通过接口定义行为契约，支持多类型战斗实体
- **事件驱动**：组件间通过事件通信，降低耦合度
- **团队支持**：内置多队伍对战机制，支持复杂战斗规则
- **能力系统**：灵活的技能/能力实现，支持冷却、消耗等核心机制

## 核心组件

### 1. 战斗参与者系统
- `ICombatant`：所有战斗实体的基础接口，定义生命、能力等核心行为
- `Combatant`：战斗者基类，实现基础战斗功能
- `TeamCombatant`：支持团队属性的战斗者，实现`ITeamMember`接口

### 2. 能力系统
- `Ability`：所有能力的抽象基类，封装冷却时间等公共逻辑
- 具体能力实现：`MeleeAttack`（近战攻击）、`FireballAbility`（火球术）等

### 3. 战斗管理
- `CombatManager`：战斗流程的核心管理器，负责战斗的开始/结束、状态更新和胜负判定

## 代码结构

```
CombatFramework/
├── Interfaces/
│   ├── ICombatant.cs       // 战斗参与者接口
│   └── ITeamMember.cs      // 团队成员接口
├── Abilities/
│   ├── Ability.cs          // 能力基类
│   ├── MeleeAttack.cs      // 近战攻击实现
│   └── FireballAbility.cs  // 火球术实现
├── Combatants/
│   ├── Combatant.cs        // 战斗者基类
│   └── TeamCombatant.cs    // 团队战斗者
├── CombatManager.cs        // 战斗管理器
└── Program.cs              // 演示程序入口
```

## 使用示例

```csharp
// 创建战斗管理器
var combatManager = new CombatManager();

// 注册战斗事件
combatManager.OnCombatStarted += () => Console.WriteLine("战斗开始！");
combatManager.OnCombatEnded += () => Console.WriteLine("战斗结束！");

// 创建战斗者
var player = new TeamCombatant("玩家", 100, 1);
player.AddAbility(new MeleeAttack("普通攻击", 2.0f, 10.0f));
player.AddAbility(new FireballAbility());

var monster = new TeamCombatant("骷髅兵", 50, 2);
monster.AddAbility(new MeleeAttack("骨棒打击", 2.5f, 8.0f));

// 开始战斗
combatManager.StartCombat(new List<ICombatant> { player, monster });

// 模拟战斗更新
while (combatManager.IsInCombat)
{
    combatManager.Update(0.3f); // 每帧更新
    // 战斗逻辑...
}
```

## 扩展指南

### 添加新能力
1. 创建新类继承自`Ability`
2. 实现`Execute`方法定义能力效果
3. 按需重写其他方法（如`OnCooldownEnd`）

```csharp
public class HealAbility : Ability
{
    public float HealAmount { get; set; }
    
    public HealAbility() : base("治疗术", 5.0f)
    {
        HealAmount = 20.0f;
    }
    
    public override void Execute(ICombatant caster, ICombatant target)
    {
        if (!IsReady) return;
        
        target.Heal(HealAmount);
        Console.WriteLine($"{caster.Name}使用了治疗术，恢复了{HealAmount}点生命");
        
        StartCooldown();
    }
}
```

### 添加新战斗者类型
1. 创建新类实现`ICombatant`接口，或继承`Combatant`/`TeamCombatant`
2. 实现或重写相应方法以满足特殊需求

## 设计理念

框架遵循"高内聚、低耦合"原则，通过接口定义行为契约（如`ICombatant`），通过抽象类实现代码复用（如`Ability`）。这种设计使系统具有良好的扩展性，能够适应不同类型游戏的战斗需求。

事件驱动机制确保了各组件间的松散耦合，例如战斗管理器不需要直接调用能力系统的方法，而是通过事件通知来协调战斗流程。

## 许可证

[MIT](LICENSE)
