using System.Drawing;
using System.Windows.Forms;

namespace eShoppingPrototype.UI
{
    public static class ControlFactory
    {
        private static readonly Font DefaultFont = new Font("Segoe UI", 9);
        private static readonly Font BoldFont = new Font("Segoe UI", 10, FontStyle.Bold);
        
        public static Label CreateLabel(string text, Point location, int width = 100)
        {
            return new Label
            {
                Text = text,
                Location = location,
                Size = new Size(width, 25),
                Font = DefaultFont,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }
        
        public static TextBox CreateTextBox(Point location, int width, bool readOnly = false)
        {
            return new TextBox
            {
                Location = location,
                Size = new Size(width, 25),
                Font = DefaultFont,
                ReadOnly = readOnly
            };
        }
        
        public static ComboBox CreateComboBox(Point location, int width, params string[] items)
        {
            var combo = new ComboBox
            {
                Location = location,
                Size = new Size(width, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = DefaultFont
            };
            combo.Items.AddRange(items);
            combo.SelectedIndex = 0;
            return combo;
        }
        
        public static GroupBox CreateGroupBox(string text, Point location, Size size)
        {
            return new GroupBox
            {
                Text = text,
                Location = location,
                Size = size,
                Font = BoldFont
            };
        }
        
        public static Button CreateButton(string text, Point location, Size size, Color backColor)
        {
            return new Button
            {
                Text = text,
                Location = location,
                Size = size,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
        }
    }
}
