using System.Windows.Controls;
using System.Windows.Media;

namespace DbAuthApp.Registration
{
    public class TextBoxDecorator
    {
        public delegate void OnIsCorrectChangedDelegate();

        private readonly Brush _defaultBorderBrush;
        private readonly string _defaultTooltip;
        private readonly Control _textBox;
        private bool _isCorrect;

        public OnIsCorrectChangedDelegate OnIsCorrectChanged;

        public TextBoxDecorator(Control textBox)
        {
            _textBox = textBox;
            _defaultBorderBrush = textBox.BorderBrush;
            _defaultTooltip = (string) textBox.ToolTip;
        }

        public bool IsCorrect
        {
            get => _isCorrect;
            private set
            {
                _isCorrect = value;
                OnIsCorrectChanged.Invoke();
            }
        }

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
