using Microsoft.EntityFrameworkCore;
using Buckeye.Lending.Api.Models;

namespace Buckeye.Lending.Api.Data;

public class LendingContext : DbContext
{
    public LendingContext(DbContextOptions<LendingContext> options)
        : base(options) { }

    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<LoanType> LoanTypes { get; set; }
    public DbSet<LoanApplicationDto> LoanApplications { get; set; }
    public DbSet<LoanPayment> LoanPayments { get; set; }
    public DbSet<LoanNote> LoanNotes { get; set; }
    public DbSet<ReviewQueue> ReviewQueues { get; set; }
    public DbSet<ReviewItem> ReviewItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed Applicants
        modelBuilder.Entity<Applicant>().HasData(
            new Applicant { Id = 1, Name = "Sarah Johnson", Email = "sarah.johnson@email.com", Phone = "614-555-0101", CreatedDate = new DateTime(2026, 1, 10, 0, 0, 0, DateTimeKind.Utc) },
            new Applicant { Id = 2, Name = "Michael Chen", Email = "michael.chen@email.com", Phone = "614-555-0102", CreatedDate = new DateTime(2026, 1, 25, 0, 0, 0, DateTimeKind.Utc) },
            new Applicant { Id = 3, Name = "Emily Rodriguez", Email = "emily.rodriguez@email.com", Phone = "614-555-0103", CreatedDate = new DateTime(2026, 1, 20, 0, 0, 0, DateTimeKind.Utc) },
            new Applicant { Id = 4, Name = "David Kim", Email = "david.kim@email.com", Phone = "614-555-0104", CreatedDate = new DateTime(2026, 1, 30, 0, 0, 0, DateTimeKind.Utc) },
            new Applicant { Id = 5, Name = "Jessica Martinez", Email = "jessica.martinez@email.com", Phone = "614-555-0105", CreatedDate = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Applicant { Id = 6, Name = "James Wilson", Email = "james.wilson@email.com", Phone = "614-555-0106", CreatedDate = new DateTime(2026, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
            new Applicant { Id = 7, Name = "Amanda Foster", Email = "amanda.foster@email.com", Phone = "614-555-0107", CreatedDate = new DateTime(2026, 2, 5, 0, 0, 0, DateTimeKind.Utc) },
            new Applicant { Id = 8, Name = "Robert Taylor", Email = "robert.taylor@email.com", Phone = "614-555-0108", CreatedDate = new DateTime(2026, 2, 2, 0, 0, 0, DateTimeKind.Utc) }
        );

        // Seed LoanTypes
        modelBuilder.Entity<LoanType>().HasData(
            new LoanType { Id = 1, Name = "Mortgage", Description = "Home purchase or refinance loan", MaxTermMonths = 360 },
            new LoanType { Id = 2, Name = "Auto", Description = "Vehicle purchase loan", MaxTermMonths = 84 },
            new LoanType { Id = 3, Name = "Personal", Description = "General-purpose unsecured loan", MaxTermMonths = 60 },
            new LoanType { Id = 4, Name = "Business", Description = "Small business or startup loan", MaxTermMonths = 120 }
        );

        // Seed LoanApplications (using FK ids instead of raw strings)
        modelBuilder.Entity<LoanApplicationDto>().HasData(
            new LoanApplicationDto { Id = 1, ApplicantName = "Sarah Johnson", ApplicantId = 1, LoanTypeId = 1, LoanAmount = 250000m, AnnualIncome = 95000m, Status = "Approved", RiskRating = 2, SubmittedDate = new DateTime(2026, 1, 15, 0, 0, 0, DateTimeKind.Utc), Notes = "Strong credit history, stable employment 8 years" },
            new LoanApplicationDto { Id = 2, ApplicantName = "Michael Chen", ApplicantId = 2, LoanTypeId = 2, LoanAmount = 32500m, AnnualIncome = 68000m, Status = "Pending", RiskRating = 3, SubmittedDate = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc), Notes = "First-time auto loan, good income-to-debt ratio" },
            new LoanApplicationDto { Id = 3, ApplicantName = "Emily Rodriguez", ApplicantId = 3, LoanTypeId = 1, LoanAmount = 320000m, AnnualIncome = 72000m, Status = "Denied", RiskRating = 5, SubmittedDate = new DateTime(2026, 1, 28, 0, 0, 0, DateTimeKind.Utc), Notes = "Income-to-loan ratio exceeds threshold" },
            new LoanApplicationDto { Id = 4, ApplicantName = "David Kim", ApplicantId = 4, LoanTypeId = 3, LoanAmount = 15000m, AnnualIncome = 52000m, Status = "Approved", RiskRating = 2, SubmittedDate = new DateTime(2026, 2, 3, 0, 0, 0, DateTimeKind.Utc), Notes = "Consolidating credit card debt, excellent history" },
            new LoanApplicationDto { Id = 5, ApplicantName = "Jessica Martinez", ApplicantId = 5, LoanTypeId = 4, LoanAmount = 500000m, AnnualIncome = 150000m, Status = "Under Review", RiskRating = 4, SubmittedDate = new DateTime(2026, 2, 5, 0, 0, 0, DateTimeKind.Utc), Notes = "New restaurant venture, limited business credit" },
            new LoanApplicationDto { Id = 6, ApplicantName = "James Wilson", ApplicantId = 6, LoanTypeId = 2, LoanAmount = 28000m, AnnualIncome = 75000m, Status = "Approved", RiskRating = 1, SubmittedDate = new DateTime(2026, 1, 20, 0, 0, 0, DateTimeKind.Utc), Notes = "Repeat customer, excellent credit score 780+" },
            new LoanApplicationDto { Id = 7, ApplicantName = "Amanda Foster", ApplicantId = 7, LoanTypeId = 1, LoanAmount = 175000m, AnnualIncome = 88000m, Status = "Pending", RiskRating = 3, SubmittedDate = new DateTime(2026, 2, 10, 0, 0, 0, DateTimeKind.Utc), Notes = "First-time homebuyer, pending employment verification" },
            new LoanApplicationDto { Id = 8, ApplicantName = "Robert Taylor", ApplicantId = 8, LoanTypeId = 4, LoanAmount = 75000m, AnnualIncome = 120000m, Status = "Denied", RiskRating = 4, SubmittedDate = new DateTime(2026, 2, 8, 0, 0, 0, DateTimeKind.Utc), Notes = "Insufficient collateral for requested amount" }
        );

        // Seed LoanPayments (for approved loans)
        modelBuilder.Entity<LoanPayment>().HasData(
            new LoanPayment { Id = 1, LoanApplicationId = 1, Amount = 1450.00m, PaymentDate = new DateTime(2026, 2, 15, 0, 0, 0, DateTimeKind.Utc), Method = "ACH" },
            new LoanPayment { Id = 2, LoanApplicationId = 4, Amount = 325.00m, PaymentDate = new DateTime(2026, 2, 10, 0, 0, 0, DateTimeKind.Utc), Method = "ACH" },
            new LoanPayment { Id = 3, LoanApplicationId = 6, Amount = 475.00m, PaymentDate = new DateTime(2026, 2, 20, 0, 0, 0, DateTimeKind.Utc), Method = "Check" }
        );

        // Seed LoanNotes
        modelBuilder.Entity<LoanNote>().HasData(
            new LoanNote { Id = 1, LoanApplicationId = 1, Author = "Loan Officer", Text = "Strong credit history, stable employment 8 years", CreatedDate = new DateTime(2026, 1, 15, 0, 0, 0, DateTimeKind.Utc) },
            new LoanNote { Id = 2, LoanApplicationId = 1, Author = "Underwriter", Text = "Approved — meets all DTI requirements", CreatedDate = new DateTime(2026, 1, 16, 0, 0, 0, DateTimeKind.Utc) },
            new LoanNote { Id = 3, LoanApplicationId = 3, Author = "Underwriter", Text = "Income-to-loan ratio exceeds 4.5x threshold", CreatedDate = new DateTime(2026, 1, 29, 0, 0, 0, DateTimeKind.Utc) },
            new LoanNote { Id = 4, LoanApplicationId = 5, Author = "Loan Officer", Text = "New restaurant venture — requesting additional financials", CreatedDate = new DateTime(2026, 2, 6, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
