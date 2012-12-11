﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Newtonsoft.Json;
using Pulsar4X.Entities.Components;

namespace Pulsar4X.Entities
{
    public class ShipClassTN
    {
        public Guid Id { get; set; }
        public Faction Faction { get; set; }

        public string ClassName { get; set; }
        public int HullDescriptionId { get; set; }
        public string Notes { get; set; }
        public decimal BuildPointCost { get; set; }

        /// <summary>
        /// In 50 ton increments
        /// </summary>
        public decimal Size { get; set; }
        public int CrossSection { get; set; }
        public int RequiredRank { get; set; }
        public int CrewSize { get; set; }
        public int MaxLifeSupport { get; set; }
        public int DamageControlRating { get; set; }
        public int FuelCapacity { get; set; }

        /// <summary>
        /// Armor statistics that matter to the class itself.
        /// </summary>
        public ArmorDefTN ShipArmorDef { get; set; }

        /// <summary>
        /// each ship class can only have one type of engine, though several copies may be present.
        /// </summary>
        public EngineDefTN ShipEngineDef { get; set; }
        public ushort ShipEngineCount;

        /// <summary>
        /// Ship class Engine statistics.
        /// </summary>
        public int MaxEnginePower { get; set; }
        public int MaxThermalSignature { get; set; }
        public int MaxSpeed { get; set; }
    }
}
