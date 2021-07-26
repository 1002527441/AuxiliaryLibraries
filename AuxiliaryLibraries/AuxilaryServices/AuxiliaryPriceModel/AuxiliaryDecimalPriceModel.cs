﻿using System;
using System.Collections.Generic;
using System.Linq;
using AuxiliaryLibraries.Resources;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// Convert some currenciesto to some others inculding Iranian currency
    /// Worjing with double
    /// </summary>
    public class AuxiliaryDecimalPriceModel : AuxiliaryBasicPriceModel<decimal>
    {
        /// <summary>
        /// The Price
        /// </summary>
        public override decimal Price
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
                    this.PriceCommaDeLimited = this.Price.ToCommaDelimited(",");
                    this.PriceCommaDeLimitedDescription = this.Price > 0 ?
                                                                $"{this.PriceCommaDeLimited} {this.CurrencyDescription}" :
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
        public AuxiliaryDecimalPriceModel(decimal price, Currency priceBaseCurrency = Currency.IRR, Currency priceTargetCurrency = Currency.Toman, bool metricSystem = false, bool setZeroAsFree = true)
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
        protected override AuxiliaryBasicPriceModel<decimal> Calculate(decimal price, bool metricSystem = false)
        {
            //Price must pass as Toman
            string priceDescriptyion = string.Empty, priceCurrency = this.CurrencyDescription;
            decimal priceShortFormat = 0;
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
                    if (price % 1 == 0)
                    {
                        CalculateIntegers(price, power, ref priceShortFormat, ref priceCurrency, ref priceDescriptyion, metricSystem);
                        return this;
                    }
                    else
                    {
                        var count = BitConverter.GetBytes(decimal.GetBits((decimal)price)[3])[2] * -1;
                        var _power = powers.Any(x => x == count) ? count : power;
                        CalculateDecimals(price, power, _power, ref priceShortFormat, ref priceCurrency, ref priceDescriptyion, metricSystem);
                        return this;
                    }
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
        protected override void CalculateIntegers(decimal price, int power, ref decimal priceShortFormat, ref string priceCurrency, ref string priceDescriptyion, bool metricSystem)
        {
            double powerd = Math.Pow(10, power);
            var remained = price % (decimal)powerd;
            priceShortFormat = price / (decimal)powerd;
            priceCurrency = GetPriceCurrency(power, metricSystem);
            if (remained > 0)
            {
                var result = Calculate(remained);
                priceDescriptyion = $"{((long)priceShortFormat).ToPersianLetters()} {priceCurrency} و {result.PriceDescription}";
            }
            else
                priceDescriptyion = $"{((long)priceShortFormat).ToPersianLetters()} {priceCurrency} {this.CurrencyDescription}";
            priceShortFormat = Convert.ToDecimal(Round(price, powerd, power));
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
        protected override void CalculateDecimals(decimal price, int power, int decimalPower, ref decimal priceShortFormat, ref string priceCurrency, ref string priceDescriptyion, bool metricSystem)
        {
            var numList = Convert.ToDecimal(price).ToString().Split('.');

            #region Decimal Side of Price
            decimal decimalNumber = numList.Count() > 1 ? Convert.ToDecimal(numList.Last()) : 0;  // The Right Side => after .
            var priceDecimalCurrency = decimalNumber > 0 ? GetPriceCurrency(decimalPower, metricSystem) : string.Empty;
            var decimalResult = decimalNumber > 0 ? Calculate(decimalNumber) : null;
            var tempThis = decimalResult != null ? (AuxiliaryDecimalPriceModel)this.Clone() : null;
            #endregion

            #region Integer Side of Price
            decimal integerNumber = numList.Count() > 0 ? Convert.ToDecimal(numList.First()) : 0; // The Left Side => before .
            var priceIntegerCurrency = integerNumber > 0 ? GetPriceCurrency(power, metricSystem) : string.Empty;
            var integerResult = integerNumber > 0 ? Calculate(integerNumber) : null;
            #endregion

            priceCurrency = GetPriceCurrency(integerNumber > 0 ? power : decimalPower, metricSystem);
            if (integerResult != null)
            {
                priceDescriptyion = $"{integerResult.PriceDescription}";
            }
            if (tempThis != null)
            {
                if (integerResult != null)
                    priceDescriptyion += $"، و ";
                priceDescriptyion = $"{priceDescriptyion.Replace($" {CurrencyDescription}", string.Empty)}{tempThis.PriceDescription.Replace(CurrencyDescription, string.Empty).Trim()} {priceDecimalCurrency} {this.CurrencyDescription}";
            }

            //this.Price = price, //Don't Set It
            this.PriceCurrency = $"{priceCurrency} {this.CurrencyDescription}".Replace("  ", " ").Trim();
            this.PriceShortFormat = integerResult != null ? integerResult.PriceShortFormat : tempThis.PriceShortFormat;
            this.PriceDescription = priceDescriptyion.Replace("  ", " ").Trim();
            this._PriceShortFormat = (long)integerNumber;
        }

        /// <summary>
        /// Round
        /// </summary>
        /// <param name="price"></param>
        /// <param name="powerd"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        protected override decimal Round(decimal price, double powerd, int power)
        {
            var number = Math.Ceiling(Math.Round(price, 0, MidpointRounding.AwayFromZero));
            var remained = (decimal)number % (decimal)powerd;
            var rounded = (decimal)number / (decimal)powerd;
            if (Convert.ToDouble(remained.ToString().ToCharArray(0, 1).FirstOrDefault().ToString()) > 5)
                rounded++;
            return rounded;
        }
        #endregion
    }
}