# Tower Defense (Unity)

Educational / pet project built with Unity featuring a Tower Defense implementation.

## 🎯 Core Mechanics
- Multiple tower types (DPS, AoE, Slow, Single, Boss)
- Each tower can hit a specific enemy only once
- Enemies move along a path using PathProgress
- Separate logic for boss enemies
- Slow effect implemented via SpeedMultiplier without breaking physics

## 🧠 Technical Solutions
- Target selection based on highest PathProgress
- Protection against multiple hits from the same tower
- Fixed double-hit issue with projectiles
- Tower effects extracted into VariousTowerMechanics
- Safe and stable Rigidbody2D handling

## 🛡 Project Protection
- `.editorconfig` — unified code style and UTF-8 encoding
- `.gitattributes` — encoding and EOL protection
- Code comments for learning and maintainability

## 🛠 Tech Stack
- Unity (2D)
- C#
- Git + GitHub

## 📌 Status
Work in progress.
