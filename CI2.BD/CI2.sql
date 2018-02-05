USE [master]
GO
/****** Object:  Database [CI2]    Script Date: 04/02/2018 19:14:11 ******/
CREATE DATABASE [CI2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CI2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\CI2.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CI2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\CI2_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CI2] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CI2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CI2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CI2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CI2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CI2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CI2] SET ARITHABORT OFF 
GO
ALTER DATABASE [CI2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CI2] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [CI2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CI2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CI2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CI2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CI2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CI2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CI2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CI2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CI2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CI2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CI2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CI2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CI2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CI2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CI2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CI2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CI2] SET RECOVERY FULL 
GO
ALTER DATABASE [CI2] SET  MULTI_USER 
GO
ALTER DATABASE [CI2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CI2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CI2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CI2] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CI2', N'ON'
GO
USE [CI2]
GO
/****** Object:  User [CI2]    Script Date: 04/02/2018 19:14:12 ******/
CREATE USER [CI2] FOR LOGIN [CI2] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [CI2]
GO
/****** Object:  Schema [CI2]    Script Date: 04/02/2018 19:14:12 ******/
CREATE SCHEMA [CI2]
GO
/****** Object:  Schema [SJI]    Script Date: 04/02/2018 19:14:12 ******/
CREATE SCHEMA [SJI]
GO
/****** Object:  Table [CI2].[__MigrationHistory]    Script Date: 04/02/2018 19:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [CI2].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_CI2.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CI2].[TabRol]    Script Date: 04/02/2018 19:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CI2].[TabRol](
	[IdRol] [nvarchar](128) NOT NULL,
	[NombreRol] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_CI2.TabRol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [CI2].[TabTareaUsuario]    Script Date: 04/02/2018 19:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [CI2].[TabTareaUsuario](
	[IdTarea] [bigint] IDENTITY(1,1) NOT NULL,
	[FechaVencimieno] [datetime2](7) NOT NULL,
	[Descripcion] [varchar](max) NOT NULL,
	[Estado] [bit] NOT NULL,
	[FechaCreacion] [datetime2](7) NOT NULL,
	[FechaActualizacion] [datetime2](7) NOT NULL,
	[IdUsuario] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_TabTareaUsuario] PRIMARY KEY CLUSTERED 
(
	[IdTarea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CI2].[TabUsuario]    Script Date: 04/02/2018 19:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CI2].[TabUsuario](
	[IdUsuario] [nvarchar](128) NOT NULL,
	[Correo] [nvarchar](256) NULL,
	[CorreoConfirmacion] [bit] NOT NULL,
	[Contrasena] [nvarchar](max) NULL,
	[Seguridad] [nvarchar](max) NULL,
	[Telefono] [nvarchar](max) NULL,
	[TelefonoConfirmacion] [bit] NOT NULL,
	[DosFactoresActivacion] [bit] NOT NULL,
	[FechaUltimoBloqueo] [datetime] NULL,
	[Bloqueo] [bit] NOT NULL,
	[NumeroIngresosFallidos] [int] NOT NULL,
	[NombreUsuario] [nvarchar](256) NOT NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_CI2.TabUsuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [CI2].[TabUsuarioLogin]    Script Date: 04/02/2018 19:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CI2].[TabUsuarioLogin](
	[IdLogin] [nvarchar](128) NOT NULL,
	[IdLlaveProveedor] [nvarchar](128) NOT NULL,
	[IdUsuario] [nvarchar](128) NOT NULL,
	[IdentityUser_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_CI2.TabUsuarioLogin] PRIMARY KEY CLUSTERED 
(
	[IdLogin] ASC,
	[IdLlaveProveedor] ASC,
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [CI2].[TabUsuarioReclamo]    Script Date: 04/02/2018 19:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CI2].[TabUsuarioReclamo](
	[IdUsuarioReclamo] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [nvarchar](max) NULL,
	[TipoReclamo] [nvarchar](max) NULL,
	[Reclamo] [nvarchar](max) NULL,
	[IdentityUser_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_CI2.TabUsuarioReclamo] PRIMARY KEY CLUSTERED 
(
	[IdUsuarioReclamo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [CI2].[TabUsuarioRol]    Script Date: 04/02/2018 19:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CI2].[TabUsuarioRol](
	[IdUsuario] [nvarchar](128) NOT NULL,
	[IdRol] [nvarchar](128) NOT NULL,
	[IdentityUser_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_CI2.TabUsuarioRol] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC,
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [CI2].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201802022204014_InitialCreate', N'CI2.Web.Models.ApplicationDbContext', 0x1F8B0800000000000400D55CDD6EE3B815BE2FD0771074B55B64ADC4E90CA681BD8B8C93B44627C9609C6C7B37A025DA2146223D12954D50F4C9F6621FA9AF50527F162952A2643A713040C696C8EF1C9E5FF290F4FF7EFF63F2CB53143A8F304E10C153F76474EC3A10FB2440783D7553BAFAE983FBCBCF7FFED3E432889E9C5FCB76A7BC1DEB8993A9FB40E9E6CCF312FF0146201945C88F49425674E493C80301F1C6C7C77FF34E4E3CC8205C86E538932F29A62882D917F67546B00F373405E13509609814CFD99B4586EADC8008261BE0C3A93B9B8F47FF82CB51DED275CE430418170B18AE5C07604C28A08CC7B3FB042E684CF07AB1610F4078F7BC81ACDD0A84092C783FDB36371DC6F1980FC3DB762CA1FC34A124EA0978725AC8C593BB0F92AE5BC98D49EE9249983EF35167D29BBAF300668FBE909009402678360B63DE78EA5E5724CE93CD0DA4A3B2E32887BC8A19DC6F24FE36AA231E39C6FD8E2A3B1A8F8EF9BF23679686348DE114C394C6203C723EA7CB10F9FF84CF77E41BC4D3D393E5EAF4C3BBF720387DFF5778FAAE3E523656D64E78C01E7D8EC906C68C37B8AAC6EF3A9ED8CF933B56DD6A7D72A9305B622EE13AD7E0E913C46BFAC09C65FCC175AED0130CCA278571DD63C43C8875A271CABEDEA461089621ACDE7BAD34F9DF16AAE377EFAD50BD018F689DA95EA2CF1C27667EF50586D9DBE4016D72F712F4FDB568761593887F17ED2B7FFB7541D2D8E78321DA2677205E432A7237F1B6C66B64D21CCABE5997A8876FDA9CD3A6792B9BF2010DF18492C44B7B43C9EF7EE91A5BDCF966C39497991697489BC189896A24F5648691BFDF9ACC89A9C9603614D7F908125848A5EE9C395B9EB1DFCC428022FB8E93C11EBEE7EC9614E6989E8E15A65513E3829218FE1D6218030A83CF805218E3AD06BACCBFD3EDD84723F36F2793698BD378194ABF8230B54D6A50D2F844D608DB37FE0CF6F08D3F63933D7E44811C33343DCAC60CDEA8BD3A3175BB98C4D94B271D61982F4DFC6532ED2077B1EF2987EF246F65D9701901145A5837185061CBF5158A23588DF22361B33480FBFB194812A6DAE01F2079D87BE659403F8D99412D2888367BA7F6F98160789346CBD6F8659D9635D5DCFD46AE80CF262F9798F7DA19EF13F1BF91945EE2E0824D84EEA95F02F2AF77283207B0C2CEB9EFC324B962C60C83194931ED9AD275C7ECD75EB76733ABF6853B67F36BD9AEB972AFBDD62EDDEB6D546BF7360EB3AC6EC061D94EC361FEBA9DC3A24D5F0E3992018345330D7FD9DB76F6F2267D6A1FE749427C9471A5A8E715D5187194CCD79CEED24C7305995752AE596E457CC9CA9EB0A4E5CA99F2165FC01052E89CFB79BD7306121F044D8365030A7A30560A45B3B45531F797064D96BE61CC3B019EAC123639409836733DC23EDA80B0534A524FC379021F7B45437E7301371073829D923021AEAEEA70062A3A9252BA2434F16A16676688F5E8D2A57065A8D11533446D1F8F46270D85B753E8B0A9A204D26A5443E551C432236EE5C0665F1E5258D4502856C57B91471E3A8D9895E2A87D6988517878C0D1CA220FE5CCBF28F32E18376B88174BFE123E51C5CA8A112F16574931379007C5C117908AB96B9B3E9461BD211911441E791BE0563A3D40CBD9472B6AE1903D60CB29432B6C61D73D60BB101B6035436822D6F74D6A0DF5BB2B723437CAE8D5782A93686405A3045CC35158859CE4C581F7108A3027D54B459B5C8CD38B663C66F25165130D603916BB122A0DBC4342AA74639C70769790945F3480E558EC4AA830CC0E0129F28F6906DA5D3C62C2B1E460E58AA14A33DBD3135E7E7CA23C66E169CE594CAEC166C3D6ABB57317C51367511CBAF869D1FF44429463787EA2389850715B51A224066B28BD65A419A757284EE805A06009F88A7816448D66CAA4AA09F425C97ADE6C6AB00CFB656BFEB958698B3B7B42826DCED68BFE576C74119FEF673B420ADDABBB67676040086245897146C234C2E577D6BF69897A80BC5C5187B821D132861A9889278DA3B1D26848ACB10C14C56FA49CA663585054357119AE2C3D844EDE65595F54DA7D928218913E8A2BD77A46DA3F00B5EDA43279BF7D378DEDE25EED9A62112740D99A27DBDB541C14A8A35DA0C48F51843060CCEE436D1D429E27FCF3EDEA871607F9F16065AD0429B63FEA383312C7B03F48AD84DE442B5E023F3BA3678E2CEE7488B898C62CAD61D0074FDACCA8032EE09ABD0A80A21CD4C25F7DBBA28E760743B822B89718D5FB112AD4A1E26CEE4E08EE4592FC2D4C7871F2B137BC62B3A28E7F05FD07701F5214918F21F99EF633327923A38E3C004EB1932124F534823199E3351306174B18A28028A6AC7A02DBBD8DE664A1C551F71CC25A22D7C106AE4692104B092F9A212C2476ED6A7C90D6F29ACF6EAAD360F4D6DF17E88720EAE586D6A677B5A35842C4449B216CD58F5BD5E15AA00EC0B2B4558C41969597FD76B32C0D863EC60BE79744A3509621DBD084E3481256081E217F0F61A00E14D60C76DF76211638F4C651150B8D945FB5EE1D377811A71130B4E5C5A6B80C434E0D51BD835763631887DA7D444B41B195C56DCE9B27FC684375ACA18714E4E2D76EE653561FCDCCA76CDD3B38742947ACBD1EA2F988C55C430E8745BE3DC43D2B51EF8D9976513A36B3ECA271DF0A5797D50835F343346BA108BF1FB3B150BA3B00CB6B6C2CC84DAA1941B5C1206D244C8AA2BEC1AD4EB9CA9F37719D32064CDDC573426134E20D468BEFE12C44902F76CB06D700A3154C687EF2D71D1F9F8CA5CBA1877351D34B9220EC715B53549CE1B1658559999C5CCEBAE5DBDDF811C4FE03889BC797FB5E69DCEE2CC8C88D630F731CC0A7A9FB9FACEB59B68BC63F658F8F98BDDF63F43D652FEEE2143AFF6D1EB1B47FEA7DB80AF479C5A2C22A229695A631852E85CDFFFD35EB79E4DCC6CC2DCF9C63494BC3789162D920AE040C53FE2CDD34B26A40AF69166555BD0556F243A36B618AEA7A4E6189E8001EB7357591CF1F22F0F4635FE66A15750B68DB8ABA45305B92D394CE8703AA6AE5395A0028A4D9C1FF7E4396608630A52B8AE788080F4014CBE17DC3535963B793E5146A15CBC93B858441E14F5161E9190335E5871EA1B042A8A9B9F37A6EEF44A5B18161DE5D2FFA5AC0B389F5F653B2A26C636A939A4286BA6D47A9C276E22F78B33E1B94C7619DC0FE26B26FC052ADDDEF39942B3DDBD3E1AF7B93C7F0F28EA608D3FBFE4ECBD9B21E640FF8D6CE3E6E6214E7FD77B89EB3ABA568CE0DF45BD40FB316DDC6B78A766759F4A50DA7C7F5A67D188E90EB06DD637AC386A3DBD77E1386637E0F6C1F76F3DA89E915ADA647727A79A391361CAA728E7C0540D671B17BA5AA9D775D88CB371AB2AD326611F9B48DFDA72AC6EAE86C8D464B6BDB4443AF5CA3F624DB793FAD9D5CFFAB7B6637F73A06A959CAB7112F62792BF1A24D3B71F525C0D7B8B7A7BC01A4BA50D911C6DA366FDFC03D3DED008CC52018A6E670CF1BB88EB7BB200427D11C5339F85B77BB8BC1A65BF4B85DD7DCEC6699B0F643C72C2D2768BD85E0DBF818FA420EACDACCF18A943959E2A86C229539AE2105014B90E731452BE053F69A9F0CCF7E87A6386F7C192D6130C7B729DDA4940D1946CB50F821379ED2DBE8675708459E27B79BECA7526C0C81B189F8B6C02DFE98A230A8F8BE5254A135107CAE50D473B92E29AFEBAE9F2BA41B820D810AF155539C3B186D420696DCE2057884437863E6F709AE81FFBC3D1FA503E9568428F6C90502EB18444981B1EDCFBE321B0EA2A79FFF0F481FE68EEF5B0000, N'6.1.3-40302')
INSERT [CI2].[TabRol] ([IdRol], [NombreRol]) VALUES (N'1', N'Administrador')
INSERT [CI2].[TabRol] ([IdRol], [NombreRol]) VALUES (N'2', N'Profesional')
SET IDENTITY_INSERT [CI2].[TabTareaUsuario] ON 

INSERT [CI2].[TabTareaUsuario] ([IdTarea], [FechaVencimieno], [Descripcion], [Estado], [FechaCreacion], [FechaActualizacion], [IdUsuario]) VALUES (1, CAST(N'2017-02-02 00:00:00.0000000' AS DateTime2), N'Descripción tarea', 1, CAST(N'2017-02-02 00:00:00.0000000' AS DateTime2), CAST(N'2017-02-02 00:00:00.0000000' AS DateTime2), N'9be6e696-0596-46e3-8f58-41926228c1ab')
INSERT [CI2].[TabTareaUsuario] ([IdTarea], [FechaVencimieno], [Descripcion], [Estado], [FechaCreacion], [FechaActualizacion], [IdUsuario]) VALUES (2, CAST(N'2018-02-04 12:55:45.5515041' AS DateTime2), N'Ejemplo descripci?n', 0, CAST(N'2018-02-04 12:55:44.6414521' AS DateTime2), CAST(N'2018-02-04 12:55:45.0504755' AS DateTime2), N'9be6e696-0596-46e3-8f58-41926228c1ab')
INSERT [CI2].[TabTareaUsuario] ([IdTarea], [FechaVencimieno], [Descripcion], [Estado], [FechaCreacion], [FechaActualizacion], [IdUsuario]) VALUES (3, CAST(N'2018-02-04 12:57:04.4570172' AS DateTime2), N'Ejemplo descripci?n', 1, CAST(N'2018-02-04 12:57:03.7589773' AS DateTime2), CAST(N'2018-02-04 12:57:04.1630004' AS DateTime2), N'9be6e696-0596-46e3-8f58-41926228c1ab')
INSERT [CI2].[TabTareaUsuario] ([IdTarea], [FechaVencimieno], [Descripcion], [Estado], [FechaCreacion], [FechaActualizacion], [IdUsuario]) VALUES (4, CAST(N'2018-02-04 12:59:28.5812607' AS DateTime2), N'Ejemplo descripci?n', 1, CAST(N'2018-02-04 12:59:28.5812607' AS DateTime2), CAST(N'2018-02-04 12:59:28.5812607' AS DateTime2), N'9be6e696-0596-46e3-8f58-41926228c1ab')
INSERT [CI2].[TabTareaUsuario] ([IdTarea], [FechaVencimieno], [Descripcion], [Estado], [FechaCreacion], [FechaActualizacion], [IdUsuario]) VALUES (5, CAST(N'2018-02-04 13:01:54.1895890' AS DateTime2), N'Ejemplo descripci?n', 1, CAST(N'2018-02-04 13:01:54.1895890' AS DateTime2), CAST(N'2018-02-04 13:01:54.1895890' AS DateTime2), N'9be6e696-0596-46e3-8f58-41926228c1ab')
SET IDENTITY_INSERT [CI2].[TabTareaUsuario] OFF
INSERT [CI2].[TabUsuario] ([IdUsuario], [Correo], [CorreoConfirmacion], [Contrasena], [Seguridad], [Telefono], [TelefonoConfirmacion], [DosFactoresActivacion], [FechaUltimoBloqueo], [Bloqueo], [NumeroIngresosFallidos], [NombreUsuario], [Discriminator]) VALUES (N'8bfa8fca-11f5-4569-9ef1-b9d5ea5f89b9', N'1@gmail.com', 0, N'ABJMXzJcwA9YQypxsFDIGcQ43u2q5PEuu/5m+qkO9g2zA7UMmdjSYNbLLBmmmdDiZw==', N'689b0674-c542-405a-ab17-4afddde7b09a', N'34343434', 0, 0, NULL, 1, 0, N'1@gmail.com', N'ApplicationUser')
INSERT [CI2].[TabUsuario] ([IdUsuario], [Correo], [CorreoConfirmacion], [Contrasena], [Seguridad], [Telefono], [TelefonoConfirmacion], [DosFactoresActivacion], [FechaUltimoBloqueo], [Bloqueo], [NumeroIngresosFallidos], [NombreUsuario], [Discriminator]) VALUES (N'9be6e696-0596-46e3-8f58-41926228c1ab', N'manuelmorenotarazona91@gmail.com', 0, N'ABJMXzJcwA9YQypxsFDIGcQ43u2q5PEuu/5m+qkO9g2zA7UMmdjSYNbLLBmmmdDiZw==', N'689b0674-c542-405a-ab17-4afddde7b09a', N'345678', 0, 0, NULL, 1, 0, N'manuelmorenotarazona91@gmail.com', N'ApplicationUser')
INSERT [CI2].[TabUsuario] ([IdUsuario], [Correo], [CorreoConfirmacion], [Contrasena], [Seguridad], [Telefono], [TelefonoConfirmacion], [DosFactoresActivacion], [FechaUltimoBloqueo], [Bloqueo], [NumeroIngresosFallidos], [NombreUsuario], [Discriminator]) VALUES (N'9e577ae1-902f-4644-993b-2d5f9624fbed', N'2@gmail.com', 0, N'ADzTJKzxSxD0xd0OAQbSG+8I3PrE2FiZFxX9/FXMwjCPxmi4Qfml96fmJFEgEuT8Rg==', N'63eb0fd9-118c-4328-aa50-3cc056935903', N'232432', 0, 0, NULL, 1, 0, N'2@gmail.com', N'ApplicationUser')
INSERT [CI2].[TabUsuario] ([IdUsuario], [Correo], [CorreoConfirmacion], [Contrasena], [Seguridad], [Telefono], [TelefonoConfirmacion], [DosFactoresActivacion], [FechaUltimoBloqueo], [Bloqueo], [NumeroIngresosFallidos], [NombreUsuario], [Discriminator]) VALUES (N'f797fca6-bde3-4dc4-8280-4796a67765e8', N'3@gmail.com', 0, N'AAAa59QFKqjvIHPP6eQUi8NudNy4iHBwlDumsRUMqJi29GcxTJWrjEGEEY2l8BjnVQ==', N'd8746cac-2a4f-45d5-b19d-e30aeb591910', N'4567', 0, 0, NULL, 1, 0, N'3@gmail.com', N'ApplicationUser')
INSERT [CI2].[TabUsuarioRol] ([IdUsuario], [IdRol], [IdentityUser_Id]) VALUES (N'8bfa8fca-11f5-4569-9ef1-b9d5ea5f89b9', N'1', N'8bfa8fca-11f5-4569-9ef1-b9d5ea5f89b9')
INSERT [CI2].[TabUsuarioRol] ([IdUsuario], [IdRol], [IdentityUser_Id]) VALUES (N'9be6e696-0596-46e3-8f58-41926228c1ab', N'1', N'9be6e696-0596-46e3-8f58-41926228c1ab')
INSERT [CI2].[TabUsuarioRol] ([IdUsuario], [IdRol], [IdentityUser_Id]) VALUES (N'9e577ae1-902f-4644-993b-2d5f9624fbed', N'1', N'9e577ae1-902f-4644-993b-2d5f9624fbed')
INSERT [CI2].[TabUsuarioRol] ([IdUsuario], [IdRol], [IdentityUser_Id]) VALUES (N'f797fca6-bde3-4dc4-8280-4796a67765e8', N'1', N'f797fca6-bde3-4dc4-8280-4796a67765e8')
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 04/02/2018 19:14:13 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [CI2].[TabRol]
(
	[NombreRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 04/02/2018 19:14:13 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [CI2].[TabUsuario]
(
	[NombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUser_Id]    Script Date: 04/02/2018 19:14:13 ******/
