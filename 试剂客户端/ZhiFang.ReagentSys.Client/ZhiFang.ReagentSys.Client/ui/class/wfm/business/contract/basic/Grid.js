/**
 * 合同基础列表
 * @author Jcall
 * @version 2016-10-31
 */
Ext.define('Shell.class.wfm.business.contract.basic.Grid', {
    extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.toolbar.Button'
	],
    title: '合同基础列表',
    width: 800,
    height: 500,

    /**获取数据服务路径*/
    selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPContractByHQL?isPlanish=true',
    /**修改服务地址*/
    editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePContractByField',
    /**删除数据服务路径*/
    delUrl: '/SingleTableService.svc/ST_UDTO_DelPContract',

    /**默认加载*/
    defaultLoad: true,

    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用查询框*/
    hasSearch: true,
    /**合同类型*/
	ProjectType: 'ContracType',
    defaultOrderBy: [{ property: 'PContract_DispOrder', direction: 'ASC' }],
     /**状态默认值*/
    defaultStatusValue:'',
    /**默认时间类型*/
	defaultDateType:'DataAddTime',
	/**时间类型列表*/
	DateTypeList:[
		['DataAddTime','创建时间'],['ApplyDate','申请时间'],
		['SignDate','签署日期'],['ReviewDate','商务评审时间'],
		['TechReviewDate','技术评审时间']
	],
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        
        me.on({
            itemdblclick: function (view, record) {
                var id = record.get(me.PKField);
                me.openShowForm(id);//查询合同信息
            }
        });
        me.initListeners();
    },

    initComponent: function () {
        var me = this;
        //数据列
        me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
        me.callParent(arguments);
    },
    
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [{
            text: '合同编号', dataIndex: 'PContract_ContractNumber', width: 100,
            sortable: false, defaultRenderer: true
        }, {
            text: '客户名称', dataIndex: 'PContract_PClientName', width: 150,
            sortable: false, defaultRenderer: true
        }, {
            text: '合同名称', dataIndex: 'PContract_Name', width: 150,
            sortable: false, defaultRenderer: true
        }, {
			xtype: 'actioncolumn',text:'交流',align:'center',width:40,
			style:'font-weight:bold;color:white;background:orange;',
			sortable:false,hideable:false,
			items: [{
				iconCls:'button-interact hand',
				tooltip:'<b>交流</b>',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.showInteractionById(id);
				}
			}]
		}, {
            text: '项目类别', dataIndex: 'PContract_Content', width: 70,
            sortable: false, defaultRenderer: true
        }, {
            text: '合同状态', dataIndex: 'PContract_ContractStatus', width: 70,
            sortable: false, renderer:function(value,meta){
            	var v = value || '';
            	
            	if(v){
            		var info = JShell.System.ClassDict.getClassInfoById('PContractStatus',v);
            		if(info){
            			v = info.Name;
            			meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
            		}
            	}
            	return v;
            }
        }, {
            text: '合同总额', dataIndex: 'PContract_Amount', width: 100,
            sortable: false, defaultRenderer: true
        }, {
            text: '单项仪器个数', dataIndex: 'PContract_EquipOneWayCount', width: 80,
            sortable: false, defaultRenderer: true
        }, {
            text: '双向仪器个数', dataIndex: 'PContract_EquipTwoWayCount', width: 80,
            sortable: false, defaultRenderer: true
        }, {
            text: '签署人', dataIndex: 'PContract_SignMan', width: 70,
            sortable: false, defaultRenderer: true
        }, {
            text: '销售负责人', dataIndex: 'PContract_Principal', width: 70,
            sortable: false, defaultRenderer: true
        }, {
            text: '申请人', dataIndex: 'PContract_ApplyMan', width: 70,
            sortable: false, defaultRenderer: true
        }, {
            text: '申请时间', dataIndex: 'PContract_ApplyDate', width: 130,
            sortable: false,isDate: true, hasTime: true, defaultRenderer: true
        }, {
            text: '评审人', dataIndex: 'PContract_ReviewMan', width: 70,
            sortable: false, defaultRenderer: true
        }, {
            text: '评审时间', dataIndex: 'PContract_ReviewDate', width: 130,
            sortable: false,isDate: true, hasTime: true, defaultRenderer: true
        }, {
            text: '创建时间', dataIndex: 'PContract_DataAddTime', width: 130,
            isDate: true, hasTime: true, hidden: true
        }, {
            text: '备注', dataIndex: 'PContract_Memo', width: 150,
            sortable: false, defaultRenderer: true
        }, {
            text: '主键ID', dataIndex: 'PContract_Id', isKey: true, hidden: true, hideable: false
        }, {
            text: '销售负责人ID', dataIndex: 'PContract_PrincipalID',  hidden: true, hideable: false
        }];

        return columns;
    },
    /**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
			if(!JShell.System.ClassDict.PContractStatus){
    			JShell.Msg.error('未获取到合同状态，请刷新列表');
    			return;
    		}
			var StatusID = me.getComponent('buttonsToolbar').getComponent('StatusID');
			var List=JShell.System.ClassDict.PContractStatus;
			
			if(StatusID.store.data.items.length==0){
			     StatusID.loadData(me.getStatusData(List));
			     StatusID.setValue(me.defaultStatusValue);
			}
			me.load(null, true, autoSelect);
    	});
	},
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			PayOrgID=null,StatusID=null,
			PClientID=null,ContentID=null,
			DateType = null,BeginDate = null,EndDate = null,
			search = null,params = [];
			
		//改变默认条件
		me.changeDefaultWhere();
			
		me.internalWhere = '';
			
		if(buttonsToolbar){
			StatusID= buttonsToolbar.getComponent('StatusID').getValue();
		}
		if(buttonsToolbar2){
			PayOrgID = buttonsToolbar2.getComponent('PContract_PayOrgID').getValue();
		    PClientID = buttonsToolbar2.getComponent('PContract_PClientID').getValue();
		    ContentID  = buttonsToolbar2.getComponent('PContract_ContentID').getValue();
		    DateType = buttonsToolbar2.getComponent('DateType').getValue();
			BeginDate = buttonsToolbar2.getComponent('BeginDate').getValue();
			EndDate = buttonsToolbar2.getComponent('EndDate').getValue();
			search = buttonsToolbar2.getComponent('search').getValue();
		}
		//PayOrgID
		if(PayOrgID) {
			params.push("pcontract.PayOrgID='" + PayOrgID + "'");
		}
		//PClientID
		if(PClientID) {
			params.push("pcontract.PClientID='" + PClientID + "'");
		}
		//ContentID
		if(ContentID) {
			params.push("pcontract.ContentID='" + ContentID + "'");
		}
		//状态
		if(StatusID) {
			params.push("pcontract.ContractStatus='" + StatusID + "'");
		}		
		//时间
		if(DateType) {
			if(BeginDate) {
				params.push("pcontract." + DateType + ">='" + JShell.Date.toString(BeginDate, true) + "'");
			}
			if(EndDate) {
				params.push("pcontract." + DateType + "<'" + JShell.Date.toString(JShell.Date.getNextDate(EndDate), true) + "'");
			}
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		
//		
//		if(search){
//			me.internalWhere = me.getSearchWhere(search);
//		}
		
		return me.callParent(arguments);
	},
	/**改变默认条件*/
	changeDefaultWhere:function(){
		var me = this;
		
		//defaultWhere追加上IsUse约束
		if(me.defaultWhere){
			var index = me.defaultWhere.indexOf('pcontract.IsUse=1');
			if(index == -1){
				me.defaultWhere += ' and pcontract.IsUse=1';
			}
		}else{
			me.defaultWhere = 'pcontract.IsUse=1';
		}
	},
    /**查询合同信息*/
	openShowForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.contract.basic.ShowTabPanel', {
			SUB_WIN_NO:'101',//内部窗口编号
            //resizable:false,
			title:'合同信息',
			PK:id
		}).show();
	},
	/**交流*/
	showInteractionById:function(id){
		var me = this;
		JShell.Win.open('Shell.class.sysbase.scinteraction.App', {
			//resizable: false,
			title:'交流信息',
			PK:id
		}).show();
	},
	/**获取当前用户所负责的客户Ids数组*/
	getClientIds:function(callback){
		var me = this;
		
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var fields = ['PSalesManClientLink_PClientID'];
		var where = 'psalesmanclientlink.SalesManID=' + userId
		
		var url = '/SingleTableService.svc/ST_UDTO_SearchPSalesManClientLinkByHQL';
		url += '?isPlanish=true&fields=' + fields.join(',') + '&where=' + where;
		url = JShell.System.Path.getRootUrl(url);
		
		JShell.Server.get(url,function(data){
			if(data.success){
				if(Ext.typeOf(callback)){
					var ids = [];
					for(var i in data.value.list){
						ids.push(data.value.list[i].PSalesManClientLink_PClientID);
					}
					callback(ids);
				}
			}else{
				JShell.Msg.error('客户销售关系获取错误：' + data.msg);
			}
		});
	},
		/**获取状态列表*/
	getStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
				var style = ['font-weight:bold;text-align:center'];
				if(obj.BGColor) {
					style.push('color:' + obj.BGColor);
				}
				data.push([obj.Id, obj.Name, style.join(';')]);
		}
		return data;
	},
		/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
    /**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];

		buttonToolbarItems.unshift('refresh','-');
			
		buttonToolbarItems.push({
			width:140,labelWidth:35,labelAlign:'right',hasStyle:true,//multiSelect:true,
			xtype:'uxSimpleComboBox',itemId:'StatusID',fieldLabel:'状态',emptyText:'状态',
//			data:me.getStatusData(),
			value: me.defaultStatusValue
		});
		
		
//		if(me.hasDept){
//		    buttonToolbarItems.push('-',);
//		}
		return buttonToolbarItems;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this;
					
	   //查询框信息
        me.searchInfo = {
            width: 220, emptyText: '合同编号/合同名称/销售负责人', isLike: true,itemId:'search',
            fields: ['pcontract.ContractNumber','pcontract.Name','pcontract.Principal']
        };
		
		var items = {
			xtype:'uxButtontoolbar',
			dock:'top',
			itemId:'buttonsToolbar2',
			items:[{
				fieldLabel:'客户',name:'PContract_PClientName',itemId:'PContract_PClientName',emptyText:'客户',
			    xtype:'uxCheckTrigger',className:'Shell.class.wfm.client.CheckGrid',labelWidth:35,width:155,labelAlign:'right'
			}, {
				fieldLabel: '客户选择',	emptyText: '客户选择',name: 'PContract_PClientID',
				itemId: 'PContract_PClientID',hidden: true,xtype: 'textfield'
			},{
				fieldLabel:'付款单位',name:'PContract_PayOrg',itemId:'PContract_PayOrg',emptyText:'付款单位',
			    xtype:'uxCheckTrigger',className:'Shell.class.wfm.client.CheckGrid',
			    labelWidth:60,width:180,labelAlign:'right'
			}, {
				fieldLabel: '付款单位选择',emptyText: '付款单位选择',name: 'PContract_PayOrgID',
				itemId: 'PContract_PayOrgID',hidden: true,	xtype: 'textfield'
			},{
				fieldLabel: '合同类型',name: 'PContract_Content',xtype: 'uxCheckTrigger',emptyText:'合同类型',
				itemId: 'PContract_Content',className: 'Shell.class.wfm.dict.CheckGrid',labelWidth:60,width:180,labelAlign:'right',
				classConfig: {
					title: '合同类型选择',
					defaultWhere: "pdict.BDictType.DictTypeCode='" + this.ProjectType + "'"
				}
			}, {
				fieldLabel: '合同类型选择',
				emptyText: '合同类型选择',
				name: 'PContract_ContentID',
				itemId: 'PContract_ContentID',
				hidden: true,
				xtype: 'textfield'
			},{
				width:160,labelWidth:60,labelAlign:'right',
				xtype:'uxSimpleComboBox',itemId:'DateType',fieldLabel:'时间范围',
				data:me.DateTypeList,
				value:me.defaultDateType
			},{
				width:95,itemId:'BeginDate',xtype:'datefield',format:'Y-m-d'
			},{
				width:100,labelWidth:5,fieldLabel:'-',labelSeparator:'',
				itemId:'EndDate',xtype:'datefield',format:'Y-m-d'
			},'->',{
			type: 'search',
			info: me.searchInfo
		}]
		};
		
		return items;
	},
	/**综合查询*/
	onGridSearch:function(){
		var me = this;
		JShell.Action.delay(function(){
			me.onSearch();
		},100);
	},
	initListeners:function(){
    	var me=this;
        var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
    	 //客户
		var PClientName = buttonsToolbar2.getComponent('PContract_PClientName'),
			PClientID = buttonsToolbar2.getComponent('PContract_PClientID');
			
		PClientName.on({
			check: function(p, record) {
				PClientName.setValue(record ? record.get('PClient_Name') : '');
				PClientID.setValue(record ? record.get('PClient_Id') : '');
				p.close();
			},
			change:function(){me.onGridSearch();}
		});
		
		//付款单位
		var PayOrg = buttonsToolbar2.getComponent('PContract_PayOrg'),
			PayOrgID = buttonsToolbar2.getComponent('PContract_PayOrgID');
			
		PayOrg.on({
			check: function(p, record) {
				PayOrg.setValue(record ? record.get('PClient_Name') : '');
				PayOrgID.setValue(record ? record.get('PClient_Id') : '');
				p.close();
			},change:function(){me.onGridSearch();}
		});
		//项目类别
		var Content = buttonsToolbar2.getComponent('PContract_Content'),
		ContentID = buttonsToolbar2.getComponent('PContract_ContentID');
		Content.on({
			check: function(p, record) {
				Content.setValue(record ? record.get('BDict_CName') : '');
				ContentID.setValue(record ? record.get('BDict_Id') : '');
				p.close();
			},change:function(){me.onGridSearch();}
		});
       //时间类型+时间
		var DateType = buttonsToolbar2.getComponent('DateType'),
			BeginDate = buttonsToolbar2.getComponent('BeginDate'),
			EndDate = buttonsToolbar2.getComponent('EndDate');
		if(DateType) {
			DateType.on({
				change: function(com, newValue, oldValue, eOpts) {
					if(BeginDate.getValue() && EndDate.getValue()){
						me.onGridSearch();
					}
				}
			});
		}
		if(BeginDate) {
			BeginDate.on({
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && EndDate.getValue()){
						me.onGridSearch();
					}				
				}
			});
		}
		if(EndDate) {
			EndDate.on({
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && BeginDate.getValue()){
						me.onGridSearch();
					}
				}
			});
		}
	    var buttonsToolbar = me.getComponent('buttonsToolbar');
		var	StatusID= buttonsToolbar.getComponent('StatusID');
        if(StatusID) {
			StatusID.on({
				change: function(com, newValue, oldValue, eOpts) {
					me.onGridSearch();
				}
			});
		}
    }
});