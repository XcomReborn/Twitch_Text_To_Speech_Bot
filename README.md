# CSharpTwitchTTSBotGUI

 A twitch bot to provide text to speech on Windows configurable using a GUI created using WPF.

## User Instructions for Windows:

-- To be added once released--

## Bot Commands.

### default Bot commands for Streamer/AdminUserName:

**!ignoreword [word]** - will mute a message containing a specific word eg: http or www.  
**!unignoreword [word]** - will remove the word from the ignore word list.  
**!closetts** - will close the program.  
**!substitute [word] [substitute]** - will substitute the words in the substitute for the word.  
**!removesubstitute [word]** - removes the word from the substitute dictionary. 

### ==Advanced Commands==

**!regex [pattern] [substitute]** - will substitute the words in the substitute for the regular expression match in the pattern. Care should be taken not to use a broad match search.  
**!removeregex [pattern]** - removes the pattern from the regex substitute dictionary.  

### Bot commands restricted to twitch channel moderators:

**!voices** - lists all available voices.  
**!voice #** - sets the users voice to an available voice listed in !voices.  
**!uservoice [userName] #** - sets the userName's voice to an available voice listed in !voices.  

**!alias [alias]** - sets mod username alias.  
**!useralias [userName] [alias]** - sets another users alias.  

**!ignore [userName]** - ignores the following user name.  
**!unignore [userName]** - unignores the following user name.  
**!blacklist** - lists all the usernames in the blacklist (!ignorelist).  


## Compilation Instructions.
 
 A simple bot that uses the twitchlib library and System.Speech.Synthesis windows SAPI5 to speak text written in twitch IRC Chat.  

 Dependancies:

 Requires the following libraries:

 TwitchLib 3.3.0 - https://github.com/TwitchLib/TwitchLib
 System.Speech - https://docs.microsoft.com/en-us/dotnet/api/system.speech.synthesis.speechsynthesizer?view=netframework-4.8

These can be installed in your IDE of choice, if you are using VSCode NUGet, I would recommend installing NuGet Gallery(v0.0.24) via the marketplace.

Library Install Instructions (VSCode):

Open market place (Ctrl+Shift+x)
search for and install NuGet.

In Command Pallet (Ctrl+Shift+P)
open NuGet Gallery
search for and install TwitchLib 3.3.0
search for and install System.Speech 6.0.0
search for and install NewtonSoft.Json 13.0.1

If using Visual Studio 2022.

Install Packages using: Tools -> NuGet Package Manager -> Manage NuGet Packages For Solution...
search for and install TwitchLib 3.3.0
search for and install System.Speech 6.0.0
search for and install NewtonSoft.Json 13.0.1


<OutputType> WinExe
<TargetFramework> net6.0-windows
<UseWPF> true


