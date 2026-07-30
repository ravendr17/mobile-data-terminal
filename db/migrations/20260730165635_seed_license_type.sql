-- migrate:up
INSERT INTO license_type (name)
VALUES 
    ('Professional'),
    ('Non-Professional'),
    ('Student Permit');

-- migrate:down

