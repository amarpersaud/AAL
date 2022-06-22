# Sprites
Classes for drawing textures to the screen in various ways

## Sprite
The base sprite class holds a texture and is used to draw a texture to the screen similar to a Texture2D

## NineSliceSprite
Inherits from Sprite. Used to slice a texture into nine regions. The corner regions receive little or no stretching, and edges are stretched or tiled in one dimension. The center is stretched or tiled in two dimensions. This preserves borders on textures so that they can be scaled without aliasing artifacts.

## StackedSprite
Composed of a series of textures which each represent one horizontal slice from a 3d object. When drawn on top of each other and then offset, it gives a pseudo-3D effect.

## Borders
Represents the border used to cut a NineSliceSprite at