using BB.BaseUI.Settings.MozBar;

namespace BB.BaseUI.Settings;

/// <summary>
/// 参数配置界面控件
/// </summary>
public partial class FirefoxDialog : DevExpress.XtraEditors.XtraUserControl
{
	#region Data Members

	private PropertyPage _activePage = null;
	private Dictionary<string, PageProp> _pages = new Dictionary<string, PageProp>();
		
	#endregion

	/// <summary>
	/// 构造函数
	/// </summary>
	public FirefoxDialog()
	{
		InitializeComponent();
	}

	#region Private
	private void AddPage(MozItem item, PropertyPage page)
	{
		PageProp pageProp = new PageProp
		{
			Page = page,
			MozItem = item
		};

		mozPane1.Items.Add(item);

		_pages.Add(item.Name, pageProp);
	}

	private MozItem GetMozItem(string text)
	{
		return GetMozItem(text, ImageList == null ? 0 : _pages.Count);
	}

	private MozItem GetMozItem(string text, int imageIndex)
	{
		MozItem item = new MozItem();
		item.Name = "mozItem" + _pages.Count + 1;
		item.Text = text;

		if (imageIndex < ImageList.Images.Count)
		{
			item.Images.NormalImage = ImageList.Images[imageIndex];
		}

		return item;
	}

	#region Activate Page
	private void mozPane1_ItemClick(object sender, MozItemClickEventArgs e)
	{
		ActivatePage(e.MozItem);
	}

	private bool ActivatePage(MozItem item)
	{
		if (!_pages.ContainsKey(item.Name))
		{
			return false;
		}

		PageProp pageProp = _pages[item.Name];
		PropertyPage page = pageProp.Page;

		if (_activePage != null)
		{
			_activePage.Visible = false;
		}

		_activePage = page;

		if (_activePage != null)
		{
			mozPane1.SelectByName(item.Name);
			_activePage.Visible = true;

			if (!page.IsInit)
			{
				page.OnInit();
				page.IsInit = true;
			}

			_activePage.OnSetActive();
		}

		return true;
	}

	#endregion
		
	#endregion

	#region Public Interface

	#region Properties

	/// <summary>
	/// 参数页面对象
	/// </summary>
	public Dictionary<string, PageProp> Pages => _pages;

	/// <summary>
	/// 图片列表
	/// </summary>
	public ImageList ImageList
	{
		get => mozPane1.ImageList;
		set => mozPane1.ImageList = value;
	}
	#endregion

	#region Methods

        
	public void AddPage(string text, PropertyPage page)
	{
		AddPage(GetMozItem(text), page);
	}

	public void AddPage(string text, int imageIndex, PropertyPage page)
	{
		AddPage(GetMozItem(text, imageIndex), page);
	}

	public void Init()
	{
		foreach (PageProp pageProp in _pages.Values)
		{
			PropertyPage page = pageProp.Page;

			pagePanel.Controls.Add(page);
			page.Dock = DockStyle.Fill;
			page.Visible = false;
		}

		if (_pages.Count != 0)
		{
			ActivatePage(mozPane1.Items[0]);
		}
	}  
	#endregion

	#endregion

	#region Dialog Buttons
	private void btnOK_Click(object sender, EventArgs e)
	{
		bool result = Apply();
		if (result)
		{
			Close();
		}
		else
		{
			if (ParentForm != null)
			{
				ParentForm.DialogResult = DialogResult.None;
			}
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnApply_Click(object sender, EventArgs e)
	{
		bool result = Apply();
		if (result)
		{
			Close();
		}
		else
		{
			if (ParentForm != null)
			{
				ParentForm.DialogResult = DialogResult.None;
			}
		}

	}

	private bool Apply()
	{
		foreach (PageProp pageProp in _pages.Values)
		{
			if (pageProp.Page.IsInit)
			{
				bool result = pageProp.Page.OnApply();
				if (!result)
				{
					return result;
				}
			}
		}
		return true;//最后全部返回
	}

	private void Close()
	{
		if (ParentForm != null)
		{
			ParentForm.Close();
		}
	}
	#endregion
}