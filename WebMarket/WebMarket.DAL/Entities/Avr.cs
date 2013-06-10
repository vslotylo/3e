using System;
using WebMarket.DAL.Infrustructure;

namespace WebMarket.DAL.Entities
{ 
    public class Avr : Regulator
    {
        public Avr()
        {
            types.Add("Релейні");
            types.Add("Сервомоторні");
            types.Add("Тиристорні");
            types.Add("Симісторні");
        }

        public double MinWorkingInput { get; set; }
        public double MaxWorkingInput { get; set; }

        public string Precision { get; set; }
        public double MinCutOff { get; set; }
        public double MaxCutOff { get; set; }
        public int Stages { get; set; }

        public int ResponseTime { get; set; }

        public bool? HasDisplay { get; set; }
        public bool? HasModeDisplay { get; set; }
        public bool? HasInputOutputDisplay { get; set; }
        public bool? HasHighInputVoltageProtection { get; set; }
        public bool? HasHighOutputVoltageProtection { get; set; }
        public bool? HasOverloadProtection { get; set; }
        public bool? HasOverheatProtection { get; set; }
        public bool? HasShortCircuitProtection { get; set; }
        public bool? HasMemoryCard { get; set; }

        public int StartDelay { get; set; }
        public int PowerEfficiency { get; set; }
        public string Efficiency { get; set; }
        public string SelfConsumption { get; set; }
        public string ProtectionLevel { get; set; }
        public string CabinetType { get; set; }

        protected override void InitializeProductInfos()
        {
            base.InitializeProductInfos();
            if (!string.IsNullOrEmpty(Precision))
            {
                infos.Add(new ProductInfo { Name = "Точність", Value = string.Format("{0}%", Precision), IsPreview = true });
            }
            if (Stages > 0)
            {
                infos.Add(new ProductInfo { Name = "Кількість ступенів", Value = Stages.ToString(), IsPreview = true });
            }
            if (MinWorkingInput > 0 && MaxWorkingInput > 0)
            {
                infos.Add(new ProductInfo { Name = "Діапазон стабілізації", Value = string.Format("{0}-{1}В", MinWorkingInput, MaxWorkingInput), IsPreview = true });
            }
            if (MinCutOff > 0)
            {
                infos.Add(new ProductInfo { Name = "Мінімальна напруга відсікання", Value = string.Format("{0} {1}", MinCutOff, "В") });
            }
            if (MaxCutOff > 0)
            {
                infos.Add(new ProductInfo { Name = "Максимальна напруга відсікання", Value = string.Format("{0} {1}", MaxCutOff, "В") });
            }
            if (ResponseTime > 0)
            {
                infos.Add(new ProductInfo { Name = "Час реакції на зміну вхідної напруги", Value = string.Format("{0} {1}", ResponseTime, "мс") });
            }
            if (StartDelay > 0)
            {
                infos.Add(new ProductInfo { Name = "Затримка включення", Value = string.Format("{0} {1}", StartDelay, "мс") });
            }
            if (PowerEfficiency > 0)
            {
                infos.Add(new ProductInfo { Name = "Коефіцієнт потужності", Value = string.Format("{0} {1}", PowerEfficiency, "%") });
            }
            if (!string.IsNullOrWhiteSpace(Efficiency))
            {
                infos.Add(new ProductInfo { Name = "Коефіцієнт корисної дії", Value = string.Format("{0} {1}", Efficiency, "%") });
            }
            if (!string.IsNullOrWhiteSpace(SelfConsumption))
            {
                infos.Add(new ProductInfo { Name = "Власне споживання", Value = string.Format("{0} {1}", SelfConsumption, "Вт") });
            }
            if (!string.IsNullOrWhiteSpace(ProtectionLevel))
            {
                infos.Add(new ProductInfo { Name = "Рівень захисту", Value = ProtectionLevel.ToString() });
            }
            if (!string.IsNullOrWhiteSpace(CabinetType))
            {
                infos.Add(new ProductInfo { Name = "Тип корпусу", Value = CabinetType.ToString() });
            }
            if (HasDisplay.HasValue && HasDisplay.Value)
            {
                infos.Add(new ProductInfo { Name = "Дисплей", IsBool = true });
            }
            if (HasModeDisplay.HasValue && HasModeDisplay.Value)
            {
                infos.Add(new ProductInfo { Name = "Індикація режимів", IsBool = true });
            }
            if (HasInputOutputDisplay.HasValue && HasInputOutputDisplay.Value)
            {
                infos.Add(new ProductInfo { Name = "Індикація вхідної/вихідної напруги", IsBool = true });
            }
            if (HasHighInputVoltageProtection.HasValue && HasHighInputVoltageProtection.Value)
            {
                infos.Add(new ProductInfo { Name = "Захист від повищеної напруги", IsBool = true });
            }
            if (HasHighOutputVoltageProtection.HasValue && HasHighOutputVoltageProtection.Value)
            {
                infos.Add(new ProductInfo { Name = "Захист від повищеної напруги на виході", IsBool = true });
            }
            if (HasOverloadProtection.HasValue && HasOverloadProtection.Value)
            {
                infos.Add(new ProductInfo { Name = "Захист від перегрузу", IsBool = true });
            }
            if (HasOverheatProtection.HasValue && HasOverheatProtection.Value)
            {
                infos.Add(new ProductInfo { Name = "Захист від перегріву", IsBool = true });
            }
            if (HasShortCircuitProtection.HasValue && HasShortCircuitProtection.Value)
            {
                infos.Add(new ProductInfo { Name = "Захист від короткого замикання", IsBool = true });
            }
            if (HasMemoryCard.HasValue && HasMemoryCard.Value)
            {
                infos.Add(new ProductInfo { Name = "Вбудована картка запису подій", IsBool = true });
            }
        }
    }
}
