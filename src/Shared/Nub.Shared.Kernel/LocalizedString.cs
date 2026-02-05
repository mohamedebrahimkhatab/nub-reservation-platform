namespace Nub.Shared.Kernel;

public class LocalizedString : ValueObject
{
    public string? English { get; private set; }
    public string? Arabic { get; private set; }

    public LocalizedString() {}

    public LocalizedString(string? english, string? arabic)
    {
        if (string.IsNullOrWhiteSpace(english) && string.IsNullOrWhiteSpace(arabic))
            throw new ArgumentException("English value or Arabic value is required.");

        English = english;
        Arabic = arabic;
    }

    public string Get(string language)
    {
        return language switch
        {
            "en" => English ?? Arabic ?? string.Empty,
            "ar" => Arabic ?? English ?? string.Empty,
            _ => throw new ArgumentException($"Unsupported language: {language}. Use 'en' or 'ar'.", nameof(language))
        };
    }

    public string GetOrDefault(string language, string fallback = "")
    {
        try
        {
            var value = Get(language);
            return string.IsNullOrEmpty(value)? fallback : value;
        }
        catch(ArgumentException)
        {
            return fallback;
        }
    }

    public bool HasEnglish()
    {
        return !string.IsNullOrWhiteSpace(English);
    }

    public bool HasArabic()
    {
        return !string.IsNullOrWhiteSpace(Arabic);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return English ?? string.Empty;
        yield return Arabic ?? string.Empty;
    }

    public override string ToString()
    {
        return $"English: {English ?? "(none)"}, Arabic: {Arabic ?? "(none)"}";
    }
}
