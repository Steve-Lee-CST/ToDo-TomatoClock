CREATE TABLE groups (
_id INTEGER PRIMARY KEY ,
online_id TEXT UNIQUE ,
local_id TEXT UNIQUE ,
change_key TEXT ,
deleted INTEGER DEFAULT(0) ,
delete_after_sync INTEGER DEFAULT(0) ,
name TEXT ,
name_changed INTEGER DEFAULT(0) ,
position TEXT DEFAULT (STRFTIME('%Y-%m-%dT%H:%M:%S', 'now')) ,
position_changed INTEGER DEFAULT(0) ,
is_expanded INTEGER DEFAULT(0)
)