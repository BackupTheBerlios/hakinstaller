IMPORTANT NOTES
===============

This tool REQUIRES version 1.0 of the Microsoft .NET Framework,
this is available for download at:

http://www.microsoft.com/downloads/details.aspx?familyid=D7158DEE-A83F-4E21-B05A-009D06457787&displaylang=en


GENERAL INFORMATION
===================

HakInstaller is a tool that allows you to easily add player based haks
to modules.  A player based hak is a hak whose contents are intended
for the player, rather than the module designer.  For example the PRC
Consortium's PRC pack.  I wrote this tool because I got sick of having
to add these haks to module after module as I played them, and having
to remember to do all of the steps correctly (often forgetting one).
Two versions of the tool are included, a windowed version and a command
line version.  The command line version is suitable for being invoked
from batch files (or even NWNX) whereas the full UI version is more
suitable to the individual player.

The application takes a 'hak' (in this case a 'hak' is a collection of 
one or more hak, erf, and tlk files, and adds/merges them (as appropriate)
to a module, and also sets up any required module event that the hak
may need to handle to work correctly.  If the module already has scripts
attached to the events, then the tool will create a new script that
executes both the old script and the haks's script.  In a nutshell, this
allows you to add haks to a module by pressing a button and letting the
tool do all the work, rather than you having to go through a checklist
of steps, possibly missing one.

The tool gets the configuration information for a hak from hif files
(hak information), these files are text files that contain configuration
information, THEY SHOULD BE PLACED IN YOUR NWN HAK DIRECTORY, the tool
will look for them there.  The tool comes with hif files for the PRC
Consortium's PRC pack, my spell pak, and the PRC merge version of my
spell pak, but there is nothing to stop anyone else from making hif
definitions for other haks.

If all you want to do is use the tool to add the PRC pack or spell pak
to modules, just copy the hif files into NWN's hak directory and double
click HakInstaller.exe.

To use the windows UI version just double click on HakInstaller.exe,
it will present you with a list of haks that have hif files on the left
and a list of all of your installed modules on the right, check the haks
that you want to install and check the modules you want them installed
in and click install, it will then add/merge the hak content to all of
your selected modules.  If there are any file conflicts (for example,
suppose you merge the prc pack hak into a module that has changed some
spell scripts, a warning dialog will be displayed showing you the files
that are going to be overwritten asking for your permission to overwrite
them.  You can check the files you want to overwrite in the module (by
default they are all checked) and you can view the hak / module scripts
in wordpad to compare them.  To view a script file, select it then right
click in the list box, you will get a menu.

To use the command line version run it from the command line, passing it
all the .hif and .mod files you want to use on the command line.  It will
then do the same thing as the windows UI version, except that it will not
warn you about overwriting files, it will just overwrite them.

Both versions of the tool read your NWN installation information from
the windows registry, if you are running the tools on a PC other than the
one that NWN is installed on (or you are running them under mono on linux)
then you must use the -n<path> command line option to specify the install
path of NWN.


VERSION HISTORY
===============

1.4 - Added support for adding areas to modules in the HIF.  Added support
for BMU music files in erfs/haks/modules.

1.32 Fixed duplicate key bug.

1.31 Fixed a bug that caused the window to not come up centered.  Changed
the content check list box to be single selection, since this was causing
some confusion.

1.3 Changed to add any areas in added content to the module's area list to fix
a bug introduced in 1.62.  Added support for the OC/XP1/XP2.  Changed the
installer check to see if content has already been installed in modules, and
warn the user before continuing.  Added extensive overwrite checking, it
will now check the module and all it's haks vs. the content being added
to make sure nobody overwrites anybody else.  Fixed the bug with GFF
versions.  Fixed the bug with "imput string is not in the correct format".

1.22 Fixed a bug that cause an exception if you did not overwrite replaced
files.

1.21 Updates HIF files for PRC pack 1.9

1.2 Changed the replace file dialog so that you can selectively replace files,
and view script files (.NSS) in wordpad.

1.1 Fixed a bug that made certain ExoLoc strings crash the application.

1.0 Initial version








APPENDIX 1 - SAMPLE HIF FILE
============================

The sample hif files contain comments in them that document the layout and
syntax of a hif file, this appendix assumes that you have already looked
at one of the sample hif files, I will go over the prc_consortium.hif file
here and show what is happening.

First, here is the contents of the hif file (minus the giant comment at the
top describing the format of hif files):

# Import there ERF files into the module.
erf : prc_consortium.erf

# Haks and custom tlk's used by the module.
module.Hak : prc_consortium.hak
module.CustomTlk : prc_consortium.tlk

# Events that need to be wired up.
module.OnClientEnter : prc_onenter
module.OnPlayerLevelUp : prc_levelup
module.OnPlayerEquipItem : prc_equip
module.OnPlayerUnequipItem : prc_unequip

# Cache PRC scripts for better performance.
module.Cache : screen_targets
module.Cache : prc_caster_level
module.Cache : set_damage_type
module.Cache : change_metamagic
module.Cache : add_spell_dc
module.Cache : add_spell_penetr
module.Cache : add_damage
module.Cache : add_damageshield
module.Cache : add_randdamage
module.Cache : add_healing
module.Cache : add_hitpoints


First there is a single erf given, prc_consortium.erf.  The tool merges the
contents of all erf files into the module, so all of the files in
prc_consortium.erf will be added to the module.  

It will then add prc_consortium.hak to the module's hak list (if multiple 
haks are specified they will be added in the order they are in the hif 
file, which means hak files listed first will take precedence over hak files 
listed later).

prc_consortium.tlk will be set as the module's custom tlk file.  

The scripts prc_onenter, prc_levelup, prc_equip, and prc_unequip will be added
to the OnClientEnter, OnPlayerLevelUp, OnPlayerEquipItem, and OnPlayerUnequipItem
module events.  If any of these events already have scripts attached, then
the tool will create a new script, naming it "hif_<EventName>" (truncating to
16 characters) and will then add ExecuteScript() calls to execute the old
script and the appropriate PRC script.

You will notice that the above steps are what the PRC pack's read me tell you to
do to each module you add the pack to.

One final step has been added, which is a speed optomization.  The scripts
screen_targets, prc_caster_level, set_damage_type, change_metamagic, 
add_spell_dc, add_spell_penetr, add_damage, add_damageshield, add_randdamage,
add_healing, and add_hitpoints will be added to the module's script cache.
This step is not strictly required to get the PRC pack to work, however it
will increase the speed at which the pack runs.  These scripts are called
internally when any spell (or some feats) are used, adding them to the cache
prevents the NWN engine from having to load them each time you cast a spell.

CAVEATS

The tool does not update the palette files, so any blueprint templates imported
from erf's will not show up in the toolset until you right click in the
appropriate custom palette and do a refresh.

The tool DOES NOT rebuild the module like the BioWare toolset
does whenever you add a hak.  If a hak/erf that the hif is importing contains
a modified BioWare script include file, then the module will not work properly
without being recompiled in the toolset.  For haks like this you will still
have to open the module in the toolset after HakInstaller is done and do a
rebuild of the module.  For example if you use the tool to import a hif that
overwrote the include file for the default combat AI, none of the changes would
take effect on any of the module's creatures until you opened the module in
the toolset and did a build (the include file is there but since no scrips have
been recompiled none of them know about the changes).
