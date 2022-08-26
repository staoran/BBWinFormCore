using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.Entity.Security;
using BB.HttpServices.Core.Dict;
using BB.HttpServices.Core.FieldControlConfig;
using BB.HttpServices.Core.Menu;
using BB.Starter.UI.Code;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.File;
using BB.Tools.Format;
using BB.Tools.TaskQueue;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Furion.DependencyInjection;

namespace BB.Starter.UI.SYS;

public partial class FrmSelectDataBase : XtraForm, ITransient
{
    private readonly FieldControlConfigHttpService _fieldControlConfigHttpService;
    private readonly MenuHttpService _menuHttpService;
    private readonly List<FieldControlConfig> _list = new();

    private readonly string _dataPath = @$"{AppDomain.CurrentDomain.BaseDirectory}FieldData\";
    private readonly DictTypeHttpService _dictTypeBLL;

    public FrmSelectDataBase(FieldControlConfigHttpService fieldControlConfigHttpService, MenuHttpService menuHttpService,
        DictTypeHttpService dictTypeHttpService)
    {
        SplashScreenHelper.Show();
        SplashScreenHelper.SetDescription("界面加载中...");
        InitializeComponent();
        _fieldControlConfigHttpService = fieldControlConfigHttpService;
        _menuHttpService = menuHttpService;
        _dictTypeBLL = dictTypeHttpService;
    }

