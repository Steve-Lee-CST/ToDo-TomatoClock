CREATE TABLE members (
_id INTEGER PRIMARY KEY ,
online_id TEXT NOT NULL ,
display_name TEXT ,
avatar_url TEXT ,
task_folder_local_id TEXT NOT NULL ,
delete_after_sync INTEGER DEFAULT(0) ,
owner INTEGER DEFAULT(0)
)