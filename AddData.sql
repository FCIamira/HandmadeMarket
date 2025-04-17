INSERT INTO Categories(name) VALUES 
('Accessories'),
('Home Decor'),
('Art Prints'),
('Bags');

INSERT INTO Sellers (storeName, email, phoneNumber, createdAt) VALUES 
('Handmade by Amira', 'amira@store.com', '0100000001', GETDATE()),
('Crafted Treasures', 'treasures@store.com', '0100000002', GETDATE()),
('Elegant Touch', 'elegant@store.com', '0100000003', GETDATE()),
('Natural Vibes', 'vibes@store.com', '0100000004', GETDATE());

INSERT INTO Customers (FirstName, LastName, Email, Password, Phone, Address) VALUES 
('Amira', 'Ahmed', 'amira1@mail.com', 'pass123', '0111111111', 'Cairo'),
('Sara', 'Mohamed', 'sara@mail.com', 'pass123', '0111111112', 'Giza'),
('Omar', 'Ali', 'omar@mail.com', 'pass123', '0111111113', 'Alexandria'),
('Laila', 'Hassan', 'laila@mail.com', 'pass123', '0111111114', 'Mansoura');


INSERT INTO Products (SKU, Description, Name, Price, Stock, Image, categoryId, sellerId) VALUES 
('P001', 'Colorful bracelet', 'Bracelet', 100.00, 10, 'bracelet.jpg', 1, 1),
('P002', 'Wooden candle holder', 'Candle Holder', 200.00, 5, 'candle.jpg', 2, 2),
('P003', 'Abstract wall art', 'Wall Art', 300.00, 7, 'art.jpg', 3, 3),
('P004', 'Crochet tote bag', 'Tote Bag', 250.00, 4, 'bag.jpg', 4, 4);


INSERT INTO Shipments (ShipmentDate, Address, City, State, ZipCode, Country, CustomerId) VALUES 
(GETDATE(), '1 Main St', 'Cairo', 'Cairo', '11111', 'Egypt', 1),
(GETDATE(), '2 Nile St', 'Giza', 'Giza', '22222', 'Egypt', 2),
(GETDATE(), '3 Sea St', 'Alexandria', 'Alex', '33333', 'Egypt', 3),
(GETDATE(), '4 Green St', 'Mansoura', 'Dakahlia', '44444', 'Egypt', 4);

INSERT INTO [Orders] (Order_Date, Total_Price, CustomerId, ShipmentId) VALUES 
(GETDATE(), 200.00, 1, 1),
(GETDATE(), 400.00, 2, 2),
(GETDATE(), 300.00, 3, 3),
(GETDATE(), 250.00, 4, 4);


INSERT INTO Items (OrderId, ProductId, Quantity, Price) VALUES 
(1, 1, 2, 100.00),
(2, 2, 2, 200.00),
(3, 3, 1, 300.00),
(4, 4, 1, 250.00);


INSERT INTO Carts (Quantity, CustomerId, ProductId) VALUES 
(1, 1, 1),
(2, 2, 2),
(1, 3, 3),
(3, 4, 4);


INSERT INTO Wishlists (CustomerId, ProductId) VALUES 
(1, 2),
(2, 3),
(3, 1),
(4, 4);