    private async void frmSelectDataBase_Load(object sender, EventArgs e)
    {
        SplashScreenHelper.SetDescription("初始化网格...");

        #region 按钮
            
        bar1.AddBarButtonItem("Load", "加载表和字段数据", "refresh2", load_Click);
        bar1.AddBarButtonItem("Gen", "生成", "download", GenClick);
        bar1.AddBarButtonItem("Exit", "退出", "close", ExitClick, null,true);

        #endregion
        
        #region 列初始化

        gridView1.Columns.Clear();
        gridView1.GroupCount = 1;
        gridView1.SortInfo.Add(gridView1.CreateColumn("TableName", "表名"), ColumnSortOrder.Ascending);
        gridView1.CreateColumn("TableDes", "表说明").Visible = false;
        gridView1.CreateColumn("CSharpFieldFullType", "字段全类型").Visible = false;
        gridView1.CreateColumn("DataBaseFieldName", "DB名", 80);
        gridView1.CreateColumn("DataBaseFieldType", "DB类型", 55);
        gridView1.CreateColumn("DataBaseFieldLong", "DB长度", 50).CreateSpinEdit();
        gridView1.CreateColumn("DataBaseFieldDes", "DB说明", 75);
        gridView1.CreateColumn("IsKey", "主键", 35);
        gridView1.CreateColumn("IsIdentity", "自增", 35);
        gridView1.CreateColumn("IsNull", "可空", 35);
        gridView1.CreateColumn("CSharpFieldName", "C#名", 100).CreateTextEdit();
        gridView1.CreateColumn("CSharpFieldDes", "C#说明", 100).CreateTextEdit();
        gridView1.CreateColumn("CSharpFieldType", "C#类型", 75).CreateTextEdit();
        gridView1.CreateColumn("CSharpFieldLong", "C#长度", 50).CreateSpinEdit();
        gridView1.CreateColumn("ControlType", "控件", 35).CreateComboBoxEdit()
            .BindDictItems(new List<CListItem>
            {
                new("文本", "TextEdit"),
                new("下拉", "ComboBoxEdit"),
                new("开关", "ToggleSwitch"),
                new("搜索网格", "SearchLookUpEdit"),
                new("多选下拉", "CheckedComboBoxEdit"),
                new("网格下拉", "LookUpEdit"),
                new("日期", "DateEdit"),
                new("时间", "TimeEdit"),
                new("数字", "SpinEdit"),
                new("勾选", "CheckEdit"),
                new("多行", "MemoEdit"),
                new("网格树", "TreeListLookUpEdit")
            }, false);
        List<string> dataSourceSelection =
            (await _dictTypeBLL.GetEndpointItemsAsync()).Select(x => $"\"{x.Value}\"").ToList();
        dataSourceSelection.Insert(0, "GB.AllRegions");
        dataSourceSelection.Insert(0, "GB.AllUserDict");
        dataSourceSelection.Insert(0, "GB.AllOuDict");
        dataSourceSelection.Insert(0, "GB.AllCostType");
        dataSourceSelection.Add("\"已审核,未审核\"");
        dataSourceSelection.Add("\"是,否\"");
        gridView1.CreateColumn("DataTableName", "数据源", 100).CreateComboBoxEdit(TextEditStyles.Standard)
            .BindDictItems(dataSourceSelection);
        gridView1.CreateColumn("ControlLabelName", "LabelName", 100).CreateTextEdit();
        gridView1.CreateColumn("ControlName", "控件Name", 100).CreateTextEdit();
        gridView1.CreateColumn("Defaults", "默认值", 100).CreateComboBoxEdit(TextEditStyles.Standard)
            .BindDictItems(new[]
            {
                "true",
                "false",
                "DateTime.Now",
                "LoginUserInfo.ID.ToString()",
                "LoginUserInfo.CompanyId",
                "0",
                "\"0\"",
                "Snowflake.Instance().GetId().ToString()"
            });
        gridView1.CreateColumn("Validation", "验证", 35).CreateComboBoxEdit()
            .BindDictItems(new List<CListItem>
            {
                new("数字", "Numeric"),
                new("整数", "Number"),
                new("小数", "Decimal"),
                new("电话", "Phone"),
                new("手机", "Mobile"),
                new("电话手机", "PhoneAndMobile"),
                new("用户名", "UserName"),
                new("中文", "Chinese"),
                new("字母", "Letter"),
                new("字母数字", "LetterNum"),
                new("大写字母", "UpLetter"),
                new("大写字母数字", "UpLetterNum"),
                new("小写字母", "LowLetter"),
                new("小写字母数字", "LowLetterNum"),
                new("身份证", "IdCard"),
                new("邮箱", "Email"),
                new("网址", "URL"),
                new("文件路径", "FilePath"),
            });
        gridView1.CreateColumn("Sort", "排序", 35).CreateSpinEdit();
        gridView1.CreateColumn("IsVisible", "可见", 35);
        gridView1.CreateColumn("IsSearch", "搜索", 35);
        gridView1.CreateColumn("IsAdvSearch", "高搜", 35);
        gridView1.CreateColumn("IsColumn", "网格", 35);
        gridView1.CreateColumn("IsAdd", "新增", 35);
        gridView1.CreateColumn("IsEdit", "编辑", 35);
        gridView1.CreateColumn("IsReadonly", "只读", 35);
        gridView1.CreateColumn("IsCheckNull", "验空", 35);
        gridView1.CreateColumn("IsCheckLong", "验长", 35);
        gridView1.CreateColumn("IsCheckDuplicate", "验重", 35);
        gridView1.CreateColumn("OrderBy", "排序", 35).CreateComboBoxEdit()
            .BindDictItems(new List<CListItem>
            {
                new("降序", "desc"),
                new("升序", "asc")
            });
        gridView1.CreateColumn("OptimisticLock", "锁", 35);
        gridView1.CreateColumn("SummaryItemType", "汇总方式", 60).CreateComboBoxEdit()
            .BindDictItems(new List<CListItem>
            {
                new("计数", "Count"),
                new("求和", "Sum"),
                new("最大值", "Max"),
                new("最小值", "Min"),
                new("平均", "Average"),
                new("手动", "Custom"),
                new("禁用", "None")
            }, false);
        gridView1.CreateColumn("SummaryItemDisplayFormat", "汇总格式", 60).CreateComboBoxEdit(TextEditStyles.Standard)
            .BindDictItems(new[]
            {
                "{0}",
                "{0:C2}",
                "{0:0.##}"
            });

        #region 一些其他设置方法的案例，包括单元格中按钮弹出选择框

        /*
        gridview.Columns.ColumnByFieldName("ID").Visible = false;//设置不可见
        gridview.Columns.ColumnByFieldName("Pallet").CreateCheckEdit();//创建复选框控件
        gridview.Columns.ColumnByFieldName("TradeMode").CreateLookUpEdit().BindDictItems("贸易方向");//创建列表并绑定字典
        gridview.Columns.ColumnByFieldName("OrganizationCode").CreateTextEdit();//文本控件
        gridview.CreateColumn("Remark", "备注", 300, true).CreateMemoEdit();//设置备件内容
        
        //设置按钮可选择机构
        var deptControl = gridview.Columns.ColumnByFieldName("OuName").CreateButtonEdit(ButtonPredefines.Search);
        deptControl.ButtonClick += (object sender, ButtonPressedEventArgs e) =>
        {
            if (gridview.GetFocusedRow() == null)
            {
                gridview.AddNewRow();//一定要增加
            }

            FrmSelectOU dlg = new FrmSelectOU();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gridview.SetFocusedRowCellValue("OuName", dlg.OuName);
                gridview.SetFocusedRowCellValue("OuID", dlg.OuID);
            }
        };
        
        //设置可编辑
        gridview.OptionsBehavior.ReadOnly = false;
        gridview.OptionsBehavior.Editable = true;
        */

        #endregion

        #endregion

        #region 网格视图初始化和设置

        // 初始化
        gridView1.InitGridView();
            
        gridView1.ViewCaption = @"客货收货人明细";
            
        // 禁止排序
        gridView1.OptionsCustomization.AllowSort = false;

        gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
        
        // 展开所有分组
        gridView1.OptionsBehavior.AutoExpandAllGroups = true;

        #endregion
        
        #region 网格相关事件绑定

        // 绑定列显示值事件
        gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
            
        // 绑定行样式事件
        gridView1.RowCellStyle += gridView1_RowCellStyle;
            
        // 绑定行数据校验事件
        gridView1.ValidateRow += gridView1_ValidateRow;
        
        gridView1.CellValueChanged += gridView1_CellValueChanged;

        #endregion
        
        SplashScreenHelper.SetDescription("加载表资料...");
        txtTable.Properties.Items.Clear();
        string[] tableName = (await _fieldControlConfigHttpService.GetTableNamesAndComments()).ToArray();
        txtTable.Properties.Items.AddRange(tableName);
        txtTable.Properties.DropDownRows = 40;
        txtChildTable.Properties.Items.AddRange(tableName);
        txtChildTable.Properties.DropDownRows = 40;
        txtChildTable.EditValueChanged += txtChildTable_EditValueChanged;
        textEdit2.Text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
        txtNameSpace.Text = "TMS";
        isAdd.Checked = true;
        isQuery.Checked = true;
        isEdit.Checked = true;
        isDelete.Checked = true;
        isCheck.Checked = true;
        isImport.Checked = true;
        isExport.Checked = true;
        isPrint.Checked = true;
        isTree.Checked = true;
        SplashScreenHelper.Close();
    }

