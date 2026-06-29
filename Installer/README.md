# Mission Planner Installer by Ashish

This folder contains the Inno Setup script used to build the Windows installer for the our Mission Planner application.

## Files

* **mission_planner_setup.iss** - Inno Setup script used to generate the installer.
* **README.md** - Documentation for building and maintaining the installer. (you are corrently reading this)
* **installer_icon.ico** *(optional)* - Custom installer icon.
* **LICENSE.txt** *(optional)* - License displayed during installation.

---

## Prerequisites

Before building the installer, ensure:

* Inno Setup 6.x is installed.
* The Mission Planner project has been successfully built in **Release** mode.
* The script points to the correct Release output directory.

Typical build output:

```
bin/Release/net461/
        MissionPlanner.exe
        *.dll
        Drivers/
        Plugins/
        gdal/
        ...
```

---

## Building the Installer

1. Build the Mission Planner solution in Release mode.
2. Open `mission_planner_setup.iss` in Inno Setup.
3. Verify the source paths if the project structure has changed.
4. Click run (or press F9).
5. The installer executable will be generated in the configured output directory.

---

## Updating the Installer

When releasing a new version:

* Update `MyAppVersion`.
* Update `OutputBaseFilename` if required.
* Ensure any new files or folders are included.
* Test the installer on a clean Windows machine before distribution.

---

## Notes

* The installer copies the complete Release directory to the installation folder.
* Desktop shortcut creation is optional during installation.
* The installer launches Mission Planner after successful installation.
* The installer includes an uninstaller.

---

## Repository

This installer script is maintained alongside the source code so installer changes are version-controlled with the application.
