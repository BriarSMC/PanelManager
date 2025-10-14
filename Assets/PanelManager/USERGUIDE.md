# Panel Manager User Guide

<!-- The following forced line break is used so the version # is single spaced. -->

**13-Oct-2025**\
_Version 0.1.0_

Copyright © 2025 by Steven M. Coghill\
This project is licensed under the MIT License.A copy of the MIT License can be found in the accompanying LICENSE.txt file.

---

## Introduction

### Document Purpose

This document explains how to user the Panel Manager Unity Package.

### What Is the PanelManager

PanelManager is a Unity Package that aids you in displaying UI Panel objects on the UI Canvas. PanelManager lets you create an empty game object as a UI Canvas child. You can then add UI Panel objects as children of the PanelManager object. PanelManager provides methods that control when its child members are displayed.

In this version of PanelManager only one UI Panel is visible at any one time.

When your application begins PanelManager displays none of the UI Panels. It will not display any panels until your application calls the `ManagerEnable()` method. You application then controls which UI Panel is displayed by using methods like `Push()` and `Pop()`.

PanelManager maintains a stack (List) of what UI Panels your application wants to display. This gives your application to _backtrack_ to a previously displayed UI Panel. Or, jump to a previously displayed panel.

### Package Contents

The PanelManager package contains the following files:

- PanelManager.cs
- Panel.cs
- USERGUIDE.md (this file)
- README.md
- IMPLEMENTATION.md
- LICENSE.txt

PanelManager.cs defines the **PanelManager** class. You attach this script to your PanelManager game object under the UI Canvas object. Panel.cs defines the **Panel** class. Each UI Panel child of **PanelManager** uses this script. Additionally, you must write scripts for the UI Panels to control each UI Panels interaction with the application.

## Getting Started

### Install the Package

You can install this package from the Unity Asset store or by downloading it from GitHub and installing it manually. The package contents will be installed to the folder:

`Packages/PanelManager`

### Create the PanelManager Game Object

1. In the Unity Editor create your UI root (it can be an empty GameObject, UI Canvas or UI Document). If you create a GameObject or UI Document, then create a UI Canvas beneath it.
2. Create child a UI Canvas if needed.
3. Choose the PanelManager GameObject. If desired, you can use the UI Canvas as your PanelManager. Or you can create an empty UI Canvas child node to act as the PanelManager.
4. Attach the script PanelManager.cs to the PanelManager node you selected above.

### Create the Panel Objects

1. Create one or more UI Panel objects as children of the PanelManager. Each of these will be a UI Panel managed by PanelManager.
2. Write and attach control scripts (your application logic) to each of the UI Panels.
3. Optionally assign names to your Panels.

We highly recommend assigning unique names to all of your Panel objects. This is the easiest way for your application to manipulate them. If you do not assign a name, then Panel will use the GameObject's name as the name.

### Starting the PanelManager

1. First you should assign the first Panel to display in the Unity Editor Inspector window. Drag your first Panel object to the **Initial Panel** field for the PanelManager object. If you do not assign a Panel to this field, then, at Start, PanelManager will use the first child Panel object it finds as the Initial Panel.
2. When the application starts the PanelManager disables itself. This is so the application must explicitly tell the PanelManager when to start displaying panels. The application calls `PanelManager.ManagerEnable(true)` to do this. And when the application wants to stop displaying panels it calls `PanelManager.ManagerEnable(false)`.

### Navigating Panels

PanelManager keeps track of Panels it's displayed on a stack (last in/first out). This allows your application to easily _back out_ (reverse the stack) to display previous panels. You use the following PanelManager methods to display panels. (PanelManager will Push the initial Panel onto the stack at start up.)

- Push()
- Pop()
- PopTo()
- Swap()
- ManagerEnable()

See the **PanelManager Class** Section for a full description of each method.

You use Push() to tell PanelManager to add the specified Panel to the stack and display it.

Pop() is used to Pop one (1) or more panels from the stack and display the item at the top of the resulting stack.

PopTo() is used to tell PanelManager to find the top stack panel matching the specified Panel, pops the other Panels from the stack, and displays the resulting top stack element.

When the application no longer needs to display the UI, then it calls the method `ManagerEnable(false)`.

Your individual Panel scripts control the applications logic and how to control the PanelManager.

## PanelManager Class

### Definition

Namespace: CoghillClan.PanelManager\
Source: PanelManager/PanelManager.cs

PanelManager controls display of its child Panel objects.

C#

```
private PanelManager panelManager;
```

### Static Properties

| Property | Description                                    |
| :------- | :--------------------------------------------- |
| Version  | String containing the version of PanelManager. |
|          |                                                |

### Serialized Fields and References

| Property     | Description                        |
| :----------- | :--------------------------------- |
| initialPanel | Sets the initial Panel to display. |
|              |

### Constructors

| Constructor             | Definition |
| :---------------------- | :--------- |
| No explicit constructor |            |
|                         |            |

### Properties

| Property      | Description                                          |
| :------------ | :--------------------------------------------------- |
| managedPanels | List<Panel> of all PanelManager child Panel objects. |
| panelStack    | List<Panel> of all displayed panels (stack);         |
|               |                                                      |

### Methods

| Method                                      | Action                                                                                                                    |
| :------------------------------------------ | :------------------------------------------------------------------------------------------------------------------------ |
| ManagerEnable(bool)                         | True begins displaying Panel objects. False stops displaying Panel objects. Returns void.                                 |
| GetManagedPanelCount()                      | Returns int of number of list elements in the managedPanel list.                                                          |
| GetPanelStackCount()                        | Returns int of number of list elements in the panelStack list.                                                            |
| Push(string)<br>Push(Panel)                 | Pushes specified panel onto the panelStack. Returns int of the Panel's index in the panelStack.                           |
| Pop()<br>Pop(int)                           | Pop int entries from the panelStack (1 is the default). Return int of the top panel in the stack.                         |
| PopTo(string)<br>PopTo(int)<br>PopTo(Panel) | Removes all the elements above the specified panel on the panelStack. Returns int of the Panel's index in the panelStack. |
| Swap()                                      | Swaps the top two (2) entries in the panelStack. Returns void.                                                            |
|                                             |                                                                                                                           |

## Panel Class

### Definition

Namespace: CoghillClan.PanelManager\
Source: PanelManager/Panel.cs

Panel defines a panel in the PanelManager.

C#

```
private PanelManager panelManager;
```

### Constructors

| Constructor   | Definition                   |
| :------------ | :--------------------------- |
| Panel()       | No properties set.           |
| Panel(string) | PanelName set to the string. |
|               |                              |

### Properties

| Property    | Description                                                    |
| :---------- | :------------------------------------------------------------- |
| PanelObject | GameObject of this panel.                                      |
| PanelName   | Name of this panel.                                            |
| PanelIndex  | Index of this panel in the PanelManager's managed panels list. |
|             |                                                                |

### Methods

| Method               | Action                                               |
| :------------------- | :--------------------------------------------------- |
| ToString()           | Returns string representation of the properties.     |
| SetPanelName(string) | Sets object's PanelName to the string. Returns void. |
|                      |
