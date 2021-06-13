using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI;
using BaoYuanSerial.Models;
using System.Reactive;
using BaoYuanSerial.Util;
namespace BaoYuanSerial.ViewModels
{
    public class ToolBoxViewModel:ViewModelBase
    {

        public ToolBoxViewModel()
        {
            OnLRCCommand = ReactiveCommand.Create(OnLRC);
            OnXORCommand = ReactiveCommand.Create(OnXOR);
            OnCheckSumCommand = ReactiveCommand.Create(OnCheckSum);
            OnFCSCommand = ReactiveCommand.Create(OnFCS);
            OnCRC16LoHiCommand = ReactiveCommand.Create(OnCRC16LoHi);
            OnDecimalTo16HexCommand = ReactiveCommand.Create(OnDecimalTo16Hex);
            OnSingleHexToDecimalCommand = ReactiveCommand.Create(OnSingleHexToDecimal);
            OnDoubleHexToDecimalCommand = ReactiveCommand.Create(OnDoubleHexToDecimal);
            OnIntegerTo16HexCommand = ReactiveCommand.Create(OnIntegerTo16Hex);
            On16BitHexToIntegerCommand = ReactiveCommand.Create(On16BitHexToInteger);
            On32BitHexToIntegerCommand = ReactiveCommand.Create(On32BitHexToInteger);
        }

        #region command
        public ReactiveCommand<Unit, Unit> OnLRCCommand { get; }

        private void OnLRC()
        {
            StrLRC = CommonCheck.CheckLRC(SrcStrings);
            StrFullStr = SrcStrings + StrLRC;
        }

        public ReactiveCommand<Unit, Unit> OnXORCommand { get; }
        private void OnXOR()
        {
            StrXOR = CommonCheck.CheckXor(SrcStrings);
            StrFullStr = SrcStrings + StrXOR;
        }
        public ReactiveCommand<Unit, Unit> OnCheckSumCommand { get; }
        private void OnCheckSum()
        {
            StrCheckSum = CommonCheck.CheckSum(SrcStrings);
            StrFullStr = SrcStrings + StrCheckSum;
        }
        public ReactiveCommand<Unit, Unit> OnFCSCommand { get; }
        private void OnFCS()
        {
            StrFCS = CommonCheck.CheckFCS(SrcStrings);
            StrFullStr = SrcStrings + StrFCS;
        }
        public ReactiveCommand<Unit, Unit> OnCRC16LoHiCommand { get; }
        private void OnCRC16LoHi()
        {
            StrCRC16LoHi = CommonCheck.CheckCRC16Modbus(SrcStrings);
            StrFullStr = SrcStrings + StrCRC16LoHi;
        }
        public ReactiveCommand<Unit, Unit> OnDecimalTo16HexCommand { get; }
        private void OnDecimalTo16Hex()
        {
            float sing = Convert.ToSingle(StrDecimal);
            StrSingleHex =DataConvertUtility.ByteArrayToHexString( BitConverter.GetBytes(sing).Reverse().ToArray());
            double dob = Convert.ToDouble(StrDecimal);
            StrDoubleHex = DataConvertUtility.ByteArrayToHexString( BitConverter.GetBytes(dob).Reverse().ToArray());
        }
        public ReactiveCommand<Unit, Unit> OnSingleHexToDecimalCommand { get; }
        private void OnSingleHexToDecimal()
        {
            byte[] data = DataConvertUtility.HexStringToByte(StrSingleHex).Reverse().ToArray();
            StrDecimal = BitConverter.ToSingle(data).ToString();          
        }
        public ReactiveCommand<Unit, Unit> OnDoubleHexToDecimalCommand { get; }
        private void OnDoubleHexToDecimal()
        {
            byte[] data = DataConvertUtility.HexStringToByte(StrDoubleHex).Reverse().ToArray();
            StrDecimal =BitConverter.ToDouble( data).ToString();
        }
        public ReactiveCommand<Unit, Unit> OnIntegerTo16HexCommand { get; }
        private void OnIntegerTo16Hex()
        {
            Int16 datashor = Convert.ToInt16(StrInteger);
            Str16BitHex =DataConvertUtility.ByteArrayToHexString( BitConverter.GetBytes(datashor).Reverse().ToArray());
            Int32 dataint = Convert.ToInt32(StrInteger);
            Str32BitHex = DataConvertUtility.ByteArrayToHexString(BitConverter.GetBytes(dataint).Reverse().ToArray());
        }
        public ReactiveCommand<Unit, Unit> On16BitHexToIntegerCommand { get; }
        private void On16BitHexToInteger()
        {
            byte[] bt = DataConvertUtility.HexStringToByte(Str16BitHex).Reverse().ToArray();
            StrInteger = BitConverter.ToInt16(bt).ToString();
        }
        public ReactiveCommand<Unit, Unit> On32BitHexToIntegerCommand { get; }
        private void On32BitHexToInteger()
        {
            byte[] bt = DataConvertUtility.HexStringToByte(Str32BitHex).Reverse().ToArray();
            StrInteger = BitConverter.ToInt32(bt).ToString();
        }




        #endregion

        #region Check
        private string _SrcStrings = "";

        public string SrcStrings
        {
            get => _SrcStrings;
            set
            {
                _SrcStrings = value;
                this.RaisePropertyChanged();
            }
        }

        private string _StrLRC = "";

        public string StrLRC
        {
            get => _StrLRC;
            set
            {
                _StrLRC = value;
                this.RaisePropertyChanged();
            }
        }
        private string _StrXOR = "";

        public string StrXOR
        {
            get => _StrXOR;
            set
            {
                _StrXOR = value;
                this.RaisePropertyChanged();
            }
        }
        private string _StrCheckSum = "";

        public string StrCheckSum
        {
            get => _StrCheckSum;
            set
            {
                _StrCheckSum = value;
                this.RaisePropertyChanged();
            }
        }

        private string _StrFCS = "";

        public string StrFCS
        {
            get => _StrFCS;
            set
            {
                _StrFCS = value;
                this.RaisePropertyChanged();
            }
        }
        private string _StrCRC16LoHi = "";

        public string StrCRC16LoHi
        {
            get => _StrCRC16LoHi;
            set
            {
                _StrCRC16LoHi = value;
                this.RaisePropertyChanged();
            }
        }
        private string _StrFullStr = "";
        public string StrFullStr
        {
            get => _StrFullStr;
            set
            {
                _StrFullStr = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion

        #region Convert
        private string _StrDecimal = "";

        public string StrDecimal
        {
            get => _StrDecimal;
            set
            {
                _StrDecimal = value;
                this.RaisePropertyChanged();
            }
        }

        private string _StrSingleHex = "";

        public string StrSingleHex
        {
            get => _StrSingleHex;
            set
            {
                _StrSingleHex = value;
                this.RaisePropertyChanged();
            }
        }
        private string _StrDoubleHex = "";

        public string StrDoubleHex
        {
            get => _StrDoubleHex;
            set
            {
                _StrDoubleHex = value;
                this.RaisePropertyChanged();
            }
        }

        private string _StrInteger = "";

        public string StrInteger
        {
            get => _StrInteger;
            set
            {
                _StrInteger = value;
                this.RaisePropertyChanged();
            }
        }

        private string _Str16BitHex = "";

        public string Str16BitHex
        {
            get => _Str16BitHex;
            set
            {
                _Str16BitHex = value;
                this.RaisePropertyChanged();
            }
        }
        private string _Str32BitHex = "";

        public string Str32BitHex
        {
            get => _Str32BitHex;
            set
            {
                _Str32BitHex = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion
    }
}
