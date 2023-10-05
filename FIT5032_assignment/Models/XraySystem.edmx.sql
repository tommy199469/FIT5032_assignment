
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/06/2023 01:35:45
-- Generated from EDMX file: C:\Users\User\source\repos\FIT5032_assignment\FIT5032_assignment\Models\XraySystem.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [XraySystem];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_GPBooking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookingSet] DROP CONSTRAINT [FK_GPBooking];
GO
IF OBJECT_ID(N'[dbo].[FK_XrayGP]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GPSet] DROP CONSTRAINT [FK_XrayGP];
GO
IF OBJECT_ID(N'[dbo].[FK_GPRating]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RatingSet] DROP CONSTRAINT [FK_GPRating];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[RatingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RatingSet];
GO
IF OBJECT_ID(N'[dbo].[BookingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BookingSet];
GO
IF OBJECT_ID(N'[dbo].[GPSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GPSet];
GO
IF OBJECT_ID(N'[dbo].[XraySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[XraySet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'RatingSet'
CREATE TABLE [dbo].[RatingSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [userId] nvarchar(max)  NOT NULL,
    [GPId] int  NOT NULL
);
GO

-- Creating table 'BookingSet'
CREATE TABLE [dbo].[BookingSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [userId] nvarchar(max)  NOT NULL,
    [GPId] int  NOT NULL,
    [bookingDateTime] datetime  NOT NULL,
    [total_cost] float  NOT NULL
);
GO

-- Creating table 'GPSet'
CREATE TABLE [dbo].[GPSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ADDRESS] nvarchar(max)  NOT NULL,
    [phone] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [XrayId] int  NOT NULL
);
GO

-- Creating table 'XraySet'
CREATE TABLE [dbo].[XraySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [cost] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'RatingSet'
ALTER TABLE [dbo].[RatingSet]
ADD CONSTRAINT [PK_RatingSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BookingSet'
ALTER TABLE [dbo].[BookingSet]
ADD CONSTRAINT [PK_BookingSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GPSet'
ALTER TABLE [dbo].[GPSet]
ADD CONSTRAINT [PK_GPSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'XraySet'
ALTER TABLE [dbo].[XraySet]
ADD CONSTRAINT [PK_XraySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [GPId] in table 'BookingSet'
ALTER TABLE [dbo].[BookingSet]
ADD CONSTRAINT [FK_GPBooking]
    FOREIGN KEY ([GPId])
    REFERENCES [dbo].[GPSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GPBooking'
CREATE INDEX [IX_FK_GPBooking]
ON [dbo].[BookingSet]
    ([GPId]);
GO

-- Creating foreign key on [XrayId] in table 'GPSet'
ALTER TABLE [dbo].[GPSet]
ADD CONSTRAINT [FK_XrayGP]
    FOREIGN KEY ([XrayId])
    REFERENCES [dbo].[XraySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_XrayGP'
CREATE INDEX [IX_FK_XrayGP]
ON [dbo].[GPSet]
    ([XrayId]);
GO

-- Creating foreign key on [GPId] in table 'RatingSet'
ALTER TABLE [dbo].[RatingSet]
ADD CONSTRAINT [FK_GPRating]
    FOREIGN KEY ([GPId])
    REFERENCES [dbo].[GPSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GPRating'
CREATE INDEX [IX_FK_GPRating]
ON [dbo].[RatingSet]
    ([GPId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------