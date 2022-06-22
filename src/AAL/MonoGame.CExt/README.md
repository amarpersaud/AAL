# MonoGame.CExt
Custom extensions to the MonoGame framework.

## Nested Namespaces

### Extensions
Extensions to add functions to existing classes
- Math Extensions
- SpriteBatch Extensions for drawing custom sprite objects
- String Extensions for handling whitespace and key conversion

### Input
Classes for helping with input handling (e.g. detecting new/old presses)

### Sprites
Custom Sprite classes
- Sprite (base class)
  - Practically the same as Texture2D
- NineSliceSprite
  - Splits texture into nine regions for better scaling without distortion of borders
- StackedSprite
  - Stacks textures for a pseudo-3d effect

### UI
Classes for generating and drawing a user interface. Contains a UIControl base class and several derived classes such as a Button and Label.

### Utility
Non-essential utility functions that are helpful.