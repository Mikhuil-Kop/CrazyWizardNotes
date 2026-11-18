**English** | [Русский](README.ru.md)

# CrazyWizardNotes

Puzzle / god simulator / wizard simulator.  
A **Unity (C#)** game.

> Status: in active development

## About

CrazyWizardNotes is a puzzle game that blends god-sim and wizard-sim elements. The player interacts with a small settlement by casting a variety of spells. The game is divided into levels, and each level asks you to complete a simple task by combining interacting systems and spells.

Villagers eat, go hungry, work, and talk to each other. Every villager has their own mood, which certain spells can influence. You can soak, set on fire, heat, or move every stone, plank, and bush. A flexible architecture with many abstractions lets the systems stay deeply developed while remaining easy to connect with one another.

The project is built on my custom framework **UNet**. UNet lets you develop a Unity game in the style of **ASP.NET**: use startup files, register services, inject dependencies through constructors, and structure the architecture in an **ECS**-like way.

The framework separates logical entities (components) from GameObjects. This gives several advantages:

- Reduces the extra data that MonoBehaviours usually carry
- Entities are not tied to specific GameObjects and communicate through component interfaces
- Virtual entities (Actors) can exist without a GameObject representation

The source code is open for study and for creating non-commercial mods. Once finished, the game is planned for a free Steam release.

## License

This project is distributed under a custom non-commercial license.

You may:

- Download and play the game.
- Study and modify the source code.
- Create and share non-commercial mods.

You may **not**:

- Sell the game or any modified versions of it.
- Use the project for commercial purposes.

Full license text is available in the [LICENSE](LICENSE) file.