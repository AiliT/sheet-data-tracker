using System.ComponentModel;

namespace SheetDataTracker
{
    public class ConstantDataRow : IDataRow
    {
        private string _name = "";
        private string _value = "";
        private string _optionalValue = "";

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Value {
            get => _value;
            set
            {
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public string OptionalValue {
            get => _optionalValue;
            set
            {
                _optionalValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OptionalValue)));
            }
        }
    }
}
