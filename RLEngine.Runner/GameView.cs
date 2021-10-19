using RLEngine.ViewState.Games;

using RLEngine.Games;
using RLEngine.Logs;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

using System;

namespace RLEngine.Runner
{
    public class GameView
    {
        private readonly GameViewState state;
        private readonly int delayMS;

        public GameView(GameContent content, int delayMS)
        {
            this.delayMS = delayMS;

            state = new GameViewState(content);
        }

        public void Process(CombinedLog log)
        {
            foreach (var sublog in log.Logs)
            {
                if (sublog is CombinedLog combinedLog)
                {
                    Process(combinedLog);
                    continue;
                }

                if (state.Update(sublog)) WriteUnsupportedByState(sublog);

                if      (sublog is        SpawnLog        spawnLog) Write(       spawnLog);
                else if (sublog is     MovementLog     movementLog) Write(    movementLog);
                else if (sublog is  DestructionLog  destructionLog) Write( destructionLog);
                else if (sublog is ModificationLog modificationLog) Write(modificationLog);
                else if (sublog is       DamageLog       damageLog) Write(      damageLog);
                else if (sublog is      HealingLog      healingLog) Write(     healingLog);
                else if (sublog is   ProjectileLog   projectileLog) Write(  projectileLog);
                else WriteUnsupported(sublog);

                if (sublog is CombinedLog) continue;
                if (delayMS > 0) System.Threading.Thread.Sleep(delayMS);
            }
        }

        private static void Write(SpawnLog log)
        {
            Write(log.Entity);
            Console.Write(" spawns at ");
            Write(log.At);
            Console.WriteLine(".");
        }

        private static void Write(MovementLog log)
        {
            Write(log.Entity);
            Console.Write(" moves to ");
            Write(log.To);
            Console.WriteLine(".");
        }

        private static void Write(DestructionLog log)
        {
            Write(log.Entity);
            var verb = "XXX";
            // TODO: var verb = log.Entity.IsAgent ? "dies" : "is destroyed";
            Console.WriteLine($" {verb}.");
        }

        private static void Write(ModificationLog log)
        {
            Write(log.PreviousType);
            Console.Write(" at ");
            Write(log.At);
            Console.Write(" is modified to ");
            Write(log.NewType);
            Console.WriteLine(".");
        }

        private static void Write(DamageLog log)
        {
            if (log.Attacker is not null)
            {
                Write(log.Attacker);
                Console.Write(" attacks. ");
            }
            Write(log.Target);
            Console.Write(" loses ");
            Write(log.ActualDamage, false);
            if (log.Damage > log.ActualDamage)
            {
                Console.Write(" (");
                Write(log.Damage, false);
                Console.Write(")");
            }
            Console.WriteLine(" health.");
        }

        private static void Write(HealingLog log)
        {
            if (log.Healer is not null)
            {
                Write(log.Healer);
                Console.Write(" heals. ");
            }
            Write(log.Target);
            Console.Write(" recovers ");
            Write(log.ActualHealing, true);
            if (log.Healing > log.ActualHealing)
            {
                Console.Write(" (");
                Write(log.Healing, true);
                Console.Write(")");
            }
            Console.WriteLine(" health.");
        }

        private static void Write(ProjectileLog log)
        {
            Console.Write("A projectile goes from ");
            if (log.From is not null) Write(log.From);
            else if (log.Source is not null) Write(log.Source);
            else WriteNull();
            Console.Write(" to ");
            if (log.To is not null) Write(log.To);
            else if (log.Target is not null) Write(log.Target);
            else WriteNull();
            Console.WriteLine(".");
        }

        private static void WriteUnsupported(Log log)
        {
            Console.Write("[");
            Write("error", ConsoleColor.Red);
            Console.WriteLine($"] {log.GetType().Name} is not supported.");
        }

        private static void WriteUnsupportedByState(Log log)
        {
            Console.Write("[");
            Write("error", ConsoleColor.Red);
            Console.WriteLine($"] {log.GetType().Name} is not supported by the view state.");
        }

        private static void WriteStateSynchronizationError(Log log)
        {
            Console.Write("[");
            Write("error", ConsoleColor.Red);
            Console.WriteLine("] The game view state is not synchronized with the game.");
        }

        private static void Write(Entity entity)
        {
            Write("XXX", ConsoleColor.Yellow);
            // TODO: Write(entity.Name, ConsoleColor.Yellow);
        }

        private static void Write(TileType tileType)
        {
            Write(tileType.Name, ConsoleColor.Yellow);
        }

        private static void Write(Coords coords)
        {
            Write(coords, ConsoleColor.Blue);
        }

        private static void Write(int amount, bool isHeal)
        {
            Write(amount, isHeal ? ConsoleColor.Green : ConsoleColor.Red);
        }

        private static void WriteNull()
        {
            Write("null", ConsoleColor.Red);
        }

        private static void Write(object obj, ConsoleColor color)
        {
            var text = obj.ToString();
            if (text is not null) Write(text, color);
        }

        private static void Write(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}