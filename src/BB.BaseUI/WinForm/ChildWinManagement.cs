using BB.Tools.Extension;
using BB.Tools.Utils;
using Furion;

namespace BB.BaseUI.WinForm;

/// <summary>
/// 窗口管理辅助类
/// </summary>
public sealed class ChildWinManagement
{
	private ChildWinManagement()
	{
	}

	/// <summary> 
	/// 获取MDI父窗口是否有窗口标题为指定字符串的子窗口（如果已经存在把此子窗口推向前台） 
	/// </summary> 
	/// <param name="mdIwin">MDI父窗口</param> 
	/// <param name="caption">窗口标题</param> 
	/// <returns></returns> 
	public static bool ExistWin(Form mdIwin, string caption)
	{
		bool result = false;
		foreach (Form f in mdIwin.MdiChildren)
		{
			if (f.Text == caption)
			{
				result = true;
				f.Show();
				f.Activate();
				break;
			}
		}
		return result;
	}
                
	/// <summary>
	/// 唯一加载某个类型的窗体，如果存在则显示，否则创建。
	/// </summary>
	/// <param name="mainDialog">主窗体对象</param>
	/// <param name="formType">待显示的窗体类型</param>
	/// <returns></returns>
	public static Form LoadMdiForm(Form mainDialog, Type formType)
	{
		return LoadMdiForm(mainDialog, formType, null);
	}

	/// <summary>
	/// 唯一加载某个类型的窗体，如果存在则显示，否则创建。
	/// </summary>
	/// <param name="mainDialog">主窗体对象</param>
	/// <param name="formType">待显示的窗体类型</param>
	/// <param name="json">传递的参数内容，自定义Json格式</param>
	/// <returns></returns>
	public static Form LoadMdiForm(Form mainDialog, Type formType, string json)
	{
		bool bFound = false;
		Form tableForm = null;
		foreach (Form form in mainDialog.MdiChildren)
		{
			if (form.GetType() == formType)
			{
				bFound = true;
				tableForm = form;
				break;
			}
		}
		if (!bFound)
		{
			// tableForm = (Form) Activator.CreateInstance(formType);
			tableForm = (Form)App.GetService(formType);
			tableForm.MdiParent = mainDialog;
			tableForm.Show();
		}

		//窗体激活的时候，传递对应的参数信息
		ILoadFormActived formActived = tableForm as ILoadFormActived;
		if (formActived != null)
		{
			formActived.OnLoadFormActived(json);
		}

		//tableForm.Dock = DockStyle.Fill;
		//tableForm.WindowState = FormWindowState.Maximized;
		tableForm.BringToFront();
		tableForm.Activate();

		return tableForm;
	}

	/// <summary>
	/// 把控件附加到窗体上弹出
	/// </summary>
	/// <param name="control">待显示的控件</param>
	/// <param name="caption">窗体显示的标题</param>
	public static void PopControlForm(Type control, string caption)
	{
		object ctr =  ReflectionExtension.CreateInstance(control);
		if ((typeof(System.Windows.Forms.Control)).IsAssignableFrom(ctr.GetType()))
		{
			Form tmp = new Form();
			tmp.WindowState = FormWindowState.Maximized;
			tmp.ShowIcon = false;
			tmp.Text = caption;
			tmp.ShowInTaskbar = false;
			tmp.StartPosition = FormStartPosition.CenterScreen;

			System.Windows.Forms.Control ctrtmp = ctr as System.Windows.Forms.Control;
			ctrtmp.Dock = DockStyle.Fill;
			tmp.Controls.Add(ctrtmp);
			tmp.ShowDialog();
		}
	}

	/// <summary>
	/// 弹出窗体
	/// </summary>
	/// <param name="type">待显示的窗体类型</param>
	public static void PopDialogForm(Type type)
	{
		// object form =  ReflectionExtension.CreateInstance(type);
		object form = App.GetService(type);
		if ((typeof(Form)).IsAssignableFrom(form.GetType()))
		{
			Form tmp = form as Form;
			tmp.ShowInTaskbar = false;
			tmp.StartPosition = FormStartPosition.CenterScreen;
			tmp.ShowDialog();
		}
	}
}

/// <summary>
/// 使用ChildWinManagement辅助类处理多文档加载的窗体，在构建或激活后，触发一个通知窗体的事件，方便传递相关参数到目标窗体。
/// 为了更加通用的处理，传递的参数使用JSON定义格式的字符串。
/// </summary>
public interface ILoadFormActived
{
	/// <summary>
	/// 窗体激活的事件处理
	/// </summary>
	/// <param name="json">传递的参数内容，自定义JSON格式</param>
	void OnLoadFormActived(string json);
}