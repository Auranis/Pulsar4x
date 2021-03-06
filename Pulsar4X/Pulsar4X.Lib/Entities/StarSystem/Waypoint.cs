﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pulsar4X.Entities
{
    public class Waypoint : StarSystemEntity
    {
        /// <summary>
        /// Which player set this waypoint?
        /// </summary>
        public int FactionId;

        public Waypoint(String Title, StarSystem Sys, double X, double Y, int FactionID)
        {
            Name = Title;

            /// <summary>
            /// create these or else anything that relies on a unique global id will break.
            /// </summary>
            Id = Guid.NewGuid();

            FactionId = FactionID;
            Position.System = Sys;
            Position.X = X;
            Position.Y = Y;

            SSEntity = StarSystemEntityType.Waypoint;
        }
    }
}
