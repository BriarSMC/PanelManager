using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine.Rendering;

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
     * https://games.coghillclan.net/
     * 
     * https://www.github.com/BriarSMC/
     *
     * Version: 0.1.0
     * Version History
     * ----------------------------------------------------------------------------
     * nn.nn.nn dd-mmm-yyyy Comment 
     **/
    public class PanelManager : MonoBehaviour
    {

        [SerializeReference] Panel initialPanel;

        public const string Version = "0.1.0";
        public List<Panel> managedPanels;
        public List<Panel> panelStack = new List<Panel>();

        /**
         * Unity Methods
         **/

        void Start()
        {
            /**
             * Die if no managed panels exist.
             * If initialPanel isn't set, then just use the first one in the managed panels.
             * Disable all panels.
             * Push the initial panel onto the stack.
             **/

            FindPanels();
            // TODO: Need to throw an exception if managedPanels contains duplicate names.

            if (managedPanels.Count == 0) throw new ApplicationException("PanalManager: No panels found.");
            if (initialPanel == null) { initialPanel = managedPanels[0]; }
            Push(initialPanel); // Put it on the stack
            ManagerEnable(false);
        }



        /**
         * Public Methods
         **/

        public void ManagerEnable(bool enable)
        {
            /**
             * When true, just turn on the last panel on the stack.
             * Otherwise, turn off all of the panels in the stack.
             **/

            if (enable) TurnOnPanel(panelStack[panelStack.Count - 1]);
            else
                TurnOffAllPanels();
        }
        //TODO Consider making the next 3 methods "private"
        public void TurnOffAllPanels() { foreach (Panel panel in managedPanels) { panel.gameObject.SetActive(false); } }
        public void TurnOffPanel(Panel panel) { panel.gameObject.SetActive(false); }
        public void TurnOnPanel(Panel panel) { panel.gameObject.SetActive(true); }

        public int AddManagedPanel(Panel panel)
        {
            /**
             * Throw an exception if the panel is not in our list.
             * Add the panel to our managedPanels list.
             * Return the index in the managedPanels list of the new panel.
             *
             * Note: This is left over from when we had each Panel object call PanelManager to 
             * add itself to the managedPanels list. We refactored so that the PanelManager now
             * finds all of its child Panel objects and adds them to the list. We are keeping it in
             * because if we ever allow for dynamically created panels, then this will the be way 
             * to add them to our list.
             **/

            if (IsPanelManaged(panel)) { throw new ApplicationException($"Panel already exists. Not added. Name: {panel.PanelName}"); }
            managedPanels.Add(panel);
            return managedPanels.Count - 1;
        }

        public int GetManagedPanelCount() { return managedPanels.Count; } //TODO Possibly remove method
        public int GetPanelStackCount() { return panelStack.Count; }

        public int Push(string name) { return Push(FindManagedPanel(name)); }
        public int Push(Panel panel)
        {
            Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}(Panel)> {panel?.PanelName}");

            /**
             * If the panel is in the managed list, then we can process it
             * We need to make the current panel (last one in the stake) disabled. (If it exists.)
             * Add the panel to the stack.
             * Enable it.
             * Return the stack index of the entry.
             *
             * Otherwise, bitch, whine and moan about it.
             **/
            if (FindManagedPanel(panel))
            {
                if (panelStack.Count > 0) TurnOffPanel(panelStack[panelStack.Count - 1]);
                panelStack.Add(panel);
                TurnOnPanel(panel);
                return panelStack.Count - 1;
            }
            else
            {
                throw new ApplicationException("Cannot Push Panel. Panel not found in managed list.");
            }
        }

        public int Pop() { return Pop(1); }
        public int Pop(int count)
        {
            /** 
             * If there are not enough panels on the stack, then we will just remove everything after the first panel.
             * Otherwise, remove count entries.
             **/
            TurnOffAllPanels();

            if (panelStack.Count <= 0) throw new ApplicationException("Panel stack is empty. Nothing to pop.");
            if (panelStack.Count == 1) return 0; ;

            if (count >= panelStack.Count)
                panelStack.RemoveRange(1, panelStack.Count - 1);
            else
                panelStack.RemoveRange(panelStack.Count - count, count);

            TurnOnPanel(panelStack[panelStack.Count - 1]);
            return panelStack.Count;
        }

        public int PopTo(string name)
        {
            int ndx = panelStack.FindIndex(x => x.PanelName == name);
            DeleteStackFromIndex(ndx);
            return 0;
        }
        public int PopTo(int ndx)
        {
            DeleteStackFromIndex(ndx);
            return ndx;
        }
        public int PopTo(Panel panel)
        {
            int ndx = panelStack.IndexOf(panel);
            DeleteStackFromIndex(ndx);
            return ndx;
        }

        public void Swap()
        {
            if (panelStack.Count <= 1) return;
            TurnOffAllPanels();
            Panel hold = panelStack[panelStack.Count - 1];
            panelStack[panelStack.Count - 1] = panelStack[panelStack.Count - 2];
            panelStack[panelStack.Count - 2] = hold;
            TurnOnPanel(panelStack[panelStack.Count - 1]);

        }

        /** 
         * Private Methods
         **/

        private bool IsPanelManaged(Panel panel) { return managedPanels.SingleOrDefault(p => p.PanelObject == panel) != null ? true : false; }
        private bool IsPanelManaged(GameObject panel) { return managedPanels.SingleOrDefault(p => p.PanelObject == panel) != null ? true : false; }
        private bool IsPanelManaged(string name) { return managedPanels.SingleOrDefault(p => p.PanelName == name) != null ? true : false; }

        private Panel FindManagedPanel(string name) { return managedPanels.SingleOrDefault(p => p.PanelName == name); }
        private Panel FindManagedPanel(Panel panel) { return managedPanels.SingleOrDefault(p => p.PanelObject == panel.gameObject); }
        private Panel FindManagedPanel(int i) { return managedPanels[Math.Clamp(i, 0, managedPanels.Count)]; }

        //TODO Possibly delete this method
        private Panel GetStackPanel(int ndx) { return panelStack.Single(i => i.PanelIndex == ndx); }
        private Panel GetStackPanel(string name) { return panelStack.Single(n => n.PanelName == name); }
        private Panel GetStackPanel(GameObject obj) { return panelStack.Single(o => o.PanelObject); }


        private void FindPanels()
        {
            /**
             * Find all the children panels and load them into managedPanels
             **/
            managedPanels = new List<Panel>(GetComponentsInChildren<Panel>());
        }

        private void DeleteStackFromIndex(int ndx)
        {
            if (ndx == -1) throw new ApplicationException("Could not pop to requested panel. Panel not found.");
            TurnOffAllPanels();
            panelStack.RemoveRange(ndx + 1, panelStack.Count - ndx - 1);
            TurnOnPanel(panelStack[ndx]);
        }
    } // End of Class PanelManager
} // End of Namespace CoghillClan.PanelManager Coghill
