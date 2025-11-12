using System.Globalization;

namespace RestaurantApp.Converters;

public class DecimalConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not decimal number)
            return string.Empty;

        return number.ToString("F2", culture);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string? text = value as string;
        if (string.IsNullOrWhiteSpace(text))
            return Binding.DoNothing;

        text = text.Replace(',', '.');

        if (decimal.TryParse(text, NumberStyles.Any, culture, out decimal result))
            return result;

        return Binding.DoNothing;
    }
}
