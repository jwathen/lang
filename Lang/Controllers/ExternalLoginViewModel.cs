using Microsoft.AspNetCore.Identity;

namespace Lang.Controllers
{
    internal class ExternalLoginViewModel
    {
        private ExternalLoginInfo info;

        public ExternalLoginViewModel(ExternalLoginInfo info)
        {
            this.info = info;
        }
    }
}