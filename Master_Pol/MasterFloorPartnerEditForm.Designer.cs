using System.ComponentModel;

namespace Master_Pol;

partial class MasterFloorPartnerEditForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterFloorPartnerEditForm));
        SuspendLayout();
        // 
        // MasterFloorPatrnerEditForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
        Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Редактирование/добавление партнера";
        ResumeLayout(false);
    }

    #endregion
}