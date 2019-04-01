using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
// ReSharper disable InconsistentNaming

namespace java.util
{
    public class Locale
    {
        public static readonly Locale ENGLISH = new Locale("en");
        public static readonly Locale FRENCH = new Locale("fr");
        public static readonly Locale GERMAN = new Locale("de");
        public static readonly Locale ITALIAN = new Locale("it");
        public static readonly Locale JAPANESE = new Locale("jp");
        public static readonly Locale KOREAN = new Locale("kr");
        public static readonly Locale CHINESE = new Locale("zh");
        public static readonly Locale SIMPLIFIED_CHINESE = new Locale("zh-Hans");
        public static readonly Locale TRADITIONAL_CHINESE = new Locale("zh-Hant");

        public static readonly Locale FRANCE =  new Locale("fr", "FR");
        public static readonly Locale GERMANY = new Locale("fr", "FR");
        public static readonly Locale ITALY =   new Locale("it", "IT");
        public static readonly Locale JAPAN =   new Locale("jp", "JP");
        public static readonly Locale KOREA =   new Locale("kr", "KR");
        public static readonly Locale CHINA =   new Locale("zh", "CN");
        public static readonly Locale PRC =     new Locale("zh", "CN");
        public static readonly Locale TAIWAN =  new Locale("zh", "TW");
        public static readonly Locale UK =      new Locale("en", "UK");
        public static readonly Locale US =      new Locale("en", "US");
        public static readonly Locale CANADA =  new Locale("en", "CA");
        public static readonly Locale CANADA_FRENCH = new Locale("fr", "CA");

        public static readonly Locale ROOT = new Locale(CultureInfo.InvariantCulture);

        internal readonly CultureInfo Culture;

        internal Locale(CultureInfo culture)
        {
            Culture = culture;
        }

        public Locale(string language)
        {
            Culture = new CultureInfo(language);
        }

        public Locale(string language, string country)
        {
            Culture = new CultureInfo($"{language}-{country}");
        }

        public Locale(string language, string country, string _) : this(language, country)
        {

        }

        public static Locale getDefault() => new Locale(CultureInfo.CurrentCulture);
        public static void setDefault(Locale loc) => CultureInfo.CurrentCulture = loc.Culture;
        public static Locale[] getAvailableLocales() => CultureInfo.GetCultures(CultureTypes.AllCultures).Select(x => new Locale(x)).ToArray();

        public static string[] getISOCountries() =>
            CultureInfo.GetCultures(CultureTypes.AllCultures).Select(x =>
                x.Name.Contains("-")
                    ? x.Name.Substring(x.Name.IndexOf('-') + 1)
                    : "").Distinct().ToArray();

        public static string[] getISOLanguaes() => CultureInfo.GetCultures(CultureTypes.AllCultures).Select(x => x.TwoLetterISOLanguageName).Distinct().ToArray();
        public string getLanguage() => Culture.TwoLetterISOLanguageName;

        public string getCountry() =>
            Culture.Name.Contains('-')
                ? Culture.Name.Split('-','_')[1]
                : "";

        public string getVariant()
        {
            var z = Culture.Name.Split('-', '_');
            return z.Length >= 3 ? z[2] : "";
        }

        public override string ToString()
        {
            var variant = getVariant();
            if (variant != "")
                return $"{getLanguage()}_{getCountry()}_{variant}";

            var country = getCountry();
            if (country != "")
                return $"{getLanguage()}_{country}";

            return getLanguage();
        }

        public string getISO3Language() => Culture.ThreeLetterISOLanguageName;

        public string getISO3Country() => "";

        public string getDisplayLanguage() => getDisplayLanguage(ROOT);

        public string getDisplayLanguage(Locale inLocale) => Culture.DisplayName.Split(' ')[0];
        public string getDisplayCountry() => getDisplayCountry(ROOT);

        public string getDisplayCountry(Locale inLocale)
        {
            var split = Culture.DisplayName.Split(' ');
            return split.Length >= 2 ? split[1] : "";
        }

        public string getDisplayVariant() => "";
        public string getDisplayVariant(Locale inLocale) => "";

        public string getDisplayName() => getDisplayName(ROOT);
        public string getDisplayName(Locale inLocale) => Culture.DisplayName;

        public object clone() => new Locale((CultureInfo) Culture.Clone());

        public override int GetHashCode()
        {
            return Culture.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Locale loc && Culture.Equals(loc.Culture);
        }
    }
}
