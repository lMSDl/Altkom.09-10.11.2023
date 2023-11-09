using IdentityServer4.Models;

namespace IdentityServer
{
    public  class Context
    {
        private Context() { }
        static Context() { }
        public static Context Instance { get; } = new Context();

        public IEnumerable<IdentityResources> IdentityResources { get; } =
            new IdentityResources[]
            {
            };

    }
}
