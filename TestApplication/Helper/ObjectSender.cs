using System;
using TestApplication.Model;

namespace TestApplication.Helper
{

    public class ObjectContainer:EventArgs
    {
        public ObjectInfrastructure ObjectInfrastructure { get; set; }
        public Set Set { get; set; }
        public Document Document { get; set; }
        public FileInfo FileInfo { get; set; }
    }

    /// <summary>
    ///простой MVVM messenger
    /// </summary>
    public static class ObjectSender
    {
        public static EventHandler Receive;
        public static event EventHandler OnReceive=delegate {};

        public static void Send(object sender, ObjectContainer container)
        {
            OnReceive(sender, container);
        }
    }
}