CREATE NONCLUSTERED INDEX [IX_IdentityUser_Id] ON [CI2].[TabUsuarioLogin]
(
	[IdentityUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUser_Id]    Script Date: 04/02/2018 19:14:13 ******/
CREATE NONCLUSTERED INDEX [IX_IdentityUser_Id] ON [CI2].[TabUsuarioReclamo]
(
	[IdentityUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUser_Id]    Script Date: 04/02/2018 19:14:13 ******/
CREATE NONCLUSTERED INDEX [IX_IdentityUser_Id] ON [CI2].[TabUsuarioRol]
(
	[IdentityUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdRol]    Script Date: 04/02/2018 19:14:13 ******/
CREATE NONCLUSTERED INDEX [IX_IdRol] ON [CI2].[TabUsuarioRol]
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [CI2].[TabTareaUsuario]  WITH CHECK ADD  CONSTRAINT [FK_TabTareaUsuario_TabUsuario] FOREIGN KEY([IdUsuario])
REFERENCES [CI2].[TabUsuario] ([IdUsuario])
GO
ALTER TABLE [CI2].[TabTareaUsuario] CHECK CONSTRAINT [FK_TabTareaUsuario_TabUsuario]
GO
ALTER TABLE [CI2].[TabUsuarioLogin]  WITH CHECK ADD  CONSTRAINT [FK_CI2.TabUsuarioLogin_CI2.TabUsuario_IdentityUser_Id] FOREIGN KEY([IdentityUser_Id])
REFERENCES [CI2].[TabUsuario] ([IdUsuario])
GO
ALTER TABLE [CI2].[TabUsuarioLogin] CHECK CONSTRAINT [FK_CI2.TabUsuarioLogin_CI2.TabUsuario_IdentityUser_Id]
GO
ALTER TABLE [CI2].[TabUsuarioReclamo]  WITH CHECK ADD  CONSTRAINT [FK_CI2.TabUsuarioReclamo_CI2.TabUsuario_IdentityUser_Id] FOREIGN KEY([IdentityUser_Id])
REFERENCES [CI2].[TabUsuario] ([IdUsuario])
GO
ALTER TABLE [CI2].[TabUsuarioReclamo] CHECK CONSTRAINT [FK_CI2.TabUsuarioReclamo_CI2.TabUsuario_IdentityUser_Id]
GO
ALTER TABLE [CI2].[TabUsuarioRol]  WITH CHECK ADD  CONSTRAINT [FK_CI2.TabUsuarioRol_CI2.TabRol_IdRol] FOREIGN KEY([IdRol])
REFERENCES [CI2].[TabRol] ([IdRol])
ON DELETE CASCADE
GO
ALTER TABLE [CI2].[TabUsuarioRol] CHECK CONSTRAINT [FK_CI2.TabUsuarioRol_CI2.TabRol_IdRol]
GO
ALTER TABLE [CI2].[TabUsuarioRol]  WITH CHECK ADD  CONSTRAINT [FK_CI2.TabUsuarioRol_CI2.TabUsuario_IdentityUser_Id] FOREIGN KEY([IdentityUser_Id])
REFERENCES [CI2].[TabUsuario] ([IdUsuario])
GO
ALTER TABLE [CI2].[TabUsuarioRol] CHECK CONSTRAINT [FK_CI2.TabUsuarioRol_CI2.TabUsuario_IdentityUser_Id]
GO
USE [master]
GO
ALTER DATABASE [CI2] SET  READ_WRITE 
GO
