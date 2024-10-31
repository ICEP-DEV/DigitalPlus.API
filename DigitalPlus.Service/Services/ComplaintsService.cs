using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ComplaintsService : ICrudInterface<Complaint>
{
    private readonly DigitalPlusDbContext _context;

    public ComplaintsService(DigitalPlusDbContext context)
    {
        _context = context;
    }

    // Add a new complaint to the database
    public async Task<Complaint> Add(Complaint complaint)
    {
        try
        {
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred while adding the complaint.", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding complaint: {ex.Message}", ex);
        }
    }

    // Delete a complaint from the database
    public async Task<Complaint> Delete(Complaint complaint)
    {
        try
        {
            var existingComplaint = await _context.Complaints.FindAsync(complaint.ComplaintId);
            if (existingComplaint == null)
            {
                throw new KeyNotFoundException("Complaint not found.");
            }

            _context.Complaints.Remove(existingComplaint);
            await _context.SaveChangesAsync();
            return existingComplaint;
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred while deleting the complaint.", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting complaint: {ex.Message}", ex);
        }
    }

    // Get a single complaint by ID
    public async Task<Complaint> Get(int id)
    {
        try
        {
            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(c => c.ComplaintId == id);

            if (complaint == null)
            {
                throw new KeyNotFoundException("Complaint not found.");
            }

            return complaint;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving complaint: {ex.Message}", ex);
        }
    }

    // Get all complaints from the database
    public async Task<IEnumerable<Complaint>> GetAll()
    {
        try
        {
            return await _context.Complaints
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving complaints: {ex.Message}", ex);
        }
    }

    // Update an existing complaint
    public async Task<Complaint> Update(Complaint complaint)
    {
        try
        {
            var existingComplaint = await _context.Complaints.FindAsync(complaint.ComplaintId);
            if (existingComplaint == null)
            {
                throw new KeyNotFoundException("Complaint not found.");
            }

            // Update fields
           
            existingComplaint.MenteeEmail = complaint.MenteeEmail;
            existingComplaint.MentorName = complaint.MentorName;
            existingComplaint.MentorEmail = complaint.MentorEmail;
            existingComplaint.ModuleName = complaint.ModuleName;
            existingComplaint.ComplaintDescription = complaint.ComplaintDescription;
            existingComplaint.Status = complaint.Status;
            existingComplaint.Action = complaint.Action;

            _context.Complaints.Update(existingComplaint);
            await _context.SaveChangesAsync();

            return existingComplaint;
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred while updating the complaint.", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating complaint: {ex.Message}", ex);
        }
    }
}
