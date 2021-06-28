USE [store]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 27/06/2021 18:49:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[orders_id] [bigint] IDENTITY(1,1) NOT NULL,
	[shopping_cart_id] [bigint] NULL,
	[amount] [float] NULL,
	[authorization] [varchar](100) NULL,
	[status] [bit] NULL,
	[creation_date] [datetimeoffset](0) NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
(
	[orders_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[personal_information]    Script Date: 27/06/2021 18:49:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[personal_information](
	[user_id] [bigint] IDENTITY(1,1) NOT NULL,
	[username] [varchar](100) NULL,
	[email] [varchar](100) NULL,
	[shipping_address] [varchar](100) NULL,
	[payment_method] [varchar](100) NULL,
	[card_number] [numeric](16, 0) NULL,
	[creation_date] [datetimeoffset](0) NULL,
 CONSTRAINT [PK_personal_information] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 27/06/2021 18:49:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[product_id] [bigint] IDENTITY(1,1) NOT NULL,
	[product_name] [nchar](10) NULL,
	[price] [float] NULL,
	[discounts] [int] NULL,
	[units_available] [int] NULL,
	[creation_date] [datetimeoffset](0) NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shopping_cart]    Script Date: 27/06/2021 18:49:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shopping_cart](
	[shopping_cart_id] [bigint] NULL,
	[product_id] [bigint] NULL,
	[user_id] [bigint] NULL,
	[units] [bigint] NULL,
	[creation_date] [datetimeoffset](0) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([orders_id], [shopping_cart_id], [amount], [authorization], [status], [creation_date]) VALUES (1, 1, 135000, N'ZO6NAS', 1, CAST(N'2021-06-27T15:51:22.0000000+00:00' AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
SET IDENTITY_INSERT [dbo].[personal_information] ON 

INSERT [dbo].[personal_information] ([user_id], [username], [email], [shipping_address], [payment_method], [card_number], [creation_date]) VALUES (1, N'Juan Camilo', N'juan.jojoa@udea.edu.co', N'KR 38 #12-34', N'VISA', CAST(1234456789012345 AS Numeric(16, 0)), CAST(N'2021-06-27T04:08:24.0000000+00:00' AS DateTimeOffset))
INSERT [dbo].[personal_information] ([user_id], [username], [email], [shipping_address], [payment_method], [card_number], [creation_date]) VALUES (2, N'Daniel Medina', N'danieldemo1999@gmail.com', N'KR 67 #12-45', N'MASTER CARD', CAST(345346434464 AS Numeric(16, 0)), CAST(N'2021-06-27T15:15:40.0000000+00:00' AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[personal_information] OFF
GO
SET IDENTITY_INSERT [dbo].[products] ON 

INSERT [dbo].[products] ([product_id], [product_name], [price], [discounts], [units_available], [creation_date]) VALUES (1, N'Producto A', 10000, 0, 100, CAST(N'2021-06-27T04:07:12.0000000+00:00' AS DateTimeOffset))
INSERT [dbo].[products] ([product_id], [product_name], [price], [discounts], [units_available], [creation_date]) VALUES (2, N'Producto B', 5000, 0, 98, CAST(N'2021-06-27T04:07:21.0000000+00:00' AS DateTimeOffset))
INSERT [dbo].[products] ([product_id], [product_name], [price], [discounts], [units_available], [creation_date]) VALUES (3, N'Producto C', 25000, 0, 95, CAST(N'2021-06-27T04:07:33.0000000+00:00' AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[products] OFF
GO
ALTER TABLE [dbo].[orders] ADD  CONSTRAINT [DF_orders_creation_date]  DEFAULT (getutcdate()) FOR [creation_date]
GO
ALTER TABLE [dbo].[personal_information] ADD  CONSTRAINT [DF_personal_information_creation_date]  DEFAULT (getutcdate()) FOR [creation_date]
GO
ALTER TABLE [dbo].[products] ADD  CONSTRAINT [DF_products_creation_date]  DEFAULT (getutcdate()) FOR [creation_date]
GO
ALTER TABLE [dbo].[shopping_cart] ADD  CONSTRAINT [DF_shopping_cart_creation_date]  DEFAULT (getutcdate()) FOR [creation_date]
GO
