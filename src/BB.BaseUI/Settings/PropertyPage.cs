using BB.BaseUI.BaseUI;

namespace BB.BaseUI.Settings;

public class PropertyPage : BaseUserControl
{
	private System.ComponentModel.Container _components = null;

	/// <summary>
	/// 是否已经初始化
	/// </summary>
	public bool IsInit { get; set; }
	
	/// <summary>
	/// 初始化函数
	/// </summary>
	public PropertyPage()
	{
		InitComponent();
	}

	protected override void Dispose( bool disposing )
	{
		if( disposing )
		{
			if(_components != null)
			{
				_components.Dispose();
			}
		}
		base.Dispose( disposing );
	}

	#region Component Designer generated code
	private void InitComponent()
	{
		_components = new System.ComponentModel.Container();
	}
	#endregion

	#region Overridables

	/// <summary>
	/// 控件文本显示
	/// </summary>
	public new virtual string Text => GetType().Name;

	/// <summary>
	/// 控件图片对象
	/// </summary>
	public virtual Image Image => null;

	/// <summary>
	/// 初始化代码
	/// </summary>
	public virtual void OnInit()
	{
		IsInit = true;
	}

	/// <summary>
	/// 页面激活的处理
	/// </summary>
	public virtual void OnSetActive()
	{
	}

	/// <summary>
	/// 保存数据事件
	/// </summary>
	/// <returns></returns>
	public virtual bool OnApply()
	{
		return true;
	}


	#endregion
}