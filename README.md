# EndlessRunner3d
## About the game
A simple Endless Runner game prototype inspired by Subway Surfers. The player avoids obstacles and switches lanes.

On the releases page, you can download and test the prototype.

## Gameplay video

https://github.com/user-attachments/assets/90de8363-87d8-4e50-a99a-8b014914a2e8

## Features
- **Dual state machine architecture for player control:** horizontal machine (line switching), vertical machine (sliding, jumping, falling)
- **State machine for a game flow:** Idle => Playing => GameOver
- **Procedural world generation:** world is generated from predefined segments, segments are spawned in random order
- **Object pooling system:** separate pool for each segment prefab, spawner requests instances from random pools
- **Difficulty scaling system:** controlled via AnimationCurve what allows precise tuning of speed progression over time
- **Customizable player movement:** AnimationCurves used for jump and line switching
- **Slope surface handling:** smooth movement on slope surfaces
- **Curve world shader:** custom shader to curve game world like in the Subway Surfers
- **Smooth transition from main menu to gameplay:** achieved by using Cinemachine
- **With Music and sound effects**
- **Designed with possible scalability in mind**

## Tech Stack
- Unity
- C#
- Zenject
- Shader Graph
- Blender (for 3D models)

## Architecture Decisions
Used `CharacterController` instead of `Rigidbody` to maintain full control over movement and avoid unnecessary physics complexity.

Instead of using a hierarchical state machine, I implemented **two independent state machines**:
- One handles horizontal movement (lane switching)
- Another handles vertical movement (sliding, jumping, falling)

This approach simplified the logic and avoided unnecessary complexity, while still allowing combined movement (e.g. jumping while switching lanes).

Instead of relying on magic numbers like `jump force` or `lane change speed`, I used **AnimationCurves** to define movement behavior.
This allows precise control over:
- Jump duration and height
- Lane switching timing
- Acceleration and deceleration

As a result, movement feels more predictable and is easier to tweak without changing code. Also this approach improves designer-friendliness (as values can be tuned visually in the editor).

## What I Learned
- Designing modular gameplay systems using state machines
- Balancing flexibility and complexity in architecture decisions
- Implementing procedural generation with object pooling
- Using AnimationCurve for precise gameplay tuning
- Working with URP and Shader Graph for visual effects
- Using Zenject to decouple the scripts
- Blender to Unity workflow
