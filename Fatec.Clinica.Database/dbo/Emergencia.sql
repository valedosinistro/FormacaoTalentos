CREATE TABLE [dbo].[Emergencia]
(
	[Id] INT NOT NULL IDENTITY,
	[IdPaciente] INT NULL,
	[IdMedico] INT NULL,

	CONSTRAINT [PK_Emergencia] PRIMARY KEY (Id),
	CONSTRAINT [FK_Emergencia_Paciente] FOREIGN KEY(IdPaciente) REFERENCES [Paciente]([Id]),
	CONSTRAINT [FK_Emergencia_Medico] FOREIGN KEY(IdMedico) REFERENCES [Medico]([Id])
)