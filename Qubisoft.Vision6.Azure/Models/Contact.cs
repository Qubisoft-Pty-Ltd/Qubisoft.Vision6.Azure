using Qubisoft.Vision6.Models;

namespace Qubisoft.Vision6.Azure.Models
{
    public class Contact : IContact
    {
        public string email { get; set; }
        public string mobile { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string answers { get; set; }
        public ISubscribedStatus subscribed { get; set; }
        public bool? is_active { get; set; }
        public bool? double_opt_in { get; set; }
    }
}
