﻿using System;
using Mono.GameMath;
using Trinity.Encore.Game.Partitioning;

namespace Trinity.Encore.Game.Entities
{
    public abstract class WorldEntity : Entity, IWorldEntity
    {
        public abstract Vector3 Position
        {
            get;
        }

        public ISpacePartition Node
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
