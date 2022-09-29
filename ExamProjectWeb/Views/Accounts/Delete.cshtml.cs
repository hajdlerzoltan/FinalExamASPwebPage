#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExamProjectWeb.Data;
using ExamProjectWeb.Models;

namespace ExamProjectWeb.Views.Accounts
{
    public class DeleteModel : PageModel
    {
        private readonly ExamProjectWeb.Data.ApplicationDbContext _context;

        public DeleteModel(ExamProjectWeb.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserInformationModel = await _context.UserInfos.FindAsync(id);

            if (UserInformationModel != null)
            {
                _context.UserInfos.Remove(UserInformationModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
