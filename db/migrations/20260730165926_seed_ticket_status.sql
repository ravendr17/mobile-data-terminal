-- migrate:up
INSERT INTO ticket_status (name)
VALUES
    ('Unsettled'),
    ('Settled');

-- migrate:down

