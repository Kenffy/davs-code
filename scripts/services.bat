@echo off
setlocal EnableDelayedExpansion
set PARAMETERS=%1

set ARGS_STRING=
:LOOP_START
if "%2"=="" goto :LOOP_END
set ARGS_STRING=%ARGS_STRING% %2
shift
goto :LOOP_START
:LOOP_END

echo ARGS_STRING !ARGS_STRING!

set CURRENT_FOLDER=%cd%
echo CURRENT_FOLDER: %CURRENT_FOLDER%


if "!PARAMETERS!"=="" (
    echo parameter required, check script for options

    goto error
) else if "!PARAMETERS!"=="--help" (
    REM Help for this batch file
    echo Available command line parameters: --execute, --shutdown, --build-sln, --clean-build-sln, --build-execute, --clean-build-execute

    goto success
) else (
    REM execute auth service
    echo execute auth service
    call authenticate.bat %PARAMETERS%
    REM execute products service
    echo execute products service
    call products.bat %PARAMETERS%
    REM execute orders service
    echo execute orders service
    call orders.bat %PARAMETERS%

    goto success
) 

:error
echo reached error exit handler
exit /B 1

:success
echo reached success exit handler
exit /B 0