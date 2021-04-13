using MSToDoDB.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSToDoDB
{
    public class MSToDoDataCollection
    {
        public List<Assignment> Assignments { get; set; }
        public List<Capability> Capabilities { get; set; }
        public List<FolderImportMetadata> FolderImportMetadatas { get; set; }
        public List<LinkedEntity> LinkedEntities { get; set; }
        public List<Member> Members { get; set; }
        public List<Setting> Settings { get; set; }

        public Group DefaultGroup { get; set; }
        public List<Group> Groups { get; set; }
        public List<TaskFolder> TaskFolders { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Step> Steps { get; set; }

        protected Dictionary<string, Group> LidToGroup { get; set; }
        protected Dictionary<string, TaskFolder> LidToTaskFolder { get; set; }
        protected Dictionary<string, Task> LidToTask { get; set; }
        protected Dictionary<string, Step> LidToStep { get; set; }
        
        public MSToDoDataCollection(MSToDoContext context)
        {
            Assignments = context.Assignments.ToList();
            Capabilities = context.Capabilities.ToList();
            FolderImportMetadatas = context.FolderImportMetadatas.ToList();
            LinkedEntities = context.LinkedEntities.ToList();
            Members = context.Members.ToList();
            Settings = context.Settings.ToList();

            Groups = context.Groups.ToList();
            TaskFolders = context.TaskFolders.ToList();
            Tasks = context.Tasks.ToList();
            Steps = context.Steps.ToList();

            LidToGroup = Groups.ToDictionary(x => x.LocalId, x => x);
            LidToTaskFolder = TaskFolders.ToDictionary(x => x.LocalId, x => x);
            LidToTask = Tasks.ToDictionary(x => x.LocalId, x => x);
            LidToStep = Steps.ToDictionary(x => x.LocalId, x => x);
            DefaultGroup = new Group();

            foreach(var step in Steps)
            {
                LidToTask[step.TaskLocalId].Childs.Add(step);
                step.Parent = LidToTask[step.TaskLocalId];
            }

            foreach(var task in Tasks)
            {
                LidToTaskFolder[task.TaskFolderLocalId].Childs.Add(task);
                task.Parent = LidToTaskFolder[task.TaskFolderLocalId];
            }

            foreach (var taskFolder in TaskFolders)
            {
                if (taskFolder.GroupLocalId != null)
                {
                    LidToGroup[taskFolder.GroupLocalId].Childs.Add(taskFolder);
                    taskFolder.Parent = LidToGroup[taskFolder.GroupLocalId];
                }
            }

            foreach (var taskFolder in TaskFolders)
            {
                if (string.IsNullOrEmpty(taskFolder.GroupLocalId))
                {
                    DefaultGroup.Childs.Add(taskFolder);
                    taskFolder.Parent = DefaultGroup;
                }
            }
        }
    }
}
