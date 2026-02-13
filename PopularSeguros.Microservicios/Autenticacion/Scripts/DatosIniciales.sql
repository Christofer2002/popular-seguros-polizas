-- Script para insertar usuarios de prueba en la tabla UsuarioTable
-- NOTA: Ejecutar después de aplicar la migración

-- Usuario de prueba: allfernandez / Popularseguros2026
-- Hash BCrypt generado con work factor 11
INSERT INTO [PopularSegurosDb].[AutenticacionSchema].[UsuarioTable] (Id, NombreUsuario, Contraseña, Email, Activo, FechaCreacion)
VALUES 
(NEWID(), 'allfernandez', '$2a$12$9/nMjyj9B2KQVmgWzgY4lOGXmniv/mBGRWLmdl1SJy3x0c7BUnnMC', 'allfernandez@bp.fi.cr', 1, GETUTCDATE());