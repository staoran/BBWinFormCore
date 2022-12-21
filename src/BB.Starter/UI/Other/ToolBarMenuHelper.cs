using System.IO;
using System.Reflection;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.Security;
using DevExpress.Images;
using DevExpress.XtraBars;
using DevExpress.XtraBars.ToolbarForm;
using Furion;
using Furion.Logging.Extensions;

namespace BB.Starter.UI.Other;

/// <summary>
/// 动态创建 ToolbarForm 菜单辅助类
/// </summary>
public class ToolBarMenuHelper
{
    private readonly ToolbarFormControl _control;
    private readonly ToolbarForm _mainForm;
    private int _index;

    public ToolBarMenuHelper(ToolbarForm mainForm, ref ToolbarFormControl control)
    {
        _mainForm = mainForm;
        _control = control;
    }

    /// <summary>
    /// 刷新菜单
    /// </summary>
    public async Task RefreshMenu()
    {
        for (var i = 0; i < _index; i++)
        {
            _control.TitleItemLinks.RemoveAt(0);
        }
        _index = 0;
        // 刷新缓存
        await GB.LoadCache();
        await AddPages();
    }

    /// <summary>
    /// 加载菜单
    /// </summary>
    public Task AddPages()
    {
        //约定菜单共有3级，第一级为大的类别，第二级为小模块分组，第三级为具体的菜单
        // List<MenuNodeInfo> menuList = await App.GetService<MenuHttpService>().GetTreeAsync(GB.SystemType);
        List<MenuNodeInfo> menuList = GB.UserMenuNode;
        if (menuList.Count == 0) return Task.CompletedTask;
        try
        {
            _control.Manager.BeginUpdate();
            foreach (MenuNodeInfo firstInfo in menuList)
            {
                //如果没有菜单的权限，则跳过
                // if (!GB.HasFunction(firstInfo.FunctionId)) continue;

                //添加页面（一级菜单）
                BarSubItem menu = new BarSubItem(_control.Manager, firstInfo.Name);
                menu.Name = firstInfo.ID;
                _control.TitleItemLinks.Insert(_index++, menu);

                if (firstInfo.Children.Count == 0) continue;
                foreach (MenuNodeInfo secondInfo in firstInfo.Children)
                {
                    //如果没有菜单的权限，则跳过
                    // if (!GB.HasFunction(secondInfo.FunctionId)) continue;

                    //添加RibbonPageGroup（二级菜单）
                    BarHeaderItem group = new BarHeaderItem();
                    group.Caption = secondInfo.Name;
                    group.Name = secondInfo.ID;
                    group.Manager = _control.Manager;
                    group.Appearance.Font = new Font("Tahoma", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
                    group.Appearance.Options.UseFont = true;
                    menu.AddItem(group);

                    if (secondInfo.Children.Count == 0) continue;
                    foreach (MenuNodeInfo thirdInfo in secondInfo.Children)
                    {
                        //如果没有菜单的权限，则跳过
                        // if (!GB.HasFunction(thirdInfo.FunctionId)) continue;

                        //添加功能按钮（三级菜单）
                        BarButtonItem button = new BarButtonItem(_control.Manager, thirdInfo.Name);
                        button.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                        button.Glyph = LoadIcon(thirdInfo.Icon);

                        button.Name = thirdInfo.ID;
                        button.Tag = thirdInfo.WinformType;
                        button.ItemClick += (_, _) =>
                        {
                            if (button.Tag != null && !string.IsNullOrEmpty(button.Tag.ToString()))
                            {
                                LoadPlugInForm(button.Tag.ToString());
                            }
                            else
                            {
                                button.Caption.ShowUxTips();
                            }
                        };
                        menu.AddItem(button);
                    }
                }
            }
        }
        finally
        {
            _control.Manager.EndUpdate();
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 加载图标，如果加载不成功，那么使用默认图标
    /// </summary>
    /// <param name="iconPath"></param>
    /// <returns></returns>
    private Image LoadIcon(string iconPath)
    {
        Image? result = null;
        try
        {
            if (!string.IsNullOrEmpty(iconPath))
            {
                string path = Path.Combine(Application.StartupPath, iconPath);
                if (File.Exists(path))
                {
                    result = Image.FromFile(path);
                }
                else
                {
                    result = ImageResourceCache.Default.GetImage(iconPath);
                }
            }
        }
        catch
        {
            $"{iconPath}，图标地址识别异常！".LogError();
        }

        return result ?? Resources.menuIcon.ToBitmap();
    }

    /// <summary>
    /// 加载插件窗体
    /// </summary>
    private void LoadPlugInForm(string? typeName)
    {
        try
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new Exception("插件名称为空");
            }
            string[] itemArray = typeName.Split(',', ';');

            string type = itemArray[0].Trim();
            string filePath = itemArray[1].Trim();//必须是相对路径

            //判断是否配置了显示模式，默认窗体为Show非模式显示
            string showDialog = (itemArray.Length > 2) ? itemArray[2].ToLower() : "";
            bool isShowDialog = (showDialog == "1") || (showDialog == "dialog");

            string dllFullPath = Path.Combine(Application.StartupPath, filePath);
            Assembly tempAssembly = Assembly.LoadFrom(dllFullPath);
            Type? objType = tempAssembly.GetType(type);
            if (objType != null)
            {
                LoadMdiForm(_mainForm, objType, isShowDialog);
            }
        }
        catch (Exception ex)
        {
            $"加载模块【{typeName}】失败，请检查书写是否正确。{ex.Message}".ShowErrorTip();
            $"加载模块【{typeName}】失败，请检查书写是否正确。".LogError(ex);
        }
    }

    /// <summary>
    /// 唯一加载某个类型的窗体，如果存在则显示，否则创建。
    /// </summary>
    /// <param name="mainDialog">主窗体对象</param>
    /// <param name="formType">待显示的窗体类型</param>
    /// <param name="isShowDialog"></param>
    /// <returns></returns>
    public static Form LoadMdiForm(Form mainDialog, Type formType, bool isShowDialog)
    {
        Form? tableForm = null;
        bool bFound = false;
        if (!isShowDialog) //如果是模态窗口，跳过
        {
            foreach (Form form in mainDialog.MdiChildren)
            {
                if (form.GetType() == formType)
                {
                    bFound = true;
                    tableForm = form;
                    break;
                }
            }
        }

        // var serviceScopeFactory = App.GetService<IServiceScopeFactory>();
        // using var scope = serviceScopeFactory.CreateScope();
        // var services = scope.ServiceProvider;

        //没有在多文档中找到或者是模态窗口，需要初始化属性
        if (!bFound || isShowDialog)
        {
            // tableForm = (Form)Activator.CreateInstance(formType);
            tableForm = (Form)App.GetService(formType);

            // //如果窗体集成了IFunction接口(第一次创建需要设置)
            // if (tableForm is IFunction function)
            // {
            //     //初始化权限控制信息
            //     function.InitFunction(GB.LoginUserInfo, GB.FunctionDict);
            //
            //     //记录程序的相关信息
            //     function.AppInfo = new AppInfo(GB.AppUnit, GB.AppName, GB.AppWholeName, GB.SystemType);
            // }
        }

        if (tableForm == null)
        {
            throw new Exception("找不到要加载的模块");
        }

        if (isShowDialog)
        {
            tableForm.ShowDialog();
        }
        else
        {
            tableForm.MdiParent = mainDialog;
            tableForm.Show();
        }
        tableForm.BringToFront();
        tableForm.Activate();

        return tableForm;
    }
}