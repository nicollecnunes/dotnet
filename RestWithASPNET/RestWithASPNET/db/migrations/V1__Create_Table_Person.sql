CREATE TABLE IF NOT EXISTS `person` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `address` varchar(100) NOT NULL,
  `fname` varchar(80) NOT NULL,
  `gender` varchar(6) NOT NULL,
  `lname` varchar(80) NOT NULL,
  PRIMARY KEY (`id`)
)