    #region GridView事件

    private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
    {
        GridView gridView = gridView1;
        if (e.Column.FieldName == "CSharpFieldName")
        {
            gridView.SetRowCellValue(e.RowHandle, "ControlName", $"txt{e.Value}");
        }
    }

    /// <summary>
    /// 行数据校验
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
    {
        //如果需要校验非空输入，那么添加对应字段
        // gridControl1.ValidateRowNull(e, "ContactPerson", "Address", "ProvinceNo", "CityNo", "AreaNo", "Mobile");
        // if (!result)
        //     e.ErrorText.ShowUxError();
    }

    /// <summary>
    /// 定义单元格样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        GridView gridView = gridView1;
        //if (e.Column.FieldName == "OrderStatus")
        //{
        //    string status = gridView.GetRowCellValue(e.RowHandle, "OrderStatus").ToString();
        //    Color color = Color.White;
        //    if (status == "已审核")
        //    {
        //        e.Appearance.BackColor = Color.Red;
        //        e.Appearance.BackColor2 = Color.LightCyan;
        //    }
        //}
    }

    /// <summary>
    /// 自定义列的显示文本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
    {
        // string columnName = e.Column.FieldName;
        // if (e.Column.ColumnType == typeof(DateTime))
        // {
        //     if (e.Value != null)
        //     {
        //         if (e.Value == DBNull.Value || Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
        //         {
        //             e.DisplayText = "";
        //         }
        //         else
        //         {
        //             e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm"); //yyyy-MM-dd
        //         }
        //     }
        // }
        // else if (e.Column.ColumnEdit.GetType() == typeof(RepositoryItemComboBox))
        // {
        //     var list = e.Column.ColumnEdit as RepositoryItemComboBox;
        //     e.DisplayText = list?.GetComboBoxValue(e.Value);
        // }
        // else if (columnName == "DictType_ID")
        // {
        //     e.DisplayText = await App.GetService<IDictDataHttpService>().GetFieldValue(string.Concat(e.Value), "Name");
        // }
    }

    #endregion

    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ExitClick(object sender, EventArgs e)
    {
        Close();
    }

    /// <summary>
    /// 表字段数据加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void load_Click(object sender, EventArgs e)
    {
        SplashScreenHelper.Show();
        SplashScreenHelper.SetDescription("元数据加载中...");
        _list.Clear();
        // 从数据库加载当前表的字段元数据
        _list.AddRange(await _fieldControlConfigHttpService.GetFieldControlConfigs(txtTable.EditValue.ObjToStr().Split('|')[0]));

        // 子表是否有勾选，如果有，则加载子表的字段元数据
        var data = txtChildTable.Properties.Items.Where(p => p.CheckState == CheckState.Checked).ToList();
        if (data.Count > 0)
        {
            foreach (CheckedListBoxItem x in data)
            {
                _list.AddRange(
                    await _fieldControlConfigHttpService.GetFieldControlConfigs(x.Value.ObjToStr().Split('|')[0]));
            }
        }

        SplashScreenHelper.SetDescription("历史数据加载中...");
        // 遍历已加载的表名，加载历史数据
        List<FieldControlConfig> fieldList = new ();
        EnumerableExtension.ForEach(_list.Select(s => s.TableName).Distinct(), e =>
        {
            var filePath = $"{_dataPath}{e}.json";

            // 当前表的历史记录不存在
            if (!FileUtil.IsExistFile(filePath)) return;

            // 读取历史记录
            string fieldsText = FileUtil.FileToString(filePath);
            if (fieldsText.Length == 0) return;

            // 历史数据反序列化为实体列表
            List<FieldControlConfig> list = JsonHelper.Deserialize<List<FieldControlConfig>>(fieldsText);
            if (!list.Any()) return;
            fieldList.AddRange(list);
        });

        // 遍历字段，填充历史配置项
        if (fieldList.Any())
        {
            _list.ForEach(x =>
            {
                FieldControlConfig? fieldData = fieldList.FirstOrDefault(w =>
                    w.TableName == x.TableName && w.DataBaseFieldName == x.DataBaseFieldName);
                if (fieldData == null) return;
                x.Defaults = fieldData.Defaults;
                x.IsColumn = fieldData.IsColumn;
                x.IsCheckDuplicate = fieldData.IsCheckDuplicate;
                x.IsCheckNull = fieldData.IsCheckNull;
                x.IsReadonly = fieldData.IsReadonly;
                x.IsAdvSearch = fieldData.IsAdvSearch;
                x.IsSearch = fieldData.IsSearch;
                x.IsCheckLong = fieldData.IsCheckLong;
                x.IsAdd = fieldData.IsAdd;
                x.IsEdit = fieldData.IsEdit;
                x.Validation = fieldData.Validation;
                x.IsVisible = fieldData.IsVisible;
                x.ControlName = fieldData.ControlName;
                x.ControlLabelName = fieldData.ControlLabelName;
                x.DataTableName = fieldData.DataTableName;
                x.ControlType = fieldData.ControlType;
                x.CSharpFieldDes = fieldData.CSharpFieldDes;
                x.CSharpFieldName = fieldData.CSharpFieldName;
                x.CSharpFieldLong = fieldData.CSharpFieldLong;
                x.CSharpFieldType = fieldData.CSharpFieldType;
                x.SummaryItemType = fieldData.SummaryItemType;
                x.SummaryItemDisplayFormat = fieldData.SummaryItemDisplayFormat;
                x.Sort = fieldData.Sort;
                x.OptimisticLock = fieldData.OptimisticLock;
                x.OrderBy = fieldData.OrderBy;
            });
        }

        // 绑定数据
        // gridControl1.DataSource = new BindingList<FieldControlConfig>(_list);
        gridControl1.DataSource = _list.OrderBy(x => x.Sort).ToList();
        gridControl1.RefreshDataSource();
        SplashScreenHelper.Close();
    }
    
    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void GenClick(object sender, EventArgs e)
    {
        SplashScreenHelper.Show();
        SplashScreenHelper.SetDescription("数据校验并构造模版数据...");
        if (string.IsNullOrWhiteSpace(txtNameSpace.Text))
        {
            "请填写命名空间".ShowUxWarning();
            txtNameSpace.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(textEdit2.Text))
        {
            "请选择输出目录".ShowUxWarning();
            textEdit2.Focus();
            return;
        }

        // 初始化生成代码所用的元数据
        var m = new ModuleImport();
        SetInfo(m);

        if (!m.MetadataImports.Any())
        {
            "主表字段未加载".ShowUxWarning();
            txtTable.Focus();
            return;
        }

        if (!string.IsNullOrEmpty(m.ChildTableName) && !m.ChildMetadataImports.Any())
        {
            "子表字段未加载".ShowUxWarning();
            txtChildTable.Focus();
            return;
        }
        
        try
        {
            var nvEngine = new NVelocityHelper(@"Template\", Path.Combine(textEdit2.Text, m.Name), m.Name, m.ChildName);
            nvEngine.AddKeyValue("M", m);
            nvEngine.AddKeyValue("NameSpace", m.NameSpace);
            nvEngine.AddKeyValue("Name", m.Name);
            nvEngine.AddKeyValue("Display", m.Display);
            nvEngine.AddKeyValue("TableName", m.TableName);
            nvEngine.AddKeyValue("EntityBaseType", m.EntityBaseType);
            nvEngine.AddKeyValue("ForeignKeyName", m.ForeignKeyName);
            nvEngine.AddKeyValue("ChildName", m.ChildName);
            nvEngine.AddKeyValue("ChildDisplay", m.ChildDisplay);
            nvEngine.AddKeyValue("ChildTableName", m.ChildTableName);
            nvEngine.AddKeyValue("ChildEntityBaseType", m.ChildEntityBaseType);
            nvEngine.AddKeyValue("IsQuery", m.IsQuery);
            nvEngine.AddKeyValue("IsAdd", m.IsAdd);
            nvEngine.AddKeyValue("IsAdd", m.IsAdd);
            nvEngine.AddKeyValue("IsEdit", m.IsEdit);
            nvEngine.AddKeyValue("IsDelete", m.IsDelete);
            nvEngine.AddKeyValue("IsCheck", m.IsCheck);
            nvEngine.AddKeyValue("IsImport", m.IsImport);
            nvEngine.AddKeyValue("IsExport", m.IsExport);
            nvEngine.AddKeyValue("IsPrint", m.IsPrint);
            nvEngine.AddKeyValue("IsTree", m.IsTree);
            nvEngine.AddKeyValue("IsChildListNull", m.IsChildListNull);
            nvEngine.AddKeyValue("MetadataImports", m.MetadataImports);
            nvEngine.AddKeyValue("ChildMetadataImports", m.ChildMetadataImports);
            nvEngine.AddKeyValue("NewLine", "\n");

            string allFieldNameString = string.Empty,
                allFieldDesString = string.Empty,
                checkNullFieldNameString = string.Empty,
                advQueryFieldNameString = string.Empty,
                columnFieldNameString = string.Empty,
                columnFieldDesString = string.Empty,
                childColumnFieldNameString = string.Empty,
                childColumnFieldDesString = string.Empty;
            EnumerableExtension.ForEach(m.MetadataImports, x =>
            {
                allFieldNameString = allFieldNameString.IsNullOrEmpty()
                    ? x.CSharpFieldName
                    : $"{allFieldNameString},{x.CSharpFieldName}";
                allFieldDesString = allFieldDesString.IsNullOrEmpty()
                    ? x.CSharpFieldDes
                    : $"{allFieldDesString},{x.CSharpFieldDes}";
                
                if (x.IsSearch)
                {
                    advQueryFieldNameString = advQueryFieldNameString.IsNullOrEmpty()
                        ? x.CSharpFieldName
                        : $"{advQueryFieldNameString},{x.CSharpFieldName}";
                }
                
                if (x.IsCheckNull)
                {
                    checkNullFieldNameString = checkNullFieldNameString.IsNullOrEmpty()
                        ? x.ControlName
                        : $"{checkNullFieldNameString},{x.ControlName}";
                }
                
                if (!x.IsColumn) return;
                columnFieldNameString = columnFieldNameString.IsNullOrEmpty()
                    ? x.CSharpFieldName
                    : $"{columnFieldNameString},{x.CSharpFieldName}";
                columnFieldDesString = columnFieldDesString.IsNullOrEmpty()
                    ? x.CSharpFieldDes
                    : $"{columnFieldDesString},{x.CSharpFieldDes}";
            });
            
            EnumerableExtension.ForEach(m.ChildMetadataImports, x =>
            {
                if (!x.IsColumn) return;
                childColumnFieldNameString = childColumnFieldNameString.IsNullOrEmpty()
                    ? x.CSharpFieldName
                    : $"{childColumnFieldNameString},{x.CSharpFieldName}";
                childColumnFieldDesString = childColumnFieldDesString.IsNullOrEmpty()
                    ? x.CSharpFieldDes
                    : $"{childColumnFieldDesString},{x.CSharpFieldDes}";
            });
            
            nvEngine.AddKeyValue("AllFieldNameString", allFieldNameString);
            nvEngine.AddKeyValue("AllFieldDesString", allFieldDesString);
            nvEngine.AddKeyValue("AdvQueryFieldNameString", advQueryFieldNameString);
            nvEngine.AddKeyValue("checkNullFieldNameString", checkNullFieldNameString);
            nvEngine.AddKeyValue("ColumnFieldNameString", columnFieldNameString);
            nvEngine.AddKeyValue("ColumnFieldDesString", columnFieldDesString);
            nvEngine.AddKeyValue("ChildColumnFieldNameString", childColumnFieldNameString);
            nvEngine.AddKeyValue("ChildColumnFieldDesString", childColumnFieldDesString);
            SplashScreenHelper.SetDescription("生成中...");
            nvEngine.ExecuteFile();
            if (isMenu.Checked && !await _menuHttpService
                    .AddTransferMenuAsync(m.Display, $"BB.{m.NameSpace}.UI.Frm{m.Name},BB.{m.NameSpace}.dll"))
            {
                "菜单生成失败！".ShowUxError();
            }
            "代码生成成功！".ShowSuccessTip(this);
        }
        catch (Exception ex)
        {
            SplashScreenHelper.Close();
            ex.StackTrace.ShowError();
        }
        SplashScreenHelper.Close();
    }
    
    /// <summary>
    /// 准备生成代码所用的数据
    /// </summary>
    /// <param name="info"></param>
    private void SetInfo(ModuleImport info)
    {
        // 基础名称、命名空间和操作项
        info.NameSpace = txtNameSpace.Text;
        info.Name = txtName.Text;
        info.Display = txtDisplay.Text;
        info.TableName = txtTable.Text.Split('|')[0];
        info.EntityBaseType = txtEntityBaseType.Text;
        info.ForeignKeyName = txtForeignKeyName.Text;
        info.ChildName = txtChildName.Text;
        info.ChildDisplay = txtChildDisplay.Text;
        info.ChildTableName = txtChildTable.Text.Split('|')[0];
        info.ChildEntityBaseType = txtChildEntityBaseType.Text;
        info.IsQuery = isQuery.Checked;
        info.IsAdd = isAdd.Checked;
        info.IsEdit = isEdit.Checked;
        info.IsDelete = isDelete.Checked;
        info.IsCheck = isCheck.Checked;
        info.IsImport = isImport.Checked;
        info.IsExport = isExport.Checked;
        info.IsPrint = isPrint.Checked;
        info.IsTree = isTree.Checked;
        info.IsChildListNull = isChildListNull.Checked;

        // info.MetadataImports = list.GroupBy(p => p.TableName);

        // grid结束编辑，更新数据
        gridView1.CloseEditor();
        gridView1.UpdateCurrentRow();

        // 加载主表和子表数据
        info.MetadataImports = _list?.Where(x => x.TableName == txtTable.Text.Split('|')[0]).OrderBy(x => x.Sort);
        info.ChildMetadataImports = _list?.Where(x => x.TableName == txtChildTable.Text.Split('|')[0]).OrderBy(x => x.Sort);

        // 初始化一个异步任务管理器，传入缓存全部字段配置的委托
        if (_list == null || !_list.Any()) return;
        var tm = new TaskManager(x =>
        {
            if (x is not List<FieldControlConfig> fieldList || fieldList.Count == 0) return;
            EnumerableExtension.ForEach(fieldList.Select(s=>s.TableName).Distinct(), e =>
            {
                List<FieldControlConfig> fileList = fieldList.Where(w => w.TableName == e).ToList();
                if (!fileList.Any()) return;

                // 序列化 字段配置
                string fileText = JsonHelper.Serialize(fileList);
                if (fileText.Length == 0) return;

                // 是否存在文件夹
                DirectoryUtil.CreateDirectory(_dataPath);

                // 写文件
                var filePath = $"{_dataPath}{e}.json";
                FileUtil.CreateFile(filePath);
                FileUtil.WriteText(filePath, fileText);
            });
        }, "AddFieldControlConfigList");

        // 结束前执行异步任务
        tm.Append(_list);
    }

    private async void txtTable_EditValueChanged(object sender, EventArgs e)
    {
        var tempTable = txtTable.Text.Split('|')[0].Split('_');
        txtName.Text = tempTable.Length > 1 ? tempTable[1] : txtTable.Text.Split('|')[0];
        txtDisplay.Text = txtTable.Text.Split('|')[1];
        
        if (txtChildTable.Text.Length != 0)
        {
            IEnumerable<string> keys = await _fieldControlConfigHttpService.GetTableKeyList(txtTable.Text.Split('|')[0]);
            txtForeignKeyName.Text = keys.FirstOrDefault();
        }
    }

    private async void txtChildTable_EditValueChanged(object sender, EventArgs e)
    {
        txtChildName.ResetText();
        txtChildDisplay.ResetText();
        txtForeignKeyName.ResetText();
        var data = txtChildTable.Properties.Items.Where(p=>p.CheckState==CheckState.Checked).ToList();
        if(data.Count == 0) return;
        var tempTable = data[0].Value.ToString().Split('|')[0].Split('_');
        txtChildName.Text = tempTable.Length > 1 ? tempTable[1] : data[0].Value.ToString();
        txtChildDisplay.Text = data[0].Value.ToString().Split('|')[1];
        if (txtTable.Text.Length != 0)
        {
            IEnumerable<string> keys = await _fieldControlConfigHttpService.GetTableKeyList(txtTable.Text.Split('|')[0]);
            txtForeignKeyName.Text = keys.FirstOrDefault();
        }
        // if (data.Count > 0)
        // {
        //     List<FieldControlConfig> list = new List<FieldControlConfig>();
        //     data.ForEach(x =>
        //         list.AddRange(
        //             _bll.GetFieldControlConfigs(x.Value.ToString())));
        //     gridControl1.DataSource = list;
        // }
        // else
        // {
        //     "请选择至少一张表！".ShowWarning();
        // }
    }
        
    private void simpleButton1_Click(object sender, EventArgs e)
    {
        textEdit2.Text = FileDialogHelper.OpenDir(AppDomain.CurrentDomain.BaseDirectory);
    }
}