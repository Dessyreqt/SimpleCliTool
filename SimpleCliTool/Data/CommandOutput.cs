namespace SimpleCliTool.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CommandOutput
    {
        private string _text;

        public string Text
        {
            get => _text;
            set => _text = value + Environment.NewLine;
        }

        public List<string> TextList { get; set; }
        public bool Success { get; set; }

        public CommandOutput()
        {
            TextList = new List<string>();
            Success = true;
        }

        public CommandOutput(string text, bool success = true)
        {
            Text = text;
            TextList = new List<string>();
            Success = success;
        }

        public void SetTextList(List<string> textList)
        {
            TextList = textList;

            var text = new StringBuilder();

            for (int i = 0; i < textList.Count; i++)
            {
                text.AppendLine($"--{i}: {textList[i]}");
            }

            _text = text.ToString();
        }
    }
}
