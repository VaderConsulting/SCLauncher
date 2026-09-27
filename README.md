# SCLauncher

WPF launcher that picks a System Center Service Manager (SCSM) or Operations Manager (SCOM) Dev, Test, or Prod environment, writes `SDKServiceMachine` under HKCU (and the SCOM equivalent), and starts the matching console from Program Files. Combo boxes list the three SDK machines plus a “set at launch” option; an optional checkbox closes the launcher after the console starts. Open `SCLauncher.sln` in Visual Studio.

**Source last updated:** 2014-01-21  
**Language:** C#  
**Target:** .NET 4.5  
**Output:** WinExe

## What it is

This is Dave Robinson’s WPF .NET 4.5 WinExe (assembly title SCSMLauncher, copyright 2013). It reads SCSM and SCOM host names from application settings, stores the chosen SDK machine in the console’s HKCU user-settings key, then launches Microsoft.EnterpriseManagement.ServiceManager.UI.Console.exe or the Operations Manager monitoring console.

Live server names were replaced with example hosts (see `App.config.example`). Copy `SCLauncher/App.config.example` to `SCLauncher/App.config` and `SCLauncher/Properties/Settings.settings.example` to `SCLauncher/Properties/Settings.settings`, then put your own SDK machine names in those files before building.

## Solution structure

| Project | Language | Output | Path |
|---------|----------|--------|------|
| `SCLauncher` | C# | WinExe | `SCLauncher/SCLauncher.csproj` |

## How to open

Open `SCLauncher.sln` in Visual Studio.

## Requirements

- Visual Studio 2013, .NET Framework 4.5

## Attribution and provenance

Working copy from my Historical Dev folder.

- **Author:** Dave Robinson / VaderConsulting
- **Assembly title:** SCSMLauncher
- **Assembly copyright:** Copyright © 2013

## License

MIT. See `LICENSE`.
