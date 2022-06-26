# UI
For creating UI Controls. Contains base classes for UI handling and descendant classes.

## Base Classes

### UIControl
Implements a UI Control that holds information about its location and size in relation to its parent object, determines if it is 
clicked or hovered over by the mouse, and triggers mouse events.

### UIHandler
Root element in a UI tree. Handles determining which of the controls is the topmost element where the mouse is placed and updates all of the UI controls.

## Derived Controls
- Label
  - Represents a label for displaying text. Can trigger mouse events
- Button
  - Represents a clickable button with text. Triggers mouse events.
- Panel
  - Panel that holds other controls. Optionally scrollable

## Misc Classes
- Side
  - Represents a side of the control
- Anchor
  - Holds information about how a side of the control is anchored to its parent
- UIControlEventArgs
  - Event arguements for UIControl events (e.g. mouse events)
- UIOverflow
  - How overflow of contents is shown (visible, hidden, scroll)
- CoordinateHelper
  - Helper class for converting between percentage coordinates (0.0-1.0) and pixel coordinates
