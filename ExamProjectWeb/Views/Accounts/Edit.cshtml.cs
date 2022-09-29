#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamProjectWeb.Data;
using ExamProjectWeb.Models;

namespace ExamProjectWeb.Views.Accounts
{
    public class EditModel : PageModel
    {
        private readonly ExamProjectWeb.Data.ApplicationDbContext _context;

        public EditModel(ExamProjectWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserInformationModel UserInformationModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserInformationModel = await _context.UserInfos.FirstOrDefaultAsync(m => m.ID == id);

            if (UserInformationModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserInformationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInformationModelExists(UserInformationModel.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserInformationModelExists(int id)
        {
            return _context.UserInfos.Any(e => e.ID == id);
        }
    }
}
