﻿namespace WFUI;

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
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create Account";
            this.button1.UseVisualStyleBackColor = true;

            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(222, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(642, 372);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 95);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(172, 37);
            this.button2.TabIndex = 2;
            this.button2.Text = "Add Money";
            this.button2.UseVisualStyleBackColor = true;

            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(24, 148);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(172, 37);
            this.button3.TabIndex = 3;
            this.button3.Text = "Take Money";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(24, 200);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(172, 37);
            this.button4.TabIndex = 4;
            this.button4.Text = "Buy";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(24, 390);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(172, 27);
            this.textBox1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 447);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Button button1;
    private ListView listView1;
    private Button button2;
    private Button button3;
    private Button button4;
    private TextBox textBox1;
}
