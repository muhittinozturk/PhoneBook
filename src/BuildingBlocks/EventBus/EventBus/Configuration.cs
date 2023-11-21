namespace EventBus
{
    public class Configuration
    {
        public string TopicName { get; set; } = "phonebook_event_bus";
        public object Connection { get; set; }
        public string SubClientAppName { get; set; } = string.Empty;
    }
}
