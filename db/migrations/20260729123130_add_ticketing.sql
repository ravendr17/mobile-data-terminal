-- migrate:up
CREATE TABLE violations (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name TEXT NOT NULL,
    is_tiered BOOLEAN NOT NULL,
    initial_fine NUMERIC(10, 2) NOT NULL,
    second_fine NUMERIC(10, 2),
    third_fine NUMERIC(10, 2),
    CONSTRAINT uq_violations_name UNIQUE (name),
    CONSTRAINT chk_violations_tiering
        CHECK (
            (is_tiered AND second_fine IS NOT NULL AND third_fine IS NOT NULL)
            OR
            (NOT is_tiered AND second_fine IS NULL AND third_fine IS NULL)
        )
);

CREATE TABLE ticket_status (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name TEXT NOT NULL,
    CONSTRAINT uq_ticket_status UNIQUE (name)
);

CREATE TABLE tickets (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    license_id INT NOT NULL,
    status_id INT NOT NULL,
    reference_number TEXT NOT NULL,
    date_of_incident DATE NOT NULL,
    place_of_incident TEXT NOT NULL,
    officer_notes TEXT,
    created_at TIMESTAMPTZ DEFAULT now(),
    updated_at TIMESTAMPTZ,
    CONSTRAINT uq_tickets_reference_number UNIQUE (reference_number),
    CONSTRAINT fk_tickets_licenses
        FOREIGN KEY (license_id)
        REFERENCES licenses(id)
        ON DELETE RESTRICT,
    CONSTRAINT fk_tickets_status
        FOREIGN KEY (status_id)
        REFERENCES ticket_status(id)
        ON DELETE RESTRICT
);

CREATE TABLE ticket_items (
    ticket_id INT NOT NULL,
    violation_id INT NOT NULL,
    fine NUMERIC(10, 2) NOT NULL,
    PRIMARY KEY (ticket_id, violation_id),
    CONSTRAINT fk_ticket_items_tickets
        FOREIGN KEY (ticket_id)
        REFERENCES tickets(id)
        ON DELETE CASCADE,
    CONSTRAINT fk_ticket_items_violations
        FOREIGN KEY (violation_id)
        REFERENCES violations(id)
        ON DELETE RESTRICT
);

-- migrate:down

