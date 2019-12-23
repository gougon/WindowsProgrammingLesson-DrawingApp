namespace DrawingForm
{
    partial class DrawingForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this._buttonLayout = new System.Windows.Forms.TableLayoutPanel();
            this._sixSideButton = new System.Windows.Forms.Button();
            this._lineButton = new System.Windows.Forms.Button();
            this._clearButton = new System.Windows.Forms.Button();
            this._rectangleButton = new System.Windows.Forms.Button();
            this._selectLabel = new System.Windows.Forms.Label();
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._redoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._buttonLayout.SuspendLayout();
            this._menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonLayout
            // 
            this._buttonLayout.BackColor = System.Drawing.SystemColors.Info;
            this._buttonLayout.ColumnCount = 4;
            this._buttonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this._buttonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this._buttonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this._buttonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this._buttonLayout.Controls.Add(this._sixSideButton, 2, 0);
            this._buttonLayout.Controls.Add(this._lineButton, 1, 0);
            this._buttonLayout.Controls.Add(this._clearButton, 3, 0);
            this._buttonLayout.Controls.Add(this._rectangleButton, 0, 0);
            this._buttonLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this._buttonLayout.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this._buttonLayout.Location = new System.Drawing.Point(0, 24);
            this._buttonLayout.Name = "_buttonLayout";
            this._buttonLayout.RowCount = 1;
            this._buttonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._buttonLayout.Size = new System.Drawing.Size(717, 47);
            this._buttonLayout.TabIndex = 0;
            // 
            // _sixSideButton
            // 
            this._sixSideButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._sixSideButton.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._sixSideButton.Location = new System.Drawing.Point(389, 3);
            this._sixSideButton.Name = "_sixSideButton";
            this._sixSideButton.Size = new System.Drawing.Size(117, 41);
            this._sixSideButton.TabIndex = 3;
            this._sixSideButton.Text = "Hexagon";
            this._sixSideButton.UseVisualStyleBackColor = true;
            // 
            // _lineButton
            // 
            this._lineButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._lineButton.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._lineButton.Location = new System.Drawing.Point(210, 3);
            this._lineButton.Name = "_lineButton";
            this._lineButton.Size = new System.Drawing.Size(117, 41);
            this._lineButton.TabIndex = 1;
            this._lineButton.Text = "Line";
            this._lineButton.UseVisualStyleBackColor = true;
            // 
            // _clearButton
            // 
            this._clearButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._clearButton.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._clearButton.Location = new System.Drawing.Point(568, 3);
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(117, 41);
            this._clearButton.TabIndex = 2;
            this._clearButton.Text = "Clear";
            this._clearButton.UseVisualStyleBackColor = true;
            // 
            // _rectangleButton
            // 
            this._rectangleButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._rectangleButton.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._rectangleButton.Location = new System.Drawing.Point(31, 3);
            this._rectangleButton.Name = "_rectangleButton";
            this._rectangleButton.Size = new System.Drawing.Size(117, 41);
            this._rectangleButton.TabIndex = 0;
            this._rectangleButton.Text = "Rectangle";
            this._rectangleButton.UseVisualStyleBackColor = true;
            // 
            // _selectLabel
            // 
            this._selectLabel.AccessibleName = "selectLabel";
            this._selectLabel.AutoSize = true;
            this._selectLabel.BackColor = System.Drawing.Color.Transparent;
            this._selectLabel.Location = new System.Drawing.Point(454, 378);
            this._selectLabel.Name = "_selectLabel";
            this._selectLabel.Size = new System.Drawing.Size(52, 12);
            this._selectLabel.TabIndex = 1;
            this._selectLabel.Text = "Selected : ";
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._undoMenuItem,
            this._redoMenuItem});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(717, 24);
            this._menuStrip.TabIndex = 2;
            this._menuStrip.Text = "menuStrip1";
            // 
            // _undoMenuItem
            // 
            this._undoMenuItem.Enabled = false;
            this._undoMenuItem.Name = "_undoMenuItem";
            this._undoMenuItem.Size = new System.Drawing.Size(51, 20);
            this._undoMenuItem.Text = "Undo";
            // 
            // _redoMenuItem
            // 
            this._redoMenuItem.Enabled = false;
            this._redoMenuItem.Name = "_redoMenuItem";
            this._redoMenuItem.Size = new System.Drawing.Size(50, 20);
            this._redoMenuItem.Text = "Redo";
            // 
            // DrawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 414);
            this.Controls.Add(this._selectLabel);
            this.Controls.Add(this._buttonLayout);
            this.Controls.Add(this._menuStrip);
            this.MainMenuStrip = this._menuStrip;
            this.Name = "DrawingForm";
            this.Text = "DrawingForm";
            this._buttonLayout.ResumeLayout(false);
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _buttonLayout;
        private System.Windows.Forms.Button _clearButton;
        private System.Windows.Forms.Button _lineButton;
        private System.Windows.Forms.Button _rectangleButton;
        private System.Windows.Forms.Button _sixSideButton;
        private System.Windows.Forms.Label _selectLabel;
        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _undoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _redoMenuItem;
    }
}

