CREATE TABLE tasks (
_id INTEGER PRIMARY KEY ,
online_id TEXT UNIQUE ,
local_id TEXT UNIQUE ,
change_key TEXT ,
delete_after_sync INTEGER DEFAULT(0) ,
due_date TEXT ,
due_date_changed INTEGER DEFAULT(0) ,
task_folder_local_id TEXT ,
task_folder_local_id_changed INTEGER DEFAULT(0) ,
status TEXT DEFAULT('NotStarted') ,
status_changed INTEGER DEFAULT(0) ,
importance INTEGER DEFAULT(0) ,
importance_changed INTEGER DEFAULT(0) ,
subject TEXT ,
subject_changed INTEGER DEFAULT(0) ,
body_content TEXT ,
body_content_changed INTEGER DEFAULT(0) ,
original_body_content TEXT ,
body_content_type TEXT DEFAULT('Text') ,
body_content_type_changed INTEGER DEFAULT(0) ,
body_last_modified TEXT ,
body_last_modified_changed INTEGER DEFAULT(0) ,
is_reminder_on INTEGER DEFAULT(0) ,
is_reminder_on_changed INTEGER DEFAULT(0) ,
reminder_datetime TEXT ,
reminder_datetime_changed INTEGER DEFAULT(0) ,
reminder_type TEXT DEFAULT('Default') ,
position TEXT DEFAULT (STRFTIME('%Y-%m-%dT%H:%M:%S', 'now')) ,
position_changed INTEGER DEFAULT(0) ,
created_datetime TEXT ,
completed_datetime TEXT ,
completed_datetime_changed INTEGER DEFAULT(0) ,
imported INTEGER DEFAULT(0) ,
postponed_date TEXT ,
postponed_date_changed INTEGER DEFAULT(0) ,
committed_date TEXT ,
committed_date_changed INTEGER DEFAULT(0) ,
committed_order TEXT ,
committed_order_changed INTEGER DEFAULT(0) ,
is_ignored INTEGER DEFAULT(0) ,
is_ignored_changed INTEGER DEFAULT(0) ,
deleted INTEGER DEFAULT(0) ,
recurrence_type TEXT ,
recurrence_interval INTEGER DEFAULT(1) ,
recurrence_interval_type TEXT ,
recurrence_days_of_week TEXT ,
recurrence_changed INTEGER DEFAULT(0) ,
source TEXT ,
created_by TEXT ,
completed_by TEXT ,
allowed_scopes TEXT ,
last_modified_date_time TEXT
)