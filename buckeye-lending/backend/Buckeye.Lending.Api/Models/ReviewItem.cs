using System.ComponentModel.DataAnnotations;

namespace Buckeye.Lending.Api.Models;

public class ReviewItem
{
    public int Id { get; set; }

    // Foreign key to the queue
    public int QueueId { get; set; }
    public ReviewQueue Queue { get; set; } = null!;

    // Foreign key to the loan application
    public int LoanApplicationId { get; set; }
    public LoanApplicationDto LoanApplication { get; set; } = null!;

    [Range(1, 5)]
    public int Priority { get; set; } = 3;

    public string? Notes { get; set; }
}