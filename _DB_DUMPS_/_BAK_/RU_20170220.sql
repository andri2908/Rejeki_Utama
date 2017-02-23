-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: sys_pos_rejekiutama
-- ------------------------------------------------------
-- Server version	5.5.28

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
-- Table structure for table `account_credit`
--

DROP TABLE IF EXISTS `account_credit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `account_credit` (
  `credit_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `credit_sales_id` int(10) unsigned NOT NULL,
  `credit_duedate` date NOT NULL,
  `credit_nominal` double NOT NULL,
  `credit_paid` tinyint(4) unsigned DEFAULT NULL,
  PRIMARY KEY (`credit_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_credit`
--

LOCK TABLES `account_credit` WRITE;
/*!40000 ALTER TABLE `account_credit` DISABLE KEYS */;
/*!40000 ALTER TABLE `account_credit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `account_debt`
--

DROP TABLE IF EXISTS `account_debt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `account_debt` (
  `debt_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `debt_purchase_id` int(10) unsigned NOT NULL,
  `debt_duedate` date NOT NULL,
  `debt_nominal` double NOT NULL,
  `debt_paid` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`debt_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_debt`
--

LOCK TABLES `account_debt` WRITE;
/*!40000 ALTER TABLE `account_debt` DISABLE KEYS */;
/*!40000 ALTER TABLE `account_debt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `account_journal`
--

DROP TABLE IF EXISTS `account_journal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `account_journal` (
  `journal_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `account_id` smallint(5) unsigned NOT NULL,
  `journal_date` date NOT NULL,
  `account_nominal` double NOT NULL,
  `branch_id` tinyint(3) unsigned NOT NULL,
  `journal_description` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`journal_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_journal`
--

LOCK TABLES `account_journal` WRITE;
/*!40000 ALTER TABLE `account_journal` DISABLE KEYS */;
/*!40000 ALTER TABLE `account_journal` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cashier_log`
--

DROP TABLE IF EXISTS `cashier_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cashier_log` (
  `ID` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `USER_ID` tinyint(3) unsigned DEFAULT NULL,
  `DATE_LOGIN` datetime DEFAULT NULL,
  `DATE_LOGOUT` datetime DEFAULT NULL,
  `AMOUNT_START` double DEFAULT NULL,
  `AMOUNT_END` double DEFAULT NULL,
  `ACCOUNT_ID` smallint(5) unsigned DEFAULT NULL,
  `COMMENT` varchar(100) DEFAULT NULL,
  `TOTAL_CASH_TRANSACTION` double DEFAULT '0',
  `TOTAL_NON_CASH_TRANSACTION` double DEFAULT '0',
  `TOTAL_OTHER_TRANSACTION` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cashier_log`
--

LOCK TABLES `cashier_log` WRITE;
/*!40000 ALTER TABLE `cashier_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `cashier_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `credit`
--

DROP TABLE IF EXISTS `credit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `credit` (
  `CREDIT_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `PM_INVOICE` varchar(30) DEFAULT NULL,
  `CREDIT_DUE_DATE` date DEFAULT NULL,
  `CREDIT_NOMINAL` double DEFAULT '0',
  `CREDIT_PAID` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`CREDIT_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `credit`
--

LOCK TABLES `credit` WRITE;
/*!40000 ALTER TABLE `credit` DISABLE KEYS */;
INSERT INTO `credit` VALUES (1,'SLO001-1',NULL,'2016-09-05',2,1),(2,'SLO001-8',NULL,'2016-10-01',400,1),(3,'SLO001-9',NULL,'2016-10-01',400,1),(4,'SLO001-10',NULL,'2016-10-17',200,1),(5,'SLO001-11',NULL,'2016-11-02',500,1),(6,'SLO001-12',NULL,'2017-02-18',200,1),(7,'SLO001-13',NULL,'2017-02-18',200,1);
/*!40000 ALTER TABLE `credit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer_product_disc`
--

DROP TABLE IF EXISTS `customer_product_disc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customer_product_disc` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `CUSTOMER_ID` smallint(6) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `DISC_1` double unsigned DEFAULT '0',
  `DISC_2` double unsigned DEFAULT '0',
  `DISC_RP` double unsigned DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer_product_disc`
--

LOCK TABLES `customer_product_disc` WRITE;
/*!40000 ALTER TABLE `customer_product_disc` DISABLE KEYS */;
INSERT INTO `customer_product_disc` VALUES (1,1,'1',0,0,0),(2,2,'2',0,0,0);
/*!40000 ALTER TABLE `customer_product_disc` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `daily_journal`
--

DROP TABLE IF EXISTS `daily_journal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `daily_journal` (
  `journal_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `account_id` smallint(5) unsigned NOT NULL,
  `journal_datetime` datetime NOT NULL,
  `journal_nominal` double NOT NULL,
  `branch_id` tinyint(3) unsigned DEFAULT NULL,
  `journal_description` varchar(100) DEFAULT NULL,
  `user_id` tinyint(3) unsigned NOT NULL,
  `pm_id` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY (`journal_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `daily_journal`
--

LOCK TABLES `daily_journal` WRITE;
/*!40000 ALTER TABLE `daily_journal` DISABLE KEYS */;
INSERT INTO `daily_journal` VALUES (1,1,'2016-09-03 00:15:00',2,NULL,'PEMBAYARAN SLO001-1',1,1),(2,1,'2016-10-01 00:42:00',400,NULL,'PEMBAYARAN SLO001-8',1,1),(3,1,'2016-10-01 14:28:00',400,NULL,'PEMBAYARAN SLO001-9',1,1),(4,1,'2016-10-17 15:11:00',200,NULL,'PEMBAYARAN SLO001-10',1,1),(5,1,'2016-11-02 12:30:00',500,NULL,'PEMBAYARAN SLO001-11',1,1),(6,1,'2017-02-18 15:07:00',200,NULL,'PEMBAYARAN SLO001-12',1,1),(7,1,'2017-02-18 15:12:00',200,NULL,'PEMBAYARAN SLO001-13',1,1),(8,1,'2017-02-18 23:57:00',200,NULL,'PEMBAYARAN SLO001-12',1,1);
/*!40000 ALTER TABLE `daily_journal` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `debt`
--

DROP TABLE IF EXISTS `debt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `debt` (
  `DEBT_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PURCHASE_INVOICE` varchar(30) DEFAULT NULL,
  `DEBT_DUE_DATE` date DEFAULT NULL,
  `DEBT_NOMINAL` double DEFAULT '0',
  `DEBT_PAID` tinyint(3) unsigned DEFAULT '0',
  PRIMARY KEY (`DEBT_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `debt`
--

LOCK TABLES `debt` WRITE;
/*!40000 ALTER TABLE `debt` DISABLE KEYS */;
INSERT INTO `debt` VALUES (1,'PO-1','2016-09-23',220,0),(2,'PR-2','2017-02-18',200,0),(3,'PR-3','2017-02-18',100,0),(4,'PO-2','2017-02-18',1200,1),(5,'PR-5','2017-02-28',200,0);
/*!40000 ALTER TABLE `debt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `delivery_order_detail`
--

DROP TABLE IF EXISTS `delivery_order_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `delivery_order_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `DO_ID` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `delivery_order_detail`
--

LOCK TABLES `delivery_order_detail` WRITE;
/*!40000 ALTER TABLE `delivery_order_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `delivery_order_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `delivery_order_header`
--

DROP TABLE IF EXISTS `delivery_order_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `delivery_order_header` (
  `DO_ID` varchar(30) NOT NULL,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `REV_NO` tinyint(3) DEFAULT NULL,
  `DO_DATE` datetime DEFAULT NULL,
  PRIMARY KEY (`DO_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `delivery_order_header`
--

LOCK TABLES `delivery_order_header` WRITE;
/*!40000 ALTER TABLE `delivery_order_header` DISABLE KEYS */;
/*!40000 ALTER TABLE `delivery_order_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `detail_reminder_customer`
--

DROP TABLE IF EXISTS `detail_reminder_customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detail_reminder_customer` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `REMINDER_ID` int(11) DEFAULT NULL,
  `CUSTOMER_ID` int(11) DEFAULT NULL,
  `CUSTOMER_PHONE` varchar(15) DEFAULT NULL,
  `LAST_SEND_STATUS` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detail_reminder_customer`
--

LOCK TABLES `detail_reminder_customer` WRITE;
/*!40000 ALTER TABLE `detail_reminder_customer` DISABLE KEYS */;
INSERT INTO `detail_reminder_customer` VALUES (1,8,1,'12345',0),(2,8,2,'123',0),(3,9,1,'12345',0),(4,9,2,'123',0),(5,10,1,'12345',0),(6,11,1,'12345',NULL),(7,12,1,'12345',NULL);
/*!40000 ALTER TABLE `detail_reminder_customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_account`
--

DROP TABLE IF EXISTS `master_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_account` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `account_id` int(10) unsigned NOT NULL,
  `account_name` varchar(50) NOT NULL,
  `account_type_id` tinyint(3) unsigned NOT NULL,
  `account_active` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `account_name_UNIQUE` (`account_name`),
  UNIQUE KEY `account_id_UNIQUE` (`account_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_account`
--

LOCK TABLES `master_account` WRITE;
/*!40000 ALTER TABLE `master_account` DISABLE KEYS */;
INSERT INTO `master_account` VALUES (1,1,'PENDAPATAN TUNAI PENJUALAN',1,1),(2,2,'PENDAPATAN BCA PENJUALAN',1,1),(3,3,'PIUTANG PENJUALAN',1,1),(4,4,'BEBAN GAJI PUSAT',2,1),(5,5,'BEBAN LISTRIK PUSAT',2,1),(6,6,'BEBAN AIR',2,1),(7,7,'PENDAPATAN LAIN-LAIN',1,1),(8,8,'BEBAN LAIN-LAIN',2,1),(9,10,'BEBAN',2,1);
/*!40000 ALTER TABLE `master_account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_account_type`
--

DROP TABLE IF EXISTS `master_account_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_account_type` (
  `account_type_id` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `account_type_name` varchar(45) NOT NULL,
  PRIMARY KEY (`account_type_id`),
  UNIQUE KEY `account_type_name_UNIQUE` (`account_type_name`),
  UNIQUE KEY `account_type_id_UNIQUE` (`account_type_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_account_type`
--

LOCK TABLES `master_account_type` WRITE;
/*!40000 ALTER TABLE `master_account_type` DISABLE KEYS */;
INSERT INTO `master_account_type` VALUES (2,'CREDIT'),(1,'DEBET');
/*!40000 ALTER TABLE `master_account_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_branch`
--

DROP TABLE IF EXISTS `master_branch`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_branch` (
  `BRANCH_ID` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `BRANCH_NAME` varchar(50) NOT NULL,
  `BRANCH_ADDRESS_1` varchar(50) DEFAULT NULL,
  `BRANCH_ADDRESS_2` varchar(50) DEFAULT NULL,
  `BRANCH_ADDRESS_CITY` varchar(50) DEFAULT NULL,
  `BRANCH_TELEPHONE` varchar(15) DEFAULT NULL,
  `BRANCH_IP4` varchar(15) NOT NULL,
  `BRANCH_ACTIVE` tinyint(1) unsigned DEFAULT NULL,
  PRIMARY KEY (`BRANCH_ID`),
  UNIQUE KEY `BRANCH_NAME_UNIQUE` (`BRANCH_NAME`),
  UNIQUE KEY `BRANCH_IP4_UNIQUE` (`BRANCH_IP4`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_branch`
--

LOCK TABLES `master_branch` WRITE;
/*!40000 ALTER TABLE `master_branch` DISABLE KEYS */;
INSERT INTO `master_branch` VALUES (1,'SOLO BARU','','','','','111.111.111.111',1);
/*!40000 ALTER TABLE `master_branch` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_category`
--

DROP TABLE IF EXISTS `master_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_category` (
  `CATEGORY_ID` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `CATEGORY_NAME` varchar(50) DEFAULT NULL,
  `CATEGORY_DESCRIPTION` varchar(100) DEFAULT NULL,
  `CATEGORY_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`CATEGORY_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_category`
--

LOCK TABLES `master_category` WRITE;
/*!40000 ALTER TABLE `master_category` DISABLE KEYS */;
INSERT INTO `master_category` VALUES (1,'CAT 1','11',1);
/*!40000 ALTER TABLE `master_category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_change_id`
--

DROP TABLE IF EXISTS `master_change_id`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_change_id` (
  `CHANGE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `CHANGE_ID_DESCRIPTION` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`CHANGE_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COMMENT='	';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_change_id`
--

LOCK TABLES `master_change_id` WRITE;
/*!40000 ALTER TABLE `master_change_id` DISABLE KEYS */;
INSERT INTO `master_change_id` VALUES (1,'LOGIN'),(2,'LOGOUT'),(3,'INSERT'),(4,'UPDATE'),(5,'SET NON ACTIVE'),(6,'CASHIER LOGIN'),(7,'CASHIER LOGOUT'),(8,'PAYMENT CREDIT'),(9,'PAYMENT DEBT');
/*!40000 ALTER TABLE `master_change_id` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_customer`
--

DROP TABLE IF EXISTS `master_customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_customer` (
  `CUSTOMER_ID` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `CUSTOMER_FULL_NAME` varchar(50) DEFAULT NULL,
  `CUSTOMER_ADDRESS1` varchar(50) DEFAULT NULL,
  `CUSTOMER_ADDRESS2` varchar(50) DEFAULT NULL,
  `CUSTOMER_ADDRESS_CITY` varchar(50) DEFAULT NULL,
  `CUSTOMER_PHONE` varchar(15) DEFAULT NULL,
  `CUSTOMER_FAX` varchar(15) DEFAULT NULL,
  `CUSTOMER_EMAIL` varchar(50) DEFAULT NULL,
  `CUSTOMER_ACTIVE` tinyint(3) unsigned DEFAULT '0',
  `CUSTOMER_JOINED_DATE` date DEFAULT NULL,
  `CUSTOMER_TOTAL_SALES_COUNT` int(11) DEFAULT NULL,
  `CUSTOMER_GROUP` tinyint(3) unsigned DEFAULT NULL,
  `MAX_CREDIT` double DEFAULT '0',
  `CUSTOMER_BLOCKED` tinyint(3) DEFAULT '0',
  `REGION_ID` tinyint(3) DEFAULT '0',
  `CUSTOMER_BIRTHDAY` date DEFAULT NULL,
  PRIMARY KEY (`CUSTOMER_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_customer`
--

LOCK TABLES `master_customer` WRITE;
/*!40000 ALTER TABLE `master_customer` DISABLE KEYS */;
INSERT INTO `master_customer` VALUES (1,'PELANGGAN 1','JALAN JALAN','JALAN SATU','KOTA KOTA','12345',' ',' ',1,'2016-09-29',0,1,0,1,0,NULL),(2,'123','123','123','123','123','123','13',1,'2016-10-20',123,1,123,0,0,'1982-10-20'),(3,'123','123',' ',' ','11111',' ',' ',1,'2016-10-01',0,1,0,0,0,'2016-10-25'),(4,'TEST PELANGGAN',' ',' ',' ',' ',' ',' ',1,'2017-02-18',0,1,0,0,0,'2011-04-11');
/*!40000 ALTER TABLE `master_customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_group`
--

DROP TABLE IF EXISTS `master_group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_group` (
  `GROUP_ID` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `GROUP_USER_NAME` varchar(50) NOT NULL,
  `GROUP_USER_DESCRIPTION` varchar(100) NOT NULL,
  `GROUP_USER_ACTIVE` tinyint(1) unsigned NOT NULL,
  `GROUP_IS_CASHIER` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`GROUP_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_group`
--

LOCK TABLES `master_group` WRITE;
/*!40000 ALTER TABLE `master_group` DISABLE KEYS */;
INSERT INTO `master_group` VALUES (1,'GLOBAL_ADMIN','GLOBAL ADMIN GROUP',1,0),(2,'KASIR','AKSES HANYA MODUL PENJUALAN',1,1);
/*!40000 ALTER TABLE `master_group` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_location`
--

DROP TABLE IF EXISTS `master_location`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_location` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `LOCATION_NAME` varchar(45) DEFAULT NULL,
  `LOCATION_TOTAL_QTY` double DEFAULT '0',
  `LOCATION_DESCRIPTION` varchar(100) DEFAULT '',
  `LOCATION_ACTIVE` tinyint(3) DEFAULT '1',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_location`
--

LOCK TABLES `master_location` WRITE;
/*!40000 ALTER TABLE `master_location` DISABLE KEYS */;
INSERT INTO `master_location` VALUES (1,'TOKO',0,NULL,1),(2,'GUDANG',0,'SSS',1);
/*!40000 ALTER TABLE `master_location` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_message`
--

DROP TABLE IF EXISTS `master_message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_message` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `STATUS` tinyint(3) unsigned DEFAULT NULL,
  `MODULE_ID` smallint(5) unsigned DEFAULT NULL,
  `IDENTIFIER_NO` varchar(45) DEFAULT NULL,
  `MSG_DATETIME_CREATED` datetime DEFAULT NULL,
  `MSG_CONTENT` varchar(200) DEFAULT NULL,
  `MSG_DATETIME_READ` datetime DEFAULT NULL,
  `MSG_READ_USER_ID` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_message`
--

LOCK TABLES `master_message` WRITE;
/*!40000 ALTER TABLE `master_message` DISABLE KEYS */;
INSERT INTO `master_message` VALUES (1,0,37,'SLO001-1','2016-09-17 00:00:00','SALES INVOICE [SLO001-1] JATUH TEMPO TGL 05-September-2016',NULL,NULL),(2,0,31,'PO-1','2016-10-15 00:00:00','PURCHASE ORDER [PO-1] JATUH TEMPO TGL 23-September-2016',NULL,NULL),(3,0,308,'SLO001-1','2017-02-20 00:00:00','SALES INVOICE [SLO001-1] JATUH TEMPO TGL 05-September-2016',NULL,NULL),(4,0,204,'PO-1','2017-02-20 00:00:00','PURCHASE ORDER [PO-1] JATUH TEMPO TGL 23-September-2016',NULL,NULL),(5,0,204,'PR-2','2017-02-20 00:00:00','PURCHASE ORDER [PR-2] JATUH TEMPO TGL 18-February-2017',NULL,NULL),(6,0,204,'PR-3','2017-02-20 00:00:00','PURCHASE ORDER [PR-3] JATUH TEMPO TGL 18-February-2017',NULL,NULL),(7,0,102,'3','2017-02-20 00:00:00','PRODUCT_NAME [] SUDAH MENDEKATI LIMIT',NULL,NULL),(8,0,102,'4','2017-02-20 00:00:00','PRODUCT_NAME [] SUDAH MENDEKATI LIMIT',NULL,NULL);
/*!40000 ALTER TABLE `master_message` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_module`
--

DROP TABLE IF EXISTS `master_module`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_module` (
  `MODULE_ID` smallint(5) unsigned NOT NULL,
  `MODULE_NAME` varchar(50) DEFAULT NULL,
  `MODULE_DESCRIPTION` varchar(100) DEFAULT NULL,
  `MODULE_FEATURES` tinyint(3) unsigned DEFAULT NULL,
  `MODULE_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`MODULE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_module`
--

LOCK TABLES `master_module` WRITE;
/*!40000 ALTER TABLE `master_module` DISABLE KEYS */;
INSERT INTO `master_module` VALUES (1,'MANAJEMEN SISTEM',NULL,1,1),(2,'DATABASE',NULL,1,1),(3,'MANAJEMEN USER',NULL,3,1),(4,'MANAJEMEN CABANG',NULL,3,1),(5,'SINKRONISASI INFORMASI',NULL,1,0),(6,'PENGATURAN PRINTER',NULL,1,1),(7,'PENGATURAN GAMBAR LATAR',NULL,1,1),(8,'PENGATURAN SMS',NULL,1,1),(101,'GUDANG',NULL,1,1),(102,'PRODUK',NULL,1,1),(103,'TAMBAH / UPDATE PRODUK',NULL,3,1),(104,'PENGATURAN HARGA PRODUK',NULL,1,1),(105,'PENGATURAN LIMIT STOK',NULL,1,1),(106,'PENGATURAN KATEGORI PRODUK',NULL,1,1),(107,'PECAH SATUAN PRODUK',NULL,1,1),(108,'PENGATURAN NOMOR RAK',NULL,1,0),(109,'KATEGORI PRODUK',NULL,3,1),(110,'SATUAN PRODUK',NULL,1,1),(111,'TAMBAH / UPDATE SATUAN',NULL,1,1),(112,'PENGATURAN KONVERSI SATUAN',NULL,1,1),(113,'STOK OPNAME',NULL,1,1),(114,'PENYESUAIAN STOK',NULL,1,1),(115,'MUTASI BARANG',NULL,1,0),(116,'TAMBAH / UPDATE MUTASI BARANG',NULL,3,0),(117,'CEK PERMINTAAN BARANG',NULL,1,0),(118,'PENERIMAAN BARANG',NULL,1,1),(119,'PENERIMAAN BARANG DARI MUTASI',NULL,1,1),(120,'PENERIMAAN BARANG DARI PO',NULL,1,1),(121,'TRANSFER STOK',NULL,1,1),(201,'PEMBELIAN',NULL,1,1),(202,'SUPPLIER',NULL,1,1),(203,'REQUEST ORDER',NULL,3,0),(204,'PURCHASE ORDER',NULL,3,1),(205,'REPRINT REQUEST ORDER',NULL,1,1),(206,'RETUR PEMBELIAN KE SUPPLIER',NULL,1,1),(207,'RETUR PERMINTAAN KE PUSAT',NULL,1,0),(301,'PENJUALAN',NULL,1,1),(302,'PELANGGAN',NULL,3,1),(303,'SALES QUOTATION',NULL,1,1),(304,'APPROVAL SALES QUOTATION',NULL,1,1),(305,'PRE ORDER SALES',NULL,1,1),(306,'SALES ORDER REVISION',NULL,1,1),(307,'DELIVERY ORDER',NULL,1,1),(308,'TRANSAKSI PENJUALAN',NULL,1,1),(309,'SET NO FAKTUR',NULL,1,1),(310,'RETUR PENJUALAN',NULL,1,1),(311,'RETUR PENJUALAN BY INVOICE',NULL,1,1),(312,'RETUR PENJUALAN BY STOK',NULL,1,0),(313,'COPY DELIVERY ORDER',NULL,1,1),(401,'KEUANGAN',NULL,1,1),(402,'PENGATURAN NO AKUN',NULL,3,1),(403,'TRANSAKSI',NULL,1,1),(404,'TAMBAH TRANSAKSI HARIAN',NULL,1,1),(405,'PEMBAYARAN PIUTANG',NULL,1,1),(406,'PEMBAYARAN PIUTANG MUTASI',NULL,1,0),(407,'PEMBAYARAN HUTANG KE SUPPLIER',NULL,1,1),(408,'PENGATURAN LIMIT PAJAK',NULL,1,1),(501,'MODUL MESSAGING',NULL,1,1),(502,'TAX_MODULE',NULL,1,1),(800,'MODUL LAPORAN',NULL,1,1),(801,'LAPORAN PEMBELIAN BARANG',NULL,1,1),(802,'LAPORAN HUTANG AKAN JATUH TEMPO',NULL,1,1),(803,'LAPORAN HUTANG LEWAT JATUH TEMPO',NULL,1,1),(804,'LAPORAN PEMBAYARAN HUTANG',NULL,1,1),(805,'LAPORAN PENJUALAN PRODUK',NULL,1,1),(806,'LAPORAN OMZET PENJUALAN',NULL,1,1),(807,'LAPORAN TOP SALE',NULL,1,1),(808,'LAPORAN PENJUALAN KASIR',NULL,1,1),(809,'LAPORAN KEUANGAN',NULL,1,1),(810,'LAPORAN PIUTANG AKAN JATUH TEMPO',NULL,1,1),(811,'LAPORAN PIUTANG LEWAT JATUH TEMPO',NULL,1,1),(812,'LAPORAN PEMBAYARAN PIUTANG',NULL,1,1),(813,'LAPORAN DEVIASI STOK',NULL,1,1),(814,'LAPORAN STOK DIBAWAH LIMIT',NULL,1,1),(815,'LAPORAN RETUR BARANG',NULL,1,1),(816,'LAPORAN MUTASI BARANG',NULL,1,0),(817,'LAPORAN PEMBAYARAN MUTASI',NULL,1,0);
/*!40000 ALTER TABLE `master_module` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_product`
--

DROP TABLE IF EXISTS `master_product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_product` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PRODUCT_ID` varchar(50) DEFAULT '',
  `PRODUCT_BARCODE` varchar(15) DEFAULT '0',
  `PRODUCT_NAME` varchar(50) DEFAULT '',
  `PRODUCT_DESCRIPTION` varchar(100) DEFAULT '',
  `PRODUCT_BASE_PRICE` double DEFAULT '0',
  `PRODUCT_RETAIL_PRICE` double DEFAULT '0',
  `PRODUCT_BULK_PRICE` double DEFAULT '0',
  `PRODUCT_WHOLESALE_PRICE` double DEFAULT '0',
  `PRODUCT_PHOTO_1` varchar(50) DEFAULT '',
  `UNIT_ID` smallint(5) unsigned DEFAULT '0',
  `PRODUCT_STOCK_QTY` double DEFAULT '0',
  `PRODUCT_LIMIT_STOCK` double DEFAULT '0',
  `PRODUCT_SHELVES` varchar(5) DEFAULT '--00',
  `PRODUCT_ACTIVE` tinyint(3) unsigned DEFAULT '0',
  `PRODUCT_BRAND` varchar(50) DEFAULT '',
  `PRODUCT_IS_SERVICE` tinyint(3) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `PRODUCT_ID_UNIQUE` (`PRODUCT_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_product`
--

LOCK TABLES `master_product` WRITE;
/*!40000 ALTER TABLE `master_product` DISABLE KEYS */;
INSERT INTO `master_product` VALUES (1,'1','0','ANOTHER PRODUCT','AAAA',10,1,1,1,' ',1,31,0,'--00',1,' ',0),(2,'2','0','SERVICE AC','AAAA',200,200,200,200,' ',2,0,0,'--00',1,' ',1),(3,'3','0','',' ',0,0,0,0,' ',2,0,0,'--00',1,' ',0),(4,'4','0','',' ',0,0,0,0,' ',2,0,0,'--00',1,' ',0),(5,'5','0','SFD','SSS',10,10,10,10,' ',2,22,0,'--00',1,' ',0),(6,'6','0','TES PRODUK UNTUK CSV','AAAA',10,10,10,10,' ',2,45,0,'--00',1,' ',0);
/*!40000 ALTER TABLE `master_product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_region`
--

DROP TABLE IF EXISTS `master_region`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_region` (
  `ID` tinyint(4) NOT NULL AUTO_INCREMENT,
  `REGION_NAME` varchar(50) DEFAULT NULL,
  `REGION_DESCRIPTION` varchar(100) DEFAULT NULL,
  `REGION_ACTIVE` tinyint(3) DEFAULT '1',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_region`
--

LOCK TABLES `master_region` WRITE;
/*!40000 ALTER TABLE `master_region` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_region` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_reminder`
--

DROP TABLE IF EXISTS `master_reminder`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_reminder` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `REMINDER_ID` int(11) DEFAULT NULL,
  `START_DATE` date DEFAULT NULL,
  `INTERVAL_VALUE` int(11) DEFAULT NULL,
  `INTERVAL_TYPE` tinyint(4) DEFAULT '0',
  `START_TIME` time DEFAULT NULL,
  `MESSAGE_CONTENT` varchar(160) DEFAULT NULL,
  `REMINDER_ACTIVE` tinyint(4) DEFAULT '0',
  `IS_REPEATED` tinyint(4) DEFAULT NULL,
  `SCHEDULED_DATE` date DEFAULT NULL,
  `LAST_SEND_DATE` date DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_reminder`
--

LOCK TABLES `master_reminder` WRITE;
/*!40000 ALTER TABLE `master_reminder` DISABLE KEYS */;
INSERT INTO `master_reminder` VALUES (1,1,'2016-10-24',1,0,'00:00:00','aaaaa',1,1,'2016-12-04','2016-12-03'),(2,2,'2016-10-24',1,1,'00:00:00','aaaaabbbb',1,1,'2017-07-26','2017-07-19'),(3,3,'2016-10-23',1,1,'00:00:00','aaaaabbbb',1,1,'2017-07-19','2017-07-12'),(4,4,'2016-10-24',1,0,'00:00:00','aaaaa',1,1,'2016-12-02','2016-12-01'),(5,5,'2016-10-24',1,1,'00:00:00','aaaaa',1,1,'2017-07-12','2017-07-05'),(6,6,'2016-10-24',3,0,'00:00:00','aaaaa',1,1,'2017-02-14','2017-02-11'),(7,7,'2016-10-24',3,1,'00:00:00','aaaaa',1,1,'2018-12-12','2018-11-21'),(8,8,'2016-10-26',1,0,'10:00:00','aaaa',1,1,'2016-12-02','2016-12-01'),(10,9,'2016-10-26',1,2,'11:44:00','aaaaaa',1,1,'2019-11-26','2019-10-26'),(11,10,'2016-10-26',1,0,'15:05:00','aaaaaa',1,1,'2016-12-02','2016-12-01'),(14,11,'2016-10-26',1,0,'15:17:00','aaa',1,1,'2016-10-27',NULL),(15,12,'2016-10-27',1,0,'21:48:00','AAABBBBCCC',1,1,'2016-10-27',NULL);
/*!40000 ALTER TABLE `master_reminder` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_sales_target`
--

DROP TABLE IF EXISTS `master_sales_target`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_sales_target` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TARGET_MONTH` tinyint(4) DEFAULT '0',
  `TARGET_YEAR` int(11) DEFAULT '0',
  `TARGET_AMOUNT` double DEFAULT '0',
  `SALES_COMMISSION` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_sales_target`
--

LOCK TABLES `master_sales_target` WRITE;
/*!40000 ALTER TABLE `master_sales_target` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_sales_target` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_supplier`
--

DROP TABLE IF EXISTS `master_supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_supplier` (
  `SUPPLIER_ID` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `SUPPLIER_FULL_NAME` varchar(50) DEFAULT NULL,
  `SUPPLIER_ADDRESS1` varchar(50) DEFAULT NULL,
  `SUPPLIER_ADDRESS2` varchar(50) DEFAULT NULL,
  `SUPPLIER_ADDRESS_CITY` varchar(50) DEFAULT NULL,
  `SUPPLIER_PHONE` varchar(15) DEFAULT NULL,
  `SUPPLIER_FAX` varchar(15) DEFAULT NULL,
  `SUPPLIER_EMAIL` varchar(50) DEFAULT NULL,
  `SUPPLIER_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`SUPPLIER_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_supplier`
--

LOCK TABLES `master_supplier` WRITE;
/*!40000 ALTER TABLE `master_supplier` DISABLE KEYS */;
INSERT INTO `master_supplier` VALUES (1,'SUPPLIER 1','AAA','11',' ',' ',' ',' ',1),(2,'SUPPLIER TEST',' ',' ',' ',' ',' ',' ',1);
/*!40000 ALTER TABLE `master_supplier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_technician`
--

DROP TABLE IF EXISTS `master_technician`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_technician` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TECHNICIAN_NAME` varchar(45) DEFAULT NULL,
  `DESCRIPTION` varchar(100) DEFAULT NULL,
  `TECHNICIAN_ACTIVE` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_technician`
--

LOCK TABLES `master_technician` WRITE;
/*!40000 ALTER TABLE `master_technician` DISABLE KEYS */;
INSERT INTO `master_technician` VALUES (1,'ANDRI','ANDRI',1),(2,'TECH 1','TECH 1',1),(3,'TECH 2','222',1);
/*!40000 ALTER TABLE `master_technician` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_template`
--

DROP TABLE IF EXISTS `master_template`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_template` (
  `ID` tinyint(4) NOT NULL AUTO_INCREMENT,
  `TEMPLATE_NAME` varchar(45) DEFAULT NULL,
  `TEMPLATE_MESSAGE` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_template`
--

LOCK TABLES `master_template` WRITE;
/*!40000 ALTER TABLE `master_template` DISABLE KEYS */;
INSERT INTO `master_template` VALUES (1,'TEMPLATE1','AAABBBBCCC');
/*!40000 ALTER TABLE `master_template` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_template_message`
--

DROP TABLE IF EXISTS `master_template_message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_template_message` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TEMPLATE_NAME` varchar(45) DEFAULT NULL,
  `TEMPLATE_CONTENT` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_template_message`
--

LOCK TABLES `master_template_message` WRITE;
/*!40000 ALTER TABLE `master_template_message` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_template_message` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_unit`
--

DROP TABLE IF EXISTS `master_unit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_unit` (
  `UNIT_ID` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `UNIT_NAME` varchar(50) DEFAULT NULL,
  `UNIT_DESCRIPTION` varchar(100) DEFAULT NULL,
  `UNIT_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`UNIT_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_unit`
--

LOCK TABLES `master_unit` WRITE;
/*!40000 ALTER TABLE `master_unit` DISABLE KEYS */;
INSERT INTO `master_unit` VALUES (1,'UNIT 1','111',1),(2,'JAM','JAM',1);
/*!40000 ALTER TABLE `master_unit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_user`
--

DROP TABLE IF EXISTS `master_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_user` (
  `ID` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `USER_NAME` varchar(15) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `USER_PASSWORD` varchar(15) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `USER_FULL_NAME` varchar(50) DEFAULT NULL,
  `USER_PHONE` varchar(15) DEFAULT NULL,
  `USER_ACTIVE` tinyint(1) unsigned DEFAULT NULL,
  `GROUP_ID` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `USER_NAME_UNIQUE` (`USER_NAME`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='	';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_user`
--

LOCK TABLES `master_user` WRITE;
/*!40000 ALTER TABLE `master_user` DISABLE KEYS */;
INSERT INTO `master_user` VALUES (1,'ADMIN','admin','ADMIN','1',1,1);
/*!40000 ALTER TABLE `master_user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `membership_point`
--

DROP TABLE IF EXISTS `membership_point`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `membership_point` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `DATE_START` date DEFAULT NULL,
  `DATE_END` date DEFAULT NULL,
  `CUSTOMER_ID` varchar(45) DEFAULT NULL,
  `POINTS_PARAMETER` double DEFAULT NULL,
  `POINTS_AMOUNT` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `membership_point`
--

LOCK TABLES `membership_point` WRITE;
/*!40000 ALTER TABLE `membership_point` DISABLE KEYS */;
/*!40000 ALTER TABLE `membership_point` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_credit`
--

DROP TABLE IF EXISTS `payment_credit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payment_credit` (
  `payment_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `credit_id` int(11) NOT NULL,
  `payment_date` date NOT NULL,
  `pm_id` tinyint(3) NOT NULL,
  `payment_nominal` double NOT NULL,
  `payment_description` varchar(100) DEFAULT NULL,
  `payment_confirmed` tinyint(3) DEFAULT NULL,
  `payment_invalid` tinyint(4) DEFAULT '0',
  `payment_confirmed_date` date DEFAULT NULL,
  `payment_due_date` date DEFAULT NULL,
  PRIMARY KEY (`payment_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_credit`
--

LOCK TABLES `payment_credit` WRITE;
/*!40000 ALTER TABLE `payment_credit` DISABLE KEYS */;
INSERT INTO `payment_credit` VALUES (1,1,'2017-02-20',1,2,'RETUR [RT-1]',1,0,'2017-02-20','2017-02-20');
/*!40000 ALTER TABLE `payment_credit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_debt`
--

DROP TABLE IF EXISTS `payment_debt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payment_debt` (
  `payment_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `debt_id` int(11) NOT NULL,
  `payment_date` date NOT NULL,
  `pm_id` tinyint(3) NOT NULL,
  `payment_nominal` double NOT NULL,
  `payment_description` varchar(100) DEFAULT NULL,
  `payment_confirmed` tinyint(3) DEFAULT '0',
  `payment_invalid` tinyint(4) DEFAULT '0',
  `payment_confirmed_date` date DEFAULT NULL,
  `payment_due_date` date DEFAULT NULL,
  PRIMARY KEY (`payment_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_debt`
--

LOCK TABLES `payment_debt` WRITE;
/*!40000 ALTER TABLE `payment_debt` DISABLE KEYS */;
/*!40000 ALTER TABLE `payment_debt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_method`
--

DROP TABLE IF EXISTS `payment_method`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payment_method` (
  `pm_id` tinyint(4) NOT NULL AUTO_INCREMENT,
  `pm_name` varchar(15) NOT NULL,
  `pm_description` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`pm_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_method`
--

LOCK TABLES `payment_method` WRITE;
/*!40000 ALTER TABLE `payment_method` DISABLE KEYS */;
INSERT INTO `payment_method` VALUES (1,'TUNAI','TUNAI'),(2,'KARTU KREDIT','KARTU KREDIT'),(3,'KARTU DEBIT','KARTU DEBIT'),(4,'TRANSFER','TRANSFER BANK'),(5,'BG','BILYET GIRO'),(6,'CEK','CEK');
/*!40000 ALTER TABLE `payment_method` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_adjustment`
--

DROP TABLE IF EXISTS `product_adjustment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_adjustment` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PRODUCT_ID` varchar(30) DEFAULT NULL,
  `PRODUCT_ADJUSTMENT_DATE` date DEFAULT NULL,
  `PRODUCT_OLD_STOCK_QTY` double DEFAULT NULL,
  `PRODUCT_NEW_STOCK_QTY` double DEFAULT NULL,
  `PRODUCT_ADJUSTMENT_DESCRIPTION` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_adjustment`
--

LOCK TABLES `product_adjustment` WRITE;
/*!40000 ALTER TABLE `product_adjustment` DISABLE KEYS */;
INSERT INTO `product_adjustment` VALUES (1,'1','2017-02-18',1,100,' '),(2,'1','2017-02-18',1,4,''),(3,'5','2017-02-18',4,5,''),(4,'1','2017-02-18',3,0,''),(5,'5','2017-02-18',5,0,''),(6,'6','2017-02-18',10,20,''),(7,'6','2017-02-18',20,30,' '),(8,'6','2017-02-18',30,5,' ');
/*!40000 ALTER TABLE `product_adjustment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_category`
--

DROP TABLE IF EXISTS `product_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_category` (
  `PRODUCT_ID` varchar(50) NOT NULL,
  `CATEGORY_ID` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY (`PRODUCT_ID`,`CATEGORY_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_category`
--

LOCK TABLES `product_category` WRITE;
/*!40000 ALTER TABLE `product_category` DISABLE KEYS */;
INSERT INTO `product_category` VALUES ('1',1),('2',1),('5',1),('6',1);
/*!40000 ALTER TABLE `product_category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_location`
--

DROP TABLE IF EXISTS `product_location`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_location` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `LOCATION_ID` int(10) unsigned DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_LOCATION_QTY` double unsigned DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_location`
--

LOCK TABLES `product_location` WRITE;
/*!40000 ALTER TABLE `product_location` DISABLE KEYS */;
INSERT INTO `product_location` VALUES (1,1,'',0),(2,2,'',0),(3,1,'1',9),(4,2,'1',22),(5,1,'2',0),(6,2,'2',0),(7,1,'3',0),(8,2,'3',0),(9,1,'4',0),(10,2,'4',0),(11,1,'5',2),(12,2,'5',10),(13,1,'6',10),(14,2,'6',35);
/*!40000 ALTER TABLE `product_location` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_loss`
--

DROP TABLE IF EXISTS `product_loss`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_loss` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PL_DATETIME` date DEFAULT NULL,
  `PRODUCT_ID` int(10) unsigned DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT NULL,
  `NEW_PRODUCT_ID` int(10) unsigned DEFAULT NULL,
  `NEW_PRODUCT_QTY` double DEFAULT NULL,
  `TOTAL_LOSS` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_loss`
--

LOCK TABLES `product_loss` WRITE;
/*!40000 ALTER TABLE `product_loss` DISABLE KEYS */;
/*!40000 ALTER TABLE `product_loss` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products_mutation_detail`
--

DROP TABLE IF EXISTS `products_mutation_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `products_mutation_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PM_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_BASE_PRICE` double DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT NULL,
  `PM_SUBTOTAL` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products_mutation_detail`
--

LOCK TABLES `products_mutation_detail` WRITE;
/*!40000 ALTER TABLE `products_mutation_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `products_mutation_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products_mutation_header`
--

DROP TABLE IF EXISTS `products_mutation_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `products_mutation_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PM_INVOICE` varchar(30) DEFAULT NULL,
  `BRANCH_ID_FROM` tinyint(3) unsigned DEFAULT NULL,
  `BRANCH_ID_TO` tinyint(3) unsigned DEFAULT NULL,
  `PM_DATETIME` date DEFAULT NULL,
  `PM_TOTAL` double DEFAULT NULL,
  `RO_INVOICE` varchar(30) DEFAULT NULL,
  `PM_RECEIVED` tinyint(4) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products_mutation_header`
--

LOCK TABLES `products_mutation_header` WRITE;
/*!40000 ALTER TABLE `products_mutation_header` DISABLE KEYS */;
/*!40000 ALTER TABLE `products_mutation_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products_received_detail`
--

DROP TABLE IF EXISTS `products_received_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `products_received_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PR_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_BASE_PRICE` double DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT NULL,
  `PRODUCT_ACTUAL_QTY` double DEFAULT NULL,
  `PR_SUBTOTAL` double DEFAULT NULL,
  `PRODUCT_PRICE_CHANGE` tinyint(4) DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products_received_detail`
--

LOCK TABLES `products_received_detail` WRITE;
/*!40000 ALTER TABLE `products_received_detail` DISABLE KEYS */;
INSERT INTO `products_received_detail` VALUES (1,'PR-1','1',10,22,22,220,0),(2,'PR-2','1',10,10,10,100,0),(3,'PR-2','5',10,10,10,100,0),(4,'PR-3','6',10,10,10,100,0),(5,'PR-4','1',10,20,20,200,0),(6,'PR-4','6',10,10,100,1000,0),(7,'PR-5','6',10,20,20,200,0);
/*!40000 ALTER TABLE `products_received_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products_received_header`
--

DROP TABLE IF EXISTS `products_received_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `products_received_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PR_INVOICE` varchar(30) DEFAULT NULL,
  `PR_FROM` tinyint(3) unsigned DEFAULT NULL,
  `PR_TO` tinyint(3) unsigned DEFAULT NULL,
  `PR_DATE` date DEFAULT NULL,
  `PR_TOTAL` double DEFAULT NULL,
  `PM_INVOICE` varchar(30) DEFAULT NULL,
  `PURCHASE_INVOICE` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `PR_INVOICE_UNIQUE` (`PR_INVOICE`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='		';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products_received_header`
--

LOCK TABLES `products_received_header` WRITE;
/*!40000 ALTER TABLE `products_received_header` DISABLE KEYS */;
INSERT INTO `products_received_header` VALUES (1,'PR-1',1,0,'2016-09-03',220,NULL,'PO-1'),(2,'PR-2',1,0,'2017-02-18',200,NULL,'PR-2'),(3,'PR-3',1,0,'2017-02-18',100,NULL,'PR-3'),(4,'PR-4',2,0,'2017-02-18',1200,NULL,'PO-2'),(5,'PR-5',1,0,'2017-02-18',200,NULL,'PR-5');
/*!40000 ALTER TABLE `products_received_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchase_detail`
--

DROP TABLE IF EXISTS `purchase_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchase_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PURCHASE_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_PRICE` double DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT NULL,
  `PURCHASE_SUBTOTAL` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase_detail`
--

LOCK TABLES `purchase_detail` WRITE;
/*!40000 ALTER TABLE `purchase_detail` DISABLE KEYS */;
INSERT INTO `purchase_detail` VALUES (1,'PO-1','1',10,22,220),(2,'PR-2','1',10,10,100),(3,'PR-2','5',10,10,100),(4,'PR-3','6',10,10,100),(5,'PO-2','1',10,20,200),(6,'PO-2','6',10,10,100),(7,'PR-5','6',10,20,200);
/*!40000 ALTER TABLE `purchase_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchase_detail_tax`
--

DROP TABLE IF EXISTS `purchase_detail_tax`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchase_detail_tax` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PURCHASE_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_PRICE` double DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT NULL,
  `PURCHASE_SUBTOTAL` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase_detail_tax`
--

LOCK TABLES `purchase_detail_tax` WRITE;
/*!40000 ALTER TABLE `purchase_detail_tax` DISABLE KEYS */;
/*!40000 ALTER TABLE `purchase_detail_tax` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchase_header`
--

DROP TABLE IF EXISTS `purchase_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchase_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PURCHASE_INVOICE` varchar(30) DEFAULT NULL,
  `SUPPLIER_ID` smallint(5) unsigned DEFAULT NULL,
  `PURCHASE_DATETIME` date DEFAULT NULL,
  `PURCHASE_TOTAL` double DEFAULT NULL,
  `PURCHASE_TERM_OF_PAYMENT` tinyint(3) unsigned DEFAULT NULL,
  `PURCHASE_TERM_OF_PAYMENT_DURATION` double unsigned DEFAULT '0',
  `PURCHASE_DATE_RECEIVED` date DEFAULT NULL,
  `PURCHASE_TERM_OF_PAYMENT_DATE` date DEFAULT NULL,
  `PURCHASE_PAID` tinyint(3) unsigned DEFAULT '0',
  `PURCHASE_SENT` tinyint(3) unsigned DEFAULT '0',
  `PURCHASE_RECEIVED` tinyint(3) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `PURCHASE_INVOICE_UNIQUE` (`PURCHASE_INVOICE`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase_header`
--

LOCK TABLES `purchase_header` WRITE;
/*!40000 ALTER TABLE `purchase_header` DISABLE KEYS */;
INSERT INTO `purchase_header` VALUES (1,'PO-1',1,'2016-09-02',220,1,20,'2016-09-03','2016-09-23',0,1,1),(2,'PR-2',1,'2017-02-18',200,1,0,'2017-02-18','2017-02-18',0,1,1),(3,'PR-3',1,'2017-02-18',100,1,0,'2017-02-18','2017-02-18',0,1,1),(4,'PO-2',2,'2017-02-18',300,0,0,'2017-02-18','2017-02-18',1,1,1),(5,'PR-5',1,'2017-02-18',200,1,10,'2017-02-18','2017-02-28',0,1,1);
/*!40000 ALTER TABLE `purchase_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchase_header_tax`
--

DROP TABLE IF EXISTS `purchase_header_tax`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchase_header_tax` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PURCHASE_INVOICE` varchar(30) DEFAULT NULL,
  `SUPPLIER_ID` smallint(5) unsigned DEFAULT NULL,
  `PURCHASE_DATETIME` date DEFAULT NULL,
  `PURCHASE_TOTAL` double DEFAULT NULL,
  `PURCHASE_TERM_OF_PAYMENT` tinyint(3) unsigned DEFAULT NULL,
  `PURCHASE_TERM_OF_PAYMENT_DURATION` double unsigned DEFAULT '0',
  `PURCHASE_DATE_RECEIVED` date DEFAULT NULL,
  `PURCHASE_TERM_OF_PAYMENT_DATE` date DEFAULT NULL,
  `PURCHASE_PAID` tinyint(3) unsigned DEFAULT '0',
  `PURCHASE_SENT` tinyint(3) unsigned DEFAULT '0',
  `PURCHASE_RECEIVED` tinyint(3) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `PURCHASE_INVOICE_UNIQUE` (`PURCHASE_INVOICE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase_header_tax`
--

LOCK TABLES `purchase_header_tax` WRITE;
/*!40000 ALTER TABLE `purchase_header_tax` DISABLE KEYS */;
/*!40000 ALTER TABLE `purchase_header_tax` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `request_order_detail`
--

DROP TABLE IF EXISTS `request_order_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `request_order_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RO_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_BASE_PRICE` double DEFAULT NULL,
  `RO_QTY` double DEFAULT NULL,
  `RO_SUBTOTAL` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `request_order_detail`
--

LOCK TABLES `request_order_detail` WRITE;
/*!40000 ALTER TABLE `request_order_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `request_order_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `request_order_header`
--

DROP TABLE IF EXISTS `request_order_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `request_order_header` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RO_INVOICE` varchar(30) DEFAULT NULL,
  `RO_BRANCH_ID_FROM` tinyint(3) unsigned DEFAULT NULL,
  `RO_BRANCH_ID_TO` tinyint(3) unsigned DEFAULT NULL,
  `RO_DATETIME` date DEFAULT NULL,
  `RO_TOTAL` double DEFAULT NULL,
  `RO_EXPIRED` date DEFAULT NULL,
  `RO_ACTIVE` tinyint(4) DEFAULT NULL,
  `RO_EXPORTED` tinyint(4) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `RO_INVOICE_UNIQUE` (`RO_INVOICE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `request_order_header`
--

LOCK TABLES `request_order_header` WRITE;
/*!40000 ALTER TABLE `request_order_header` DISABLE KEYS */;
/*!40000 ALTER TABLE `request_order_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `return_purchase_detail`
--

DROP TABLE IF EXISTS `return_purchase_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `return_purchase_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RP_ID` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_BASEPRICE` double DEFAULT '0',
  `PRODUCT_QTY` double DEFAULT '0',
  `RP_DESCRIPTION` varchar(100) DEFAULT NULL,
  `RP_SUBTOTAL` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `return_purchase_detail`
--

LOCK TABLES `return_purchase_detail` WRITE;
/*!40000 ALTER TABLE `return_purchase_detail` DISABLE KEYS */;
INSERT INTO `return_purchase_detail` VALUES (1,'RT-1','1',10,10,' ',100),(2,'RT-4','6',10,100,' ',1000);
/*!40000 ALTER TABLE `return_purchase_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `return_purchase_header`
--

DROP TABLE IF EXISTS `return_purchase_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `return_purchase_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RP_ID` varchar(30) DEFAULT NULL,
  `SUPPLIER_ID` smallint(5) unsigned DEFAULT NULL,
  `RP_DATE` date DEFAULT NULL,
  `RP_TOTAL` double DEFAULT '0',
  `RP_PROCESSED` tinyint(3) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `RP_INVOICE_UNIQUE` (`RP_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `return_purchase_header`
--

LOCK TABLES `return_purchase_header` WRITE;
/*!40000 ALTER TABLE `return_purchase_header` DISABLE KEYS */;
INSERT INTO `return_purchase_header` VALUES (1,'RT-1',1,'2017-02-18',100,1),(2,'RT-4',1,'2017-02-18',1000,1);
/*!40000 ALTER TABLE `return_purchase_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `return_sales_detail`
--

DROP TABLE IF EXISTS `return_sales_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `return_sales_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RS_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_SALES_PRICE` double DEFAULT '0',
  `PRODUCT_SALES_QTY` double DEFAULT '0',
  `PRODUCT_RETURN_QTY` double DEFAULT '0',
  `RS_DESCRIPTION` varchar(100) DEFAULT NULL,
  `RS_SUBTOTAL` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `return_sales_detail`
--

LOCK TABLES `return_sales_detail` WRITE;
/*!40000 ALTER TABLE `return_sales_detail` DISABLE KEYS */;
INSERT INTO `return_sales_detail` VALUES (1,'RT-1','1',1,2,2,' ',2);
/*!40000 ALTER TABLE `return_sales_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `return_sales_header`
--

DROP TABLE IF EXISTS `return_sales_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `return_sales_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RS_INVOICE` varchar(30) DEFAULT NULL,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `CUSTOMER_ID` smallint(5) unsigned DEFAULT NULL,
  `RS_DATETIME` date DEFAULT NULL,
  `RS_TOTAL` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `return_sales_header`
--

LOCK TABLES `return_sales_header` WRITE;
/*!40000 ALTER TABLE `return_sales_header` DISABLE KEYS */;
INSERT INTO `return_sales_header` VALUES (1,'RT-1','SLO001-1',0,'2017-02-20',2);
/*!40000 ALTER TABLE `return_sales_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_commission`
--

DROP TABLE IF EXISTS `sales_commission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_commission` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `DATE_START` date DEFAULT NULL,
  `DATE_END` date DEFAULT NULL,
  `SALES_ID` varchar(45) DEFAULT NULL,
  `SALES_PARAMETER` double DEFAULT NULL,
  `COMMISSION_AMOUNT` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_commission`
--

LOCK TABLES `sales_commission` WRITE;
/*!40000 ALTER TABLE `sales_commission` DISABLE KEYS */;
/*!40000 ALTER TABLE `sales_commission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_detail`
--

DROP TABLE IF EXISTS `sales_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `REV_NO` tinyint(3) DEFAULT '0',
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_PRICE` double DEFAULT '0',
  `PRODUCT_SALES_PRICE` double DEFAULT '0',
  `PRODUCT_QTY` double DEFAULT '0',
  `PRODUCT_DISC1` double DEFAULT '0',
  `PRODUCT_DISC2` double DEFAULT '0',
  `PRODUCT_DISC_RP` double DEFAULT '0',
  `SALES_SUBTOTAL` double DEFAULT '0',
  `IS_COMPLETED` tinyint(3) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_detail`
--

LOCK TABLES `sales_detail` WRITE;
/*!40000 ALTER TABLE `sales_detail` DISABLE KEYS */;
INSERT INTO `sales_detail` VALUES (1,'SLO001-1',0,'1',10,1,2,0,0,0,2,NULL),(2,'SLO001-2',0,'1',10,1,2,0,0,0,2,NULL),(3,'SLO001-3',0,'1',10,1,2,0,0,0,0,NULL),(4,'SLO001-4',0,'1',10,1,2,0,0,0,2,NULL),(5,'SLO001-5',0,'1',10,1,2,0,0,0,0,NULL),(6,'SLO001-6',0,'1',10,1,2,0,0,0,0,NULL),(7,'SLO001-7',0,'1',10,1,2,0,0,0,0,NULL),(8,'SLO001-8',0,'1',10,200,2,0,0,0,400,NULL),(9,'SLO001-9',0,'1',0,200,2,0,0,0,400,NULL),(10,'SLO001-10',0,'1',0,100,2,0,0,0,200,NULL),(11,'SLO001-11',0,'2',0,250,2,0,0,0,500,NULL),(12,'SLO001-12',0,'6',10,10,20,0,0,0,200,NULL),(13,'SLO001-13',0,'6',10,10,20,0,0,0,200,NULL),(14,'SLO001-12',1,'6',10,10,20,0,0,0,200,NULL);
/*!40000 ALTER TABLE `sales_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_detail_tax`
--

DROP TABLE IF EXISTS `sales_detail_tax`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_detail_tax` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_PRICE` double DEFAULT '0',
  `PRODUCT_SALES_PRICE` double DEFAULT '0',
  `PRODUCT_QTY` double DEFAULT '0',
  `PRODUCT_DISC1` double DEFAULT '0',
  `PRODUCT_DISC2` double DEFAULT '0',
  `PRODUCT_DISC_RP` double DEFAULT '0',
  `SALES_SUBTOTAL` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_detail_tax`
--

LOCK TABLES `sales_detail_tax` WRITE;
/*!40000 ALTER TABLE `sales_detail_tax` DISABLE KEYS */;
INSERT INTO `sales_detail_tax` VALUES (1,'SLO001-1','1',0,0,2,0,0,0,0),(2,'SLO001-2','1',0,0,2,0,0,0,0),(3,'SLO001-3','1',0,0,2,0,0,0,0);
/*!40000 ALTER TABLE `sales_detail_tax` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_header`
--

DROP TABLE IF EXISTS `sales_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `REV_NO` tinyint(3) DEFAULT '0',
  `CUSTOMER_ID` smallint(5) unsigned DEFAULT '0',
  `SALES_DATE` datetime DEFAULT NULL,
  `SALES_TOTAL` double DEFAULT '0',
  `SALES_DISC_PERCENT_FINAL` double DEFAULT '0',
  `SALES_DISCOUNT_FINAL` double DEFAULT '0',
  `SALES_TOP` tinyint(3) unsigned DEFAULT '0',
  `SALES_TOP_DATE` date DEFAULT NULL,
  `SALES_PAID` tinyint(3) unsigned DEFAULT '0',
  `SALES_PAYMENT` double unsigned DEFAULT '0',
  `SALES_PAYMENT_CHANGE` double unsigned DEFAULT '0',
  `SALES_PAYMENT_METHOD` tinyint(3) unsigned DEFAULT '0',
  `SQ_INVOICE` varchar(30) DEFAULT NULL,
  `SALES_VOID` tinyint(3) DEFAULT '0',
  `SALES_ACTIVE` tinyint(3) DEFAULT '1',
  `IS_SERVICE` tinyint(3) DEFAULT '0',
  `IS_PREORDER` tinyint(3) DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_header`
--

LOCK TABLES `sales_header` WRITE;
/*!40000 ALTER TABLE `sales_header` DISABLE KEYS */;
INSERT INTO `sales_header` VALUES (1,'SLO001-1',0,0,'2016-09-03 00:15:00',2,0,0,0,'2016-09-05',1,0,0,0,NULL,0,1,0,0),(2,'SLO001-2',0,0,'2016-09-27 15:26:00',2,0,0,1,'2016-09-27',1,2,0,0,NULL,0,1,1,0),(4,'SLO001-3',0,1,'2016-09-30 00:19:00',0,0,0,1,'2016-09-30',1,0,0,0,NULL,0,1,0,0),(5,'SLO001-4',0,1,'2016-09-30 00:30:00',2,0,0,1,'2016-09-30',1,0,0,0,NULL,0,0,0,0),(6,'SLO001-5',0,1,'2016-09-30 00:33:00',0,0,0,1,'2016-09-30',1,0,0,0,NULL,0,1,0,0),(7,'SLO001-6',0,1,'2016-09-30 00:34:00',0,0,0,1,'2016-09-30',1,0,0,0,NULL,0,1,0,0),(8,'SLO001-7',0,1,'2016-09-30 00:49:00',0,0,0,1,'2016-09-30',1,0,0,0,NULL,0,1,0,0),(9,'SLO001-8',0,1,'2016-10-01 00:42:00',400,0,0,1,'2016-10-01',1,1000,600,0,NULL,0,0,0,0),(10,'SLO001-9',0,0,'2016-10-01 14:28:00',400,0,0,1,'2016-10-01',1,400,0,0,NULL,0,1,0,0),(11,'SLO001-10',0,1,'2016-10-17 15:11:00',200,10,20,1,'2016-10-17',1,200,20,0,NULL,0,0,0,0),(12,'SLO001-11',0,2,'2016-11-02 12:30:00',500,0,0,1,'2016-11-02',1,500,0,0,NULL,0,1,0,0),(13,'SLO001-12',0,0,'2017-02-18 15:07:00',200,10,50,1,'2017-02-18',1,150,20,0,NULL,1,0,0,0),(14,'SLO001-13',0,0,'2017-02-18 15:12:00',200,10,50,1,'2017-02-18',1,150,20,0,NULL,0,1,0,0),(15,'SLO001-12',1,0,'2017-02-18 23:57:00',200,20,70,1,'2017-02-18',1,150,60,0,'',0,1,0,0);
/*!40000 ALTER TABLE `sales_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_header_tax`
--

DROP TABLE IF EXISTS `sales_header_tax`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_header_tax` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `CUSTOMER_ID` smallint(5) unsigned DEFAULT '0',
  `SALES_DATE` datetime DEFAULT NULL,
  `SALES_TOTAL` double DEFAULT '0',
  `SALES_DISCOUNT_FINAL` double DEFAULT '0',
  `SALES_TOP` tinyint(3) unsigned DEFAULT '0',
  `SALES_TOP_DATE` date DEFAULT NULL,
  `SALES_PAID` tinyint(3) unsigned DEFAULT '0',
  `SALES_PAYMENT` double unsigned DEFAULT '0',
  `SALES_PAYMENT_CHANGE` double unsigned DEFAULT '0',
  `SALES_PAYMENT_METHOD` tinyint(3) unsigned DEFAULT '0',
  `SQ_INVOICE` varchar(30) DEFAULT NULL,
  `ORIGIN_SALES_INVOICE` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `SALES_INVOICE_UNIQUE` (`SALES_INVOICE`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_header_tax`
--

LOCK TABLES `sales_header_tax` WRITE;
/*!40000 ALTER TABLE `sales_header_tax` DISABLE KEYS */;
INSERT INTO `sales_header_tax` VALUES (1,'SLO001-1',1,'2016-10-01 01:06:00',0,0,1,'2016-10-01',1,0,0,0,NULL,'0'),(2,'SLO001-2',0,'2016-10-01 22:20:00',0,0,1,'2016-10-01',1,0,0,0,NULL,'0'),(3,'SLO001-3',0,'2016-10-01 22:29:00',0,0,1,'2016-10-01',1,0,0,0,NULL,'0');
/*!40000 ALTER TABLE `sales_header_tax` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_quotation_detail`
--

DROP TABLE IF EXISTS `sales_quotation_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_quotation_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SQ_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_SALES_PRICE` double DEFAULT '0',
  `PRODUCT_QTY` double DEFAULT '0',
  `SQ_SUBTOTAL` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_quotation_detail`
--

LOCK TABLES `sales_quotation_detail` WRITE;
/*!40000 ALTER TABLE `sales_quotation_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `sales_quotation_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_quotation_header`
--

DROP TABLE IF EXISTS `sales_quotation_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_quotation_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SQ_INVOICE` varchar(30) DEFAULT NULL,
  `CUSTOMER_ID` smallint(5) unsigned DEFAULT '0',
  `SQ_DATE` datetime DEFAULT NULL,
  `SQ_TOTAL` double DEFAULT '0',
  `SALES_DISCOUNT_FINAL` double DEFAULT '0',
  `SQ_TOP` tinyint(3) unsigned DEFAULT '0',
  `SQ_TOP_DATE` date DEFAULT NULL,
  `SQ_APPROVED` tinyint(3) unsigned DEFAULT '0',
  `SQ_APPROVED_DATE` date DEFAULT NULL,
  `SALESPERSON_ID` tinyint(3) DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `SQ_INVOICE_UNIQUE` (`SQ_INVOICE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_quotation_header`
--

LOCK TABLES `sales_quotation_header` WRITE;
/*!40000 ALTER TABLE `sales_quotation_header` DISABLE KEYS */;
/*!40000 ALTER TABLE `sales_quotation_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `surat_tugas_detail`
--

DROP TABLE IF EXISTS `surat_tugas_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `surat_tugas_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `surat_tugas_detail`
--

LOCK TABLES `surat_tugas_detail` WRITE;
/*!40000 ALTER TABLE `surat_tugas_detail` DISABLE KEYS */;
INSERT INTO `surat_tugas_detail` VALUES (8,'SLO001-8','1',2),(10,'SLO001-9','1',2),(11,'SLO001-7','1',2),(12,'SLO001-6','1',2),(13,'SLO001-10','1',2),(15,'SLO001-13','6',20),(20,'SLO001-12','6',20),(21,'SLO001-12','6',20);
/*!40000 ALTER TABLE `surat_tugas_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `surat_tugas_header`
--

DROP TABLE IF EXISTS `surat_tugas_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `surat_tugas_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `CUSTOMER_ID` smallint(5) unsigned DEFAULT '0',
  `SALES_DATE` datetime DEFAULT NULL,
  `TECHNICIAN_ID` smallint(6) DEFAULT '0',
  `JOB_SCHEDULED_DATE` date DEFAULT NULL,
  `JOB_SCHEDULED_TIME` time DEFAULT NULL,
  `JOB_FINISHED_DATE` date DEFAULT NULL,
  `JOB_FINISHED_TIME` time DEFAULT NULL,
  `IS_JOB_FINISHED` tinyint(3) DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `surat_tugas_header`
--

LOCK TABLES `surat_tugas_header` WRITE;
/*!40000 ALTER TABLE `surat_tugas_header` DISABLE KEYS */;
INSERT INTO `surat_tugas_header` VALUES (9,'SLO001-8',1,'2016-10-01 01:06:00',1,'2016-10-01','08:00:00','2016-10-06','10:00:00',0),(10,'SLO001-9',2,'2016-10-01 22:20:00',1,'2016-10-01','10:00:00','2016-10-01','20:00:00',1),(11,'SLO001-7',1,'2016-10-07 12:47:00',2,'2016-10-07','10:00:00','2016-10-07','00:00:00',0),(12,'SLO001-6',1,'2016-10-07 12:51:00',3,'2016-10-07','20:00:00','2016-10-07','00:00:00',0),(13,'SLO001-10',1,'2016-10-17 15:18:00',0,'2016-11-01','10:00:00','2016-10-17','00:00:00',0),(14,'SLO001-13',0,'2017-02-19 22:46:00',1,'2017-02-19','00:00:00','2017-02-19','00:00:00',0),(15,'SLO001-12',0,'2017-02-19 23:33:00',1,'2017-02-19','10:00:00','2017-02-19','00:00:00',1);
/*!40000 ALTER TABLE `surat_tugas_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_config`
--

DROP TABLE IF EXISTS `sys_config`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_config` (
  `id` tinyint(3) unsigned NOT NULL,
  `no_faktur` varchar(30) NOT NULL DEFAULT '',
  `branch_id` tinyint(3) unsigned DEFAULT '0',
  `location_id` tinyint(3) unsigned DEFAULT '0',
  `HQ_IP4` varchar(15) DEFAULT NULL,
  `store_name` varchar(50) DEFAULT NULL,
  `store_address` varchar(100) DEFAULT NULL,
  `store_phone` varchar(20) DEFAULT NULL,
  `store_email` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_config`
--

LOCK TABLES `sys_config` WRITE;
/*!40000 ALTER TABLE `sys_config` DISABLE KEYS */;
INSERT INTO `sys_config` VALUES (1,'SLO001',0,0,'127.0.0.1',NULL,NULL,NULL,NULL),(2,'',1,2,'127.0.0.1','AA','AA','11','aa');
/*!40000 ALTER TABLE `sys_config` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_config_tax`
--

DROP TABLE IF EXISTS `sys_config_tax`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_config_tax` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PERSENTASE_PENJUALAN` int(11) DEFAULT '0',
  `PERSENTASE_PEMBELIAN` int(11) DEFAULT '0',
  `AVERAGE_PENJUALAN_HARIAN` double DEFAULT '0',
  `AVERAGE_PEMBELIAN_HARIAN` double DEFAULT '0',
  `RASIO_TOLERANSI` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_config_tax`
--

LOCK TABLES `sys_config_tax` WRITE;
/*!40000 ALTER TABLE `sys_config_tax` DISABLE KEYS */;
/*!40000 ALTER TABLE `sys_config_tax` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `temp_master_product`
--

DROP TABLE IF EXISTS `temp_master_product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `temp_master_product` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_BARCODE` int(10) unsigned DEFAULT NULL,
  `PRODUCT_NAME` varchar(50) DEFAULT NULL,
  `PRODUCT_DESCRIPTION` varchar(100) DEFAULT NULL,
  `PRODUCT_BASE_PRICE` double DEFAULT NULL,
  `PRODUCT_RETAIL_PRICE` double DEFAULT NULL,
  `PRODUCT_BULK_PRICE` double DEFAULT NULL,
  `PRODUCT_WHOLESALE_PRICE` double DEFAULT NULL,
  `UNIT_ID` smallint(5) unsigned DEFAULT '0',
  `PRODUCT_IS_SERVICE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `PRODUCT_ID_UNIQUE` (`PRODUCT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `temp_master_product`
--

LOCK TABLES `temp_master_product` WRITE;
/*!40000 ALTER TABLE `temp_master_product` DISABLE KEYS */;
/*!40000 ALTER TABLE `temp_master_product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `temp_product_category`
--

DROP TABLE IF EXISTS `temp_product_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `temp_product_category` (
  `PRODUCT_ID` varchar(50) NOT NULL,
  `CATEGORY_ID` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY (`PRODUCT_ID`,`CATEGORY_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `temp_product_category`
--

LOCK TABLES `temp_product_category` WRITE;
/*!40000 ALTER TABLE `temp_product_category` DISABLE KEYS */;
/*!40000 ALTER TABLE `temp_product_category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `unit_convert`
--

DROP TABLE IF EXISTS `unit_convert`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `unit_convert` (
  `CONVERT_UNIT_ID_1` smallint(5) unsigned NOT NULL,
  `CONVERT_UNIT_ID_2` smallint(5) unsigned NOT NULL,
  `CONVERT_MULTIPLIER` float DEFAULT NULL,
  PRIMARY KEY (`CONVERT_UNIT_ID_1`,`CONVERT_UNIT_ID_2`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `unit_convert`
--

LOCK TABLES `unit_convert` WRITE;
/*!40000 ALTER TABLE `unit_convert` DISABLE KEYS */;
INSERT INTO `unit_convert` VALUES (2,1,2);
/*!40000 ALTER TABLE `unit_convert` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_access_management`
--

DROP TABLE IF EXISTS `user_access_management`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_access_management` (
  `ID` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `GROUP_ID` tinyint(3) unsigned DEFAULT NULL,
  `MODULE_ID` smallint(5) unsigned DEFAULT NULL,
  `USER_ACCESS_OPTION` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=67 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_access_management`
--

LOCK TABLES `user_access_management` WRITE;
/*!40000 ALTER TABLE `user_access_management` DISABLE KEYS */;
INSERT INTO `user_access_management` VALUES (1,1,1,1),(2,1,2,1),(3,1,3,6),(4,1,6,1),(5,1,7,1),(6,1,8,1),(7,1,101,1),(8,1,102,1),(9,1,103,6),(10,1,104,1),(11,1,105,1),(12,1,106,1),(13,1,107,1),(14,1,109,6),(15,1,110,1),(16,1,111,1),(17,1,112,1),(18,1,113,1),(19,1,114,1),(20,1,118,1),(21,1,119,1),(22,1,120,1),(23,1,121,1),(24,1,201,1),(25,1,202,1),(26,1,204,6),(27,1,205,1),(28,1,206,1),(29,1,301,1),(30,1,302,6),(31,1,303,1),(32,1,304,1),(33,1,305,1),(34,1,306,1),(35,1,307,1),(36,1,308,1),(37,1,309,1),(38,1,310,1),(39,1,311,1),(40,1,313,1),(41,1,401,1),(42,1,402,6),(43,1,403,1),(44,1,404,1),(45,1,405,1),(46,1,407,1),(47,1,408,1),(48,1,501,1),(49,1,502,1),(50,1,800,1),(51,1,801,1),(52,1,802,1),(53,1,803,1),(54,1,804,1),(55,1,805,1),(56,1,806,1),(57,1,807,1),(58,1,808,1),(59,1,809,1),(60,1,810,1),(61,1,811,1),(62,1,812,1),(63,1,813,1),(64,1,814,1),(65,1,815,1),(66,1,4,6);
/*!40000 ALTER TABLE `user_access_management` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_change_log`
--

DROP TABLE IF EXISTS `user_change_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_change_log` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `USER_ID` tinyint(3) unsigned DEFAULT NULL,
  `MODULE_ID` smallint(6) DEFAULT NULL,
  `CHANGE_ID` tinyint(3) unsigned DEFAULT NULL,
  `CHANGE_DATETIME` datetime DEFAULT NULL,
  `CHANGE_DESCRIPTION` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_change_log`
--

LOCK TABLES `user_change_log` WRITE;
/*!40000 ALTER TABLE `user_change_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_change_log` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-02-20 23:21:23
