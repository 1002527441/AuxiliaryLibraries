using System;
using System.Collections.Generic;
using System.Linq;
using AuxiliaryLibraries.Core.Enums;
using AuxiliaryLibraries.Core.Resources;

namespace AuxiliaryLibraries.Core.AuxilaryServices.AuxiliaryPriceModel
{
    /// <summary>
    /// Convert some currenciesto to some others inculding Iranian currency
    /// Worjing with double
    /// </summary>
    public class AuxiliaryFloatPriceModel : AuxiliaryBasicPriceModel<float>
    {
        /// <summary>
        /// The Price
        /// </summary>
        public override float Price
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
                _price = (float)decimalResult.Price;
                CurrencyDescription = decimalResult.CurrencyDescription;
                PriceShortFormat = decimalResult.PriceShortFormat;
                PriceCurrency = decimalResult.PriceCurrency;
                PriceDescription = decimalResult.PriceDescription;
                PriceCommaDeLimited = decimalResult.PriceCommaDeLimited;
                PriceCommaDeLimitedDescription = decimalResult.PriceCommaDeLimitedDescription;
            }
        }

        /// <summary>
        /// Pass price as a value to constructor
        /// </summary>
        /// <param name="price"></param>
        /// <param name="priceBaseCurrency">If the price value which you passed is Rial set it "Rial", otherwise pass it as "Toman"</param>
        /// <param name="priceTargetCurrency">If you need to receive the price as Toman set it "Toman", otherwise pass it as "Rial"</param>
        /// <param name="metricSystem">If you need to receive the price as Mili, Micro, Nano, and ... set it as true</param>
        public AuxiliaryFloatPriceModel(float price, Currency priceBaseCurrency = Currency.IRR, Currency priceTargetCurrency = Currency.Toman, bool metricSystem = false, bool setZeroAsFree = true)
        {
            _sourceCurrency = priceBaseCurrency;
            _destinationCurrency = priceTargetCurrency;
            MetricSystem = metricSystem;
            SetZeroAsFree = setZeroAsFree;
            //This one would be the Last one
            Price = price;
        }
    }
}