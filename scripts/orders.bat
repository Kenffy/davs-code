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


set SLN_DIR=%~dp0..\services\order\CodeWithDavs.Orders
echo SLN_DIR: %SLN_DIR%
set PROJECT_DIR=%SLN_DIR%\Orders


cd %SLN_DIR%

if "!PARAMETERS!"=="--help" (
    REM Help for this batch file
    echo Available command line parameters: --execute, --shutdown, --build-sln, --clean-build-sln, --build-execute, --clean-build-execute

    goto success
) else if "!PARAMETERS!"=="--execute" (
    REM execute programm
    echo CURRENT_FOLDER: %PROJECT_DIR%
    cd %PROJECT_DIR%
    echo run orders service
    dotnet run

    goto success
) else if "!PARAMETERS!"=="--shutdown" (
    REM shutdown programm
    echo CURRENT_FOLDER: %PROJECT_DIR%
    cd %PROJECT_DIR%
    echo terminate all orders service dotnet servers...
    dotnet build-server shutdown

    goto success
) else if "!PARAMETERS!"=="--build-execute"  (
    echo Build orders service solution...
    dotnet build
    REM execute programm
    echo CURRENT_FOLDER: %PROJECT_DIR%
    cd %PROJECT_DIR%
    echo run orders service
    dotnet run

    goto success
) else if "!PARAMETERS!"=="--clean-build-execute"  (
    echo Clean orders service solution...
    dotnet clean
    echo Build orders service solution...
    dotnet build
    REM execute programm
    echo CURRENT_FOLDER: %PROJECT_DIR%
    cd %PROJECT_DIR%
    echo run orders service
    dotnet run

    goto success
) else if "!PARAMETERS!"=="--build-sln" (
    echo Build orders service solution...
    dotnet build
    if !ERRORLEVEL! neq 0 goto error

    goto success
) else if "!PARAMETERS!"=="--clean-build-sln" (
    echo Clean orders service solution...
    dotnet clean
    echo Build orders service solution...
    dotnet build
    if !ERRORLEVEL! neq 0 goto error

    goto success
) else (
    echo parameter required, check script for options
    goto error
)

:error
echo reached error exit handler
cd %CURRENT_FOLDER%
exit /B 1

:success
echo reached success exit handler
cd %CURRENT_FOLDER%
exit /B 0