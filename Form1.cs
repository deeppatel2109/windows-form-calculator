using System;
using System.Data;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtDisplay.Text = "0";
        }

        // NUMBER & DECIMAL BUTTONS
        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (txtDisplay.Text == "0" && btn.Text != ".")
            {
                txtDisplay.Text = btn.Text;
            }
            else
            {
                txtDisplay.Text += btn.Text;
            }
        }

        // OPERATOR BUTTONS
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            // Prevent multiple operators back to back, or operators at the very start (except "-")
            string text = txtDisplay.Text;
            if (text.Length > 0)
            {
                char lastChar = text[text.Length - 1];
                if (lastChar == '+' || lastChar == '−' || lastChar == '×' || lastChar == '÷' || lastChar == '.')
                {
                    // Overwrite the last operator if a new one is typed
                    txtDisplay.Text = text.Substring(0, text.Length - 1) + btn.Text;
                }
                else
                {
                    txtDisplay.Text += btn.Text;
                }
            }
        }

        // EQUALS
        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                string expression = txtDisplay.Text;
                
                // Replace special characters with standard math operators for DataTable evaluation
                expression = expression.Replace("×", "*");
                expression = expression.Replace("÷", "/");
                expression = expression.Replace("−", "-");

                // Use DataTable to safely evaluate string mathematical expression
                DataTable dt = new DataTable();
                var result = dt.Compute(expression, "");
                
                txtDisplay.Text = result.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid expression", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDisplay.Text = "0";
            }
        }

        // CLEAR
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
        }

        // BACKSPACE
        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Length > 1)
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            else
                txtDisplay.Text = "0";
        }
    }
}
