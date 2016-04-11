-- MySQL dump 10.13  Distrib 5.6.28, for debian-linux-gnu (x86_64)
--
-- Host: localhost    Database: mess_db
-- ------------------------------------------------------
-- Server version	5.6.28-0ubuntu0.15.04.1

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
-- Table structure for table `ClosingStock`
--

DROP TABLE IF EXISTS `ClosingStock`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ClosingStock` (
  `article` varchar(45) NOT NULL,
  `unit` double NOT NULL,
  `balance` double NOT NULL,
  `rate` double NOT NULL,
  PRIMARY KEY (`article`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ClosingStock`
--

LOCK TABLES `ClosingStock` WRITE;
/*!40000 ALTER TABLE `ClosingStock` DISABLE KEYS */;
/*!40000 ALTER TABLE `ClosingStock` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `EmployeeShare`
--

DROP TABLE IF EXISTS `EmployeeShare`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `EmployeeShare` (
  `epf` double NOT NULL,
  `esi` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `EmployeeShare`
--

LOCK TABLES `EmployeeShare` WRITE;
/*!40000 ALTER TABLE `EmployeeShare` DISABLE KEYS */;
/*!40000 ALTER TABLE `EmployeeShare` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Employees`
--

DROP TABLE IF EXISTS `Employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Employees` (
  `sno` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `addr` varchar(100) DEFAULT NULL,
  `mobno` int(11) DEFAULT NULL,
  `category` varchar(20) NOT NULL,
  `slrytype` varchar(20) NOT NULL,
  `Accno` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`sno`),
  KEY `slrytype` (`slrytype`),
  CONSTRAINT `Employees_ibfk_1` FOREIGN KEY (`slrytype`) REFERENCES `Salary` (`slrytype`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Employees`
--

LOCK TABLES `Employees` WRITE;
/*!40000 ALTER TABLE `Employees` DISABLE KEYS */;
/*!40000 ALTER TABLE `Employees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `FixedMenu`
--

DROP TABLE IF EXISTS `FixedMenu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `FixedMenu` (
  `article` varchar(45) NOT NULL,
  `rate` double NOT NULL,
  PRIMARY KEY (`article`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `FixedMenu`
--

LOCK TABLES `FixedMenu` WRITE;
/*!40000 ALTER TABLE `FixedMenu` DISABLE KEYS */;
/*!40000 ALTER TABLE `FixedMenu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Menu`
--

DROP TABLE IF EXISTS `Menu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Menu` (
  `article` varchar(45) NOT NULL,
  `rate` double NOT NULL,
  `day` int(11) NOT NULL,
  PRIMARY KEY (`article`,`day`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Menu`
--

LOCK TABLES `Menu` WRITE;
/*!40000 ALTER TABLE `Menu` DISABLE KEYS */;
/*!40000 ALTER TABLE `Menu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Salary`
--

DROP TABLE IF EXISTS `Salary`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Salary` (
  `slrytype` varchar(20) NOT NULL,
  `wage` double NOT NULL,
  PRIMARY KEY (`slrytype`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Salary`
--

LOCK TABLES `Salary` WRITE;
/*!40000 ALTER TABLE `Salary` DISABLE KEYS */;
/*!40000 ALTER TABLE `Salary` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Smt`
--

DROP TABLE IF EXISTS `Smt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Smt` (
  `sid` int(11) NOT NULL AUTO_INCREMENT,
  `roll` int(11) NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `item` varchar(45) NOT NULL,
  `quantity` int(11) NOT NULL DEFAULT '0',
  `price` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`sid`),
  KEY `roll` (`roll`),
  CONSTRAINT `Smt_ibfk_1` FOREIGN KEY (`roll`) REFERENCES `Student` (`roll`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Smt`
--

LOCK TABLES `Smt` WRITE;
/*!40000 ALTER TABLE `Smt` DISABLE KEYS */;
/*!40000 ALTER TABLE `Smt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Student`
--

DROP TABLE IF EXISTS `Student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Student` (
  `name` varchar(45) DEFAULT NULL,
  `roll` int(11) NOT NULL,
  `remark` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`roll`),
  UNIQUE KEY `roll` (`roll`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Student`
--

LOCK TABLES `Student` WRITE;
/*!40000 ALTER TABLE `Student` DISABLE KEYS */;
/*!40000 ALTER TABLE `Student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Supplier`
--

DROP TABLE IF EXISTS `Supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Supplier` (
  `sid` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `article` varchar(45) NOT NULL,
  PRIMARY KEY (`sid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Supplier`
--

LOCK TABLES `Supplier` WRITE;
/*!40000 ALTER TABLE `Supplier` DISABLE KEYS */;
/*!40000 ALTER TABLE `Supplier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `VendorInvoices`
--

DROP TABLE IF EXISTS `VendorInvoices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `VendorInvoices` (
  `sno` int(11) NOT NULL,
  `sid` int(11) NOT NULL,
  `invno` varchar(30) NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `puramt` double DEFAULT NULL,
  `discount` double DEFAULT NULL,
  PRIMARY KEY (`sno`),
  KEY `sid` (`sid`),
  CONSTRAINT `VendorInvoices_ibfk_1` FOREIGN KEY (`sid`) REFERENCES `Supplier` (`sid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `VendorInvoices`
--

LOCK TABLES `VendorInvoices` WRITE;
/*!40000 ALTER TABLE `VendorInvoices` DISABLE KEYS */;
/*!40000 ALTER TABLE `VendorInvoices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `WorkerAttendence`
--

DROP TABLE IF EXISTS `WorkerAttendence`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `WorkerAttendence` (
  `name` varchar(45) NOT NULL,
  `sno` int(11) NOT NULL,
  `bio` int(11) NOT NULL,
  `diff` int(11) NOT NULL,
  `month` varchar(20) NOT NULL,
  `year` varchar(20) NOT NULL,
  `advance` double NOT NULL,
  PRIMARY KEY (`name`,`month`,`year`),
  KEY `sno` (`sno`),
  CONSTRAINT `WorkerAttendence_ibfk_1` FOREIGN KEY (`sno`) REFERENCES `Employees` (`sno`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `WorkerAttendence`
--

LOCK TABLES `WorkerAttendence` WRITE;
/*!40000 ALTER TABLE `WorkerAttendence` DISABLE KEYS */;
/*!40000 ALTER TABLE `WorkerAttendence` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-04-11 10:48:58
