-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema everlastingcomments
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema everlastingcomments
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `everlastingcomments` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_bin ;
USE `everlastingcomments` ;

-- -----------------------------------------------------
-- Table `everlastingcomments`.`comment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `everlastingcomments`.`comment` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `content` TEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_bin' NOT NULL,
  `author_name` VARCHAR(45) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_bin' NOT NULL,
  `author_email` VARCHAR(45) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_bin' NOT NULL,
  `article_id` INT NOT NULL,
  `created_at` DATETIME NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `author_email` (`author_email` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_bin;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
