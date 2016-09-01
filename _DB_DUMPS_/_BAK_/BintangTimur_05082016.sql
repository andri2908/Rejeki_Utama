CREATE DATABASE  IF NOT EXISTS `sys_pos_bintangtimur` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `sys_pos_bintangtimur`;
-- MySQL dump 10.13  Distrib 5.5.16, for Win32 (x86)
--
-- Host: localhost    Database: sys_pos_bintangtimur
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `credit`
--

LOCK TABLES `credit` WRITE;
/*!40000 ALTER TABLE `credit` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer_product_disc`
--

LOCK TABLES `customer_product_disc` WRITE;
/*!40000 ALTER TABLE `customer_product_disc` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `daily_journal`
--

LOCK TABLES `daily_journal` WRITE;
/*!40000 ALTER TABLE `daily_journal` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `debt`
--

LOCK TABLES `debt` WRITE;
/*!40000 ALTER TABLE `debt` DISABLE KEYS */;
/*!40000 ALTER TABLE `debt` ENABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_branch`
--

LOCK TABLES `master_branch` WRITE;
/*!40000 ALTER TABLE `master_branch` DISABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_category`
--

LOCK TABLES `master_category` WRITE;
/*!40000 ALTER TABLE `master_category` DISABLE KEYS */;
INSERT INTO `master_category` VALUES (3,'BESI BETON','PANJANG 12 M',1),(4,'ALM','ALUMUNIUM',1),(5,'PLAT SS','PLAT STAINLESS',1),(6,'PLAT BESI/ HITAM','PLAT BESI/ HITAM',1),(7,'BORDES BESI/ HITAM','BORDES BESI/ HITAM',1),(8,'CNP','CNP',1),(9,'UNP','UNP',1),(10,'BENDRAT','ROLL',1);
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
  PRIMARY KEY (`CUSTOMER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_customer`
--

LOCK TABLES `master_customer` WRITE;
/*!40000 ALTER TABLE `master_customer` DISABLE KEYS */;
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
INSERT INTO `master_group` VALUES (1,'GLOBAL_ADMIN','GLOBAL ADMIN GROUP',1,0),(2,'NORMAL USER','NORMAL USER',1,1);
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
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_location`
--

LOCK TABLES `master_location` WRITE;
/*!40000 ALTER TABLE `master_location` DISABLE KEYS */;
INSERT INTO `master_location` VALUES (1,'TOKO',0),(2,'GUDANG',0);
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_message`
--

LOCK TABLES `master_message` WRITE;
/*!40000 ALTER TABLE `master_message` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_message` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_module`
--

DROP TABLE IF EXISTS `master_module`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_module` (
  `MODULE_ID` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `MODULE_NAME` varchar(50) DEFAULT NULL,
  `MODULE_DESCRIPTION` varchar(100) DEFAULT NULL,
  `MODULE_FEATURES` tinyint(3) unsigned DEFAULT NULL,
  `MODULE_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`MODULE_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=54 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_module`
--

LOCK TABLES `master_module` WRITE;
/*!40000 ALTER TABLE `master_module` DISABLE KEYS */;
INSERT INTO `master_module` VALUES (1,'MANAJEMEN SISTEM',NULL,1,1),(2,'DATABASE',NULL,1,1),(3,'MANAJEMEN USER',NULL,3,1),(4,'MANAJEMEN CABANG',NULL,3,1),(5,'SINKRONISASI INFORMASI',NULL,1,1),(6,'PENGATURAN PRINTER',NULL,1,1),(7,'PENGATURAN GAMBAR LATAR',NULL,1,1),(8,'GUDANG',NULL,1,1),(9,'PRODUK',NULL,1,1),(10,'TAMBAH / UPDATE PRODUK',NULL,3,1),(11,'PENGATURAN HARGA PRODUK',NULL,1,1),(12,'PENGATURAN LIMIT STOK',NULL,1,1),(13,'PENGATURAN KATEGORI PRODUK',NULL,1,1),(14,'PECAH SATUAN PRODUK',NULL,1,1),(15,'PENGATURAN NOMOR RAK',NULL,1,1),(16,'KATEGORI PRODUK',NULL,3,1),(17,'SATUAN PRODUK',NULL,1,1),(18,'TAMBAH / UPDATE SATUAN',NULL,1,1),(19,'PENGATURAN KONVERSI SATUAN',NULL,1,1),(20,'STOK OPNAME',NULL,1,1),(21,'PENYESUAIAN STOK',NULL,1,1),(22,'MUTASI BARANG',NULL,1,1),(23,'TAMBAH / UPDATE MUTASI BARANG',NULL,3,1),(24,'CEK PERMINTAAN BARANG',NULL,1,1),(25,'PENERIMAAN BARANG',NULL,1,1),(26,'PENERIMAAN BARANG DARI MUTASI',NULL,1,1),(27,'PENERIMAAN BARANG DARI PO',NULL,1,1),(28,'PEMBELIAN',NULL,1,1),(29,'SUPPLIER',NULL,1,1),(30,'REQUEST ORDER',NULL,3,1),(31,'PURCHASE ORDER',NULL,3,1),(32,'REPRINT REQUEST ORDER',NULL,1,1),(33,'RETUR PEMBELIAN KE SUPPLIER',NULL,1,1),(34,'RETUR PERMINTAAN KE PUSAT',NULL,1,1),(35,'PENJUALAN',NULL,1,1),(36,'PELANGGAN',NULL,3,1),(37,'TRANSAKSI PENJUALAN',NULL,1,1),(38,'SET NO FAKTUR',NULL,1,1),(39,'RETUR PENJUALAN',NULL,1,1),(40,'RETUR PENJUALAN BY INVOICE',NULL,1,1),(41,'RETUR PENJUALAN BY STOK',NULL,1,1),(42,'KEUANGAN',NULL,1,1),(43,'PENGATURAN NO AKUN',NULL,3,1),(44,'TRANSAKSI',NULL,1,1),(45,'TAMBAH TRANSAKSI HARIAN',NULL,1,1),(46,'PEMBAYARAN PIUTANG',NULL,1,1),(47,'PEMBAYARAN PIUTANG MUTASI',NULL,1,1),(48,'PEMBAYARAN HUTANG KE SUPPLIER',NULL,1,1),(49,'PENGATURAN LIMIT PAJAK',NULL,1,1),(50,'MODUL MESSAGING',NULL,1,1),(51,'TAX_MODULE',NULL,1,1),(52,'SALES QUOTATION',NULL,1,1),(53,'APPROVAL SALES QUOTATION',NULL,1,1);
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
) ENGINE=InnoDB AUTO_INCREMENT=123 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_product`
--

LOCK TABLES `master_product` WRITE;
/*!40000 ALTER TABLE `master_product` DISABLE KEYS */;
INSERT INTO `master_product` VALUES (6,'1',NULL,'BESI BETON 8.0MMX12MTR','BESI BETON 8.0X12',28000,30000,28500,29000,'1.jpg',6,8773,100,'--00',1,'PSI',0),(7,'2',NULL,'ALUMUNIUM 1.4 KG','ALUMUNIUM 100 X 200',49500,51000,50000,50500,'2.jpg',8,0,100,'--00',1,'INTIBUMI',0),(8,'3',NULL,'ALUMUNIUM 1.65 KG','ALUMUNIUM 100 X 200',58500,60000,59000,59500,'3.jpg',8,0,0,'--00',1,'INTIBUMI',0),(9,'4',NULL,'ALUMUNIUM 1.85 KG','ALUMUNIUM 100 X 200',65500,67000,66000,66500,'4.jpg',8,0,0,'--00',1,'INTIBUMI',0),(10,'5',NULL,'ALUMUNIUM 1.95 KG','ALUMUNIUM 100 X 200',69500,71000,70000,70500,'5.jpg',8,0,0,'--00',1,'INTIBUMI',0),(11,'6',NULL,'ALUMUNIUM 2.0 KG','ALUMUNIUM 100 X 200',70500,72000,71000,71500,'6.jpg',8,0,0,'--00',1,'INTIBUMI',0),(12,'7',NULL,'ALUMUNIUM 2.35 KG','ALUMUNIUM 100 X 200',82500,84000,83000,83500,'7.jpg',8,0,0,'--00',1,'INTIBUMI',0),(13,'8',NULL,'ALUMUNIUM 2.6 KG','ALUMUNIUM 100 X 200',91500,93000,92000,92500,'8.jpg',8,0,0,'--00',1,'INTIBUMI',0),(14,'9',NULL,'ALUMUNIUM 2.8 KG','ALUMUNIUM 100 X 200',98500,100000,99000,99500,'9.jpg',8,0,0,'--00',1,'INTIBUMI',0),(15,'10',NULL,'ALUMUNIUM 3.0 KG','ALUMUNIUM 100 X 200',106500,108000,107000,107500,'10.jpg',8,0,0,'--00',1,'INTIBUMI',0),(29,'11',NULL,'ALUMUNIUM 3.3 KG','ALUMUNIUM 100 X 200',116500,118000,117000,117500,'11.jpg',8,0,0,'--00',1,'INTI BUMI',0),(30,'12',NULL,'ALUMUNIUM 3.6 KG','ALUMUNIUM 100 X 200',127500,129000,128000,128500,'12.jpg',8,0,0,'--00',1,'INTIBUMI',0),(31,'13',NULL,'ALUMUNIUM 3.85 KG','ALUMUNIUM 100 X 200',136500,138000,137000,137500,'13.jpg',8,0,0,'--00',1,'INTIBUMI',0),(32,'14',NULL,'ALUMUNIUM 4.1 KG','ALUMUNIUM 100 X 200',145500,147000,146000,146500,'14.jpg',8,0,0,'--00',1,'INTIBUMI',0),(33,'15',NULL,'ALUMUNIUM 4.3 KG','ALUMUNIUM 100 X 200',152500,154000,153000,153500,'15.jpg',8,0,0,'--00',1,'INTIBUMI',0),(34,'16',NULL,'ALUMUNIUM 4.6 KG','ALUMUNIUM 100 X 200',163500,165000,164000,164500,'16.jpg',8,0,0,'--00',1,'INTIBUMI',0),(35,'17',NULL,'ALUMUNIUM 5.2 KG','ALUMUNIUM 100 X 200',185500,187000,186000,186500,'17.jpg',8,0,0,'--00',1,'INTIBUMI',0),(36,'18',NULL,'ALUMUNIUM 0.5 (120 X 240)','ALUMUNIUM 120 X 240',137500,139000,138000,138500,'18.jpg',8,0,0,'--00',1,'INTIBUMI',0),(37,'19',NULL,'ALUMUNIUM 0.8 (120 X 240)','ALUMUNIUM 120 X 240',208500,210000,209000,209500,'19.jpg',8,0,0,'--00',1,'INTBUMI',0),(38,'20',NULL,'ALUMUNIUM 6.4 KG (1.2 MM)','ALUMUNIUM 100 X 200',232500,234000,233000,233500,'20.jpg',8,0,0,'--00',1,'INTIBUMI',0),(39,'21',NULL,'ALUMUNIUM 7.6 KG (1.5 MM)','ALUMUNIUM 100 X 200',276000,277500,276500,277000,'21.jpg',8,0,0,'--00',1,'INTIBUMI',0),(40,'22',NULL,'ALUMUNIUM 9.8 KG (2 MM)','ALUMUNIUM 100 X 200',356500,358000,357000,357500,'22.jpg',8,0,0,'--00',1,'INTIBUMI',0),(41,'23',NULL,'ALUMUNIUM 15.2 KG (3 MM)','ALUMUNIUM 100 X 200',553500,555000,554000,554500,'23.jpg',8,0,0,'--00',1,'INTIBUMI',0),(42,'24',NULL,'ALUMUNIUM 21.5 KG (4 MM)','ALUMUNIUM 100 X 200',783500,785000,784000,784500,'24.jpg',8,0,0,'--00',1,'INTIBUMI',0),(43,'25',NULL,'PLAT SS 430 0.3 MM','PLAT SS 430 100 X 200',103500,105000,104000,104500,' ',8,0,0,'--00',1,'JINDAL',0),(44,'26',NULL,'PLAT SS 430 0.4 MM','PLAT SS 430 100 X 200',127500,129000,128000,128500,' ',8,0,0,'--00',1,'JINDAL',0),(45,'27',NULL,'PLAT SS 430 0.4 MM TEBAL','PLAT SS 430 100 X 200',133500,135000,134000,134500,' ',8,0,0,'--00',1,'JINDAL',0),(46,'28',NULL,'PLAT SS 430 0.5 MM','PLAT SS 430 100 X 200',182500,184000,183000,183500,' ',8,0,0,'--00',1,'JINDAL',0),(47,'29',NULL,'PLAT SS 430 0.6 MM','PLAT SS 430 100 X 200',220500,222000,221000,221500,' ',8,0,0,'--00',1,'JINDAL',0),(48,'30',NULL,'PLAT SS 430 0.7 MM','PLAT SS 430 100 X 200',268500,270000,269000,269500,' ',8,0,0,'--00',1,'JINDAL',0),(49,'31',NULL,'PLAT SS 430 0.8 MM','PLAT SS 430 100 X 200',300000,301500,300500,301000,' ',8,0,0,'--00',1,'JINDAL',0),(50,'32',NULL,'PLAT SS 430 1.0 MM','PLAT SS 430 100 X 200',375500,377000,376000,376500,' ',8,0,0,'--00',1,'JINDAL',0),(51,'33',NULL,'PLAT SS 430 1.2 MM','PLAT SS 430 100 X 200',451000,452500,451500,452000,'33.jpg',8,0,0,'--00',1,'JINDAL',0),(52,'34',NULL,'PLAT SS 430 1.5 MM','PLAT SS 430 100 X 200',564000,565500,564500,565000,'34.jpg',8,0,0,'--00',1,'JINDAL',0),(53,'35',NULL,'PLAT SS 430 2.0 MM','PLAT SS 430 100 X 200',752500,754000,753000,753500,'35.jpg',8,0,0,'--00',1,'JINDAL',0),(54,'36',NULL,'PLAT SS 430 0.4 MM (120 X 240)','PLAT SS 430 120 X 240',228500,230000,229000,229500,' ',8,0,0,'--00',1,'JINDAL',0),(55,'37',NULL,'PLAT SS 430 0.5 MM (120 X 240)','PLAT SS 430 120 X 240',318500,320000,319000,319500,' ',8,0,0,'--00',1,'JINDAL',0),(56,'38',NULL,'PLAT SS 430 0.7 MM (120 X 240)','PLAT SS 430 120 X 240',338500,340000,339000,339500,' ',8,0,0,'--00',1,'JINDAL',0),(57,'39',NULL,'PLAT SS 430 0.8 MM (120 X 240)','PLAT SS 430 120 X 240',448500,450000,449000,449500,' ',8,0,0,'--00',1,'JINDAL',0),(58,'40',NULL,'PLAT SS 430 1.0 MM (120 X 240)','PLAT SS 430 120 X 240',565000,566500,565500,566000,' ',8,0,0,'--00',1,'JINDAL',0),(59,'41',NULL,'PLAT SS 430 1.2 MM (120 X 240)','PLAT SS 430 120 X 240',678500,680000,679000,679500,' ',8,0,0,'--00',1,'JINDAL',0),(60,'42',NULL,'PLAT SS 430 1.5 MM (120 X 240)','PLAT SS 430 120 X 240',848500,850000,849000,849500,' ',8,0,0,'--00',1,'JINDAL',0),(61,'43',NULL,'PLAT SS 430 2.0 MM (120 X 240)','PLAT SS 430 120 X 240',1131500,1133000,1132000,1132500,' ',8,0,0,'--00',1,'JINDAL',0),(62,'44',NULL,'PLAT SS 201 0.4 MM (120 X 240)','PLAT SS 201 120 X 240',248500,250000,249000,249500,' ',8,0,0,'--00',1,'JINDAL',0),(63,'45',NULL,'PLAT SS 201 0.5 MM (120 X 240)','PLAT SS 201 120 X 240',308500,310000,309000,309500,' ',8,0,0,'--00',1,'JINDAL',0),(64,'46',NULL,'PLAT SS 201 0.6 MM (120 X 240)','PLAT SS 201 120 X 240',368500,370000,369000,369500,'46.jpg',8,0,0,'--00',1,'JINDAL',0),(65,'47',NULL,'PLAT SS 201 0.8 MM (120 X 240)','PLAT SS 201 120 X 240',489500,491000,490000,490500,'47.jpg',8,0,0,'--00',1,'JINDAL',0),(66,'48',NULL,'PLAT SS 201 1.0 MM (120 X 240)','PLAT SS 201 120 X 240',612500,614000,613000,613500,'48.jpg',8,0,0,'--00',1,'JINDAL',0),(67,'49',NULL,'PLAT SS 201 1.2 MM (120 X 240)','PLAT SS 201 120 X 240',735000,736500,735500,736000,'49.jpg',8,0,0,'--00',1,'JINDAL',0),(68,'50',NULL,'PLAT SS 201 1.5 MM (120 X 240)','PLAT SS 201 120 X 240',919000,920500,919500,920000,'50.jpg',8,0,0,'--00',1,'JINDAL',0),(69,'51',NULL,'PLAT SS 201 2.0 MM (120 X 240)','PLAT SS 201 120 X 240',1226000,1227500,1226500,1227000,'51.jpg',8,0,0,'--00',1,'JINDAL',0),(70,'52',NULL,'PLAT SS 201 3.0 MM (120 X 240)','PLAT SS 201 120 X 240',1839500,1841000,1840000,1840500,'52.jpg',8,0,0,'--00',1,'JINDAL',0),(71,'53',NULL,'PLAT SS 201 4.0 MM (120 X 240)','PLAT SS 201 120 X 240',2453000,2454500,2453500,2454000,'53.jpg',8,0,0,'--00',1,'JINDAL',0),(72,'54',NULL,'PLAT SS 201 5.0 MM (120 X 240)','PLAT SS 201 120 X 240',3066500,3068000,3067000,3067500,'54.jpg',8,0,0,'--00',1,'JINDAL',0),(73,'55',NULL,'PLAT SS 201 6.0 MM (120 X 240)','PLAT SS 201 120 X 240',3680500,3682000,3681000,3681500,'55.jpg',8,0,0,'--00',1,'JINDAL',0),(74,'56',NULL,'PLAT SS 304 0.6 MM (120 X 240)','PLAT SS 304 120 X240',548500,550000,549000,549500,'56.jpg',8,0,0,'--00',1,'JINDAL',0),(75,'57',NULL,'PLAT SS 304 0.8 MM (120 X 240)','PLAT SS 304 120 X 240',716000,717500,716500,717000,'57.jpg',8,0,0,'--00',1,'JINDAL',0),(76,'58',NULL,'PLAT SS 304 1.0 MM (120 X 240)','PLAT SS 301 120 X 240',897500,899000,898000,898500,'58.jpg',8,0,0,'--00',1,'JINDAL',0),(77,'59',NULL,'PLAT SS 304 1.2 MM (120 X 240)','PLAT SS 304 120 X 240',1075000,1076500,1075500,1076000,'59.jpg',8,0,0,'--00',1,'JINDAL',0),(78,'60',NULL,'PLAT SS 304 1.5 MM (120 X 240)','PLAT SS 304 120 X 240',1344000,1345500,1344500,1345000,'60.jpg',8,0,0,'--00',1,'JINDAL',0),(79,'61',NULL,'PLAT SS 304 2.0 MM (120 X 240)','PLAT SS 304 120 X 240',1792500,1794000,1793000,1793500,'61.jpg',8,0,0,'--00',1,'JINDAL',0),(80,'62',NULL,'PLAT SS 304 3.0 MM (120 X 240)','PLAT SS 304 120 X 240',2680000,2690500,2680500,2690000,'62.jpg',8,0,0,'--00',1,'JINDAL',0),(81,'63',NULL,'PLAT SS 304 4.0 MM (120 X 240)','PLAT SS 304 120 X 240',3586000,3587500,3586500,3587000,'63.jpg',8,0,0,'--00',1,'JINDAL',0),(82,'64',NULL,'PLAT SS 304 5.0 MM (120 X 240)','PLAT SS 304 120 X 240',10,30,20,40,'64.jpg',8,0,0,'--00',1,'JINDAL',0),(83,'65',NULL,'PLAT SS 304 6.0 MM (120 X 240)','PLAT SS 304 120 X 240',10,30,20,40,'65.jpg',8,0,0,'--00',1,'JINDAL',0),(84,'66',NULL,'PLATESER 0.6 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(85,'67',NULL,'PLATESER 0.9 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(86,'68',NULL,'PLATESER 1.2 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(87,'69',NULL,'PLATESER 1.4 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(88,'70',NULL,'PLATESER 1.6 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(89,'71',NULL,'PLATESER 1.8 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(90,'72',NULL,'PLATESER 2.0 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,4,' ',8,0,0,'--00',1,' ',0),(91,'73',NULL,'PLATESER 2.8 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(92,'74',NULL,'PLATESER 3.0 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(93,'75',NULL,'PLATESER 3.8 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(94,'76',NULL,'PLATESER 4.8 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(95,'77',NULL,'PLATESER 5.8 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(96,'78',NULL,'PLATESER 8.0 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(97,'79',NULL,'PLATESER 10.0 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(98,'80',NULL,'PLATESER 12.0 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(99,'81',NULL,'PLATESER 16.0 MM (120 X 240)','PLAT BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(100,'82',NULL,'BORDES 1.8 MM (120 X 240)','BORDES BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(101,'83',NULL,'BORDES 2.3 MM (120 X 240)','BORDES BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(102,'84',NULL,'BORDES 3.0 MM (120 X 240)','BORDES BESI/ HITAM 120 X 240',10,30,20,40,' ',8,0,0,'--00',1,' ',0),(103,'85',NULL,'CNP 75 X 35 X 2.0 MM','CNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(104,'86',NULL,'CNP 100 X 50 X 2.0 MM','CNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(105,'87',NULL,'CNP 100 X 50 X 2.3 MM','CNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(106,'88',NULL,'CNP 125 X 50 X 2.0 MM','CNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(107,'89',NULL,'CNP 125 X 50 X 2.3 MM','CNP 6 METER',10,30,230,40,' ',6,0,0,'--00',1,' ',0),(108,'90',NULL,'CNP 125 X 50 X 2.8 MM','CNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(109,'91',NULL,'CNP 150 X 50 X 2.0 MM','CNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(110,'92',NULL,'CNP 150 X 50 X 2.3 MM','CNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(111,'93',NULL,'CNP 150 X 50 X 2.8 MM','CNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(112,'94',NULL,'CNP 200 X 70 X 2.3 MM','CNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(113,'95',NULL,'CNP 200 X 70 X 2.8 MM','CNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(114,'96',NULL,'UNP 50 MM','UNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(115,'97',NULL,'UNP 65 MM','UNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(116,'98',NULL,'UNP 80 MM','UNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(117,'99',NULL,'UNP 100 MM','UNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(118,'100',NULL,'UNP 120 MM','UNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(119,'101',NULL,'UNP 150 MM','UNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(120,'102',NULL,'UNP 200 MM','UNP 6 METER',10,30,20,40,' ',6,0,0,'--00',1,' ',0),(121,'103',NULL,'BENDRAT 21 KG','BENDRAT ROLL',10,30,20,40,' ',10,0,0,'--00',1,' ',0),(122,'104',NULL,'BENDRAT 25 KG','BENDRAT ROLL',10,30,20,40,' ',10,0,0,'--00',1,' ',0);
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_supplier`
--

