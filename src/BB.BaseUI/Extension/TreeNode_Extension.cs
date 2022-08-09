using BB.BaseUI.Other;
using BB.Tools.Entity;

namespace BB.BaseUI.Extension;

public static class TreeNodeExtension
{
    /// <summary>
    /// 初始化TreeView对象
    /// </summary>
    public static void InitTree(this TreeView treeView, Action<TreeNodeCollection> nodesAction = null, bool isExpandAll = true)
    {
        treeView.BeginUpdate();
        treeView.Nodes.Clear();

        var topNode = new TreeNode("所有记录", 0, 0);
        treeView.Nodes.Add(topNode);

        nodesAction?.Invoke(treeView.Nodes);
        
        if (isExpandAll)
        {
            treeView.ExpandAll();
        }
        treeView.EndUpdate();
    }
    
    /// <summary>
    /// 根据字典名称增加树节点
    /// </summary>
    /// <param name="nodes">上级节点</param>
    /// <param name="fieldName">对应字段</param>
    /// <param name="dictTypeName">字典名称</param>
    /// <param name="anotherName">节点别名</param>
    /// <param name="parentImageIndex">父节点图标</param>
    /// <param name="childImageIndex">子节点图标</param>
    public static void AddNode(this TreeNodeCollection nodes, string fieldName, string dictTypeName, string anotherName  = null, int parentImageIndex = 0, int childImageIndex = 1)
    {
        List<CListItem> dictList = GB.GetDictByName(dictTypeName);
        AddNode(nodes, fieldName, anotherName ?? dictTypeName, dictList, parentImageIndex, childImageIndex);
    }
    
    /// <summary>
    /// 根据键值列增加树节点
    /// </summary>
    /// <param name="nodes">上级节点</param>
    /// <param name="fieldName">对应字段</param>
    /// <param name="nodeName">节点名称</param>
    /// <param name="itemList">子节点数据</param>
    /// <param name="parentImageIndex">父节点图标</param>
    /// <param name="childImageIndex">子节点图标</param>
    public static void AddNode(this TreeNodeCollection nodes, string fieldName, string nodeName, List<CListItem> itemList, int parentImageIndex = 0, int childImageIndex = 1)
    {
        var node = new TreeNode(nodeName, parentImageIndex, parentImageIndex)
        {
            Tag = fieldName
        };
        itemList.ForEach(x => node.Nodes.Add(new TreeNode(x.Text, childImageIndex, childImageIndex)
        {
            Tag = x
        }));
        nodes.Add(node);
    }
    
    /// <summary>
    /// 增加数据组节点
    /// </summary>
    /// <param name="nodes">上级节点</param>
    /// <param name="nodeName">节点名称</param>
    /// <param name="itemList">节点数据组</param>
    /// <param name="parentImageIndex">节点图标</param>
    public static void AddNode(this TreeNodeCollection nodes, string nodeName, List<CListItem> itemList, int imageIndex)
    {
        var node = new TreeNode(nodeName, imageIndex, imageIndex)
        {
            Tag = itemList
        };
        nodes.Add(node);
    }
}