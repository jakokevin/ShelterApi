-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Gép: mysql:3306
-- Létrehozás ideje: 2026. Nov 23. 09:18
-- Kiszolgáló verziója: 26.7.0
-- PHP verzió: 8.3.35

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `animalshelter`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `animals`
--

CREATE TABLE `animals` (
  `Id` char(36) NOT NULL,
  `Name` varchar(40) NOT NULL,
  `Species` varchar(30) NOT NULL,
  `Age` int NOT NULL,
  `Weight` double NOT NULL,
  `RegTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `ShelterId` char(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `animals`
--

INSERT INTO `animals` (`Id`, `Name`, `Species`, `Age`, `Weight`, `RegTime`, `ShelterId`) VALUES
('4f14da97-8e6c-4c9b-9a04-0beba9b7c509', 'magyar', 'kisgyerek', 1, 1, '2026-11-23 09:22:06', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('98587398-e711-11f1-a88b-26a6373f2e20', 'Bodri', 'Kutya', 3, 18.5, '2026-11-23 07:43:38', 'dd87a9dd-de9b-55c7-8fa6-fdafdfcc4a64'),
('98589d0d-e711-11f1-a88b-26a6373f2e20', 'Cirmi', 'Macska', 2, 4.2, '2026-11-23 07:43:38', '95f1187f-f2c2-54b7-afc1-3dba453580ab'),
('98589eb9-e711-11f1-a88b-26a6373f2e20', 'Max', 'Kutya', 5, 25, '2026-11-23 07:43:38', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('9858a002-e711-11f1-a88b-26a6373f2e20', 'Luna', 'Macska', 4, 3.8, '2026-11-23 07:43:38', 'c41845d3-62ac-5c31-9b16-80193b82aec0'),
('acdc2fea-4782-465b-b0c7-42616bd79218', 'bigmate', 'hörcsög hamster', 1, 1, '2026-11-23 09:50:05', 'a2691474-e139-594f-a782-443415ee7d41'),
('bd326987-393b-4ad6-b84b-5808f7588a8d', 'Brendon', 'vizzilo', 1, 1, '2026-11-23 09:37:12', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('c7115840-7131-4b80-97de-9a21a56d65a3', 'vidákadriánó', 'cigany', 1, 1, '2026-11-23 09:47:10', 'a2691474-e139-594f-a782-443415ee7d41');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `shelters`
--

CREATE TABLE `shelters` (
  `Id` char(36) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `City` varchar(40) NOT NULL,
  `Capacity` int NOT NULL,
  `RegTime` datetime DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `shelters`
--

INSERT INTO `shelters` (`Id`, `Name`, `City`, `Capacity`, `RegTime`) VALUES
('8d32af8e-bca2-5907-997f-05ff3b325fe7', 'Gazdira Vár', 'Debrecen', 25, '2026-08-05 08:04:01'),
('95f1187f-f2c2-54b7-afc1-3dba453580ab', 'Cirmos Menedék', 'Eger', 15, '2026-07-18 11:51:48'),
('a2691474-e139-594f-a782-443415ee7d41', 'Boldog Mancs', 'Szeged', 40, '2026-01-08 15:34:23'),
('c41845d3-62ac-5c31-9b16-80193b82aec0', 'Kis Menedék', 'Salgótarján', 3, '2026-05-25 09:52:06'),
('dd87a9dd-de9b-55c7-8fa6-fdafdfcc4a64', 'Négylábú Otthon', 'Miskolc', 30, '2026-04-10 08:46:25');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `animals`
--
ALTER TABLE `animals`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_animals_shelters` (`ShelterId`);

--
-- A tábla indexei `shelters`
--
ALTER TABLE `shelters`
  ADD PRIMARY KEY (`Id`);

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `animals`
--
ALTER TABLE `animals`
  ADD CONSTRAINT `fk_animals_shelters` FOREIGN KEY (`ShelterId`) REFERENCES `shelters` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
