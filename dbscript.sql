USE [InventoryDB]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 09-04-2021 11:05:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[PID] [int] IDENTITY(1,1) NOT NULL,
	[PName] [varchar](60) NOT NULL,
	[PDescription] [varchar](150) NOT NULL,
	[PQty] [int] NOT NULL,
	[PPrice] [decimal](18, 2) NULL,
	[DateAdded] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([PID], [PName], [PDescription], [PQty], [PPrice], [DateAdded], [DateUpdated]) VALUES (1, N'Product1', N'Product Desc1', 10, CAST(100.34 AS Decimal(18, 2)), CAST(N'2021-04-09T10:38:10.180' AS DateTime), CAST(N'2021-04-09T10:38:10.180' AS DateTime))
INSERT [dbo].[Product] ([PID], [PName], [PDescription], [PQty], [PPrice], [DateAdded], [DateUpdated]) VALUES (2, N'Product2', N'Product Desc2', 45, CAST(540.54 AS Decimal(18, 2)), CAST(N'2021-04-09T10:38:41.653' AS DateTime), CAST(N'2021-04-09T10:38:41.653' AS DateTime))
INSERT [dbo].[Product] ([PID], [PName], [PDescription], [PQty], [PPrice], [DateAdded], [DateUpdated]) VALUES (3, N'Product 3', N'Product Desc3', 56, CAST(343.54 AS Decimal(18, 2)), CAST(N'2021-04-09T10:39:03.877' AS DateTime), CAST(N'2021-04-09T10:39:03.877' AS DateTime))
INSERT [dbo].[Product] ([PID], [PName], [PDescription], [PQty], [PPrice], [DateAdded], [DateUpdated]) VALUES (4, N'Product4', N'Product Desc4', 78, CAST(34.21 AS Decimal(18, 2)), CAST(N'2021-04-09T10:39:29.157' AS DateTime), CAST(N'2021-04-09T10:39:29.157' AS DateTime))
INSERT [dbo].[Product] ([PID], [PName], [PDescription], [PQty], [PPrice], [DateAdded], [DateUpdated]) VALUES (5, N'Product5', N'Product Desc5', 50, CAST(594.23 AS Decimal(18, 2)), CAST(N'2021-04-09T10:39:59.390' AS DateTime), CAST(N'2021-04-09T10:39:59.390' AS DateTime))
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ('') FOR [PName]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ('') FOR [PDescription]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [PQty]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [PPrice]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_PRODUCT]    Script Date: 09-04-2021 11:05:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[SP_CREATE_PRODUCT]
(
  @PName varchar(60),
  @PDescription varchar(150),
  @PQty int,
  @PPrice decimal(18,2)
)
as
begin
set nocount on;
begin try
begin transaction
insert into [dbo].[Product](PName,PDescription,PQty,PPrice,DateAdded,DateUpdated) 
values(@PName,@PDescription,@PQty,@PPrice,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP)
select 1
commit transaction
end try
begin catch
 declare @ErrorMessage varchar(4000),@ErrorSeverity int, @ErrorState int;
 select @ErrorMessage= ERROR_MESSAGE(), @ErrorSeverity=ERROR_SEVERITY(), @ErrorSeverity=ERROR_STATE();
 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorSeverity);
 rollback transaction
end catch
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_PRODUCT]    Script Date: 09-04-2021 11:05:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[SP_DELETE_PRODUCT]
(
 @PID int
)
as 
begin 
set nocount on;

begin try
begin transaction 
delete [dbo].[Product]
where PID=@PID
select 1
commit transaction
end try
begin catch
declare @ErrorMessage varchar(4000), @ErrorSeverity int, @ErrorState int;
select @ErrorMessage=ERROR_MESSAGE(),@ErrorSeverity=ERROR_SEVERITY(),@ErrorState=ERROR_STATE();
raiserror(@ErrorMessage,@ErrorSeverity,@ErrorState);
rollback transaction
end catch
end
GO
/****** Object:  StoredProcedure [dbo].[SP_READ_PRODUCT]    Script Date: 09-04-2021 11:05:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[SP_READ_PRODUCT]
@PageNo INT,
@RowCountPerPage INT,
@IsPaging INT
As
Begin
set nocount on;

if(@IsPaging=0)
begin
select top (@RowCountPerPage) * from [dbo].[Product] order by PID desc
end

if(@IsPaging=1)
begin
declare @SkipRow INT
set @SkipRow=(@PageNo-1) * @RowCountPerPage

select * from [dbo].[Product] order by PID desc

offset @SkipRow ROWS FETCH NEXT @RowCountPerPage rows only
end
end
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_PRODUCT]    Script Date: 09-04-2021 11:05:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[SP_UPDATE_PRODUCT]
(
  @PID int,
  @PName varchar(60),
  @PDescription varchar(150),
  @PQty int,
  @PPrice decimal(18,2)
)
as 
begin
set nocount on;
begin try
begin transaction
update [dbo].[Product] 
set PName=@PName,
PDescription=@PDescription,
PPrice=@PPrice,
PQty=@PQty,
DateUpdated=CURRENT_TIMESTAMP
where PID=@PID
select 1
commit transaction
end try
begin catch
declare @ErrorMessage varchar(4000), @ErrorSeverity int, @ErrorState int;
select @ErrorMessage=ERROR_MESSAGE(),@ErrorSeverity=ERROR_SEVERITY(),@ErrorState=ERROR_STATE();
raiserror(@ErrorMessage,@ErrorSeverity,@ErrorState);
rollback transaction
end catch
end
GO
/****** Object:  StoredProcedure [dbo].[SP_VIEW_PRODUCT]    Script Date: 09-04-2021 11:05:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[SP_VIEW_PRODUCT]
(
@PID int
)
as
begin
set nocount on;

select * from [dbo].[Product] where PID=@PID

end
GO
