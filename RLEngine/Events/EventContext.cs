using RLEngine.Turns;
using RLEngine.Boards;

namespace RLEngine.Events
{
    internal class EventContext
    {
        public EventContext(EventQueue eventQueue, TurnManager turnManager, Board board)
        {
            EventQueue = eventQueue;
            TurnManager = turnManager;
            Board = board;
        }

        public EventQueue EventQueue { get; }
        public TurnManager TurnManager { get; }
        public Board Board { get; }
    }
}