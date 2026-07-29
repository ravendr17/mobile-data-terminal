-- migrate:up
CREATE TABLE license_type (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name TEXT NOT NULL,
    CONSTRAINT uq_license_type_name UNIQUE (name)
);

CREATE TABLE license_status (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name TEXT NOT NULL,
    CONSTRAINT uq_license_status_name UNIQUE (name)
);

CREATE TABLE nationality (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name TEXT NOT NULL,
    CONSTRAINT uq_nationality_name UNIQUE (name)
);

CREATE TABLE licenses (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    type_id INT NOT NULL,
    status_id INT NOT NULL,
    nationality_id INT NOT NULL,
    number TEXT NOT NULL,
    issuance_date DATE NOT NULL,
    expiry_date DATE NOT NULL,
    first_name TEXT NOT NULL,
    middle_name TEXT,
    last_name TEXT NOT NULL,
    sex TEXT NOT NULL,
    date_of_birth DATE NOT NULL,
    address TEXT NOT NULL,
    eye_color TEXT NOT NULL,
    height INT NOT NULL,
    weight INT NOT NULL,
    blood_type TEXT NOT NULL,
    CONSTRAINT uq_licenses_number 
        UNIQUE(number),
    CONSTRAINT chk_licenses_sex 
        CHECK (sex in ('Male', 'Female')),
    CONSTRAINT chk_licenses_eye_color 
        CHECK (eye_color in ('Brown', 'Blue', 'Green', 'Hazel', 'Gray', 'Amber')),
    CONSTRAINT chk_licenses_blood_type 
        CHECK (blood_type in ('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-')),
    CONSTRAINT fk_licenses_type
        FOREIGN KEY (type_id)
        REFERENCES license_type(id)
        ON DELETE RESTRICT,
    CONSTRAINT fk_licenses_status
        FOREIGN KEY (status_id)
        REFERENCES license_status(id)
        ON DELETE RESTRICT,
    CONSTRAINT fk_licenses_nationality
        FOREIGN KEY (nationality_id)
        REFERENCES nationality(id)
        ON DELETE RESTRICT
);

CREATE TABLE user_role (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name TEXT NOT NULL,
    CONSTRAINT uq_user_role_name UNIQUE (name)
);

CREATE TABLE users (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    role_id INT NOT NULL,
    license_id INT,
    email TEXT NOT NULL,
    password TEXT NOT NULL,
    CONSTRAINT uq_users_email UNIQUE (email),
    CONSTRAINT uq_users_license_id UNIQUE (license_id),
    CONSTRAINT fk_users_role
        FOREIGN KEY (role_id)
        REFERENCES user_role(id)
        ON DELETE RESTRICT,
    CONSTRAINT fk_users_licenses
        FOREIGN KEY (license_id)
        REFERENCES licenses(id)
        ON DELETE SET NULL
);

CREATE TABLE vehicles (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    license_id INT,
    plate_number TEXT NOT NULL,
    mv_file_number TEXT,
    vin TEXT NOT NULL,
    issuance_date DATE NOT NULL,
    expiry_date DATE NOT NULL,
    make TEXT NOT NULL,
    model TEXT NOT NULL,
    year INTEGER NOT NULL,
    color TEXT NOT NULL,
    CONSTRAINT uq_vehicles_plate_number UNIQUE (plate_number),
    CONSTRAINT uq_vehicles_mv_file_number UNIQUE (mv_file_number),
    CONSTRAINT uq_vehicles_vin UNIQUE (vin),
    CONSTRAINT fk_vehicles_licenses
        FOREIGN KEY (license_id)
        REFERENCES licenses(id)
        ON DELETE SET NULL
);

-- migrate:down

