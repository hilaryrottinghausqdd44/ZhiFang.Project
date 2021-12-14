/**
 * 样本删除
 * @author liangyl
 * @version 2017-10-24
 */
Ext.define('Shell.class.pki.sample.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '样本删除 ',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/StatService.svc/Stat_UDTO_SearchDeleteNRequestItem?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/BaseService.svc/ST_UDTO_UpdateNRequestItemByField',
	/**删除服务地址*/
	delUrl: '/StatService.svc/Stat_UDTO_DeleteNRequestItemByIDList',

	/**下载EXCEL文件服务地址*/
	downLoadExcelUrl: '/StatService.svc/Stat_UDTO_ReconciliationToExcel',
	/**
	 * 排序字段
	 * 
	 * @exception 
	 * [{property: 'NRequestItem_ReconciliationState',direction: 'ASC'}]
	 */
	defaultOrderBy: [{
		property: 'NRequestItem_ReconciliationState',
		direction: 'ASC'
	}, {
		property: 'NRequestItem_IsGetPrice',
		direction: 'ASC'
	}],
	/**财务报表类型*/
	reportType: null,

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认选中数据*/
	autoSelect: false,
	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 100,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
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
	/**查询栏参数设置*/
	searchToolbarConfig: {},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.getComponent('filterToolbar').on({
			search: function(p, params) {
				me.params = params;
				me.onSearch();
			}
		});
		me.store.on({
			beforeload: function(store, operation, eOpts) {
				me.defaultOrderBy = operation.sorters;
				operation.sorters = [];
				return me.onBeforeLoad();
			},
			load: function(store, records, successful) {
				store.sort(me.defaultOrderBy);
				me.onAfterLoad(records, successful);
			}
		});
	},
	initComponent: function() {
		var me = this,
			config = me.searchToolbarConfig || {};

	
        //创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.sample.SearchToolbar', Ext.apply(config, {
			itemId: 'filterToolbar',
			dock: 'top',
			isLocked: true,
			height: 55
		}))];
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
	
		if(me.reportType) {
			buttonToolbarItems.push('exp_excel');
		}
       buttonToolbarItems=['del',
       {
			width: 60,
			iconCls: 'button-cancel',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '清空',
			tooltip: '<b>清空查询条件</b>',
			handler: function() {
				var filterToolbar=me.getComponent('filterToolbar');
				filterToolbar.onClearSearch();
			}
		}, {
			width: 60,
			iconCls: 'button-search',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '查询',
			tooltip: '<b>查询</b>',
			handler: function() {
				var filterToolbar=me.getComponent('filterToolbar');
				filterToolbar.onFilterSearch();
			}
		}];
		return buttonToolbarItems;
	},
	rendererIsFinanceLockedAndIsLocked: function(value, meta, record, rowIndex, colIndex, store, view) {
		/**
		 * 对账状态有3种：待对账、销售锁定（即：待财务锁定）、财务锁定
		 * IsFinanceLocked=1 财务锁定;  IsLocked=1 销售锁定; IsLocked=0 待对账
		 * 原来逻辑的不变,在列表里新增一个显示列,原来的两列隐藏
		 * 依IsFinanceLocked值和IsLocked值判断处理
		 * IsFinanceLocked财务锁定等于1,就显示为财务锁定,
		 * 如果IsFinanceLocked不为1,判断IsLocked并显示IsLocked值
		 */
		var IsFinanceLocked = record.get("NRequestItem_IsFinanceLocked");
		var IsLocked = record.get("NRequestItem_IsLocked");
		var v = "",
			color = "#FFFFFF";
		if(IsFinanceLocked == "1") {
			v = "财务锁定";
			color = JcallShell.PKI.Enum.Color['E7'];
		} else {
			switch(IsLocked) {
				case "1":
					v = "销售锁定";
					color = JcallShell.PKI.Enum.Color['E10'];
					break;
				case "0":
					v = "待对账";
					color = JcallShell.PKI.Enum.Color['E0'];
					break;
				default:
					v = "待对账";
					color = JcallShell.PKI.Enum.Color['E0'];
					break;
			}
		}
		value = v;
		if(value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
		meta.style = 'background-color:' + color || '#FFFFFF';
		record.set("NRequestItem_ReconciliationState", value);
		//record.commit();
		return value;
	},
	rendererIsGetPriceStyle: function(value, meta, record, rowIndex, colIndex, store, view) {
		var me = this;
		var v = JShell.PKI.Enum.IsGetPriceList[value] || '';
		if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
		var color = "black",
			backgroundcolor = "#FFFFFF";
		if(value == "-1" || value == "-2") {
			color = "#FFFFFF";
			backgroundcolor = JcallShell.PKI.Enum.Color['E-1'];
		} else {
			backgroundcolor = JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
		}
		meta.style = 'color:' + color + ';background-color:' + backgroundcolor;
		return v;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
            dataIndex: 'NRequestItem_ReconciliationState',
            align: 'center',
            text: '对帐状态',
            sortable: true,
            width: 80,
            renderer: function (value, meta, record, rowIndex, colIndex, store, view) {
                return me.rendererIsFinanceLockedAndIsLocked(value, meta, record, rowIndex, colIndex, store, view);
            }
        }, {
            dataIndex: 'NRequestItem_IsLocked',
            align: 'center',
            text: '对帐状态',
            sortable: false,
            hideable: false,
            menuDisabled: true,
            hidden: true,
            width: 75,
            renderer: function (value, meta) {
                var v = JShell.PKI.Enum.IsLocked['E' + value] || '';
                if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
                meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
                return v;
            }
        }, {
            dataIndex: 'NRequestItem_IsFinanceLocked',
            align: 'center',
            sortable: false,
            hideable: false,
            menuDisabled: false,
            text: '财务锁定标志',
            hidden: true,
            width: 80,
            renderer: function (value, meta) {
                var v = JShell.PKI.Enum.IsFinanceLocked['E' + value] || '';
                if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
                meta.style = 'background-color:' + JcallShell.PKI.Enum.IsFinanceLockedColor['E' + value] || '#FFFFFF';
                return v;
            }
        },{
            dataIndex: 'NRequestItem_NRequestForm_BLaboratory_CName',
            text: '送检单位',
            width: 140,
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_NRequestForm_BDept_CName',
            text: '科室',
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_NRequestForm_CName',
            text: '病人姓名',
            width: 80,
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_BTestItem_CName',
            text: '项目名称',
            width: 140,
            defaultRenderer: true
        }, {
			dataIndex: 'NRequestItem_BTestItem_IsCombiItem',
			text: '是否组套',
			width: 70,
			isBool: true,
			align: 'center',
			type: 'bool',defaultRenderer: true
		},{
            dataIndex: 'NRequestItem_NRequestForm_SerialNo',
            text: '样本预制条码',
            width: 90,
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_BarCode',
            text: '实验室条码',
            width: 90,
            defaultRenderer: true
        }, {
            xtype: 'actioncolumn',
            text: '删除',
            align: 'center',
            width: 40,
            style: 'font-weight:bold;color:white;background:orange;',
            hideable: false,
            items: [{
                getClass: function (v, meta, record) {
                    return 'button-del hand';
                },
                handler: function (grid, rowIndex, colIndex) {
                    var rec = grid.getStore().getAt(rowIndex);
                    var id = rec.get(me.PKField);
                    var IsFinanceLocked = rec.get("NRequestItem_IsFinanceLocked");
                    if(IsFinanceLocked == "1") {
                        JShell.Msg.error("所选样本中已经有【财务锁定】的样本<br/>不能执行删除操作!");
                        return;
                    }
                    JShell.Msg.del(function(but) {
			            if (but != "ok") return;
                        me.delOneById(id);
                    });
                }
            }]
        }, {
            dataIndex: 'NRequestItem_IsSpread',
            text: '已返差价',
            width: 60,
            isBool: true,
            align: 'center',
            type: 'bool'
        }, {
            dataIndex: 'NRequestItem_SpreadTime',
            text: '已返差价时间',
            width: 130,
            isDate: true,
            hasTime: true
        }, {
            dataIndex: 'NRequestItem_SpreadMemo',
            text: '<b style="color:blue;">已返差价备注</b>',
            width: 200,
            editor: {},
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_CollectDate',
            text: '采样时间',
            width: 130,
            isDate: true,
            hasTime: true
        }, {
            dataIndex: 'NRequestItem_OperDate',
            text: '录入时间',
            width: 130,
            isDate: true,
            hasTime: true
        }, {
            dataIndex: 'NRequestItem_ReceiveDate',
            text: '核收时间',
            width: 130,
            isDate: true,
            hasTime: true
        }, {
            dataIndex: 'NRequestItem_SenderTime2',
            text: '报告时间',
            width: 130,
            isDate: true,
            hasTime: true
        }, {
            dataIndex: 'NRequestItem_IsStepPrice',
            text: '是否有阶梯价',
            width: 90,
            align: 'center',
            isBool: true,
            type: 'bool'
        }, {
            dataIndex: 'NRequestItem_IsFree',
            text: '是否免单',
            width: 60,
            align: 'center',
            isBool: true,
            type: 'bool'
        }, {
            dataIndex: 'NRequestItem_ItemPriceType',
            text: '价格类型',
            width: 60,
            align: 'center',
            renderer: function (value, meta) {
                var v = JShell.PKI.Enum.ItemPriceType['E' + value] || '';
                if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
                meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
                return v;
            }
        }, {
            dataIndex: 'NRequestItem_IsFreeType',
            text: '免单类型',
            width: 60,
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_ItemFreePrice',
            text: '免单价格',
            align: 'right',
            width: 60,
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_ItemEditPrice',
            text: '终端价',
            align: 'right',
            width: 60,
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_ItemStepPrice',
            text: '阶梯价',
            align: 'right',
            width: 60,
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_ItemContPrice',
            text: '合同价',
            align: 'right',
            width: 60,
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_ItemPrice',
            text: '应收价',
            align: 'right',
            width: 60,
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_FirstLocker',
            text: '对账人',
            width: 80,
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_FirstLockedDate',
            text: '对账时间',
            width: 130,
            isDate: true,
            hasTime: true
        }, {
            dataIndex: 'NRequestItem_LockBatchNumber',
            text: '对账批次号',
            width: 80,
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_BSeller_AreaIn',
            text: '销售区域',
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_BSeller_Name',
            text: '销售',
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_BDealer_Name',
            text: '经销商',
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_CoopLevel',
            align: 'center',
            text: '合作分级',
            width: 60,
            renderer: function (value, meta) {
                var v = JShell.PKI.Enum.CoopLevel['E' + value] || '';
                if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
                meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
                return v;
            }
        }, {
            dataIndex: 'NRequestItem_BillingUnitType',
            align: 'center',
            text: '开票方类型',
            width: 75,
            renderer: function (value, meta) {
                var v = JShell.PKI.Enum.UnitType['E' + value] || '';
                if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
                meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
                return v;
            }
        }, {
            dataIndex: 'NRequestItem_BBillingUnit_Name',
            text: '开票方(付款单位)',
            width: 105,
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_BillingUnitInfo',
            text: '个人开票信息',
            width: 85,
            defaultRenderer: true
        },  {
            dataIndex: 'NRequestItem_IsGetPrice',
            align: 'center',
            text: '匹配状态',
            sortable: true,
            width: 100,
            renderer: function (value, meta, record, rowIndex, colIndex, store, view) {
                return me.rendererIsGetPriceStyle(value, meta, record, rowIndex, colIndex, store, view);
            }
        }, {
            dataIndex: 'NRequestItem_GetPriceUser',
            text: '匹配人',
            defaultRenderer: true
        }, {
            dataIndex: 'NRequestItem_GetPriceTime',
            text: '匹配时间',
            width: 130,
            isDate: true,
            hasTime: true
        },{
            dataIndex: 'NRequestItem_SampleState',
            align: 'center',
            text: '样本状态',
            width: 70,
            renderer: function (value, meta) {
                var v = JShell.PKI.Enum.SampleStateList[value] || '';
                if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
                meta.style = 'background-color:' + JcallShell.PKI.Enum.SampleStateColor[value] || '#FFFFFF';
                return v;
            }
        }, {
            dataIndex: 'NRequestItem_Id',
            text: '主键ID',
            hidden: true,
            hideable: false,
            isKey: true
        }];

		return columns;
	},

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
//		me.doFilterParams();

		var arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true);

		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";

        var params = me.params || me.getComponent('filterToolbar').getParams();
         //开始时间
	    if(params.BeginDate) {
	    	url +="&startDate="+params.BeginDate;
		}
	    if(params.EndDate) {
			url +="&endDate="+params.EndDate;
		}
	    
	    //送检单位
	    if(params.Laboratory_Id){
	    	url +="&labID="+params.Laboratory_Id;
	    }
	     //预制条码
	    if(params.SerialNo){
	    	url +="&serialNo="+params.SerialNo;
	    }
	     //上机条码
	    if(params.BarCode) {
	    	url +="&barCode="+params.BarCode;
		}
	    //样本状态
	    if(params.SampleState) {
	    	url +="&sampleState="+params.SampleState;
		}
	     //姓名
	    if(params.NRequestForm_CName) {
	    	url +="&patName="+params.NRequestForm_CName;
		}
		return url;
    },
	
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
      //是否需要作判断处理
        var isBreak = false;
        for (var i in records) {
            isBreak = records[i].get("NRequestItem_IsFinanceLocked");
            switch (isBreak) {
                case "1":
                    isBreak = true;
                    break;
                default:
                    isBreak = false;
                    break;
            }
            if (isBreak) {
                break;
            }
        }
        if (isBreak) {
            JShell.Msg.error("所选样本中已经有【财务锁定】的样本<br/>不能执行删除操作!");
            return;
        }
        
		var idlist='';
		for (var i in records) {
			idlist+=','+records[i].get(me.PKField);
		}
        idlist=0==idlist.indexOf(",")?idlist.substr(1):idlist;
		JShell.Msg.del(function(but) {
			if (but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;
            
			me.showMask(me.delText); //显示遮罩层
			me.delOneById(idlist);
		});
	},
	/**删除一条数据*/
	delOneById: function(idlist) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
        var entity={idList:idlist};
        var params = Ext.JSON.encode(entity);
		setTimeout(function() {
			JShell.Server.post(url, params, function (data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {
					me.onSearch();
				} else{
					JShell.Msg.error(data.msg);
				}
			});
		});
	}
});