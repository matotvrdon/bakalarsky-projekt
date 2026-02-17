namespace Web.Domain.Models;

public class InvoiceParticipant
{
    public int InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
    public int ParticipantId { get; set; }
    public Participant? Participant { get; set; }
}