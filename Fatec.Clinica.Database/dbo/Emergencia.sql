CREATE TABLE [dbo].[Emergencia]
(
	[Id] INT NOT NULL IDENTITY,
	[IdPaciente] INT NOT NULL,
	[IdMedico] INT NULL,
	[DataHora] DATETIME NULL,
	[Status] CHAR

	CONSTRAINT [PK_Emergencia] PRIMARY KEY (Id),
	CONSTRAINT [PK_Emergencia_Paciente] FOREIGN KEY(IdPaciente) REFERENCES [Paciente]([Id]),
	CONSTRAINT [FK_Emergencia_Medico] FOREIGN KEY(IdMedico) REFERENCES [Medico]([Id])
)