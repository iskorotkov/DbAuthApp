using System.Windows.Controls;
using System.Windows.Media;

namespace DbAuthApp.Registration
{
    public class TextBoxDecorator
    {
        private TextBox _textBox;
        private Brush _defaultBorderBrush;
        
        public TextBoxDecorator(TextBox textBox)
        {
            _textBox = textBox;
            _defaultBorderBrush = textBox.BorderBrush;
        }
        
        public void InputIsIncorrect(string message)
        {
            _textBox.BorderBrush = Brushes.Red;
            _textBox.ToolTip = message;
        }

        public void InputIsCorrect()
        {
            _textBox.BorderBrush = Brushes.Green;
            _textBox.ToolTip = "";
        }

        public void Reset()
        {
            _textBox.BorderBrush = _defaultBorderBrush;
        }
    }
}
