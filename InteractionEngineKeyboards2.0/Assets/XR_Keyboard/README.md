# Setup Instructions

## Dependencies

- Interaction Engine
- NaughtyAttributes <https://github.com/dbrizov/NaughtyAttributes>
  - Easy setup - just add the git url to your manifest.json
- TextMeshPro

## Set Up The Keyboard

1. Place the `QwertyKeyboardWithGrabHandle` prefab into the scene

## Set Up The Text Fields

1. Add `InputFieldTextReceiver` to any text fields you want the keyboard to input to (note: only compatible with TextMeshPro InputFields)

## Customisation

The keyboard is an editable set of panels defined by prefabs & keymaps.
There are two main panels - Alphanumeric & Symbols, which are each defined by a keymap, a prefab for the key & a prefab for the key's shadow.

These two panels can be found here:

`QwertyKeyboardWithGrabHandle/QwertyKeyboard/Parent/AlphaNumericPanel`

`QwertyKeyboardWithGrabHandle/QwertyKeyboard/Parent/SymbolsPanel`

### Customising Looks

Most of the customisation for each panel can be done on the `DefaultKey` prefab - here you can change the

- Image used for the key
- Text Mesh
- Interaction Button colour
- Interaction Glow colour
- Sound Effects (`InteractionButtonSounds`)

In addition to this you can change the shadow design by changing the `DefaultShadow` prefab.

Once happy with your new design, select your panel & press `Regenerate Keyboard` to generate a new keyboard prefab with your new design. You can choose for this to overwrite the current prefab you're working on by setting the `Over Write Prefab` option to **true**.

If you choose to use a new key or shadow prefab, you must update the prefab fields on the AlphaNumeric, Symbols and AccentKeys panels to reference your new prefab.

Key, panel and gap size can be changed on the `UI Keyboard Resizer` component on your panel. This tool allows us to set the default size of a key & panels independently of the scale of the gameobject. Change the values on this object and press `Resize Keyboard` to apply your changes.

 ***Note:*** pressing `Regenerate Keyboard` on the `Key Map Generator` also triggers the `Resize Keyboard` function.

## Generating New Keyboard Prefabs

The `KeyMapGenerator` component provides the ability to automatically generate new keyboard layouts based on:

- A prefab for the individual keyboard key
- A prefab for the shadow of the key
- A Key Map that defines what keys are placed on each row of the keyboard

Open up one of prefabs and look at the panel object for an example of how to configure the `KeyMapGenerator`.

You can choose whether to overwrite or generate new keyboard prefabs when you regenerate, just check the box if you want to overrite your current keyboard.

### Prefab Handling

When you use the generator to create a new keyboard it will create a new set of prefabs, leaving your existing prefab intact. This helps prevent accidental overriding of existing prefabs.

### Key Maps

Key maps can be saved and loaded from JSON files to allow for extending the keyboard in built apps. The `DefaultKeyMap` component provides a button that will create a new JSON file, placing it in `StreamingAssets`. This can be used as a template for new key maps.

Add a `JSONKeyMap` component to an object in the scene, give it a path to a JSON file and then `LoadFromJSON` using the button provided. You can then modify the map in the Inspector and save it as a new key map.

## Known issues

- The Max Font Size in can get lost in the prefabs. If the characters look small, edit the max font size in the InteractionButtonUIKey prefab
