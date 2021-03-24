# Setup Instructions

## Dependencies

- Interaction Engine
- NaughtyAttributes <https://github.com/dbrizov/NaughtyAttributes>
  - Easy setup - just add the git url to your manifest.json
- TextMeshPro

## Set Up The Keyboard

1. Place the `InteractionEngineUIKeyboardWithGrabHandle` prefab into the scene

## Set Up The Text Fields

1. Add `InputFieldTextReceiver` to any text fields you want the keyboard to input to (note: only compatible with TextMeshPro InputFields)


## Customisation

The keyboard is a set of prefabs that you can edit. Most of the customisation can be done on the `InteractionButtonUIKey` prefab - here you can change

- Image used for the key
- Text Mesh
- Interaction Button colour
- Interaction Glow colour
- Sound Effects (`InteractionButtonSounds`)

In addition to this you can change the shadow colour by editing the colour & pressing "Set Child Image Colours" on the `SetChildImageColour` component on the following:

- `InteractionEngineUIKeyboardWithGrabHandle -> GrabFollow -> GrabGimbal -> UIInteractionEngineKeyboard -> Parent -> NumberRow -> NumberKeysShadows`
- `InteractionEngineUIKeyboardWithGrabHandle -> GrabFollow -> GrabGimbal -> UIInteractionEngineKeyboard -> Parent -> KeyboardKeys -> KeyboardKeysShadows`

## Generating New Keyboard Prefabs

The `KeyMapGenerator` component provides the ability to automatically generate new keyboard layouts based on:

- A prefab for the individual keyboard key
- A prefab for the shadow of the key
- A Key Map that defines what keys are placed on each row of the keyboard

Open up one of prefabs and look at the root object for an example of how to configure the `KeyMapGenerator`.

You can coose whether to overrite or generate new keyboard prefabs when you regenerate, just check the box if you want to overrite your current keyboard.

### Prefab Handling

When you use the generator to create a new keyboard it will create a new set of prefabs, leaving your existing prefab intact. This helps prevent accidental overriding of existing prefabs.

### Key Maps

Key maps can be saved and loaded from JSON files to allow for extending the keyboard in built apps. The `DefaultKeyMap` component provides a button that will create a new JSON file, placing it in `StreamingAssets`. This can be used as a template for new key maps.

Add a `JSONKeyMap` component to an object in the scene, give it a path to a JSON file and then `LoadFromJSON` using the button provided. You can then modify the map in the Inspector and save it as a new key map.

## Known issues

- The Max Font Size in can get lost in the prefabs. If the characters look small, edit the max font size in the InteractionButtonUIKey prefab
- The shadow colour can get lost in the prefabs. If the shadow looks incorrect, follow the instructions in the Customisation section to set the shadow colour
