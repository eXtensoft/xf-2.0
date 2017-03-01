SELECT SPECIFIC_SCHEMA AS [StoredProcedureSchema],
		SPECIFIC_NAME AS [StoredProcedureName], 
		ORDINAL_POSITION AS [OrdinalPosition], 
		PARAMETER_MODE AS [Mode], 
		PARAMETER_NAME AS [ParameterName], 
		DATA_TYPE AS [Datatype],
		CHARACTER_MAXIMUM_LENGTH AS [MaxLength]
		 FROM INFORMATION_SCHEMA.[PARAMETERS] JOIN SYSOBJECTS so ON SPECIFIC_NAME = so.name
		 WHERE so.xtype = 'P' AND left(SPECIFIC_NAME,3) != 'sp_'
		 

UNION ALL SELECT SPECIFIC_SCHEMA AS StoredProcedureSchema, 
		name AS StoredProcedureName,
		0 AS [OrdinalPosition],
		NULL AS [Mode],
		NULL AS [ParameterName],
		NULL AS [Datatype],
		NULL AS MaxLength
FROM sys.procedures JOIN INFORMATION_SCHEMA.ROUTINES ON sys.procedures.name = INFORMATION_SCHEMA.ROUTINES.ROUTINE_NAME
WHERE left(name,3) != 'sp_' 
ORDER BY [StoredProcedureName], [OrdinalPosition]