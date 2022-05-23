-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th5 07, 2022 lúc 05:34 AM
-- Phiên bản máy phục vụ: 10.4.22-MariaDB
-- Phiên bản PHP: 8.0.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `maximus`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `contents`
--

CREATE TABLE `contents` (
  `Id` char(36) CHARACTER SET ascii NOT NULL,
  `ContentType` int(11) NOT NULL,
  `Suggest` longtext NOT NULL,
  `CreatedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `contents`
--

INSERT INTO `contents` (`Id`, `ContentType`, `Suggest`, `CreatedAt`) VALUES
('61e68157-94ee-431d-be12-7e8b02b6bbcd', 2, 'string', '2022-04-26 04:08:52.423003'),
('a97d4ea3-aa0a-45ce-a77e-74936df62592', 1, 'string', '2022-04-26 04:08:48.464900');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `contentsessions`
--

CREATE TABLE `contentsessions` (
  `ContentId` char(36) CHARACTER SET ascii NOT NULL,
  `SessionId` char(36) CHARACTER SET ascii NOT NULL,
  `CreatedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `contentsessions`
--

INSERT INTO `contentsessions` (`ContentId`, `SessionId`, `CreatedAt`) VALUES
('61e68157-94ee-431d-be12-7e8b02b6bbcd', 'a11e030f-365c-4186-aa66-16557fe5b571', '2022-04-26 08:12:19.127199'),
('a97d4ea3-aa0a-45ce-a77e-74936df62592', 'a11e030f-365c-4186-aa66-16557fe5b571', '2022-04-26 08:12:19.127348');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `items`
--

CREATE TABLE `items` (
  `Id` char(36) CHARACTER SET ascii NOT NULL,
  `Name` longtext NOT NULL,
  `Order` int(11) NOT NULL,
  `ItemType` int(11) NOT NULL,
  `RoundType` int(11) NOT NULL,
  `SessionId` char(36) CHARACTER SET ascii NOT NULL,
  `CreatedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `items`
--

INSERT INTO `items` (`Id`, `Name`, `Order`, `ItemType`, `RoundType`, `SessionId`, `CreatedAt`) VALUES
('1562dfa9-2490-46a8-a62f-d7abe5822cce', 'put 4', 10, 0, 0, 'e9c57a96-8bf6-47b2-843f-87231aec3c47', '2022-04-20 07:03:44.717644'),
('2f804145-a455-4a19-bfa8-9d00d0ef9073', 'string', 0, 1, 1, 'f43f199f-bd8f-44c0-84ae-097ee69f24d3', '2022-04-21 09:23:11.506830'),
('5d1a62a9-7a4f-49e3-99f3-356f9849160f', 'string', 0, 1, 1, 'e9c57a96-8bf6-47b2-843f-87231aec3c47', '2022-04-20 07:03:44.717644'),
('71802444-851f-4cdf-86e3-0f09b101cd89', 'string', 0, 10, 1, 'e9c57a96-8bf6-47b2-843f-87231aec3c47', '2022-04-20 07:03:44.717641'),
('80b48d3f-28be-41d4-8110-24825a82bb9f', 'string', 0, 1, 1, 'f43f199f-bd8f-44c0-84ae-097ee69f24d3', '2022-04-21 09:23:11.507206'),
('8741f374-c685-45d2-8be1-013f48108a30', 'string', 0, 1, 1, 'f43f199f-bd8f-44c0-84ae-097ee69f24d3', '2022-04-21 09:23:11.507203'),
('8e7c813c-3d99-46e1-8fe6-0f00fd14d4be', 'string', 10, 9, 1, 'a3ba779b-aa06-4ac3-b1f3-b4b2bf2ee254', '2022-04-20 07:06:59.446062'),
('945d8046-fcbf-46a3-92c2-2ed8257a6789', 'string', 0, 1, 1, 'f43f199f-bd8f-44c0-84ae-097ee69f24d3', '2022-04-21 09:23:11.507202'),
('b52c8241-5a88-4f19-9feb-6dab98cfe462', 'string', 10, 1, 1, 'cdd15c71-d11d-4d28-89f9-233a62003893', '2022-04-20 07:06:33.843317'),
('ca2b0a91-e6d4-43fc-836c-4757b93defcd', 'string', 0, 1, 1, 'f43f199f-bd8f-44c0-84ae-097ee69f24d3', '2022-04-20 07:01:03.368759'),
('cd84593f-660d-4084-9802-5fb315429c67', 'string', 0, 1, 1, 'e9c57a96-8bf6-47b2-843f-87231aec3c47', '2022-04-20 07:03:44.717645'),
('de137a24-a02a-417b-810f-6dc94296e0b3', 'string', 0, 1, 1, 'f43f199f-bd8f-44c0-84ae-097ee69f24d3', '2022-04-21 09:23:11.507204'),
('dfe177c7-9794-461e-84b1-70f4fc94d677', 'vận khí', 0, 1, 1, 'd5076ff6-a730-47e5-9240-18db4dcf35ff', '2022-04-20 06:39:56.713276');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `sessions`
--

CREATE TABLE `sessions` (
  `Id` char(36) CHARACTER SET ascii NOT NULL,
  `Name` longtext NOT NULL,
  `CreatedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `sessions`
--

INSERT INTO `sessions` (`Id`, `Name`, `CreatedAt`) VALUES
('a11e030f-365c-4186-aa66-16557fe5b571', 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa', '2022-04-20 06:47:16.177574'),
('a3ba779b-aa06-4ac3-b1f3-b4b2bf2ee254', 'string', '2022-04-20 07:06:59.446054'),
('cdd15c71-d11d-4d28-89f9-233a62003893', 'string', '2022-04-20 07:06:33.843017'),
('d5076ff6-a730-47e5-9240-18db4dcf35ff', 'session 1', '2022-04-20 06:39:56.713063'),
('e9c57a96-8bf6-47b2-843f-87231aec3c47', 'sesion 6', '2022-04-20 07:03:44.717637'),
('f43f199f-bd8f-44c0-84ae-097ee69f24d3', 'sesion 5', '2022-04-20 07:01:03.368445');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `users`
--

CREATE TABLE `users` (
  `Id` char(36) CHARACTER SET ascii NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `Avatar` longtext DEFAULT NULL,
  `EmailAddress` longtext NOT NULL,
  `FullName` longtext NOT NULL,
  `PasswordHash` longtext NOT NULL,
  `Code` longtext DEFAULT NULL,
  `CodeExpires` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `users`
--

INSERT INTO `users` (`Id`, `CreatedAt`, `Avatar`, `EmailAddress`, `FullName`, `PasswordHash`, `Code`, `CodeExpires`) VALUES
('01a9230c-474a-45e3-ba9b-3da395308f0b', '2022-04-25 11:39:15.408567', NULL, 'dat@gmail.com', 'string', '$2a$11$IN3r94gjsbNvHUhMGrnlUuKzZTBVhUSYSAQF1hffzmJTYVXTeDnty', NULL, NULL),
('19cbde37-21c4-4b9c-90a1-2ff88ac51954', '2022-04-23 02:36:55.976782', NULL, 'dattran02022000@gmail.com', '123456', '$2a$11$Pmw1tsIB9s8.g9Lxz2/O0ufmQfWlTXUC8Do4iJ6mNtDgMmNUhgQJ6', '389257', '2022-04-27 07:36:38.991877'),
('6dee68e8-d013-4b74-9437-8b13c0d4145a', '2022-04-18 03:31:35.285427', NULL, 'tdat1155@gmail.com', 'dat tran', '$2a$11$/2bXWBEZVqMRq0UczuiUPu79E.hkwT4CqGsTYZFZISE047hnAQeRG', '868005', '2022-04-21 07:50:10.391696'),
('6f45a0e1-7ac5-4a3a-a57d-4f9fceaa4977', '2022-04-23 02:27:25.707894', NULL, 'dat.197pm09434@vanlanguni.vn', 'Trần Thành Đạt', '$2a$11$QL8AXOJFSWzF47HqTfkeB.pKxapTdIUekWPCcCPQjOydTzDB8TWL.', '649101', '2022-04-23 02:38:55.923291');

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `contents`
--
ALTER TABLE `contents`
  ADD PRIMARY KEY (`Id`);

--
-- Chỉ mục cho bảng `contentsessions`
--
ALTER TABLE `contentsessions`
  ADD PRIMARY KEY (`SessionId`,`ContentId`),
  ADD KEY `IX_ContentSessions_ContentId` (`ContentId`);

--
-- Chỉ mục cho bảng `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Items_SessionId` (`SessionId`);

--
-- Chỉ mục cho bảng `sessions`
--
ALTER TABLE `sessions`
  ADD PRIMARY KEY (`Id`);

--
-- Chỉ mục cho bảng `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`);

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `contentsessions`
--
ALTER TABLE `contentsessions`
  ADD CONSTRAINT `FK_ContentSessions_Contents_ContentId` FOREIGN KEY (`ContentId`) REFERENCES `contents` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_ContentSessions_Sessions_SessionId` FOREIGN KEY (`SessionId`) REFERENCES `sessions` (`Id`) ON DELETE CASCADE;

--
-- Các ràng buộc cho bảng `items`
--
ALTER TABLE `items`
  ADD CONSTRAINT `FK_Items_Sessions_SessionId` FOREIGN KEY (`SessionId`) REFERENCES `sessions` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
