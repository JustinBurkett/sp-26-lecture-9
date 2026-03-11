using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Buckeye.Lending.Api.Data;
using Buckeye.Lending.Api.Models;
using Buckeye.Lending.Api.Dtos;

namespace Buckeye.Lending.Api.Controllers;

[ApiController]
[Route("api/review-queue")]
public class ReviewQueueController : ControllerBase
{
    private readonly LendingContext _context;
    private const string CurrentOfficerId = "default-officer";

    public ReviewQueueController(LendingContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ReviewQueue>> GetQueue()
    {
        var queue = await _context.ReviewQueues
            .Include(q => q.Items)
            .ThenInclude(i => i.LoanApplication)
            .FirstOrDefaultAsync(q => q.OfficerId == CurrentOfficerId);

        if (queue == null)
        {
            return NotFound();
        }

        return Ok(queue);
    }

    [HttpPost]
    public async Task<ActionResult<ReviewItem>> AddToQueue(AddToQueueRequest request)
    {
        // 1. Verify the loan application exists
        var loanApp = await _context.LoanApplications.FindAsync(request.LoanApplicationId);
        if (loanApp == null)
        {
            return BadRequest($"Loan application {request.LoanApplicationId} not found.");
        }

        // 2. Find or create the queue for this officer
        var queue = await _context.ReviewQueues
            .Include(q => q.Items)
            .FirstOrDefaultAsync(q => q.OfficerId == CurrentOfficerId);

        if (queue == null)
        {
            queue = new ReviewQueue { OfficerId = CurrentOfficerId };
            _context.ReviewQueues.Add(queue);
        }

        // 3. Check if this loan application is already in the queue (UPSERT)
        var existingItem = queue.Items
            .FirstOrDefault(i => i.LoanApplicationId == request.LoanApplicationId);

        if (existingItem != null)
        {
            // Update — loan already in queue, just update priority
            existingItem.Priority = request.Priority;
            queue.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            // Insert — new item
            var newItem = new ReviewItem
            {
                LoanApplicationId = request.LoanApplicationId,
                Priority = request.Priority
            };
            queue.Items.Add(newItem);
            queue.UpdatedAt = DateTime.UtcNow;
        }

        // 4. Save everything in one transaction
        await _context.SaveChangesAsync();

        // 5. Reload with navigation properties for the response
        var savedItem = await _context.ReviewItems
            .Include(i => i.LoanApplication)
            .FirstAsync(i => i.QueueId == queue.Id
                && i.LoanApplicationId == request.LoanApplicationId);

        return CreatedAtAction(nameof(GetQueue), savedItem);
    }

    [HttpPut("{itemId}")]
    public async Task<ActionResult<ReviewItem>> UpdateItem(int itemId, UpdateItemRequest request)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    [HttpDelete("{itemId}")]
    public async Task<IActionResult> RemoveItem(int itemId)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> ClearQueue()
    {
        // TODO: implement
        throw new NotImplementedException();
    }
}