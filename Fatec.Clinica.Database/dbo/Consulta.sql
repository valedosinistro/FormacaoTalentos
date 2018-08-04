CREATE TABLE [dbo].[Consulta]
(
	[Id] INT NOT NULL IDENTITY,
	[IdPaciente] INT NOT NULL,
	[IdMedico] INT NOT NULL,
	[DataConsulta] DATE NOT NULL,
	[Horario] TIME(0) NOT NULL,
	[Status] Char not null,

	CONSTRAINT [PK_Consulta] PRIMARY KEY (Id),
	CONSTRAINT [FK_Consulta_Paciente] FOREIGN KEY ([IdPaciente]) REFERENCES [Paciente]([Id]),
	CONSTRAINT [FK_Consulta_Medico] FOREIGN KEY ([IdMedico]) REFERENCES [Medico]([Id])
)