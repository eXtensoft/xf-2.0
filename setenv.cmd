@echo off
REM
REM SetEnv.cmd - Sets up the enlistment environment
REM    2010-12-22 - [thomasde] created
REM   
echo ------------
echo  SETENV.CMD
echo ------------
REM
REM ensure %ROOT% is defined (we DON'T run if ROOT is not defined)
REM
if not defined root (
    echo %%ROOT%% environment variable is not defined.
    echo EXITING...
    pause
    exit
)
REM
REM Start - call VS Environment variables environment setting
REM
REM Dev11?
set vcvarsall="C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\VsDevCmd.bat"
if not exist %vcvarsall% (
	set vcvarsall="C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\Tools\VsDevCmd.bat"
	if not exist %vcvarsall% (
		REM Dev11?
		set vcvarsall="C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\Tools\VsDevCmd.bat"
		if not exist %vcvarsall% (
			REM Dev10?
			set vcvarsall="%programfiles%\Microsoft Visual Studio 10.0\vc\vcvarsall.bat"
			REM Dev10 on Wow?
			if not exist %vcvarsall% (
				REM Maybe we're on an x64 machine with DevEnv 2010 in WoW32
				set vcvarsall="%programfiles(x86)%\Microsoft Visual Studio 10.0\vc\vcvarsall.bat"
			)
		)
	)
)
REM Either call vsvarsall or warn
if exist %vcvarsall% (
    call %vcvarsall% %PROCESSOR_ARCHITECTURE%
	echo vcvarsall set up
) else (
    echo WARNING: environment setup script [vcvarsall or vsdevcmd] was not found...
    echo .........The environment may not behave as expected.
)
REM
REM Set Environment variables
REM
if not defined configuration (
    set configuration=Debug
)
REM
REM Check for the existence of the %platform% environment variable
REM -For some reason, this variable is set to BNB on HP machines
REM
if "%platform%"=="BNB" (
    set platform=
)
REM
REM Set title if defined
REM
if defined title (
    echo Setting title to [%TITLE% - %CONFIGURATION%]
    title [%TITLE% - %CONFIGURATION%]
)
REM 
REM Define the environment variables
REM
if not defined outputbase (
	set outputbase=%root%\bins
)
set private=%root%\private
set devdir=%private%\developer\%username%
set product=%root%\eXtensibleFramework
set sln=%root%\eXtensibleFramework\solutions
set tests=%private%\tests
set tools=%private%\tools
set binaries=%private%\binaries
REM set referencedAssemblies=%root%\externaldlls
set VisualStudioVersion=14.0

REM
REM Output the Environment information
REM
echo %%CONFIGURATION%%             = %configuration%
echo %%ROOT%%                      = %root%
echo %%PRIVATE%%                   = %private%
echo %%PRODUCT%%                   = %product%
echo %%TESTS%%                     = %tests%
echo %%TOOLS%%                     = %tools%
echo %%DEVDIR%%                    = %devdir%
echo %%BINARIES%%                  = %binaries%
echo %%BUILDTOOLS%%                = %buildtools%
echo %%OUTPUTBASE%%                = %outputbase%
REM echo %%REFERENCEDASSEMBLIES%%      = %referencedAssemblies%
echo %%SSSISXMLFILENAME%%          = %ssisxmlfilename% 
echo %%VisualStudioVersion%%       = %VisualStudioVersion%
REM
REM Register the Binaries
REM
set PATH=%path%;%binaries%
if exist "%binaries%\RegisterBinaries.cmd" (
	echo Running "%binaries%\RegisterBinaries.cmd"
	call "%binaries%\RegisterBinaries.cmd"
)
REM
REM Register build tools
REM
set PATH=%path%;%buildtools%
if exist "%buildtools%\RegisterBuildTools.cmd" (
	echo Running "%buildtools%\RegisterBuildTools.cmd"
	call "%buildtools%\RegisterBuildTools.cmd"
)
REM
REM Load the aliases
REM
doskey /macrofile=aliases.pub
REM
REM Remind the user to create a developer directory
REM
if not exist %devdir% (
    echo Developer directory [%devdir%] was not found
)
REM
REM Load the user's aliases if they exist
REM
set devAliases=%devdir%\environment\aliases.pub
if exist %devAliases% (
    echo Loading aliases for [%username%]
    doskey /macrofile="%devAliases%"
) else (
    echo The aliases file for %username% was not found.
    echo Create a file %devAliases% with aliases to use in this enlistment
)
REM 
REM Invoke the user defined setenv.cmd if it exists
REM
set usersetenv=%devdir%\environment\setenv.cmd
if exist %usersetenv% (
    echo Customizing environment for [%username%]
    call "%usersetenv%"
) else (
    echo The environment file for %username% was not found.
    echo Create a file %userSetEnv% with instructions on how to customize your environment
)
REM
REM Display aliases
REM
echo The following aliases are available:
doskey /macros
REM echo Done...
