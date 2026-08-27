# ShishaBar

ShishaBar is a Unity-based business simulation game prototype where the player manages a shisha lounge.

The project was inspired by classic business management games such as *Pizza Syndicate*, but focuses on running a shisha bar — serving customers, managing seating, preparing orders and handling the day-to-day workflow of the lounge.

## Current Features

* Customer spawning system
* Automatic customer seating
* Available seat/chair management
* NPC movement using Unity NavMesh
* Player-controlled waiter
* Click/touch-based movement
* Interactive objects and UI
* Customer order events
* Shisha order generation
* Different shisha names, flavors and strength values
* Order and completed-order tracking
* Character animations
* Basic kitchen / preparation interaction logic

## Gameplay Flow

The current prototype implements the foundation of the customer service loop:

```text
Customer Spawns
      ↓
Finds Available Seat
      ↓
Moves to Chair
      ↓
Places Shisha Order
      ↓
Waiter Interacts With Customer
      ↓
Order Preparation
      ↓
Order Delivery
```

The goal is to gradually expand this into a complete business simulation where the player can manage and grow their own shisha lounge.

## Tech Stack

* **Unity 6**
* **C#**
* **Unity NavMesh**
* **Unity Input System**
* **Unity Animator**
* **TextMesh Pro**

Developed with:

```text
Unity 6000.0.24f1
```

## Project Structure

```text
Assets/
├── Animation/
├── Art/
├── CharactersArt/
├── Materials/
├── Menu/
├── Prefebs/
├── Scenes/
└── Scripts/
    ├── Chair.cs
    ├── Customer.cs
    ├── CustomerSpawner.cs
    ├── IAvailablePlace.cs
    ├── IInteractible.cs
    ├── KitchenTable.cs
    ├── OpenOrdersPrep.cs
    ├── Orders.cs
    ├── Seat.cs
    ├── SeatHolder.cs
    ├── Shisha.cs
    ├── Timer.cs
    └── Waiter.cs
```

## NPC System

Customers are spawned dynamically and search for available seating.

The customer system uses Unity's `NavMeshAgent` to move NPCs through the environment. Once a customer reaches an available chair, the seat becomes occupied and the customer transitions into the ordering state.

## Interaction System

The waiter can move around the environment using mouse or touch input.

Interactive objects implement shared interfaces, allowing the player to interact with different gameplay elements such as customers, seats and preparation areas.

## Order System

Customers generate shisha orders containing properties such as:

* Shisha type
* Flavor
* Strength

The project contains separate collections for active and completed orders, forming the foundation for a larger order-management system.

## Project Status

**Prototype / Work in Progress**

This project was created as a game development and Unity learning project.

The current version focuses on building the core systems required for the main gameplay loop rather than providing a finished game.

Potential future features include:

* Economy and money system
* Customer satisfaction
* Employee management
* Shisha inventory
* Product purchasing
* Lounge upgrades
* New furniture and tables
* Multiple locations
* Reputation system
* Business expansion

## What I Practiced

While developing this project I worked with:

* C# gameplay programming
* Object-oriented programming
* Interfaces
* Events
* Unity components
* NPC navigation
* NavMesh
* Character animation
* Player input
* Interaction systems
* Runtime object spawning
* Game state management
* Basic simulation architecture

## Assets

The repository contains both custom project code and third-party Unity assets used for prototyping and development.

## Author

**MXR1XN**
