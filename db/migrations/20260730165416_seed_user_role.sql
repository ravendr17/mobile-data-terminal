-- migrate:up
INSERT INTO user_role (name)
VALUES 
    ('Civilian'),
    ('Officer'),
    ('Supervisor'),
    ('Admin');

-- migrate:down

