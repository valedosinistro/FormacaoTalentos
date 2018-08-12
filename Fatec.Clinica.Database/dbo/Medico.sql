CREATE TABLE [dbo].[Medico]
(
	[Id] INT NOT NULL IDENTITY, 
	[IdEspecialidade] INT NOT NULL,
    [Nome] VARCHAR(100) NOT NULL, 
	[Cpf] VARCHAR(14) UNIQUE NOT NULL,
    [Crm] VARCHAR(10) UNIQUE NOT NULL,
	[Sexo] Char NOT NULL,
	[Telefone_r] VARCHAR(16) NOT NULL,
	[Telefone_c] VARCHAR(16) NOT NULL,
	[Endereco_c] VARCHAR(100) NOT NULL,
	Ativo_Adm bit not null,
	Email varchar(100) not null UNIQUE,
	Senha varchar(8) not null,
	[Cidade] VARCHAR(50) NOT NULL,
	[Estado] VARCHAR(2) NOT NULL,
	[Ativo] BIT NOT NULL,

	CONSTRAINT [PK_Medico] PRIMARY KEY (Id),
    CONSTRAINT [FK_Medico_Especialidade] FOREIGN KEY (IdEspecialidade) REFERENCES [Especialidade]([Id]),
)