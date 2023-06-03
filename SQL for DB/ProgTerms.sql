USE [ProgTerms]
GO
/****** Object:  Table [dbo].[Terms]    Script Date: 03.06.2023 17:01:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Terms](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Definition] [nvarchar](max) NOT NULL,
	[AddInformation] [nvarchar](max) NULL,
	[Image] [nvarchar](50) NULL,
	[IsSave] [bit] NOT NULL,
 CONSTRAINT [PK_Terms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Terms] ADD  CONSTRAINT [DF_Terms_IsSave]  DEFAULT ((0)) FOR [IsSave]
GO
