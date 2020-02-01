using System.Windows.Controls;
using System.Windows.Media;

namespace DbAuthApp.Registration
{
    public class TextBoxDecorator
    {
        private readonly TextBox _textBox;
        private readonly Brush _defaultBorderBrush;
        private readonly string _defaultTooltip;

        public TextBoxDecorator(TextBox textBox)
        {
            _textBox = textBox;
            _defaultBorderBrush = textBox.BorderBrush;
            _defaultTooltip = (string) textBox.ToolTip;
        }

        public void InputIsIncorrect(string message)
        {
            _textBox.BorderBrush = Brushes.Red;
            _textBox.ToolTip = message;
        }

        public void InputIsCorrect()
        {
            _textBox.BorderBrush = Brushes.Green;
            _textBox.ToolTip = _defaultTooltip;
        }

        public void Reset()
        {
            _textBox.BorderBrush = _defaultBorderBrush;
            _textBox.ToolTip = _defaultTooltip;
        }
    }
}
