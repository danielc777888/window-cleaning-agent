if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Session_Agent]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Session] DROP CONSTRAINT FK_Session_Agent
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Session_Building]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Session] DROP CONSTRAINT FK_Session_Building
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Action_Session]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ActionState] DROP CONSTRAINT FK_Action_Session
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ins_ActionState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ins_ActionState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ins_Building]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ins_Building]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ins_Session]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ins_Session]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sel_ActionState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sel_ActionState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sel_Agent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sel_Agent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sel_Session]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sel_Session]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sel_SessionByBuilding]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sel_SessionByBuilding]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[upd_Session]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[upd_Session]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ActionState]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ActionState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Agent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Agent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Building]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Building]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Session]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Session]
GO

CREATE TABLE [dbo].[ActionState] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[SessionId] [int] NOT NULL ,
	[Step] [int] NOT NULL ,
	[Action] [int] NOT NULL ,
	[Direction] [int] NOT NULL ,
	[Column] [int] NOT NULL ,
	[Row] [int] NOT NULL ,
	[Bay] [bit] NOT NULL ,
	[BuildingEdge] [bit] NOT NULL ,
	[Window] [bit] NOT NULL ,
	[DirtLevel] [int] NOT NULL ,
	[Obstacle] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Agent] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Building] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Session] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[AgentId] [int] NOT NULL ,
	[BuildingId] [int] NOT NULL ,
	[SearchStrategy] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[StartTime] [datetime] NOT NULL ,
	[EndTime] [datetime] NULL ,
	[WindowsCleaned] [int] NOT NULL ,
	[WindowsToClean] [int] NOT NULL ,
	[StepCost] [int] NOT NULL ,
	[Tick] [int] NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ActionState] WITH NOCHECK ADD 
	CONSTRAINT [PK_Action] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Agent] WITH NOCHECK ADD 
	CONSTRAINT [PK_Agent] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Building] WITH NOCHECK ADD 
	CONSTRAINT [PK_Building] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Session] WITH NOCHECK ADD 
	CONSTRAINT [PK_Session] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ActionState] ADD 
	CONSTRAINT [FK_Action_Session] FOREIGN KEY 
	(
		[SessionId]
	) REFERENCES [dbo].[Session] (
		[Id]
	)
GO

ALTER TABLE [dbo].[Session] ADD 
	CONSTRAINT [FK_Session_Agent] FOREIGN KEY 
	(
		[AgentId]
	) REFERENCES [dbo].[Agent] (
		[Id]
	),
	CONSTRAINT [FK_Session_Building] FOREIGN KEY 
	(
		[BuildingId]
	) REFERENCES [dbo].[Building] (
		[Id]
	)
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE ins_ActionState
(
	@SessionId int,
	@Step int,
	@Action int,
	@Direction int,
	@Column int,
	@Row int,
	@Bay bit,
	@BuildingEdge bit,
	@Window bit,
	@DirtLevel int,
	@Obstacle bit
	
)
 AS

INSERT INTO [ActionState]
(SessionId, Step, [Action], Direction, [Column],Row,Bay,BuildingEdge,Window,DirtLevel,Obstacle)
VALUES
(@SessionId,@Step,@Action,@Direction,@Column,@Row,@Bay,@BuildingEdge,@Window,@DirtLevel,@Obstacle)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE ins_Building
(
	@Name varchar(50)
)
 AS

INSERT INTO Building ([Name])
VALUES (@Name)

SELECT @@IDENTITY
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE ins_Session
(
	@AgentId int,
	@BuildingId int,
	@SearchStrategy varchar(50),
	@StartTime datetime,
	@WindowsCleaned int,
	@WindowsToClean int,
	@StepCost int,
	@Tick int
)
 AS

INSERT INTO Session (AgentId,BuildingId,SearchStrategy,StartTime,WindowsCleaned,WindowsToClean,StepCost,Tick)
VALUES (@AgentId,@BuildingId,@SearchStrategy,@StartTime,@WindowsCleaned,@WindowsToClean,@StepCost,@Tick)

SELECT @@IDENTITY
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE sel_ActionState
(
	@SessionId int
)
 AS

SELECT * FROM ActionState WHERE SessionId = @SessionId
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE sel_Agent
(
	@Id int
)
AS
SET NOCOUNT ON
SELECT * FROM Agent WHERE Id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE sel_Session
(
	@SessionId int
)
AS

SELECT * FROM Session WHERE Id = @SessionId
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE sel_SessionByBuilding
(
	@BuildingId int
)
AS
SELECT * FROM Session WHERE BuildingId = @BuildingId
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE upd_Session
(
	@Id int,
	@AgentId int,
	@BuildingId int,
	@SearchStrategy varchar(50),
	@StartTime datetime,
	@EndTime datetime=NULL,
	@WindowsCleaned int,
	@WindowsToClean int,
	@StepCost int,
	@Tick int
)
 AS

UPDATE Session 
SET AgentId=@AgentId,
	BuildingId=@BuildingId,
	SearchStrategy=@SearchStrategy,
	StartTime=@StartTime,
	EndTime=@EndTime,
	WindowsCleaned=@WindowsCleaned,
	WindowsToClean=@WindowsToClean,
	StepCost=@StepCost,
	Tick = @Tick
WHERE Id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

INSERT INTO Agent ([Name]) VALUES ('Agent1')