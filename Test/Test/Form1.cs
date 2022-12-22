using System.Diagnostics;

namespace Test
{
    public partial class Form1 : Form
    {
        string num = "";
        double num1, num2;
        string curr_sym;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonNum_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            num += btn.Tag;

            label1.Text = num.ToString();

        }

        private void buttonSymbol_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if((num1==0) || (num != ""))
            {
                if (num == "")
                {
                    num = "0";
                }
                num1 = Convert.ToDouble(num);
            }

            num = "";
            curr_sym = (string)btn.Tag;
            label2.Text = curr_sym.ToString();
        }

        private void buttonOperation_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            num2 = Convert.ToDouble(num);
            switch ((string)btn.Tag)
            {
                case "=":
                    switch (curr_sym)
                    {
                        case "+":
                            num1 += num2;
                            break;
                        case "-":
                            num1 -= num2;
                            break;
                        case "*":
                            num1 *= num2;
                            break;
                        case "/":
                            num1 /= num2;
                            break;
                    }
                    
                    label1.Text = num1.ToString();
                    label2.Text = "";
                    break;
            }
            num = "";
        }
    }
}