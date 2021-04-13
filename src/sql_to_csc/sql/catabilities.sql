CREATE TABLE capabilities (
_id INTEGER PRIMARY KEY ,
name TEXT UNIQUE NOT NULL ,
is_supported INTEGER DEFAULT(0) ,
delete_after_sync INTEGER DEFAULT(0)
)