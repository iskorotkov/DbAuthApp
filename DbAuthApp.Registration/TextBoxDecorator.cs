using System.Windows.Controls;
using System.Windows.Media;

namespace DbAuthApp.Registration
{
    public class TextBoxDecorator
    {
        private readonly Brush _defaultBorderBrush;
        private readonly string _defaultTooltip;
        private readonly Control _textBox;

        public TextBoxDecorator(Control textBox)
        {
            _textBox = textBox;
            _defaultBorderBrush = textBox.BorderBrush;
            _defaultTooltip = (string) textBox.ToolTip;
        }

        public bool IsCorrect { get; private set; }

        public void InputIsIncorrect(string message)
        {
            _textBox.BorderBrush = Brushes.Red;
            _textBox.ToolTip = message;
            IsCorrect = false;
        }

        public void InputIsCorrect()
        {
            _textBox.BorderBrush = Brushes.Green;
            _textBox.ToolTip = _defaultTooltip;
            IsCorrect = true;
        }

        public void Reset()
        {
            _textBox.BorderBrush = _defaultBorderBrush;
            _textBox.ToolTip = _defaultTooltip;
            IsCorrect = false;
        }
    }
}
