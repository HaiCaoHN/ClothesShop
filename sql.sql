USE [master]
GO
/****** Object:  Database [Project_PRN]    Script Date: 11/15/2022 1:38:38 AM ******/
CREATE DATABASE [Project_PRN]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Project_PRN', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.HAICAO\MSSQL\DATA\Project_PRN.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Project_PRN_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.HAICAO\MSSQL\DATA\Project_PRN_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Project_PRN] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Project_PRN].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Project_PRN] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Project_PRN] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Project_PRN] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Project_PRN] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Project_PRN] SET ARITHABORT OFF 
GO
ALTER DATABASE [Project_PRN] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Project_PRN] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Project_PRN] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Project_PRN] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Project_PRN] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Project_PRN] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Project_PRN] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Project_PRN] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Project_PRN] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Project_PRN] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Project_PRN] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Project_PRN] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Project_PRN] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Project_PRN] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Project_PRN] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Project_PRN] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Project_PRN] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Project_PRN] SET RECOVERY FULL 
GO
ALTER DATABASE [Project_PRN] SET  MULTI_USER 
GO
ALTER DATABASE [Project_PRN] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Project_PRN] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Project_PRN] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Project_PRN] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Project_PRN] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Project_PRN] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Project_PRN', N'ON'
GO
ALTER DATABASE [Project_PRN] SET QUERY_STORE = OFF
GO
USE [Project_PRN]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 11/15/2022 1:38:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart_Items]    Script Date: 11/15/2022 1:38:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart_Items](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CartID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Cart_Item] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/15/2022 1:38:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 11/15/2022 1:38:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Total] [int] NOT NULL,
	[PaymentID] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Items]    Script Date: 11/15/2022 1:38:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Items](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
 CONSTRAINT [PK_Order_Items] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 11/15/2022 1:38:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentID] [int] NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/15/2022 1:38:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](50) NULL,
	[CategoryID] [int] NULL,
	[Price] [float] NULL,
	[Desciption] [text] NULL,
	[Image] [varchar](50) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/15/2022 1:38:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[DisplayName] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[Telephone] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cart] ON 

INSERT [dbo].[Cart] ([CartID], [UserID]) VALUES (1, 1)
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (1, N'Sportwear')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (2, N'Men')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (3, N'Women')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (4, N'Kid')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [ProductName], [CategoryID], [Price], [Desciption], [Image]) VALUES (1, N'Camp Flannel Shirt', 2, 34, NULL, N'product7.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [CategoryID], [Price], [Desciption], [Image]) VALUES (2, N'Unisex Baby Cotton ', 4, 16, NULL, N'product8.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [CategoryID], [Price], [Desciption], [Image]) VALUES (3, N'Womens''s EcoSmart Sweatshirt', 3, 9, NULL, N'product12.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [CategoryID], [Price], [Desciption], [Image]) VALUES (4, N'Fleexce Pullover Dhirt', 2, 52, NULL, N'product9.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [CategoryID], [Price], [Desciption], [Image]) VALUES (5, N'Long-Sleeve Shirt', 4, 23, NULL, N'product10.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [CategoryID], [Price], [Desciption], [Image]) VALUES (6, N'Pocket T-Shirt', 2, 14, NULL, N'product11.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [CategoryID], [Price], [Desciption], [Image]) VALUES (7, N'Toodle Girl Jacket', 3, 12, NULL, N'product8.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [CategoryID], [Price], [Desciption], [Image]) VALUES (8, N'Carter''s Baby Boys Bodysuit', 4, 23, NULL, N'product12.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [CategoryID], [Price], [Desciption], [Image]) VALUES (9, N'Techfit Vloeeyball Shorts', 1, 11, NULL, N'product7.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [CategoryID], [Price], [Desciption], [Image]) VALUES (10, N'Sleeves Golf Polo Shirt', 1, 14, NULL, N'product10.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [CategoryID], [Price], [Desciption], [Image]) VALUES (11, N'Armour Fleece T-Shirt', 2, 40, NULL, N'product9.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [CategoryID], [Price], [Desciption], [Image]) VALUES (12, N'Loose Fit Pocket T-Shirt', 3, 13, NULL, N'product7.jpg')
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [UserName], [Password], [DisplayName], [Address], [Telephone]) VALUES (1, N'admin', N'admin', N'Administrator', NULL, NULL)
INSERT [dbo].[User] ([UserID], [UserName], [Password], [DisplayName], [Address], [Telephone]) VALUES (3, N'cus1', N'123', N'Cus1', NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_User]
GO
ALTER TABLE [dbo].[Cart_Items]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Item_Cart] FOREIGN KEY([CartID])
REFERENCES [dbo].[Cart] ([CartID])
GO
ALTER TABLE [dbo].[Cart_Items] CHECK CONSTRAINT [FK_Cart_Item_Cart]
GO
ALTER TABLE [dbo].[Cart_Items]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Item_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Cart_Items] CHECK CONSTRAINT [FK_Cart_Item_Product]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Payment] FOREIGN KEY([PaymentID])
REFERENCES [dbo].[Payment] ([PaymentID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Payment]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[Order_Items]  WITH CHECK ADD  CONSTRAINT [FK_Order_Items_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[Order_Items] CHECK CONSTRAINT [FK_Order_Items_Order]
GO
ALTER TABLE [dbo].[Order_Items]  WITH CHECK ADD  CONSTRAINT [FK_Order_Items_Payment] FOREIGN KEY([Quantity])
REFERENCES [dbo].[Payment] ([PaymentID])
GO
ALTER TABLE [dbo].[Order_Items] CHECK CONSTRAINT [FK_Order_Items_Payment]
GO
ALTER TABLE [dbo].[Order_Items]  WITH CHECK ADD  CONSTRAINT [FK_Order_Items_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Order_Items] CHECK CONSTRAINT [FK_Order_Items_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
USE [master]
GO
ALTER DATABASE [Project_PRN] SET  READ_WRITE 
GO
