﻿CREATE SCHEMA ORMModel2
GO

GO


CREATE TABLE ORMModel2.Component
(
	id1 INTEGER IDENTITY (1, 1) NOT NULL,
	componentName NATIONAL CHARACTER VARYING(255),
	componentQuantity DOUBLE PRECISION,
	id2 INTEGER,
	CONSTRAINT Component_PK PRIMARY KEY(id1)
)
GO


CREATE TABLE ORMModel2.Shape
(
	id INTEGER IDENTITY (1, 1) NOT NULL,
	shapeName NATIONAL CHARACTER VARYING(255),
	CONSTRAINT Shape_PK PRIMARY KEY(id)
)
GO


CREATE TABLE ORMModel2.Drug
(
	id1 INTEGER IDENTITY (1, 1) NOT NULL,
	drugName NATIONAL CHARACTER VARYING(255),
	quantity DOUBLE PRECISION,
	id2 INTEGER,
	CONSTRAINT Drug_PK PRIMARY KEY(id1)
)
GO


CREATE TABLE ORMModel2.Substance
(
	id INTEGER IDENTITY (1, 1) NOT NULL,
	substanceName NATIONAL CHARACTER(255),
	substanceQuantity DOUBLE PRECISION,
	CONSTRAINT Substance_PK PRIMARY KEY(id)
)
GO


CREATE TABLE ORMModel2.Container
(
	id INTEGER IDENTITY (1, 1) NOT NULL,
	containerName NATIONAL CHARACTER VARYING(255),
	CONSTRAINT Container_PK PRIMARY KEY(id)
)
GO


CREATE TABLE ORMModel2.Prescription
(
	id1 INTEGER IDENTITY (1, 1) NOT NULL,
	id2 INTEGER NOT NULL,
	componentPackageUnit NATIONAL CHARACTER VARYING(50) NOT NULL,
	componentDrugConcentrationUnitTotal NATIONAL CHARACTER VARYING(50) NOT NULL,
	frequency DOUBLE PRECISION,
	frequencyUnit NATIONAL CHARACTER VARYING(50),
	"time" DOUBLE PRECISION,
	timeUnit NATIONAL CHARACTER VARYING(50),
	prescriptionRate DOUBLE PRECISION,
	ratePackageUnit NATIONAL CHARACTER VARYING(50),
	rateAdjustUnit NATIONAL CHARACTER VARYING(50),
	rateTimeUnit NATIONAL CHARACTER VARYING(50),
	prescriptionTotalQuantity DOUBLE PRECISION,
	prescriptionQuantity DOUBLE PRECISION,
	quantityPackageUnit NATIONAL CHARACTER VARYING(50),
	quantityAdjustUnit NATIONAL CHARACTER VARYING(50),
	totalPackageUnit NATIONAL CHARACTER VARYING(50),
	totalAdjustUnit NATIONAL CHARACTER VARYING(50),
	totalTimeUnit NATIONAL CHARACTER VARYING(50),
	startDate DATETIME,
	endDate DATETIME,
	CONSTRAINT Prescription_PK PRIMARY KEY(id1),
	CONSTRAINT Prescription_UC UNIQUE(id2)
)
GO


CREATE TABLE ORMModel2.Dose
(
	id INTEGER NOT NULL,
	generic INTEGER NOT NULL,
	componentDrugConcentrationUnit NATIONAL CHARACTER VARYING(50) NOT NULL,
	substanceComponentConcentrationUnit NATIONAL CHARACTER VARYING(50) NOT NULL,
	substancePackageUnit NATIONAL CHARACTER VARYING(50) NOT NULL,
	substanceDrugConcentrationUnitTotal NATIONAL CHARACTER VARYING(50) NOT NULL,
	substanceComponentConcentrationUnitTotal NATIONAL CHARACTER VARYING(MAX) NOT NULL,
	substanceDrugConcentrationUnit NATIONAL CHARACTER VARYING(50) NOT NULL,
	doseQuantity DOUBLE PRECISION,
	doseRate DOUBLE PRECISION,
	doseTotal DOUBLE PRECISION,
	quantitySubstanceUnit NATIONAL CHARACTER VARYING(50),
	quantityAdjustUnit NATIONAL CHARACTER VARYING(50),
	rateSubstanceUnit NATIONAL CHARACTER VARYING(50),
	rateTimeUnit NATIONAL CHARACTER VARYING(50),
	totalSubstanceUnit NATIONAL CHARACTER VARYING(50),
	totalAdjustUnit NATIONAL CHARACTER VARYING(50),
	totalTimeUnit NATIONAL CHARACTER VARYING(50),
	CONSTRAINT Dose_PK PRIMARY KEY(id, generic)
)
GO


