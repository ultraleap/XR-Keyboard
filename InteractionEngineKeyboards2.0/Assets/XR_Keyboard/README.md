# Setup Instructions

## Dependencies

- Interaction Engine
- NaughtyAttributes
- TextMeshPro

## Set Up The Keyboard

1. Place the Keyboard prefab into the scene
2. Add the player camera as the Head Transform

## Set Up The Text Fields

1. Add `InputFieldTextReceiver` to any text fields you want the keyboard to input to (note: only compatible with TextMeshPro InputFields)

## Set Up The Ball Gizmos

1. Set the left & right hand index tip on your hand mesh in the targets array in `Scale Based On Distance From Target` on the following objects
   1. `InteractionEngineUIKeyboardWithGrabHandle -> GrabFollow -> GrabGimbal -> Visuals`
   2. `InteractionEngineUIKeyboardWithGrabHandle -> RotationGizmos -> RotationGizmoLeft -> Sphere`
   3. `InteractionEngineUIKeyboardWithGrabHandle -> RotationGizmos -> RotationGizmoRight-> Sphere`