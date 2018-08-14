create table Paciente (
Id int not null identity,
Nome varchar(100) not null,
[Cpf] VARCHAR(14) UNIQUE NOT NULL,
Telefone varchar(16) not null,
Sexo char not null,
Data_Nasc Date not null,
Ativo bit not null,
Ativo_Adm bit not null,
Email varchar(100) not null UNIQUE,
Senha varchar(8) not null,
Constraint [PK_Paciente] Primary Key (Id),
)
