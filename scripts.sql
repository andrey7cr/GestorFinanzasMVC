USE [GestorFinanzasDB ]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 3/24/2025 7:08:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Nombre] [nvarchar](100) NOT NULL,
	[CategoriaId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[CategoriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gastos]    Script Date: 3/24/2025 7:08:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gastos](
	[GastoId] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
	[Monto] [decimal](18, 2) NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[CategoriaId] [int] NOT NULL,
	[UsuarioId] [int] NOT NULL,
 CONSTRAINT [PK_Gastos] PRIMARY KEY CLUSTERED 
(
	[GastoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingresos]    Script Date: 3/24/2025 7:08:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingresos](
	[IngresoId] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
	[Monto] [decimal](18, 2) NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[CategoriaId] [int] NOT NULL,
	[UsuarioId] [int] NULL,
 CONSTRAINT [PK_Ingresos] PRIMARY KEY CLUSTERED 
(
	[IngresoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MetasFinancieras]    Script Date: 3/24/2025 7:08:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MetasFinancieras](
	[MetaFinancieraId] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[CategoriaId] [int] NOT NULL,
	[MontoObjetivo] [decimal](18, 2) NOT NULL,
	[Tipo] [int] NOT NULL,
	[Alcanzada] [bit] NOT NULL,
	[FechaRegistro] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_MetasFinancieras] PRIMARY KEY CLUSTERED 
(
	[MetaFinancieraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 3/24/2025 7:08:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[FechaRegistro] [datetime2](7) NULL,
	[FechaExpiracionToken] [datetime2](7) NULL,
	[TokenRecuperacion] [nvarchar](max) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Gastos] ADD  DEFAULT ((0)) FOR [UsuarioId]
GO
ALTER TABLE [dbo].[Gastos]  WITH CHECK ADD  CONSTRAINT [FK_Gastos_Categorias_CategoriaId] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([CategoriaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Gastos] CHECK CONSTRAINT [FK_Gastos_Categorias_CategoriaId]
GO
ALTER TABLE [dbo].[Gastos]  WITH CHECK ADD  CONSTRAINT [FK_Gastos_Usuarios_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Gastos] CHECK CONSTRAINT [FK_Gastos_Usuarios_UsuarioId]
GO
ALTER TABLE [dbo].[Ingresos]  WITH CHECK ADD  CONSTRAINT [FK_Ingresos_Categorias_CategoriaId] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([CategoriaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ingresos] CHECK CONSTRAINT [FK_Ingresos_Categorias_CategoriaId]
GO
ALTER TABLE [dbo].[Ingresos]  WITH CHECK ADD  CONSTRAINT [FK_Ingresos_Usuarios_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
ALTER TABLE [dbo].[Ingresos] CHECK CONSTRAINT [FK_Ingresos_Usuarios_UsuarioId]
GO
ALTER TABLE [dbo].[MetasFinancieras]  WITH CHECK ADD  CONSTRAINT [FK_MetasFinancieras_Categorias_CategoriaId] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([CategoriaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MetasFinancieras] CHECK CONSTRAINT [FK_MetasFinancieras_Categorias_CategoriaId]
GO
ALTER TABLE [dbo].[MetasFinancieras]  WITH CHECK ADD  CONSTRAINT [FK_MetasFinancieras_Usuarios_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MetasFinancieras] CHECK CONSTRAINT [FK_MetasFinancieras_Usuarios_UsuarioId]
GO
