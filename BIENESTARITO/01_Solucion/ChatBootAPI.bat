@echo off
title Limpiar y desbloquear ChatBootAPI
echo ===============================================
echo   LIMPIEZA Y DESBLOQUEO DE CHATBOOTAPI (.NET)
echo ===============================================
echo.

:: 🔹 Cerrar procesos que bloquean el exe
echo Cerrando procesos dotnet.exe, chatBootAPI.exe e IIS Express...
taskkill /IM dotnet.exe /F >nul 2>&1
taskkill /IM chatBootAPI.exe /F >nul 2>&1
taskkill /IM iisexpress.exe /F >nul 2>&1
echo Procesos cerrados correctamente.
echo.

:: 🔹 Rutas del proyecto (ajusta si es diferente)
set PROJECT_PATH=D:\Informacion\Proyectos\ICBF_BOG\HU\01_ChatBots\01_Solucion\chatBootAPI

:: 🔹 Eliminar carpetas bin y obj
echo Eliminando carpetas temporales (bin y obj)...
if exist "%PROJECT_PATH%\bin" rd /s /q "%PROJECT_PATH%\bin"
if exist "%PROJECT_PATH%\obj" rd /s /q "%PROJECT_PATH%\obj"
echo Carpetas eliminadas correctamente.
echo.

:: 🔹 Confirmación
echo ✅ Limpieza completada.
echo Ahora puedes volver a abrir Visual Studio y compilar el proyecto.
echo.
pause
