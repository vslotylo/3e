using WebMarket.DAL.Infrustructure;

namespace WebMarket.DAL.Entities
{
    public class MesurmentUnit : ElectricProduct
    {
        public string MinOperationValue { get; set; }
        public string MaxOperationValue { get; set; }
        public string MeasurementError { get; set; }
        public string IndicationError { get; set; }

        protected override void InitializeProductInfos()
        {
            base.InitializeProductInfos();
            if (!string.IsNullOrEmpty(MinOperationValue))
            {
                this.Infos.Add(new ProductInfo { Name = "Нижній діапозан відключення", Value = string.Format("{0} В", MinOperationValue), IsPreview = true });
            }
            if (!string.IsNullOrEmpty(MaxOperationValue))
            {
                this.Infos.Add(new ProductInfo { Name = "Верхній діапозан відключення", Value = string.Format("{0} В", MaxOperationValue), IsPreview = true });
            }
            if (!string.IsNullOrEmpty(MeasurementError))
            {
                this.Infos.Add(new ProductInfo { Name = "Погрішність вимірювання", Value = MeasurementError, IsPreview = false });
            }
            if (!string.IsNullOrEmpty(MeasurementError))
            {
                this.Infos.Add(new ProductInfo { Name = "Погрішність індикації", Value = MeasurementError, IsPreview = false });
            }
        }
    }
}
