-- Passo 1: Criação do Banco de Dados (se não existir)
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DatabaseGeanBagatolli')
BEGIN
    CREATE DATABASE [DatabaseGeanBagatolli];
    PRINT 'Banco de dados [DatabaseGeanBagatolli] criado com sucesso.';
END
ELSE
BEGIN
    PRINT 'Banco de dados [DatabaseGeanBagatolli] já existe.';
END
GO

USE [DatabaseGeanBagatolli];
GO

---------------------------------------------------------------------------------
-- Tabela 1: PF (Dados da Pessoa Física) - Tabela Central
---------------------------------------------------------------------------------
PRINT 'Criando tabela [PF]...';
CREATE TABLE PF (
    ID_PF INT IDENTITY(1,1) PRIMARY KEY,

    -- Dados Pessoais
    NomeCompleto NVARCHAR(255) NOT NULL,
    DataNascimento DATE NOT NULL,
    Profissao NVARCHAR(150) NOT NULL,
    SenhaGovBR NVARCHAR(255) NULL, -- Armazenar senhas de forma segura requer técnicas de hash. Por enquanto, um campo de texto.

    -- Filiação
    NomeDoPai NVARCHAR(255) NOT NULL,
    NomeDaMae NVARCHAR(255) NOT NULL,
    
    -- Naturalidade (Conforme nossa decisão, fica na tabela principal)
    Naturalidade_Cidade NVARCHAR(150) NOT NULL,
    Naturalidade_UF CHAR(2) NOT NULL,

    -- Estado Civil (Conforme nossa decisão, fica na tabela principal)
    EstadoCivil_Tipo INT NOT NULL,
    EstadoCivil_NomeConjuge NVARCHAR(255) NULL,
    EstadoCivil_RegimeCasamento INT NULL,
    
    -- Metadados
    DataCadastro DATETIME2 NOT NULL DEFAULT GETDATE(),
    StatusPessoa NVARCHAR(20) NOT NULL DEFAULT 'Ativo'
);
GO

---------------------------------------------------------------------------------
-- Tabela 2: Enderecos (1-para-Muitos)
---------------------------------------------------------------------------------
PRINT 'Criando tabela [Enderecos]...';
CREATE TABLE Enderecos (
    ID_Endereco INT IDENTITY(1,1) PRIMARY KEY,
    Logradouro NVARCHAR(255) NOT NULL,
    Numero NVARCHAR(30) NOT NULL,
    Bairro NVARCHAR(150) NOT NULL,
    Cidade NVARCHAR(150) NOT NULL,
    UF CHAR(2) NOT NULL,
    CEP VARCHAR(9) NOT NULL,
    
    -- Chave Estrangeira
    ID_PF INT NOT NULL,
    CONSTRAINT FK_Enderecos_PF FOREIGN KEY (ID_PF) REFERENCES PF(ID_PF) ON DELETE CASCADE
);
GO

---------------------------------------------------------------------------------
-- Tabela 3: Documentos (1-para-Muitos)
---------------------------------------------------------------------------------
PRINT 'Criando tabela [Documentos]...';
CREATE TABLE Documentos (
    ID_Documento INT IDENTITY(1,1) PRIMARY KEY,
    TipoDocumento NVARCHAR(50) NOT NULL,
    NumeroDocumento NVARCHAR(50) NOT NULL,
    
    -- Chave Estrangeira
    ID_PF INT NOT NULL,
    CONSTRAINT FK_Documentos_PF FOREIGN KEY (ID_PF) REFERENCES PF(ID_PF) ON DELETE CASCADE
);
GO

---------------------------------------------------------------------------------
-- Tabela 4: Contatos (1-para-Muitos)
---------------------------------------------------------------------------------
PRINT 'Criando tabela [Contatos]...';
CREATE TABLE Contatos (
    ID_Contato INT IDENTITY(1,1) PRIMARY KEY,
    TipoContato NVARCHAR(50) NOT NULL,
    ValorContato NVARCHAR(255) NOT NULL,
    
    -- Chave Estrangeira
    ID_PF INT NOT NULL,
    CONSTRAINT FK_Contatos_PF FOREIGN KEY (ID_PF) REFERENCES PF(ID_PF) ON DELETE CASCADE
);
GO

PRINT '=========================================================';
PRINT 'SCRIPT DE SETUP FINALIZADO. Arquitetura do banco de dados está pronta!';
PRINT '=========================================================';
GO