
-----------------------------------------------------------
-- DATABASE CREATION SCRIPT
-- Author: Phan Le Giang
-- Created with Visual Paradigm Script Generator
--
-----------------------------------------------------------


USE master;
GO

IF DB_ID('DentalClinicPlatform') IS NOT NULL
DROP DATABASE [DentalClinicPlatform];
GO

IF DB_ID('DentalClinicPlatform') IS NULL
CREATE DATABASE DentalClinicPlatform;
GO

USE DentalClinicPlatform;

--###################################  GENERATED CONTENT ################################### -- 
CREATE TABLE Booking (
  book_id          uniqueidentifier DEFAULT (NEWID()) NOT NULL, 
  customer         int NOT NULL, 
  dentist          int NOT NULL, 
  slot             uniqueidentifier NOT NULL, 
  appointment_date date NOT NULL, 
  creation_date    datetime DEFAULT (GETDATE()) NOT NULL, 
  status           int NOT NULL, 
  PRIMARY KEY (book_id));
CREATE TABLE Clinic (
  clinic_id   int IDENTITY(1, 1) NOT NULL, 
  name        nvarchar(255) NOT NULL, 
  description nvarchar(2000) NULL, 
  address     nvarchar(255) NULL, 
  owner       int NOT NULL, 
  phone       nvarchar(10) NULL, 
  email       nvarchar(80) NULL, 
  status      int NOT NULL, 
  open_hour   time(7) NULL, 
  close_hour  time(7) NULL, 
  PRIMARY KEY (clinic_id));
CREATE TABLE Clinic_Service (
  clain_service     uniqueidentifier DEFAULT (NEWID()) NOT NULL, 
  Serviceservice_id int NOT NULL, 
  Clinicclinic_id   int NOT NULL, 
  PRIMARY KEY (clain_service));
CREATE TABLE Media (
  media_id     uniqueidentifier DEFAULT (NEWID()) NOT NULL, 
  media_path   int NULL, 
  creator      int NOT NULL, 
  created_date datetime DEFAULT (GETDATE()) NOT NULL, 
  PRIMARY KEY (media_id));
CREATE TABLE Message (
  message_id    uniqueidentifier DEFAULT (NEWID()) NOT NULL, 
  sender        int NOT NULL UNIQUE, 
  reciever      int NOT NULL, 
  content       nvarchar(1000) NOT NULL, 
  creation_date datetime DEFAULT (GETDATE()) NOT NULL, 
  PRIMARY KEY (message_id));
CREATE TABLE Payment (
  payment_id     uniqueidentifier DEFAULT (NEWID()) NOT NULL, 
  payment_type   int NOT NULL, 
  payment_status int NOT NULL, 
  appointment    uniqueidentifier NOT NULL, 
  PRIMARY KEY (payment_id));
CREATE TABLE PaymentType (
  type_id              int IDENTITY NOT NULL, 
  type_provider        nvarchar(45) NOT NULL UNIQUE, 
  type_provider_secret nvarchar(255) NOT NULL, 
  type_provider_token  nvarchar(255) NULL, 
  PRIMARY KEY (type_id));
CREATE TABLE Result (
  result_id     uniqueidentifier DEFAULT (NEWID()) NOT NULL, 
  creator       int NOT NULL, 
  appointment   uniqueidentifier NOT NULL, 
  media         uniqueidentifier NOT NULL, 
  note          nvarchar(255) NULL, 
  creatiom_date datetime DEFAULT (GETDATE()) NOT NULL, 
  PRIMARY KEY (result_id));
CREATE TABLE Role (
  role_id          int IDENTITY(1, 1) NOT NULL, 
  role_name        nvarchar(50) NOT NULL UNIQUE, 
  role_description nvarchar(255) NULL, 
  PRIMARY KEY (role_id));
CREATE TABLE Service (
  service_id  int IDENTITY(1, 1) NOT NULL, 
  name        nvarchar(100) NOT NULL, 
  description nvarchar(255) NULL, 
  PRIMARY KEY (service_id));
CREATE TABLE Slot (
  slot_id    uniqueidentifier DEFAULT (NEWID()) NOT NULL, 
  start_time time(7) NOT NULL, 
  end_time   time(7) NOT NULL, 
  weekdays   tinyint NOT NULL, 
  clinic     int NOT NULL, 
  PRIMARY KEY (slot_id));
CREATE TABLE Status (
  status_id          int IDENTITY(1, 1) NOT NULL, 
  status_name        nvarchar(20) NOT NULL UNIQUE, 
  status_description nvarchar(255) NULL, 
  PRIMARY KEY (status_id));
