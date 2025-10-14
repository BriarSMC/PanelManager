# Panel Manager Implementation Document

<!-- The following forced line break is used so the version # is single spaced. -->

**13-Oct-2025**\
_Version 0.1.0_

Copyright © 2025 by Steven M. Coghill\
This project is licensed under the MIT License.A copy of the MIT License can be found in the accompanying LICENSE.txt file.

---

## Document Purpose

This document details how the PanelManager Unity Package is constructed and how to package manages UI panels.

## Unity Package

PanelManager is meant to be a Unity package available for download and installation from the Unity Asset Store.

The following scripts comprise the package:

- PanelManager.cs
- Panel.cs

The following files are included with the package:

- IMPLEMENATION.md (this file)
- README.md (Mostly introduction and version information)
- USERGUIDE.md (How to use PanelManager)
- LICENSE.txt

## Source Code

PanelManager is OpenSource under the MIT License. The GitHub repository for the source is:

https://www.github.com/BriarSMC/PanelManager

## Building the Package

//TODO: Add Building Package Chapter

## UI

Panel Manager controls displaying panels on a UI Canvas. The UI has a similar structure:

```
UI Document (optional)
  Canvas
    PanelManager (empty game object)
      Panel
      Panel
      Panel
      .
      .
      .
```

<!--PanelManager must be in the child tree of the Canvas object. Panels must be children of the **PanelManager** object. **PanelManager.cs** finds all the `<Panel>` child objects and adds them to its list of managed panels. -->

The UI consists of a **PanelManager** object with one or more child UI panel objects. The developer attaches the **PanelManager.cs** script to an child object of the **Canvas** UI object. Usually an empty object. (The **PanelManager.cs** script can be attached to the **Canvas** object, but not recommended for clarity's sake.) The developer attaches **Panel.cs** script to each of the UI panel objects.

**PanelManager** uses to List<Panel> objects to control what panels

## How It Works

The goal of **PanelManager** is to control which of multiple UI Panels to display on the UI canvas. The manager uses a _stack_ to control which UI panels the UI currently displays. This developer pushes **Panel** objects onto the stack as needed. The manager displays top **Panel** on the stack. It does this my using `Panel.gameObject.SetActive(bool)`. The manager sets all of the **Panel** objects on the stack to _false_ except for the top element (which is set to _true_). A result of this approach is that, regardless of panel positioning on the canvas, only the top **Panel** on the stack is visible. (A future version may allow the **Panel** objects to occlude objects lower on the stack.)

The developer controls the stack by using the **PanelManager** methods:

- ManagerEnable
- Push
- Pop
- PopTo
- Swap

### Setup

**PanelManager** requires only one setup step. That is to optionally set:

`[SerializeReference] Panel initialPanel`

This allows the developer to direct which **Panel** **PanelManager** displays when activated. This is optional. If not set (null), then **PanelManager** will use the first child **Panel** object as the first **Panel** to display.

Each UI Panel requires two setup steps.

First the developer **Panel** optionally sets:

`[SerializeField] string panelName`

If the the developer does not set this property, then **Panel** will use the GameObject.name as the **Panel** name.

Second the developer must attach a separate, controller script to each UI panel object. We do not recommend modifying the **Panel.cs** script to control how individual UI panels respond to game events.

Each individual UI panel script must include the following to access the **PanelManager** methods and properties:

```
  [SerialReference] PanelManager panelManager;
```

This is not optional. The developer must set this property in the Unity Editor Inspector window so that the script can access **PanelManager** properties and methods.

### PanelManager

**_Properties_**

- firstPanel
- Version
- managedPanels
- panelStack

`firstPanel` controls which panel to display first and can be set by the developer in the Unity Editor Inspector.

`Version` is a constant string with this package's version number.

`managedPanels` is a `List<Panel>` used to store references to the child **Panel** objects.

`panelStack` is a `List<Panel>` used to store the current chain of displayed **Panel** objects.

**_Unity Methods_**

`Start()`

1. Loads **managedPanels** with references to all the **PanelManager** child **Panel** objects
1. If **initialPanel** is not set (null), then set it to the first element of **managedPanels**
1. Push **initialPanel** onto **panelStack**
1. Disable the **PanelManager**

Disabling the **PanelManager** prevents it from displaying the UI at start up. The developer must explicitly call `PanelManager.ManagerEnable(true)` to display the first panel. Likewise, the developer calls `PanelManager.ManagerEnable(false)` to stop displaying UI panels.

**_Public Methods_**

The public methods allow the developer to manipulate the `panelStack` list. **PanelManager** always displays the **Panel** that is on top of the stack.

**_Private Methods_**

These methods allow **PanelManager** to manipulate the `managedPanels` list. **Panel** objects should never manipulate this list.

### Panel Class

**_Properties_**

- panelName
- PanelObject
- PanelName
- PanelIndex

**_Unity Methods_**

`OnAwake()`

We use `OnAwake()'` to insure the **Panel** objects are initialized before **PanelManager** accesses them in its `Start()` method. We set the **Panel** object's properties in this method.

`public override string ToString()` allows for easy conversion of a **Panel** object to string format.

**_Public Methods_**

`public void SetPanelName(string panelName)` allows the developer to set the **Panel** objects name programmatically. **WARNING:** _The developer must ensure that the rest of the application uses this new name when calling **PanelManager** methods._

**_Private Methods_**

None.

## Panel Class

### Serialized Properties

`[SerializeField] string panelName;`

**panelName** is a private, serialized property the developer can set in the Unity Editor Inspector. If present (not null), then the constructors or the `Awake()` method will set property **PanelName** to this value.

### Public Properties

`public GameObject PanelObject`

**PanelObject** stores the GameObject of the panel.

`public string PanelName`

**PanelName** is the name of the object.

`public int PanelIndex`

**PanelIndex** is the index the **PanelManager** uses to directly reference the panel in its list.

## Private Properties

None.

### Constructors

`public Panel()` - The `Awake()` method it initialize the public properties in this case.

`public Panel(string)` - Initializes both the **PanelName** property.

**Note:** _PanelManager is written right now to support only the null constructor. The developer attaches the panel script to objects created in the Unity Editor **Panel** objects Scene hierarchy. The package does not support dynamic creation of **Panel** objects. However, future versions may support this. So these constructors are here as placeholders for when we implement this ability._

### Unity Methods

`void Awake()`

_Function_

- Initialize PanelObject
- Initialize PanelName

`Awake` Sets **PanelObject** to `this.gameObject`.
If the developer set **panelName** in the Unity Editor Inspector, then `Awake` will set **PanelName** to this value. Otherwise, it sets it to `this.gameObject.name`.

`public override void ToString()`

_Function_

- Return the properties of the Panel object as a string.

The string will contain `PanelObject.name`, `PanelName`, and `PanelIndex.ToString()` separated by a "/" character.

### Public Methods

`public void SetPanelName(string)`

_Function_

- Set the index of the panel in the **PanelManager**'s list of managed panels.

### Private Methods

None.

## PanelManager Class

## Serialized Properties

`[SerializeReference] Panel initialPanel`

First **Panel** To display when Unity starts **PanelManager**.

## Public Properties

`public const string Version`

Contains the version of the package.

`public List<Panel> managedPanels`

**PanelManager** stores references to all the panels it controls. The order of panels is what `GetComponentsInChildren<Panel>` returns.

`public List<Panel> panelStack`

**PanelManager** uses this list as a stack of **Panel** objects to display. The last element in the list is the panel currently displayed.

## Private Properties

None.

## Constructors

No explicit constructor.

## Unity Methods

`void Start()`

- Find all the child **Panel** objects.
- Throw and exception if none are found. We do not provide a "no **Panel**s solution.
- If the developer didn't set the first **Panel** to display in the Unity Editor Inspector, then set the first **Panel** to display to the first **Penal** returned when finding all child **Panel** objects.
- Push the first **Panel** onto the stack.
- Disable the PanelManager logic. Application must explicitly enable **PanelManager** to display UI panels.

**NOTE:** _Need to add logic to throw an error if any of the child panels contain duplicate names._

## Public Methods

`public void ManagerEnable(bool)`

When true, just turn on the last panel on the stack. Otherwise, turn off all of the panels in the stack.

**NOTE:** _This is left over from when we had each Panel object call PanelManager to
add itself to the managedPanels list. We refactored so that the PanelManager now
finds all of its child Panel objects and adds them to the list. We are keeping it in
because if we ever allow for dynamically created panels, then this will the be way
to add them to our list._

`public void TurnOffAllPanels()`

Turn all panels off. This approach is brute force. We don't go through the panel stack and try to figure out which panels to turn off. Since this version simply displays the last **Panel** on the stack we just turn everything off cuz we're lazy.

`public void TurnOffPanel(Panel)`

Turn the specified panel off.

`public TurnOnPanel(Panel)`

Turn the specified panel on

`public int GetManagedPanelCount()`

Just return the number of elements in the managed panels list.

**NOTE:** \_We added this method just for completeness sake. No one outside of PanelManager should care about how many elements are in the managed panel list. So we don't need a public method, and then we don't need a method period since we can just use `managedPanels.Count`.

`public int GetPanelStackCount()`

Return the number of panels on the stack.

`public int Push(string)`\
`public int Push(Panel)`

Push the specified panel onto the stack.

Return the index of the panel on the stack.

Throw an exception if the **Panel** is not contained in `managedPanels`.\
Add the **Panel** to the end of the stack. Turn on the new **Panel**.

`public int Pop([int])`

We throw an exception if the stack doesn't have enough element for the Pop request. (We are not a forgiving package. (i. e. We're lazy. (We think we mentioned this before.)))

