# Character Setup Guidelines

This project uses a simple player prefab composed of a physics capsule and a visual armature. The following structure keeps the hierarchy tidy and ensures the player only renders the model:

```
Player (root GameObject)
├─ CharacterController + PlayerController
└─ PlayerArmature (visuals only)
    └─ cameraRoot (head transform)
```

* **Root (`Player`)**
  * Contains the `CharacterController` component used for collisions.
  * Holds the `PlayerController` script.
  * Does **not** render a mesh. The capsule primitive is kept only for physics.
* **PlayerArmature**
  * Child object with the visible skinned mesh and animations.
  * No scripts are attached directly to the armature.
* **cameraRoot**
  * Transform under the armature (usually at the head) used as the follow target for the Cinemachine camera.

The `PlayerController` automatically disables any `MeshRenderer` or `CapsuleCollider` found on the root so the capsule mesh stays hidden in Play mode.
