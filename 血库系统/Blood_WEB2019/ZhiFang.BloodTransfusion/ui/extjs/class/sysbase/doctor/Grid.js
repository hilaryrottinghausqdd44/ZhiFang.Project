/**
 * 医生信息维护
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.doctor.Grid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: ['Ext.ux.CheckColumn'],

	title: '字典类型列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchDoctorByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateDoctorByField',
	/**删除数据服务路径*/
	delUrl: '/BloodTransfusionManageService.svc/BT_UDTO_DelDoctor',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 50,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	//	/**是否启用修改按钮*/
	//	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'Doctor_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'doctor.Grid',
	/**用户UI配置Name*/
	userUIName: "字典类型列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '编码/名称/Code1',
			isLike: true,
			fields: ['doctor.Id', 'doctor.CName','doctor.Code1']
		};

		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
				text: '医生编码',
				dataIndex: 'Doctor_Id',
				width: 100,
				isKey: true,
				hideable: false
			}, {
				text: '名称',
				dataIndex: 'Doctor_CName',
				width: 100,
				menuDisabled: true,
				defaultRenderer: true
			}, {
				text: '医生等级',
				dataIndex: 'Doctor_GradeNo',
				width: 85,
				menuDisabled: true,
				renderer: function(value, meta) {
					var v = "";
					if (value == "1") {
						v = "申请医生";
						meta.style = "color:green;";
					} else if (value == "2") {
						v = "主治医师";
						meta.style = "color:green;";
					} else if (value == "3") {
						v = "科主任";
						meta.style = "color:green;";
					} else if (value == "4") {
						v = "医务科";
						meta.style = "color:green;";
					}
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					return v;
				}
			},
			{
				text: 'Code1',
				dataIndex: 'Doctor_Code1',
				width: 95,
				menuDisabled: true,
				defaultRenderer: true
			}, {
				text: 'Code2',
				dataIndex: 'Doctor_Code2',
				width: 95,
				menuDisabled: true,
				defaultRenderer: true
			}, {
				text: 'Code3',
				dataIndex: 'Doctor_Code3',
				width: 95,
				menuDisabled: true,
				defaultRenderer: true
			}, {
				xtype: 'checkcolumn',
				text: '使用',
				dataIndex: 'Doctor_Visible',
				width: 40,
				align: 'center',
				sortable: false,
				menuDisabled: true,
				stopSelection: false,
				type: 'boolean'
			}, {
				text: '次序',
				dataIndex: 'Doctor_DispOrder',
				width: 100,
				defaultRenderer: true,
				align: 'center',
				type: 'int'
			}, {
				xtype: 'actioncolumn',
				text: '操作',
				align: 'center',
				width: 45,
				hideable: false,
				sortable: false,
				menuDisabled: true,
				items: [{
					iconCls: 'button-show hand',
					handler: function(grid, rowIndex, colIndex) {
						var rec = grid.getStore().getAt(rowIndex);
						me.onShowOperation(rec);
					}
				}]
			}
		];

		return columns;
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var IsUse = rec.get('Doctor_Visible');
			me.updateOneByIsUse(i, id, IsUse);
		}
	},
	updateOneByIsUse: function(index, id, IsUse) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		if (IsUse == true || IsUse == 1) {
			IsUse = 1;
		} else {
			IsUse = 0;
		}
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		
		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				Visible: IsUse
			},
			empID:empID,
			empName:empName,
			fields: 'Id,Visible'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		this.fireEvent('addclick', this);
	},
	onShowOperation: function(record) {
		var me = this;
		if (!record) {
			var records = me.ApplyGrid.getSelectionModel().getSelection();
			if (records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get("Doctor_Id");
		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			PK: id,
			classNameSpace: 'ZhiFang.Entity.BloodTransfusion', //类域
			className: 'UpdateOperationType', //类名
			title: '医生信息操作记录',
			defaultWhere: "scoperation.BusinessModuleCode='Doctor'"
		};
		var win = JShell.Win.open('Shell.class.blood.scoperation.SCOperation', config);
		win.show();
	}
});
