-- migrate:up
INSERT INTO license_status (name)
VALUES 
    ('Active'),
    ('Revoked'),
    ('Expired');

-- migrate:down

