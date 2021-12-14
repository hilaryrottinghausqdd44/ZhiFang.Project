/**
 * 供货单选择列表(待选供货单)
 * @author liangyl
 * @version 2017-06-02
 */
Ext.define('Shell.class.rea.recorded.basic.censaledoc.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '待入帐供货单',
	width: 1260,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocByHQL?isPlanish=true',
	/**是否单选*/
	checkOne: false,

	/**数据已被选到已选供货单的数据*/
	changeList: [],
	/**机构Id*/
	CENORG_ID: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.changeList = [];
		me.on({
			nodata: function() {
				me.getView().update('');
			}
		});
	},
	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += '(bmscensaledoc.IsAccountInput !=1 or bmscensaledoc.IsAccountInput is null)';
		if(me.CENORG_ID) {
			me.defaultWhere += ' and (bmscensaledoc.Lab.Id =' + me.CENORG_ID + ")";
		}
		me.defaultWhere += ' and (bmscensaledoc.Status=1)';

		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '供货单号',
			isLike: true,
			fields: ['bmscensaledoc.SaleDocNo']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.initButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'BmsCenSaleDoc_OperDate',
			text: '操作时间',
			align: 'center',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'BmsCenSaleDoc_SaleDocNo',
			text: '供货单号',
			width: 180,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_TotalPrice',
			text: '总计金额',
			align:'center',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_Lab_CName',
			text: '订货方',			
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_Comp_CName',
			text: '供货方',			
			minWidth: 150,
			flex:1,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**删除多行数据*/
	DelDataRecS: function(records) {
		var me = this;
		for(var i in records) {
			me.changeList.push(records[i].get('BmsCenSaleDoc_Id'));
			me.store.remove(records[i]);
		}
	},
	/**删除单行数据*/
	DelDataRec: function(record) {
		var me = this;
		me.changeList.push(record.get('BmsCenSaleDoc_Id'));
		me.store.remove(record);
	},
	/**增加行*/
	addDateRec: function(record) {
		var me = this;
		var obj = {
			BmsCenSaleDoc_SaleDocNo: record.get('BmsAccountSaleDoc_BmsCenSaleDoc_SaleDocNo'),
			BmsCenSaleDoc_Id: record.get('BmsAccountSaleDoc_BmsCenSaleDoc_Id'),
			BmsCenSaleDoc_Comp_CName: record.get('BmsAccountSaleDoc_BmsCenSaleDoc_Comp_CName'),
			BmsCenSaleDoc_Lab_CName: record.get('BmsAccountSaleDoc_BmsCenSaleDoc_Lab_CName'),
			BmsCenSaleDoc_TotalPrice: record.get('BmsAccountSaleDoc_BmsCenSaleDoc_TotalPrice'),
			BmsCenSaleDoc_OperDate: record.get('BmsAccountSaleDoc_BmsCenSaleDoc_OperDate')
		};
		me.store.insert(me.getStore().getCount(), obj);
	},
	initButtonToolbarItems: function() {
		var me = this;
		if(!me.searchInfo.width) me.searchInfo.width = 205;
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			type: 'search',
			info: me.searchInfo
		}];
		if(me.hasAcceptButton) me.buttonToolbarItems.push('-', 'accept');
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		var result = {},
			list = [],
			arr = [];
		//过滤存在已选择供货单的数据
		if(data.value) {
			list = data.value.list;
			Ext.Array.each(list, function(obj, index) {
				var bo = Ext.Array.contains(me.changeList, obj.BmsCenSaleDoc_Id); //返回true 检查数组内是否包含指定元素
				if(!bo) {
					arr.push(obj);
				}
			});
		}
		result.list = arr;
		return result;
	}
});