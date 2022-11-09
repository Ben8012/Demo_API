namespace Demo1_ASP_MVC.Service
{
    public class SessionManager
    {

        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor contextAccessor)
        {
            _session = contextAccessor.HttpContext.Session;
        }

        public int Id
        {
            get { return (int)_session.GetInt32("Id"); } 
            set { _session.SetInt32("Id", value); } 
        }

        public string Email
        {
            get { return _session.GetString("Email"); }
            set { _session.SetString("Email", value); }
        }

        public string UserName
        {
            get { return _session.GetString("Username"); }
            set { _session.SetString("Username", value); }
        }

        public void Clear()
        {
            _session.Clear();
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Email);
        }
    }
}
