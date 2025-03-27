BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'OrderName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Orders] ALTER COLUMN [OrderName] nvarchar(50) NOT NULL;
GO

ALTER TABLE [FoodsOrders] ADD [Count] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [DrinksOrders] (
    [DrinkId] int NOT NULL,
    [OrderId] int NOT NULL,
    [Created] datetime2 NOT NULL,
    [Count] int NOT NULL,
    CONSTRAINT [PK_DrinksOrders] PRIMARY KEY ([DrinkId], [OrderId]),
    CONSTRAINT [FK_DrinksOrders_Drinks_DrinkId] FOREIGN KEY ([DrinkId]) REFERENCES [Drinks] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_DrinksOrders_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_DrinksOrders_OrderId] ON [DrinksOrders] ([OrderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241119195139_AddedDrinkOrderTable', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FoodTypes]') AND [c].[name] = N'Name');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [FoodTypes] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [FoodTypes] ALTER COLUMN [Name] nvarchar(60) NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Foods]') AND [c].[name] = N'Name');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Foods] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Foods] ALTER COLUMN [Name] nvarchar(60) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250124141016_UpdateFoodNameMaxLength', N'8.0.8');
GO

COMMIT;
GO

