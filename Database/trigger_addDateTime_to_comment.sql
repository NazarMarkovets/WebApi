delimiter //

CREATE DEFINER=`root`@`localhost` TRIGGER `AddDateBeforeInsert` BEFORE INSERT ON `comment` FOR EACH ROW BEGIN
		SET new.created_at = SYSDATE();
END
//