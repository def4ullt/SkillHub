SELECT 'CREATE DATABASE "TaskWorkService"'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'TaskWorkService')\gexec

\c TaskWorkService

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS work_submission_statuses (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL UNIQUE,
    createdat TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc')
);

CREATE TABLE IF NOT EXISTS submission_delivery_methods (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL UNIQUE,
    createdat TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc')
);

CREATE TABLE IF NOT EXISTS work_submissions (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    taskid UUID NOT NULL,
    taskname VARCHAR(200) NOT NULL,
    userid UUID NOT NULL,
    userfirstname VARCHAR(100) NOT NULL,
    userlastname VARCHAR(100) NOT NULL,
    statusid UUID NOT NULL,
    xpreward INTEGER NOT NULL DEFAULT 0,
    submissiondate TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc'),
    CONSTRAINT fk_worksubmission_status FOREIGN KEY (statusid)
        REFERENCES work_submission_statuses(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

ALTER TABLE work_submissions ADD COLUMN IF NOT EXISTS xpreward INTEGER NOT NULL DEFAULT 0;

CREATE TABLE IF NOT EXISTS work_submission_files (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    worksubmissionid UUID NOT NULL,
    deliverymethodid UUID NOT NULL,
    fileurl TEXT NOT NULL,
    CONSTRAINT fk_worksubmission_file FOREIGN KEY (worksubmissionid)
        REFERENCES work_submissions(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    CONSTRAINT fk_worksubmission_delivery FOREIGN KEY (deliverymethodid)
        REFERENCES submission_delivery_methods(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS user_xp (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    userid UUID NOT NULL,
    taskid UUID NOT NULL,
    xpamount INTEGER NOT NULL DEFAULT 0,
    earnedat TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc')
);
