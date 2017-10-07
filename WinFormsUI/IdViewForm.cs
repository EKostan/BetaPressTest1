using System;
using System.Windows.Forms;
using Test1ApplicationCore;

namespace WinFormsUI
{
    public partial class IdViewForm : Form, ILongIdentifierView
    {
        public IdViewForm()
        {
            InitializeComponent();
        }

        public event EventHandler InitAlphaNumericIdentifier;
        public event EventHandler IncrementIdentifier;

        public string AlphaNumericIdentifier
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        public string IncrementValue
        {
            get => textBox2.Text;
            set => textBox2.Text = value;
        }

        public void ShowMessage(string message)
        {
            richTextBox1.AppendText(string.Concat(Environment.NewLine, message));
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        protected virtual void OnOnInitAlphaNumericIdentifier()
        {
            InitAlphaNumericIdentifier?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnOnIncrementIdentifier()
        {
            IncrementIdentifier?.Invoke(this, EventArgs.Empty);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnOnInitAlphaNumericIdentifier();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OnOnIncrementIdentifier();
        }
    }
}
