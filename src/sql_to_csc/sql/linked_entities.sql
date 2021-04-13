CREATE TABLE linked_entities (
_id INTEGER PRIMARY KEY ,
online_id TEXT ,
local_id TEXT UNIQUE ,
web_link TEXT ,
entity_type TEXT NOT NULL ,
entity_subtype TEXT ,
display_name TEXT ,
preview TEXT ,
application_name TEXT ,
client_state TEXT ,
delete_after_sync INTEGER DEFAULT(0) ,
task_local_id TEXT ,
position TEXT DEFAULT (STRFTIME('%Y-%m-%dT%H:%M:%S', 'now')) ,
position_changed INTEGER DEFAULT(0) ,
deleted INTEGER DEFAULT(0)
)