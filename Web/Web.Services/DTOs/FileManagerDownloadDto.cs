namespace Web.Services.DTOs;

public class FileManagerDownloadDto
{
    public required string FilePath { get; set; }
    public required string ContentType { get; set; }
    public required string FileName { get; set; }
}