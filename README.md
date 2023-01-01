### Overview
AQ3D Commander is an addon to add a few QoL commands to the game, such as missing join commands for various areas. *As of right now, join commands are the only thing inside, but more will possibly in the future.*

You won't need a new version for any added join commands, they're fetched from my api on runtime. So whenever I add one, restart the game to use it.

Join commands can be seen here: [https://api.saryion.com/aq3d/joincommands](https://api.saryion.com/aq3d/joincommands).

### Instructions
DLL included in releases page, you only need to call `Commander.Load()` in the `AQ3D_Commander` namespace to load it.