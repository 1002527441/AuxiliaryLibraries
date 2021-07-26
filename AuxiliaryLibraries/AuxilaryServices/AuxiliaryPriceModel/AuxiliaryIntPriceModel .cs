using System;
using System.Collections.Generic;
using System.Linq;
using AuxiliaryLibraries.Resources;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// Convert some currenciesto to some others inculding Iranian currency
    /// Worjing with double
    /// </summary>
    public class AuxiliaryIntPriceModel : AuxiliaryBasicPriceModel<int>
    {
        /// <summary>
        /// The Price
        /// </summary>
        public override int Price
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
                    var result = Calculate(_price, MetricSystem);
                    this.PriceShortFormat = result.PriceShortFormat;
                    this.PriceCurrency = result.PriceCurrency;
                    this.PriceDescription = result.PriceDescription;
                    this.PriceCommaDeLimited = this.Price % 1 == 0 ? this.Price.ToCommaDelimited(",") : this._PriceShortFormat.ToCommaDelimited(",");
                    this.PriceCommaDeLimitedDescription = this.Price > 0 ?
                                                                this.Price % 1 == 0 ?
                                                                $"{this.PriceCommaDeLimited} {this.CurrencyDescription}" :
                                                                $"{this.PriceCommaDeLimited} {this.PriceCurrency}" :
                                                                DetermineZeroOrFree();
                }
            }
        }

        /// <summary>
        /// Pass price as a value to constructor
        /// </summary>
        /// <param name="price"></param>
        /// <param name="priceBaseCurrency">If the price value which you passed is Rial set it "Rial", otherwise pass it as "Toman"</param>
        /// <param name="priceTargetCurrency">If you need to receive the price as Toman set it "Toman", otherwise pass it as "Rial"</param>
        /// <param name="metricSystem">If you need to receive the price as Mili, Micro, Nano, and ... set it as true</param>
        public AuxiliaryIntPriceModel(int price, Currency priceBaseCurrency = Currency.IRR, Currency priceTargetCurrency = Currency.Toman, bool metricSystem = false, bool setZeroAsFree = true)
        {
            try
            {
                this._sourceCurrency = priceBaseCurrency;
                this._destinationCurrency = priceTargetCurrency;
                this.MetricSystem = metricSystem;
                this.SetZeroAsFree = setZeroAsFree;
                //This one would be the Last one
                this.Price = price;
            }
            catch (Exception ex)
            {
                ex.ToString();
                Price = 0;
            }
        }

        #region Protected Functions
        /// <summary>
        /// The main Function
        /// Convert price either Toman or Rial to other formats of Iranian currency
        /// </summary>
        /// <param name="price"></param>
        /// <param name="metricSystem">If you need to receive the price as Mili, Micro, Nano, and ... set it as true</param>
        /// <returns></returns>
        protected override AuxiliaryBasicPriceModel<int> Calculate(int price, bool metricSystem = false)
        {
            //Price must pass as Toman
            string priceDescriptyion = string.Empty, priceCurrency = this.CurrencyDescription;
            int priceShortFormat = 0;
            if (price <= 0)
            {
                //this.Price = 0, //Don't Set it
                this.PriceCurrency = priceCurrency;
                this.PriceShortFormat = 0;
                this.PriceDescription = DetermineZeroOrFree();
                return this;
            }
            priceShortFormat = price;

            foreach (var power in powers)
            {
                if (price >= Convert.ToDecimal(Math.Pow(10, power)))
                {
                    CalculateIntegers(price, power, ref priceShortFormat, ref priceCurrency, ref priceDescriptyion, metricSystem);
                    return this;
                }
            }

            //this.Price = price, //Don't Set It
            this.PriceCurrency = priceCurrency;
            this.PriceShortFormat = (long)price;
            this.PriceDescription = $"{((long)price).ToPersianLetters()} {priceCurrency}";
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
        protected override void CalculateIntegers(int price, int power, ref int priceShortFormat, ref string priceCurrency, ref string priceDescriptyion, bool metricSystem)
        {
            double powerd = Math.Pow(10, power);
            var remained = price % (int)powerd;
            priceShortFormat = price / (int)powerd;
            priceCurrency = GetPriceCurrency(power, metricSystem);
            if (remained > 0)
            {
                var result = Calculate(remained);
                priceDescriptyion = $"{((long)priceShortFormat)} {priceCurrency} و {result.PriceDescription}";
            }
            else
                priceDescriptyion = $"{((long)priceShortFormat).ToPersianLetters()} {priceCurrency} {this.CurrencyDescription}";
            priceShortFormat = Convert.ToInt32(Round(price, powerd, power));
            //this.Price = price, //Don't Set It
            this.PriceCurrency = $"{priceCurrency} {this.CurrencyDescription}";
            this.PriceShortFormat = (long)priceShortFormat;
            this.PriceDescription = priceDescriptyion;
        }

        /// <summary>
        /// Calculate Decimals
        /// </summary>
        /// <param name="price"></param>
        /// <param name="power"></param>
        /// <param name="priceShortFormat"></param>
        /// <param name="priceCurrency"></param>
        /// <param name="priceDescriptyion"></param>
        /// <param name="metricSystem"></param>
        protected override void CalculateDecimals(int price, int power, int decimalPower, ref int priceShortFormat, ref string priceCurrency, ref string priceDescriptyion, bool metricSystem)
        {
            CalculateIntegers(price, power, ref priceShortFormat, ref priceCurrency, ref priceDescriptyion, metricSystem);
        }

        /// <summary>
        /// Round
        /// </summary>
        /// <param name="price"></param>
        /// <param name="powerd"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        protected override int Round(int price, double powerd, int power)
        {
            var number = Math.Ceiling(Math.Round((decimal)price, 0, MidpointRounding.AwayFromZero));
            var remained = (decimal)number % (decimal)powerd;
            var rounded = (decimal)number / (decimal)powerd;
            if (Convert.ToDouble(remained.ToString().ToCharArray(0, 1).FirstOrDefault().ToString()) > 5)
                rounded++;
            return (int)rounded;
        }
        #endregion
    }
}