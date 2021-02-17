using System;
using System.Collections.Generic;
using System.Linq;
using AuxiliaryLibraries.Resources;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// Convert some currenciesto to some others inculding Iranian currency
    /// https://fa.wikipedia.org/wiki/%D9%BE%DB%8C%D8%B4%D9%88%D9%86%D8%AF%D9%87%D8%A7%DB%8C_%D8%A7%D8%B3%E2%80%8C%D8%A2%DB%8C
    /// </summary>
    public abstract partial class AuxiliaryBasicPriceModel<T>
    {
        /// <summary>
        /// powers
        /// </summary>
        protected List<int> powers = new List<int>() { 21, 18, 15, 12, 9, 6, 3, 0, -1, -2, -3, -6, -9, -12 };
        /// <summary>
        /// Source Currency
        /// </summary>
        protected Currency _sourceCurrency = Currency.IRR;
        /// <summary>
        /// Destination Currency
        /// </summary>
        protected Currency _destinationCurrency = Currency.Toman;
        /// <summary>
        /// Real Price
        /// </summary>
        protected T _realPrice = default;
        /// <summary>
        /// Price
        /// </summary>
        protected T _price = default;
        /// <summary>
        /// Price Short Format
        /// </summary>
        protected long _PriceShortFormat { get; set; }

        /// <summary>
        /// The Price
        /// </summary>
        public virtual T Price { get; set; }
        /// <summary>
        /// Price Short Format
        /// </summary>
        public long PriceShortFormat { get; set; }
        /// <summary>
        /// Price Currency
        /// </summary>
        public string PriceCurrency { get; set; }
        /// <summary>
        /// Price Format
        /// </summary>
        public string PriceFormat => $"{PriceShortFormat} {PriceCurrency}";
        /// <summary>
        /// Currency
        /// </summary>
        public string CurrencyDescription { get; set; }
        /// <summary>
        /// Price Description
        /// </summary>
        public string PriceDescription { get; set; }
        /// <summary>
        /// Price Comma DeLimited
        /// </summary>
        public string PriceCommaDeLimited { get; set; }
        /// <summary>
        /// Price Comma DeLimited Description
        /// </summary>
        public string PriceCommaDeLimitedDescription { get; set; }
        /// <summary>
        /// Metric System
        /// </summary>
        public bool MetricSystem { get; set; }

        /// <summary>
        /// Change Toman To Rial or vice versa
        /// </summary>
        /// <param name="priceBaseCurrency"></param>
        /// <param name="priceTargetCurrency"></param>
        /// <returns></returns>
        /// <param name="metricSystem">If you need to receive the price as Mili, Micro, Nano, and ... set it as true</param>
        public void SetPersianCurrency(Currency priceBaseCurrency = Currency.IRR, Currency priceTargetCurrency = Currency.Toman, bool metricSystem = false)
        {
            this._sourceCurrency = priceBaseCurrency;
            this._destinationCurrency = priceTargetCurrency;
            this.MetricSystem = metricSystem;
            Price = _realPrice;
        }

        #region Protected Functions
        /// <summary>
        /// The main Function
        /// Convert price either Toman or Rial to other formats of Iranian currency
        /// </summary>
        /// <param name="price"></param>
        /// <param name="metricSystem">If you need to receive the price as Mili, Micro, Nano, and ... set it as true</param>
        /// <returns></returns>
        protected virtual AuxiliaryBasicPriceModel<T> Calculate(T price, bool metricSystem = false)
        {
            return this;
        }

        /// <summary>
        /// Calculate Integers
        /// </summary>
        /// <param name="price"></param>
        /// <param name="power"></param>
        /// <param name="priceShortFormat"></param>
        /// <param name="priceCurrency"></param>
        /// <param name="priceDescriptyion"></param>
        /// <param name="metricSystem"></param>
        protected virtual void CalculateIntegers(T price, int power, ref T priceShortFormat, ref string priceCurrency, ref string priceDescriptyion, bool metricSystem)
        {}

        /// <summary>
        /// Calculate Decimals
        /// </summary>
        /// <param name="price"></param>
        /// <param name="power"></param>
        /// <param name="priceShortFormat"></param>
        /// <param name="priceCurrency"></param>
        /// <param name="priceDescriptyion"></param>
        /// <param name="metricSystem"></param>
        protected virtual void CalculateDecimals(T price, int power, ref T priceShortFormat, ref string priceCurrency, ref string priceDescriptyion, bool metricSystem)
        {}

        /// <summary>
        /// Round
        /// </summary>
        /// <param name="price"></param>
        /// <param name="powerd"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        protected virtual T Round(T price, double powerd, int power)
        {
            return default;
        }

        /// <summary>
        /// Get Price Currency
        /// </summary>
        /// <param name="power"></param>
        /// <param name="metricSystem"></param>
        /// <returns></returns>
        protected string GetPriceCurrency(int power, bool metricSystem)
        {
            return power switch
            {
                21 => metricSystem ? DisplayNames.Number_Zetta : DisplayNames.Number_Trilliard,
                18 => metricSystem ? DisplayNames.Number_Exa : DisplayNames.Number_Trillion,
                15 => metricSystem ? DisplayNames.Number_Peta : DisplayNames.Number_Billiard,
                12 => metricSystem ? DisplayNames.Number_Tera : DisplayNames.Number_Billion,
                9 => metricSystem ? DisplayNames.Number_Giga : DisplayNames.Number_Milliard,
                6 => metricSystem ? DisplayNames.Number_Mega : DisplayNames.Number_Million,
                3 => metricSystem ? DisplayNames.Number_Kilo : DisplayNames.Number_Thousand,
                -1 => metricSystem ? DisplayNames.Number_Deci : DisplayNames.Number_Thenth,
                -2 => metricSystem ? DisplayNames.Number_Centi : DisplayNames.Number_Hunrdredth,
                -3 => metricSystem ? DisplayNames.Number_Mili : DisplayNames.Number_Thousandth,
                -6 => metricSystem ? DisplayNames.Number_Micro : DisplayNames.Number_Millionth,
                -9 => metricSystem ? DisplayNames.Number_Nano : DisplayNames.Number_Billionth,
                -12 => metricSystem ? DisplayNames.Number_Pico : DisplayNames.Number_Billiardth,
                _ => string.Empty
            };
        }

        /// <summary>
        /// To Letters
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        protected string ToLetters(int price)
        {
            var _price = string.Empty;
            int hundreds = price >= 100 ? price / 100 : 0, tens = 0, units = 0;

            #region tens
            if (hundreds > 0)
                tens = (price % 100) >= 10 ? (price % 100) / 10 : 0;
            else
                tens = price >= 10 ? price / 10 : 0;
            #endregion

            #region units
            if (hundreds > 0)
            {
                if (tens > 0)
                    units = ((price % 100) % 10);
                else
                    units = (price % 100);
            }
            else
            {
                if (tens > 0)
                    units = (price % 10);
                else
                    units = price;
            }
            #endregion

            var lst = new List<string>();
            lst.Add(GetLetter(hundreds, NumberType.hundreds));
            if (tens == 1 && units > 0)
                lst.Add(GetLetter(Convert.ToInt32($"{tens}{units}"), NumberType.tens));
            else
            {
                lst.Add(GetLetter(tens, NumberType.tens));
                lst.Add(GetLetter(units, NumberType.units));
            }

            return string.Join(" و ", lst.Where(s => !string.IsNullOrEmpty(s)).ToList());
        }

        /// <summary>
        /// Get Letter
        /// </summary>
        /// <param name="price"></param>
        /// <param name="numberType"></param>
        /// <returns></returns>
        protected string GetLetter(int price, NumberType numberType)
        {
            switch (price)
            {
                case 19:
                    return DisplayNames.Number_Nineteen;
                case 18:
                    return DisplayNames.Number_Eighteen;
                case 17:
                    return DisplayNames.Number_Seventeen;
                case 16:
                    return DisplayNames.Number_Sixteen;
                case 15:
                    return DisplayNames.Number_Fifteen;
                case 14:
                    return DisplayNames.Number_Fourteen;
                case 13:
                    return DisplayNames.Number_Thirteen;
                case 12:
                    return DisplayNames.Number_Twelve;
                case 11:
                    return DisplayNames.Number_Eleven;
                case 9:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_NineHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Ninety;
                            case NumberType.units:
                                return DisplayNames.Number_Nine;
                        }
                    }
                    break;
                case 8:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_EightHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Eighty;
                            case NumberType.units:
                                return DisplayNames.Number_Eight;
                        }
                    }
                    break;
                case 7:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_SevenHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Seventeen;
                            case NumberType.units:
                                return DisplayNames.Number_Seven;
                        }
                    }
                    break;
                case 6:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_SixHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Sixty;
                            case NumberType.units:
                                return DisplayNames.Number_Six;
                        }
                    }
                    break;
                case 5:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_FiveHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Fifty;
                            case NumberType.units:
                                return DisplayNames.Number_Five;
                        }
                    }
                    break;
                case 4:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_FourHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Fourty;
                            case NumberType.units:
                                return DisplayNames.Number_Four;
                        }
                    }
                    break;
                case 3:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_ThreeHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Thirty;
                            case NumberType.units:
                                return DisplayNames.Number_Three;
                        }
                    }
                    break;
                case 2:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_TwoHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Twenty;
                            case NumberType.units:
                                return DisplayNames.Number_Two;
                        }
                    }
                    break;
                case 1:
                    {
                        switch (numberType)
                        {
                            case NumberType.hundreds:
                                return DisplayNames.Number_OneHundred;
                            case NumberType.tens:
                                return DisplayNames.Number_Ten;
                            case NumberType.units:
                                return DisplayNames.Number_One;
                        }
                    }
                    break;
            }
            return string.Empty;
        }

        /// <summary>
        /// Get Currency Description
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        protected string GetCurrencyDescription(Currency currency)
        {
            return currency switch
            {
                Currency.IRR => DisplayNames.Rial,
                Currency.Toman => DisplayNames.Toman,
                Currency.BTC => DisplayNames.BTC,
                Currency.EUR => DisplayNames.EUR,
                Currency.GBP => DisplayNames.GBP,
                Currency.USD => DisplayNames.USD,
                _ => DisplayNames.Rial
            };
        }
        #endregion
    }
}
