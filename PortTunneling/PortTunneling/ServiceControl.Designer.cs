using System;
using System.Drawing;
using System.Windows.Forms;

namespace PortTunneling
{
    partial class ServiceControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private GroupBox groupBox3;
        private Label label5;
        private TextBox textBox_ServiceStatus;
        private Label label4;
        private TextBox textBox_ServiceName;
        private Button button_start;
        private Button button_stop;
        private Button button_delete;
        private Button button_create;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox3 = new GroupBox();
            this.button_create = new Button();
            this.button_delete = new Button();
            this.button_stop = new Button();
            this.label5 = new Label();
            this.button_start = new Button();
            this.textBox_ServiceStatus = new TextBox();
            this.label4 = new Label();
            this.textBox_ServiceName = new TextBox();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            this.groupBox3.BackColor = SystemColors.Control;
            this.groupBox3.Controls.Add((Control)this.button_create);
            this.groupBox3.Controls.Add((Control)this.button_delete);
            this.groupBox3.Controls.Add((Control)this.button_stop);
            this.groupBox3.Controls.Add((Control)this.label5);
            this.groupBox3.Controls.Add((Control)this.button_start);
            this.groupBox3.Controls.Add((Control)this.textBox_ServiceStatus);
            this.groupBox3.Controls.Add((Control)this.label4);
            this.groupBox3.Controls.Add((Control)this.textBox_ServiceName);
            this.groupBox3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.groupBox3.Location = new Point(0, 0);
            this.groupBox3.Margin = new Padding(0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new Padding(0);
            this.groupBox3.Size = new Size(264, 95);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Service mode";
            this.button_create.FlatStyle = FlatStyle.Popup;
            this.button_create.Location = new Point(7, 66);
            this.button_create.Margin = new Padding(3, 4, 3, 4);
            this.button_create.Name = "button_create";
            this.button_create.Size = new Size(58, 22);
            this.button_create.TabIndex = 2;
            this.button_create.Text = "Create";
            this.button_create.UseVisualStyleBackColor = true;
            this.button_create.Click += new EventHandler(this.Button_Create_Click);
            this.button_delete.FlatStyle = FlatStyle.Popup;
            this.button_delete.Location = new Point(71, 66);
            this.button_delete.Margin = new Padding(3, 4, 3, 4);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new Size(58, 22);
            this.button_delete.TabIndex = 2;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new EventHandler(this.Button_Delete_Click);
            this.button_stop.FlatStyle = FlatStyle.Popup;
            this.button_stop.Location = new Point(135, 66);
            this.button_stop.Margin = new Padding(3, 4, 3, 4);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new Size(58, 22);
            this.button_stop.TabIndex = 2;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new EventHandler(this.Button_Stop_Click);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 42);
            this.label5.Name = "label5";
            this.label5.Size = new Size(79, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Service status:";
            this.label5.TextAlign = ContentAlignment.MiddleRight;
            this.button_start.FlatStyle = FlatStyle.Popup;
            this.button_start.Location = new Point(199, 66);
            this.button_start.Margin = new Padding(3, 4, 3, 4);
            this.button_start.Name = "button_start";
            this.button_start.Size = new Size(58, 22);
            this.button_start.TabIndex = 2;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new EventHandler(this.Button_Start_Click);
            this.textBox_ServiceStatus.Location = new Point(93, 39);
            this.textBox_ServiceStatus.Margin = new Padding(3, 2, 3, 2);
            this.textBox_ServiceStatus.Name = "textBox_ServiceStatus";
            this.textBox_ServiceStatus.ReadOnly = true;
            this.textBox_ServiceStatus.Size = new Size(164, 21);
            this.textBox_ServiceStatus.TabIndex = 3;
            this.textBox_ServiceStatus.WordWrap = false;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 17);
            this.label4.Name = "label4";
            this.label4.Size = new Size(75, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Service name:";
            this.label4.TextAlign = ContentAlignment.MiddleRight;
            this.textBox_ServiceName.Location = new Point(93, 14);
            this.textBox_ServiceName.Margin = new Padding(3, 2, 3, 2);
            this.textBox_ServiceName.Name = "textBox_ServiceName";
            this.textBox_ServiceName.Size = new Size(164, 21);
            this.textBox_ServiceName.TabIndex = 0;
            this.textBox_ServiceName.Text = "ServiceName";
            this.textBox_ServiceName.WordWrap = false;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Controls.Add((Control)this.groupBox3);
            this.Margin = new Padding(0);
            this.Name = nameof(ServiceControl);
            this.Size = new Size(264, 95);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
