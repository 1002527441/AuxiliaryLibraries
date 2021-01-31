using System;
using System.Collections.Generic;
using System.Linq;
using AuxiliaryLibraries.Resources;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// Convert price either Toman or Rial to other formats of Iranian currency
    /// </summary>
    public class AuxiliaryPriceModel
    {
        /// <summary>
        /// Persian Currency
        /// </summary>
        public enum Currency
        {
            /// <summary>
            /// Rial
            /// </summary>
            IRR = 1,
            /// <summary>
            /// Toman
            /// </summary>
            Toman = 2,
            /// <summary>
            /// US Dollar
            /// </summary>
            USD = 3,
            /// <summary>
            /// Euro
            /// </summary>
            EUR = 4,
            /// <summary>
            /// British Pund
            /// </summary>
            GBP = 5,
            /// <summary>
            /// Bitcoin
            /// </summary>
            BTC = 6,

        }

        private List<int> powers = new List<int>() { 21, 18, 15, 12, 9, 6, 3, 0, -1, -2, -3, -6, -9, -12 };
        private double _realPrice = -1;
        private double _price = 0;
        private Currency _sourceCurrency = Currency.IRR;
        private Currency _destinationCurrency = Currency.Toman;

        /// <summary>
        /// The Price
        /// </summary>
        public double Price
        {
            get => _price;
            set
            {
                _realPrice = value;

                if (_realPrice > 0)
                {
                    if (_sourceCurrency != _destinationCurrency)
                    {
                        if (_sourceCurrency == Currency.IRR && _destinationCurrency == Currency.Toman)
                            _price = _realPrice / 10;
                        else if (_sourceCurrency == Currency.Toman && _destinationCurrency == Currency.IRR)
                            _price = _realPrice * 10;
                        else
                            _price = _realPrice;
                    }
                    else
                        _price = _realPrice;
                }
                else
                    _price = 0;

                this.CurrencyDescription = GetCurrencyDescription(_destinationCurrency);
                if (_realPrice >= 0)
                {
                    var result = Calculate(_price);
                    this.PriceShortFormat = result.PriceShortFormat;
                    this.PriceCurrency = result.PriceCurrency;
                    this.PriceDescription = result.PriceDescription;
                    this.PriceCommaDeLimited = this.Price.ToCommaDelimited(",");
                    this.PriceCommaDeLimitedDescription = this.Price > 0 ? $"{this.PriceCommaDeLimited} {this.CurrencyDescription}" : DisplayNames.Free;
                }
            }
        }
        /// <summary>
        /// Price Short Format
        /// </summary>
        public long PriceShortFormat { get; set; }
        /// <summary>
        /// Price Currency
        /// </summary>
        public string PriceCurrency { get; set; }
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
        public bool MetricSystem { get; set; }

        /// <summary>
        /// Pass price as a value to constructor
        /// </summary>
        /// <param name="price"></param>
        /// <param name="priceBaseCurrency">If the price value which you passed is Rial set it "Rial", otherwise pass it as "Toman"</param>
        /// <param name="priceTargetCurrency">If you need to receive the price as Toman set it "Toman", otherwise pass it as "Rial"</param>
        public AuxiliaryPriceModel(double price, Currency priceBaseCurrency = Currency.IRR, Currency priceTargetCurrency = Currency.Toman, bool metricSystem = false)
        {
            try
            {
                this._sourceCurrency = priceBaseCurrency;
                this._destinationCurrency = priceTargetCurrency;
                this.Price = price;
                this.MetricSystem = metricSystem;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Change Toman To Rial or vice versa
        /// </summary>
        /// <param name="priceBaseCurrency"></param>
        /// <param name="priceTargetCurrency"></param>
        /// <returns></returns>
        public void SetPersianCurrency(Currency priceBaseCurrency = Currency.IRR, Currency priceTargetCurrency = Currency.Toman, bool metricSystem = false)
        {
            this._sourceCurrency = priceBaseCurrency;
            this._destinationCurrency = priceTargetCurrency;
            Price = _realPrice;
            this.MetricSystem = metricSystem;
        }

        #region Private Functions
        /// <summary>
        /// The main Function
        /// Convert price either Toman or Rial to other formats of Iranian currency
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private AuxiliaryPriceModel Calculate(double price, bool metricSystem = false)
        {
            //Price must pass as Toman
            string priceDescriptyion = string.Empty, priceCurrency = this.CurrencyDescription;
            double priceShortFormat = 0;
            if (price <= 0)
            {
                //this.Price = 0, //Don't Set it
                this.PriceCurrency = priceCurrency;
                this.PriceShortFormat = 0;
                this.PriceDescription = DisplayNames.Free;
                return this;
            }
            priceShortFormat = price;

            foreach (var power in powers)
            {
                if (price >= Math.Pow(10, power))
                {
                    double powerd = Math.Pow(10, power);
                    var remained = price % powerd;
                    priceShortFormat = price / powerd;
                    priceCurrency = GetPriceCurrency(power, metricSystem);
                    if (remained > 0)
                    {
                        var result = Calculate((double)remained);
                        priceDescriptyion = $"{ToLetters((int)priceShortFormat)} {priceCurrency} و {result.PriceDescription}";
                    }
                    else
                        priceDescriptyion = $"{ToLetters((int)priceShortFormat)} {priceCurrency} {this.CurrencyDescription}";
                    priceShortFormat = Convert.ToDouble(Round(price, powerd, power));
                    //this.Price = price, //Don't Set It
                    this.PriceCurrency = $"{priceCurrency} {this.CurrencyDescription}";
                    this.PriceShortFormat = (long)priceShortFormat;
                    this.PriceDescription = priceDescriptyion;
                    return this;
                }
            }

            //this.Price = price, //Don't Set It
            this.PriceCurrency = priceCurrency;
            this.PriceShortFormat = (long)price;
            this.PriceDescription = $"{ToLetters((int)price)} {priceCurrency}";
            return this;
        }

        private double Round(double price, double powerd, int power)
        {
            var number = Math.Ceiling(Math.Round(price, 0, MidpointRounding.AwayFromZero));
            var remained = (double)number % (double)powerd;
            var rounded = (double)number / (double)powerd;
            if (Convert.ToDouble(remained.ToString().ToCharArray(0, 1).FirstOrDefault().ToString()) > 5)
                rounded++;
            return rounded;
        }

        private string GetPriceCurrency(int power, bool metricSystem)
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

        private string ToLetters(int price)
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

        private enum NumberType
        {
            hundreds = 1,
            tens = 2,
            units = 3
        }

        private string GetLetter(int price, NumberType numberType)
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

        private string GetCurrencyDescription(Currency currency)
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
