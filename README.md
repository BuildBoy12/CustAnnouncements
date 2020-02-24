# Custom Announcements
A tinybrain's port of Cyanox's Custom Announcements from SMod for EXILED.
- Note this plugin does not include the prior's `countdown`, `timer`, `autowarhead`, `waitingforplayers`, `preset`, or `textannouncement` configs/commands.

- Requires [EXILED](https://github.com/galaxy119/EXILED/).
- All C.A.S.S.I.E. acceptable words can be found [here](https://pastebin.com/rpMuRYNn).

## Configs

| Config Option | Value Type | Default Value | Description |
|:------------------------:|:----------:|:-------------:|:------------------------------------------:|
| `ca_EnableCHS` | bool | false | Enables/disables the Chaos Insurgency spawn announcement. |
| `ca_CHSMessage` | string |  | Announcement that will be played when Chaos spawns. |
| `ca_EnableRE` | bool | false | Enables/disables the Round End announcement. |
| `ca_REMessage` | string |  | Announcement that will be played when the Round Ends. |
| `ca_EnableRS` | bool | false | Enables/disables the Round Start announcement. |
| `ca_RSMessage` | string |  | Announcement that will be played when the round starts. |
| `ca_EnableDE` | bool | false | Enables/disables the DClass escape announcement. |
| `ca_DEMessage` | string |  | Announcement that will be played when a DClass escapes. |
| `ca_EnableSE` | bool | false | Enables/disables the Scientist escape announcement. |
| `ca_SEMessage` | string |  | Enables/disables the DClass escape announcement. |
| `ca_EnableDE` | bool | false | Enables/disables the DClass escape announcement. |
| `ca_JoinMsgs` | dict |  | People and their corresponding announcement |

- __Usage for ca_JoinMsgs: PlayerID@steam:announcement, PlayerID@discord:announcement__

## Commands
### Note you can use v/p instead of view/play
- ca: Displays commands and info

- chs (view/play): Plays configured chaos announcement

- re (view/play): Plays configured round end announcement

- rs (view/play): Plays configured round start announcement

- de (view/play): Plays configured DClass escape announcement

- se (view/play): Plays configured Scientist escape announcement

- mtfa (scps left) (mtf number) (mtf letter): Plays mtf annoucement

- scpa (scp number) (death type): Plays scp death announcement
    - Use tesla, dclass, science, mtf, or chaos for `death type`
    - You can also use t, d, s, m, or c respectively
