![.NET](https://img.shields.io/badge/.NET-11.0.preview6-blue?logo=dotnet)
![MAUI](https://img.shields.io/badge/MAUI-11.0.preview6-brightgreen?logo=dotnet)
![xUnit](https://img.shields.io/badge/xUnit-tests-orange)
![License](https://img.shields.io/badge/License-MIT-green)


# OctoType

Do you want to learn dactylo ? Or improve your typing speed ?  
OctoType have been design in this very way  
Select you exercice, type then look at your stats.  


---
## Exercices

You can design your own exercices  
You will have to select the letters you want, text you want or dynamically generate pseudo words based on these letters.  


---
## Roadmap
- [ ] Add user database for exercice's stats  
  - [ ] Local 
  - [ ] Online (not the priority)
- [ ] From stat page, Add buttons: redo, or next exercice
- [ ] Exercices settings: reorder exercice

---

# Architectue
MVVM  
Clean Architecture

```mermaid
flowchart LR

Domain["Domain"]
Application["Application"]
UI["Maui"]
ViewModels["ViewModels"]
Infrastucture["Infrastructure"]

Infrastucture --> Application
UI ---> Application
UI --> ViewModels
ViewModels --> Application
Application --> Domain

```

---
## Technos
.NET11 preview5  
MAUI11.0.0-preview.5  
EF Core 11.0.0-preview.5  
Sqlite 11.0.0-preview.5  
CommunityToolkit.Mvvm 8.4.2  
CommunityToolkit.Maui 14.2.0  
Google.Protobuf 3.35.1  
Grpc.Tools 2.81.1  

----

## Licence  
Mit