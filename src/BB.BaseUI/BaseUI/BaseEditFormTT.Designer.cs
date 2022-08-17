using System.ComponentModel;
using BB.Entity.Base;
using BB.HttpServices.Base;
using FluentValidation;

namespace BB.BaseUI.BaseUI;

partial class BaseEditForm<T, IT, TV, T1, IT1, TV1> where T : BaseEntity<T1> where IT : BaseHttpService<T> where TV : AbstractValidator<T>, new() where T1 : BaseEntity, new() where IT1 : BaseHttpService<T1> where TV1 : AbstractValidator<T1>, new()
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
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "BaseEditFormTT";
    }

    #endregion
}