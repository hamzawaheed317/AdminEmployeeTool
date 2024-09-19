namespace AdminEmployeeTool.Models
{
    public class FileUploadSettings
        {
            public string UploadFolderPath { get; set; } = "wwwroot/uploads";
            public string[] AllowedExtensions { get; set; } = { ".png", ".pdf", ".doc", ".docx", ".jpeg" };
            public int MaxFileSizeMB { get; set; } = 5; // 5 MB
        }
    }


