# MMO Frontend

This repository contains a minimal Unity 2022 project scaffold for early MMO client development.

## Project Structure

```
Assets/
  Prefabs/
  Scenes/
    TestArena.unity
  Scripts/
    Network/
      DummyNetworkManager.cs
    Player/
      PlayerController.cs
      Targeting.cs
    Camera/
      ThirdPersonCamera.cs
    UI/
      TargetUI.cs
  SceneBootstrapper.cs
```

## Usage

1. Open the project in **Unity 2022 LTS**.
2. Ensure the **Input System** and **Cinemachine** packages are installed via the Package Manager.
3. Open `Assets/Scenes/TestArena.unity` and press Play.
4. Use **WASD** to move, **Space** to jump and **Right Mouse Button** to rotate the camera.
5. Press **Tab** to cycle targets. Selected targets display their name and a placeholder HP bar at the top of the screen.

All scene objects are created at runtime by `SceneBootstrapper` for simplicity.
See `Docs/CharacterSetup.md` for details on structuring the player prefab and camera hierarchy.

