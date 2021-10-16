using RLEngine.Input;

using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace RLEngine.Runner
{
    public abstract class Subcommand
    {
        protected readonly Action<PlayerInput> callback;

        protected Subcommand(Action<PlayerInput> callback)
        {
            this.callback = callback;
        }

        public abstract Command CreateDefinition();
    }
}