Pop specified number of **Panel** objects from the stack. The default is 1.

We turn off all of the **Panel** objects until we are finished. Then we turn on the last item in the stack.

We use the `List.RemoveRange()` to pop off items.

`public int PopTo(string)`\
`public int PopTo(int)`\
`public int PopTo(Panel)`

Delete all the items on top of the stack until we reach the specified panel.

`public void Swap()`

Swap the last two (2) entries on the stack.

`public int GetPanelStackCount

## Private Methods

`private bool IsPanelManaged(Panel)`\
`private bool IsPanelManaged(GameObject)` (might be redundant)\
`private bool IsPanelManaged(name)`

Return if the specified **Panel** object exists in the `managedPanels` list.

**NOTE:** _May have to implement `private bool IsPanelManaged(int)` for completeness._

`private Panel FindManagedPanel(string)`\
`private Panel FindManagedPanel(Panel)`\
`private Panel FindManagedPanel(int)`

Return the panel the specified **Panel** if it is in the `managedPanels` list. Return null if not found.

`private Panel GetStackPanel(string)`\
`private Panel GetStackPanel(GameObject)`\
`private Panel GetStackPanel(int)`

Return the panel the specified **Panel** if it is in the `panelStack` list. Return null if not found.

**NOTE:** _We don't use these in the class. We wrote the in anticipation of using them. We will ponder deleting them in the future._

`private void FindPanels()`

Find all child **Panel** objects and load the **Panel** references into the `managedPanels` list.

`private void DeleteStackFromIndex(int)`

Remove the specified **Panel** (by index) from the `panelStack` list.
