/**
 * Sanger项目维护
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.pki.item.SangerGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: 'Sanger项目维护',
	width: 1000,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchBTestItemByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/BaseService.svc/ST_UDTO_DelBTestItem',
	/**获取数据服务路径*/
	editUrl: '/BaseService.svc/ST_UDTO_UpdateBTestItemByField',
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
    defaultWhere:'btestitem.IsSanger=1',
   /**收费类型*/
	ChargeType: {
		'E0': '不收费',
		'E1': '收费'
	},
   /**项目类型*/
	ItemType: {
		'E0': '检验项目',
		'E1': '开单项目',
		'E2': '分样项目',
		'E3': '开单、检验项目',
		'E4': '开单、分样项目',
		'E5': '分样、检验项目',
		'E6': '开单、分样、检验项目'
	},
 
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
		me.addEvents('accept');
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '项目名称/英文名称/代码',
			isLike: true,
			fields: ['btestitem.CName',  'btestitem.EName','btestitem.UseCode']
		};
		//自定义按钮功能栏
		me.buttonToolbarItems = ['refresh','add',  'del', '->', {
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = [{
			dataIndex: 'BTestItem_CName',text: '项目名称',
			width: 180,defaultRenderer: true
		}, {
			dataIndex: 'BTestItem_EName',text: '英文名称',
			width: 120,defaultRenderer: true
		},{
			dataIndex: 'BTestItem_SName',text: '简称',
			width: 120,defaultRenderer: true
		}, {
			dataIndex: 'BTestItem_UseCode',text: '代码',
			width: 100,defaultRenderer: true
		}, {
			dataIndex: 'BTestItem_ItemType',text: '项目类型',
			width: 100,
		    renderer: function (value, meta) {
                var v = me.ItemType['E' + value] || '';
                if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
                return v;
            }
		}, {
			dataIndex: 'BTestItem_Unit',text: '结果单位',
			width: 100,defaultRenderer: true
		}, {
			dataIndex: 'BTestItem_RefRange',text: '默认参考范围',
			width: 100,defaultRenderer: true
		},  {
			dataIndex: 'BTestItem_ValueType',text: '结果类型',
			width: 100,defaultRenderer: true,hidden:true
		}, {
			dataIndex: 'BTestItem_SamplingRequire',text: '采样要求',
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'BTestItem_ClinicalSignificance',text: '临床意义',
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'BTestItem_ChargeType',text: '收费类型',
			width: 100,  
			renderer: function (value, meta) {
                var v = me.ChargeType['E' + value] || '';
                if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
                return v;
            }
		},{
			dataIndex: 'BTestItem_ItemCharge',text: '项目价格',
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'BTestItem_Precision',text: '精度',
			width: 100,defaultRenderer: true
		},  {
			dataIndex: 'BTestItem_UseCode',text: '代码',
			width: 100,defaultRenderer: true
		},  {
			dataIndex: 'BTestItem_StandCode',text: '标准代码',
			width: 100,defaultRenderer: true,hidden:true
		}, {
			dataIndex: 'BTestItem_DeveCode',text: '开发商标准代码',
			width: 100,defaultRenderer: true,hidden:true
		}, {
			dataIndex: 'BTestItem_Shortcode',text: '快捷码',
			width: 100,defaultRenderer: true,hidden:true
		}, {
			dataIndex: 'BTestItem_PinYinZiTou',text: '拼音字头',
			width: 100,defaultRenderer: true,hidden:true
		}, {
			dataIndex: 'BTestItem_Id',text: '项目id',
			hidden: true,hideable: false,isKey: true
		}];

		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
	    me.showForm();
	},
   

	/**打开表单*/
	showForm: function() {
		var me = this;
		var config = {
			showSuccessInfo: false, //成功信息不显示
			resizable: false,
			formtype: 'add',
			checkOne:false,
//			defaultWhere:'btestitem.IsSanger!=1',
			listeners: {
				accept:function(p,records){
					me.onAccept(records);
					p.close();
				}
			}
		};
		JShell.Win.open('Shell.class.pki.item.CheckGrid', config).show();
	},
	/**获取没有保存过的数据*/
	getUnCheck:function(records){
		var me = this,
			recs = records || [],
			len = recs.length,
			result = [];
		
		for(var i=0;i<len;i++){
			var index = me.store.find('DUnitItem_BTestItem_Id',recs[i].get('BTestItem_Id'));
			if(index == -1) result.push(recs[i]);
		}
		
		return result;
	},
	/**选择确认处理*/
	onAccept:function(records){
		var me = this;
		//获取没有保存过的数据
		var recs = me.getUnCheck(records);
		var len = recs.length;
		if(len == 0) return;
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = recs[i];
		    me.updateOneByParams({
	            entity: {
	                Id: rec.get(me.PKField),
	                IsSanger: 1
	            },
	            fields: 'Id,IsSanger'
	        });
		}
	},
	/**修改数据*/
	updateOneByParams: function(params) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode(params);

		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.saveCount++;
			} else {
				me.saveErrorCount++;
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if(me.saveErrorCount == 0) {
					me.onSearch();
				}
			}
		}, false);
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		JShell.Msg.del(function(but) {
			if (but != "ok") return;
            var len = records.length;
			if(len == 0) return;
			me.showMask(me.saveText); //显示遮罩层
			me.saveErrorCount = 0;
			me.saveCount = 0;
			me.saveLength = len;
			
			for(var i=0;i<len;i++){
				var rec = records[i];
			    me.updateOneByParams({
		            entity: {
		                Id: rec.get(me.PKField),
		                IsSanger: 0
		            },
		            fields: 'Id,IsSanger'
		        });
			}
		});
	}
});