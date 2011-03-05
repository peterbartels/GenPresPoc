﻿CREATE SCHEMA GenPres
GO

GO


CREATE TABLE GenPres.Prescription
(
	Id INTEGER IDENTITY (1, 1) NOT NULL,
	PatientId INTEGER NOT NULL,
	StartDate DATETIME,
	EndDate DATETIME,
	"Date" DATETIME,
	Continuous BIT,
	Infusion BIT,
	Onrequest BIT,
	Solution BIT,
	IsTemplate BIT,
	State NATIONAL CHARACTER VARYING(MAX),
	TPN BIT,
	Remarks NATIONAL CHARACTER VARYING(MAX),
	Frequency INTEGER,
	Quantity INTEGER,
	Total INTEGER,
	Rate INTEGER,
	"Time" INTEGER,
	AdjustLength INTEGER,
	AdjustWeight INTEGER,
	DrugId INTEGER,
	MedicineId INTEGER,
	CONSTRAINT Prescription_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE GenPres.UnitValue
(
	Id INTEGER IDENTITY (1, 1) NOT NULL,
	BaseValue DOUBLE PRECISION,
	Unit NATIONAL CHARACTER VARYING(MAX),
	"Value" DOUBLE PRECISION,
	UIState NATIONAL CHARACTER VARYING(MAX),
	"Time" NATIONAL CHARACTER VARYING(MAX),
	Total NATIONAL CHARACTER VARYING(MAX),
	Adjust NATIONAL CHARACTER VARYING(MAX),
	CONSTRAINT UnitValue_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE GenPres.Dose
(
	Id INTEGER IDENTITY (1, 1) NOT NULL,
	PrescriptionId INTEGER NOT NULL,
	Quantity INTEGER,
	Total INTEGER,
	Rate INTEGER,
	SubstanceId INTEGER,
	CONSTRAINT Dose_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE GenPres.Drug
(
	Id INTEGER IDENTITY (1, 1) NOT NULL,
	Name NATIONAL CHARACTER VARYING(MAX),
	Shape NATIONAL CHARACTER VARYING(MAX),
	Route NATIONAL CHARACTER VARYING(MAX),
	SolutionType NATIONAL CHARACTER VARYING(MAX),
	Quantity INTEGER,
	CONSTRAINT Drug_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE GenPres.Patient
(
	Id INTEGER IDENTITY (1, 1) NOT NULL,
	MedicationWeight DOUBLE PRECISION,
	Height DOUBLE PRECISION,
	PID NATIONAL CHARACTER VARYING(MAX),
	CONSTRAINT Patient_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE GenPres.Substance
(
	Id INTEGER IDENTITY (1, 1) NOT NULL,
	ComponentId INTEGER NOT NULL,
	SubstanceName NATIONAL CHARACTER VARYING(MAX),
	ComponentConcentration INTEGER,
	DrugConcentration INTEGER,
	Quantity INTEGER,
	CustomIncrement INTEGER,
	CONSTRAINT Substance_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE GenPres.Component
(
	Id INTEGER IDENTITY (1, 1) NOT NULL,
	DrugId INTEGER NOT NULL,
	ComponentName NATIONAL CHARACTER VARYING(MAX),
	IsSolution BIT,
	SolutionRelation DOUBLE PRECISION,
	Quantity INTEGER,
	DrugConcentration INTEGER,
	CONSTRAINT Component_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE GenPres.Medicine
(
	Id INTEGER IDENTITY (1, 1) NOT NULL,
	GenericName NATIONAL CHARACTER VARYING(MAX),
	ShapeName NATIONAL CHARACTER VARYING(MAX),
	RouteName NATIONAL CHARACTER VARYING(MAX),
	ComponentIncrement INTEGER,
	DoseIncrement INTEGER,
	Quantity INTEGER,
	CONSTRAINT Medicine_PK PRIMARY KEY(Id)
)
GO


ALTER TABLE GenPres.Prescription ADD CONSTRAINT Prescription_FK1 FOREIGN KEY (Frequency) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Prescription ADD CONSTRAINT Prescription_FK2 FOREIGN KEY (Quantity) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Prescription ADD CONSTRAINT Prescription_FK3 FOREIGN KEY (Total) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Prescription ADD CONSTRAINT Prescription_FK4 FOREIGN KEY (Rate) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Prescription ADD CONSTRAINT Prescription_FK5 FOREIGN KEY ("Time") REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Prescription ADD CONSTRAINT Prescription_FK6 FOREIGN KEY (AdjustLength) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Prescription ADD CONSTRAINT Prescription_FK7 FOREIGN KEY (AdjustWeight) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Prescription ADD CONSTRAINT Prescription_FK8 FOREIGN KEY (PatientId) REFERENCES GenPres.Patient (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Prescription ADD CONSTRAINT Prescription_FK9 FOREIGN KEY (DrugId) REFERENCES GenPres.Drug (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Prescription ADD CONSTRAINT Prescription_FK10 FOREIGN KEY (MedicineId) REFERENCES GenPres.Medicine (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Dose ADD CONSTRAINT Dose_FK1 FOREIGN KEY (Quantity) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Dose ADD CONSTRAINT Dose_FK2 FOREIGN KEY (Total) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Dose ADD CONSTRAINT Dose_FK3 FOREIGN KEY (Rate) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Dose ADD CONSTRAINT Dose_FK4 FOREIGN KEY (SubstanceId) REFERENCES GenPres.Substance (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Dose ADD CONSTRAINT Dose_FK5 FOREIGN KEY (PrescriptionId) REFERENCES GenPres.Prescription (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Drug ADD CONSTRAINT Drug_FK FOREIGN KEY (Quantity) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Substance ADD CONSTRAINT Substance_FK1 FOREIGN KEY (ComponentConcentration) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Substance ADD CONSTRAINT Substance_FK2 FOREIGN KEY (DrugConcentration) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Substance ADD CONSTRAINT Substance_FK3 FOREIGN KEY (ComponentId) REFERENCES GenPres.Component (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Substance ADD CONSTRAINT Substance_FK4 FOREIGN KEY (Quantity) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Substance ADD CONSTRAINT Substance_FK5 FOREIGN KEY (CustomIncrement) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Component ADD CONSTRAINT Component_FK1 FOREIGN KEY (Quantity) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Component ADD CONSTRAINT Component_FK2 FOREIGN KEY (DrugConcentration) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Component ADD CONSTRAINT Component_FK3 FOREIGN KEY (DrugId) REFERENCES GenPres.Drug (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Medicine ADD CONSTRAINT Medicine_FK1 FOREIGN KEY (ComponentIncrement) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Medicine ADD CONSTRAINT Medicine_FK2 FOREIGN KEY (DoseIncrement) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenPres.Medicine ADD CONSTRAINT Medicine_FK3 FOREIGN KEY (Quantity) REFERENCES GenPres.UnitValue (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


GO