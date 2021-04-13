CREATE TABLE settings (
_id INTEGER PRIMARY KEY ,
key TEXT UNIQUE ,
value TEXT ,
value_changed INTEGER DEFAULT(0)
)