using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Model
    {
        private enum Operate
        {
            PLUS,
            MINUS,
            TIMES,
            DIVISION
        }

        // 記憶體數字
        private double memoryNumber = 0;
        // 累加數字
        private double accNumber = 0;
        // 暫存數字(連續點擊 = 時用到)
        private double inputNumber = 0;
        // 顯示數字(字串方便串接小數點)
        private string displayNumber = "0";
        // 紀錄上一個運算子(用於連續點擊等於時)
        // 因為這變數只在意上一個運算的符號
        // 而且最後一個為 = 時要做的判斷恨不一樣，所以沒有把 = 放到 enum
        private Operate lastOperate = Operate.PLUS;

        // flag
        // 連續按下 = 鍵時，第一次需要記住輸入框的內容
        private bool lastPressIsEqual = false;
        // 用於輸入數字時清除結果的判斷和阻止連續按下 +-*/ 時不要運算
        private bool clearDisplay = true;

        public void processDigit(int digit)
        {
            // 清除輸入框
            if (clearDisplay)
            {
                // 最後運算子為 = 時
                if (lastPressIsEqual)
                {
                    accNumber = 0;
                    inputNumber = 0;
                    lastPressIsEqual = false;
                }
                displayNumber = "0";
                clearDisplay = false;
            }

            // 輸入數字
            if (displayNumber == "0")
                displayNumber = "";
            displayNumber += digit.ToString();
        }
        public void processDot()
        {
            if (!displayNumber.Contains("."))
                displayNumber += ".";
        }
        public void processPlus()
        {
            processOperate(Operate.PLUS);
        }
        public void processMinus()
        {
            processOperate(Operate.MINUS);
        }
        public void processTimes()
        {
            processOperate(Operate.TIMES);
        }
        public void processDivsion()
        {
            processOperate(Operate.DIVISION);
        }
        private void processOperate(Operate operate)
        {
            // 記住當前輸入框內容
            inputNumber = double.Parse(displayNumber);
            // true 時代表前一個結果已經計算出來，不用再計算
            if (!clearDisplay)
                calculateWithLastOperate();
            clearDisplay = true;
            lastPressIsEqual = false;
            this.lastOperate = operate;
        }
        public void processEqual()
        {
            if (!lastPressIsEqual)
                inputNumber = double.Parse(displayNumber);
            lastPressIsEqual = true;
            calculateWithLastOperate();
            clearDisplay = true;
        }
        private void calculateWithLastOperate()
        {
            switch (lastOperate)
            {
                case Operate.PLUS:
                    accNumber += inputNumber;
                    break;
                case Operate.MINUS:
                    accNumber -= inputNumber;
                    break;
                case Operate.TIMES:
                    accNumber *= inputNumber;
                    break;
                case Operate.DIVISION:
                    accNumber /= inputNumber;
                    break;
                default:
                    break;
            }
            displayNumber = accNumber.ToString();
        }
        public void processMemoryClear()
        {
            this.memoryNumber = 0;
            this.clearDisplay = true;
        }
        public void processMemoryRead()
        {
            this.displayNumber = this.memoryNumber.ToString();
            this.inputNumber = this.memoryNumber;
        }
        public void processMemoryPlus()
        {
            this.memoryNumber += double.Parse(this.displayNumber);
            this.inputNumber = double.Parse(displayNumber);
        }
        public void processMemoryMinus()
        {
            this.memoryNumber -= double.Parse(this.displayNumber);
            this.inputNumber = double.Parse(displayNumber);
        }
        public void processMemoryStore()
        {
            this.memoryNumber = double.Parse(this.displayNumber);
            this.clearDisplay = true;
        }
        public void processClear()
        {
            this.accNumber = 0;
            processClearEntry();
        }
        public void processClearEntry()
        {
            this.displayNumber = "0";
        }
        public string getDisplayNumber()
        {
            return displayNumber;
        }
    }
}
