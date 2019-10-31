# editorconfigenforcer

This is a small Visual Studio Extension, which automatically updates yours root .editorconfig file, when solution is being opened

## Plot

Checks the url -> downloads .editorconfig file -> writes it to target folder
The whole process is triggered by solution opening or configuration change

## Installation

Launch and install .vsix package as usual. Can't be installed from marketplace for now.

## Configuration

Extension adds it's own section in "Tools -> Options..." dialog under "EditorConfig enforcer"
Configuration incudes two parameters:
* Download url
  
  url to get .editorconfig file from, for instance, github's "raw" view of .editorconfig in team repo
* Projects root

  target location for .editorconfig file, like the folder containing all of your projects

[![Build status](https://ci.appveyor.com/api/projects/status/lq0yc3jy96rqvs0f?svg=true)](https://ci.appveyor.com/project/amzak/editorconfigenforcer)
