-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema everlastingblog
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema everlastingblog
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `everlastingblog` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_bin ;
USE `everlastingblog` ;

-- -----------------------------------------------------
-- Table `everlastingblog`.`author`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `everlastingblog`.`author` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `first_name` VARCHAR(45) CHARACTER SET 'utf8mb4' NOT NULL,
  `last_name` VARCHAR(45) CHARACTER SET 'utf8mb4' NOT NULL,
  `email` VARCHAR(45) CHARACTER SET 'utf8mb4' NOT NULL,
  `password` VARCHAR(64) CHARACTER SET 'utf8mb4' NOT NULL,
  `username` VARCHAR(45) CHARACTER SET 'utf8mb4' NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `username` (`username` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 4346
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_bin
COMMENT = '	';


-- -----------------------------------------------------
-- Table `everlastingblog`.`article`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `everlastingblog`.`article` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `slur` VARCHAR(45) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_bin' NOT NULL,
  `content` TEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_bin' NOT NULL,
  `author_id` INT UNSIGNED NOT NULL,
  `title` VARCHAR(45) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_bin' NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `slur_idx` (`slur` ASC) VISIBLE,
  INDEX `fk_article_author_idx` (`author_id` ASC) VISIBLE,
  CONSTRAINT `fk_article_author`
    FOREIGN KEY (`author_id`)
    REFERENCES `everlastingblog`.`author` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_bin;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
