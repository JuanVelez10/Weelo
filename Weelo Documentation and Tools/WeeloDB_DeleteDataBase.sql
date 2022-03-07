--Delete database if it exists
USE WeeloDB
GO
DROP TABLE [Message]
DROP TABLE PropertyImage
DROP TABLE PropertyTrace
DROP TABLE Property
DROP TABLE Zone
DROP TABLE City
DROP TABLE State
DROP TABLE Country
DROP TABLE Account
USE MASTER
GO
DROP DATABASE WeeloDB