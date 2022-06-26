namespace Quera.Models {
    public class GitBranch {
        public GitBranch(string name, bool isCurrentBranch) {
            Name = name;
            IsCurrentBranch = isCurrentBranch;
        }
        
        public string Name { get; set; }
        public bool IsCurrentBranch { get; set; }
    }
}