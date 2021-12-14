/**
 * 项目跟踪列表
 * @author liangyl	
 * @version 2017-08-07
 */
Ext.define('Shell.class.wfm.business.projecttrack.follow.basic.Grid', {
    extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.toolbar.Button'
	],
    title: '项目跟踪基础列表',
    width: 800,
    height: 500,

    /**获取数据服务路径*/
    selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractFollowByHQL?isPlanish=true',
    /**修改服务地址*/
    editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractFollowByField',
    /**删除数据服务路径*/
    delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPContractFollow',

    /**默认加载*/
    defaultLoad: true,

    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用查询框*/
    hasSearch: true,
    /**项目跟踪类型*/
	ProjectType: 'ContracType',
    defaultOrderBy: [{ property: 'PContractFollow_DispOrder', direction: 'ASC' }],
     /**状态默认值*/
    defaultStatusValue:'',
    /**默认时间类型*/
	defaultDateType:'DataAddTime',
	/**时间类型列表*/
	DateTypeList:[
		['DataAddTime','创建时间'],['ApplyDate','申请时间'],
		['SignDate','计划签署日期']
	],
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        
        me.on({
            itemdblclick: function (view, record) {
                var id = record.get(me.PKField);
                me.openShowForm(id);//查询项目跟踪信息
            }
        });
        me.initListeners();
    },

    initComponent: function () {
        var me = this;
        me.addEvents('saveInteraction');
        //数据列
        me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
        me.callParent(arguments);
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
	/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this;
	   //查询框信息
        me.searchInfo = {
            width: 165, emptyText: '项目编号/项目名称/负责人', isLike: true,itemId:'search',
            fields: ['pcontractfollow.ContractNumber','pcontractfollow.Name','pcontractfollow.Principal']
        };
		var items = {
			xtype:'uxButtontoolbar',
			dock:'top',
			itemId:'buttonsToolbar2',
			items:[{
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
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [{
            text: '项目跟踪编号', dataIndex: 'PContractFollow_ContractNumber', width: 100,
            sortable: false, defaultRenderer: true
        }, {
            text: '客户名称', dataIndex: 'PContractFollow_PClientName', width: 150,
            sortable: false, defaultRenderer: true
        }, {
            text: '项目跟踪名称', dataIndex: 'PContractFollow_Name', width: 150,
            sortable: false, defaultRenderer: true
        }, {
			xtype: 'actioncolumn',text:'跟踪记录',align:'center',width:70,
			style:'font-weight:bold;color:white;background:orange;',
			sortable:false,hideable:false,
			items: [{
				iconCls:'button-interact hand',
				tooltip:'<b>跟踪记录</b>',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.showInteractionById(id);
				}
			}]
		}, {
            text: '项目类别', dataIndex: 'PContractFollow_Content', width: 70,
            sortable: false, defaultRenderer: true
        },  {
            text: '项目跟踪总额', dataIndex: 'PContractFollow_Amount', width: 100,
            sortable: false, defaultRenderer: true
        }, {
            text: '单项仪器个数', dataIndex: 'PContractFollow_EquipOneWayCount', width: 80,
            sortable: false, defaultRenderer: true
        }, {
            text: '双向仪器个数', dataIndex: 'PContractFollow_EquipTwoWayCount', width: 80,
            sortable: false, defaultRenderer: true
        }, {
            text: '签署人', dataIndex: 'PContractFollow_SignMan', width: 70,
            sortable: false, defaultRenderer: true
        }, {
            text: '销售负责人', dataIndex: 'PContractFollow_Principal', width: 70,
            sortable: false, defaultRenderer: true
        }, {
            text: '申请人', dataIndex: 'PContractFollow_ApplyMan', width: 70,
            sortable: false, defaultRenderer: true
        }, {
            text: '申请时间', dataIndex: 'PContractFollow_ApplyDate', width: 130,
            sortable: false,isDate: true, hasTime: true, defaultRenderer: true
        },{
            text: '创建时间', dataIndex: 'PContractFollow_DataAddTime', width: 130,
            isDate: true, hasTime: true, hidden: true
        }, {
            text: '备注', dataIndex: 'PContractFollow_Memo', width: 150,
            sortable: false, defaultRenderer: true
        }, {
            text: '主键ID', dataIndex: 'PContractFollow_Id', isKey: true, hidden: true, hideable: false
        }, {
            text: '销售负责人ID', dataIndex: 'PContractFollow_PrincipalID',  hidden: true, hideable: false
        }];

        return columns;
    },
   
	/**改变默认条件*/
	changeDefaultWhere:function(){
		var me = this;
		
		//defaultWhere追加上IsUse约束
		if(me.defaultWhere){
			var index = me.defaultWhere.indexOf('pcontractfollow.IsUse=1');
			if(index == -1){
				me.defaultWhere += ' and pcontractfollow.IsUse=1';
			}
		}else{
			me.defaultWhere = 'pcontractfollow.IsUse=1';
		}
	},
    /**查询项目跟踪信息*/
	openShowForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.projecttrack.follow.basic.ShowTabPanel', {
			SUB_WIN_NO:'101',//内部窗口编号
            //resizable:false,
			title:'项目跟踪信息',
			PK:id
		}).show();
	},
	/**交流*/
	showInteractionById:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.projecttrack.interaction.App', {
			//resizable: false,
			title:'跟踪记录信息',
			PK:id,
			width: 700,
	        height: 500,
			listeners:{
                save:function(p){
                   me.fireEvent('saveInteraction', p);
                }
            }
		}).show();
	},
	
    /**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];

		buttonToolbarItems.unshift('refresh','-',{
				fieldLabel:'客户',name:'PContractFollow_PClientName',itemId:'PContractFollow_PClientName',emptyText:'客户',
			    xtype:'uxCheckTrigger',className:'Shell.class.wfm.client.CheckGrid',labelWidth:35,width:155,labelAlign:'right'
			}, {
				fieldLabel: '客户选择',	emptyText: '客户选择',name: 'PContractFollow_PClientID',
				itemId: 'PContractFollow_PClientID',hidden: true,xtype: 'textfield'
			},{
				fieldLabel:'付款单位',name:'PContractFollow_PayOrg',itemId:'PContractFollow_PayOrg',emptyText:'付款单位',
			    xtype:'uxCheckTrigger',className:'Shell.class.wfm.client.CheckGrid',
			    labelWidth:60,width:180,labelAlign:'right'
			}, {
				fieldLabel: '付款单位选择',emptyText: '付款单位选择',name: 'PContractFollow_PayOrgID',
				itemId: 'PContractFollow_PayOrgID',hidden: true,	xtype: 'textfield'
			},{
				fieldLabel: '项目跟踪类型',name: 'PContractFollow_Content',xtype: 'uxCheckTrigger',emptyText:'项目跟踪类型',
				itemId: 'PContractFollow_Content',className: 'Shell.class.wfm.dict.CheckGrid',labelWidth:80,width:200,labelAlign:'right',
				classConfig: {
					title: '项目跟踪类型选择',
					defaultWhere: "pdict.PDictType.DictTypeCode='" + this.ProjectType + "'"
				}
			}, {
				fieldLabel: '项目跟踪类型选择',
				emptyText: '项目跟踪类型选择',
				name: 'PContractFollow_ContentID',
				itemId: 'PContractFollow_ContentID',
				hidden: true,
				xtype: 'textfield'
			});
			
		return buttonToolbarItems;
	},
	initListeners:function(){
    	var me=this;
        var buttonsToolbar = me.getComponent('buttonsToolbar');

        var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
    	 //客户
		var PClientName = buttonsToolbar.getComponent('PContractFollow_PClientName'),
			PClientID = buttonsToolbar.getComponent('PContractFollow_PClientID');
			
		PClientName.on({
			check: function(p, record) {
				PClientName.setValue(record ? record.get('PClient_Name') : '');
				PClientID.setValue(record ? record.get('PClient_Id') : '');
				p.close();
			},
			change:function(){me.onGridSearch();}
		});
		
		//付款单位
		var PayOrg = buttonsToolbar.getComponent('PContractFollow_PayOrg'),
			PayOrgID = buttonsToolbar.getComponent('PContractFollow_PayOrgID');
			
		PayOrg.on({
			check: function(p, record) {
				PayOrg.setValue(record ? record.get('PClient_Name') : '');
				PayOrgID.setValue(record ? record.get('PClient_Id') : '');
				p.close();
			},change:function(){me.onGridSearch();}
		});
		//项目类别
		var Content = buttonsToolbar.getComponent('PContractFollow_Content'),
		ContentID = buttonsToolbar.getComponent('PContractFollow_ContentID');
		Content.on({
			check: function(p, record) {
				Content.setValue(record ? record.get('PDict_CName') : '');
				ContentID.setValue(record ? record.get('PDict_Id') : '');
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
		
   },
     /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			PayOrgID=null,
			PClientID=null,ContentID=null,
			DateType = null,BeginDate = null,EndDate = null,
			search = null,params = [];
			
		//改变默认条件
		me.changeDefaultWhere();
			
		me.internalWhere = '';
			
		if(buttonsToolbar2){
			PayOrgID = buttonsToolbar.getComponent('PContractFollow_PayOrgID').getValue();
		    PClientID = buttonsToolbar.getComponent('PContractFollow_PClientID').getValue();
		    ContentID  = buttonsToolbar.getComponent('PContractFollow_ContentID').getValue();
		    DateType = buttonsToolbar2.getComponent('DateType').getValue();
			BeginDate = buttonsToolbar2.getComponent('BeginDate').getValue();
			EndDate = buttonsToolbar2.getComponent('EndDate').getValue();
			search = buttonsToolbar2.getComponent('search').getValue();
		}
		//PayOrgID
		if(PayOrgID) {
			params.push("pcontractfollow.PayOrgID='" + PayOrgID + "'");
		}
		//PClientID
		if(PClientID) {
			params.push("pcontractfollow.PClientID='" + PClientID + "'");
		}
		//ContentID
		if(ContentID) {
			params.push("pcontractfollow.ContentID='" + ContentID + "'");
		}

		//时间
		if(DateType) {
			if(BeginDate) {
				params.push("pcontractfollow." + DateType + ">='" + JShell.Date.toString(BeginDate, true) + "'");
			}
			if(EndDate) {
				params.push("pcontractfollow." + DateType + "<'" + JShell.Date.toString(JShell.Date.getNextDate(EndDate), true) + "'");
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
		return me.callParent(arguments);
	},
	
	/**综合查询*/
	onGridSearch:function(){
		var me = this;
		JShell.Action.delay(function(){
			me.onSearch();
		},100);
	}
	
});