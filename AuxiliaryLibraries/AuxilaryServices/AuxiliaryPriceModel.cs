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
        public enum PersianCurrency
        {
            /// <summary>
            /// Rial
            /// </summary>
            Rial = 1,
            /// <summary>
            /// Toman
            /// </summary>
            Toman = 2
        }

        private List<int> powers = new List<int>() { 21, 18, 15, 12, 9, 6, 3 };
        private long _realPrice = -1;
        private long _price = 0;
        private PersianCurrency _priceBaseCurrency = PersianCurrency.Rial;
        private PersianCurrency _priceTargetCurrency = PersianCurrency.Toman;

        /// <summary>
        /// The Price
        /// </summary>
        public long Price
        {
            get => _price;
            set
            {
                _realPrice = value;

                if (_realPrice > 0)
                {
                    if (_priceBaseCurrency != _priceTargetCurrency)
                    {
                        if (_priceBaseCurrency == PersianCurrency.Rial && _priceTargetCurrency == PersianCurrency.Toman)
                            _price = _realPrice / 10;
                        else if (_priceBaseCurrency == PersianCurrency.Toman && _priceTargetCurrency == PersianCurrency.Rial)
                            _price = _realPrice * 10;
                    }
                    else
                        _price = _realPrice;
                }
                else
                    _price = 0;
                
                this.Currency = _priceTargetCurrency == PersianCurrency.Rial ? DisplayNames.Rial : DisplayNames.Toman;
                if (_realPrice >= 0)
                {
                    var result = Calculate(_price);
                    this.PriceShortFormat = result.PriceShortFormat;
                    this.PriceCurrency = result.PriceCurrency;
                    this.PriceDescription = result.PriceDescription;
                    this.PriceCommaDeLimited = this.Price.ToCommaDelimited(",");
                    this.PriceCommaDeLimitedDescription = this.Price > 0 ? $"{this.PriceCommaDeLimited} {this.Currency}" : DisplayNames.Free;
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
        public string Currency { get; set; }
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
        /// Default constructor
        /// </summary>
        public AuxiliaryPriceModel()
        {

        }

        /// <summary>
        /// Pass price as a value to constructor
        /// </summary>
        /// <param name="price"></param>
        /// <param name="priceBaseCurrency">If the price value which you passed is Rial set it "Rial", otherwise pass it as "Toman"</param>
        /// <param name="priceTargetCurrency">If you need to receive the price as Toman set it "Toman", otherwise pass it as "Rial"</param>
        public AuxiliaryPriceModel(long price, PersianCurrency priceBaseCurrency = PersianCurrency.Rial, PersianCurrency priceTargetCurrency = PersianCurrency.Toman)
        {
            this._priceBaseCurrency = priceBaseCurrency;
            this._priceTargetCurrency = priceTargetCurrency;
            this.Price = price;
        }

        /// <summary>
        /// Change Toman To Rial or vice versa
        /// </summary>
        /// <param name="priceBaseCurrency"></param>
        /// <param name="priceTargetCurrency"></param>
        /// <returns></returns>
        public void SetPersianCurrency(PersianCurrency priceBaseCurrency = PersianCurrency.Rial, PersianCurrency priceTargetCurrency = PersianCurrency.Toman)
        {
            this._priceBaseCurrency = priceBaseCurrency;
            this._priceTargetCurrency = priceTargetCurrency;
            Price = _realPrice;
        }

        #region Private Functions
        /// <summary>
        /// The main Function
        /// Convert price either Toman or Rial to other formats of Iranian currency
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private AuxiliaryPriceModel Calculate(long price)
        {
            //Price must pass as Toman
            string priceDescriptyion = string.Empty, priceCurrency = this.Currency;
            long priceShortFormat = 0;
            if (price <= 0)
                return new AuxiliaryPriceModel()
                {
                    //Price = 0, //Don't Set it
                    PriceCurrency = priceCurrency,
                    PriceShortFormat = 0,
                    PriceDescription = DisplayNames.Free
                };
            priceShortFormat = price;

            foreach (var power in powers)
            {
                if (price >= Math.Pow(10, power))
                {
                    var powerd = Math.Pow(10, power);
                    var remained = price % powerd;
                    priceShortFormat = (long)price / (long)powerd;
                    priceCurrency = GetPriceCurrency(power);
                    if (remained > 0)
                    {
                        var result = Calculate((long)remained);
                        priceDescriptyion = $"{ToLetters((int)priceShortFormat)} {priceCurrency} و {result.PriceDescription}";
                    }
                    else
                        priceDescriptyion = $"{ToLetters((int)priceShortFormat)} {priceCurrency} {this.Currency}";
                    priceShortFormat = Convert.ToInt64(Round(price, powerd, power));
                    return new AuxiliaryPriceModel()
                    {
                        //Price = price, //Don't Set It
                        PriceCurrency = $"{priceCurrency} {this.Currency}",
                        PriceShortFormat = priceShortFormat,
                        PriceDescription = priceDescriptyion
                    };
                }
            }
            return new AuxiliaryPriceModel()
            {
                //Price = price, //Don't Set It
                PriceCurrency = priceCurrency,
                PriceShortFormat = price,
                PriceDescription = $"{ToLetters((int)price)} {priceCurrency}"
            };
        }

        private double Round(double price, double powerd, int power)
        {
            var number = Math.Ceiling(Math.Round(price, 0, MidpointRounding.AwayFromZero));
            var remained = (long)number % (long)powerd;
            var rounded = (long)number / (long)powerd;
            if (Convert.ToInt32(remained.ToString().ToCharArray(0, 1).FirstOrDefault().ToString()) > 5)
                rounded++;
            return rounded;
        }

        private string GetPriceCurrency(int power)
        {
            switch (power)
            {
                case 21:
                    return DisplayNames.Number_Trilliard;
                case 18:
                    return DisplayNames.Number_Trillion;
                case 15:
                    return DisplayNames.Number_Billiard;
                case 12:
                    return DisplayNames.Number_Billion;
                case 9:
                    return DisplayNames.Number_Milliard;
                case 6:
                    return DisplayNames.Number_Million;
                case 3:
                    return DisplayNames.Number_Thousand;
                default:
                    return string.Empty;
            }
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
        #endregion
    }
}
