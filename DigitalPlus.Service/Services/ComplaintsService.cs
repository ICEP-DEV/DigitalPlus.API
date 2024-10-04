using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public async Task<Complaint> Add(Complaint t)
    {
        try
        {
            _context.Complaints.Add(t);
            await _context.SaveChangesAsync();  // Save changes to the database
            return t;  // Return the added complaint
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding complaint: {ex.Message}");
        }
    }

    // Delete a complaint from the database
    public async Task<Complaint> Delete(Complaint t)
    {
        try
        {
            var complaint = await _context.Complaints.FindAsync(t.ComplaintId);
            if (complaint == null)
            {
                throw new Exception("Complaint not found");
            }

            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync();  // Save changes to the database
            return complaint;  // Return the deleted complaint
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting complaint: {ex.Message}");
        }
    }

    // Get a single complaint by ID
    public async Task<Complaint> Get(int id)
    {
        try
        {
            var complaint = await _context.Complaints
                .Include(c => c.Module)  // Include the related Module entity
                .FirstOrDefaultAsync(c => c.ComplaintId == id);

            if (complaint == null)
            {
                throw new Exception("Complaint not found");
            }

            return complaint;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving complaint: {ex.Message}");
        }
    }

    // Get all complaints from the database
    public async Task<IEnumerable<Complaint>> GetAll()
    {
        try
        {
            return await _context.Complaints
                .Include(c => c.Module)  // Include related Module entity in the query
                .ToListAsync();  // Return all complaints
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving complaints: {ex.Message}");
        }
    }

    // Update an existing complaint
    public async Task<Complaint> Update(Complaint t)
    {
        try
        {
            var existingComplaint = await _context.Complaints.FindAsync(t.ComplaintId);
            if (existingComplaint == null)
            {
                throw new Exception("Complaint not found");
            }

            // Update the existing complaint's properties
            existingComplaint.MenteeName = t.MenteeName;
            existingComplaint.MenteeEmail = t.MenteeEmail;
            existingComplaint.MentorName = t.MentorName;
            existingComplaint.MentorEmail = t.MentorEmail;
            existingComplaint.ModuleId = t.ModuleId;  // Update Module ID if needed
            existingComplaint.ComplaintDescription = t.ComplaintDescription;
            existingComplaint.Status = t.Status;
            existingComplaint.Action = t.Action;

            _context.Complaints.Update(existingComplaint);
            await _context.SaveChangesAsync();  // Save changes to the database

            return existingComplaint;  // Return the updated complaint
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating complaint: {ex.Message}");
        }
    }
}
