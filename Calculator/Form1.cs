using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        Model model = new Model();
        public Form1(Model model)
        {
            InitializeComponent();
            this.model = model;
            setBtnNumpad();
            setBtnOperation();
            setBtnMemory();
            setBtnClear();
            setBtnDot();
        }

        // 綁定數字按鈕點擊事件
        private void setBtnNumpad()
        {
            Button[] btns = { btn_numpad0, btn_numpad1, btn_numpad2, btn_numpad3, btn_numpad4, btn_numpad5, btn_numpad6, btn_numpad7, btn_numpad8, btn_numpad9 };
            foreach (var btn in btns)
            {
                btn.Click += btn_numpad_Click;
            }
        }
        private void btn_numpad_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int digit = int.Parse(btn.Text);
            model.processDigit(digit);
            updateDisplay();
        }
        // 綁定運算按鈕點擊事件
        private void setBtnOperation()
        {
            btn_plus.Click += (s, e) =>
            {
                model.processPlus();
                updateDisplay();
            };
            btn_minus.Click += (s, e) =>
            {
                model.processMinus();
                updateDisplay();
            };
            btn_times.Click += (s, e) =>
            {
                model.processTimes();
                updateDisplay();
            };
            btn_division.Click += (s, e) =>
            {
                model.processDivsion();
                updateDisplay();
            };
            btn_equal.Click += (s, e) =>
            {
                model.processEqual();
                updateDisplay();
            };
        }
        // 綁定記憶體相關按鈕鍵點擊事件
        private void setBtnMemory()
        {
            btn_mc.Click += (s, e) =>
            {
                model.processMemoryClear();
            };
            btn_mr.Click += (s, e) =>
            {
                model.processMemoryRead();
                updateDisplay();
            };
            btn_mplus.Click += (s, e) =>
            {
                model.processMemoryPlus();
            };
            btn_mminus.Click += (s, e) =>
            {
                model.processMemoryMinus();
            };
            btn_ms.Click += (s, e) =>
            {
                model.processMemoryStore();
                updateDisplay();
            };
        }
        // 綁定清除相關按鈕點擊事件
        private void setBtnClear()
        {
            btn_c.Click += (s, e) =>
            {
                model.processClear();
                updateDisplay();
            };
            btn_ce.Click += (s, e) =>
            {
                model.processClearEntry();
                updateDisplay();
            };
        }
        // 綁定小數點點擊事件
        private void setBtnDot()
        {
            btn_dot.Click += (s, e) =>
            {
                model.processDot();
                updateDisplay();
            };
        }

        // 更新顯示數字
        private void updateDisplay()
        {
            this.textbox_display.Text = model.getDisplayNumber();
        }
    }
}
