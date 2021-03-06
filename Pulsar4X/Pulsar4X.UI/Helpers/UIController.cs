﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace Pulsar4X.UI.Helpers
{
    public class UIController
    {
        /// <summary>
        /// The Instance of this class/singelton
        /// </summary>
        private static readonly UIController m_oInstance = new UIController();

        /// <summary>
        /// Returns the instance of the OpenTKUtilities class.
        /// </summary>
        public static UIController Instance
        {
            get
            {
                return m_oInstance;
            }
        }

        /// <summary>
        /// True if running on the mono framework instead of .Net
        /// </summary>
        public bool IsRunningOnMono { get; set; }


        Handlers.SystemGenAndDisplay m_oSystemGenAndDisplay;

        /// <summary>
        /// Handler for all the System Information and Generation Panels, can be used to control them all or just individual ones.
        /// </summary>
        public Handlers.SystemGenAndDisplay SystemGenAndDisplay
        {
            get
            {
                return m_oSystemGenAndDisplay;
            }
        }

        Handlers.SystemMap m_oSystemMap;

        /// <summary>
        /// Handler for all the System Map Panels.
        /// </summary>
        public Handlers.SystemMap SystemMap
        {
            get
            {
                return m_oSystemMap;
            }
        }

        Handlers.Economics m_oEconomics;

        /// <summary>
        /// Handler for all the Economic Screens.
        /// </summary>
        public Handlers.Economics Economics
        {
            get
            {
                return m_oEconomics;
            }
        }

        Handlers.Ships m_oShips;

        /// <summary>
        /// Handler for all the Ship detials screens.
        /// </summary>
        public Handlers.Ships Ships
        {
            get
            {
                return m_oShips;
            }
        }

        Handlers.ClassDesign m_oClassDesign;

        /// <summary>
        /// Handler for all Class Design Screens.
        /// </summary>
        public Handlers.ClassDesign ClassDesign
        {
            get
            {
                return m_oClassDesign;
            }
        }

        Handlers.TaskGroup m_oTaskGroup;

        /// <summary>
        /// Handler for all taskgroup screens.
        /// </summary>
        public Handlers.TaskGroup TaskGroup
        {
            get { return m_oTaskGroup; }
        }

        Handlers.Components m_oComponentRP;

        /// <summary>
        /// Handles research project creation.
        /// </summary>
        public Handlers.Components ComponentRP
        {
            get { return m_oComponentRP; }
        }


        Handlers.FastOOB m_oFastOOB;

        /// <summary>
        /// Handles Racial order of battle creation.
        /// </summary>
        public Handlers.FastOOB FastOOBScreen
        {
            get { return m_oFastOOB; }
        }

        Handlers.MissileDesignHandler m_oMissileDesign;

        /// <summary>
        /// Missile Design handler.
        /// </summary>
        public Handlers.MissileDesignHandler MissileDesign
        {
            get { return m_oMissileDesign; }
        }

        Handlers.TurretDesignHandler m_oTurretDesign;
        /// <summary>
        /// Turret design handler.
        /// </summary>
        public Handlers.TurretDesignHandler TurretDesign
        {
            get { return m_oTurretDesign; }
        }

        public bool SuspendAutoPanelDisplay { get; set; }

        /// <summary>
        /// Constructor that prevents a default instance of this class from being created.
        /// </summary>
        private UIController() { }

        /// <summary>
        /// Initialises this singelton.
        /// </summary>
        public void Initialise()
        {
            // Test to see if we are running on mono:
            Type t = Type.GetType("Mono.Runtime");
            if (t != null)
            {
                IsRunningOnMono = true;
            }
            else
            {
                IsRunningOnMono = false;
            }

            SuspendAutoPanelDisplay = false;

            // now init ui handlers
            m_oSystemGenAndDisplay = new Handlers.SystemGenAndDisplay();
            m_oSystemMap = new Handlers.SystemMap();
            m_oEconomics = new Handlers.Economics();
            m_oShips = new Handlers.Ships();
            m_oClassDesign = new Handlers.ClassDesign();
            m_oTaskGroup = new Handlers.TaskGroup();
            m_oComponentRP = new Handlers.Components();
            m_oFastOOB = new Handlers.FastOOB();
            m_oMissileDesign = new Handlers.MissileDesignHandler();
            m_oTurretDesign = new Handlers.TurretDesignHandler();
        }

        #region PublicMethods

        /// <summary>
        /// Enables SM mode.
        /// </summary>
        public void SMOn()
        {
            m_oSystemGenAndDisplay.SMOn();
            m_oShips.SMOn();
            m_oClassDesign.SMOn();
            m_oComponentRP.SMOn();
            m_oMissileDesign.SMOn();
            m_oTurretDesign.SMOn();
        }

        /// <summary>
        /// Disables SM mode
        /// </summary>
        public void SMOff()
        {
            m_oSystemGenAndDisplay.SMOff();
            m_oShips.SMOff();
            m_oClassDesign.SMOff();
            m_oComponentRP.SMOff();
            m_oMissileDesign.SMOff();
            m_oTurretDesign.SMOff();
        }

        /// <summary>
        /// This Function will update the active/shown Panels on a DockPanel based on the current active panel.
        /// </summary>
        /// <param name="a_oDockPanel">The Dockpanel</param>
        public void DockPanelActiveDocumentChanged(DockPanel a_oDockPanel)
        {
            if (SuspendAutoPanelDisplay)
            {
                return; // do nothing because we dont want to :)
            }

            if (a_oDockPanel == null || m_oSystemGenAndDisplay == null || m_oSystemMap == null
                || m_oEconomics == null || m_oShips == null || m_oClassDesign == null || m_oTaskGroup == null
                || m_oComponentRP == null || m_oFastOOB == null || m_oMissileDesign == null || m_oTurretDesign == null)
            {
                return; // do nothing if we dont have the full UI!!
            }

            if (a_oDockPanel.ActiveDocument == null)
            {
                return; // another sainty check.
            }

            if (a_oDockPanel.ActiveDocument.GetType() == typeof(Panels.SGaD_DataPanel))
            {
                m_oSystemGenAndDisplay.ActivateControlsPanel();
            }
            else if (a_oDockPanel.ActiveDocument.GetType() == typeof(Panels.SysMap_ViewPort))
            {
                m_oSystemMap.ActivateControlsPanel();
            }
            else if (a_oDockPanel.ActiveDocument.GetType() == typeof(Panels.Eco_Summary))
            {
                m_oEconomics.ActivateSummaryPanel();
            }
            else if (a_oDockPanel.ActiveDocument.GetType() == typeof(Panels.Ships_Design) || a_oDockPanel.ActiveDocument.GetType() == typeof(Panels.Individual_Unit_Details_Panel))
            {
                m_oShips.ActivateShipListPanel();
            }
            else if (a_oDockPanel.ActiveDocument.GetType() == typeof(Panels.ClassDes_DesignAndInfo))
            {
                m_oClassDesign.ActivateOptionsPanel();
                //m_oClassDesign.ActivatePropertiesPanel();
            }
            else if (a_oDockPanel.ActiveDocument.GetType() == typeof(Panels.TaskGroup_Panel))
            {
                m_oTaskGroup.ShowAllPanels(a_oDockPanel);
            }
        }

        #endregion
    }
}
