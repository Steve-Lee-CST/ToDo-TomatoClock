CREATE TABLE assignments (
_id INTEGER PRIMARY KEY ,
online_id TEXT ,
local_id TEXT UNIQUE ,
task_local_id TEXT ,
deleted INTEGER DEFAULT(0) ,
delete_after_sync INTEGER DEFAULT(0) ,
assignee_id TEXT ,
assignee_id_type TEXT ,
assignee_display_name TEXT ,
assignee_avatar_url TEXT ,
assigner_id TEXT ,
assignment_source TEXT ,
assigned_datetime TEXT ,
position TEXT DEFAULT (STRFTIME('%Y-%m-%dT%H:%M:%S', 'now'))
)