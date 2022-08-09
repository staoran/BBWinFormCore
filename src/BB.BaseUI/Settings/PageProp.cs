using BB.BaseUI.Settings.MozBar;

namespace BB.BaseUI.Settings;

/// <summary>
/// 页面对象信息
/// </summary>
public class PageProp
{       
    /// <summary>
    /// 控件文本显示
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// 控件图片对象
    /// </summary>
    public int ImageIndex { get; set; }

    /// <summary>
    /// PropertyPage对象
    /// </summary>
    public PropertyPage Page { get; set; }
        
    /// <summary>
    /// MozItem对象
    /// </summary>
    public MozItem MozItem { get; set; }	
}