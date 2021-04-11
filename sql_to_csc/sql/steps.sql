CREATE TABLE steps (
_id INTEGER PRIMARY KEY ,
online_id TEXT ,
local_id TEXT UNIQUE ,
delete_after_sync INTEGER DEFAULT(0) ,
task_local_id TEXT ,
task_local_id_changed INTEGER DEFAULT(0) ,
completed INTEGER DEFAULT(0) ,
completed_changed INTEGER DEFAULT(0) ,
subject TEXT ,
subject_changed INTEGER DEFAULT(0) ,
position TEXT DEFAULT (STRFTIME('%Y-%m-%dT%H:%M:%S', 'now')) ,
position_changed INTEGER DEFAULT(0) ,
created_datetime TEXT ,
deleted INTEGER DEFAULT(0)
)