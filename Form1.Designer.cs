using System.Windows.Forms;
using Smudging.src.CustomizeException;
using Smudging.src.Window;
using Smudging.src.Server;
namespace App
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 0);
            webView.Margin = new Padding(0);
            webView.Name = "webView";
            webView.Size = new Size(800, 450);
            webView.TabIndex = 0;
            webView.ZoomFactor = 1D;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(webView);
            Name = "Form1";
            WebViewControls.WebView = webView;
            StartPosition = FormStartPosition.CenterScreen;
            Load += Loading;
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public Microsoft.Web.WebView2.WinForms.WebView2 webView;

        private void Loading(object sender, EventArgs e)
        {
            // 错误窗口标题
            MyException.WindowTitle = base.Text;
            WinControls winControls = new WinControls(this);
            WebViewControls WebControls = new WebViewControls();
            HttpServer.CONTROLS = winControls;
            HttpServer.WebControls = WebControls;

            Task.Run(async () =>
            {
                await HttpServer.Instance().StartAsync();
            });
        }
    }
}
