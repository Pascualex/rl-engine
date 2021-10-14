using RLEngine.Logs;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

using System;

namespace RLEngine.Runner
{
    public class Logger
    {
        private readonly int delayMS;

        public Logger(int delayMS)
        {
            this.delayMS = delayMS;
        }

        public void Write(CombinedLog log)
        {
            foreach (var currentLog in log.Logs)
            {
                if (currentLog is CombinedLog combinedLog) Write(combinedLog);
                else if (currentLog is      SpawnLog      spawnLog) Write(     spawnLog);
                else if (currentLog is       MoveLog       moveLog) Write(      moveLog);
                else if (currentLog is    DestroyLog    destroyLog) Write(   destroyLog);
                else if (currentLog is     ModifyLog     modifyLog) Write(    modifyLog);
                else if (currentLog is     DamageLog     damageLog) Write(    damageLog);
                else if (currentLog is       HealLog       healLog) Write(      healLog);
                else if (currentLog is ProjectileLog projectileLog) Write(projectileLog);
                else WriteUnsuported(currentLog);
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

        private static void Write(MoveLog log)
        {
            Write(log.Entity);
            Console.Write(" moves to ");
            Write(log.To);
            Console.WriteLine(".");
        }

        private static void Write(DestroyLog log)
        {
            Write(log.Entity);
            var verb = log.Entity.IsAgent ? "dies" : "is destroyed";
            Console.WriteLine($" {verb}.");
        }

        private static void Write(ModifyLog log)
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
            Write(log.Damage, false);
            Console.WriteLine(" health.");
        }

        private static void Write(HealLog log)
        {
            if (log.Healer is not null)
            {
                Write(log.Healer);
                Console.Write(" heals. ");
            }
            Write(log.Target);
            Console.Write(" recovers ");
            Write(log.ActualHealing, true);
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

        private static void WriteUnsuported(Log log)
        {
            Console.Write("[");
            Write("error", ConsoleColor.Red);
            Console.WriteLine($"] {log.GetType().Name} is not supported.");
        }

        private static void Write(Entity entity)
        {
            Write(entity.Name, ConsoleColor.Yellow);
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