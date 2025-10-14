using UnityEngine;
using System.Reflection;

namespace CoghillClan.PanelManager
{
    /**
     *
     * Copyright © 2025 by Steven M. Coghill
     * This project is licensed under the MIT License.
     * A copy of the MIT License can be found in the 
     * accompanying LICENSE.txt file.
     **/

    /** 
    * https://games.coghillclan.net/unity/packages/PanelManager
    * 
    * https://www.github.com/BriarSMC/Panel/Manager
    *
    * Version: 0.1.0
    * Version History
    * ----------------------------------------------------------------------------
    * 0.1.0    13-Oct-2025 Pre-release initial version
    **/
    public class Panel : MonoBehaviour
    {
        [SerializeField] string panelName;

        public GameObject PanelObject { get; private set; }
        public string PanelName { get; private set; }
        public int PanelIndex { get; private set; }

        /**
        * Constructors
        **/

        public Panel()
        {
        }

        public Panel(string panelName)
        {
            this.PanelName = panelName;
        }

        /** 
         * Unity Methods
         **/
        void Awake()
        {
            /**
             * Store our GameObject
             * Store the Panel name.
             *   If the constructor has not set the name (PanelName is null) and panelName is not 
             *   blank, then set the name to the serialized field panelName. (Meaning developer set the
             *   name in the Unity Editor.)
             *   If PanelName is still blank, then set PanelName to the gameObject.name.
             *
             * NOTE: Even though the Implementation Document says we don't support dynamically created
             *       Panel objects yet, these statements allow for it.
             **/
            this.PanelObject = this.PanelObject == null ? this.gameObject : this.PanelObject;
            if (string.IsNullOrEmpty(this.PanelName) && !string.IsNullOrEmpty(panelName)) this.PanelName = panelName;
            if (string.IsNullOrEmpty(this.PanelName)) this.PanelName = this.gameObject.name;
        }

        public override string ToString()
        {
            // Simple string format of the object
            return $"{this.PanelObject.name}/{this.PanelName}/{this.PanelIndex}";
        }

        /**
         * Public Methods
         **/
        public void SetPanelName(string panelName)
        {
            // The PanelManager has to set this property through this method
            this.PanelName = string.IsNullOrEmpty(panelName)
                || string.IsNullOrWhiteSpace(panelName) ? this.PanelName : panelName;
        }
    } // End of Class Panel
} // End of Namespace CoghillClan.PanelManager