CREATE TABLE ORMModel2.Patient
(
	id INTEGER IDENTITY (1, 1) NOT NULL,
	medicationWeight DOUBLE PRECISION,
	height DOUBLE PRECISION,
	CONSTRAINT Patient_PK PRIMARY KEY(id)
)
GO


CREATE TABLE ORMModel2.PatientPrescription
(
	id1 INTEGER NOT NULL,
	id2 INTEGER NOT NULL,
	CONSTRAINT PatientPrescription_PK PRIMARY KEY(id2, id1)
)
GO


CREATE TABLE ORMModel2.Prescriber
(
	id INTEGER IDENTITY (1, 1) NOT NULL,
	userName NATIONAL CHARACTER(255),
	CONSTRAINT Prescriber_PK PRIMARY KEY(id)
)
GO


CREATE TABLE ORMModel2.PrescriberPrescription
(
	id1 INTEGER NOT NULL,
	id2 INTEGER NOT NULL,
	CONSTRAINT PrescriberPrescription_PK PRIMARY KEY(id2, id1)
)
GO


CREATE TABLE ORMModel2.ComponenSubstance
(
	id1 INTEGER NOT NULL,
	id2 INTEGER NOT NULL,
	CONSTRAINT ComponenSubstance_PK PRIMARY KEY(id1, id2)
)
GO


CREATE TABLE ORMModel2.DrugComponent
(
	id1 INTEGER NOT NULL,
	id2 INTEGER NOT NULL,
	CONSTRAINT DrugComponent_PK PRIMARY KEY(id2, id1)
)
GO


ALTER TABLE ORMModel2.Component ADD CONSTRAINT Component_FK FOREIGN KEY (id2) REFERENCES ORMModel2.Shape (id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE ORMModel2.Drug ADD CONSTRAINT Drug_FK FOREIGN KEY (id2) REFERENCES ORMModel2.Container (id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE ORMModel2.Prescription ADD CONSTRAINT Prescription_FK FOREIGN KEY (id2) REFERENCES ORMModel2.Drug (id1) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE ORMModel2.Dose ADD CONSTRAINT Dose_FK1 FOREIGN KEY (id) REFERENCES ORMModel2.Prescription (id1) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE ORMModel2.Dose ADD CONSTRAINT Dose_FK2 FOREIGN KEY (generic) REFERENCES ORMModel2.Substance (id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE ORMModel2.PatientPrescription ADD CONSTRAINT PatientPrescription_FK1 FOREIGN KEY (id1) REFERENCES ORMModel2.Prescription (id1) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE ORMModel2.PatientPrescription ADD CONSTRAINT PatientPrescription_FK2 FOREIGN KEY (id2) REFERENCES ORMModel2.Patient (id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE ORMModel2.PrescriberPrescription ADD CONSTRAINT PrescriberPrescription_FK1 FOREIGN KEY (id1) REFERENCES ORMModel2.Prescription (id1) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE ORMModel2.PrescriberPrescription ADD CONSTRAINT PrescriberPrescription_FK2 FOREIGN KEY (id2) REFERENCES ORMModel2.Prescriber (id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE ORMModel2.ComponenSubstance ADD CONSTRAINT ComponenSubstance_FK1 FOREIGN KEY (id1) REFERENCES ORMModel2.Component (id1) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE ORMModel2.ComponenSubstance ADD CONSTRAINT ComponenSubstance_FK2 FOREIGN KEY (id2) REFERENCES ORMModel2.Substance (id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE ORMModel2.DrugComponent ADD CONSTRAINT DrugComponent_FK1 FOREIGN KEY (id1) REFERENCES ORMModel2.Drug (id1) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE ORMModel2.DrugComponent ADD CONSTRAINT DrugComponent_FK2 FOREIGN KEY (id2) REFERENCES ORMModel2.Component (id1) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


GO