CREATE TABLE [User] (
  user_id        int IDENTITY(1, 1) NOT NULL, 
  username       nvarchar(45) NOT NULL UNIQUE, 
  password       nvarchar(30) NOT NULL, 
  profile_pic    nvarchar(255) NULL, 
  fullname       nvarchar(60) NULL, 
  email          nvarchar(80) NULL, 
  phone          nvarchar(10) NULL, 
  creation_date  datetime DEFAULT (GETDATE()) NULL, 
  insurance      nvarchar(20) NULL, 
  status         int NOT NULL, 
  role           int NOT NULL, 
  clinic_dentist int NULL, 
  PRIMARY KEY (user_id));
ALTER TABLE [User] ADD CONSTRAINT FKUser590756 FOREIGN KEY (role) REFERENCES Role (role_id);
ALTER TABLE Message ADD CONSTRAINT FKMessage130039 FOREIGN KEY (sender) REFERENCES [User] (user_id);
ALTER TABLE Message ADD CONSTRAINT FKMessage948125 FOREIGN KEY (reciever) REFERENCES [User] (user_id);
ALTER TABLE Slot ADD CONSTRAINT FKSlot545721 FOREIGN KEY (clinic) REFERENCES Clinic (clinic_id);
ALTER TABLE Payment ADD CONSTRAINT FKPayment556319 FOREIGN KEY (payment_type) REFERENCES PaymentType (type_id);
ALTER TABLE Payment ADD CONSTRAINT FKPayment993691 FOREIGN KEY (appointment) REFERENCES Booking (book_id);
ALTER TABLE Booking ADD CONSTRAINT FKBooking941670 FOREIGN KEY (customer) REFERENCES [User] (user_id);
ALTER TABLE Booking ADD CONSTRAINT FKBooking369135 FOREIGN KEY (dentist) REFERENCES [User] (user_id);
ALTER TABLE Clinic ADD CONSTRAINT FKClinic810483 FOREIGN KEY (owner) REFERENCES [User] (user_id);
ALTER TABLE Booking ADD CONSTRAINT FKBooking797160 FOREIGN KEY (slot) REFERENCES Slot (slot_id);
ALTER TABLE Result ADD CONSTRAINT FKResult540405 FOREIGN KEY (appointment) REFERENCES Booking (book_id);
ALTER TABLE Result ADD CONSTRAINT FKResult492515 FOREIGN KEY (creator) REFERENCES [User] (user_id);
ALTER TABLE Media ADD CONSTRAINT FKMedia288296 FOREIGN KEY (creator) REFERENCES [User] (user_id);
ALTER TABLE Clinic ADD CONSTRAINT FKClinic804252 FOREIGN KEY (status) REFERENCES Status (status_id);
ALTER TABLE [User] ADD CONSTRAINT FKUser682274 FOREIGN KEY (status) REFERENCES Status (status_id);
ALTER TABLE Booking ADD CONSTRAINT FKBooking12906 FOREIGN KEY (status) REFERENCES Status (status_id);
ALTER TABLE Clinic_Service ADD CONSTRAINT FKClinic_Ser816210 FOREIGN KEY (Clinicclinic_id) REFERENCES Clinic (clinic_id);
ALTER TABLE Clinic_Service ADD CONSTRAINT FKClinic_Ser554189 FOREIGN KEY (Serviceservice_id) REFERENCES Service (service_id);
ALTER TABLE Result ADD CONSTRAINT FKResult627124 FOREIGN KEY (media) REFERENCES Media (media_id);
ALTER TABLE [User] ADD CONSTRAINT FKUser6807 FOREIGN KEY (clinic_dentist) REFERENCES Clinic (clinic_id);
ALTER TABLE Payment ADD CONSTRAINT FKPayment787020 FOREIGN KEY (payment_status) REFERENCES Status (status_id);


-- ============ INITIAL DATA SETUP ==================== --

INSERT INTO [Role](role_name,role_description) VALUES ('Admin', 'System Admin.');
INSERT INTO [Role](role_name,role_description) VALUES ('Clinic Owner', 'Clinic owners.');
INSERT INTO [Role](role_name,role_description) VALUES ('Dentist', 'Created only by Clinic Owner');
INSERT INTO [Role](role_name,role_description) VALUES ('Customer', 'Normal user permission.');

INSERT INTO [Status](status_name, status_description) VALUES ('actived', 'The user, clinic is currently active.')
INSERT INTO [Status](status_name, status_description) VALUES ('deactivated', 'The user or clinic is currently non-active.')
INSERT INTO [Status](status_name, status_description) VALUES ('unverified', 'The clinic is currently active.')
INSERT INTO [Status](status_name, status_description) VALUES ('finished', 'The appointment is finished.')
INSERT INTO [Status](status_name, status_description) VALUES ('future', 'The appointment is not yet started.')

INSERT INTO [User](username, password, role, status) VALUES ('admin', 'GagisageoDFMVlvaav', 1, 1);