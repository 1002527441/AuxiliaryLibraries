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
    public class AuxiliaryLongPriceModel : AuxiliaryBasicPriceModel<long>
    {
        /// <summary>
        /// The Price
        /// </summary>
        public override long Price
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
                                                                DisplayNames.Free;
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
        public AuxiliaryLongPriceModel(long price, Currency priceBaseCurrency = Currency.IRR, Currency priceTargetCurrency = Currency.Toman, bool metricSystem = false)
        {
            try
            {
                this._sourceCurrency = priceBaseCurrency;
                this._destinationCurrency = priceTargetCurrency;
                this.MetricSystem = metricSystem;
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
        protected override AuxiliaryBasicPriceModel<long> Calculate(long price, bool metricSystem = false)
        {
            //Price must pass as Toman
            string priceDescriptyion = string.Empty, priceCurrency = this.CurrencyDescription;
            long priceShortFormat = 0;
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
                if (price >= Convert.ToDecimal(Math.Pow(10, power)))
                {
                    if (price % 1 == 0)
                    {
                        CalculateIntegers(price, power, ref priceShortFormat, ref priceCurrency, ref priceDescriptyion, metricSystem);
                        return this;
                    }
                    else
                    {
                        var count = BitConverter.GetBytes(decimal.GetBits((decimal)price)[3])[2] * -1;
                        var _power = powers.Any(x => x == count) ? count : power;
                        CalculateDecimals(price, _power, ref priceShortFormat, ref priceCurrency, ref priceDescriptyion, metricSystem);
                        return this;
                    }
                }
            }

            //this.Price = price, //Don't Set It
            this.PriceCurrency = priceCurrency;
            this.PriceShortFormat = (long)price;
            this.PriceDescription = $"{ToLetters((int)price)} {priceCurrency}";
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
        protected override void CalculateIntegers(long price, int power, ref long priceShortFormat, ref string priceCurrency, ref string priceDescriptyion, bool metricSystem)
        {
            double powerd = Math.Pow(10, power);
            var remained = price % (long)powerd;
            priceShortFormat = price / (long)powerd;
            priceCurrency = GetPriceCurrency(power, metricSystem);
            if (remained > 0)
            {
                var result = Calculate(remained);
                priceDescriptyion = $"{ToLetters((int)priceShortFormat)} {priceCurrency} و {result.PriceDescription}";
            }
            else
                priceDescriptyion = $"{ToLetters((int)priceShortFormat)} {priceCurrency} {this.CurrencyDescription}";
            priceShortFormat = Convert.ToInt64(Round(price, powerd, power));
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
        protected override void CalculateDecimals(long price, int power, ref long priceShortFormat, ref string priceCurrency, ref string priceDescriptyion, bool metricSystem)
        {
            double remained = Convert.ToDouble(price.ToString().Replace("0", string.Empty).Replace(".", string.Empty));
            priceCurrency = GetPriceCurrency(power, metricSystem);
            var result = Calculate((long)remained);

            priceDescriptyion = $"{result.PriceDescription.Replace(CurrencyDescription, string.Empty).Trim()} {priceCurrency} {this.CurrencyDescription}";

            //this.Price = price, //Don't Set It
            this.PriceCurrency = $"{priceCurrency} {this.CurrencyDescription}".Replace("  ", " ").Trim();
            this.PriceShortFormat = result.PriceShortFormat;
            this.PriceDescription = priceDescriptyion;
            this._PriceShortFormat = (long)remained;
        }

        /// <summary>
        /// Round
        /// </summary>
        /// <param name="price"></param>
        /// <param name="powerd"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        protected override long Round(long price, double powerd, int power)
        {
            var number = Math.Ceiling(Math.Round((decimal)price, 0, MidpointRounding.AwayFromZero));
            var remained = (decimal)number % (decimal)powerd;
            var rounded = (decimal)number / (decimal)powerd;
            if (Convert.ToDouble(remained.ToString().ToCharArray(0, 1).FirstOrDefault().ToString()) > 5)
                rounded++;
            return (long)rounded;
        }
        #endregion
    }
}