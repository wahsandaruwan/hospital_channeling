-- phpMyAdmin SQL Dump
-- version 4.7.9
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Jul 08, 2020 at 11:23 PM
-- Server version: 5.7.21
-- PHP Version: 5.6.35

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `orangehos`
--

-- --------------------------------------------------------

--
-- Table structure for table `appointment`
--

DROP TABLE IF EXISTS `appointment`;
CREATE TABLE IF NOT EXISTS `appointment` (
  `app_no` int(11) NOT NULL AUTO_INCREMENT,
  `a_date` date NOT NULL,
  `r_no` int(11) DEFAULT NULL,
  `p_id` int(11) DEFAULT NULL,
  `e_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`app_no`),
  KEY `r_no` (`r_no`),
  KEY `e_id` (`e_id`),
  KEY `p_id` (`p_id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `appointment`
--

INSERT INTO `appointment` (`app_no`, `a_date`, `r_no`, `p_id`, `e_id`) VALUES
(1, '2020-07-01', 3, 1, 4),
(3, '2020-07-03', 1, 2, 4),
(4, '2020-07-04', 2, 2, 6);

-- --------------------------------------------------------

--
-- Table structure for table `department`
--

DROP TABLE IF EXISTS `department`;
CREATE TABLE IF NOT EXISTS `department` (
  `d_no` int(11) NOT NULL AUTO_INCREMENT,
  `d_name` varchar(50) NOT NULL,
  PRIMARY KEY (`d_no`),
  UNIQUE KEY `d_name` (`d_name`)
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `department`
--

INSERT INTO `department` (`d_no`, `d_name`) VALUES
(1, 'Management'),
(2, 'Dermatology'),
(3, 'Allergy & immunology'),
(4, 'Anesthesiology'),
(5, 'Diagnostic radiology'),
(6, 'Family medicine'),
(7, 'Neurology');

-- --------------------------------------------------------

--
-- Table structure for table `doctorpatient`
--

DROP TABLE IF EXISTS `doctorpatient`;
CREATE TABLE IF NOT EXISTS `doctorpatient` (
  `e_id` int(11) NOT NULL,
  `p_id` int(11) NOT NULL,
  `treat` varchar(500) NOT NULL,
  PRIMARY KEY (`e_id`,`p_id`,`treat`),
  KEY `p_id` (`p_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `doctorpatient`
--

INSERT INTO `doctorpatient` (`e_id`, `p_id`, `treat`) VALUES
(1, 4, 'tttt'),
(4, 1, 'fghgfhfj'),
(4, 4, 'fghgfhfj');

-- --------------------------------------------------------

--
-- Table structure for table `doctorroom`
--

DROP TABLE IF EXISTS `doctorroom`;
CREATE TABLE IF NOT EXISTS `doctorroom` (
  `e_id` int(11) NOT NULL,
  `room_no` int(11) NOT NULL,
  PRIMARY KEY (`e_id`,`room_no`),
  KEY `room_no` (`room_no`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
CREATE TABLE IF NOT EXISTS `employee` (
  `e_id` int(11) NOT NULL AUTO_INCREMENT,
  `e_name` varchar(100) NOT NULL,
  `salary` float NOT NULL,
  `etype` varchar(30) NOT NULL,
  `dep_no` int(11) DEFAULT NULL,
  `major` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`e_id`),
  KEY `dep_no` (`dep_no`)
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `employee`
--

INSERT INTO `employee` (`e_id`, `e_name`, `salary`, `etype`, `dep_no`, `major`) VALUES
(1, 'Tharidu De Silva', 120000, 'Manager', 1, NULL),
(2, 'Sanduni Perera', 35000, 'Receptionist', 1, NULL),
(3, 'Dilki Imasha', 35000, 'Receptionist', 1, NULL),
(4, 'Chamal Weerasinghe', 100000, 'Doctor', 2, 'Dermatology Specialist'),
(5, 'Kasuni Tharika', 50000, 'Nurse', 2, NULL),
(6, 'Achala Weerasinghe', 110000, 'Doctor', 6, 'Family Medicine Specialist'),
(7, 'Dasuni Fernando', 50000, 'Nurse', 6, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `nurseroom`
--

DROP TABLE IF EXISTS `nurseroom`;
CREATE TABLE IF NOT EXISTS `nurseroom` (
  `e_id` int(11) NOT NULL,
  `room_no` int(11) NOT NULL,
  PRIMARY KEY (`e_id`,`room_no`),
  KEY `room_no` (`room_no`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `nurseroom`
--

INSERT INTO `nurseroom` (`e_id`, `room_no`) VALUES
(1, 7);

-- --------------------------------------------------------

--
-- Table structure for table `patient`
--

DROP TABLE IF EXISTS `patient`;
CREATE TABLE IF NOT EXISTS `patient` (
  `p_id` int(11) NOT NULL AUTO_INCREMENT,
  `p_name` varchar(100) NOT NULL,
  PRIMARY KEY (`p_id`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `patient`
--

INSERT INTO `patient` (`p_id`, `p_name`) VALUES
(1, 'Sampath Perera'),
(2, 'Seetha Gunawardana');

-- --------------------------------------------------------

--
-- Table structure for table `payment`
--

DROP TABLE IF EXISTS `payment`;
CREATE TABLE IF NOT EXISTS `payment` (
  `bill_no` int(11) NOT NULL AUTO_INCREMENT,
  `amount` float NOT NULL,
  `p_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`bill_no`),
  KEY `p_id` (`p_id`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `payment`
--

INSERT INTO `payment` (`bill_no`, `amount`, `p_id`) VALUES
(1, 4000, 1),
(2, 2500, 2);

-- --------------------------------------------------------

--
-- Table structure for table `room`
--

DROP TABLE IF EXISTS `room`;
CREATE TABLE IF NOT EXISTS `room` (
  `room_no` int(11) NOT NULL AUTO_INCREMENT,
  `rtype` varchar(50) NOT NULL,
  PRIMARY KEY (`room_no`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `room`
--

INSERT INTO `room` (`room_no`, `rtype`) VALUES
(1, 'Management Room'),
(2, 'Emergency Medicine Room'),
(3, 'Dermetalogy Room');

-- --------------------------------------------------------

--
-- Table structure for table `useracc`
--

DROP TABLE IF EXISTS `useracc`;
CREATE TABLE IF NOT EXISTS `useracc` (
  `e_id` int(11) NOT NULL,
  `pass` varchar(200) NOT NULL,
  `ustype` varchar(50) NOT NULL,
  PRIMARY KEY (`e_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `useracc`
--

INSERT INTO `useracc` (`e_id`, `pass`, `ustype`) VALUES
(1, 'admin', 'Manager'),
(3, '11111', 'Receptionist'),
(4, 'abcd', 'Doctor'),
(5, '22222', 'Nurse');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
