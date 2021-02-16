using System;

namespace SimpleMVVM.Services
{
    public class Option
    {
        public Option(string text, object result)
        {
            Id = Guid.NewGuid().ToString();
            Text = text;
            Result = result;
        }

        public string Text { get; set; }

        public object Id { get; set; }

        public object Result { get; set; }
    }
}