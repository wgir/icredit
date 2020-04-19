-- MySQL dump 10.13  Distrib 5.5.35, for Win64 (x86)
--
-- Host: adisoft.cahzix8kpxdf.us-west-2.rds.amazonaws.com    Database: icredit
-- ------------------------------------------------------
-- Server version	5.6.23-log

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
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `abono`
--

LOCK TABLES `abono` WRITE;
/*!40000 ALTER TABLE `abono` DISABLE KEYS */;
INSERT INTO `abono` VALUES (37,385,'2016-02-08 10:36:59',91679.99,91679.99,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-02-08 10:37:14',NULL,NULL,6,'2016-02-08 11:42:41','');
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
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aporte`
--

LOCK TABLES `aporte` WRITE;
/*!40000 ALTER TABLE `aporte` DISABLE KEYS */;
INSERT INTO `aporte` VALUES (8,2000000,'2016-02-01 00:00:00',5,7,NULL,'','97f27fc9-1061-4355-b689-206de7790f54','2016-02-01 13:45:54','97f27fc9-1061-4355-b689-206de7790f54','2016-02-02 08:42:45'),(9,2000000,'2016-02-02 00:00:00',5,7,NULL,'','97f27fc9-1061-4355-b689-206de7790f54','2016-02-01 14:11:36','97f27fc9-1061-4355-b689-206de7790f54','2016-02-02 08:42:36'),(12,2000,'2016-02-06 00:00:00',5,7,NULL,'','97f27fc9-1061-4355-b689-206de7790f54','2016-02-06 15:29:57','97f27fc9-1061-4355-b689-206de7790f54','2016-02-06 15:30:21'),(13,1000,'2016-02-06 00:00:00',5,7,NULL,'','97f27fc9-1061-4355-b689-206de7790f54','2016-02-06 15:30:46',NULL,NULL);
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
INSERT INTO `aspnetuserroles` VALUES ('97f27fc9-1061-4355-b689-206de7790f54','0'),('4947aa29-7b69-469a-8a8f-3136da8a8e61','1');
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
INSERT INTO `aspnetusers` VALUES ('4947aa29-7b69-469a-8a8f-3136da8a8e61','william.gustavo@gmail.com',0,'AERLQLzAJ1ZQU3lAwJpgfEy5tf/7zA8nOL9mMuDdEBaaiwXGGbwFITok8TB6ORD0OA==','28c6acdb-c564-4529-b021-2b94b0882003',NULL,0,0,NULL,1,0,'william.gustavo@gmail.com'),('97f27fc9-1061-4355-b689-206de7790f54','williamgustavo@gmail.com',0,'AGxC2jbVBudlsq9ux7d8KkTAZB47jup7kyhiQcW3ppst7RtUzc/nA28YrxhlIUB6dQ==','523ccd8e-9bac-4e4c-9d64-a66cd28fed4c',NULL,0,0,NULL,1,0,'williamgustavo@gmail.com');
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
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ciudad`
--

LOCK TABLES `ciudad` WRITE;
/*!40000 ALTER TABLE `ciudad` DISABLE KEYS */;
INSERT INTO `ciudad` VALUES (1,'Manizales',1,'','0c6d9e5b-f0bf-4aa6-b98b-4f95c63631c1','2016-01-28 13:48:34','0c6d9e5b-f0bf-4aa6-b98b-4f95c63631c1','2016-01-28 13:56:32'),(8,'Pereira',1,'',NULL,NULL,'abe8421a-76b6-4b22-845a-82350e80c094','2016-01-30 10:26:41'),(14,'Armenia',1,'','abe8421a-76b6-4b22-845a-82350e80c094','2016-01-30 10:41:34',NULL,NULL),(18,'Manizales',3,'',NULL,'2016-02-09 18:14:39',NULL,NULL),(19,'Pereira',3,'',NULL,'2016-02-09 18:14:39',NULL,NULL),(20,'Armenia',3,'',NULL,'2016-02-09 18:14:39',NULL,NULL);
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
  CONSTRAINT `FK_Cliente_Empresa_EmpresaId` FOREIGN KEY (`EmpresaId`) REFERENCES `empresa` (`EmpresaId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `cliente_ciudad` FOREIGN KEY (`CiudadId`) REFERENCES `ciudad` (`CiudadId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cliente`
--

LOCK TABLES `cliente` WRITE;
/*!40000 ALTER TABLE `cliente` DISABLE KEYS */;
INSERT INTO `cliente` VALUES (1,'75088866','william giraldo r',NULL,NULL,'williamgustavo@gmail.com',1,NULL,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-02-06 19:55:27',1,''),(2,'1','prueba',NULL,NULL,NULL,1,'97f27fc9-1061-4355-b689-206de7790f54','2015-12-09 10:13:52',NULL,NULL,1,''),(5,'2','p2',NULL,NULL,'p2@gmail.com',1,'97f27fc9-1061-4355-b689-206de7790f54','2016-01-30 10:50:35',NULL,NULL,14,'');
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
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `conceptoaporte`
--

LOCK TABLES `conceptoaporte` WRITE;
/*!40000 ALTER TABLE `conceptoaporte` DISABLE KEYS */;
INSERT INTO `conceptoaporte` VALUES (7,'Capital Inicial',1,'','97f27fc9-1061-4355-b689-206de7790f54','2016-01-28 14:44:44',NULL,NULL),(9,'Capital Inicial',3,'',NULL,'2016-02-09 18:14:39',NULL,NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `conceptoretiro`
--

LOCK TABLES `conceptoretiro` WRITE;
/*!40000 ALTER TABLE `conceptoretiro` DISABLE KEYS */;
INSERT INTO `conceptoretiro` VALUES (4,'Retiro de Capital',1,'','97f27fc9-1061-4355-b689-206de7790f54','2016-01-28 14:45:05','97f27fc9-1061-4355-b689-206de7790f54','2016-01-28 14:45:10'),(6,'Retiro de Capital',3,'',NULL,'2016-02-09 18:14:39',NULL,NULL);
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
INSERT INTO `consecutivo` VALUES (1,13,2,6),(3,0,0,0);
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
  CONSTRAINT `FK_Credito_TipoCuota_TipoCuotaId` FOREIGN KEY (`TipoCuotaId`) REFERENCES `tipocuota` (`TipoCuotaId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FK_credito_empresa_1` FOREIGN KEY (`EmpresaId`) REFERENCES `empresa` (`EmpresaId`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `credito`
--

LOCK TABLES `credito` WRITE;
/*!40000 ALTER TABLE `credito` DISABLE KEYS */;
INSERT INTO `credito` VALUES (13,'2016-02-07 00:00:00',1,1000000,1.5,12,'FI',0,30,0,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-02-07 08:22:10','97f27fc9-1061-4355-b689-206de7790f54','2016-02-07 08:42:32',10,1,NULL,''),(14,'2016-02-07 00:00:00',1,1000000,1.5,12,'FI',0,30,0,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-02-07 08:49:31',NULL,NULL,11,1,NULL,''),(15,'2016-02-07 00:00:00',1,1000000,1.5,12,'FI',0,30,0,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-02-07 15:13:38',NULL,NULL,12,1,1,''),(16,'2016-02-08 00:00:00',1,1000000,1.5,12,'FI',0,30,0,0,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-02-08 10:12:09',NULL,NULL,13,1,NULL,'');
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
) ENGINE=InnoDB AUTO_INCREMENT=397 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cuota`
--

LOCK TABLES `cuota` WRITE;
/*!40000 ALTER TABLE `cuota` DISABLE KEYS */;
INSERT INTO `cuota` VALUES (349,13,1,'2016-03-07 00:00:00',76679.9929062295,15000,923320.007093771),(350,13,2,'2016-04-07 00:00:00',77830.1927998229,13849.8001064066,845489.814293948),(351,13,3,'2016-05-07 00:00:00',78997.6456918203,12682.3472144092,766492.168602127),(352,13,4,'2016-06-07 00:00:00',80182.6103771976,11497.3825290319,686309.55822493),(353,13,5,'2016-07-07 00:00:00',81385.3495328555,10294.6433733739,604924.208692074),(354,13,6,'2016-08-07 00:00:00',82606.1297758484,9073.86313038111,522318.078916226),(355,13,7,'2016-09-07 00:00:00',83845.2217224861,7834.77118374339,438472.85719374),(356,13,8,'2016-10-07 00:00:00',85102.9000483234,6577.0928579061,353369.957145416),(357,13,9,'2016-11-07 00:00:00',86379.4435490482,5300.54935718124,266990.513596368),(358,13,10,'2016-12-07 00:00:00',87675.1352022839,4004.85770394552,179315.378394084),(359,13,11,'2017-01-07 00:00:00',88990.2622303182,2689.73067591126,90325.116163766),(360,13,12,'2017-02-07 00:00:00',90325.116163773,1354.87674245649,-0.00000000698491930961609),(361,14,1,'2016-03-07 00:00:00',76679.9929062295,15000,923320.007093771),(362,14,2,'2016-04-07 00:00:00',77830.1927998229,13849.8001064066,845489.814293948),(363,14,3,'2016-05-07 00:00:00',78997.6456918203,12682.3472144092,766492.168602127),(364,14,4,'2016-06-07 00:00:00',80182.6103771976,11497.3825290319,686309.55822493),(365,14,5,'2016-07-07 00:00:00',81385.3495328555,10294.6433733739,604924.208692074),(366,14,6,'2016-08-07 00:00:00',82606.1297758484,9073.86313038111,522318.078916226),(367,14,7,'2016-09-07 00:00:00',83845.2217224861,7834.77118374339,438472.85719374),(368,14,8,'2016-10-07 00:00:00',85102.9000483234,6577.0928579061,353369.957145416),(369,14,9,'2016-11-07 00:00:00',86379.4435490482,5300.54935718124,266990.513596368),(370,14,10,'2016-12-07 00:00:00',87675.1352022839,4004.85770394552,179315.378394084),(371,14,11,'2017-01-07 00:00:00',88990.2622303182,2689.73067591126,90325.116163766),(372,14,12,'2017-02-07 00:00:00',90325.116163773,1354.87674245649,-0.00000000698491930961609),(373,15,1,'2016-03-07 00:00:00',76679.9929062295,15000,923320.007093771),(374,15,2,'2016-04-07 00:00:00',77830.1927998229,13849.8001064066,845489.814293948),(375,15,3,'2016-05-07 00:00:00',78997.6456918203,12682.3472144092,766492.168602127),(376,15,4,'2016-06-07 00:00:00',80182.6103771976,11497.3825290319,686309.55822493),(377,15,5,'2016-07-07 00:00:00',81385.3495328555,10294.6433733739,604924.208692074),(378,15,6,'2016-08-07 00:00:00',82606.1297758484,9073.86313038111,522318.078916226),(379,15,7,'2016-09-07 00:00:00',83845.2217224861,7834.77118374339,438472.85719374),(380,15,8,'2016-10-07 00:00:00',85102.9000483234,6577.0928579061,353369.957145416),(381,15,9,'2016-11-07 00:00:00',86379.4435490482,5300.54935718124,266990.513596368),(382,15,10,'2016-12-07 00:00:00',87675.1352022839,4004.85770394552,179315.378394084),(383,15,11,'2017-01-07 00:00:00',88990.2622303182,2689.73067591126,90325.116163766),(384,15,12,'2017-02-07 00:00:00',90325.116163773,1354.87674245649,-0.00000000698491930961609),(385,16,1,'2016-03-08 00:00:00',76679.9929062295,15000,923320.007093771),(386,16,2,'2016-04-08 00:00:00',77830.1927998229,13849.8001064066,845489.814293948),(387,16,3,'2016-05-08 00:00:00',78997.6456918203,12682.3472144092,766492.168602127),(388,16,4,'2016-06-08 00:00:00',80182.6103771976,11497.3825290319,686309.55822493),(389,16,5,'2016-07-08 00:00:00',81385.3495328555,10294.6433733739,604924.208692074),(390,16,6,'2016-08-08 00:00:00',82606.1297758484,9073.86313038111,522318.078916226),(391,16,7,'2016-09-08 00:00:00',83845.2217224861,7834.77118374339,438472.85719374),(392,16,8,'2016-10-08 00:00:00',85102.9000483234,6577.0928579061,353369.957145416),(393,16,9,'2016-11-08 00:00:00',86379.4435490482,5300.54935718124,266990.513596368),(394,16,10,'2016-12-08 00:00:00',87675.1352022839,4004.85770394552,179315.378394084),(395,16,11,'2017-01-08 00:00:00',88990.2622303182,2689.73067591126,90325.116163766),(396,16,12,'2017-02-08 00:00:00',90325.116163773,1354.87674245649,-0.00000000698491930961609);
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `empresa`
--

LOCK TABLES `empresa` WRITE;
/*!40000 ALTER TABLE `empresa` DISABLE KEYS */;
INSERT INTO `empresa` VALUES (1,'1','Adisoft','Av 19 nr 7b 140','300 658 72 32','',NULL,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-02-02 16:32:06','LogoUrl0.jpg','sisadisoft@gmail.com',2016,2),(3,'','FGR',NULL,NULL,'',NULL,'2016-02-09 18:14:37',NULL,NULL,NULL,'william.gustavo@gmail.com',2016,2);
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
INSERT INTO `loghome` VALUES (1,37);
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
INSERT INTO `paramcorreo` VALUES (3,'smtp.gmail.com',587,'williamgustavo@gmail.com','wg196507',1,0,'97f27fc9-1061-4355-b689-206de7790f54','2016-01-28 15:10:36','97f27fc9-1061-4355-b689-206de7790f54','2016-02-09 17:08:19');
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
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retiro`
--

LOCK TABLES `retiro` WRITE;
/*!40000 ALTER TABLE `retiro` DISABLE KEYS */;
INSERT INTO `retiro` VALUES (8,50000,'2016-02-02 00:00:00',5,4,NULL,'\0','97f27fc9-1061-4355-b689-206de7790f54','2016-02-01 14:31:43','97f27fc9-1061-4355-b689-206de7790f54','2016-02-02 08:43:02');
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retirointeres`
--

LOCK TABLES `retirointeres` WRITE;
/*!40000 ALTER TABLE `retirointeres` DISABLE KEYS */;
INSERT INTO `retirointeres` VALUES (1,2000,'2016-02-02 00:00:00',1,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-02-02 11:05:47','97f27fc9-1061-4355-b689-206de7790f54','2016-02-02 11:22:25','',1),(2,2000,'2016-02-10 00:00:00',1,NULL,'97f27fc9-1061-4355-b689-206de7790f54','2016-02-02 11:07:15','97f27fc9-1061-4355-b689-206de7790f54','2016-02-02 14:14:34','',NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `socio`
--

LOCK TABLES `socio` WRITE;
/*!40000 ALTER TABLE `socio` DISABLE KEYS */;
INSERT INTO `socio` VALUES (5,'1','SOCIO 1',1,'','97f27fc9-1061-4355-b689-206de7790f54','2016-02-01 13:45:00',NULL,NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,'75088866','William Gustavo Giraldo',NULL,'97f27fc9-1061-4355-b689-206de7790f54',1,NULL,NULL,'0c6d9e5b-f0bf-4aa6-b98b-4f95c63631c1','2016-02-03 11:37:06','','williamgustavo@gmail.com'),(3,NULL,'FGR',NULL,'4947aa29-7b69-469a-8a8f-3136da8a8e61',3,NULL,'2016-02-09 18:14:37',NULL,NULL,'','william.gustavo@gmail.com');
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

-- Dump completed on 2016-02-09 20:51:01
