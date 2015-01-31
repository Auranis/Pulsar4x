﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Pulsar4X.Entities.Components;

/// <summary>
/// I am concerned that the distances here are going to get too massive without some bounds checking, that will have to be added in at some point.
/// </summary>

namespace Pulsar4X.Entities
{
    public class SystemContact : StarSystemEntity
    {
        /// <summary>
        /// To which faction does this contact belong?
        /// </summary>
        public Faction faction { get; set; }

        /// <summary>
        /// where the contact was on the last tick.
        /// </summary>
        public SystemPosition LastPosition;

        /// <summary>
        /// Bascking entity of this contact.
        /// </summary>
        public StarSystemEntity Entity { get; set; }

        // TODO: Make distance table it's own class/struct, get it out of here.

        /// <summary>
        /// Distance between this contact and the other contacts in the system in AU.
        /// </summary>
        public BindingList<float> DistanceTable { get; set; }

        /// <summary>
        /// Second that the distance table was updated.
        /// </summary>
        public BindingList<int> DistanceTable_LastUpdateSecond { get; set; }

        /// <summary>
        /// Year that the distance table was updated.
        /// </summary>
        public List<int> DistanceTable_LastUpdateYear { get; set; }
        
        /// <summary>
        /// Creates a new system contact.
        /// </summary>
        /// <param name="Fact">Faction of contact.</param>
        /// <param name="entity">Backing entity of the contact.</param>
        public SystemContact(Faction Fact, StarSystemEntity entity)
        {
            Id = Guid.NewGuid();
            faction = Fact;
            Position = entity.Position;
            LastPosition = Position;

            Entity = entity;

            DistanceTable = new BindingList<float>();
            DistanceTable_LastUpdateSecond = new BindingList<int>();
            DistanceTable_LastUpdateYear = new List<int>();

            SSEntity = entity.SSEntity;
        }

        /// <summary>
        /// Updates the location of the contact.
        /// </summary>
        /// <param name="X">X position in AU.</param>
        /// <param name="Y">Y Position in AU.</param>
        public void UpdateLocationInSystem(double X, double Y)
        {
            LastPosition.X = Position.X;
            LastPosition.Y = Position.Y;
            Position.X = X;
            Position.Y = Y;
        }

        /// <summary>
        /// Updates the contact after transiting a jump point, LastPosition.X needs to be set to current position for the travel line.
        /// </summary>
        /// <param name="X">X position in AU in the new system</param>
        /// <param name="Y">Y position in AU in the new system</param>
        public void UpdateLocationAfterTransit(double X, double Y)
        {
            LastPosition.X = X;
            LastPosition.Y = Y;
            Position.X = X;
            Position.Y = Y;
        }

        /// <summary>
        /// Updates the system location of this contact. The 4 blocks of updates to the lists will, I hope, facilitate efficient updating of the binding list.
        /// </summary>
        /// <param name="system">new System.</param>
        public void UpdateSystem(StarSystem system)
        {
            Position.System = system;

            DistanceTable.Clear();
            DistanceTable_LastUpdateSecond.Clear();
            DistanceTable_LastUpdateYear.Clear();

            DistanceTable.RaiseListChangedEvents = false;
            DistanceTable_LastUpdateSecond.RaiseListChangedEvents = false;

            for (int loop = 0; loop < Position.System.SystemContactList.Count; loop++)
            {
                DistanceTable.Add(0.0f);
                DistanceTable_LastUpdateSecond.Add(-1);
                DistanceTable_LastUpdateYear.Add(-1);
            }

            DistanceTable.RaiseListChangedEvents = true;
            DistanceTable_LastUpdateSecond.RaiseListChangedEvents = true;

            /// <summary>
            /// BindingLists apparently do a bunch of events every time they are changed.
            /// This has hopefully circumvented that, with just 1 event.
            /// </summary>
            DistanceTable.ResetBindings();
            DistanceTable_LastUpdateSecond.ResetBindings();
        }
    }
}
