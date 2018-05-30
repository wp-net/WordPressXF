using System;

namespace WordPressXF.Views.AppShell
{
    public class AppShellMenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Type TargetType { get; set; }
    }
}
