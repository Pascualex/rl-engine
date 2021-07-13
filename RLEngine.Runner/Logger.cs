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
                else if (currentLog is SpawnLog spawnLog) Write(spawnLog);
                else if (currentLog is MoveLog moveLog) Write(moveLog);
                else if (currentLog is DestroyLog destroyLog) Write(destroyLog);
                else Write(currentLog);
                if (delayMS > 0) System.Threading.Thread.Sleep(delayMS);
            }
        }

        private static void Write(SpawnLog log)
        {
            Write(log.Entity);
            Console.Write(" has spawned at ");
            Write(log.At);
            Console.WriteLine();
        }

        private static void Write(MoveLog log)
        {
            Write(log.Entity);
            Console.Write(" has moved to ");
            Write(log.To);
            Console.WriteLine();
        }

        private static void Write(DestroyLog log)
        {
            Write(log.Entity);
            var verb = log.Entity.Type.IsAgent ? "killed" : "destroyed";
            Console.WriteLine($" has been {verb}");
        }

        private static void Write(Log log)
        {
            var type = log.GetType();
            Console.Write($"The log type \"{type}\" is ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("not supported");
            Console.ResetColor();
        }

        private static void Write(Entity entity)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(entity.Type.Name);
            Console.ResetColor();
        }

        private static void Write(Coords coords)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(coords);
            Console.ResetColor();
        }
    }
}