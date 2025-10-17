namespace Flow.Application.Models;

public sealed class DemandRecordVm {
    public int DemandRecordId { get; set; }
    public int? Demand { get; set; }
    public string? Site { get; set; }
    public DateTime Timestamp { get; set; }
}