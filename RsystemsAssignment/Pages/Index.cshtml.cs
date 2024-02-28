using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RsystemsAssignment.Business.Interfaces;
using RSystemsAssignment.Data.DTO;

namespace RsystemsAssignment.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAccountService _accountService;
        public List<AccountDTO> dto = new List<AccountDTO>();

        public IndexModel(ILogger<IndexModel> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public async void OnGet()
        {
            
        }
    }
}
