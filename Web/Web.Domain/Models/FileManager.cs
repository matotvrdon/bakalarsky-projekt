using Web.Domain.Enums;

namespace Web.Domain.Models;

public class FileManager
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public required Participant Participant { get; set; }
    public FileType FileType { get; set; }
    public FileStatus FileStatus { get; set; }
    public required string FilePath { get; set; }
    public required string FileName { get; set; }
    public required string OriginalFileName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public int? ReviewedByUserId { get; set; }
}