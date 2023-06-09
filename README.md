### What does this mod do?

I heard the rumor that there are people around that tend to forget the time
in Sons of the Forest and so also forget to regularly save the game.

As you can quickly end up in situations that are pretty bad, not having a
recent save game can set you back by hours. This mod will show you the time
since you have saved for the last time on the screen, so that you hopefully
don't ever forget about it again.

It does not autosave or anything else. It would be easy to implement some
autosave mod, as there are functions available for this, but it drastically
changes the game experience. So this will not be included.

### Forking the mod

I will only maintain this mod as long as I use it for myself. But feel free to
fork and do your own development.

Please honor the license: This is GPLv2.1. Whenever you copy the code and
create your own code based on it, release your changes. OpenSource mods are the
driving force of the modding community. Doing closed source stuff, destroys the
spirit of the community. Stealing code ignoring the licenses, is even worse.

Be fair, as the more open the community is, the better mods will be

### License

    Sons Save Clock - Save Game Clock for Sons of the Forest
    Copyright (C) 2023  eluxo <code@eluxo.net>

    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA

See LICENSE.md for details.

### Build instructions

Install VisualStuio 2023 and make sure that you have netstandard2.1 
(required dotnet version for BepInEx) installed.

Also make sure that you have the proper BepInEx version for SotF
installed
([I used this one](https://thunderstore.io/c/sons-of-the-forest/p/BepInEx/BepInExPack_IL2CPP/)).

As it makes things easier, if you don't have to copy your libraries manually
and as shipping those via github is not an option due to copyright issues, I
have added an environment variable you can set to have all references
in your project.

Set the variable "SOTF_INSTALL" to the path where you have your BepInEx
modded SotF installed. For the default use:

    setx "SOTF_INSTALL" "C:\Program Files (x86)\Steam\steamapps\common\Sons Of The Forest" /M

Afterwards, you can build the mod and it will automatically placed into the
plugins folder as a post build step.

Have fun! <3

    


