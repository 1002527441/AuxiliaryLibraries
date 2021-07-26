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
    public class AuxiliaryDoublePriceModel : AuxiliaryBasicPriceModel<double>
    {
        /// <summary>
        /// The Price
        /// </summary>
        public override double Price
        {
            get => _price;
            set
            {
                #region Check If Convertable To Deciaml Or Not
                try
                { Convert.ToDecimal(value); }
                catch (Exception ex)
                {
                    ex.ToString(); value = 0;
                }
                #endregion
                var decimalResult = new AuxiliaryDecimalPriceModel(Convert.ToDecimal(value), _sourceCurrency, _destinationCurrency, MetricSystem, SetZeroAsFree);
                _realPrice = value;
                _price = Convert.ToDouble(decimalResult.Price);
                this.CurrencyDescription = decimalResult.CurrencyDescription;
                this.PriceShortFormat = decimalResult.PriceShortFormat;
                this.PriceCurrency = decimalResult.PriceCurrency;
                this.PriceDescription = decimalResult.PriceDescription;
                this.PriceCommaDeLimited = decimalResult.PriceCommaDeLimited;
                this.PriceCommaDeLimitedDescription = decimalResult.PriceCommaDeLimitedDescription;
            }
        }

        /// <summary>
        /// Pass price as a value to constructor
        /// </summary>
        /// <param name="price"></param>
        /// <param name="priceBaseCurrency">If the price value which you passed is Rial set it "Rial", otherwise pass it as "Toman"</param>
        /// <param name="priceTargetCurrency">If you need to receive the price as Toman set it "Toman", otherwise pass it as "Rial"</param>
        /// <param name="metricSystem">If you need to receive the price as Mili, Micro, Nano, and ... set it as true</param>
        public AuxiliaryDoublePriceModel(double price, Currency priceBaseCurrency = Currency.IRR, Currency priceTargetCurrency = Currency.Toman, bool metricSystem = false, bool setZeroAsFree = true)
        {
            this._sourceCurrency = priceBaseCurrency;
            this._destinationCurrency = priceTargetCurrency;
            this.MetricSystem = metricSystem;
            this.SetZeroAsFree = setZeroAsFree;
            //This one would be the Last one
            this.Price = price;
        }
    }
}