
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/03/2023 17:23:57
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [userId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [isAdmin] bit  NOT NULL
);
GO

-- Creating table 'RatingSet'
CREATE TABLE [dbo].[RatingSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [User_userId] int  NOT NULL,
    [GPId] int  NOT NULL
);
GO

-- Creating table 'BookingSet'
CREATE TABLE [dbo].[BookingSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [User_userId] int  NOT NULL,
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

-- Creating primary key on [userId] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([userId] ASC);
GO

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

-- Creating foreign key on [User_userId] in table 'RatingSet'
ALTER TABLE [dbo].[RatingSet]
ADD CONSTRAINT [FK_UserRating]
    FOREIGN KEY ([User_userId])
    REFERENCES [dbo].[UserSet]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRating'
CREATE INDEX [IX_FK_UserRating]
ON [dbo].[RatingSet]
    ([User_userId]);
GO

-- Creating foreign key on [User_userId] in table 'BookingSet'
ALTER TABLE [dbo].[BookingSet]
ADD CONSTRAINT [FK_UserBooking]
    FOREIGN KEY ([User_userId])
    REFERENCES [dbo].[UserSet]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserBooking'
CREATE INDEX [IX_FK_UserBooking]
ON [dbo].[BookingSet]
    ([User_userId]);
GO

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