LOCK TABLES `master_supplier` WRITE;
/*!40000 ALTER TABLE `master_supplier` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_supplier` ENABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_unit`
--

LOCK TABLES `master_unit` WRITE;
/*!40000 ALTER TABLE `master_unit` DISABLE KEYS */;
INSERT INTO `master_unit` VALUES (6,'BATANG','BATANG',1),(7,'','',1),(8,'LEMBAR','LEMBAR',1),(9,'','',1),(10,'ROLL','ROLL',1);
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
INSERT INTO `master_user` VALUES (1,'ADMIN','admin','ADMIN','1',1,1),(2,'INA','lovejesus97','ROSALINA','085211999554',1,2);
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_credit`
--

LOCK TABLES `payment_credit` WRITE;
/*!40000 ALTER TABLE `payment_credit` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_adjustment`
--

LOCK TABLES `product_adjustment` WRITE;
/*!40000 ALTER TABLE `product_adjustment` DISABLE KEYS */;
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
INSERT INTO `product_category` VALUES ('1',3),('10',4),('100',9),('101',9),('102',9),('103',10),('104',10),('11',4),('12',4),('13',4),('14',4),('15',4),('16',4),('17',4),('18',4),('19',4),('2',4),('20',4),('21',4),('22',4),('23',4),('24',4),('25',5),('26',5),('27',5),('28',5),('29',5),('3',4),('30',5),('31',5),('32',5),('33',5),('34',5),('35',5),('36',5),('37',5),('38',5),('39',5),('4',4),('40',5),('41',5),('42',5),('43',5),('44',5),('45',5),('46',5),('47',5),('48',5),('49',5),('5',4),('50',5),('51',5),('52',5),('53',5),('54',5),('55',5),('56',5),('57',5),('58',5),('59',5),('6',4),('60',5),('61',5),('62',5),('63',5),('64',5),('65',5),('66',6),('67',6),('68',6),('69',6),('7',4),('70',6),('71',6),('72',6),('73',6),('74',6),('75',6),('76',6),('77',6),('78',6),('79',6),('8',4),('80',6),('81',6),('82',7),('83',7),('84',7),('85',8),('86',8),('87',8),('88',8),('89',8),('9',4),('90',8),('91',8),('92',8),('93',8),('94',8),('95',8),('96',9),('97',9),('98',9),('99',9);
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
) ENGINE=InnoDB AUTO_INCREMENT=209 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_location`
--

LOCK TABLES `product_location` WRITE;
/*!40000 ALTER TABLE `product_location` DISABLE KEYS */;
INSERT INTO `product_location` VALUES (1,1,'1',0),(2,2,'1',8773),(3,1,'2',0),(4,2,'2',0),(5,1,'3',0),(6,2,'3',0),(7,1,'4',0),(8,2,'4',0),(9,1,'5',0),(10,2,'5',0),(11,1,'6',0),(12,2,'6',0),(13,1,'7',0),(14,2,'7',0),(15,1,'8',0),(16,2,'8',0),(17,1,'9',0),(18,2,'9',0),(19,1,'10',0),(20,2,'10',0),(21,1,'11',0),(22,2,'11',0),(23,1,'12',0),(24,2,'12',0),(25,1,'13',0),(26,2,'13',0),(27,1,'14',0),(28,2,'14',0),(29,1,'15',0),(30,2,'15',0),(31,1,'16',0),(32,2,'16',0),(33,1,'17',0),(34,2,'17',0),(35,1,'18',0),(36,2,'18',0),(37,1,'19',0),(38,2,'19',0),(39,1,'20',0),(40,2,'20',0),(41,2,'21',0),(42,1,'21',0),(43,1,'22',0),(44,2,'22',0),(45,1,'23',0),(46,2,'23',0),(47,1,'24',0),(48,2,'24',0),(49,1,'25',0),(50,2,'25',0),(51,1,'26',0),(52,2,'26',0),(53,1,'27',0),(54,2,'27',0),(55,1,'28',0),(56,2,'28',0),(57,1,'29',0),(58,2,'29',0),(59,1,'30',0),(60,2,'30',0),(61,1,'31',0),(62,2,'31',0),(63,1,'32',0),(64,2,'32',0),(65,1,'33',0),(66,2,'33',0),(67,1,'34',0),(68,2,'34',0),(69,1,'35',0),(70,2,'35',0),(71,1,'36',0),(72,2,'36',0),(73,1,'37',0),(74,2,'37',0),(75,1,'38',0),(76,2,'38',0),(77,1,'39',0),(78,2,'39',0),(79,1,'40',0),(80,2,'40',0),(81,1,'41',0),(82,2,'41',0),(83,1,'42',0),(84,2,'42',0),(85,1,'43',0),(86,2,'43',0),(87,1,'44',0),(88,2,'44',0),(89,1,'45',0),(90,2,'45',0),(91,1,'46',0),(92,2,'46',0),(93,1,'47',0),(94,2,'47',0),(95,1,'48',0),(96,2,'48',0),(97,1,'49',0),(98,2,'49',0),(99,1,'50',0),(100,2,'50',0),(101,1,'51',0),(102,2,'51',0),(103,1,'52',0),(104,2,'52',0),(105,1,'53',0),(106,2,'53',0),(107,1,'54',0),(108,2,'54',0),(109,1,'55',0),(110,2,'55',0),(111,1,'56',0),(112,2,'56',0),(113,1,'57',0),(114,2,'57',0),(115,1,'58',0),(116,2,'58',0),(117,1,'59',0),(118,2,'59',0),(119,1,'60',0),(120,2,'60',0),(121,1,'61',0),(122,2,'61',0),(123,1,'62',0),(124,2,'62',0),(125,1,'63',0),(126,2,'63',0),(127,1,'64',0),(128,2,'64',0),(129,1,'65',0),(130,2,'65',0),(131,1,'66',0),(132,2,'66',0),(133,1,'67',0),(134,2,'67',0),(135,1,'68',0),(136,2,'68',0),(137,1,'69',0),(138,2,'69',0),(139,1,'70',0),(140,2,'70',0),(141,1,'71',0),(142,2,'71',0),(143,1,'72',0),(144,2,'72',0),(145,1,'73',0),(146,2,'73',0),(147,1,'74',0),(148,2,'74',0),(149,1,'75',0),(150,2,'75',0),(151,1,'76',0),(152,2,'76',0),(153,1,'77',0),(154,2,'77',0),(155,1,'78',0),(156,2,'78',0),(157,1,'79',0),(158,2,'79',0),(159,1,'80',0),(160,2,'80',0),(161,1,'81',0),(162,2,'81',0),(163,1,'82',0),(164,2,'82',0),(165,1,'83',0),(166,2,'83',0),(167,1,'84',0),(168,2,'84',0),(169,1,'85',0),(170,2,'85',0),(171,1,'86',0),(172,2,'86',0),(173,1,'87',0),(174,2,'87',0),(175,1,'88',0),(176,2,'88',0),(177,1,'89',0),(178,2,'89',0),(179,1,'90',0),(180,2,'90',0),(181,1,'91',0),(182,2,'91',0),(183,1,'92',0),(184,2,'92',0),(185,1,'93',0),(186,2,'93',0),(187,1,'94',0),(188,2,'94',0),(189,1,'95',0),(190,2,'95',0),(191,1,'96',0),(192,2,'96',0),(193,1,'97',0),(194,2,'97',0),(195,1,'98',0),(196,2,'98',0),(197,1,'99',0),(198,2,'99',0),(199,1,'100',0),(200,2,'100',0),(201,1,'101',0),(202,2,'101',0),(203,1,'102',0),(204,2,'102',0),(205,1,'103',0),(206,2,'103',0),(207,1,'104',0),(208,2,'104',0);
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products_received_detail`
--

