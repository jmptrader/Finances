﻿
--
alter TABLE [dbo].[Transfer] add CONSTRAINT FK_Transfer_TransferCategory FOREIGN KEY (TransferCategoryId) REFERENCES dbo.TransferCategory(TransferCategoryId)

-- DROP TABLE [dbo].[Transfer] 

IF OBJECT_ID('dbo.Transfer') IS NULL
BEGIN
PRINT '	Creating [dbo].[Transfer] ...'
CREATE TABLE [dbo].[Transfer] 
(
     TransferId			INT IDENTITY (1, 1) NOT NULL
    ,Name				VARCHAR(100) NOT NULL
	,FromBankAccountId	INT NULL
	,ToBankAccountId	INT NULL
	,TransferCategoryId	INT NOT NULL
	,Amount				MONEY NOT NULL
	,AmountTolerence	NUMERIC(3,2) NOT NULL	-- %
	,ScheduleStartDate			DATE NOT NULL
	,ScheduleEndDate			DATE NULL
	,ScheduleFrequency			VARCHAR(100) NOT NULL
	,ScheduleFrequencyEvery		INT NOT NULL
	,IsEnabled			BIT NOT NULL

    ,RecordCreatedDateTime DATETIME NOT NULL DEFAULT(GETDATE())
    ,RecordUpdatedDateTime DATETIME NOT NULL DEFAULT(GETDATE())
    
    ,PRIMARY KEY CLUSTERED (TransferId ASC)
    ,CONSTRAINT UK_Transfer_Name UNIQUE (Name)
    ,CONSTRAINT FK_Transfer_FromBankAccountId FOREIGN KEY (FromBankAccountId) REFERENCES dbo.BankAccount(BankAccountId)
    ,CONSTRAINT FK_Transfer_ToBankAccountId FOREIGN KEY (ToBankAccountId) REFERENCES dbo.BankAccount(BankAccountId)
	,CONSTRAINT FK_Transfer_TransferCategory FOREIGN KEY (TransferCategoryId) REFERENCES dbo.TransferCategory(TransferCategoryId)
);
PRINT '	Done.'
END
go

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TR_Transfer_UPDATE]'))
DROP TRIGGER [dbo].[TR_Transfer_UPDATE]
GO
PRINT '	Creating trigger dbo.TR_Transfer_UPDATE ...'
GO
CREATE TRIGGER dbo.TR_Transfer_UPDATE
ON dbo.Transfer
FOR UPDATE
AS
BEGIN
	SET NOCOUNT ON
	UPDATE	a
	SET		RecordUpdatedDateTime=GETDATE()
	FROM	dbo.Transfer a
		INNER JOIN inserted i
			ON	a.TransferId=i.TransferId
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
		WHERE	TABLE_SCHEMA+'.'+TABLE_NAME+'.'+COLUMN_NAME='dbo.Transfer.FrequencyEvery')
BEGIN
	ALTER TABLE dbo.Transfer
	ADD	FrequencyEvery INT NOT NULL DEFAULT(1)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
		WHERE	TABLE_SCHEMA+'.'+TABLE_NAME+'.'+COLUMN_NAME='dbo.Transfer.ScheduleStartDate')
BEGIN
	exec sp_rename 'dbo.Transfer.StartDate','ScheduleStartDate','COLUMN'
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
		WHERE	TABLE_SCHEMA+'.'+TABLE_NAME+'.'+COLUMN_NAME='dbo.Transfer.ScheduleEndDate')
BEGIN
	exec sp_rename 'dbo.Transfer.EndDate','ScheduleEndDate','COLUMN'
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
		WHERE	TABLE_SCHEMA+'.'+TABLE_NAME+'.'+COLUMN_NAME='dbo.Transfer.ScheduleFrequency')
BEGIN
	exec sp_rename 'dbo.Transfer.Frequency','ScheduleFrequency','COLUMN'
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
		WHERE	TABLE_SCHEMA+'.'+TABLE_NAME+'.'+COLUMN_NAME='dbo.Transfer.ScheduleFrequencyEvery')
BEGIN
	exec sp_rename 'dbo.Transfer.FrequencyEvery','ScheduleFrequencyEvery','COLUMN'
END

