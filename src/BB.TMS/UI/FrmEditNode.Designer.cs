namespace BB.TMS.UI
{
    partial class FrmEditNode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditNode));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtTranNodeNO = new DevExpress.XtraEditors.TextEdit();
            this.txtTranNodeCostNo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtTranNodeName = new DevExpress.XtraEditors.TextEdit();
            this.txtTranNodeType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtTranNodeBeginDate = new DevExpress.XtraEditors.DateEdit();
            this.txtTranNodeEndDate = new DevExpress.XtraEditors.DateEdit();
            this.txtParentNo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtTranNodePerson = new DevExpress.XtraEditors.TextEdit();
            this.txtTranNodePersonID = new DevExpress.XtraEditors.TextEdit();
            this.txtTranNodeMobile = new DevExpress.XtraEditors.TextEdit();
            this.txtLockLimit = new DevExpress.XtraEditors.ToggleSwitch();
            this.txtLockLimitAmt = new DevExpress.XtraEditors.SpinEdit();
            this.txtWarningLimitAmt = new DevExpress.XtraEditors.SpinEdit();
            this.txtSendSMS = new DevExpress.XtraEditors.ToggleSwitch();
            this.txtISLocked = new DevExpress.XtraEditors.ToggleSwitch();
            this.txtAckRec = new DevExpress.XtraEditors.ToggleSwitch();
            this.txtAgencyRecLimitAmt = new DevExpress.XtraEditors.SpinEdit();
            this.txtCarriageForwardLimitAmt = new DevExpress.XtraEditors.SpinEdit();
            this.txtAreaNo = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gvAreaNo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtInTime = new DevExpress.XtraEditors.TimeEdit();
            this.txtOutTime = new DevExpress.XtraEditors.TimeEdit();
            this.txtRemark = new DevExpress.XtraEditors.TextEdit();
            this.txtCreationDate = new DevExpress.XtraEditors.DateEdit();
            this.txtCreatedBy = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtLastUpdateDate = new DevExpress.XtraEditors.DateEdit();
            this.txtLastUpdatedBy = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtTranNodeStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtPublicYN = new DevExpress.XtraEditors.ToggleSwitch();
            this.txtFlagApp = new DevExpress.XtraEditors.ToggleSwitch();
            this.txtAppUser = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtAppDate = new DevExpress.XtraEditors.DateEdit();
            this.txtSignLoopEndTime = new DevExpress.XtraEditors.TimeEdit();
            this.txtSignLimitTime = new DevExpress.XtraEditors.TimeEdit();
            this.txtSignDays = new DevExpress.XtraEditors.TextEdit();
            this.txtAckRecDays = new DevExpress.XtraEditors.TextEdit();
            this.txtCostMasterYN = new DevExpress.XtraEditors.ToggleSwitch();
            this.txtManagementFee = new DevExpress.XtraEditors.SpinEdit();
            this.txtUsageFee = new DevExpress.XtraEditors.SpinEdit();
            this.txtDeposit = new DevExpress.XtraEditors.SpinEdit();
            this.txtContractNote = new DevExpress.XtraEditors.TextEdit();
            this.txtDispatchOnly = new DevExpress.XtraEditors.ToggleSwitch();
            this.txtPickupWeightLimit = new DevExpress.XtraEditors.SpinEdit();
            this.txtPickupVolumeLimit = new DevExpress.XtraEditors.SpinEdit();
            this.txtTranNodeAxes = new DevExpress.XtraEditors.TextEdit();
            this.txtIsLockLimitKPI = new DevExpress.XtraEditors.ToggleSwitch();
            this.txtFinancialCenter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtWhiteList = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtBlackList = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtTranNodeAddress = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutTranNodeNO = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTranNodeAddress = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutLockLimitAmt = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutWarningLimitAmt = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutInTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutSignDays = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutAckRecDays = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutManagementFee = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutUsageFee = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTranNodeName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutWhiteList = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTranNodeType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutRemark = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutContractNote = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTranNodeBeginDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTranNodeEndDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTranNodePerson = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTranNodePersonID = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTranNodeMobile = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutAreaNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutAgencyRecLimitAmt = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCarriageForwardLimitAmt = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutOutTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutSignLoopEndTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutSignLimitTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutPickupWeightLimit = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutPickupVolumeLimit = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutDeposit = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCostMasterYN = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutParentNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTranNodeCostNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutFinancialCenter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTranNodeStatus = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTranNodeAxes = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutAppUser = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutAppDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutIsLockLimitKPI = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCreatedBy = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCreationDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutLastUpdatedBy = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutLastUpdateDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutLockLimit = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutISLocked = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutFlagApp = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutDispatchOnly = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutAckRec = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutSendSMS = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutPublicYN = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutBlackList = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeCostNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeBeginDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeBeginDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParentNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodePerson.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodePersonID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeMobile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLockLimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLockLimitAmt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarningLimitAmt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendSMS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtISLocked.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAckRec.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgencyRecLimitAmt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarriageForwardLimitAmt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAreaNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAreaNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdateDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdatedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPublicYN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlagApp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAppUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAppDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAppDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSignLoopEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSignLimitTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSignDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAckRecDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostMasterYN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManagementFee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsageFee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeposit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDispatchOnly.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPickupWeightLimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPickupVolumeLimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeAxes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIsLockLimitKPI.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinancialCenter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWhiteList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBlackList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLockLimitAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWarningLimitAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutInTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSignDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAckRecDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutManagementFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutUsageFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWhiteList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutContractNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeBeginDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodePerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodePersonID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeMobile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAreaNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAgencyRecLimitAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCarriageForwardLimitAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutOutTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSignLoopEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSignLimitTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutPickupWeightLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutPickupVolumeLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDeposit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCostMasterYN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutParentNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeCostNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFinancialCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeAxes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAppUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAppDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutIsLockLimitKPI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCreatedBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCreationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLastUpdatedBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLastUpdateDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLockLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutISLocked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFlagApp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDispatchOnly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAckRec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSendSMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutPublicYN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBlackList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.ImageOptions.Image")));
            this.btnOK.Location = new System.Drawing.Point(848, 863);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(942, 863);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(754, 863);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(10, 867);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(207, 868);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.txtTranNodeNO);
            this.layoutControl1.Controls.Add(this.txtTranNodeCostNo);
            this.layoutControl1.Controls.Add(this.txtTranNodeName);
            this.layoutControl1.Controls.Add(this.txtTranNodeType);
            this.layoutControl1.Controls.Add(this.txtTranNodeBeginDate);
            this.layoutControl1.Controls.Add(this.txtTranNodeEndDate);
            this.layoutControl1.Controls.Add(this.txtParentNo);
            this.layoutControl1.Controls.Add(this.txtTranNodePerson);
            this.layoutControl1.Controls.Add(this.txtTranNodePersonID);
            this.layoutControl1.Controls.Add(this.txtTranNodeMobile);
            this.layoutControl1.Controls.Add(this.txtLockLimit);
            this.layoutControl1.Controls.Add(this.txtLockLimitAmt);
            this.layoutControl1.Controls.Add(this.txtWarningLimitAmt);
            this.layoutControl1.Controls.Add(this.txtSendSMS);
            this.layoutControl1.Controls.Add(this.txtISLocked);
            this.layoutControl1.Controls.Add(this.txtAckRec);
            this.layoutControl1.Controls.Add(this.txtAgencyRecLimitAmt);
            this.layoutControl1.Controls.Add(this.txtCarriageForwardLimitAmt);
            this.layoutControl1.Controls.Add(this.txtAreaNo);
            this.layoutControl1.Controls.Add(this.txtInTime);
            this.layoutControl1.Controls.Add(this.txtOutTime);
            this.layoutControl1.Controls.Add(this.txtRemark);
            this.layoutControl1.Controls.Add(this.txtCreationDate);
            this.layoutControl1.Controls.Add(this.txtCreatedBy);
            this.layoutControl1.Controls.Add(this.txtLastUpdateDate);
            this.layoutControl1.Controls.Add(this.txtLastUpdatedBy);
            this.layoutControl1.Controls.Add(this.txtTranNodeStatus);
            this.layoutControl1.Controls.Add(this.txtPublicYN);
            this.layoutControl1.Controls.Add(this.txtFlagApp);
            this.layoutControl1.Controls.Add(this.txtAppUser);
            this.layoutControl1.Controls.Add(this.txtAppDate);
            this.layoutControl1.Controls.Add(this.txtSignLoopEndTime);
            this.layoutControl1.Controls.Add(this.txtSignLimitTime);
            this.layoutControl1.Controls.Add(this.txtSignDays);
            this.layoutControl1.Controls.Add(this.txtAckRecDays);
            this.layoutControl1.Controls.Add(this.txtCostMasterYN);
            this.layoutControl1.Controls.Add(this.txtManagementFee);
            this.layoutControl1.Controls.Add(this.txtUsageFee);
            this.layoutControl1.Controls.Add(this.txtDeposit);
            this.layoutControl1.Controls.Add(this.txtContractNote);
            this.layoutControl1.Controls.Add(this.txtDispatchOnly);
            this.layoutControl1.Controls.Add(this.txtPickupWeightLimit);
            this.layoutControl1.Controls.Add(this.txtPickupVolumeLimit);
            this.layoutControl1.Controls.Add(this.txtTranNodeAxes);
            this.layoutControl1.Controls.Add(this.txtIsLockLimitKPI);
            this.layoutControl1.Controls.Add(this.txtFinancialCenter);
            this.layoutControl1.Controls.Add(this.txtWhiteList);
            this.layoutControl1.Controls.Add(this.txtBlackList);
            this.layoutControl1.Controls.Add(this.txtTranNodeAddress);
            this.layoutControl1.Location = new System.Drawing.Point(10, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1022, 380);
            this.layoutControl1.TabIndex = 200;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtTranNodeNO
            // 
            this.txtTranNodeNO.Location = new System.Drawing.Point(126, 12);
            this.txtTranNodeNO.Name = "txtTranNodeNO";
            this.txtTranNodeNO.Size = new System.Drawing.Size(132, 20);
            this.txtTranNodeNO.StyleController = this.layoutControl1;
            this.txtTranNodeNO.TabIndex = 0;
            // 
            // txtTranNodeCostNo
            // 
            this.txtTranNodeCostNo.Location = new System.Drawing.Point(627, 36);
            this.txtTranNodeCostNo.Name = "txtTranNodeCostNo";
            this.txtTranNodeCostNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtTranNodeCostNo.Size = new System.Drawing.Size(132, 20);
            this.txtTranNodeCostNo.StyleController = this.layoutControl1;
            this.txtTranNodeCostNo.TabIndex = 1;
            // 
            // txtTranNodeName
            // 
            this.txtTranNodeName.Location = new System.Drawing.Point(376, 12);
            this.txtTranNodeName.Name = "txtTranNodeName";
            this.txtTranNodeName.Size = new System.Drawing.Size(133, 20);
            this.txtTranNodeName.StyleController = this.layoutControl1;
            this.txtTranNodeName.TabIndex = 2;
            // 
            // txtTranNodeType
            // 
            this.txtTranNodeType.Location = new System.Drawing.Point(627, 12);
            this.txtTranNodeType.Name = "txtTranNodeType";
            this.txtTranNodeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtTranNodeType.Size = new System.Drawing.Size(132, 20);
            this.txtTranNodeType.StyleController = this.layoutControl1;
            this.txtTranNodeType.TabIndex = 3;
            // 
            // txtTranNodeBeginDate
            // 
            this.txtTranNodeBeginDate.EditValue = null;
            this.txtTranNodeBeginDate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtTranNodeBeginDate.Location = new System.Drawing.Point(126, 36);
            this.txtTranNodeBeginDate.Name = "txtTranNodeBeginDate";
            this.txtTranNodeBeginDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtTranNodeBeginDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.txtTranNodeBeginDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtTranNodeBeginDate.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "HH:mm";
            this.txtTranNodeBeginDate.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTranNodeBeginDate.Properties.CalendarTimeProperties.EditFormat.FormatString = "HH:mm";
            this.txtTranNodeBeginDate.Properties.CalendarTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTranNodeBeginDate.Properties.CalendarTimeProperties.Mask.EditMask = "HH:mm";
            this.txtTranNodeBeginDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.txtTranNodeBeginDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtTranNodeBeginDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTranNodeBeginDate.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtTranNodeBeginDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTranNodeBeginDate.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.txtTranNodeBeginDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.txtTranNodeBeginDate.Size = new System.Drawing.Size(132, 20);
            this.txtTranNodeBeginDate.StyleController = this.layoutControl1;
            this.txtTranNodeBeginDate.TabIndex = 4;
            // 
            // txtTranNodeEndDate
            // 
            this.txtTranNodeEndDate.EditValue = null;
            this.txtTranNodeEndDate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtTranNodeEndDate.Location = new System.Drawing.Point(376, 36);
            this.txtTranNodeEndDate.Name = "txtTranNodeEndDate";
            this.txtTranNodeEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtTranNodeEndDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.txtTranNodeEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtTranNodeEndDate.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "HH:mm";
            this.txtTranNodeEndDate.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTranNodeEndDate.Properties.CalendarTimeProperties.EditFormat.FormatString = "HH:mm";
            this.txtTranNodeEndDate.Properties.CalendarTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTranNodeEndDate.Properties.CalendarTimeProperties.Mask.EditMask = "HH:mm";
            this.txtTranNodeEndDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.txtTranNodeEndDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtTranNodeEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTranNodeEndDate.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtTranNodeEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtTranNodeEndDate.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.txtTranNodeEndDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.txtTranNodeEndDate.Size = new System.Drawing.Size(133, 20);
            this.txtTranNodeEndDate.StyleController = this.layoutControl1;
            this.txtTranNodeEndDate.TabIndex = 5;
            // 
            // txtParentNo
            // 
            this.txtParentNo.Location = new System.Drawing.Point(877, 12);
            this.txtParentNo.Name = "txtParentNo";
            this.txtParentNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtParentNo.Size = new System.Drawing.Size(133, 20);
            this.txtParentNo.StyleController = this.layoutControl1;
            this.txtParentNo.TabIndex = 6;
            // 
            // txtTranNodePerson
            // 
            this.txtTranNodePerson.Location = new System.Drawing.Point(126, 60);
            this.txtTranNodePerson.Name = "txtTranNodePerson";
            this.txtTranNodePerson.Size = new System.Drawing.Size(132, 20);
            this.txtTranNodePerson.StyleController = this.layoutControl1;
            this.txtTranNodePerson.TabIndex = 7;
            // 
            // txtTranNodePersonID
            // 
            this.txtTranNodePersonID.Location = new System.Drawing.Point(376, 60);
            this.txtTranNodePersonID.Name = "txtTranNodePersonID";
            this.txtTranNodePersonID.Size = new System.Drawing.Size(133, 20);
            this.txtTranNodePersonID.StyleController = this.layoutControl1;
            this.txtTranNodePersonID.TabIndex = 8;
            // 
            // txtTranNodeMobile
            // 
            this.txtTranNodeMobile.Location = new System.Drawing.Point(627, 60);
            this.txtTranNodeMobile.Name = "txtTranNodeMobile";
            this.txtTranNodeMobile.Size = new System.Drawing.Size(132, 20);
            this.txtTranNodeMobile.StyleController = this.layoutControl1;
            this.txtTranNodeMobile.TabIndex = 9;
            // 
            // txtLockLimit
            // 
            this.txtLockLimit.Location = new System.Drawing.Point(627, 204);
            this.txtLockLimit.Name = "txtLockLimit";
            this.txtLockLimit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.txtLockLimit.Properties.Appearance.Options.UseFont = true;
            this.txtLockLimit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtLockLimit.Properties.OffText = "Off";
            this.txtLockLimit.Properties.OnText = "On";
            this.txtLockLimit.Size = new System.Drawing.Size(132, 20);
            this.txtLockLimit.StyleController = this.layoutControl1;
            this.txtLockLimit.TabIndex = 11;
            // 
            // txtLockLimitAmt
            // 
            this.txtLockLimitAmt.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.txtLockLimitAmt.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtLockLimitAmt.Location = new System.Drawing.Point(126, 108);
            this.txtLockLimitAmt.Name = "txtLockLimitAmt";
            this.txtLockLimitAmt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtLockLimitAmt.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtLockLimitAmt.Size = new System.Drawing.Size(132, 20);
            this.txtLockLimitAmt.StyleController = this.layoutControl1;
            this.txtLockLimitAmt.TabIndex = 12;
            // 
            // txtWarningLimitAmt
            // 
            this.txtWarningLimitAmt.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.txtWarningLimitAmt.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtWarningLimitAmt.Location = new System.Drawing.Point(376, 108);
            this.txtWarningLimitAmt.Name = "txtWarningLimitAmt";
            this.txtWarningLimitAmt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtWarningLimitAmt.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtWarningLimitAmt.Size = new System.Drawing.Size(133, 20);
            this.txtWarningLimitAmt.StyleController = this.layoutControl1;
            this.txtWarningLimitAmt.TabIndex = 13;
            // 
            // txtSendSMS
            // 
            this.txtSendSMS.Location = new System.Drawing.Point(877, 276);
            this.txtSendSMS.Name = "txtSendSMS";
            this.txtSendSMS.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.txtSendSMS.Properties.Appearance.Options.UseFont = true;
            this.txtSendSMS.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtSendSMS.Properties.OffText = "Off";
            this.txtSendSMS.Properties.OnText = "On";
            this.txtSendSMS.Size = new System.Drawing.Size(133, 20);
            this.txtSendSMS.StyleController = this.layoutControl1;
            this.txtSendSMS.TabIndex = 14;
            // 
            // txtISLocked
            // 
            this.txtISLocked.Location = new System.Drawing.Point(877, 204);
            this.txtISLocked.Name = "txtISLocked";
            this.txtISLocked.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.txtISLocked.Properties.Appearance.Options.UseFont = true;
            this.txtISLocked.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtISLocked.Properties.OffText = "Off";
            this.txtISLocked.Properties.OnText = "On";
            this.txtISLocked.Size = new System.Drawing.Size(133, 20);
            this.txtISLocked.StyleController = this.layoutControl1;
            this.txtISLocked.TabIndex = 15;
            // 
            // txtAckRec
            // 
            this.txtAckRec.Location = new System.Drawing.Point(877, 252);
            this.txtAckRec.Name = "txtAckRec";
            this.txtAckRec.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.txtAckRec.Properties.Appearance.Options.UseFont = true;
            this.txtAckRec.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtAckRec.Properties.OffText = "Off";
            this.txtAckRec.Properties.OnText = "On";
            this.txtAckRec.Size = new System.Drawing.Size(133, 20);
            this.txtAckRec.StyleController = this.layoutControl1;
            this.txtAckRec.TabIndex = 16;
            // 
            // txtAgencyRecLimitAmt
            // 
            this.txtAgencyRecLimitAmt.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.txtAgencyRecLimitAmt.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtAgencyRecLimitAmt.Location = new System.Drawing.Point(627, 108);
            this.txtAgencyRecLimitAmt.Name = "txtAgencyRecLimitAmt";
            this.txtAgencyRecLimitAmt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtAgencyRecLimitAmt.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtAgencyRecLimitAmt.Size = new System.Drawing.Size(132, 20);
            this.txtAgencyRecLimitAmt.StyleController = this.layoutControl1;
            this.txtAgencyRecLimitAmt.TabIndex = 17;
            // 
            // txtCarriageForwardLimitAmt
            // 
            this.txtCarriageForwardLimitAmt.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.txtCarriageForwardLimitAmt.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCarriageForwardLimitAmt.Location = new System.Drawing.Point(877, 108);
            this.txtCarriageForwardLimitAmt.Name = "txtCarriageForwardLimitAmt";
            this.txtCarriageForwardLimitAmt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtCarriageForwardLimitAmt.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtCarriageForwardLimitAmt.Size = new System.Drawing.Size(133, 20);
            this.txtCarriageForwardLimitAmt.StyleController = this.layoutControl1;
            this.txtCarriageForwardLimitAmt.TabIndex = 18;
            // 
            // txtAreaNo
            // 
            this.txtAreaNo.Location = new System.Drawing.Point(126, 84);
            this.txtAreaNo.Name = "txtAreaNo";
            this.txtAreaNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtAreaNo.Properties.NullText = "";
            this.txtAreaNo.Properties.PopupSizeable = false;
            this.txtAreaNo.Properties.PopupView = this.gvAreaNo;
            this.txtAreaNo.Size = new System.Drawing.Size(132, 20);
            this.txtAreaNo.StyleController = this.layoutControl1;
            this.txtAreaNo.TabIndex = 19;
            // 
            // gvAreaNo
            // 
            this.gvAreaNo.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvAreaNo.Name = "gvAreaNo";
            this.gvAreaNo.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAreaNo.OptionsView.ShowGroupPanel = false;
            // 
            // txtInTime
            // 
            this.txtInTime.EditValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.txtInTime.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtInTime.Location = new System.Drawing.Point(126, 132);
            this.txtInTime.Name = "txtInTime";
            this.txtInTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtInTime.Properties.DisplayFormat.FormatString = "HH:mm";
            this.txtInTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtInTime.Properties.EditFormat.FormatString = "HH:mm";
            this.txtInTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtInTime.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtInTime.Properties.Mask.EditMask = "HH:mm";
            this.txtInTime.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            this.txtInTime.Properties.TouchUIMaxValue = new System.DateTime(1900, 1, 1, 23, 59, 0, 0);
            this.txtInTime.Properties.TouchUIMinValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.txtInTime.Size = new System.Drawing.Size(132, 20);
            this.txtInTime.StyleController = this.layoutControl1;
            this.txtInTime.TabIndex = 20;
            // 
            // txtOutTime
            // 
            this.txtOutTime.EditValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.txtOutTime.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtOutTime.Location = new System.Drawing.Point(376, 132);
            this.txtOutTime.Name = "txtOutTime";
            this.txtOutTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtOutTime.Properties.DisplayFormat.FormatString = "HH:mm";
            this.txtOutTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtOutTime.Properties.EditFormat.FormatString = "HH:mm";
            this.txtOutTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtOutTime.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtOutTime.Properties.Mask.EditMask = "HH:mm";
            this.txtOutTime.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            this.txtOutTime.Properties.TouchUIMaxValue = new System.DateTime(1900, 1, 1, 23, 59, 0, 0);
            this.txtOutTime.Properties.TouchUIMinValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.txtOutTime.Size = new System.Drawing.Size(133, 20);
            this.txtOutTime.StyleController = this.layoutControl1;
            this.txtOutTime.TabIndex = 21;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(126, 252);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(633, 20);
            this.txtRemark.StyleController = this.layoutControl1;
            this.txtRemark.TabIndex = 22;
            // 
            // txtCreationDate
            // 
            this.txtCreationDate.EditValue = null;
            this.txtCreationDate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCreationDate.Location = new System.Drawing.Point(376, 324);
            this.txtCreationDate.Name = "txtCreationDate";
            this.txtCreationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtCreationDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.txtCreationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtCreationDate.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "HH:mm";
            this.txtCreationDate.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate.Properties.CalendarTimeProperties.EditFormat.FormatString = "HH:mm";
            this.txtCreationDate.Properties.CalendarTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate.Properties.CalendarTimeProperties.Mask.EditMask = "HH:mm";
            this.txtCreationDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.txtCreationDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtCreationDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtCreationDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreationDate.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.txtCreationDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.txtCreationDate.Size = new System.Drawing.Size(133, 20);
            this.txtCreationDate.StyleController = this.layoutControl1;
            this.txtCreationDate.TabIndex = 23;
            // 
            // txtCreatedBy
            // 
            this.txtCreatedBy.Location = new System.Drawing.Point(126, 324);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtCreatedBy.Size = new System.Drawing.Size(132, 20);
            this.txtCreatedBy.StyleController = this.layoutControl1;
            this.txtCreatedBy.TabIndex = 24;
            // 
            // txtLastUpdateDate
            // 
            this.txtLastUpdateDate.EditValue = null;
            this.txtLastUpdateDate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtLastUpdateDate.Location = new System.Drawing.Point(877, 324);
            this.txtLastUpdateDate.Name = "txtLastUpdateDate";
            this.txtLastUpdateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtLastUpdateDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.txtLastUpdateDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtLastUpdateDate.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "HH:mm";
            this.txtLastUpdateDate.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtLastUpdateDate.Properties.CalendarTimeProperties.EditFormat.FormatString = "HH:mm";
            this.txtLastUpdateDate.Properties.CalendarTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtLastUpdateDate.Properties.CalendarTimeProperties.Mask.EditMask = "HH:mm";
            this.txtLastUpdateDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.txtLastUpdateDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtLastUpdateDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtLastUpdateDate.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtLastUpdateDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtLastUpdateDate.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.txtLastUpdateDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.txtLastUpdateDate.Size = new System.Drawing.Size(133, 20);
            this.txtLastUpdateDate.StyleController = this.layoutControl1;
            this.txtLastUpdateDate.TabIndex = 25;
            // 
            // txtLastUpdatedBy
            // 
            this.txtLastUpdatedBy.Location = new System.Drawing.Point(627, 324);
            this.txtLastUpdatedBy.Name = "txtLastUpdatedBy";
            this.txtLastUpdatedBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtLastUpdatedBy.Size = new System.Drawing.Size(132, 20);
            this.txtLastUpdatedBy.StyleController = this.layoutControl1;
            this.txtLastUpdatedBy.TabIndex = 26;
            // 
            // txtTranNodeStatus
            // 
            this.txtTranNodeStatus.Location = new System.Drawing.Point(877, 60);
            this.txtTranNodeStatus.Name = "txtTranNodeStatus";
            this.txtTranNodeStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtTranNodeStatus.Size = new System.Drawing.Size(133, 20);
            this.txtTranNodeStatus.StyleController = this.layoutControl1;
            this.txtTranNodeStatus.TabIndex = 27;
            // 
            // txtPublicYN
            // 
            this.txtPublicYN.Location = new System.Drawing.Point(877, 180);
            this.txtPublicYN.Name = "txtPublicYN";
            this.txtPublicYN.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.txtPublicYN.Properties.Appearance.Options.UseFont = true;
            this.txtPublicYN.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtPublicYN.Properties.OffText = "Off";
            this.txtPublicYN.Properties.OnText = "On";
            this.txtPublicYN.Size = new System.Drawing.Size(133, 20);
            this.txtPublicYN.StyleController = this.layoutControl1;
            this.txtPublicYN.TabIndex = 28;
            // 
            // txtFlagApp
            // 
            this.txtFlagApp.Location = new System.Drawing.Point(877, 348);
            this.txtFlagApp.Name = "txtFlagApp";
            this.txtFlagApp.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.txtFlagApp.Properties.Appearance.Options.UseFont = true;
            this.txtFlagApp.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtFlagApp.Properties.OffText = "Off";
            this.txtFlagApp.Properties.OnText = "On";
            this.txtFlagApp.Size = new System.Drawing.Size(133, 20);
            this.txtFlagApp.StyleController = this.layoutControl1;
            this.txtFlagApp.TabIndex = 29;
            // 
            // txtAppUser
            // 
            this.txtAppUser.Location = new System.Drawing.Point(376, 348);
            this.txtAppUser.Name = "txtAppUser";
            this.txtAppUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtAppUser.Size = new System.Drawing.Size(133, 20);
            this.txtAppUser.StyleController = this.layoutControl1;
            this.txtAppUser.TabIndex = 30;
            // 
            // txtAppDate
            // 
            this.txtAppDate.EditValue = null;
            this.txtAppDate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtAppDate.Location = new System.Drawing.Point(627, 348);
            this.txtAppDate.Name = "txtAppDate";
            this.txtAppDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtAppDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.txtAppDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtAppDate.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "HH:mm";
            this.txtAppDate.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtAppDate.Properties.CalendarTimeProperties.EditFormat.FormatString = "HH:mm";
            this.txtAppDate.Properties.CalendarTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtAppDate.Properties.CalendarTimeProperties.Mask.EditMask = "HH:mm";
            this.txtAppDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.txtAppDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtAppDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtAppDate.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.txtAppDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtAppDate.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.txtAppDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.txtAppDate.Size = new System.Drawing.Size(132, 20);
            this.txtAppDate.StyleController = this.layoutControl1;
            this.txtAppDate.TabIndex = 31;
            // 
            // txtSignLoopEndTime
            // 
            this.txtSignLoopEndTime.EditValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.txtSignLoopEndTime.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSignLoopEndTime.Location = new System.Drawing.Point(627, 132);
            this.txtSignLoopEndTime.Name = "txtSignLoopEndTime";
            this.txtSignLoopEndTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtSignLoopEndTime.Properties.DisplayFormat.FormatString = "HH:mm";
            this.txtSignLoopEndTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSignLoopEndTime.Properties.EditFormat.FormatString = "HH:mm";
            this.txtSignLoopEndTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSignLoopEndTime.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtSignLoopEndTime.Properties.Mask.EditMask = "HH:mm";
            this.txtSignLoopEndTime.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            this.txtSignLoopEndTime.Properties.TouchUIMaxValue = new System.DateTime(1900, 1, 1, 23, 59, 0, 0);
            this.txtSignLoopEndTime.Properties.TouchUIMinValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.txtSignLoopEndTime.Size = new System.Drawing.Size(132, 20);
            this.txtSignLoopEndTime.StyleController = this.layoutControl1;
            this.txtSignLoopEndTime.TabIndex = 32;
            // 
            // txtSignLimitTime
            // 
            this.txtSignLimitTime.EditValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.txtSignLimitTime.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSignLimitTime.Location = new System.Drawing.Point(877, 132);
            this.txtSignLimitTime.Name = "txtSignLimitTime";
            this.txtSignLimitTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtSignLimitTime.Properties.DisplayFormat.FormatString = "HH:mm";
            this.txtSignLimitTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSignLimitTime.Properties.EditFormat.FormatString = "HH:mm";
            this.txtSignLimitTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSignLimitTime.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtSignLimitTime.Properties.Mask.EditMask = "HH:mm";
            this.txtSignLimitTime.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            this.txtSignLimitTime.Properties.TouchUIMaxValue = new System.DateTime(1900, 1, 1, 23, 59, 0, 0);
            this.txtSignLimitTime.Properties.TouchUIMinValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.txtSignLimitTime.Size = new System.Drawing.Size(133, 20);
            this.txtSignLimitTime.StyleController = this.layoutControl1;
            this.txtSignLimitTime.TabIndex = 33;
            // 
            // txtSignDays
            // 
            this.txtSignDays.Location = new System.Drawing.Point(627, 156);
            this.txtSignDays.Name = "txtSignDays";
            this.txtSignDays.Size = new System.Drawing.Size(132, 20);
            this.txtSignDays.StyleController = this.layoutControl1;
            this.txtSignDays.TabIndex = 34;
            // 
            // txtAckRecDays
            // 
            this.txtAckRecDays.Location = new System.Drawing.Point(877, 156);
            this.txtAckRecDays.Name = "txtAckRecDays";
            this.txtAckRecDays.Size = new System.Drawing.Size(133, 20);
            this.txtAckRecDays.StyleController = this.layoutControl1;
            this.txtAckRecDays.TabIndex = 35;
            // 
            // txtCostMasterYN
            // 
            this.txtCostMasterYN.Location = new System.Drawing.Point(376, 204);
            this.txtCostMasterYN.Name = "txtCostMasterYN";
            this.txtCostMasterYN.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.txtCostMasterYN.Properties.Appearance.Options.UseFont = true;
            this.txtCostMasterYN.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtCostMasterYN.Properties.OffText = "Off";
            this.txtCostMasterYN.Properties.OnText = "On";
            this.txtCostMasterYN.Size = new System.Drawing.Size(133, 20);
            this.txtCostMasterYN.StyleController = this.layoutControl1;
            this.txtCostMasterYN.TabIndex = 36;
            // 
            // txtManagementFee
            // 
            this.txtManagementFee.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.txtManagementFee.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtManagementFee.Location = new System.Drawing.Point(126, 180);
            this.txtManagementFee.Name = "txtManagementFee";
            this.txtManagementFee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtManagementFee.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtManagementFee.Size = new System.Drawing.Size(132, 20);
            this.txtManagementFee.StyleController = this.layoutControl1;
            this.txtManagementFee.TabIndex = 37;
            // 
            // txtUsageFee
            // 
            this.txtUsageFee.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.txtUsageFee.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtUsageFee.Location = new System.Drawing.Point(376, 180);
            this.txtUsageFee.Name = "txtUsageFee";
            this.txtUsageFee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtUsageFee.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtUsageFee.Size = new System.Drawing.Size(133, 20);
            this.txtUsageFee.StyleController = this.layoutControl1;
            this.txtUsageFee.TabIndex = 38;
            // 
            // txtDeposit
            // 
            this.txtDeposit.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.txtDeposit.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtDeposit.Location = new System.Drawing.Point(627, 180);
            this.txtDeposit.Name = "txtDeposit";
            this.txtDeposit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtDeposit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtDeposit.Size = new System.Drawing.Size(132, 20);
            this.txtDeposit.StyleController = this.layoutControl1;
            this.txtDeposit.TabIndex = 39;
            // 
            // txtContractNote
            // 
            this.txtContractNote.Location = new System.Drawing.Point(126, 228);
            this.txtContractNote.Name = "txtContractNote";
            this.txtContractNote.Size = new System.Drawing.Size(633, 20);
            this.txtContractNote.StyleController = this.layoutControl1;
            this.txtContractNote.TabIndex = 40;
            // 
            // txtDispatchOnly
            // 
            this.txtDispatchOnly.Location = new System.Drawing.Point(877, 228);
            this.txtDispatchOnly.Name = "txtDispatchOnly";
            this.txtDispatchOnly.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.txtDispatchOnly.Properties.Appearance.Options.UseFont = true;
            this.txtDispatchOnly.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtDispatchOnly.Properties.OffText = "Off";
            this.txtDispatchOnly.Properties.OnText = "On";
            this.txtDispatchOnly.Size = new System.Drawing.Size(133, 20);
            this.txtDispatchOnly.StyleController = this.layoutControl1;
            this.txtDispatchOnly.TabIndex = 41;
            // 
            // txtPickupWeightLimit
            // 
            this.txtPickupWeightLimit.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.txtPickupWeightLimit.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtPickupWeightLimit.Location = new System.Drawing.Point(126, 156);
            this.txtPickupWeightLimit.Name = "txtPickupWeightLimit";
            this.txtPickupWeightLimit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtPickupWeightLimit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtPickupWeightLimit.Size = new System.Drawing.Size(132, 20);
            this.txtPickupWeightLimit.StyleController = this.layoutControl1;
            this.txtPickupWeightLimit.TabIndex = 42;
            // 
            // txtPickupVolumeLimit
            // 
            this.txtPickupVolumeLimit.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.txtPickupVolumeLimit.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtPickupVolumeLimit.Location = new System.Drawing.Point(376, 156);
            this.txtPickupVolumeLimit.Name = "txtPickupVolumeLimit";
            this.txtPickupVolumeLimit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            this.txtPickupVolumeLimit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtPickupVolumeLimit.Size = new System.Drawing.Size(133, 20);
            this.txtPickupVolumeLimit.StyleController = this.layoutControl1;
            this.txtPickupVolumeLimit.TabIndex = 43;
            // 
            // txtTranNodeAxes
            // 
            this.txtTranNodeAxes.Location = new System.Drawing.Point(126, 348);
            this.txtTranNodeAxes.Name = "txtTranNodeAxes";
            this.txtTranNodeAxes.Size = new System.Drawing.Size(132, 20);
            this.txtTranNodeAxes.StyleController = this.layoutControl1;
            this.txtTranNodeAxes.TabIndex = 44;
            // 
            // txtIsLockLimitKPI
            // 
            this.txtIsLockLimitKPI.Location = new System.Drawing.Point(126, 204);
            this.txtIsLockLimitKPI.Name = "txtIsLockLimitKPI";
            this.txtIsLockLimitKPI.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.txtIsLockLimitKPI.Properties.Appearance.Options.UseFont = true;
            this.txtIsLockLimitKPI.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtIsLockLimitKPI.Properties.OffText = "Off";
            this.txtIsLockLimitKPI.Properties.OnText = "On";
            this.txtIsLockLimitKPI.Size = new System.Drawing.Size(132, 20);
            this.txtIsLockLimitKPI.StyleController = this.layoutControl1;
            this.txtIsLockLimitKPI.TabIndex = 45;
            // 
            // txtFinancialCenter
            // 
            this.txtFinancialCenter.Location = new System.Drawing.Point(877, 36);
            this.txtFinancialCenter.Name = "txtFinancialCenter";
            this.txtFinancialCenter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtFinancialCenter.Size = new System.Drawing.Size(133, 20);
            this.txtFinancialCenter.StyleController = this.layoutControl1;
            this.txtFinancialCenter.TabIndex = 46;
            // 
            // txtWhiteList
            // 
            this.txtWhiteList.Location = new System.Drawing.Point(126, 276);
            this.txtWhiteList.Name = "txtWhiteList";
            this.txtWhiteList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtWhiteList.Size = new System.Drawing.Size(633, 20);
            this.txtWhiteList.StyleController = this.layoutControl1;
            this.txtWhiteList.TabIndex = 47;
            // 
            // txtBlackList
            // 
            this.txtBlackList.Location = new System.Drawing.Point(126, 300);
            this.txtBlackList.Name = "txtBlackList";
            this.txtBlackList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.txtBlackList.Size = new System.Drawing.Size(884, 20);
            this.txtBlackList.StyleController = this.layoutControl1;
            this.txtBlackList.TabIndex = 48;
            // 
            // txtTranNodeAddress
            // 
            this.txtTranNodeAddress.Location = new System.Drawing.Point(376, 84);
            this.txtTranNodeAddress.Name = "txtTranNodeAddress";
            this.txtTranNodeAddress.Size = new System.Drawing.Size(634, 20);
            this.txtTranNodeAddress.StyleController = this.layoutControl1;
            this.txtTranNodeAddress.TabIndex = 10;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { this.layoutTranNodeNO, this.layoutTranNodeAddress, this.layoutLockLimitAmt, this.layoutWarningLimitAmt, this.layoutInTime, this.layoutSignDays, this.layoutAckRecDays, this.layoutManagementFee, this.layoutUsageFee, this.layoutTranNodeName, this.layoutWhiteList, this.layoutTranNodeType, this.layoutRemark, this.layoutContractNote, this.layoutTranNodeBeginDate, this.layoutTranNodeEndDate, this.layoutTranNodePerson, this.layoutTranNodePersonID, this.layoutTranNodeMobile, this.layoutAreaNo, this.layoutAgencyRecLimitAmt, this.layoutCarriageForwardLimitAmt, this.layoutOutTime, this.layoutSignLoopEndTime, this.layoutSignLimitTime, this.layoutPickupWeightLimit, this.layoutPickupVolumeLimit, this.layoutDeposit, this.layoutCostMasterYN, this.layoutParentNo, this.layoutTranNodeCostNo, this.layoutFinancialCenter, this.layoutTranNodeStatus, this.layoutTranNodeAxes, this.layoutAppUser, this.layoutAppDate, this.layoutIsLockLimitKPI, this.layoutCreatedBy, this.layoutCreationDate, this.layoutLastUpdatedBy, this.layoutLastUpdateDate, this.layoutLockLimit, this.layoutISLocked, this.layoutFlagApp, this.layoutDispatchOnly, this.layoutAckRec, this.layoutSendSMS, this.layoutPublicYN, this.layoutBlackList });
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1022, 380);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutTranNodeNO
            // 
            this.layoutTranNodeNO.Control = this.txtTranNodeNO;
            this.layoutTranNodeNO.CustomizationFormText = "网点ID";
            this.layoutTranNodeNO.Location = new System.Drawing.Point(0, 0);
            this.layoutTranNodeNO.Name = "layoutTranNodeNO";
            this.layoutTranNodeNO.Size = new System.Drawing.Size(250, 24);
            this.layoutTranNodeNO.Text = "网点ID";
            this.layoutTranNodeNO.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutTranNodeAddress
            // 
            this.layoutTranNodeAddress.Control = this.txtTranNodeAddress;
            this.layoutTranNodeAddress.CustomizationFormText = "网点地址";
            this.layoutTranNodeAddress.Location = new System.Drawing.Point(250, 72);
            this.layoutTranNodeAddress.Name = "layoutTranNodeAddress";
            this.layoutTranNodeAddress.Size = new System.Drawing.Size(752, 24);
            this.layoutTranNodeAddress.Text = "地址";
            this.layoutTranNodeAddress.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutLockLimitAmt
            // 
            this.layoutLockLimitAmt.Control = this.txtLockLimitAmt;
            this.layoutLockLimitAmt.CustomizationFormText = "锁机金额";
            this.layoutLockLimitAmt.Location = new System.Drawing.Point(0, 96);
            this.layoutLockLimitAmt.Name = "layoutLockLimitAmt";
            this.layoutLockLimitAmt.Size = new System.Drawing.Size(250, 24);
            this.layoutLockLimitAmt.Text = "锁机金额";
            this.layoutLockLimitAmt.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutWarningLimitAmt
            // 
            this.layoutWarningLimitAmt.Control = this.txtWarningLimitAmt;
            this.layoutWarningLimitAmt.CustomizationFormText = "警戒金额";
            this.layoutWarningLimitAmt.Location = new System.Drawing.Point(250, 96);
            this.layoutWarningLimitAmt.Name = "layoutWarningLimitAmt";
            this.layoutWarningLimitAmt.Size = new System.Drawing.Size(251, 24);
            this.layoutWarningLimitAmt.Text = "警戒金额";
            this.layoutWarningLimitAmt.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutInTime
            // 
            this.layoutInTime.Control = this.txtInTime;
            this.layoutInTime.CustomizationFormText = "进港时间";
            this.layoutInTime.Location = new System.Drawing.Point(0, 120);
            this.layoutInTime.Name = "layoutInTime";
            this.layoutInTime.Size = new System.Drawing.Size(250, 24);
            this.layoutInTime.Text = "进港时间";
            this.layoutInTime.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutSignDays
            // 
            this.layoutSignDays.Control = this.txtSignDays;
            this.layoutSignDays.CustomizationFormText = "签收天数";
            this.layoutSignDays.Location = new System.Drawing.Point(501, 144);
            this.layoutSignDays.Name = "layoutSignDays";
            this.layoutSignDays.Size = new System.Drawing.Size(250, 24);
            this.layoutSignDays.Text = "签收天数";
            this.layoutSignDays.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutAckRecDays
            // 
            this.layoutAckRecDays.Control = this.txtAckRecDays;
            this.layoutAckRecDays.CustomizationFormText = "回单返回天数";
            this.layoutAckRecDays.Location = new System.Drawing.Point(751, 144);
            this.layoutAckRecDays.Name = "layoutAckRecDays";
            this.layoutAckRecDays.Size = new System.Drawing.Size(251, 24);
            this.layoutAckRecDays.Text = "回单返回天数";
            this.layoutAckRecDays.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutManagementFee
            // 
            this.layoutManagementFee.Control = this.txtManagementFee;
            this.layoutManagementFee.CustomizationFormText = "管理费";
            this.layoutManagementFee.Location = new System.Drawing.Point(0, 168);
            this.layoutManagementFee.Name = "layoutManagementFee";
            this.layoutManagementFee.Size = new System.Drawing.Size(250, 24);
            this.layoutManagementFee.Text = "管理费";
            this.layoutManagementFee.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutUsageFee
            // 
            this.layoutUsageFee.Control = this.txtUsageFee;
            this.layoutUsageFee.CustomizationFormText = "系统使用费";
            this.layoutUsageFee.Location = new System.Drawing.Point(250, 168);
            this.layoutUsageFee.Name = "layoutUsageFee";
            this.layoutUsageFee.Size = new System.Drawing.Size(251, 24);
            this.layoutUsageFee.Text = "系统使用费";
            this.layoutUsageFee.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutTranNodeName
            // 
            this.layoutTranNodeName.Control = this.txtTranNodeName;
            this.layoutTranNodeName.CustomizationFormText = "网点名称";
            this.layoutTranNodeName.Location = new System.Drawing.Point(250, 0);
            this.layoutTranNodeName.Name = "layoutTranNodeName";
            this.layoutTranNodeName.Size = new System.Drawing.Size(251, 24);
            this.layoutTranNodeName.Text = "网点名称";
            this.layoutTranNodeName.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutWhiteList
            // 
            this.layoutWhiteList.Control = this.txtWhiteList;
            this.layoutWhiteList.CustomizationFormText = "白名单";
            this.layoutWhiteList.Location = new System.Drawing.Point(0, 264);
            this.layoutWhiteList.Name = "layoutWhiteList";
            this.layoutWhiteList.Size = new System.Drawing.Size(751, 24);
            this.layoutWhiteList.Text = "白名单";
            this.layoutWhiteList.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutTranNodeType
            // 
            this.layoutTranNodeType.Control = this.txtTranNodeType;
            this.layoutTranNodeType.CustomizationFormText = "网点类型";
            this.layoutTranNodeType.Location = new System.Drawing.Point(501, 0);
            this.layoutTranNodeType.Name = "layoutTranNodeType";
            this.layoutTranNodeType.Size = new System.Drawing.Size(250, 24);
            this.layoutTranNodeType.Text = "网点类型";
            this.layoutTranNodeType.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutRemark
            // 
            this.layoutRemark.Control = this.txtRemark;
            this.layoutRemark.CustomizationFormText = "备注";
            this.layoutRemark.Location = new System.Drawing.Point(0, 240);
            this.layoutRemark.Name = "layoutRemark";
            this.layoutRemark.Size = new System.Drawing.Size(751, 24);
            this.layoutRemark.Text = "备注";
            this.layoutRemark.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutContractNote
            // 
            this.layoutContractNote.Control = this.txtContractNote;
            this.layoutContractNote.CustomizationFormText = "合同备注";
            this.layoutContractNote.Location = new System.Drawing.Point(0, 216);
            this.layoutContractNote.Name = "layoutContractNote";
            this.layoutContractNote.Size = new System.Drawing.Size(751, 24);
            this.layoutContractNote.Text = "合同备注";
            this.layoutContractNote.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutTranNodeBeginDate
            // 
            this.layoutTranNodeBeginDate.Control = this.txtTranNodeBeginDate;
            this.layoutTranNodeBeginDate.CustomizationFormText = "合同开始时间";
            this.layoutTranNodeBeginDate.Location = new System.Drawing.Point(0, 24);
            this.layoutTranNodeBeginDate.Name = "layoutTranNodeBeginDate";
            this.layoutTranNodeBeginDate.Size = new System.Drawing.Size(250, 24);
            this.layoutTranNodeBeginDate.Text = "合同开始时间";
            this.layoutTranNodeBeginDate.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutTranNodeEndDate
            // 
            this.layoutTranNodeEndDate.Control = this.txtTranNodeEndDate;
            this.layoutTranNodeEndDate.CustomizationFormText = "合同终止时间";
            this.layoutTranNodeEndDate.Location = new System.Drawing.Point(250, 24);
            this.layoutTranNodeEndDate.Name = "layoutTranNodeEndDate";
            this.layoutTranNodeEndDate.Size = new System.Drawing.Size(251, 24);
            this.layoutTranNodeEndDate.Text = "合同终止时间";
            this.layoutTranNodeEndDate.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutTranNodePerson
            // 
            this.layoutTranNodePerson.Control = this.txtTranNodePerson;
            this.layoutTranNodePerson.CustomizationFormText = "网点负责人";
            this.layoutTranNodePerson.Location = new System.Drawing.Point(0, 48);
            this.layoutTranNodePerson.Name = "layoutTranNodePerson";
            this.layoutTranNodePerson.Size = new System.Drawing.Size(250, 24);
            this.layoutTranNodePerson.Text = "网点负责人";
            this.layoutTranNodePerson.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutTranNodePersonID
            // 
            this.layoutTranNodePersonID.Control = this.txtTranNodePersonID;
            this.layoutTranNodePersonID.CustomizationFormText = "负责人证件号码";
            this.layoutTranNodePersonID.Location = new System.Drawing.Point(250, 48);
            this.layoutTranNodePersonID.Name = "layoutTranNodePersonID";
            this.layoutTranNodePersonID.Size = new System.Drawing.Size(251, 24);
            this.layoutTranNodePersonID.Text = "负责人证件号码";
            this.layoutTranNodePersonID.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutTranNodeMobile
            // 
            this.layoutTranNodeMobile.Control = this.txtTranNodeMobile;
            this.layoutTranNodeMobile.CustomizationFormText = "网点联系方式";
            this.layoutTranNodeMobile.Location = new System.Drawing.Point(501, 48);
            this.layoutTranNodeMobile.Name = "layoutTranNodeMobile";
            this.layoutTranNodeMobile.Size = new System.Drawing.Size(250, 24);
            this.layoutTranNodeMobile.Text = "网点联系方式";
            this.layoutTranNodeMobile.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutAreaNo
            // 
            this.layoutAreaNo.Control = this.txtAreaNo;
            this.layoutAreaNo.CustomizationFormText = "省市区";
            this.layoutAreaNo.Location = new System.Drawing.Point(0, 72);
            this.layoutAreaNo.Name = "layoutAreaNo";
            this.layoutAreaNo.Size = new System.Drawing.Size(250, 24);
            this.layoutAreaNo.Text = "省市区";
            this.layoutAreaNo.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutAgencyRecLimitAmt
            // 
            this.layoutAgencyRecLimitAmt.Control = this.txtAgencyRecLimitAmt;
            this.layoutAgencyRecLimitAmt.CustomizationFormText = "代收款限额";
            this.layoutAgencyRecLimitAmt.Location = new System.Drawing.Point(501, 96);
            this.layoutAgencyRecLimitAmt.Name = "layoutAgencyRecLimitAmt";
            this.layoutAgencyRecLimitAmt.Size = new System.Drawing.Size(250, 24);
            this.layoutAgencyRecLimitAmt.Text = "代收款限额";
            this.layoutAgencyRecLimitAmt.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutCarriageForwardLimitAmt
            // 
            this.layoutCarriageForwardLimitAmt.Control = this.txtCarriageForwardLimitAmt;
            this.layoutCarriageForwardLimitAmt.CustomizationFormText = "到付款限额";
            this.layoutCarriageForwardLimitAmt.Location = new System.Drawing.Point(751, 96);
            this.layoutCarriageForwardLimitAmt.Name = "layoutCarriageForwardLimitAmt";
            this.layoutCarriageForwardLimitAmt.Size = new System.Drawing.Size(251, 24);
            this.layoutCarriageForwardLimitAmt.Text = "到付款限额";
            this.layoutCarriageForwardLimitAmt.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutOutTime
            // 
            this.layoutOutTime.Control = this.txtOutTime;
            this.layoutOutTime.CustomizationFormText = "出港时间";
            this.layoutOutTime.Location = new System.Drawing.Point(250, 120);
            this.layoutOutTime.Name = "layoutOutTime";
            this.layoutOutTime.Size = new System.Drawing.Size(251, 24);
            this.layoutOutTime.Text = "出港时间";
            this.layoutOutTime.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutSignLoopEndTime
            // 
            this.layoutSignLoopEndTime.Control = this.txtSignLoopEndTime;
            this.layoutSignLoopEndTime.CustomizationFormText = "签收周期起算时间";
            this.layoutSignLoopEndTime.Location = new System.Drawing.Point(501, 120);
            this.layoutSignLoopEndTime.Name = "layoutSignLoopEndTime";
            this.layoutSignLoopEndTime.Size = new System.Drawing.Size(250, 24);
            this.layoutSignLoopEndTime.Text = "签收周期起算时间";
            this.layoutSignLoopEndTime.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutSignLimitTime
            // 
            this.layoutSignLimitTime.Control = this.txtSignLimitTime;
            this.layoutSignLimitTime.CustomizationFormText = "签收最晚时间";
            this.layoutSignLimitTime.Location = new System.Drawing.Point(751, 120);
            this.layoutSignLimitTime.Name = "layoutSignLimitTime";
            this.layoutSignLimitTime.Size = new System.Drawing.Size(251, 24);
            this.layoutSignLimitTime.Text = "签收最晚时间";
            this.layoutSignLimitTime.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutPickupWeightLimit
            // 
            this.layoutPickupWeightLimit.Control = this.txtPickupWeightLimit;
            this.layoutPickupWeightLimit.CustomizationFormText = "自提限重";
            this.layoutPickupWeightLimit.Location = new System.Drawing.Point(0, 144);
            this.layoutPickupWeightLimit.Name = "layoutPickupWeightLimit";
            this.layoutPickupWeightLimit.Size = new System.Drawing.Size(250, 24);
            this.layoutPickupWeightLimit.Text = "自提限重";
            this.layoutPickupWeightLimit.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutPickupVolumeLimit
            // 
            this.layoutPickupVolumeLimit.Control = this.txtPickupVolumeLimit;
            this.layoutPickupVolumeLimit.CustomizationFormText = "自提限方";
            this.layoutPickupVolumeLimit.Location = new System.Drawing.Point(250, 144);
            this.layoutPickupVolumeLimit.Name = "layoutPickupVolumeLimit";
            this.layoutPickupVolumeLimit.Size = new System.Drawing.Size(251, 24);
            this.layoutPickupVolumeLimit.Text = "自提限方";
            this.layoutPickupVolumeLimit.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutDeposit
            // 
            this.layoutDeposit.Control = this.txtDeposit;
            this.layoutDeposit.CustomizationFormText = "押金";
            this.layoutDeposit.Location = new System.Drawing.Point(501, 168);
            this.layoutDeposit.Name = "layoutDeposit";
            this.layoutDeposit.Size = new System.Drawing.Size(250, 24);
            this.layoutDeposit.Text = "押金";
            this.layoutDeposit.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutCostMasterYN
            // 
            this.layoutCostMasterYN.Control = this.txtCostMasterYN;
            this.layoutCostMasterYN.CustomizationFormText = "跨平台结算主体";
            this.layoutCostMasterYN.Location = new System.Drawing.Point(250, 192);
            this.layoutCostMasterYN.Name = "layoutCostMasterYN";
            this.layoutCostMasterYN.Size = new System.Drawing.Size(251, 24);
            this.layoutCostMasterYN.Text = "跨平台结算主体";
            this.layoutCostMasterYN.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutParentNo
            // 
            this.layoutParentNo.Control = this.txtParentNo;
            this.layoutParentNo.CustomizationFormText = "上级网点ID";
            this.layoutParentNo.Location = new System.Drawing.Point(751, 0);
            this.layoutParentNo.Name = "layoutParentNo";
            this.layoutParentNo.Size = new System.Drawing.Size(251, 24);
            this.layoutParentNo.Text = "上级网点ID";
            this.layoutParentNo.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutTranNodeCostNo
            // 
            this.layoutTranNodeCostNo.Control = this.txtTranNodeCostNo;
            this.layoutTranNodeCostNo.CustomizationFormText = "结算网点编号";
            this.layoutTranNodeCostNo.Location = new System.Drawing.Point(501, 24);
            this.layoutTranNodeCostNo.Name = "layoutTranNodeCostNo";
            this.layoutTranNodeCostNo.Size = new System.Drawing.Size(250, 24);
            this.layoutTranNodeCostNo.Text = "结算网点编号";
            this.layoutTranNodeCostNo.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutFinancialCenter
            // 
            this.layoutFinancialCenter.Control = this.txtFinancialCenter;
            this.layoutFinancialCenter.CustomizationFormText = "所属财务中心";
            this.layoutFinancialCenter.Location = new System.Drawing.Point(751, 24);
            this.layoutFinancialCenter.Name = "layoutFinancialCenter";
            this.layoutFinancialCenter.Size = new System.Drawing.Size(251, 24);
            this.layoutFinancialCenter.Text = "所属财务中心";
            this.layoutFinancialCenter.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutTranNodeStatus
            // 
            this.layoutTranNodeStatus.Control = this.txtTranNodeStatus;
            this.layoutTranNodeStatus.CustomizationFormText = "网点状态";
            this.layoutTranNodeStatus.Location = new System.Drawing.Point(751, 48);
            this.layoutTranNodeStatus.Name = "layoutTranNodeStatus";
            this.layoutTranNodeStatus.Size = new System.Drawing.Size(251, 24);
            this.layoutTranNodeStatus.Text = "网点状态";
            this.layoutTranNodeStatus.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutTranNodeAxes
            // 
            this.layoutTranNodeAxes.Control = this.txtTranNodeAxes;
            this.layoutTranNodeAxes.CustomizationFormText = "网点坐标";
            this.layoutTranNodeAxes.Location = new System.Drawing.Point(0, 336);
            this.layoutTranNodeAxes.Name = "layoutTranNodeAxes";
            this.layoutTranNodeAxes.Size = new System.Drawing.Size(250, 24);
            this.layoutTranNodeAxes.Text = "网点坐标";
            this.layoutTranNodeAxes.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutAppUser
            // 
            this.layoutAppUser.Control = this.txtAppUser;
            this.layoutAppUser.CustomizationFormText = "审核人";
            this.layoutAppUser.Location = new System.Drawing.Point(250, 336);
            this.layoutAppUser.Name = "layoutAppUser";
            this.layoutAppUser.Size = new System.Drawing.Size(251, 24);
            this.layoutAppUser.Text = "审核人";
            this.layoutAppUser.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutAppDate
            // 
            this.layoutAppDate.Control = this.txtAppDate;
            this.layoutAppDate.CustomizationFormText = "审核时间";
            this.layoutAppDate.Location = new System.Drawing.Point(501, 336);
            this.layoutAppDate.Name = "layoutAppDate";
            this.layoutAppDate.Size = new System.Drawing.Size(250, 24);
            this.layoutAppDate.Text = "审核时间";
            this.layoutAppDate.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutIsLockLimitKPI
            // 
            this.layoutIsLockLimitKPI.Control = this.txtIsLockLimitKPI;
            this.layoutIsLockLimitKPI.CustomizationFormText = "网点锁机kpi是否执行";
            this.layoutIsLockLimitKPI.Location = new System.Drawing.Point(0, 192);
            this.layoutIsLockLimitKPI.Name = "layoutIsLockLimitKPI";
            this.layoutIsLockLimitKPI.Size = new System.Drawing.Size(250, 24);
            this.layoutIsLockLimitKPI.Text = "网点锁机kpi是否执行";
            this.layoutIsLockLimitKPI.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutCreatedBy
            // 
            this.layoutCreatedBy.Control = this.txtCreatedBy;
            this.layoutCreatedBy.CustomizationFormText = "创建人";
            this.layoutCreatedBy.Location = new System.Drawing.Point(0, 312);
            this.layoutCreatedBy.Name = "layoutCreatedBy";
            this.layoutCreatedBy.Size = new System.Drawing.Size(250, 24);
            this.layoutCreatedBy.Text = "创建人";
            this.layoutCreatedBy.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutCreationDate
            // 
            this.layoutCreationDate.Control = this.txtCreationDate;
            this.layoutCreationDate.CustomizationFormText = "创建时间";
            this.layoutCreationDate.Location = new System.Drawing.Point(250, 312);
            this.layoutCreationDate.Name = "layoutCreationDate";
            this.layoutCreationDate.Size = new System.Drawing.Size(251, 24);
            this.layoutCreationDate.Text = "创建时间";
            this.layoutCreationDate.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutLastUpdatedBy
            // 
            this.layoutLastUpdatedBy.Control = this.txtLastUpdatedBy;
            this.layoutLastUpdatedBy.CustomizationFormText = "更新人";
            this.layoutLastUpdatedBy.Location = new System.Drawing.Point(501, 312);
            this.layoutLastUpdatedBy.Name = "layoutLastUpdatedBy";
            this.layoutLastUpdatedBy.Size = new System.Drawing.Size(250, 24);
            this.layoutLastUpdatedBy.Text = "更新人";
            this.layoutLastUpdatedBy.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutLastUpdateDate
            // 
            this.layoutLastUpdateDate.Control = this.txtLastUpdateDate;
            this.layoutLastUpdateDate.CustomizationFormText = "更新时间";
            this.layoutLastUpdateDate.Location = new System.Drawing.Point(751, 312);
            this.layoutLastUpdateDate.Name = "layoutLastUpdateDate";
            this.layoutLastUpdateDate.Size = new System.Drawing.Size(251, 24);
            this.layoutLastUpdateDate.Text = "更新时间";
            this.layoutLastUpdateDate.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutLockLimit
            // 
            this.layoutLockLimit.Control = this.txtLockLimit;
            this.layoutLockLimit.CustomizationFormText = "锁机限制";
            this.layoutLockLimit.Location = new System.Drawing.Point(501, 192);
            this.layoutLockLimit.Name = "layoutLockLimit";
            this.layoutLockLimit.Size = new System.Drawing.Size(250, 24);
            this.layoutLockLimit.Text = "锁机限制";
            this.layoutLockLimit.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutISLocked
            // 
            this.layoutISLocked.Control = this.txtISLocked;
            this.layoutISLocked.CustomizationFormText = "封锁";
            this.layoutISLocked.Location = new System.Drawing.Point(751, 192);
            this.layoutISLocked.Name = "layoutISLocked";
            this.layoutISLocked.Size = new System.Drawing.Size(251, 24);
            this.layoutISLocked.Text = "封锁";
            this.layoutISLocked.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutFlagApp
            // 
            this.layoutFlagApp.Control = this.txtFlagApp;
            this.layoutFlagApp.CustomizationFormText = "审核";
            this.layoutFlagApp.Location = new System.Drawing.Point(751, 336);
            this.layoutFlagApp.Name = "layoutFlagApp";
            this.layoutFlagApp.Size = new System.Drawing.Size(251, 24);
            this.layoutFlagApp.Text = "审核";
            this.layoutFlagApp.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutDispatchOnly
            // 
            this.layoutDispatchOnly.Control = this.txtDispatchOnly;
            this.layoutDispatchOnly.CustomizationFormText = "仅送货";
            this.layoutDispatchOnly.Location = new System.Drawing.Point(751, 216);
            this.layoutDispatchOnly.Name = "layoutDispatchOnly";
            this.layoutDispatchOnly.Size = new System.Drawing.Size(251, 24);
            this.layoutDispatchOnly.Text = "仅送货";
            this.layoutDispatchOnly.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutAckRec
            // 
            this.layoutAckRec.Control = this.txtAckRec;
            this.layoutAckRec.CustomizationFormText = "开通回单";
            this.layoutAckRec.Location = new System.Drawing.Point(751, 240);
            this.layoutAckRec.Name = "layoutAckRec";
            this.layoutAckRec.Size = new System.Drawing.Size(251, 24);
            this.layoutAckRec.Text = "开通回单";
            this.layoutAckRec.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutSendSMS
            // 
            this.layoutSendSMS.Control = this.txtSendSMS;
            this.layoutSendSMS.CustomizationFormText = "开通短信";
            this.layoutSendSMS.Location = new System.Drawing.Point(751, 264);
            this.layoutSendSMS.Name = "layoutSendSMS";
            this.layoutSendSMS.Size = new System.Drawing.Size(251, 24);
            this.layoutSendSMS.Text = "开通短信";
            this.layoutSendSMS.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutPublicYN
            // 
            this.layoutPublicYN.Control = this.txtPublicYN;
            this.layoutPublicYN.CustomizationFormText = "是否开放";
            this.layoutPublicYN.Location = new System.Drawing.Point(751, 168);
            this.layoutPublicYN.Name = "layoutPublicYN";
            this.layoutPublicYN.Size = new System.Drawing.Size(251, 24);
            this.layoutPublicYN.Text = "是否开放";
            this.layoutPublicYN.TextSize = new System.Drawing.Size(111, 14);
            // 
            // layoutBlackList
            // 
            this.layoutBlackList.Control = this.txtBlackList;
            this.layoutBlackList.CustomizationFormText = "黑名单";
            this.layoutBlackList.Location = new System.Drawing.Point(0, 288);
            this.layoutBlackList.Name = "layoutBlackList";
            this.layoutBlackList.Size = new System.Drawing.Size(1002, 24);
            this.layoutBlackList.Text = "黑名单";
            this.layoutBlackList.TextSize = new System.Drawing.Size(111, 14);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(10, 394);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1022, 459);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "明细清单";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 21);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1018, 436);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridView1 });
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 40;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.ViewCaptionHeight = 100;
            // 
            // FrmEditNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 903);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmEditNode";
            this.Text = "网点资料";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.groupControl1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeCostNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeBeginDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeBeginDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParentNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodePerson.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodePersonID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeMobile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLockLimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLockLimitAmt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarningLimitAmt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendSMS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtISLocked.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAckRec.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgencyRecLimitAmt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarriageForwardLimitAmt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAreaNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAreaNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreationDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdateDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastUpdatedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPublicYN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlagApp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAppUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAppDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAppDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSignLoopEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSignLimitTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSignDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAckRecDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostMasterYN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManagementFee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsageFee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeposit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDispatchOnly.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPickupWeightLimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPickupVolumeLimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeAxes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIsLockLimitKPI.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinancialCenter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWhiteList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBlackList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranNodeAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLockLimitAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWarningLimitAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutInTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSignDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAckRecDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutManagementFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutUsageFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWhiteList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutContractNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeBeginDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodePerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodePersonID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeMobile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAreaNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAgencyRecLimitAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCarriageForwardLimitAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutOutTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSignLoopEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSignLimitTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutPickupWeightLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutPickupVolumeLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDeposit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCostMasterYN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutParentNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeCostNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFinancialCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTranNodeAxes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAppUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAppDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutIsLockLimitKPI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCreatedBy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCreationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLastUpdatedBy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLastUpdateDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLockLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutISLocked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFlagApp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDispatchOnly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutAckRec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSendSMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutPublicYN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBlackList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // protected new DevExpress.XtraGrid.GridControl gridControl1;
        // protected new DevExpress.XtraEditors.SimpleButton btnCancel;
        // protected new DevExpress.XtraEditors.SimpleButton btnOK;
        // protected new DevExpress.XtraEditors.SimpleButton btnAdd;
        // protected new BB.BaseUI.BaseUI.DataNavigator dataNavigator1;
        // protected new System.Windows.Forms.PictureBox picPrint;
        //
        //
        // protected new DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        // protected new DevExpress.XtraLayout.LayoutControl layoutControl1;

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtTranNodeNO;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranNodeNO;
        private DevExpress.XtraEditors.ComboBoxEdit txtTranNodeCostNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranNodeCostNo;
        private DevExpress.XtraEditors.TextEdit txtTranNodeName;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranNodeName;
        private DevExpress.XtraEditors.ComboBoxEdit txtTranNodeType;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranNodeType;
        private DevExpress.XtraEditors.DateEdit txtTranNodeBeginDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranNodeBeginDate;
        private DevExpress.XtraEditors.DateEdit txtTranNodeEndDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranNodeEndDate;
        private DevExpress.XtraEditors.ComboBoxEdit txtParentNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutParentNo;
        private DevExpress.XtraEditors.TextEdit txtTranNodePerson;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranNodePerson;
        private DevExpress.XtraEditors.TextEdit txtTranNodePersonID;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranNodePersonID;
        private DevExpress.XtraEditors.TextEdit txtTranNodeMobile;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranNodeMobile;
        private DevExpress.XtraEditors.TextEdit txtTranNodeAddress;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranNodeAddress;
        private DevExpress.XtraEditors.ToggleSwitch txtLockLimit;
        private DevExpress.XtraLayout.LayoutControlItem layoutLockLimit;
        private DevExpress.XtraEditors.SpinEdit txtLockLimitAmt;
        private DevExpress.XtraLayout.LayoutControlItem layoutLockLimitAmt;
        private DevExpress.XtraEditors.SpinEdit txtWarningLimitAmt;
        private DevExpress.XtraLayout.LayoutControlItem layoutWarningLimitAmt;
        private DevExpress.XtraEditors.ToggleSwitch txtSendSMS;
        private DevExpress.XtraLayout.LayoutControlItem layoutSendSMS;
        private DevExpress.XtraEditors.ToggleSwitch txtISLocked;
        private DevExpress.XtraLayout.LayoutControlItem layoutISLocked;
        private DevExpress.XtraEditors.ToggleSwitch txtAckRec;
        private DevExpress.XtraLayout.LayoutControlItem layoutAckRec;
        private DevExpress.XtraEditors.SpinEdit txtAgencyRecLimitAmt;
        private DevExpress.XtraLayout.LayoutControlItem layoutAgencyRecLimitAmt;
        private DevExpress.XtraEditors.SpinEdit txtCarriageForwardLimitAmt;
        private DevExpress.XtraLayout.LayoutControlItem layoutCarriageForwardLimitAmt;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAreaNo;
        private DevExpress.XtraEditors.SearchLookUpEdit txtAreaNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutAreaNo;
        private DevExpress.XtraEditors.TimeEdit txtInTime;
        private DevExpress.XtraLayout.LayoutControlItem layoutInTime;
        private DevExpress.XtraEditors.TimeEdit txtOutTime;
        private DevExpress.XtraLayout.LayoutControlItem layoutOutTime;
        private DevExpress.XtraEditors.TextEdit txtRemark;
        private DevExpress.XtraLayout.LayoutControlItem layoutRemark;
        private DevExpress.XtraEditors.DateEdit txtCreationDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutCreationDate;
        private DevExpress.XtraEditors.ComboBoxEdit txtCreatedBy;
        private DevExpress.XtraLayout.LayoutControlItem layoutCreatedBy;
        private DevExpress.XtraEditors.DateEdit txtLastUpdateDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutLastUpdateDate;
        private DevExpress.XtraEditors.ComboBoxEdit txtLastUpdatedBy;
        private DevExpress.XtraLayout.LayoutControlItem layoutLastUpdatedBy;
        private DevExpress.XtraEditors.ComboBoxEdit txtTranNodeStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranNodeStatus;
        private DevExpress.XtraEditors.ToggleSwitch txtPublicYN;
        private DevExpress.XtraLayout.LayoutControlItem layoutPublicYN;
        private DevExpress.XtraEditors.ToggleSwitch txtFlagApp;
        private DevExpress.XtraLayout.LayoutControlItem layoutFlagApp;
        private DevExpress.XtraEditors.ComboBoxEdit txtAppUser;
        private DevExpress.XtraLayout.LayoutControlItem layoutAppUser;
        private DevExpress.XtraEditors.DateEdit txtAppDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutAppDate;
        private DevExpress.XtraEditors.TimeEdit txtSignLoopEndTime;
        private DevExpress.XtraLayout.LayoutControlItem layoutSignLoopEndTime;
        private DevExpress.XtraEditors.TimeEdit txtSignLimitTime;
        private DevExpress.XtraLayout.LayoutControlItem layoutSignLimitTime;
        private DevExpress.XtraEditors.TextEdit txtSignDays;
        private DevExpress.XtraLayout.LayoutControlItem layoutSignDays;
        private DevExpress.XtraEditors.TextEdit txtAckRecDays;
        private DevExpress.XtraLayout.LayoutControlItem layoutAckRecDays;
        private DevExpress.XtraEditors.ToggleSwitch txtCostMasterYN;
        private DevExpress.XtraLayout.LayoutControlItem layoutCostMasterYN;
        private DevExpress.XtraEditors.SpinEdit txtManagementFee;
        private DevExpress.XtraLayout.LayoutControlItem layoutManagementFee;
        private DevExpress.XtraEditors.SpinEdit txtUsageFee;
        private DevExpress.XtraLayout.LayoutControlItem layoutUsageFee;
        private DevExpress.XtraEditors.SpinEdit txtDeposit;
        private DevExpress.XtraLayout.LayoutControlItem layoutDeposit;
        private DevExpress.XtraEditors.TextEdit txtContractNote;
        private DevExpress.XtraLayout.LayoutControlItem layoutContractNote;
        private DevExpress.XtraEditors.ToggleSwitch txtDispatchOnly;
        private DevExpress.XtraLayout.LayoutControlItem layoutDispatchOnly;
        private DevExpress.XtraEditors.SpinEdit txtPickupWeightLimit;
        private DevExpress.XtraLayout.LayoutControlItem layoutPickupWeightLimit;
        private DevExpress.XtraEditors.SpinEdit txtPickupVolumeLimit;
        private DevExpress.XtraLayout.LayoutControlItem layoutPickupVolumeLimit;
        private DevExpress.XtraEditors.TextEdit txtTranNodeAxes;
        private DevExpress.XtraLayout.LayoutControlItem layoutTranNodeAxes;
        private DevExpress.XtraEditors.ToggleSwitch txtIsLockLimitKPI;
        private DevExpress.XtraLayout.LayoutControlItem layoutIsLockLimitKPI;
        private DevExpress.XtraEditors.ComboBoxEdit txtFinancialCenter;
        private DevExpress.XtraLayout.LayoutControlItem layoutFinancialCenter;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtWhiteList;
        private DevExpress.XtraLayout.LayoutControlItem layoutWhiteList;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtBlackList;
        private DevExpress.XtraLayout.LayoutControlItem layoutBlackList;
    }
}