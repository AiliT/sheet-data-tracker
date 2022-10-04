using System.Windows.Controls;
using System.Windows.Input;

namespace SheetDataTracker
{
    public class UnfocusableTextBox : TextBox
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Return)
                Keyboard.ClearFocus();
        }
    }
}
