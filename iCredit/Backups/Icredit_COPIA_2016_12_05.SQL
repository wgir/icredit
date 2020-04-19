-- MySQL dump 10.13  Distrib 5.5.49, for Win64 (x86)
--
-- Host: localhost    Database: icredit
-- ------------------------------------------------------
-- Server version	5.5.49

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__migrationhistory`
--

DROP TABLE IF EXISTS `__migrationhistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `__migrationhistory` (
  `MigrationId` varchar(255) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `Model` longblob NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__migrationhistory`
--

LOCK TABLES `__migrationhistory` WRITE;
/*!40000 ALTER TABLE `__migrationhistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `__migrationhistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `abono`
--

DROP TABLE IF EXISTS `abono`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `abono` (
  `AbonoId` int(11) NOT NULL AUTO_INCREMENT,
  `CuotaId` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Valor` double NOT NULL,
  `Paga` double NOT NULL,
  `Devolucion` double NOT NULL,
  `Observacion` text,
  `CreadoPor` varchar(128) DEFAULT NULL,
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT NULL,
  `FechaModificacion` datetime DEFAULT NULL,
  `AbonoNro` int(11) DEFAULT NULL,
  `FechaEnvio` datetime DEFAULT NULL,
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`AbonoId`),
  KEY `IX_CuotaId` (`CuotaId`),
  CONSTRAINT `FK_Abono_Cuota_CuotaId` FOREIGN KEY (`CuotaId`) REFERENCES `cuota` (`CuotaId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=75 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `abono`
--

LOCK TABLES `abono` WRITE;
/*!40000 ALTER TABLE `abono` DISABLE KEYS */;
INSERT INTO `abono` VALUES (38,397,'2016-02-11 10:39:02',91679.99,91679.99,0,NULL,'6efe93b6-f004-4f42-956f-35013aad9140','2016-02-11 10:39:20',NULL,NULL,1,'2016-02-11 21:39:28',''),(39,409,'2016-02-19 20:04:48',73.34,73.34,0,NULL,'db856a4c-88dd-4888-8eb6-59171ad9ac17','2016-02-19 20:04:52','db856a4c-88dd-4888-8eb6-59171ad9ac17','2016-03-12 20:59:08',1,NULL,''),(40,439,'2016-02-21 18:12:02',1800,1800,0,NULL,'c10563cf-c8f1-4e23-b9c1-e773f58db58f','2016-02-21 18:12:47',NULL,NULL,1,NULL,''),(41,457,'2016-03-09 23:47:39',1150000,1150000,0,NULL,'1d401696-e6fa-4843-88ee-5a2df8f484ec','2016-03-09 23:47:54',NULL,NULL,1,NULL,''),(42,458,'2016-05-10 21:08:39',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-05-10 21:08:42',NULL,NULL,7,'2016-05-10 21:09:37',''),(43,482,'2016-05-10 21:11:04',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-05-10 21:11:06',NULL,NULL,8,NULL,''),(44,459,'2016-05-23 15:04:03',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-05-23 15:04:16',NULL,NULL,9,NULL,''),(45,483,'2016-05-23 15:04:54',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-05-23 15:04:57',NULL,NULL,10,NULL,''),(46,506,'2016-05-24 09:06:02',540103.65,540103.65,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-05-24 09:06:08',NULL,NULL,11,NULL,''),(47,578,'2016-07-05 15:01:52',27276.33,27276.33,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-07-05 15:05:41',NULL,NULL,12,NULL,''),(48,507,'2016-07-05 15:06:24',540103.65,540103.65,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-07-05 15:06:27',NULL,NULL,13,NULL,''),(49,579,'2016-08-03 12:34:47',27276.33,27276.33,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-08-03 12:35:17',NULL,NULL,14,NULL,''),(50,508,'2016-08-03 12:35:30',540103.64,540103.64,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-08-03 12:35:31',NULL,NULL,15,NULL,''),(51,484,'2016-08-03 12:35:45',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-08-03 12:35:47',NULL,NULL,16,NULL,''),(52,460,'2016-08-03 12:35:59',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-08-03 12:36:00',NULL,NULL,17,NULL,''),(53,626,'2016-09-07 16:10:43',40909.73,40909.73,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-09-07 16:10:46',NULL,NULL,18,NULL,''),(54,580,'2016-09-07 16:11:05',27276.33,27276.33,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-09-07 16:11:06',NULL,NULL,19,NULL,''),(55,509,'2016-09-07 16:11:30',540103.65,540103.65,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-09-07 16:11:32',NULL,NULL,20,NULL,''),(56,485,'2016-09-07 16:11:48',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-09-07 16:11:50',NULL,NULL,21,NULL,''),(57,461,'2016-09-07 16:12:02',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-09-07 16:12:03',NULL,NULL,22,NULL,''),(58,627,'2016-10-04 08:02:16',40909.73,40909.73,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-10-04 08:02:37',NULL,NULL,23,NULL,''),(59,581,'2016-10-04 08:02:59',27276.33,27276.33,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-10-04 08:03:02',NULL,NULL,24,NULL,''),(60,510,'2016-10-04 08:03:12',540103.65,540103.65,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-10-04 08:03:14',NULL,NULL,25,NULL,''),(61,486,'2016-10-04 08:03:24',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-10-04 08:03:26',NULL,NULL,26,NULL,''),(62,462,'2016-10-04 08:03:36',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-10-04 08:03:38',NULL,NULL,27,NULL,''),(63,582,'2016-11-03 07:45:12',27276.33,27276.33,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-11-03 07:45:46',NULL,NULL,28,NULL,''),(64,487,'2016-11-03 07:45:59',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-11-03 07:46:00',NULL,NULL,29,NULL,''),(65,463,'2016-11-03 07:46:11',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-11-03 07:46:12',NULL,NULL,30,NULL,''),(66,674,'2016-11-03 08:23:34',63283.82,63283.82,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-11-03 08:23:38',NULL,NULL,31,NULL,''),(67,628,'2016-11-03 08:24:36',40909.73,40909.73,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-11-03 08:24:38',NULL,NULL,32,NULL,''),(68,511,'2016-11-03 08:25:53',540103.64,540103.64,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-11-03 08:25:55',NULL,NULL,33,NULL,''),(69,675,'2016-11-30 00:00:00',63283.82,63283.82,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-12-05 09:03:13',NULL,NULL,34,NULL,''),(70,629,'2016-11-30 00:00:00',40909.73,40909.73,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-12-05 09:03:30',NULL,NULL,35,NULL,''),(71,512,'2016-11-30 00:00:00',540103.65,540103.65,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-12-05 09:03:43',NULL,NULL,36,NULL,''),(72,583,'2016-11-30 00:00:00',27276.33,27276.33,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-12-05 09:47:31',NULL,NULL,37,NULL,''),(73,488,'2016-11-30 00:00:00',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-12-05 09:47:40',NULL,NULL,38,NULL,''),(74,464,'2016-11-30 00:00:00',151517.52,151517.52,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-12-05 09:47:47',NULL,NULL,39,NULL,'');
/*!40000 ALTER TABLE `abono` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aporte`
--

DROP TABLE IF EXISTS `aporte`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `aporte` (
  `AporteId` int(11) NOT NULL AUTO_INCREMENT,
  `Valor` double NOT NULL,
  `Fecha` datetime NOT NULL,
  `SocioId` int(11) NOT NULL,
  `ConceptoAporteId` int(11) NOT NULL,
  `Observacion` text,
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  `CreadoPor` varchar(128) DEFAULT '',
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT '',
  `FechaModificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`AporteId`),
  KEY `IX_SocioId` (`SocioId`),
  KEY `IX_ConceptoAporteId` (`ConceptoAporteId`),
  CONSTRAINT `FK_Aporte_ConceptoAporte_ConceptoAporteId` FOREIGN KEY (`ConceptoAporteId`) REFERENCES `conceptoaporte` (`ConceptoAporteId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FK_Aporte_Socio_SocioId` FOREIGN KEY (`SocioId`) REFERENCES `socio` (`SocioId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aporte`
--

LOCK TABLES `aporte` WRITE;
/*!40000 ALTER TABLE `aporte` DISABLE KEYS */;
INSERT INTO `aporte` VALUES (14,1000000,'2016-02-11 00:00:00',6,36,NULL,'','6efe93b6-f004-4f42-956f-35013aad9140','2016-02-11 14:16:40',NULL,NULL),(15,1000000,'2016-02-11 00:00:00',6,36,NULL,'','6efe93b6-f004-4f42-956f-35013aad9140','2016-02-11 14:17:05',NULL,NULL),(16,10000,'2016-02-11 00:00:00',7,37,NULL,'','67801670-1198-46d4-b939-f7522f92a5ae','2016-02-11 14:37:22',NULL,NULL),(17,24700000,'2016-04-08 00:00:00',5,45,'Venta Mazda KIG-681','','97f27fc9-1061-4355-b689-206de7790f54','2016-04-08 10:22:54',NULL,NULL),(18,30000,'2016-04-26 00:00:00',5,45,'inyeccion de capital para ajustar los 18\'000.000 para prestarle a emma','','97f27fc9-1061-4355-b689-206de7790f54','2016-04-26 13:04:47',NULL,NULL),(19,312687.44,'2016-05-25 00:00:00',5,45,'resultado de utilidad de mayo 2016','','97f27fc9-1061-4355-b689-206de7790f54','2016-05-25 14:05:27',NULL,NULL),(20,376758.02,'2016-08-03 00:00:00',5,45,NULL,'','97f27fc9-1061-4355-b689-206de7790f54','2016-08-03 12:37:55',NULL,NULL),(21,382134.84,'2016-09-07 00:00:00',5,45,'aporte de utilidades del mes de agosto 2016','','97f27fc9-1061-4355-b689-206de7790f54','2016-09-07 16:18:03',NULL,NULL),(22,500000,'2016-09-13 00:00:00',5,45,'plata enviada desde chile.. se enviaron 830.000, 230.000 para ddf. 100.000 para completar plata de FGR. y 500.000 para Icredit. ','','97f27fc9-1061-4355-b689-206de7790f54','2016-09-13 14:45:46',NULL,NULL),(23,373983.14,'2016-10-04 00:00:00',5,45,'aporte de utilidades del mes de septiembre 2016 ','','97f27fc9-1061-4355-b689-206de7790f54','2016-10-04 08:37:50',NULL,NULL);
/*!40000 ALTER TABLE `aporte` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroles`
--

DROP TABLE IF EXISTS `aspnetroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `aspnetroles` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroles`
--

LOCK TABLES `aspnetroles` WRITE;
/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
INSERT INTO `aspnetroles` VALUES ('0','Master'),('1','Administrador'),('2','Cobrador');
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserclaims`
--

DROP TABLE IF EXISTS `aspnetuserclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(128) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id` (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `ApplicationUser_Claims` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserclaims`
--

LOCK TABLES `aspnetuserclaims` WRITE;
/*!40000 ALTER TABLE `aspnetuserclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `aspnetuserlogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  KEY `ApplicationUser_Logins` (`UserId`),
  CONSTRAINT `ApplicationUser_Logins` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserlogins`
--

LOCK TABLES `aspnetuserlogins` WRITE;
/*!40000 ALTER TABLE `aspnetuserlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserlogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserroles`
--

DROP TABLE IF EXISTS `aspnetuserroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(128) NOT NULL,
  `RoleId` varchar(128) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IdentityRole_Users` (`RoleId`),
  CONSTRAINT `ApplicationUser_Roles` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IdentityRole_Users` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserroles`
--

LOCK TABLES `aspnetuserroles` WRITE;
/*!40000 ALTER TABLE `aspnetuserroles` DISABLE KEYS */;
INSERT INTO `aspnetuserroles` VALUES ('97f27fc9-1061-4355-b689-206de7790f54','0'),('1d401696-e6fa-4843-88ee-5a2df8f484ec','1'),('437ac981-2694-48af-be1e-d2c6b9697065','1'),('67801670-1198-46d4-b939-f7522f92a5ae','1'),('6efe93b6-f004-4f42-956f-35013aad9140','1'),('7b31884f-1895-4394-bcaa-55a0cbced37e','1'),('87b4e46f-a94f-4f31-849f-105128047b89','1'),('b8502b83-ada2-4dda-a1f9-28fc2f8b35ad','1'),('c10563cf-c8f1-4e23-b9c1-e773f58db58f','1'),('db856a4c-88dd-4888-8eb6-59171ad9ac17','1');
/*!40000 ALTER TABLE `aspnetuserroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusers`
--

DROP TABLE IF EXISTS `aspnetusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `aspnetusers` (
  `Id` varchar(128) NOT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `UserName` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetusers`
--

LOCK TABLES `aspnetusers` WRITE;
/*!40000 ALTER TABLE `aspnetusers` DISABLE KEYS */;
INSERT INTO `aspnetusers` VALUES ('1d401696-e6fa-4843-88ee-5a2df8f484ec','geovanni.almiron@tigo.net.py',0,'ALeVaiXRHtIirUzY64jD4mviu1Q8W7J52DlH5t2cmigAFYc+LEwGGRBN68YaO9y3nw==','fe93389f-83e3-419b-9436-be4815a1b888',NULL,0,0,NULL,1,0,'geovanni.almiron@tigo.net.py'),('437ac981-2694-48af-be1e-d2c6b9697065','geovanni.almiron@mail.com',0,'ABaM6sw2FEQeSbPnmp15ZlfrpfgAYJAoscQsv880hchXXFWmWmCpYHFcHIkRBGU9Fw==','1c805e8a-9deb-4308-8574-bcee3df7f6ec',NULL,0,0,NULL,1,0,'geovanni.almiron@mail.com'),('67801670-1198-46d4-b939-f7522f92a5ae','lilalg16@hotmail.com',0,'ALSz+fE6cvo/Cj/ml5TqSPMS2zSD+fI06NYXbnvP5tFUZVcUIunlmuMjcr1PUUXLIA==','4e7d649f-394b-4bcf-9214-7ac49806b735',NULL,0,0,NULL,1,0,'lilalg16@hotmail.com'),('6efe93b6-f004-4f42-956f-35013aad9140','william.gustavo@gmail.com',0,'AMHn7uh04IAOy2QVuP6ZLIlw4jpgPtyyIVoAkWejJlVb4VlKYJBY+quHf+zYFTj5yQ==','d7c57271-6f5a-43c2-9333-21b1f70cdba4',NULL,0,0,NULL,1,0,'william.gustavo@gmail.com'),('7b31884f-1895-4394-bcaa-55a0cbced37e','drfernandez18@gmail.com',0,'AHoTbk2VL4+nGf774aMYYjjlOLdbNQ9JMULToCOozi939LZ5yBQyZZkRnGVN6JOk+g==','af5309a8-5bee-42b5-97ed-d025c36a1465',NULL,0,0,NULL,1,0,'drfernandez18@gmail.com'),('87b4e46f-a94f-4f31-849f-105128047b89','100211829PACF@gmail.com',0,'ADXCq9hP+UB84Ubuy3ovf9cQ7jGUq4SqD7jIKKe5vsFBKuzs5crYf3vfyI24zhywEw==','3fc0bae9-0b69-4f8a-947d-4a3be3523347',NULL,0,0,NULL,1,0,'100211829PACF@gmail.com'),('97f27fc9-1061-4355-b689-206de7790f54','williamgustavo@gmail.com',0,'AGozkje7Rvgl1kJNobMllYE3F3YL70GwBlciD9OPDxD2enVcf2oEHJDTxdwm7nvcHA==','f15a26c3-4161-4e9a-b48e-206ff2adcbae',NULL,0,0,NULL,1,0,'williamgustavo@gmail.com'),('b8502b83-ada2-4dda-a1f9-28fc2f8b35ad','horka2000@hotmail.com',0,'AOOQeaj3dgPoZX8uhcVf0DY0TF3L9Ia4AG8qn5qHgemeZvaI6H2sbyZmpdhygFWFzg==','a0ba7204-d644-4ae2-95bf-ab740478211d',NULL,0,0,NULL,1,0,'horka2000@hotmail.com'),('c10563cf-c8f1-4e23-b9c1-e773f58db58f','vpadillaramirez@gmail.com',0,'AHVftmzNOiGeUXK1tqrs9SO7k9idbSd4NYynV5vRvJSbBRukXCry4B0YLN4ZRSMk9g==','159622fc-191c-420f-92c8-b1a1094d72ac',NULL,0,0,NULL,1,0,'vpadillaramirez@gmail.com'),('db856a4c-88dd-4888-8eb6-59171ad9ac17','saske_r@hotmail.com',0,'AE+JtzxT5cTshs1FTDT8UfMY5NJlF8VxNqxtDklFt4i9HeevV9ARXK+z7dtD5LX2xg==','184850c1-0db5-4259-a675-85323cb88458',NULL,0,0,NULL,1,0,'saske_r@hotmail.com');
/*!40000 ALTER TABLE `aspnetusers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ciudad`
--

DROP TABLE IF EXISTS `ciudad`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ciudad` (
  `CiudadId` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(254) DEFAULT '',
  `EmpresaId` int(11) NOT NULL,
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  `CreadoPor` varchar(128) DEFAULT '',
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT '',
  `FechaModificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`CiudadId`),
  KEY `ciudad_empresa` (`EmpresaId`),
  CONSTRAINT `ciudad_empresa` FOREIGN KEY (`EmpresaId`) REFERENCES `empresa` (`EmpresaId`)
) ENGINE=InnoDB AUTO_INCREMENT=128 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ciudad`
--

LOCK TABLES `ciudad` WRITE;
/*!40000 ALTER TABLE `ciudad` DISABLE KEYS */;
INSERT INTO `ciudad` VALUES (1,'Manizales',1,'','0c6d9e5b-f0bf-4aa6-b98b-4f95c63631c1','2016-01-28 13:48:34','0c6d9e5b-f0bf-4aa6-b98b-4f95c63631c1','2016-01-28 13:56:32'),(8,'Pereira',1,'',NULL,NULL,'abe8421a-76b6-4b22-845a-82350e80c094','2016-01-30 10:26:41'),(14,'Armenia',1,'','abe8421a-76b6-4b22-845a-82350e80c094','2016-01-30 10:41:34',NULL,NULL),(99,'Manizales',36,'',NULL,'2016-02-11 14:09:16',NULL,NULL),(100,'Pereira',36,'',NULL,'2016-02-11 14:09:16',NULL,NULL),(101,'Armenia',36,'',NULL,'2016-02-11 14:09:16',NULL,NULL),(102,'Manizales',37,'',NULL,'2016-02-11 14:32:58',NULL,NULL),(103,'Pereira',37,'',NULL,'2016-02-11 14:32:58',NULL,NULL),(104,'Armenia',37,'',NULL,'2016-02-11 14:32:58',NULL,NULL),(105,'Manizales',38,'',NULL,'2016-02-12 16:00:48',NULL,NULL),(106,'Pereira',38,'',NULL,'2016-02-12 16:00:48',NULL,NULL),(107,'Armenia',38,'',NULL,'2016-02-12 16:00:48',NULL,NULL),(108,'Manizales',39,'',NULL,'2016-02-19 20:00:13',NULL,NULL),(109,'Pereira',39,'',NULL,'2016-02-19 20:00:13',NULL,NULL),(110,'Armenia',39,'',NULL,'2016-02-19 20:00:13',NULL,NULL),(111,'Manizales',40,'',NULL,'2016-02-21 17:34:42',NULL,NULL),(112,'Pereira',40,'',NULL,'2016-02-21 17:34:42',NULL,NULL),(113,'Armenia',40,'',NULL,'2016-02-21 17:34:42',NULL,NULL),(114,'Manizales',41,'',NULL,'2016-03-06 20:12:43',NULL,NULL),(115,'Pereira',41,'',NULL,'2016-03-06 20:12:43',NULL,NULL),(116,'Armenia',41,'',NULL,'2016-03-06 20:12:43',NULL,NULL),(117,'Manizales',42,'',NULL,'2016-03-07 00:22:59',NULL,NULL),(118,'Pereira',42,'',NULL,'2016-03-07 00:22:59',NULL,NULL),(119,'Armenia',42,'',NULL,'2016-03-07 00:22:59',NULL,NULL),(120,'SFM',42,'','7b31884f-1895-4394-bcaa-55a0cbced37e','2016-03-07 00:30:30',NULL,NULL),(121,'LA VEGA',42,'','7b31884f-1895-4394-bcaa-55a0cbced37e','2016-03-07 00:30:46',NULL,NULL),(122,'Manizales',43,'',NULL,'2016-03-09 23:21:07',NULL,NULL),(123,'Pereira',43,'',NULL,'2016-03-09 23:21:07',NULL,NULL),(124,'Armenia',43,'',NULL,'2016-03-09 23:21:07',NULL,NULL),(125,NULL,44,'',NULL,'2016-03-09 23:42:36','1d401696-e6fa-4843-88ee-5a2df8f484ec','2016-03-09 23:50:13'),(126,NULL,44,'',NULL,'2016-03-09 23:42:36','1d401696-e6fa-4843-88ee-5a2df8f484ec','2016-03-09 23:50:22'),(127,NULL,44,'',NULL,'2016-03-09 23:42:36','1d401696-e6fa-4843-88ee-5a2df8f484ec','2016-03-09 23:50:26');
/*!40000 ALTER TABLE `ciudad` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cliente`
--

DROP TABLE IF EXISTS `cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cliente` (
  `ClienteId` int(11) NOT NULL AUTO_INCREMENT,
  `Nit` varchar(30) NOT NULL,
  `Nombre` varchar(200) NOT NULL DEFAULT '',
  `Direccion` varchar(500) DEFAULT '',
  `Telefono` varchar(200) DEFAULT '',
  `Email` varchar(200) DEFAULT '',
  `EmpresaId` int(11) NOT NULL,
  `CreadoPor` varchar(128) DEFAULT NULL,
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT NULL,
  `FechaModificacion` datetime DEFAULT NULL,
  `CiudadId` int(11) DEFAULT NULL,
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`ClienteId`),
  KEY `IX_EmpresaId` (`EmpresaId`),
  KEY `cliente_ciudad` (`CiudadId`),
  CONSTRAINT `cliente_ciudad` FOREIGN KEY (`CiudadId`) REFERENCES `ciudad` (`CiudadId`),
  CONSTRAINT `FK_Cliente_Empresa_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `empresa` (`EmpresaId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cliente`
--

LOCK TABLES `cliente` WRITE;
/*!40000 ALTER TABLE `cliente` DISABLE KEYS */;
INSERT INTO `cliente` VALUES (6,'75088866','William Gustavo giraldo',NULL,NULL,'Williamgustavo@Gmail.com',36,'6efe93b6-f004-4f42-956f-35013aad9140','2016-02-11 14:28:51',NULL,NULL,99,''),(7,'1','x',NULL,NULL,'x@gmail.com',36,'6efe93b6-f004-4f42-956f-35013aad9140','2016-02-11 14:50:56',NULL,NULL,99,''),(8,'121212','sadasdasd','asd',NULL,'assda@gmail.com',39,'db856a4c-88dd-4888-8eb6-59171ad9ac17','2016-02-19 20:02:08','db856a4c-88dd-4888-8eb6-59171ad9ac17','2016-03-12 20:50:54',110,''),(9,'049-0068350-1','Estanislao Genao Padilla','B/ Libertad, Cotui',NULL,'estanislao@gmail.com',40,'c10563cf-c8f1-4e23-b9c1-e773f58db58f','2016-02-21 17:56:30',NULL,NULL,113,''),(10,'049-0069417-7','Luz MarÃ­a MuÃ±Ã³z Sanchez','B/ Libertad, Cotui',NULL,'luzmaria@gmail.com',40,'c10563cf-c8f1-4e23-b9c1-e773f58db58f','2016-02-21 18:16:40',NULL,NULL,113,''),(11,'047-0214292-0','PACO ALBERTO CACERES FERNANDEZ','SABANA REY ARRIBA C/PRINCIPAL','8099640040','100211829pac@gmail.com',42,'7b31884f-1895-4394-bcaa-55a0cbced37e','2016-03-07 00:29:22',NULL,NULL,118,''),(12,'000-0000000-0','PACO ALBERTO CACERES FERNANDEZ','SABANA REY ARRIBA C/PRINCIPAL','8099640040','100211829pac@gmail.com',42,'7b31884f-1895-4394-bcaa-55a0cbced37e','2016-03-07 00:29:51',NULL,NULL,118,''),(13,'4280336','Geovanni',NULL,NULL,'geovanni.almiron@hotmail.com',44,'1d401696-e6fa-4843-88ee-5a2df8f484ec','2016-03-09 23:45:39','1d401696-e6fa-4843-88ee-5a2df8f484ec','2016-03-09 23:45:50',127,''),(14,'24784577','Maria Emma Restrepo',NULL,NULL,'williamgustavo@gmail.com',1,'97f27fc9-1061-4355-b689-206de7790f54','2016-04-08 10:14:15','97f27fc9-1061-4355-b689-206de7790f54','2016-04-08 11:12:11',1,''),(15,'STQ525','Taxi kia picanto STQ525',NULL,NULL,'williamgustavo@gmail.com',1,'97f27fc9-1061-4355-b689-206de7790f54','2016-04-11 08:34:37',NULL,NULL,1,'');
/*!40000 ALTER TABLE `cliente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `conceptoaporte`
--

DROP TABLE IF EXISTS `conceptoaporte`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `conceptoaporte` (
  `ConceptoAporteId` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(254) DEFAULT '',
  `EmpresaId` int(11) NOT NULL,
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  `CreadoPor` varchar(128) DEFAULT '',
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT '',
  `FechaModificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`ConceptoAporteId`),
  KEY `IX_EmpresaId` (`EmpresaId`),
  CONSTRAINT `FK_ConceptoAporte_Empresa_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `empresa` (`EmpresaId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `conceptoaporte`
--

LOCK TABLES `conceptoaporte` WRITE;
/*!40000 ALTER TABLE `conceptoaporte` DISABLE KEYS */;
INSERT INTO `conceptoaporte` VALUES (7,'Capital Inicial',1,'','97f27fc9-1061-4355-b689-206de7790f54','2016-01-28 14:44:44',NULL,NULL),(36,'Capital Inicial',36,'',NULL,'2016-02-11 14:09:16',NULL,NULL),(37,'Capital Inicial',37,'',NULL,'2016-02-11 14:32:58',NULL,NULL),(38,'Capital Inicial',38,'',NULL,'2016-02-12 16:00:48',NULL,NULL),(39,'Capital Inicial',39,'',NULL,'2016-02-19 20:00:13',NULL,NULL),(40,'Capital Inicial',40,'',NULL,'2016-02-21 17:34:42',NULL,NULL),(41,'Capital Inicial',41,'',NULL,'2016-03-06 20:12:43',NULL,NULL),(42,'Capital Inicial',42,'',NULL,'2016-03-07 00:22:59',NULL,NULL),(43,'Capital Inicial',43,'',NULL,'2016-03-09 23:21:07',NULL,NULL),(44,'Capital Inicial',44,'',NULL,'2016-03-09 23:42:36',NULL,NULL),(45,'Capital',1,'','97f27fc9-1061-4355-b689-206de7790f54','2016-04-08 10:04:09',NULL,NULL);
/*!40000 ALTER TABLE `conceptoaporte` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `conceptoretiro`
--

DROP TABLE IF EXISTS `conceptoretiro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `conceptoretiro` (
  `ConceptoRetiroId` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(254) DEFAULT '',
  `EmpresaId` int(11) NOT NULL,
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  `CreadoPor` varchar(128) DEFAULT '',
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT '',
  `FechaModificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`ConceptoRetiroId`),
  KEY `IX_EmpresaId` (`EmpresaId`),
  CONSTRAINT `FK_ConceptoRetiro_Empresa_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `empresa` (`EmpresaId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `conceptoretiro`
--

LOCK TABLES `conceptoretiro` WRITE;
/*!40000 ALTER TABLE `conceptoretiro` DISABLE KEYS */;
INSERT INTO `conceptoretiro` VALUES (4,'Retiro de Capital',1,'','97f27fc9-1061-4355-b689-206de7790f54','2016-01-28 14:45:05','97f27fc9-1061-4355-b689-206de7790f54','2016-01-28 14:45:10'),(33,'Retiro de Capital',36,'',NULL,'2016-02-11 14:09:16',NULL,NULL),(34,'Retiro de Capital',37,'',NULL,'2016-02-11 14:32:58',NULL,NULL),(35,'Retiro de Capital',38,'',NULL,'2016-02-12 16:00:48',NULL,NULL),(36,'Retiro de Capital',39,'',NULL,'2016-02-19 20:00:13',NULL,NULL),(37,'Retiro de Capital',40,'',NULL,'2016-02-21 17:34:42',NULL,NULL),(38,'Retiro de Capital',41,'',NULL,'2016-03-06 20:12:43',NULL,NULL),(39,'Retiro de Capital',42,'',NULL,'2016-03-07 00:22:59',NULL,NULL),(40,'Retiro de Capital',43,'',NULL,'2016-03-09 23:21:07',NULL,NULL),(41,'Retiro de Capital',44,'',NULL,'2016-03-09 23:42:36',NULL,NULL),(42,'Ajuste',1,'','97f27fc9-1061-4355-b689-206de7790f54','2016-10-11 14:14:42',NULL,NULL);
/*!40000 ALTER TABLE `conceptoretiro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `consecutivo`
--

DROP TABLE IF EXISTS `consecutivo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `consecutivo` (
  `EmpresaId` int(11) NOT NULL DEFAULT '0',
  `CreditoNro` int(11) DEFAULT NULL,
  `RetiroInteresNro` int(11) DEFAULT NULL,
  `AbonoNro` int(11) DEFAULT NULL,
  PRIMARY KEY (`EmpresaId`) USING BTREE,
  CONSTRAINT `FK_consecutivo_empresa` FOREIGN KEY (`EmpresaId`) REFERENCES `empresa` (`EmpresaId`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consecutivo`
--

LOCK TABLES `consecutivo` WRITE;
/*!40000 ALTER TABLE `consecutivo` DISABLE KEYS */;
INSERT INTO `consecutivo` VALUES (1,20,10,39),(36,1,0,1),(37,0,0,0),(38,0,0,0),(39,1,1,1),(40,1,0,1),(41,0,0,0),(42,1,0,0),(43,0,0,0),(44,1,0,1);
/*!40000 ALTER TABLE `consecutivo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contacto`
--

DROP TABLE IF EXISTS `contacto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contacto` (
  `ContactoId` int(11) NOT NULL AUTO_INCREMENT,
  `ConNombre` varchar(200) NOT NULL DEFAULT '',
  `ConTelefono` varchar(200) DEFAULT '',
  `ConEmail` varchar(200) DEFAULT '',
  `ConObserva` varchar(1000) DEFAULT NULL,
  `CreadoPor` varchar(128) DEFAULT NULL,
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT NULL,
  `FechaModificacion` datetime DEFAULT NULL,
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`ContactoId`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contacto`
--

LOCK TABLES `contacto` WRITE;
/*!40000 ALTER TABLE `contacto` DISABLE KEYS */;
INSERT INTO `contacto` VALUES (1,'william giraldo r',NULL,'williamgustavo@gmail.com',NULL,NULL,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-02-06 19:55:27',''),(2,'prueba',NULL,NULL,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2015-12-09 10:13:52',NULL,NULL,''),(5,'p2',NULL,'p2@gmail.com',NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-01-30 10:50:35',NULL,NULL,''),(6,'william ing',NULL,'william@gmail.com',NULL,'83ac4bd1-cc4d-4caa-9f29-9e8e2e269780','2016-02-01 16:28:04','83ac4bd1-cc4d-4caa-9f29-9e8e2e269780','2016-02-01 16:28:37',''),(7,'cliente 1  (empresa prueba)',NULL,'william.gustavo@gmail.com',NULL,'96c65816-09cc-47ad-ac89-337ae088fcfe','2016-02-06 19:14:22',NULL,NULL,''),(8,'william','88888','wggr@gmail.com','xxx','97f27fc9-1061-4355-b689-206de7790f54','2016-02-07 09:52:49',NULL,NULL,''),(9,'william','88888','wggr@gmail.com','xxx',NULL,'2016-02-07 09:58:06',NULL,NULL,''),(10,'william','88888','wggr@gmail.com','xxx','97f27fc9-1061-4355-b689-206de7790f54','2016-02-07 10:08:40',NULL,NULL,''),(11,'william','88888','wggr@gmail.com','xxx','97f27fc9-1061-4355-b689-206de7790f54','2016-02-07 10:09:03',NULL,NULL,''),(12,'william','88888','wggr@gmail.com','xx','97f27fc9-1061-4355-b689-206de7790f54','2016-02-07 10:13:33',NULL,NULL,''),(13,'william','88888','wggr@gmail.com','xxx','97f27fc9-1061-4355-b689-206de7790f54','2016-02-07 10:14:56',NULL,NULL,'');
/*!40000 ALTER TABLE `contacto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `credito`
--

DROP TABLE IF EXISTS `credito`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `credito` (
  `CreditoId` int(11) NOT NULL AUTO_INCREMENT,
  `Fecha` datetime NOT NULL,
  `ClienteId` int(11) NOT NULL,
  `Valor` double NOT NULL,
  `Interes` double NOT NULL,
  `Meses` int(11) NOT NULL,
  `TipoCuotaId` varchar(128) NOT NULL,
  `PrimCuota` tinyint(1) NOT NULL,
  `DivisionCreditoId` int(11) NOT NULL,
  `InteresPrimCuota` tinyint(1) NOT NULL,
  `CapitalFinalCredito` tinyint(1) NOT NULL,
  `Observacion` text,
  `CreadoPor` varchar(128) DEFAULT NULL,
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT NULL,
  `FechaModificacion` datetime DEFAULT NULL,
  `CreditoNro` int(11) DEFAULT NULL,
  `EmpresaId` int(11) DEFAULT NULL,
  `UsuarioId` int(11) DEFAULT NULL,
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`CreditoId`),
  KEY `IX_ClienteId` (`ClienteId`),
  KEY `IX_TipoCuotaId` (`TipoCuotaId`),
  KEY `IX_DivisionCreditoId` (`DivisionCreditoId`),
  KEY `FK_credito_empresa_1` (`EmpresaId`),
  CONSTRAINT `FK_Credito_Cliente_ClienteId` FOREIGN KEY (`ClienteId`) REFERENCES `cliente` (`ClienteId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FK_Credito_DivisionCredito_DivisionCreditoId` FOREIGN KEY (`DivisionCreditoId`) REFERENCES `divisioncredito` (`DivisionCreditoId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FK_credito_empresa_1` FOREIGN KEY (`EmpresaId`) REFERENCES `empresa` (`EmpresaId`) ON UPDATE CASCADE,
  CONSTRAINT `FK_Credito_TipoCuota_TipoCuotaId` FOREIGN KEY (`TipoCuotaId`) REFERENCES `tipocuota` (`TipoCuotaId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `credito`
--

LOCK TABLES `credito` WRITE;
/*!40000 ALTER TABLE `credito` DISABLE KEYS */;
INSERT INTO `credito` VALUES (17,'2016-02-11 00:00:00',6,1000000,1.5,12,'FI',0,30,0,0,NULL,'6efe93b6-f004-4f42-956f-35013aad9140','2016-02-11 14:49:22',NULL,NULL,1,36,NULL,''),(18,'2016-02-02 00:00:00',8,2000,10,1,'FI',1,1,0,0,NULL,'db856a4c-88dd-4888-8eb6-59171ad9ac17','2016-02-19 20:04:10',NULL,NULL,1,39,39,''),(19,'2016-02-01 00:00:00',9,10000,8.9,3,'IM',1,15,0,0,NULL,'c10563cf-c8f1-4e23-b9c1-e773f58db58f','2016-02-21 18:11:05',NULL,NULL,1,40,40,''),(20,'2016-03-07 00:00:00',12,1000,10,3,'IM',0,7,0,0,NULL,'7b31884f-1895-4394-bcaa-55a0cbced37e','2016-03-07 00:39:31',NULL,NULL,1,42,42,''),(21,'2016-03-09 00:00:00',13,1000000,15,1,'FI',1,30,0,0,NULL,'1d401696-e6fa-4843-88ee-5a2df8f484ec','2016-03-09 23:47:02',NULL,NULL,1,44,44,''),(22,'2016-04-11 00:00:00',15,3000000,1.6,24,'FI',0,30,0,0,'Abono a credicheque en citi, (Credicheque por valor de 6.250.000 que se habia utilizado apra acabar de pagar a finesa)','97f27fc9-1061-4355-b689-206de7790f54','2016-04-11 08:38:30',NULL,NULL,14,1,NULL,''),(23,'2016-04-26 00:00:00',15,3000000,1.6,24,'FI',0,30,0,0,'Abono a credicheque en citi, (Credicheque por valor de 6.250.000 que se habia utilizado apra acabar de pagar a finesa) ','97f27fc9-1061-4355-b689-206de7790f54','2016-04-26 13:03:01',NULL,NULL,15,1,NULL,''),(24,'2016-04-27 00:00:00',14,18000000,1.6,48,'FI',0,30,0,0,'Para abonar a los 60.000.000 que se le debian a maxirepuestos. Se completaron 20.000.000 con plata de gustavo para un saldo de 40\'','97f27fc9-1061-4355-b689-206de7790f54','2016-04-27 19:24:48',NULL,NULL,16,1,NULL,''),(25,'2016-05-25 00:00:00',15,473000,1.6,24,'IM',0,30,0,0,'abono a doña gladys de los 3000.000 que presto','97f27fc9-1061-4355-b689-206de7790f54','2016-05-25 13:49:23','97f27fc9-1061-4355-b689-206de7790f54','2016-07-05 14:13:13',17,1,1,''),(26,'2016-07-30 00:00:00',14,810000,1.6,24,'FI',0,30,0,0,'para pagar a maxirepuestos. En realidad lo que se pago fue 3.000.000 pero solo se le presto 2.500.000 1.690.000FGR 810.000iCredit','97f27fc9-1061-4355-b689-206de7790f54','2016-08-03 12:49:09','97f27fc9-1061-4355-b689-206de7790f54','2016-08-03 12:51:42',18,1,NULL,''),(27,'2016-09-29 00:00:00',14,1253000,1.6,24,'FI',0,30,0,0,'Abono a maxirepuestos. FRG dio 2747000 y icredit 1253000','97f27fc9-1061-4355-b689-206de7790f54','2016-10-04 10:04:51','97f27fc9-1061-4355-b689-206de7790f54','2016-10-11 14:12:07',19,1,NULL,''),(28,'2016-11-30 00:00:00',14,1575000,1.6,36,'FI',0,30,0,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-12-05 09:44:12',NULL,NULL,20,1,NULL,'');
/*!40000 ALTER TABLE `credito` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cuota`
--

DROP TABLE IF EXISTS `cuota`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cuota` (
  `CuotaId` int(11) NOT NULL AUTO_INCREMENT,
  `CreditoId` int(11) NOT NULL,
  `Numero` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `AbonoCapital` double NOT NULL,
  `AbonoInteres` double NOT NULL,
  `Saldo` double NOT NULL,
  PRIMARY KEY (`CuotaId`),
  KEY `IX_CreditoId` (`CreditoId`),
  CONSTRAINT `FK_Cuota_Credito_CreditoId` FOREIGN KEY (`CreditoId`) REFERENCES `credito` (`CreditoId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=734 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cuota`
--

LOCK TABLES `cuota` WRITE;
/*!40000 ALTER TABLE `cuota` DISABLE KEYS */;
INSERT INTO `cuota` VALUES (397,17,1,'2016-03-11 00:00:00',76679.99290622947,15000,923320.0070937705),(398,17,2,'2016-04-11 00:00:00',77830.19279982291,13849.800106406557,845489.8142939477),(399,17,3,'2016-05-11 00:00:00',78997.64569182026,12682.347214409214,766492.1686021273),(400,17,4,'2016-06-11 00:00:00',80182.61037719756,11497.38252903191,686309.5582249297),(401,17,5,'2016-07-11 00:00:00',81385.34953285553,10294.643373373945,604924.2086920742),(402,17,6,'2016-08-11 00:00:00',82606.12977584836,9073.863130381113,522318.0789162258),(403,17,7,'2016-09-11 00:00:00',83845.22172248608,7834.771183743387,438472.85719373974),(404,17,8,'2016-10-11 00:00:00',85102.90004832337,6577.092857906096,353369.95714541635),(405,17,9,'2016-11-11 00:00:00',86379.44354904823,5300.549357181245,266990.51359636814),(406,17,10,'2016-12-11 00:00:00',87675.13520228394,4004.8577039455217,179315.3783940842),(407,17,11,'2017-01-11 00:00:00',88990.2622303182,2689.730675911263,90325.116163766),(408,17,12,'2017-02-11 00:00:00',90325.11616377298,1354.8767424564899,-0.000000006984919309616089),(409,18,1,'2016-02-03 00:00:00',66.6666666666666,6.666666666666667,1933.3333333333335),(410,18,2,'2016-02-04 00:00:00',66.6666666666666,6.666666666666667,1866.666666666667),(411,18,3,'2016-02-05 00:00:00',66.6666666666666,6.666666666666667,1800.0000000000005),(412,18,4,'2016-02-06 00:00:00',66.6666666666666,6.666666666666667,1733.333333333334),(413,18,5,'2016-02-07 00:00:00',66.6666666666666,6.666666666666667,1666.6666666666674),(414,18,6,'2016-02-08 00:00:00',66.6666666666666,6.666666666666667,1600.000000000001),(415,18,7,'2016-02-09 00:00:00',66.6666666666666,6.666666666666667,1533.3333333333344),(416,18,8,'2016-02-10 00:00:00',66.6666666666666,6.666666666666667,1466.6666666666679),(417,18,9,'2016-02-11 00:00:00',66.6666666666666,6.666666666666667,1400.0000000000014),(418,18,10,'2016-02-12 00:00:00',66.6666666666666,6.666666666666667,1333.3333333333348),(419,18,11,'2016-02-13 00:00:00',66.6666666666666,6.666666666666667,1266.6666666666683),(420,18,12,'2016-02-14 00:00:00',66.6666666666666,6.666666666666667,1200.0000000000018),(421,18,13,'2016-02-15 00:00:00',66.6666666666666,6.666666666666667,1133.3333333333353),(422,18,14,'2016-02-16 00:00:00',66.6666666666666,6.666666666666667,1066.6666666666688),(423,18,15,'2016-02-17 00:00:00',66.6666666666666,6.666666666666667,1000.0000000000022),(424,18,16,'2016-02-18 00:00:00',66.6666666666666,6.666666666666667,933.3333333333355),(425,18,17,'2016-02-19 00:00:00',66.6666666666666,6.666666666666667,866.6666666666689),(426,18,18,'2016-02-20 00:00:00',66.6666666666666,6.666666666666667,800.0000000000023),(427,18,19,'2016-02-21 00:00:00',66.6666666666666,6.666666666666667,733.3333333333356),(428,18,20,'2016-02-22 00:00:00',66.6666666666666,6.666666666666667,666.666666666669),(429,18,21,'2016-02-23 00:00:00',66.6666666666666,6.666666666666667,600.0000000000024),(430,18,22,'2016-02-24 00:00:00',66.6666666666666,6.666666666666667,533.3333333333358),(431,18,23,'2016-02-25 00:00:00',66.6666666666666,6.666666666666667,466.66666666666913),(432,18,24,'2016-02-26 00:00:00',66.6666666666666,6.666666666666667,400.0000000000025),(433,18,25,'2016-02-27 00:00:00',66.6666666666666,6.666666666666667,333.3333333333359),(434,18,26,'2016-02-28 00:00:00',66.6666666666666,6.666666666666667,266.66666666666924),(435,18,27,'2016-02-29 00:00:00',66.6666666666666,6.666666666666667,200.00000000000264),(436,18,28,'2016-03-01 00:00:00',66.6666666666666,6.666666666666667,133.33333333333604),(437,18,29,'2016-03-02 00:00:00',66.6666666666666,6.666666666666667,66.66666666666944),(438,18,30,'2016-03-03 00:00:00',66.6666666666666,6.666666666666667,0.0000000000028421709430404007),(439,19,1,'2016-02-16 00:00:00',1666.6666666666667,445.00000000000006,8333.333333333334),(440,19,2,'2016-03-02 00:00:00',1666.6666666666667,445.00000000000006,6666.666666666667),(441,19,3,'2016-03-17 00:00:00',1666.6666666666667,445.00000000000006,5000),(442,19,4,'2016-04-01 00:00:00',1666.6666666666667,445.00000000000006,3333.333333333333),(443,19,5,'2016-04-16 00:00:00',1666.6666666666667,445.00000000000006,1666.6666666666663),(444,19,6,'2016-05-01 00:00:00',1666.6666666666667,445.00000000000006,-0.0000000000004547473508864641),(445,20,1,'2016-04-14 00:00:00',83.33333333333333,25,916.6666666666666),(446,20,2,'2016-04-21 00:00:00',83.33333333333333,25,833.3333333333333),(447,20,3,'2016-04-28 00:00:00',83.33333333333333,25,749.9999999999999),(448,20,4,'2016-05-05 00:00:00',83.33333333333333,25,666.6666666666665),(449,20,5,'2016-05-12 00:00:00',83.33333333333333,25,583.3333333333331),(450,20,6,'2016-05-19 00:00:00',83.33333333333333,25,499.99999999999983),(451,20,7,'2016-05-26 00:00:00',83.33333333333333,25,416.6666666666665),(452,20,8,'2016-06-02 00:00:00',83.33333333333333,25,333.3333333333332),(453,20,9,'2016-06-09 00:00:00',83.33333333333333,25,249.9999999999999),(454,20,10,'2016-06-16 00:00:00',83.33333333333333,25,166.66666666666657),(455,20,11,'2016-06-23 00:00:00',83.33333333333333,25,83.33333333333324),(456,20,12,'2016-06-30 00:00:00',83.33333333333333,25,-0.00000000000008526512829121202),(457,21,1,'2016-03-09 00:00:00',1000000.0000000007,150000,-0.0000000006984919309616089),(458,22,1,'2016-05-11 00:00:00',103517.52272336697,48000,2896482.477276633),(459,22,2,'2016-06-11 00:00:00',105173.80308694084,46343.71963642613,2791308.6741896924),(460,22,3,'2016-07-11 00:00:00',106856.5839363319,44660.93878703508,2684452.0902533606),(461,22,4,'2016-08-11 00:00:00',108566.2892793132,42951.23344405377,2575885.8009740473),(462,22,5,'2016-09-11 00:00:00',110303.34990778222,41214.17281558476,2465582.451066265),(463,22,6,'2016-10-11 00:00:00',112068.20350630673,39449.31921706024,2353514.247559958),(464,22,7,'2016-11-11 00:00:00',113861.29476240763,37656.22796095933,2239652.9527975507),(465,22,8,'2016-12-11 00:00:00',115683.07547860616,35834.44724476081,2123969.877318945),(466,22,9,'2017-01-11 00:00:00',117534.00468626386,33983.51803710312,2006435.872632681),(467,22,10,'2017-02-11 00:00:00',119414.54876124408,32102.973962122895,1887021.3238714368),(468,22,11,'2017-03-11 00:00:00',121325.18154142398,30192.34118194299,1765696.1423300127),(469,22,12,'2017-04-11 00:00:00',123266.38444608677,28251.138277280206,1642429.757883926),(470,22,13,'2017-05-11 00:00:00',125238.64659722416,26278.876126142815,1517191.1112867019),(471,22,14,'2017-06-11 00:00:00',127242.46494277974,24275.05778058723,1389948.6463439222),(472,22,15,'2017-07-11 00:00:00',129278.34438186421,22239.178341502757,1260670.301962058),(473,22,16,'2017-08-11 00:00:00',131346.79789197404,20170.72483139293,1129323.504070084),(474,22,17,'2017-09-11 00:00:00',133448.34665824563,18069.176065121344,995875.1574118384),(475,22,18,'2017-10-11 00:00:00',135583.52020477757,15934.002518589416,860291.6372070608),(476,22,19,'2017-11-11 00:00:00',137752.856528054,13764.666195312973,722538.7806790068),(477,22,20,'2017-12-11 00:00:00',139956.90223250285,11560.62049086411,582581.878446504),(478,22,21,'2018-01-11 00:00:00',142196.2126682229,9321.310055144064,440385.6657782811),(479,22,22,'2018-02-11 00:00:00',144471.35207091447,7046.170652452498,295914.31370736664),(480,22,23,'2018-03-11 00:00:00',146782.89370404911,4734.629019317867,149131.42000331753),(481,22,24,'2018-04-11 00:00:00',149131.4200033139,2386.1027200530807,0.000000003637978807091713),(482,23,1,'2016-05-26 00:00:00',103517.52272336697,48000,2896482.477276633),(483,23,2,'2016-06-26 00:00:00',105173.80308694084,46343.71963642613,2791308.6741896924),(484,23,3,'2016-07-26 00:00:00',106856.5839363319,44660.93878703508,2684452.0902533606),(485,23,4,'2016-08-26 00:00:00',108566.2892793132,42951.23344405377,2575885.8009740473),(486,23,5,'2016-09-26 00:00:00',110303.34990778222,41214.17281558476,2465582.451066265),(487,23,6,'2016-10-26 00:00:00',112068.20350630673,39449.31921706024,2353514.247559958),(488,23,7,'2016-11-26 00:00:00',113861.29476240763,37656.22796095933,2239652.9527975507),(489,23,8,'2016-12-26 00:00:00',115683.07547860616,35834.44724476081,2123969.877318945),(490,23,9,'2017-01-26 00:00:00',117534.00468626386,33983.51803710312,2006435.872632681),(491,23,10,'2017-02-26 00:00:00',119414.54876124408,32102.973962122895,1887021.3238714368),(492,23,11,'2017-03-26 00:00:00',121325.18154142398,30192.34118194299,1765696.1423300127),(493,23,12,'2017-04-26 00:00:00',123266.38444608677,28251.138277280206,1642429.757883926),(494,23,13,'2017-05-26 00:00:00',125238.64659722416,26278.876126142815,1517191.1112867019),(495,23,14,'2017-06-26 00:00:00',127242.46494277974,24275.05778058723,1389948.6463439222),(496,23,15,'2017-07-26 00:00:00',129278.34438186421,22239.178341502757,1260670.301962058),(497,23,16,'2017-08-26 00:00:00',131346.79789197404,20170.72483139293,1129323.504070084),(498,23,17,'2017-09-26 00:00:00',133448.34665824563,18069.176065121344,995875.1574118384),(499,23,18,'2017-10-26 00:00:00',135583.52020477757,15934.002518589416,860291.6372070608),(500,23,19,'2017-11-26 00:00:00',137752.856528054,13764.666195312973,722538.7806790068),(501,23,20,'2017-12-26 00:00:00',139956.90223250285,11560.62049086411,582581.878446504),(502,23,21,'2018-01-26 00:00:00',142196.2126682229,9321.310055144064,440385.6657782811),(503,23,22,'2018-02-26 00:00:00',144471.35207091447,7046.170652452498,295914.31370736664),(504,23,23,'2018-03-26 00:00:00',146782.89370404911,4734.629019317867,149131.42000331753),(505,23,24,'2018-04-26 00:00:00',149131.4200033139,2386.1027200530807,0.000000003637978807091713),(506,24,1,'2016-05-27 00:00:00',252103.6469794711,288000,17747896.35302053),(507,24,2,'2016-06-27 00:00:00',256137.30533114262,283966.34164832847,17491759.047689386),(508,24,3,'2016-07-27 00:00:00',260235.50221644092,279868.14476303017,17231523.545472946),(509,24,4,'2016-08-27 00:00:00',264399.2702519039,275704.37672756717,16967124.275221042),(510,24,5,'2016-09-27 00:00:00',268629.6585759344,271473.98840353667,16698494.616645107),(511,24,6,'2016-10-27 00:00:00',272927.73311314936,267175.91386632173,16425566.883531958),(512,24,7,'2016-11-27 00:00:00',277294.57684295974,262809.07013651135,16148272.306688998),(513,24,8,'2016-12-27 00:00:00',281731.29007244715,258372.35690702396,15866541.016616551),(514,24,9,'2017-01-27 00:00:00',286238.99071360624,253864.65626586482,15580302.025902946),(515,24,10,'2017-02-27 00:00:00',290818.814565024,249284.83241444713,15289483.211337922),(516,24,11,'2017-03-27 00:00:00',295471.9155980643,244631.73138140675,14994011.295739857),(517,24,12,'2017-04-27 00:00:00',300199.46624763333,239904.18073183773,14693811.829492224),(518,24,13,'2017-05-27 00:00:00',305002.6577075955,235100.9892718756,14388809.171784628),(519,24,14,'2017-06-27 00:00:00',309882.700230917,230220.94674855407,14078926.471553711),(520,24,15,'2017-07-27 00:00:00',314840.82343461167,225262.8235448594,13764085.6481191),(521,24,16,'2017-08-27 00:00:00',319878.2766095655,220225.3703699056,13444207.371509533),(522,24,17,'2017-09-27 00:00:00',324996.3290353186,215107.31794415254,13119211.042474214),(523,24,18,'2017-10-27 00:00:00',330196.2702998837,209907.37667958744,12789014.77217433),(524,24,19,'2017-11-27 00:00:00',335479.4106246818,204624.2363547893,12453535.36154965),(525,24,20,'2017-12-27 00:00:00',340847.0811946767,199256.5657847944,12112688.280354973),(526,24,21,'2018-01-27 00:00:00',346300.6344937915,193803.01248567956,11766387.645861182),(527,24,22,'2018-02-27 00:00:00',351841.4446456921,188262.20233377893,11414546.20121549),(528,24,23,'2018-03-27 00:00:00',357470.90776002326,182632.73921944786,11057075.293455467),(529,24,24,'2018-04-27 00:00:00',363190.4422841836,176913.20469528748,10693884.851171283),(530,24,25,'2018-05-27 00:00:00',369001.4893607305,171102.15761874054,10324883.361810552),(531,24,26,'2018-06-27 00:00:00',374905.51319050224,165198.13378896884,9949977.84862005),(532,24,27,'2018-07-27 00:00:00',380904.00140155025,159199.6455779208,9569073.847218499),(533,24,28,'2018-08-27 00:00:00',386998.46542397514,153105.18155549598,9182075.381794523),(534,24,29,'2018-09-27 00:00:00',393190.4408707587,146913.2061087124,8788884.940923765),(535,24,30,'2018-10-27 00:00:00',399481.4879246908,140622.15905478023,8389403.452999074),(536,24,31,'2018-11-27 00:00:00',405873.1917314859,134230.4552479852,7983530.2612675885),(537,24,32,'2018-12-27 00:00:00',412367.1627991897,127736.48418028142,7571163.098468399),(538,24,33,'2019-01-27 00:00:00',418965.0374039767,121138.60957549438,7152198.061064422),(539,24,34,'2019-02-27 00:00:00',425668.4780024403,114435.16897703076,6726529.583061982),(540,24,35,'2019-03-27 00:00:00',432479.17365047935,107624.47332899172,6294050.409411503),(541,24,36,'2019-04-27 00:00:00',439398.84042888705,100704.80655058405,5854651.568982616),(542,24,37,'2019-05-27 00:00:00',446429.22187574924,93674.42510372186,5408222.347106867),(543,24,38,'2019-06-27 00:00:00',453572.08942576125,86531.55755370986,4954650.257681105),(544,24,39,'2019-07-27 00:00:00',460829.24285657343,79274.40412289769,4493821.014824532),(545,24,40,'2019-08-27 00:00:00',468202.5107422786,71901.1362371925,4025618.504082253),(546,24,41,'2019-09-27 00:00:00',475693.75091415504,64409.896065316054,3549924.753168098),(547,24,42,'2019-10-27 00:00:00',483304.8509287815,56798.79605068957,3066619.9022393166),(548,24,43,'2019-11-27 00:00:00',491037.72854364204,49065.918435829066,2575582.1736956746),(549,24,44,'2019-12-27 00:00:00',498894.3322003403,41209.31477913079,2076687.8414953344),(550,24,45,'2020-01-27 00:00:00',506876.6415155457,33227.00546392535,1569811.1999797886),(551,24,46,'2020-02-27 00:00:00',514986.6677797945,25116.979199676618,1054824.5321999942),(552,24,47,'2020-03-27 00:00:00',523226.4544642712,16877.192515199906,531598.077735723),(553,24,48,'2020-04-27 00:00:00',531598.0777356995,8505.569243771568,0.000000023515895009040833),(578,25,1,'2016-06-25 00:00:00',19708.333333333332,7568,453291.6666666667),(579,25,2,'2016-07-25 00:00:00',19708.333333333332,7568,433583.3333333334),(580,25,3,'2016-08-25 00:00:00',19708.333333333332,7568,413875.00000000006),(581,25,4,'2016-09-25 00:00:00',19708.333333333332,7568,394166.66666666674),(582,25,5,'2016-10-25 00:00:00',19708.333333333332,7568,374458.33333333343),(583,25,6,'2016-11-25 00:00:00',19708.333333333332,7568,354750.0000000001),(584,25,7,'2016-12-25 00:00:00',19708.333333333332,7568,335041.6666666668),(585,25,8,'2017-01-25 00:00:00',19708.333333333332,7568,315333.3333333335),(586,25,9,'2017-02-25 00:00:00',19708.333333333332,7568,295625.0000000002),(587,25,10,'2017-03-25 00:00:00',19708.333333333332,7568,275916.66666666686),(588,25,11,'2017-04-25 00:00:00',19708.333333333332,7568,256208.33333333352),(589,25,12,'2017-05-25 00:00:00',19708.333333333332,7568,236500.00000000017),(590,25,13,'2017-06-25 00:00:00',19708.333333333332,7568,216791.66666666683),(591,25,14,'2017-07-25 00:00:00',19708.333333333332,7568,197083.3333333335),(592,25,15,'2017-08-25 00:00:00',19708.333333333332,7568,177375.00000000015),(593,25,16,'2017-09-25 00:00:00',19708.333333333332,7568,157666.6666666668),(594,25,17,'2017-10-25 00:00:00',19708.333333333332,7568,137958.33333333346),(595,25,18,'2017-11-25 00:00:00',19708.333333333332,7568,118250.00000000013),(596,25,19,'2017-12-25 00:00:00',19708.333333333332,7568,98541.6666666668),(597,25,20,'2018-01-25 00:00:00',19708.333333333332,7568,78833.33333333347),(598,25,21,'2018-02-25 00:00:00',19708.333333333332,7568,59125.000000000146),(599,25,22,'2018-03-25 00:00:00',19708.333333333332,7568,39416.66666666682),(600,25,23,'2018-04-25 00:00:00',19708.333333333332,7568,19708.333333333485),(601,25,24,'2018-05-25 00:00:00',19708.333333333332,7568,0.00000000015279510989785194),(626,26,1,'2016-08-30 00:00:00',27949.731135309084,12960,782050.2688646909),(627,26,2,'2016-09-30 00:00:00',28396.92683347403,12512.804301835055,753653.3420312168),(628,26,3,'2016-10-30 00:00:00',28851.277662809614,12058.45347249947,724802.0643684072),(629,26,4,'2016-11-30 00:00:00',29312.89810541457,11596.833029894517,695489.1662629927),(630,26,5,'2016-12-30 00:00:00',29781.904475101204,11127.826660207882,665707.2617878915),(631,26,6,'2017-01-30 00:00:00',30258.41494670282,10651.316188606264,635448.8468411887),(632,26,7,'2017-02-28 00:00:00',30742.549585850065,10167.18154945902,604706.2972553386),(633,26,8,'2017-03-30 00:00:00',31234.430379223668,9675.300756085418,573471.866876115),(634,26,9,'2017-04-30 00:00:00',31734.181265291245,9175.54987001784,541737.6856108237),(635,26,10,'2017-05-30 00:00:00',32241.928165535905,8667.80296977318,509495.7574452878),(636,26,11,'2017-06-30 00:00:00',32757.79901618448,8151.9321191246045,476737.9584291033),(637,26,12,'2017-07-30 00:00:00',33281.92380044343,7627.8073348656535,443456.0346286599),(638,26,13,'2017-08-30 00:00:00',33814.43458125053,7095.296554058558,409641.60004740936),(639,26,14,'2017-09-30 00:00:00',34355.46553455053,6554.26560075855,375286.1345128588),(640,26,15,'2017-10-30 00:00:00',34905.15298310334,6004.578152205741,340380.98152975546),(641,26,16,'2017-11-30 00:00:00',35463.635430833,5446.095704476087,304917.3460989225),(642,26,17,'2017-12-30 00:00:00',36031.05359772632,4878.67753758276,268886.29250119615),(643,26,18,'2018-01-30 00:00:00',36607.550455289944,4302.180680019139,232278.7420459062),(644,26,19,'2018-02-28 00:00:00',37193.271262574584,3716.459872734499,195085.4707833316),(645,26,20,'2018-03-30 00:00:00',37788.36360277578,3121.3675325333056,157297.10718055582),(646,26,21,'2018-04-30 00:00:00',38392.97742042019,2516.7537148888932,118904.12976013563),(647,26,22,'2018-05-30 00:00:00',39007.265059146914,1902.46607616217,79896.86470098872),(648,26,23,'2018-06-30 00:00:00',39631.381300093264,1278.3498352158197,40265.48340089546),(649,26,24,'2018-07-30 00:00:00',40265.48340089476,644.2477344143273,0.0000000006984919309616089),(674,27,1,'2016-10-29 00:00:00',43235.818657459604,20048,1209764.1813425403),(675,27,2,'2016-11-29 00:00:00',43927.59175597896,19356.226901480644,1165836.5895865613),(676,27,3,'2016-12-29 00:00:00',44630.43322407462,18653.38543338498,1121206.1563624868),(677,27,4,'2017-01-29 00:00:00',45344.52015565982,17939.29850179979,1075861.636206827),(678,27,5,'2017-02-28 00:00:00',46070.03247815037,17213.78617930923,1029791.6037286766),(679,27,6,'2017-03-29 00:00:00',46807.152997800775,16476.665659658825,982984.4507308758),(680,27,7,'2017-04-29 00:00:00',47556.06744576559,15727.751211694014,935428.3832851102),(681,27,8,'2017-05-29 00:00:00',48316.96452489784,14966.854132561764,887111.4187602124),(682,27,9,'2017-06-29 00:00:00',49090.035957296204,14193.782700163398,838021.3828029162),(683,27,10,'2017-07-29 00:00:00',49875.47653261294,13408.34212484666,788145.9062703033),(684,27,11,'2017-08-29 00:00:00',50673.48415713475,12610.334500324852,737472.4221131685),(685,27,12,'2017-09-29 00:00:00',51484.259903648905,11799.558753810697,685988.1622095197),(686,27,13,'2017-10-29 00:00:00',52308.008062107285,10975.810595352315,633680.1541474124),(687,27,14,'2017-11-29 00:00:00',53144.936191101006,10138.882466358598,580535.2179563114),(688,27,15,'2017-12-29 00:00:00',53995.25517015862,9288.563487300982,526539.9627861528),(689,27,16,'2018-01-29 00:00:00',54859.17925288116,8424.639404578445,471680.7835332717),(690,27,17,'2018-02-28 00:00:00',55736.92612092726,7546.892536532348,415943.8574123444),(691,27,18,'2018-03-29 00:00:00',56628.7169388621,6655.101718597511,359315.1404734823),(692,27,19,'2018-04-29 00:00:00',57534.77640988389,5749.042247575717,301780.36406359845),(693,27,20,'2018-05-29 00:00:00',58455.33283244203,4828.485825017575,243325.03123115643),(694,27,21,'2018-06-29 00:00:00',59390.6181577611,3893.200499698503,183934.41307339532),(695,27,22,'2018-07-29 00:00:00',60340.86804828528,2942.950609174325,123593.54502511004),(696,27,23,'2018-08-29 00:00:00',61306.321937057844,1977.4967204017607,62287.2230880522),(697,27,24,'2018-09-29 00:00:00',62287.22308805077,996.5955694088352,0.0000000014260876923799515),(698,28,1,'2016-12-30 00:00:00',32692.618597802175,25200,1542307.3814021978),(699,28,2,'2017-01-30 00:00:00',33215.70049536701,24676.918102435164,1509091.6809068308),(700,28,3,'2017-02-28 00:00:00',33747.15170329288,24145.466894509293,1475344.5292035379),(701,28,4,'2017-03-30 00:00:00',34287.106130545566,23605.512467256605,1441057.4230729924),(702,28,5,'2017-04-30 00:00:00',34835.6998286343,23056.918769167878,1406221.723244358),(703,28,6,'2017-05-30 00:00:00',35393.071025892445,22499.54757190973,1370828.6522184657),(704,28,7,'2017-06-30 00:00:00',35959.36016230672,21933.258435495452,1334869.2920561589),(705,28,8,'2017-07-30 00:00:00',36534.70992490363,21357.908672898542,1298334.5821312552),(706,28,9,'2017-08-30 00:00:00',37119.26528370209,20773.353314100084,1261215.316847553),(707,28,10,'2017-09-30 00:00:00',37713.17352824133,20179.44506956085,1223502.1433193118),(708,28,11,'2017-10-30 00:00:00',38316.584304693184,19576.034293108987,1185185.5590146186),(709,28,12,'2017-11-30 00:00:00',38929.64965356828,18962.968944233897,1146255.9093610505),(710,28,13,'2017-12-30 00:00:00',39552.52404802537,18340.094549776808,1106703.3853130252),(711,28,14,'2018-01-30 00:00:00',40185.36443279377,17707.254165008402,1066518.0208802314),(712,28,15,'2018-02-28 00:00:00',40828.33026371847,17064.288334083703,1025689.6906165129),(713,28,16,'2018-03-30 00:00:00',41481.58354793797,16411.035049864207,984208.107068575),(714,28,17,'2018-04-30 00:00:00',42145.288884704976,15747.3297130972,942062.81818387),(715,28,18,'2018-05-30 00:00:00',42819.61350686025,15073.00509094192,899243.2046770098),(716,28,19,'2018-06-30 00:00:00',43504.727322970015,14387.891274832158,855738.4773540398),(717,28,20,'2018-07-30 00:00:00',44200.80296013754,13691.815637664637,811537.6743939023),(718,28,21,'2018-08-30 00:00:00',44908.01580749974,12984.602790302437,766629.6585864025),(719,28,22,'2018-09-30 00:00:00',45626.544060419736,12266.074537382441,721003.1145259828),(720,28,23,'2018-10-30 00:00:00',46356.56876538645,11536.049832415725,674646.5457605963),(721,28,24,'2018-11-30 00:00:00',47098.27386563263,10794.34473216954,627548.2718949637),(722,28,25,'2018-12-30 00:00:00',47851.846247482754,10040.77235031942,579696.4256474809),(723,28,26,'2019-01-30 00:00:00',48617.47578744248,9275.142810359694,531078.9498600384),(724,28,27,'2019-02-28 00:00:00',49395.35540004156,8497.263197760614,481683.5944599968),(725,28,28,'2019-03-30 00:00:00',50185.68108644223,7706.937511359949,431497.9133735546),(726,28,29,'2019-04-30 00:00:00',50988.6519838253,6903.966613976873,380509.2613897293),(727,28,30,'2019-05-30 00:00:00',51804.47041556651,6088.148182235668,328704.79097416275),(728,28,31,'2019-06-30 00:00:00',52633.34194221557,5259.276655586604,276071.4490319472),(729,28,32,'2019-07-30 00:00:00',53475.47541329102,4417.143184511155,222595.97361865616),(730,28,33,'2019-08-30 00:00:00',54331.08301990368,3561.5355778984986,168264.89059875248),(731,28,34,'2019-09-30 00:00:00',55200.380348222134,2692.23824958004,113064.51025053035),(732,28,35,'2019-10-30 00:00:00',56083.58643379369,1809.0321640084856,56980.923816736664),(733,28,36,'2019-11-30 00:00:00',56980.92381673439,911.6947810677866,0.0000000022773747332394123);
/*!40000 ALTER TABLE `cuota` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `divisioncredito`
--

DROP TABLE IF EXISTS `divisioncredito`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `divisioncredito` (
  `DivisionCreditoId` int(11) NOT NULL,
  `Nombre` varchar(100) DEFAULT NULL,
  `Estado` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`DivisionCreditoId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `divisioncredito`
--

LOCK TABLES `divisioncredito` WRITE;
/*!40000 ALTER TABLE `divisioncredito` DISABLE KEYS */;
INSERT INTO `divisioncredito` VALUES (1,'Dias',1),(7,'Semanas',1),(15,'Quincenas',1),(30,'Meses',1);
/*!40000 ALTER TABLE `divisioncredito` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `empresa`
--

DROP TABLE IF EXISTS `empresa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `empresa` (
  `EmpresaId` int(11) NOT NULL AUTO_INCREMENT,
  `Nit` varchar(30) DEFAULT NULL,
  `Nombre` varchar(200) NOT NULL DEFAULT '',
  `Direccion` varchar(500) DEFAULT '',
  `Telefono` varchar(100) DEFAULT '',
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  `CreadoPor` varchar(128) DEFAULT '',
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT '',
  `FechaModificacion` datetime DEFAULT NULL,
  `LogoUrl` varchar(500) DEFAULT NULL,
  `EmpEmail` varchar(200) NOT NULL,
  `EmpAnioActual` int(11) DEFAULT NULL,
  `EmpMesActual` int(11) DEFAULT NULL,
  PRIMARY KEY (`EmpresaId`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `empresa`
--

LOCK TABLES `empresa` WRITE;
/*!40000 ALTER TABLE `empresa` DISABLE KEYS */;
INSERT INTO `empresa` VALUES (1,'75088866','Adisoft','Av 19 nr 7b 140','300 658 72 32','',NULL,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-11-03 07:45:43','LogoUrl1.bmp','sisadisoft@gmail.com',2016,11),(36,'','FGR',NULL,NULL,'',NULL,'2016-02-11 14:09:16',NULL,NULL,NULL,'william.gustavo@gmail.com',2016,2),(37,'','liliana',NULL,NULL,'',NULL,'2016-02-11 14:32:58',NULL,NULL,NULL,'lilalg16@hotmail.com',2016,2),(38,'','horka2000@hotmail.com',NULL,NULL,'',NULL,'2016-02-12 16:00:48',NULL,NULL,NULL,'horka2000@hotmail.com',2016,2),(39,'','asas',NULL,NULL,'',NULL,'2016-02-19 20:00:12',NULL,NULL,NULL,'saske_r@hotmail.com',2016,2),(40,'','Inversiones Padilla y A ',NULL,NULL,'',NULL,'2016-02-21 17:34:42',NULL,NULL,NULL,'vpadillaramirez@gmail.com',2016,2),(41,'','SERVI PRESTAMOS',NULL,NULL,'',NULL,'2016-03-06 20:12:43',NULL,NULL,NULL,'100211829PACF@gmail.com',2016,3),(42,NULL,'SERVI PRESTAMOS','RANCHITO/LA VEGA','849 262 5308','',NULL,'2016-03-07 00:22:59','7b31884f-1895-4394-bcaa-55a0cbced37e','2016-03-07 00:32:08',NULL,'drfernandez18@gmail.com',2016,3),(43,'','Credi GIO',NULL,NULL,'',NULL,'2016-03-09 23:21:07',NULL,NULL,NULL,'geovanni.almiron@mail.com',2016,3),(44,'','Credi GIO',NULL,NULL,'',NULL,'2016-03-09 23:42:35',NULL,NULL,NULL,'geovanni.almiron@tigo.net.py',2016,3);
/*!40000 ALTER TABLE `empresa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `loghome`
--

DROP TABLE IF EXISTS `loghome`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `loghome` (
  `Id` bigint(11) NOT NULL AUTO_INCREMENT,
  `Visitantes` bigint(11) DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `loghome`
--

LOCK TABLES `loghome` WRITE;
/*!40000 ALTER TABLE `loghome` DISABLE KEYS */;
INSERT INTO `loghome` VALUES (1,167);
/*!40000 ALTER TABLE `loghome` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `paramcorreo`
--

DROP TABLE IF EXISTS `paramcorreo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `paramcorreo` (
  `ParamCorreoId` int(11) NOT NULL AUTO_INCREMENT,
  `Servidor` varchar(100) NOT NULL,
  `Puerto` int(11) NOT NULL,
  `Usuario` varchar(100) DEFAULT NULL,
  `Password` varchar(100) DEFAULT NULL,
  `EmpresaId` int(11) NOT NULL,
  `Estado` tinyint(1) NOT NULL,
  `CreadoPor` longtext,
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` longtext,
  `FechaModificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`ParamCorreoId`),
  KEY `IX_EmpresaId` (`EmpresaId`),
  CONSTRAINT `FK_ParamCorreo_Empresa_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `empresa` (`EmpresaId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `paramcorreo`
--

LOCK TABLES `paramcorreo` WRITE;
/*!40000 ALTER TABLE `paramcorreo` DISABLE KEYS */;
INSERT INTO `paramcorreo` VALUES (3,'smtp.gmail.com',587,'sisadisoft@gmail.com','wg196507',1,0,'97f27fc9-1061-4355-b689-206de7790f54','2016-01-28 15:10:36','97f27fc9-1061-4355-b689-206de7790f54','2016-02-10 08:14:16');
/*!40000 ALTER TABLE `paramcorreo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `retiro`
--

DROP TABLE IF EXISTS `retiro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `retiro` (
  `RetiroId` int(11) NOT NULL AUTO_INCREMENT,
  `Valor` double NOT NULL,
  `Fecha` datetime NOT NULL,
  `SocioId` int(11) NOT NULL,
  `ConceptoRetiroId` int(11) NOT NULL,
  `Observacion` text,
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  `CreadoPor` varchar(128) DEFAULT '',
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT '',
  `FechaModificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`RetiroId`),
  KEY `IX_SocioId` (`SocioId`),
  KEY `IX_ConceptoRetiroId` (`ConceptoRetiroId`),
  CONSTRAINT `FK_Retiro_ConceptoRetiro_ConceptoRetiroId` FOREIGN KEY (`ConceptoRetiroId`) REFERENCES `conceptoretiro` (`ConceptoRetiroId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FK_Retiro_Socio_SocioId` FOREIGN KEY (`SocioId`) REFERENCES `socio` (`SocioId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retiro`
--

LOCK TABLES `retiro` WRITE;
/*!40000 ALTER TABLE `retiro` DISABLE KEYS */;
INSERT INTO `retiro` VALUES (9,1000000,'2016-02-11 00:00:00',6,33,NULL,'','6efe93b6-f004-4f42-956f-35013aad9140','2016-02-11 14:23:14',NULL,NULL),(10,5000,'2016-02-11 00:00:00',7,34,NULL,'','67801670-1198-46d4-b939-f7522f92a5ae','2016-02-11 14:38:04',NULL,NULL),(11,580000,'2016-04-13 00:00:00',5,4,'pago tarjeta de credito','','97f27fc9-1061-4355-b689-206de7790f54','2016-04-13 12:45:23',NULL,NULL),(12,150000,'2016-04-13 00:00:00',5,4,'completar pago pasaporte','','97f27fc9-1061-4355-b689-206de7790f54','2016-04-13 12:45:46',NULL,NULL),(13,14000,'2016-05-16 00:00:00',5,4,'pago cuoto manajo tarjeta de credito','','97f27fc9-1061-4355-b689-206de7790f54','2016-06-14 12:15:23',NULL,NULL),(14,20000,'2016-06-20 00:00:00',5,4,'cuota manejo tarjeta de credito','','97f27fc9-1061-4355-b689-206de7790f54','2016-06-20 09:13:09',NULL,NULL),(15,200000,'2016-05-25 00:00:00',5,4,'para comprar botellas de wisky y ultimos gastos del viaje a chile','','97f27fc9-1061-4355-b689-206de7790f54','2016-07-05 14:03:33',NULL,NULL),(16,100000,'2016-07-06 00:00:00',5,4,'se sacan para pagar plata que debo a fgr','','97f27fc9-1061-4355-b689-206de7790f54','2016-07-06 10:51:47',NULL,NULL),(17,451019.37,'2016-07-07 00:00:00',5,4,'para pagar deudas en colombia, despues de esto quedo un saldo por pagar de 115.000 a fgr 336.000 a citibank','','97f27fc9-1061-4355-b689-206de7790f54','2016-07-07 17:06:13',NULL,NULL),(18,60415,'2016-08-03 00:00:00',5,4,'para pago tarjeta credito citi','','97f27fc9-1061-4355-b689-206de7790f54','2016-08-03 12:55:19',NULL,NULL),(19,300000,'2016-09-07 00:00:00',5,4,'pago declaracion de renta 2015','','97f27fc9-1061-4355-b689-206de7790f54','2016-09-07 16:19:35',NULL,NULL),(20,300000,'2016-09-07 00:00:00',5,4,'pago credito tax la feria credito william','','97f27fc9-1061-4355-b689-206de7790f54','2016-09-07 16:20:34',NULL,NULL),(21,112000,'2016-09-07 00:00:00',5,4,'pago pension agosto 2016. (Es posible que sea la pension de septiembre no estoy seguro)','','97f27fc9-1061-4355-b689-206de7790f54','2016-09-07 16:21:37',NULL,NULL),(22,150000,'2016-09-07 00:00:00',5,4,'pago cuota apto chimenea','','97f27fc9-1061-4355-b689-206de7790f54','2016-09-07 16:22:49',NULL,NULL),(23,49324.76,'2016-09-07 00:00:00',5,4,'otros gastos ','','97f27fc9-1061-4355-b689-206de7790f54','2016-09-07 16:24:13',NULL,NULL),(24,100000,'2016-09-16 00:00:00',5,4,'Pago tarjeta de credito Citi Visa','','97f27fc9-1061-4355-b689-206de7790f54','2016-09-16 15:04:52',NULL,NULL),(25,50000,'2016-09-29 00:00:00',5,4,'para regalar plat a la abuelita','','97f27fc9-1061-4355-b689-206de7790f54','2016-09-29 07:37:06',NULL,NULL),(26,8324.75,'2016-10-11 00:00:00',5,42,'ajuste','','97f27fc9-1061-4355-b689-206de7790f54','2016-10-11 14:14:52',NULL,NULL);
/*!40000 ALTER TABLE `retiro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `retirointeres`
--

DROP TABLE IF EXISTS `retirointeres`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `retirointeres` (
  `RetiroInteresId` int(11) NOT NULL AUTO_INCREMENT,
  `Valor` double NOT NULL,
  `Fecha` datetime NOT NULL,
  `EmpresaId` int(11) NOT NULL,
  `Observacion` text,
  `CreadoPor` varchar(128) DEFAULT NULL,
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT NULL,
  `FechaModificacion` datetime DEFAULT NULL,
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  `RetiroInteresNro` int(11) DEFAULT NULL,
  PRIMARY KEY (`RetiroInteresId`),
  KEY `IX_EmpresaId` (`EmpresaId`),
  CONSTRAINT `FK_RetiroInteres_Empresa_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `empresa` (`EmpresaId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retirointeres`
--

LOCK TABLES `retirointeres` WRITE;
/*!40000 ALTER TABLE `retirointeres` DISABLE KEYS */;
INSERT INTO `retirointeres` VALUES (3,3000,'2016-02-10 00:00:00',39,NULL,'db856a4c-88dd-4888-8eb6-59171ad9ac17','2016-02-19 20:07:25',NULL,NULL,'',1),(4,7000,'2016-05-16 00:00:00',1,'se saca esta utilidad para pagar cuota de manejo de tarjeta de credito. Deberian ser 14000 pero habian 7000 de mas en la cuent de ahorros','97f27fc9-1061-4355-b689-206de7790f54','2016-05-22 09:38:02',NULL,NULL,'',3),(5,57000,'2016-05-23 00:00:00',1,'para ajustar descuadre en fgr,','97f27fc9-1061-4355-b689-206de7790f54','2016-05-23 15:17:14',NULL,NULL,'',4),(6,100000,'2016-05-24 00:00:00',1,'pa gastar','97f27fc9-1061-4355-b689-206de7790f54','2016-05-24 15:39:01',NULL,NULL,'',5),(7,312687.44,'2016-05-25 00:00:00',1,'utilidad del mes de mayo. Esto se aporta de nuevo','97f27fc9-1061-4355-b689-206de7790f54','2016-05-25 14:04:13',NULL,NULL,'',6),(8,291534.34,'2016-07-06 00:00:00',1,'se retiran para acabar de pagar gastos viaje chile','97f27fc9-1061-4355-b689-206de7790f54','2016-07-06 09:01:59',NULL,NULL,'',7),(9,376758.02,'2016-08-03 00:00:00',1,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-08-03 12:37:09',NULL,NULL,'',8),(10,382134.84,'2016-09-07 00:00:00',1,'retiro utilidades mes agosto 2016','97f27fc9-1061-4355-b689-206de7790f54','2016-09-07 16:17:09',NULL,NULL,'',9),(11,373983.14,'2016-10-04 00:00:00',1,'retiro utilidades septiembre 2016',NULL,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-10-04 10:32:13','',10);
/*!40000 ALTER TABLE `retirointeres` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `socio`
--

DROP TABLE IF EXISTS `socio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `socio` (
  `SocioId` int(11) NOT NULL AUTO_INCREMENT,
  `Nit` varchar(100) NOT NULL DEFAULT '',
  `Nombre` varchar(100) NOT NULL,
  `EmpresaId` int(11) NOT NULL,
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  `CreadoPor` varchar(128) DEFAULT '',
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT '',
  `FechaModificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`SocioId`),
  KEY `IX_EmpresaId` (`EmpresaId`),
  CONSTRAINT `FK_Socio_Empresa_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `empresa` (`EmpresaId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `socio`
--

LOCK TABLES `socio` WRITE;
/*!40000 ALTER TABLE `socio` DISABLE KEYS */;
INSERT INTO `socio` VALUES (5,'75088866','William Gustavo Giraldo Restrepo',1,'','97f27fc9-1061-4355-b689-206de7790f54','2016-02-01 13:45:00','97f27fc9-1061-4355-b689-206de7790f54','2016-04-08 09:51:56'),(6,'1311968','Frg',36,'','6efe93b6-f004-4f42-956f-35013aad9140','2016-02-11 14:15:35',NULL,NULL),(7,'12345','lilia',37,'','67801670-1198-46d4-b939-f7522f92a5ae','2016-02-11 14:36:15',NULL,NULL);
/*!40000 ALTER TABLE `socio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tipocuota`
--

DROP TABLE IF EXISTS `tipocuota`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tipocuota` (
  `TipoCuotaId` varchar(128) NOT NULL,
  `Nombre` varchar(100) DEFAULT NULL,
  `Estado` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`TipoCuotaId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipocuota`
--

LOCK TABLES `tipocuota` WRITE;
/*!40000 ALTER TABLE `tipocuota` DISABLE KEYS */;
INSERT INTO `tipocuota` VALUES ('FI','CUOTA FIJA CALCULANDO INTERESES SOBRE SALDO',1),('IM','CUOTA FIJA CALCULANDO INTERESES POR MES',1),('VA','CUOTA VARIABLE CALCULANDO INTERESES SOBRE SALDO',1);
/*!40000 ALTER TABLE `tipocuota` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuario` (
  `UsuarioId` int(11) NOT NULL AUTO_INCREMENT,
  `UsuDocumento` varchar(100) DEFAULT NULL,
  `UsuNombre` varchar(255) DEFAULT NULL,
  `UsuTelefono` varchar(255) DEFAULT NULL,
  `aspnetusersId` varchar(128) NOT NULL DEFAULT '',
  `EmpresaId` int(11) NOT NULL,
  `CreadoPor` varchar(128) DEFAULT NULL,
  `FechaCreacion` datetime DEFAULT NULL,
  `ModificadoPor` varchar(128) DEFAULT NULL,
  `FechaModificacion` datetime DEFAULT NULL,
  `Estado` bit(1) NOT NULL DEFAULT b'1',
  `UsuEmail` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`UsuarioId`),
  KEY `usuario_empresa` (`EmpresaId`),
  CONSTRAINT `usuario_empresa` FOREIGN KEY (`EmpresaId`) REFERENCES `empresa` (`EmpresaId`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,'75088866','William Gustavo Giraldo',NULL,'97f27fc9-1061-4355-b689-206de7790f54',1,NULL,NULL,'0c6d9e5b-f0bf-4aa6-b98b-4f95c63631c1','2016-02-03 11:37:06','','williamgustavo@gmail.com'),(36,NULL,'FGR',NULL,'6efe93b6-f004-4f42-956f-35013aad9140',36,NULL,'2016-02-11 14:09:16',NULL,NULL,'','william.gustavo@gmail.com'),(37,NULL,'liliana',NULL,'67801670-1198-46d4-b939-f7522f92a5ae',37,NULL,'2016-02-11 14:32:58',NULL,NULL,'','lilalg16@hotmail.com'),(38,NULL,'horka2000@hotmail.com',NULL,'b8502b83-ada2-4dda-a1f9-28fc2f8b35ad',38,NULL,'2016-02-12 16:00:48',NULL,NULL,'','horka2000@hotmail.com'),(39,NULL,'asas',NULL,'db856a4c-88dd-4888-8eb6-59171ad9ac17',39,NULL,'2016-02-19 20:00:12',NULL,NULL,'','saske_r@hotmail.com'),(40,NULL,'Inversiones Padilla y A ',NULL,'c10563cf-c8f1-4e23-b9c1-e773f58db58f',40,NULL,'2016-02-21 17:34:42',NULL,NULL,'','vpadillaramirez@gmail.com'),(41,NULL,'SERVI PRESTAMOS',NULL,'87b4e46f-a94f-4f31-849f-105128047b89',41,NULL,'2016-03-06 20:12:43',NULL,NULL,'','100211829PACF@gmail.com'),(42,NULL,'SERVI PRESTAMOS',NULL,'7b31884f-1895-4394-bcaa-55a0cbced37e',42,NULL,'2016-03-07 00:22:59',NULL,NULL,'','drfernandez18@gmail.com'),(43,NULL,'Credi GIO',NULL,'437ac981-2694-48af-be1e-d2c6b9697065',43,NULL,'2016-03-09 23:21:07',NULL,NULL,'','geovanni.almiron@mail.com'),(44,NULL,'Credi GIO',NULL,'1d401696-e6fa-4843-88ee-5a2df8f484ec',44,NULL,'2016-03-09 23:42:35',NULL,NULL,'','geovanni.almiron@tigo.net.py');
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-12-05 10:39:10
