# Panel Manager Implementation Document

<!-- The following forced line break is used so the version # is single spaced. -->

**13-Oct-2025**\
_Version 0.1.0_

---

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

PanelManager must be in the child tree of the Canvas object. Panels must be children of the **PanelManager** object. **PanelManager.cs** finds all the `<Panel>` child objects and adds them to its list of managed panels.

### Scripts

The **PanelManager.cs** script must be attached to the **PanelManager** object and the **Panel.cs** script must be attached to all the **Panel** objects.

Each individual **Panel** must have a separate user-written script to control actions for that **Panel**. Include the following to access the Panel Manager methods and properties:

```
  [SerialReference] PanelManager panelManager;
```

Drag the **PanelManager** object in the Unity Editor to this field.

## Panel Class

### Serialized Properties

`[SerializeField] string panelName;`

**panelName** is a private, serialized property the developer can set in the Unity Editor. If present (not null), then the constructors or the `Awake()` method will set property **PanelName** to this value.

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
If the developer set **panelName** in the Unity editor, then `Awake` will set **PanelName** to this value. Otherwise, it sets it to `this.gameObject.name`.

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

**PanelManager** stores references to all the panels it controls. The order of panels is what `GetCompnentsInChildren<Panel>` returns.

`public List<Panel> panelStack`

**PanelManager** uses this list as a stack of **Panel** objects to display. The last element in the list is the panel currently displayed.

## Private Properties

None.

## Constructors

No explicit constructor.

## Unity Methods

## Public Methods

## Private Methods
