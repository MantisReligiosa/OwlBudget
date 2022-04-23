CREATE TABLE "Users"
(
    "Id"           BLOB    NOT NULL UNIQUE,
    "Login"        TEXT    NOT NULL UNIQUE,
    "PasswordHash" TEXT    NOT NULL,
    "Role"         INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

CREATE TABLE "DrillingRigs"
(
    "Id"   BLOB    NOT NULL UNIQUE,
    "Name" INTEGER NOT NULL UNIQUE,
    PRIMARY KEY ("Id")
);

CREATE TABLE "Regions"
(
    "Id"   BLOB NOT NULL UNIQUE,
    "Name" TEXT NOT NULL,
    PRIMARY KEY ("Id")
);

CREATE TABLE "WellConstructions"
(
    "Id"   BLOB    NOT NULL UNIQUE,
    "Name" INTEGER NOT NULL UNIQUE,
    PRIMARY KEY ("Id")
);

CREATE TABLE "WellTypes"
(
    "Id"   BLOB    NOT NULL UNIQUE,
    "Name" INTEGER NOT NULL UNIQUE,
    PRIMARY KEY ("Id")
);

CREATE TABLE "Projects"
(
    "Id"                   BLOB NOT NULL UNIQUE,
    "Name"                 TEXT NOT NULL,
    "ContractTypeId"       BLOB NOT NULL,
    "Location"             TEXT,
    "RegionId"             BLOB NOT NULL,
    "Description"          TEXT,
    "OwnerId"              BLOB NOT NULL,
    "CreatedTimestamp"     NUMERIC,
    "ModificatedTimestamp" NUMERIC,
    FOREIGN KEY ("RegionId") REFERENCES "Regions" ("Id"),
    FOREIGN KEY ("OwnerId") REFERENCES "Users" ("Id"),
    PRIMARY KEY ("Id")
);

CREATE TABLE "Scenarios"
(
    "Id"         BLOB    NOT NULL UNIQUE,
    "Name"       TEXT    NOT NULL,
    "ProjectId"  BLOB    NOT NULL,
    "BudgetType" INTEGER NOT NULL,
    FOREIGN KEY ("ProjectId") REFERENCES "Projects" ("Id"),
    PRIMARY KEY ("Id")
);

CREATE TABLE "Lots"
(
    "Id"        BLOB NOT NULL UNIQUE,
    "Name"      TEXT,
    "ProjectId" BLOB NOT NULL,
    FOREIGN KEY ("ProjectId") REFERENCES "Projects" ("Id"),
    PRIMARY KEY ("Id")
);

CREATE TABLE "Clusters"
(
    "Id"         BLOB NOT NULL UNIQUE,
    "Name"       TEXT NOT NULL,
    "LotId"      BLOB NOT NULL,
    "WellTypeId" BLOB NOT NULL,
    FOREIGN KEY ("LotId") REFERENCES "Lots" ("Id"),
    FOREIGN KEY ("WellTypeId") REFERENCES "WellTypes" ("Id"),
    PRIMARY KEY ("Id")
);

CREATE TABLE "Wells"
(
    "Id"             BLOB NOT NULL UNIQUE,
    "Name"           TEXT,
    "ClusterId"      BLOB,
    "ConstructionId" BLOB NOT NULL,
    FOREIGN KEY ("ClusterId") REFERENCES "Clusters" ("Id"),
    FOREIGN KEY ("ConstructionId") REFERENCES "WellConstructions" ("Id"),
    PRIMARY KEY ("Id")
);

CREATE TABLE "DrillingRigSequences"
(
    "Id"            BLOB NOT NULL UNIQUE,
    "Name"          TEXT,
    "ScenarioId"    BLOB NOT NULL,
    "DrillingRigId" BLOB NOT NULL,
    FOREIGN KEY ("DrillingRigId") REFERENCES "DrillingRigs" ("Id"),
    FOREIGN KEY ("ScenarioId") REFERENCES "Scenarios" ("Id"),
    PRIMARY KEY ("Id")
);

CREATE TABLE "WellsToDrillingRigSequences"
(
    "Id"                    BLOB    NOT NULL UNIQUE,
    "WellId"                BLOB    NOT NULL,
    "DrillingRigSequenceId" BLOB    NOT NULL,
    "DrillingOrder"         INTEGER NOT NULL,
    FOREIGN KEY ("WellId") REFERENCES "Wells" ("Id"),
    FOREIGN KEY ("DrillingRigSequenceId") REFERENCES "DrillingRigSequences" ("Id"),
    PRIMARY KEY ("Id")
);

INSERT INTO "main"."DrillingRigs" ("Id", "Name")
VALUES ('00000000-0000-0000-0000-000000000001', 'БУ 1');
INSERT INTO "main"."DrillingRigs" ("Id", "Name")
VALUES ('00000000-0000-0000-0000-000000000002', 'БУ 2');
INSERT INTO "main"."Regions" ("Id", "Name")
VALUES ('00000000-0000-0000-0000-000000000001', 'Регион1');
INSERT INTO "main"."Regions" ("Id", "Name")
VALUES ('00000000-0000-0000-0000-000000000002', 'Регион2');
INSERT INTO "main"."Users" ("Id", "Login", "PasswordHash", "Role")
VALUES ('FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF', 'admin',
        '7523C62ABDB7628C5A9DAD8F97D8D8C5C040EDE36535E531A8A3748B6CAE7E00', '0');
INSERT INTO "main"."WellConstructions" ("Id", "Name")
VALUES ('00000000-0000-0000-0000-000000000001', 'Конструкция 1');
INSERT INTO "main"."WellConstructions" ("Id", "Name")
VALUES ('00000000-0000-0000-0000-000000000002', 'Конструкция 2');
INSERT INTO "main"."WellTypes" ("Id", "Name")
VALUES ('00000000-0000-0000-0000-000000000001', 'Тип 1');
INSERT INTO "main"."WellTypes" ("Id", "Name")
VALUES ('00000000-0000-0000-0000-000000000002', 'Тип 2');