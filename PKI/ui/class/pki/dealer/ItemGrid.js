/**
 * 经销商项目列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.dealer.ItemGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '经销商项目列表',
	width: 333,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDUnitItemByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/BaseService.svc/ST_UDTO_UpdateDUnitItemByField',
	/**删除数据服务路径*/
	delUrl: '/BaseService.svc/ST_UDTO_DelDUnitItem',
	/**新增经销商项目服务路径*/
	saveUrl: '/BaseService.svc/ST_UDTO_AddDUnitItem',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**只读*/
	readOnly: false,

	/**经销商ID*/
	DealerId: null,
	/**经销商时间戳*/
//	DealerDataTimeStamp: null,

	requires: ['Shell.ux.form.field.BoolComboBox'],
	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: '100%',
			emptyText: '项目名称',
			isLike: true,
			fields: ['dunititem.BTestItem.CName']
		};
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			type: 'search',
			info: me.searchInfo
		}];

		if(!me.readOnly) {
			//创建挂靠功能栏
			me.dockedItems = [Ext.create('Shell.ux.toolbar.Button', {
				dock: 'bottom',
				items: ['add', 'del']
			})];
			me.hasDel = true;
		}

		//数据列
		me.columns = [{
			dataIndex: 'DUnitItem_BTestItem_CName',
			text: '项目名称',
			flex: 1,
			defaultRenderer: true
				//width:250,
		}];

		if(!me.readOnly) {
			me.columns.push({
				dataIndex: 'DUnitItem_IsStepPrice',
				text: '<b style="color:blue;">阶梯价</b>',
				width: 50,
				isBool: true,
				align: 'center',
				type: 'bool',
				hidden: true
					//				editor:{xtype:'uxBoolComboBox'},
			});
		} else {
			me.columns.push({
				dataIndex: 'DUnitItem_IsStepPrice',
				text: '阶梯价',
				hidden: true,
				width: 50,
				isBool: true,
				align: 'center',
				type: 'bool'
			});
		}

		me.columns.push({
			dataIndex: 'DUnitItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'DUnitItem_BTestItem_Id',
			text: '项目ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'DUnitItem_DataTimeStamp',
			text: '时间戳',
			hidden: true,
			hideable: false
		});

		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		if(me.DealerId=='0' || !me.DealerId){
			JShell.Msg.error('请选择具体的经销商');
			return;
		}
		JShell.Win.open('Shell.class.pki.dealer.item.CheckGrid', {
			resizable: false,
			checkOne: false,
			listeners: {
				accept: function(p, records) {
					me.onAccept(records);
					p.close();
				}
			}
		}).show();
	},
	/**选择确认处理*/
	onAccept: function(records) {
		var me = this;
		//获取没有保存过的数据
		var recs = me.getUnCheck(records);
		var len = recs.length;

		me.saveCount = len;
		me.saveIndex = 0;
		me.saveError = [];

		for(var i = 0; i < len; i++) {
			var rec = recs[i];
			me.saveOne({
				UnitType: 2,
				UnitID: me.DealerId,
				BTestItem: {
					Id: rec.get('BTestItem_Id'),
					DataTimeStamp: rec.get('BTestItem_DataTimeStamp').split(',')

				},
				IsStepPrice: rec.get('DUnitItem_IsStepPrice') == true ? '1' : '0'
			});
		}
	},
	/**保存一条信息*/
	saveOne: function(data) {
		var me = this;
		var url = (me.saveUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.saveUrl;

		var params = {
			entity: data
		};
		params = Ext.JSON.encode(params);

		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.saveIndex++;
			if(!data.success) {
				me.saveError.push(data.msg);
			}
			if(me.saveIndex == me.saveCount) {
				me.hideMask(); //隐藏遮罩层
				if(me.saveError.length == 0) {
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT);
					me.onSearch();
				} else {
					JShell.Msg.error(me.saveError.join('</br>'));
				}
			}
		}, false);
	},
	/**获取没有保存过的数据*/
	getUnCheck: function(records) {
		var me = this,
			recs = records || [],
			len = recs.length,
			result = [];

		for(var i = 0; i < len; i++) {
			var index = me.store.find('DUnitItem_BTestItem_Id', recs[i].get('BTestItem_Id'));
			if(index == -1) result.push(recs[i]);
		}

		return result;
	},
	/**根据经销商ID获取数据*/
	loadByDealerId: function(id) {
		var me = this;
		me.DealerId = id;
		me.defaultWhere = 'dunititem.UnitType=2 and dunititem.UnitID=' + id;
		me.onSearch();
	},
	/**保存按钮点击*/
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var IsStepPrice = rec.get('DUnitItem_IsStepPrice') == true ? 1 : 0;
			me.updateOneByIsStepPrice(id, IsStepPrice);
		}
	},
	/**修改合作类型*/
	updateOneByIsStepPrice: function(id, isStepPrice) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				IsStepPrice: isStepPrice
			},
			fields: 'Id,IsStepPrice'
		});
		JShell.Server.post(url, params, function(data) {
			var record = me.store.findRecord(me.PKField, id);
			if(data.success) {
				if(record) {
					record.set(me.DelField, true);
					record.commit();
				}
				me.saveCount++;
			} else {
				me.saveErrorCount++;
				if(record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if(me.saveErrorCount == 0) me.onSearch();
			}
		}, false);
	}
});