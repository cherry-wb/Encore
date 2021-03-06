using System;

namespace Trinity.Encore.Game.Network.Handling
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class WorldPacketHandlerAttribute : PacketHandlerAttribute
    {
        public WorldPacketHandlerAttribute(WorldOpCode opCode)
            : base(opCode)
        {
        }
    }
}
