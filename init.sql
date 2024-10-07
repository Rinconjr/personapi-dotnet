-- Habilitar opciones avanzadas
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;

-- Habilitar autenticación mixta (SQL Server y Windows)
EXEC sp_configure 'mixed mode authentication', 1;
RECONFIGURE;

-- Habilitar el inicio de sesión de 'sa'
ALTER LOGIN sa ENABLE;
GO
