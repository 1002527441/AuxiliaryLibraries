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
        /// Set Zero As Free
        /// </summary>
        public bool SetZeroAsFree { get; set; }

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
        protected virtual void CalculateDecimals(T price, int power, int decimalPower, ref T priceShortFormat, ref string priceCurrency, ref string priceDescriptyion, bool metricSystem)
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

        protected virtual string DetermineZeroOrFree()
        {
            return SetZeroAsFree ? DisplayNames.Free : $"{DisplayNames.Zero} {PriceCurrency}";
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
                Currency.USDT => DisplayNames.USDT,
                _ => DisplayNames.Rial
            };
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion
    }
}
