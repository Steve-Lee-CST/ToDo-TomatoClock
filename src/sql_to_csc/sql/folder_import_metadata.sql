CREATE TABLE folder_import_metadata (
_id INTEGER PRIMARY KEY ,
folder_local_id TEXT UNIQUE ,
wunderlist_id TEXT ,
was_shared INTEGER DEFAULT(0) ,
members TEXT ,
dismissed INTEGER DEFAULT(0) ,
dismissed_changed INTEGER DEFAULT(0)
)