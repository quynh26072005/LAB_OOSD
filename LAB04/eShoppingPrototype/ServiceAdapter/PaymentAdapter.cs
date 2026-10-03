using System;
using System.Text.RegularExpressions;
using eShoppingPrototype.Models;

namespace eShoppingPrototype.ServiceAdapter
{
    public class PaymentAdapter
    {
        public ValidationResult ValidateCardInfo(TheTinDung card)
        {
            if (string.IsNullOrWhiteSpace(card.SoThe))
                return new ValidationResult(false, "Card number is required");

            string cleanedNumber = Regex.Replace(card.SoThe, @"[^\d]", "");

            switch (card.LoaiThe)
            {
                case "American Express":
                    if (cleanedNumber.Length != 15)
                        return new ValidationResult(false, "American Express card must have 15 digits");
                    if (card.CSV?.Length != 4)
                        return new ValidationResult(false, "American Express CSV must have 4 digits");
                    break;

                case "VISA":
                case "Master":
                case "Discover":
                    if (cleanedNumber.Length != 16)
                        return new ValidationResult(false, $"{card.LoaiThe} card must have 16 digits");
                    if (card.CSV?.Length != 3)
                        return new ValidationResult(false, $"{card.LoaiThe} CSV must have 3 digits");
                    break;

                default:
                    return new ValidationResult(false, "Invalid card type");
            }

            if (card.NgayHetHan < DateTime.Now)
                return new ValidationResult(false, "Card has expired");

            if (string.IsNullOrWhiteSpace(card.TenChuThe))
                return new ValidationResult(false, "Cardholder name is required");

            return new ValidationResult(true, "Validation successful");
        }

        public PaymentResult ProcessPayment(TheTinDung card, decimal amount)
        {
            var validation = ValidateCardInfo(card);
            if (!validation.IsValid)
                return new PaymentResult(false, validation.Message);

            System.Threading.Thread.Sleep(500);

            Random random = new Random();
            bool success = random.Next(100) < 95;

            if (success)
                return new PaymentResult(true, "Payment processed successfully");
            else
                return new PaymentResult(false, "Payment declined by bank");
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }

        public ValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }
    }

    public class PaymentResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public PaymentResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
