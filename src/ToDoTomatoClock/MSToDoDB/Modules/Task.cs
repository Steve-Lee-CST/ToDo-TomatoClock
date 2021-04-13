
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSToDoDB.Modules
{
    [Table("tasks")]
    public class Task {
        
        [Column("_id")]
        public Int64 Id { get; set; }
        
        [Column("online_id")]
        public String OnlineId { get; set; }
        
        [Column("local_id")]
        public String LocalId { get; set; }
        
        [Column("change_key")]
        public String ChangeKey { get; set; }
        
        [Column("delete_after_sync"), DefaultValue(0)]
        public Int64 DeleteAfterSync { get; set; }
        
        [Column("due_date")]
        public String DueDate { get; set; }
        
        [Column("due_date_changed"), DefaultValue(0)]
        public Int64 DueDateChanged { get; set; }
        
        [Column("task_folder_local_id")]
        public String TaskFolderLocalId { get; set; }
        
        [Column("task_folder_local_id_changed"), DefaultValue(0)]
        public Int64 TaskFolderLocalIdChanged { get; set; }
        
        [Column("status"), DefaultValue("NotStarted")]
        public String Status { get; set; }
        
        [Column("status_changed"), DefaultValue(0)]
        public Int64 StatusChanged { get; set; }
        
        [Column("importance"), DefaultValue(0)]
        public Int64 Importance { get; set; }
        
        [Column("importance_changed"), DefaultValue(0)]
        public Int64 ImportanceChanged { get; set; }
        
        [Column("subject")]
        public String Subject { get; set; }
        
        [Column("subject_changed"), DefaultValue(0)]
        public Int64 SubjectChanged { get; set; }
        
        [Column("body_content")]
        public String BodyContent { get; set; }
        
        [Column("body_content_changed"), DefaultValue(0)]
        public Int64 BodyContentChanged { get; set; }
        
        [Column("original_body_content")]
        public String OriginalBodyContent { get; set; }
        
        [Column("body_content_type"), DefaultValue("Text")]
        public String BodyContentType { get; set; }
        
        [Column("body_content_type_changed"), DefaultValue(0)]
        public Int64 BodyContentTypeChanged { get; set; }
        
        [Column("body_last_modified")]
        public String BodyLastModified { get; set; }
        
        [Column("body_last_modified_changed"), DefaultValue(0)]
        public Int64 BodyLastModifiedChanged { get; set; }
        
        [Column("is_reminder_on"), DefaultValue(0)]
        public Int64 IsReminderOn { get; set; }
        
        [Column("is_reminder_on_changed"), DefaultValue(0)]
        public Int64 IsReminderOnChanged { get; set; }
        
        [Column("reminder_datetime")]
        public String ReminderDatetime { get; set; }
        
        [Column("reminder_datetime_changed"), DefaultValue(0)]
        public Int64 ReminderDatetimeChanged { get; set; }
        
        [Column("reminder_type"), DefaultValue("Default")]
        public String ReminderType { get; set; }
        
        [Column("position")]
        public String Position { get; set; }
        
        [Column("position_changed"), DefaultValue(0)]
        public Int64 PositionChanged { get; set; }
        
        [Column("created_datetime")]
        public String CreatedDatetime { get; set; }
        
        [Column("completed_datetime")]
        public String CompletedDatetime { get; set; }
        
        [Column("completed_datetime_changed"), DefaultValue(0)]
        public Int64 CompletedDatetimeChanged { get; set; }
        
        [Column("imported"), DefaultValue(0)]
        public Int64 Imported { get; set; }
        
        [Column("postponed_date")]
        public String PostponedDate { get; set; }
        
        [Column("postponed_date_changed"), DefaultValue(0)]
        public Int64 PostponedDateChanged { get; set; }
        
        [Column("committed_date")]
        public String CommittedDate { get; set; }
        
        [Column("committed_date_changed"), DefaultValue(0)]
        public Int64 CommittedDateChanged { get; set; }
        
        [Column("committed_order")]
        public String CommittedOrder { get; set; }
        
        [Column("committed_order_changed"), DefaultValue(0)]
        public Int64 CommittedOrderChanged { get; set; }
        
        [Column("is_ignored"), DefaultValue(0)]
        public Int64 IsIgnored { get; set; }
        
        [Column("is_ignored_changed"), DefaultValue(0)]
        public Int64 IsIgnoredChanged { get; set; }
        
        [Column("deleted"), DefaultValue(0)]
        public Int64 Deleted { get; set; }
        
        [Column("recurrence_type")]
        public String RecurrenceType { get; set; }
        
        [Column("recurrence_interval"), DefaultValue(1)]
        public Int64 RecurrenceInterval { get; set; }
        
        [Column("recurrence_interval_type")]
        public String RecurrenceIntervalType { get; set; }
        
        [Column("recurrence_days_of_week")]
        public String RecurrenceDaysOfWeek { get; set; }
        
        [Column("recurrence_changed"), DefaultValue(0)]
        public Int64 RecurrenceChanged { get; set; }
        
        [Column("source")]
        public String Source { get; set; }
        
        [Column("created_by")]
        public String CreatedBy { get; set; }
        
        [Column("completed_by")]
        public String CompletedBy { get; set; }
        
        [Column("allowed_scopes")]
        public String AllowedScopes { get; set; }
        
        [Column("last_modified_date_time")]
        public String LastModifiedDateTime { get; set; }

        // extra-datastructure
        // tree: (Group-)TaskFolder-Task-Step
        [NotMapped]
        public TaskFolder Parent { get; set; }

        [NotMapped]
        public List<Step> Childs { get; set; } = new List<Step>();
    }
}

