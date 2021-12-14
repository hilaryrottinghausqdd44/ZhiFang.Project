/**
 * 送检单位列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.laboratory.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '送检单位列表',

	width: 360,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchBLaboratoryByHQL?isPlanish=true',
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**排序字段*/
	//	defaultOrderBy:[
	//		{property:'BLaboratory_BBillingUnit_Name',direction:'DESC'}
	//	],
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**只读*/
	readOnly: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(view, record) {
				me.onEditClick();
			}
		});
	},
	initComponent: function() {
		var me = this;

		//自定义按钮功能栏
		me.buttonToolbarItems = [];
		//查询框信息
		me.searchInfo = {
			width: '100%',
			emptyText: '送检单位名称',
			isLike: true,
			fields: ['blaboratory.CName']
		};
		if(!me.readOnly) {
			me.buttonToolbarItems.push('edit', {
				xtype: 'button',
				text: '科室',
				tooltip: '<b>查看该送检单位下属的所有科室</b>',
				iconCls: 'button-show',
				handler: function() {
						me.onShowLabDpet();
					}
					//			},{
					//				xtype:'button',
					//				text:'项目',
					//				tooltip:'<b>维护该送检单位的所有项目</b>',
					//				iconCls:'button-config',
					//				handler:function(){me.onItemConfig();}
			}, '-');

			me.searchInfo.width = 180;
		}

		me.buttonToolbarItems.push('->', {
			type: 'search',
			info: me.searchInfo
		});

		if(!me.readOnly) {
			//分页栏自定义功能组件
			me.agingToolbarCustomItems = [{
				xtype: 'button',
				iconCls: 'button-save',
				text: '<b style="color:blue">数据同步</b>',
				tooltip: '<b style="color:blue">数据同步</b>',
				handler: function() {
					me.onDataSync();
				}
			}, '-'];
		}

		//数据列
		me.columns = [{
			dataIndex: 'BLaboratory_CName',
			text: '送检单位名称',
			flex:1,//width: 180,
			defaultRenderer: true
		}, {
			dataIndex: 'BLaboratory_BBillingUnit_Name',
			text: '默认开票方',
			hidden:true,
			width: 110,
			defaultRenderer: true
		}, {
			dataIndex: 'BLaboratory_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BLaboratory_DataTimeStamp',
			text: '时间戳',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'BLaboratory_BBillingUnit_Id',
			text: '默认开票方ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'BLaboratory_BBillingUnit_DataTimeStamp',
			text: '默认开票方时间戳',
			hidden: true,
			hideable: false
		}];

		me.callParent(arguments);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var id = records[0].get(me.PKField);
		me.openDealerForm(id);
	},
	/**打开表单*/
	openDealerForm: function(id) {
		var me = this;
		var config = {
			showSuccessInfo: false, //成功信息不显示
			resizable: false,
			formtype: 'add',
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		if(id) {
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.pki.laboratory.Form', config).show();
	},
	/**查看该送检单位下属的所有科室*/
	onShowLabDpet: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var id = records[0].get(me.PKField);
		JShell.Win.open('Shell.class.pki.labdept.GridShow', {
			resizable: false,
			defaultWhere: 'blabdept.BLaboratory.Id=' + id
		}).show();
	},
	/**项目维护*/
	//	onItemConfig:function(){
	//		var me = this;
	//		var records = me.getSelectionModel().getSelection();
	//		if(!records || records.length != 1){
	//			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
	//			return;
	//		}
	//		
	//		var id = records[0].get(me.PKField);
	//		
	//		var win = JShell.Win.open('Shell.class.pki.laboratory.ItemGrid',{
	//			resizable:false,
	//			height:500,
	//			LaboratoryId:id
	//		}).show();
	//		win.loadByLaboratoryId(id);
	//	},
	/**数据同步*/
	onDataSync: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + '/StatService.svc/Stat_UDTO_SyncBaseDataInfo';

		me.showMask('数据同步中...'); //显示遮罩层
		JShell.Server.get(url, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
				me.onSearch();
			} else {
				var msg = data.msg.replace(/,/g, '</br>');
				JShell.Msg.error(msg);
			}
		});
	}
});