LOCK TABLES `products_received_detail` WRITE;
/*!40000 ALTER TABLE `products_received_detail` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='		';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products_received_header`
--

LOCK TABLES `products_received_header` WRITE;
/*!40000 ALTER TABLE `products_received_header` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase_detail`
--

LOCK TABLES `purchase_detail` WRITE;
/*!40000 ALTER TABLE `purchase_detail` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase_header`
--

LOCK TABLES `purchase_header` WRITE;
/*!40000 ALTER TABLE `purchase_header` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `return_purchase_detail`
--

LOCK TABLES `return_purchase_detail` WRITE;
/*!40000 ALTER TABLE `return_purchase_detail` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `return_purchase_header`
--

LOCK TABLES `return_purchase_header` WRITE;
/*!40000 ALTER TABLE `return_purchase_header` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `return_sales_detail`
--

LOCK TABLES `return_sales_detail` WRITE;
/*!40000 ALTER TABLE `return_sales_detail` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `return_sales_header`
--

LOCK TABLES `return_sales_header` WRITE;
/*!40000 ALTER TABLE `return_sales_header` DISABLE KEYS */;
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
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_detail`
--

LOCK TABLES `sales_detail` WRITE;
/*!40000 ALTER TABLE `sales_detail` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_detail_tax`
--

