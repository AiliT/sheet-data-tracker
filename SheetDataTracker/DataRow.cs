using System.ComponentModel;
using System.Windows.Input;

namespace SheetDataTracker
{
    public class DataRow : IDataRow
    {
        private int _value;
        private int _maxValue;
        private int _roundChangeAmount;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name { get; set; } = "";
        public int Value
        {
            get => _value;
            set
            {
                if (value > MaxValue)
                {
                    _value = MaxValue;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                }
                else if (value >= 0)
                {
                    _value = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                }
            }
        }
        public int MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxValue)));
            }
        }

        public int RoundChangeAmount
        {
            get => _roundChangeAmount;
            set
            {
                _roundChangeAmount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoundChangeAmount)));
            }
        }

        public int ManualChangeAmount
        {
            get => 0;
            set
            {
                int newValue = _value + value;

                if (newValue > MaxValue)
                {
                    _value = MaxValue;
                }
                else if (newValue < 0)
                {
                    _value = 0;
                }
                else
                {
                    _value = newValue;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                Keyboard.ClearFocus();
            }
        }

        public void Update()
        {
            Value += RoundChangeAmount;
        }

        public void Reset()
        {
            Value = MaxValue;
        }
    }
}
