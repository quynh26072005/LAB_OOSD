using System.Windows.Forms;

namespace eShoppingPrototype.UI
{
    public static class FormValidator
    {
        public static bool ValidateRequired(TextBox textBox, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show($"Please enter {fieldName}", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Focus();
                return false;
            }
            return true;
        }
        
        public static bool ValidateRecipientInfo(TextBox name, TextBox address, TextBox phone)
        {
            return ValidateRequired(name, "recipient name") &&
                   ValidateRequired(address, "recipient address") &&
                   ValidateRequired(phone, "recipient phone");
        }
        
        public static bool ValidatePaymentInfo(TextBox cardNumber, TextBox cardHolder, TextBox csv)
        {
            return ValidateRequired(cardNumber, "card number") &&
                   ValidateRequired(cardHolder, "cardholder name") &&
                   ValidateRequired(csv, "CSV code");
        }
    }
}
