namespace BB.BaseUI.Control.Security;

public partial class UserNameControl : DevExpress.XtraEditors.XtraUserControl
{
    public delegate void DeleteEventHandler(string id);
    public event DeleteEventHandler OnDeleteItem;

    public UserNameControl()
    {
        InitializeComponent();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (OnDeleteItem != null)
        {
            if (lblInfo.Tag != null)
            {
                OnDeleteItem(lblInfo.Tag.ToString());
            }
        }
    }

    public void BindData(string id, string name)
    {
        lblInfo.Text = name;
        lblInfo.Tag = id;

        btnDelete.Tag = id;
    }
}