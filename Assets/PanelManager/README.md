# PanelManager

PanelManager consists of the following:

- PanelManager.cs (Class PanelManager)
- Panel.cs (Class Panel)

Panel Manager allow the Unity programmer to easily manage UI Panel components in their application. This package provides:

1. A way to group a set of panels into a collection.
1. Ability to automatically display and hide panels as needed.
1. Provides a stack oriented approach to displaying panels.

## How It Works

### Panel Manager

The developer creates an empty object to be the Panel Manager. This panel manager will manage one set of panels. Panels managed by
the Panel Manager are children of the Panel Manager. The **Panel** object will add itself to the managed panels list.

### Panel

## Class PanelManager

### Properties

- **_List\<Panel\> managedPanels_**

  This is a list of all the panels under this panel manager's control. The public method **AddManagedPanel(Panel)** allows a panel to add itself to the list.

- **_List<Panel> panelStack_**

  This is a list (stack) of panels as they are displayed _one on top of the other_.

### Public Methods

- **_int AddManagedPanel(Panel panel)_**

  Adds panel to the end of the _managedPanels_ list. It returns the index of the new list item.

- **_int GetManagedPanelsCount_**

  Return the number of items in _managedPanels_.

- **_int GetPanelStackCount_**

  Return the number of items in _panelStack_.

### Static Methods

### Private Methods

- **_bool IsPanelManaged(Panel panel)_**\
  **_bool IsPanelManaged(string name)_**

  Test to see if an object already exists in the managedPanels list.

## Class Panel

### Properties

### Public Methods

### Static Methods

### Private Methods
