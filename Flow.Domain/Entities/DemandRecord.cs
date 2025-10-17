namespace Flow.Domain.Entities;

public partial class DemandRecord {
    public int DemandRecordId { get; set; }
    public int? Demand { get; set; }
    public string? Site { get; set; }
    public DateTime Timestamp { get; set; }
}