USE [AuthorizationDb]
GO
/****** Object:  UserDefinedFunction [dbo].[TicksToDateTime2]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[TicksToDateTime2] ( @Ticks bigint )
  RETURNS datetime2
AS
BEGIN
    DECLARE @DateTime datetime2 = '00010101';
    SET @DateTime = DATEADD( DAY, @Ticks / 864000000000, @DateTime );
    SET @DateTime = DATEADD( SECOND, ( @Ticks % 864000000000) / 10000000, @DateTime );
    RETURN DATEADD( NANOSECOND, ( @Ticks % 10000000 ) * 100, @DateTime );
END
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](60) NOT NULL,
	[RoleDescription] [nvarchar](60) NULL,
	[Active] [bit] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[HashType] [nvarchar](50) NULL,
	[PasswordHash] [nvarchar](200) NULL,
	[PasswordSalt] [nvarchar](50) NULL,
	[Status] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAuthToken]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuthToken](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [varchar](1000) NULL,
	[UserId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ExpiryDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAuthTokenArchive]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuthTokenArchive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](1000) NULL,
	[UserId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ExpiryDate] [datetime] NULL,
	[ArchiveDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserModule]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserModule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserModuleAccess]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserModuleAccess](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[AccessLevel] [tinyint] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [tinyint] NULL,
	[ModuleId] [int] NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[RoleId] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[Status] [tinyint] NOT NULL,
	[CreatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [RoleName], [RoleDescription], [Active], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [Status]) VALUES (1, N'SUPER ADMIN', N'SUPER ADMIN', 1, 1, CAST(N'2022-06-10T19:58:00.750' AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [Name], [Email], [HashType], [PasswordHash], [PasswordSalt], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [ModifiedBy]) VALUES (1, N'Super Admin', N'abc@gmail.com', N'SHA2_256', N'6874A25B87464E826463A95D1B8C5F08928C1ED374103A664B79E637833034EA', N'F3FDD4C3-99BE-44F1-8551-658AE382EEFA', 1, 1, 1, CAST(N'2022-05-08T09:42:04.220' AS DateTime), CAST(N'2022-06-10T22:32:37.873' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserAuthTokenArchive] ON 

INSERT [dbo].[UserAuthTokenArchive] ([Id], [Token], [UserId], [CreatedDate], [ExpiryDate], [ArchiveDate]) VALUES (1, N'366CB191-131A-4625-9468-C964D366A252', 1, CAST(N'2022-06-10T20:45:00.230' AS DateTime), CAST(N'2022-06-10T21:34:40.337' AS DateTime), CAST(N'2022-06-10T21:04:55.040' AS DateTime))
INSERT [dbo].[UserAuthTokenArchive] ([Id], [Token], [UserId], [CreatedDate], [ExpiryDate], [ArchiveDate]) VALUES (2, N'E749A9C0-E112-4CA7-AF58-830FDFA55126', 1, CAST(N'2022-06-10T22:24:54.160' AS DateTime), CAST(N'2022-06-10T23:02:37.847' AS DateTime), CAST(N'2022-06-10T22:32:37.873' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserAuthTokenArchive] OFF
GO
SET IDENTITY_INSERT [dbo].[UserModule] ON 

INSERT [dbo].[UserModule] ([Id], [ModuleName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (1, N'User Management', 1, CAST(N'2022-06-10T18:57:16.447' AS DateTime), 1)
INSERT [dbo].[UserModule] ([Id], [ModuleName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (2, N'Dashboard Management', 1, CAST(N'2022-06-11T03:56:51.587' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[UserModule] OFF
GO
SET IDENTITY_INSERT [dbo].[UserModuleAccess] ON 

INSERT [dbo].[UserModuleAccess] ([Id], [RoleId], [AccessLevel], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [Status], [ModuleId], [IsActive]) VALUES (1, 1, 15, 1, CAST(N'2022-06-10T19:59:48.640' AS DateTime), NULL, NULL, 1, 1, 1)
INSERT [dbo].[UserModuleAccess] ([Id], [RoleId], [AccessLevel], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [Status], [ModuleId], [IsActive]) VALUES (2, 1, 15, 1, CAST(N'2022-06-10T22:27:14.387' AS DateTime), NULL, NULL, 1, 2, 1)
SET IDENTITY_INSERT [dbo].[UserModuleAccess] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId], [IsActive], [CreatedDate], [ModifiedDate], [ModifiedBy], [Status], [CreatedBy]) VALUES (1, 1, 1, 1, CAST(N'2022-06-10T19:58:59.247' AS DateTime), NULL, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[UserModule] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserModule] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserModuleAccess] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserRoles] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[UserModuleAccess]  WITH CHECK ADD FOREIGN KEY([ModuleId])
REFERENCES [dbo].[UserModule] ([Id])
GO
ALTER TABLE [dbo].[UserModuleAccess]  WITH CHECK ADD FOREIGN KEY([ModuleId])
REFERENCES [dbo].[UserModule] ([Id])
GO
ALTER TABLE [dbo].[UserModuleAccess]  WITH CHECK ADD FOREIGN KEY([ModuleId])
REFERENCES [dbo].[UserModule] ([Id])
GO
ALTER TABLE [dbo].[UserModuleAccess]  WITH CHECK ADD FOREIGN KEY([ModuleId])
REFERENCES [dbo].[UserModule] ([Id])
GO
ALTER TABLE [dbo].[UserModuleAccess]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UserModuleAccess]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
/****** Object:  StoredProcedure [dbo].[USP_User_DeleteUserByUserid]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_User_DeleteUserByUserid]              
(              
@UserId int,              
@ModifiedBy int              
)              
AS              
	BEGIN          
		DECLARE @errorNo INT= 0, @message NVARCHAR(500)='Success';         
		BEGIN TRANSACTION        
			BEGIN TRY         
				UPDATE [dbo].[User] set              
				IsActive=0,              
				ModifiedBy = @ModifiedBy,              
				ModifiedDate = GETUTCDATE()              
				where UserId=@UserId          
            
				--archive the token            
				DELETE              
				FROM [dbo].[UserAuthToken]              
				OUTPUT deleted.Token, deleted.UserId, deleted.CreatedDate, deleted.ExpiryDate, GETUTCDATE()              
				INTO dbo.[UserAuthTokenArchive] (Token, UserId, CreatedDate, ExpiryDate, ArchiveDate)              
				WHERE UserId = @userId;              
				
				COMMIT TRANSACTION;                               
			END TRY                
			BEGIN CATCH              
				ROLLBACK TRANSACTION;          
				SET @errorNo = ERROR_NUMBER()                
				IF @errorNo=547--Foreign Key violation                
				BEGIN SET @message =  'Please enter correct values.' END                
				ELSE THROW;              
			END CATCH                 
	  SELECT ISNULL(@errorNo, 0) AS [ErrorNo],                             
	  @message AS [Message];                 
	END 
GO
/****** Object:  StoredProcedure [dbo].[USP_User_GetAllUsersWithPagintion]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_User_GetAllUsersWithPagintion]                    
(                                            
 @Page_Index_int         INT    = 1 ,                      
 @Page_Size_int         SMALLINT  = 10                     
)                    
As                    
Begin                    
                    
	DECLARE @OFFSET INT;
	SET @OFFSET = (@Page_Index_int - 1) * @Page_Size_int;

	Declare @_count int = 0;
	SELECT  @_count = COUNT (DISTINCT u.UserId) from [User] u 
	INNER JOIN [UserRoles] aur WITH(NOLOCK) ON aur.UserId = u.UserId AND aur.IsActive=1    
	LEFT JOIN [Roles] ar WITH(NOLOCK) ON ar.RoleId=aur.RoleId AND ar.Active=1                 
	WHERE u.IsActive=1 AND u.[Status]=1 

	SELECT                    
	ROW_NUMBER() OVER( ORDER BY u.UserId ) [RowNum],                    
	u.UserId,               
	u.[Name],                    
	aur.RoleId,                    
	ar.RoleName,                    
	u.Email,                                             
	u.CreatedDate,                    
	u.IsActive,                    
	u.[Status],                    
	u.CreatedBy,                    
	@_count as [Count]                    
	FROM [dbo].[User] u                     
	INNER JOIN [UserRoles] aur WITH(NOLOCK) ON aur.UserId = u.UserId AND aur.IsActive=1                   
	LEFT JOIN [Roles] ar WITH(NOLOCK) ON ar.RoleId=aur.RoleId AND ar.Active=1                   
	WHERE u.IsActive=1 AND u.[Status]=1 
	ORDER BY  u.UserId                      
	OFFSET @OFFSET                
	ROWS FETCH NEXT @Page_Size_int  ROWS ONLY;                      
                   
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_User_GetUserByEmailAndPass]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_User_GetUserByEmailAndPass]              
(              
@email NVARCHAR(100),            
@password NVARCHAR(100)            
)              
AS            
BEGIN  
	DECLARE @UserId INT = 0;
	SELECT @UserId = UserId FROM [User] ou
	WHERE ou.IsActive=1          
	AND ou.email = @email            
	AND CONVERT(VARCHAR(200),HASHBYTES(ou.HashType, @password + ou.PasswordSalt), 2) = ou.PasswordHash 

	IF(ISNULL(@UserId,0)!=0)
		BEGIN
			EXECUTE [dbo].[USP_User_GetUserByUserId] @UserId
		END            
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_User_GetUserByUserId]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_User_GetUserByUserId]                        
(                        
@UserId INT                        
)                        
AS                        
 BEGIN                        
  SELECT                      
  --USER                      
  u.UserId,                      
  u.[Name],                       
  u.Email,              
  u.IsActive,         
  u.[Status],                                     
  u.CreatedBy,                      
  u.CreatedDate,
 --ADMIN USER ROLE                      
  aur.RoleId,                        
  aur.IsActive AS [IsActiveUserRole],                      
  aur.[Status] AS [StatusUserRole],                      
  --ADMIN ROLE                      
  ar.RoleName,                      
  ar.RoleDescription,                      
  ar.Active AS [IsActiveRole],                      
  ar.[Status] AS [StatusRole],          
  --ADMIN MODULE ACCESS                      
  ama.Id AS [UserModuleAccessId],                      
  ama.ModuleId,                      
  ama.AccessLevel,                      
  ama.[Status] AS [AccessStatus],                      
  ama.IsActive AS [AccessIsActive],                      
  --MODULE                      
  mm.ModuleName
  FROM [dbo].[User] u                         
  INNER JOIN [UserRoles] aur WITH(NOLOCK) ON aur.UserId = u.UserId AND aur.IsActive=1                        
  LEFT JOIN [Roles] ar WITH(NOLOCK) ON ar.RoleId=aur.RoleId AND ar.Active=1        
  LEFT JOIN [UserModuleAccess] ama WITH(NOLOCK) ON aur.RoleId = ama.RoleId  AND ama.IsActive=1                    
  LEFT JOIN [UserModule] mm WITH(NOLOCK) ON mm.Id = ama.ModuleId AND mm.IsActive=1      
  WHERE u.IsActive=1              
  AND u.UserId=@UserId                        
 END; 
GO
/****** Object:  StoredProcedure [dbo].[USP_User_InsertAuthToken]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_User_InsertAuthToken]              
(@UserId  INT,                    
@Ticks BIGINT=0        
)              
AS              
	BEGIN      
		DECLARE @errorNo INT= 0, @message NVARCHAR(500)='Success';    
		BEGIN TRANSACTION    
			BEGIN TRY     
				DECLARE @ExpiryDate DATETIME2;        
				IF (@ticks>0)        
					BEGIN      
						EXECUTE @ExpiryDate = dbo.TicksToDateTime2 @ticks;        
					END    
				ELSE    
					SET @ExpiryDate = DATEADD(minute, 30, GETDATE())        
       
				DECLARE @token NVARCHAR(200) = NEWID();
				
				IF EXISTS(SELECT TOP 1 1 FROM UserAuthToken WHERE UserId = @userId)        
					BEGIN        
						UPDATE dbo.[UserAuthToken] SET         
						Token = @token,        
						UserId = @userId,        
						ModifiedDate = GETUTCDATE(),        
						ExpiryDate = @ExpiryDate        
						WHERE UserId = @userId        
					END        
				ELSE        
					BEGIN        
						INSERT INTO dbo.[UserAuthToken]              
						(Token,               
						UserId,               
						CreatedDate,              
						ExpiryDate              
						)              
						VALUES              
						(@token,               
						@userId,               
						GETUTCDATE(),               
						@ExpiryDate              
						);            
					END
				COMMIT TRANSACTION;       
			END TRY    
			BEGIN CATCH    
				ROLLBACK TRANSACTION;    
				THROW;    
			END CATCH     
		SELECT ISNULL(@errorNo, 0) AS [ErrorNo],                 
		@message AS [Message],
		@token AS [Identity];       
	END 
GO
/****** Object:  StoredProcedure [dbo].[USP_User_Logout]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_User_Logout]
(@token VARCHAR(1000)
)
AS
BEGIN
	DECLARE @errorNo INT= 0, @message NVARCHAR(500)='Success';
    -- After logout, we should archive the token
	BEGIN TRANSACTION
		BEGIN TRY 
			DELETE
			FROM [dbo].[UserAuthToken]
			OUTPUT deleted.Token, deleted.UserId, deleted.CreatedDate, deleted.ExpiryDate, GETUTCDATE()
				INTO dbo.[UserAuthTokenArchive] (Token, UserId, CreatedDate, ExpiryDate, ArchiveDate)
			WHERE Token = @token;

			COMMIT TRANSACTION;			
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
			THROW;
		END CATCH	
	SELECT ISNULL(@errorNo, 0) AS [ErrorNo],             
    @message AS [Message];   
END
GO
/****** Object:  StoredProcedure [dbo].[USP_User_VerifyAuthToken]    Script Date: 11-06-2022 04:04:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_User_VerifyAuthToken]      
(    
@token varchar(2000),      
@minutes int = 30,    
@ModuleID INT,    
@AccessLevelID INT)    
AS      
	BEGIN    
		DECLARE @errorNo INT= 0;    
		DECLARE @message NVARCHAR(500);    
		DECLARE @userId INT;    
		DECLARE @expiry DATETIME;    
		DECLARE @status INT;    
		DECLARE @isActive BIT;      
		DECLARE @STATUS_ACTIVE int = 1;    
		
		BEGIN TRANSACTION    
			BEGIN TRY     
				SELECT @userId = ot.[UserId]      
				, @expiry = ot.ExpiryDate      
				, @status = ou.[Status]    
				, @isactive = ou.IsActive    
				FROM [dbo].[UserAuthToken] ot WITH (NOLOCK)      
				INNER JOIN [User] ou on ot.UserId = ou.UserId      
				WHERE ot.Token = @token AND ou.IsActive=1    
      
				IF @userId is null      
					BEGIN      
						SET @errorNo = 5000;--not an authorized user    
						SET @message = 'You not an authorized user'     
					END    
				ELSE    
					BEGIN    
					-- If the record found and expiry passed, move the record to archive      
					IF @STATUS_ACTIVE != @status OR @expiry <= GETUTCDATE()  OR @isActive = 0    
						BEGIN      
							DELETE      
							FROM [dbo].[UserAuthToken]      
							OUTPUT deleted.Token, deleted.UserId, deleted.CreatedDate, deleted.ExpiryDate, GETUTCDATE()      
							INTO dbo.[UserAuthTokenArchive] (Token, UserId, CreatedDate, ExpiryDate, ArchiveDate)      
							WHERE Token = @token;     
            
							SET @errorNo = 5001    
							SET @message = 'Access token has expired.'     
						END    
						
						-- If record found and not expired yet, sliding the expiry for next 30 minutes      
					ELSE IF @expiry > GETUTCDATE()      
						BEGIN      
							IF NOT EXISTS((SELECT TOP 1 1 FROM UserModuleAccess OMA     
							JOIN UserRoles OUR ON OUR.roleId = OMA.roleId AND OUR.userid = @userId    
							WHERE OMA.Status = 1 AND OMA.IsActive = 1 AND OMA.moduleid = @ModuleID 
							AND OMA.accesslevel = AccessLevel | @AccessLevelID))    
								BEGIN    
									SET @errorNo = 403    
									SET @message = 'Permission denied'    
								END    
							ELSE    
								BEGIN    
									UPDATE [dbo].[UserAuthToken] SET    
									ExpiryDate = DATEADD(minute, @minutes, GETUTCDATE())    
									WHERE Token = @token;     
									
									SET @errorNo = 0;    
									SET @message = 'Success'    
								END     
						END    
					END;    
				COMMIT TRANSACTION;       
			END TRY    
			BEGIN CATCH    
				ROLLBACK TRANSACTION;    
				SET @errorNo = ERROR_NUMBER()    
				IF @errorNo=547--Foreign Key violation    
				BEGIN SET @message =  'Please enter correct values.' END    
				ELSE THROW;    
			END CATCH     
		SELECT ISNULL(@errorNo, 0) AS [ErrorNo],                 
        @message AS [Message],
		@userId AS [Identity];       
	END; 
GO