LOCK TABLES `sales_detail_tax` WRITE;
/*!40000 ALTER TABLE `sales_detail_tax` DISABLE KEYS */;
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
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_header`
--

LOCK TABLES `sales_header` WRITE;
/*!40000 ALTER TABLE `sales_header` DISABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_header_tax`
--

LOCK TABLES `sales_header_tax` WRITE;
/*!40000 ALTER TABLE `sales_header_tax` DISABLE KEYS */;
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
INSERT INTO `sys_config` VALUES (1,'SLO001',0,0,'127.0.0.1',NULL,NULL,NULL,NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=103 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_access_management`
--

LOCK TABLES `user_access_management` WRITE;
/*!40000 ALTER TABLE `user_access_management` DISABLE KEYS */;
INSERT INTO `user_access_management` VALUES (1,1,1,1),(2,1,2,1),(3,1,3,6),(4,1,4,6),(5,1,5,1),(6,1,6,1),(7,1,7,1),(8,1,8,1),(9,1,9,1),(10,1,10,6),(11,1,11,1),(12,1,12,1),(13,1,13,1),(14,1,14,1),(15,1,15,1),(16,1,16,6),(17,1,17,1),(18,1,18,1),(19,1,19,1),(20,1,20,1),(21,1,21,1),(22,1,22,1),(23,1,23,6),(24,1,24,1),(25,1,25,1),(26,1,26,1),(27,1,27,1),(28,1,28,1),(29,1,29,1),(30,1,30,6),(31,1,31,6),(32,1,32,1),(33,1,33,1),(34,1,34,1),(35,1,35,1),(36,1,36,6),(37,1,37,1),(38,1,38,1),(39,1,39,1),(40,1,40,1),(41,1,41,1),(42,1,42,1),(43,1,43,6),(44,1,44,1),(45,1,45,1),(46,1,46,1),(47,1,47,1),(48,1,48,1),(49,2,1,0),(50,2,2,0),(51,2,3,0),(52,2,4,0),(53,2,5,0),(54,2,6,0),(55,2,7,0),(56,2,8,0),(57,2,9,0),(58,2,10,0),(59,2,11,0),(60,2,12,0),(61,2,13,0),(62,2,14,0),(63,2,15,0),(64,2,16,0),(65,2,17,0),(66,2,18,0),(67,2,19,0),(68,2,20,0),(69,2,21,0),(70,2,22,0),(71,2,23,0),(72,2,24,0),(73,2,25,0),(74,2,26,0),(75,2,27,0),(76,2,28,0),(77,2,29,0),(78,2,30,0),(79,2,31,0),(80,2,32,0),(81,2,33,0),(82,2,34,0),(83,2,35,1),(84,2,36,6),(85,2,37,1),(86,2,38,0),(87,2,39,1),(88,2,40,1),(89,2,41,1),(90,2,42,1),(91,2,43,0),(92,2,44,1),(93,2,45,1),(94,2,46,1),(95,2,47,0),(96,2,48,0),(97,1,49,1),(98,2,49,0),(99,1,50,1),(100,1,51,1),(101,1,52,1),(102,1,53,1);
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

-- Dump completed on 2016-07-21  1:24:32
