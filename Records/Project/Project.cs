using System.Collections.Generic;

#if NET6_0_OR_GREATER
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace Zoho.Records.Project
{
    public record Project(
        string is_strict,
        BugCount bug_count,
        string owner_id,
        string bug_client_permission,
        string taskbug_prefix,
        long start_date_long,
        long updated_date_long,
        bool show_project_overview,
        TaskCount task_count,
        string updated_date_format,
        string bug_defaultview,
        string id,
        bool is_chat_enabled,
        bool is_sprints_project,
        string owner_name,
        long created_date_long,
        CustomField[] custom_fields,
        string created_by,
        string created_date_format,
        long profile_id,
        string name,
        string updated_by,
        string updated_by_id,
        string created_by_id,
        Task tasks,
        List<Task> subTasks
    );
    
};

#endif
