namespace BO
{
    /// <summary>
    /// User using the program: mannager or passenger
    /// Identifier: Username
    /// </summary>
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsManager { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
